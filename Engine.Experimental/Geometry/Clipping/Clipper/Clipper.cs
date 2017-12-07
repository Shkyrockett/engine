/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System;
using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// Clipper 
    /// </summary>
    public class Clipper
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        internal ScanLine Scanline;

        /// <summary>
        /// 
        /// </summary>
        internal bool HasOpenPaths;

        /// <summary>
        /// 
        /// </summary>
        internal int CurrentLocMinIdx;

        /// <summary>
        /// 
        /// </summary>
        internal bool LocMinListSorted;

        /// <summary>
        /// 
        /// </summary>
        internal List<List<Vertex>> VertexList = new List<List<Vertex>>();

        /// <summary>
        /// 
        /// </summary>
        internal List<OutRec> OutRecList = new List<OutRec>();

        /// <summary>
        /// 
        /// </summary>
        internal Edge Actives;

        /// <summary>
        /// 
        /// </summary>
        private Edge SEL;

        /// <summary>
        /// 
        /// </summary>
        private List<LocalMinima> LocMinimaList = new List<LocalMinima>();

        /// <summary>
        /// 
        /// </summary>
        IComparer<LocalMinima> LocalMinimaComparer = new MyLocalMinSort();

        /// <summary>
        /// 
        /// </summary>
        private List<IntersectNode> IntersectList = new List<IntersectNode>();

        /// <summary>
        /// 
        /// </summary>
        IComparer<IntersectNode> IntersectNodeComparer = new MyIntersectNodeSort();

        /// <summary>
        /// 
        /// </summary>
        private ClipingOperations ClipType;

        /// <summary>
        /// 
        /// </summary>
        private WindingRules FillType;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            LocMinimaList.Clear();
            CurrentLocMinIdx = 0;
            VertexList.Clear();
            HasOpenPaths = false;
        }

        /// <summary>
        /// 
        /// </summary>
        virtual public void CleanUp()
        {
            while (Actives != null)
            {
                DeleteFromAEL(Actives);
            }

            DisposeScanLineList();
            OutRecList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Reset()
        {
            if (!LocMinListSorted)
            {
                LocMinimaList.Sort(LocalMinimaComparer);
                LocMinListSorted = true;
            }
            foreach (LocalMinima locMin in LocMinimaList)
            {
                InsertScanline(locMin.Vertex.Point.Y);
            }

            CurrentLocMinIdx = 0;
            Actives = null;
            SEL = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Y"></param>
        private void InsertScanline(double Y)
        {
            //single-linked list: sorted descending, ignoring dups.
            if (Scanline == null)
            {
                Scanline = new ScanLine
                {
                    NextScanLine = null,
                    Y = Y
                };
            }
            else if (Y > Scanline.Y)
            {
                var newSb = new ScanLine
                {
                    Y = Y,
                    NextScanLine = Scanline
                };
                Scanline = newSb;
            }
            else
            {
                var sb2 = Scanline;
                while (sb2.NextScanLine != null && (Y <= sb2.NextScanLine.Y))
                {
                    sb2 = sb2.NextScanLine;
                }

                if (Y == sb2.Y)
                {
                    return; //ie ignores duplicates
                }

                var newSb = new ScanLine
                {
                    Y = Y,
                    NextScanLine = sb2.NextScanLine
                };
                sb2.NextScanLine = newSb;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Y"></param>
        /// <returns></returns>
        internal bool PopScanline(out double Y)
        {
            if (Scanline == null)
            {
                Y = 0;
                return false;
            }
            Y = Scanline.Y;
            var tmp = Scanline.NextScanLine;
            Scanline = null;
            Scanline = tmp;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisposeScanLineList()
        {
            while (Scanline != null)
            {
                var tmp = Scanline.NextScanLine;
                Scanline = null;
                Scanline = tmp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="locMin"></param>
        /// <returns></returns>
        private bool PopLocalMinima(double Y, out LocalMinima locMin)
        {
            locMin = null;
            if (CurrentLocMinIdx == LocMinimaList.Count)
            {
                return false;
            }

            locMin = LocMinimaList[CurrentLocMinIdx];
            if (locMin.Vertex.Point.Y == Y)
            {
                CurrentLocMinIdx++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vert"></param>
        /// <param name="pt"></param>
        /// <param name="isOpen"></param>
        private void AddLocMin(Vertex vert, PolygonRelations pt, bool isOpen)
        {
            //make sure the vertex is added only once ...
            if ((VertexFlags.LocMin & vert.Flags) != 0)
            {
                return;
            }

            vert.Flags |= VertexFlags.LocMin;
            var lm = new LocalMinima
            {
                Vertex = vert,
                PathType = pt,
                IsOpen = isOpen
            };
            LocMinimaList.Add(lm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pt"></param>
        /// <param name="isOpen"></param>
        private void AddPathToVertexList(PolygonContour p, PolygonRelations pt, bool isOpen)
        {
            var pathLen = p.Count;
            while (pathLen > 1 && p[pathLen - 1] == p[0])
            {
                pathLen--;
            }

            if (pathLen < 2)
            {
                return;
            }

            var P0IsMinima = false;
            var P0IsMaxima = false;
            var goingUp = false;
            var i = 1;
            //find the first non-horizontal segment in the path ...
            while (i < pathLen && p[i].Y == p[0].Y)
            {
                i++;
            }

            if (i == pathLen) //it's a totally flat path
            {
                if (!isOpen)
                {
                    return;       //Ignore closed paths that have ZERO area.
                }
            }
            else
            {
                goingUp = p[i].Y < p[0].Y; //because I'm using an inverted Y-axis display
                if (goingUp)
                {
                    i = pathLen - 1;
                    while (p[i].Y == p[0].Y)
                    {
                        i--;
                    }

                    P0IsMinima = p[i].Y < p[0].Y; //p[0].Y == a minima
                }
                else
                {
                    i = pathLen - 1;
                    while (p[i].Y == p[0].Y)
                    {
                        i--;
                    }

                    P0IsMaxima = p[i].Y > p[0].Y; //p[0].Y == a maxima
                }
            }

            var va = new List<Vertex>(pathLen);
            VertexList.Add(va);
            var v = new Vertex(p[0]);
            if (isOpen)
            {
                v.Flags = VertexFlags.OpenStart;
                if (goingUp)
                {
                    AddLocMin(v, pt, isOpen);
                }
                else
                {
                    v.Flags |= VertexFlags.LocMax;
                }
            }
            va.Add(v);
            //nb: polygon orientation is determined later (see InsertLocalMinimaIntoAEL).
            for (var j = 1; j < pathLen; j++)
            {
                if (p[j] == v.Point)
                {
                    continue; //ie skips duplicates
                }

                var v2 = new Vertex(p[j]);
                v.NextVertex = v2;
                v2.PreviousVertex = v;
                if (v2.Point.Y > v.Point.Y && goingUp)
                {
                    v.Flags |= VertexFlags.LocMax;
                    goingUp = false;
                }
                else if (v2.Point.Y < v.Point.Y && !goingUp)
                {
                    goingUp = true;
                    AddLocMin(v, pt, isOpen);
                }
                va.Add(v2);
                v = v2;
            }
            //i: index of the last vertex in the path.
            v.NextVertex = va[0];
            va[0].PreviousVertex = v;

            if (isOpen)
            {
                v.Flags |= VertexFlags.OpenEnd;
                if (goingUp)
                {
                    v.Flags |= VertexFlags.LocMax;
                }
                else
                {
                    AddLocMin(v, pt, isOpen);
                }
            }
            else if (goingUp)
            {
                //going up so find local maxima ...
                while (v.NextVertex.Point.Y <= v.Point.Y)
                {
                    v = v.NextVertex;
                }

                v.Flags |= VertexFlags.LocMax;
                if (P0IsMinima)
                {
                    AddLocMin(va[0], pt, isOpen); //ie just turned to going up
                }
            }
            else
            {
                //going down so find local minima ...
                while (v.NextVertex.Point.Y >= v.Point.Y)
                {
                    v = v.NextVertex;
                }

                AddLocMin(v, pt, isOpen);
                if (P0IsMaxima)
                {
                    va[0].Flags |= VertexFlags.LocMax;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pt"></param>
        /// <param name="isOpen"></param>
        public void AddPath(PolygonContour path, PolygonRelations pt, bool isOpen = false)
        {
            if (isOpen)
            {
                if (pt == PolygonRelations.Clipping)
                {
                    throw new EngineException("AddPath: Only PathType.Subject paths can be open.");
                }

                HasOpenPaths = true;
            }
            AddPathToVertexList(path, pt, isOpen);
            LocMinListSorted = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="pt"></param>
        /// <param name="isOpen"></param>
        public void AddPaths(Polygon paths, PolygonRelations pt, bool isOpen = false)
        {
            foreach (PolygonContour path in paths)
            {
                AddPath(path, pt, isOpen);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsContributingClosed(Edge e)
        {
            switch (FillType)
            {
                case WindingRules.NonZero:
                    if (Abs(e.WindCnt) != 1)
                    {
                        return false;
                    }

                    break;
                case WindingRules.Positive:
                    if (e.WindCnt != 1)
                    {
                        return false;
                    }

                    break;
                case WindingRules.Negative:
                    if (e.WindCnt != -1)
                    {
                        return false;
                    }

                    break;
            }

            switch (ClipType)
            {
                case ClipingOperations.Intersection:
                    switch (FillType)
                    {
                        case WindingRules.EvenOdd:
                        case WindingRules.NonZero:
                            return (e.WindCnt2 != 0);
                        case WindingRules.Positive:
                            return (e.WindCnt2 > 0);
                        case WindingRules.Negative:
                            return (e.WindCnt2 < 0);
                    }
                    break;
                case ClipingOperations.Union:
                    switch (FillType)
                    {
                        case WindingRules.EvenOdd:
                        case WindingRules.NonZero:
                            return (e.WindCnt2 == 0);
                        case WindingRules.Positive:
                            return (e.WindCnt2 <= 0);
                        case WindingRules.Negative:
                            return (e.WindCnt2 >= 0);
                    }
                    break;
                case ClipingOperations.Difference:
                    if (e.GetPathType() == PolygonRelations.Subject)
                    {
                        switch (FillType)
                        {
                            case WindingRules.EvenOdd:
                            case WindingRules.NonZero:
                                return (e.WindCnt2 == 0);
                            case WindingRules.Positive:
                                return (e.WindCnt2 <= 0);
                            case WindingRules.Negative:
                                return (e.WindCnt2 >= 0);
                        }
                    }
                    else
                    {
                        switch (FillType)
                        {
                            case WindingRules.EvenOdd:
                            case WindingRules.NonZero:
                                return (e.WindCnt2 != 0);
                            case WindingRules.Positive:
                                return (e.WindCnt2 > 0);
                            case WindingRules.Negative:
                                return (e.WindCnt2 < 0);
                        }
                    }; break;
                case ClipingOperations.Xor:
                    return true; //XOr is always contributing unless open
            }
            return false; //we never get here but this stops a compiler issue.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsContributingOpen(Edge e)
        {
            switch (ClipType)
            {
                case ClipingOperations.Intersection:
                    return (e.WindCnt2 != 0);
                case ClipingOperations.Union:
                    return (e.WindCnt == 0 && e.WindCnt2 == 0);
                case ClipingOperations.Difference:
                    return (e.WindCnt2 == 0);
                case ClipingOperations.Xor:
                    return (e.WindCnt != 0) != (e.WindCnt2 != 0);
                case ClipingOperations.None:
                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void SetWindingLeftEdgeOpen(Edge e)
        {
            var e2 = Actives;
            if (FillType == WindingRules.EvenOdd)
            {
                int cnt1 = 0, cnt2 = 0;
                while (e2 != e)
                {
                    if (e2.GetPathType() == PolygonRelations.Clipping)
                    {
                        cnt2++;
                    }
                    else if (!e2.IsOpen())
                    {
                        cnt1++;
                    }

                    e2 = e2.NextInAEL;
                }
                e.WindCnt = (IsOdd(cnt1) ? 1 : 0);
                e.WindCnt2 = (IsOdd(cnt2) ? 1 : 0);
            }
            else
            {
                //if FClipType in [ctUnion, ctDifference] then e.WindCnt := e.WindDx;
                while (e2 != e)
                {
                    if (e2.GetPathType() == PolygonRelations.Clipping)
                    {
                        e.WindCnt2 += e2.WindDx;
                    }
                    else if (!e2.IsOpen())
                    {
                        e.WindCnt += e2.WindDx;
                    }

                    e2 = e2.NextInAEL;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftE"></param>
        private void SetWindingLeftEdgeClosed(Edge leftE)
        {
            //Wind counts generally refer to polygon regions not edges, so here an edge's
            //WindCnt indicates the higher of the two wind counts of the regions touching
            //the edge. (Note also that adjacent region wind counts only ever differ
            //by one, and open paths have no meaningful wind directions or counts.)

            var e = leftE.PrevInAEL;
            //find the nearest closed path edge of the same PathType in AEL (heading left)
            var pt = leftE.GetPathType();
            while (e != null && (e.GetPathType() != pt || e.IsOpen()))
            {
                e = e.PrevInAEL;
            }

            if (e == null)
            {
                leftE.WindCnt = leftE.WindDx;
                e = Actives;
            }
            else if (FillType == WindingRules.EvenOdd)
            {
                leftE.WindCnt = leftE.WindDx;
                leftE.WindCnt2 = e.WindCnt2;
                e = e.NextInAEL;
            }
            else
            {
                //NonZero, Positive, or Negative filling here ...
                //if e's WindCnt is in the SAME direction as its WindDx, then e is either
                //an outer left or a hole right boundary, so leftE must be inside 'e'.
                //(neither e.WindCnt nor e.WindDx should ever be 0)
                if (e.WindCnt * e.WindDx < 0)
                {
                    //opposite directions so leftE is outside 'e' ...
                    if (Abs(e.WindCnt) > 1)
                    {
                        //outside prev poly but still inside another.
                        if (e.WindDx * leftE.WindDx < 0)
                        {
                            //reversing direction so use the same WC
                            leftE.WindCnt = e.WindCnt;
                        }
                        else
                        {
                            //otherwise keep 'reducing' the WC by 1 (ie towards 0) ...
                            leftE.WindCnt = e.WindCnt + leftE.WindDx;
                        }
                    }
                    else
                    {
                        //now outside all polys of same PathType so set own WC ...
                        leftE.WindCnt = (leftE.IsOpen() ? 1 : leftE.WindDx);
                    }
                }
                else
                {
                    //leftE must be inside 'e'
                    if (e.WindDx * leftE.WindDx < 0)
                    {
                        //reversing direction so use the same WC
                        leftE.WindCnt = e.WindCnt;
                    }
                    else
                    {
                        //otherwise keep 'increasing' the WC by 1 (ie away from 0) ...
                        leftE.WindCnt = e.WindCnt + leftE.WindDx;
                    }
                }
                leftE.WindCnt2 = e.WindCnt2;
                e = e.NextInAEL; //ie get ready to calc WindCnt2
            }

            //update WindCnt2 ...
            if (FillType == WindingRules.EvenOdd)
            {
                while (e != leftE)
                {
                    if (e.GetPathType() != pt && !e.IsOpen())
                    {
                        leftE.WindCnt2 = (leftE.WindCnt2 == 0 ? 1 : 0);
                    }

                    e = e.NextInAEL;
                }
            }
            else
            {
                while (e != leftE)
                {
                    if (e.GetPathType() != pt && !e.IsOpen())
                    {
                        leftE.WindCnt2 += e.WindDx;
                    }

                    e = e.NextInAEL;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="startEdge"></param>
        private void InsertEdgeIntoAEL(Edge edge, Edge startEdge)
        {
            if (Actives == null)
            {
                edge.PrevInAEL = null;
                edge.NextInAEL = null;
                Actives = edge;
            }
            else if (startEdge == null && Edge.E2InsertsBeforeE1(Actives, edge))
            {
                edge.PrevInAEL = null;
                edge.NextInAEL = Actives;
                Actives.PrevInAEL = edge;
                Actives = edge;
            }
            else
            {
                if (startEdge == null)
                {
                    startEdge = Actives;
                }

                while (startEdge.NextInAEL != null &&
                  !Edge.E2InsertsBeforeE1(startEdge.NextInAEL, edge))
                {
                    startEdge = startEdge.NextInAEL;
                }

                edge.NextInAEL = startEdge.NextInAEL;
                if (startEdge.NextInAEL != null)
                {
                    startEdge.NextInAEL.PrevInAEL = edge;
                }

                edge.PrevInAEL = startEdge;
                startEdge.NextInAEL = edge;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BotY"></param>
        private void InsertLocalMinimaIntoAEL(double BotY)
        {
            Edge leftB, rightB;
            //Add any local minima at BotY ...
            while (PopLocalMinima(BotY, out LocalMinima locMin))
            {
                if ((locMin.Vertex.Flags & VertexFlags.OpenStart) > 0)
                {
                    leftB = null;
                }
                else
                {
                    leftB = new Edge
                    {
                        Bot = locMin.Vertex.Point
                    };
                    leftB.Curr = leftB.Bot;
                    leftB.VertTop = locMin.Vertex.PreviousVertex; //ie descending
                    leftB.Top = leftB.VertTop.Point;
                    leftB.WindDx = -1;
                    leftB.LocalMin = locMin;
                    leftB.SetDx();
                }

                if ((locMin.Vertex.Flags & VertexFlags.OpenEnd) > 0)
                {
                    rightB = null;
                }
                else
                {
                    rightB = new Edge
                    {
                        Bot = locMin.Vertex.Point
                    };
                    rightB.Curr = rightB.Bot;
                    rightB.VertTop = locMin.Vertex.NextVertex; //ie ascending
                    rightB.Top = rightB.VertTop.Point;
                    rightB.WindDx = 1;
                    rightB.LocalMin = locMin;
                    rightB.SetDx();
                }

                //Currently LeftB is just the descending bound and RightB is the ascending.
                //Now if the LeftB isn't on the left of RightB then we need swap them.
                if (leftB != null && rightB != null)
                {
                    if (leftB.IsHorizontal())
                    {
                        if (leftB.Top.X > leftB.Bot.X)
                        {
                            Edge.SwapActives(ref leftB, ref rightB);
                        }
                    }
                    else if (rightB.IsHorizontal())
                    {
                        if (rightB.Top.X < rightB.Bot.X)
                        {
                            Edge.SwapActives(ref leftB, ref rightB);
                        }
                    }
                    else if (leftB.Dx < rightB.Dx)
                    {
                        Edge.SwapActives(ref leftB, ref rightB);
                    }
                }
                else if (leftB == null)
                {
                    leftB = rightB;
                    rightB = null;
                }

                bool contributing;
                InsertEdgeIntoAEL(leftB, null);      //insert left edge
                if (leftB.IsOpen())
                {
                    SetWindingLeftEdgeOpen(leftB);
                    contributing = IsContributingOpen(leftB);
                }
                else
                {
                    SetWindingLeftEdgeClosed(leftB);
                    contributing = IsContributingClosed(leftB);
                }

                if (rightB != null)
                {
                    rightB.WindCnt = leftB.WindCnt;
                    rightB.WindCnt2 = leftB.WindCnt2;
                    InsertEdgeIntoAEL(rightB, leftB); //insert right edge
                    if (contributing)
                    {
                        AddLocalMinPoly(leftB, rightB, leftB.Bot);
                    }

                    if (rightB.IsHorizontal())
                    {
                        PushHorz(rightB);
                    }
                    else
                    {
                        InsertScanline(rightB.Top.Y);
                    }
                }
                else if (contributing)
                {
                    StartOpenPath(leftB, leftB.Bot);
                }

                if (leftB.IsHorizontal())
                {
                    PushHorz(leftB);
                }
                else
                {
                    InsertScanline(leftB.Top.Y);
                }

                if (rightB != null && leftB.NextInAEL != rightB)
                {
                    //intersect edges that are between left and right bounds ...
                    Edge e = rightB.NextInAEL;
                    rightB.MoveEdgeToFollowLeftInAEL(leftB);
                    while (rightB.NextInAEL != e)
                    {
                        //nb: For calculating winding counts etc, IntersectEdges() assumes
                        //that rightB will be to the right of e ABOVE the intersection ...
                        IntersectEdges(rightB, rightB.NextInAEL, rightB.Bot);
                        SwapPositionsInAEL(rightB, rightB.NextInAEL);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="pt"></param>
        virtual protected void AddLocalMinPoly(Edge e1, Edge e2, Point2D pt)
        {
            var outRec = CreateOutRec();
            outRec.IDx = OutRecList.Count;
            OutRecList.Add(outRec);
            outRec.Owner = e1.GetOwner();
            outRec.PolyPath = null;

            if (e1.IsOpen())
            {
                outRec.Owner = null;
                outRec.Flag = OutrecFlag.Open;
            }
            else if (outRec.Owner == null || (outRec.Owner.Flag == OutrecFlag.Inner))
            {
                outRec.Flag = OutrecFlag.Outer;
            }
            else
            {
                outRec.Flag = OutrecFlag.Inner;
            }

            //now set orientation ...
            var swapSideNeeded = false;    //todo: recheck this with open paths
            if (e1.IsHorizontal())
            {
                swapSideNeeded |= e1.Top.X > e1.Bot.X;
            }
            else if (e2.IsHorizontal())
            {
                swapSideNeeded |= e2.Top.X < e2.Bot.X;
            }
            else
            {
                swapSideNeeded |= e1.Dx < e2.Dx;
            }

            if ((outRec.Flag == OutrecFlag.Inner) == swapSideNeeded)
            {
                outRec.SetOrientation(e1, e2);
            }
            else
            {
                outRec.SetOrientation(e2, e1);
            }

            var op = CreateOutPt();
            op.Pt = pt;
            op.Next = op;
            op.Prev = op;
            outRec.Pts = op;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="Pt"></param>
        virtual protected void AddLocalMaxPoly(Edge e1, Edge e2, Point2D Pt)
        {
            if (!e2.IsHotEdge())
            {
                throw new EngineException("Error in AddLocalMaxPoly().");
            }

            AddOutPt(e1, Pt);
            if (e1.OutRec == e2.OutRec)
            {
                e1.OutRec.EndOutRec();
            }
            //and to preserve the winding orientation of Outrec ...
            else if (e1.OutRec.IDx < e2.OutRec.IDx)
            {
                Edge.JoinOutrecPaths(e1, e2);
            }
            else
            {
                Edge.JoinOutrecPaths(e2, e1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void PushHorz(Edge e)
        {
            e.NextInSEL = (SEL ?? null);
            SEL = e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool PopHorz(out Edge e)
        {
            e = SEL;
            if (e == null)
            {
                return false;
            }

            SEL = SEL.NextInSEL;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        private void StartOpenPath(Edge e, Point2D pt)
        {
            var outRec = CreateOutRec();
            outRec.IDx = OutRecList.Count;
            OutRecList.Add(outRec);
            outRec.Flag = OutrecFlag.Open;
            e.OutRec = outRec;

            var op = CreateOutPt();
            op.Pt = pt;
            op.Next = op;
            op.Prev = op;
            outRec.Pts = op;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual protected OutPoint CreateOutPt() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutPt ...
          new OutPoint();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        virtual protected OutRec CreateOutRec() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutRec ...
          new OutRec();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        virtual protected OutPoint AddOutPt(Edge e, Point2D pt)
        {
            //Outrec.Pts: a circular double-linked-list of POutPt.
            var toStart = e.IsStartSide();
            var opStart = e.OutRec.Pts;
            var opEnd = opStart.Next;
            if (toStart)
            {
                if (pt == opStart.Pt)
                {
                    return opStart;
                }
            }
            else if (pt == opEnd.Pt)
            {
                return opEnd;
            }

            var opNew = CreateOutPt();
            opNew.Pt = pt;
            opEnd.Prev = opNew;
            opNew.Prev = opStart;
            opNew.Next = opEnd;
            opStart.Next = opNew;
            if (toStart)
            {
                e.OutRec.Pts = opNew;
            }

            return opNew;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void UpdateEdgeIntoAEL(ref Edge e)
        {
            e.Bot = e.Top;
            e.VertTop = e.NextVertex();
            e.Top = e.VertTop.Point;
            e.Curr = e.Bot;
            e.SetDx();
            if (!e.IsHorizontal())
            {
                InsertScanline(e.Top.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="pt"></param>
        private void IntersectEdges(Edge e1, Edge e2, Point2D pt)
        {

            e1.Curr = pt;
            e2.Curr = pt;

            //if either edge is an OPEN path ...
            if (HasOpenPaths && (e1.IsOpen() || e2.IsOpen()))
            {
                if (e1.IsOpen() && e2.IsOpen())
                {
                    return; //ignore lines that intersect
                }
                //the following line just avoids duplicating a whole lot of code ...
                if (e2.IsOpen())
                {
                    Edge.SwapActives(ref e1, ref e2);
                }

                switch (ClipType)
                {
                    case ClipingOperations.Intersection:
                    case ClipingOperations.Difference:
                        if (e1.IsSamePathType(e2) || (Abs(e2.WindCnt) != 1))
                        {
                            return;
                        }

                        break;
                    case ClipingOperations.Union:
                        if (e1.IsHotEdge() != ((Abs(e2.WindCnt) != 1) ||
                          (e1.IsHotEdge() != (e2.WindCnt2 != 0))))
                        {
                            return; //just works!
                        }

                        break;
                    case ClipingOperations.Xor:
                        if (Abs(e2.WindCnt) != 1)
                        {
                            return;
                        }

                        break;
                    case ClipingOperations.None:
                        break;
                }
                //toggle contribution ...
                if (e1.IsHotEdge())
                {
                    AddOutPt(e1, pt);
                    e1.TerminateHotOpen();
                }
                else
                {
                    StartOpenPath(e1, pt);
                }

                return;
            }

            //update winding counts...
            //assumes that e1 will be to the right of e2 ABOVE the intersection
            int oldE1WindCnt, oldE2WindCnt;
            if (e1.LocalMin.PathType == e2.LocalMin.PathType)
            {
                if (FillType == WindingRules.EvenOdd)
                {
                    oldE1WindCnt = e1.WindCnt;
                    e1.WindCnt = e2.WindCnt;
                    e2.WindCnt = oldE1WindCnt;
                }
                else
                {
                    if (e1.WindCnt + e2.WindDx == 0)
                    {
                        e1.WindCnt = -e1.WindCnt;
                    }
                    else
                    {
                        e1.WindCnt += e2.WindDx;
                    }

                    if (e2.WindCnt - e1.WindDx == 0)
                    {
                        e2.WindCnt = -e2.WindCnt;
                    }
                    else
                    {
                        e2.WindCnt -= e1.WindDx;
                    }
                }
            }
            else
            {
                if (FillType != WindingRules.EvenOdd)
                {
                    e1.WindCnt2 += e2.WindDx;
                }
                else
                {
                    e1.WindCnt2 = (e1.WindCnt2 == 0) ? 1 : 0;
                }

                if (FillType != WindingRules.EvenOdd)
                {
                    e2.WindCnt2 -= e1.WindDx;
                }
                else
                {
                    e2.WindCnt2 = (e2.WindCnt2 == 0) ? 1 : 0;
                }
            }

            switch (FillType)
            {
                case WindingRules.Positive:
                    oldE1WindCnt = e1.WindCnt;
                    oldE2WindCnt = e2.WindCnt;
                    break;
                case WindingRules.Negative:
                    oldE1WindCnt = -e1.WindCnt;
                    oldE2WindCnt = -e2.WindCnt;
                    break;
                case WindingRules.NonZero:
                case WindingRules.EvenOdd:
                default:
                    oldE1WindCnt = Abs(e1.WindCnt);
                    oldE2WindCnt = Abs(e2.WindCnt);
                    break;
            }

            if (e1.IsHotEdge() && e2.IsHotEdge())
            {
                if ((oldE1WindCnt != 0 && oldE1WindCnt != 1) || (oldE2WindCnt != 0 && oldE2WindCnt != 1) ||
                  (e1.LocalMin.PathType != e2.LocalMin.PathType && ClipType != ClipingOperations.Xor))
                {
                    AddLocalMaxPoly(e1, e2, pt);
                }
                else if (e1.OutRec == e2.OutRec) //optional
                {
                    AddLocalMaxPoly(e1, e2, pt);
                    AddLocalMinPoly(e1, e2, pt);
                }
                else
                {
                    AddOutPt(e1, pt);
                    AddOutPt(e2, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if (e1.IsHotEdge())
            {
                if (oldE2WindCnt == 0 || oldE2WindCnt == 1)
                {
                    AddOutPt(e1, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if (e2.IsHotEdge())
            {
                if (oldE1WindCnt == 0 || oldE1WindCnt == 1)
                {
                    AddOutPt(e2, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if ((oldE1WindCnt == 0 || oldE1WindCnt == 1) &&
              (oldE2WindCnt == 0 || oldE2WindCnt == 1))
            {
                //neither edge is currently contributing ...
                Int64 e1Wc2, e2Wc2;
                switch (FillType)
                {
                    case WindingRules.Positive:
                        e1Wc2 = e1.WindCnt2;
                        e2Wc2 = e2.WindCnt2;
                        break;
                    case WindingRules.Negative:
                        e1Wc2 = -e1.WindCnt2;
                        e2Wc2 = -e2.WindCnt2;
                        break;
                    case WindingRules.NonZero:
                    case WindingRules.EvenOdd:
                    default:
                        e1Wc2 = Abs(e1.WindCnt2);
                        e2Wc2 = Abs(e2.WindCnt2);
                        break;
                }

                if (e1.LocalMin.PathType != e2.LocalMin.PathType)
                {
                    AddLocalMinPoly(e1, e2, pt);
                }
                else if (oldE1WindCnt == 1 && oldE2WindCnt == 1)
                {
                    switch (ClipType)
                    {
                        case ClipingOperations.Intersection:
                            if (e1Wc2 > 0 && e2Wc2 > 0)
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClipingOperations.Union:
                            if (e1Wc2 <= 0 && e2Wc2 <= 0)
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClipingOperations.Difference:
                            if (((e1.GetPathType() == PolygonRelations.Clipping) && (e1Wc2 > 0) && (e2Wc2 > 0)) ||
                                ((e1.GetPathType() == PolygonRelations.Subject) && (e1Wc2 <= 0) && (e2Wc2 <= 0)))
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClipingOperations.Xor:
                            AddLocalMinPoly(e1, e2, pt);
                            break;
                        case ClipingOperations.None:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DeleteFromAEL(Edge e)
        {
            var AelPrev = e.PrevInAEL;
            var AelNext = e.NextInAEL;
            if (AelPrev == null && AelNext == null && (e != Actives))
            {
                return; //already deleted
            }

            if (AelPrev != null)
            {
                AelPrev.NextInAEL = AelNext;
            }
            else
            {
                Actives = AelNext;
            }

            if (AelNext != null)
            {
                AelNext.PrevInAEL = AelPrev;
            }

            e.NextInAEL = null;
            e.PrevInAEL = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CopyAELToSEL()
        {
            Edge e = Actives;
            SEL = e;
            while (e != null)
            {
                e.PrevInSEL = e.PrevInAEL;
                e.NextInSEL = e.NextInAEL;
                e = e.NextInAEL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topY"></param>
        private void CopyActivesToSELAdjustCurrX(double topY)
        {
            var e = Actives;
            SEL = e;
            while (e != null)
            {
                e.PrevInSEL = e.PrevInAEL;
                e.NextInSEL = e.NextInAEL;
                e.Curr.X = e.TopX(topY);
                e = e.NextInAEL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        virtual protected bool ExecuteInternal(ClipingOperations ct, WindingRules ft)
        {
            if (ct == ClipingOperations.None)
            {
                return true;
            }

            FillType = ft;
            ClipType = ct;
            Reset();
            if (!PopScanline(out var Y))
            {
                return false;
            }

            while (true)
            {
                InsertLocalMinimaIntoAEL(Y);
                while (PopHorz(out Edge e))
                {
                    ProcessHorizontal(e);
                }

                if (!PopScanline(out Y))
                {
                    break;   //Y is now at the top of the scan-beam
                }

                ProcessIntersections(Y);
                SEL = null;                       //SEL reused to flag horizontals
                DoTopOfScanbeam(Y);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        virtual public Polygon Execute(ClipingOperations clipType, WindingRules ft = WindingRules.EvenOdd)
        {
            try
            {
                if (!ExecuteInternal(clipType, ft))
                {
                    return null;
                }
                return BuildResult(null);
            }
            finally { CleanUp(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="Closed"></param>
        /// <param name="Open"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        virtual public Polygon Execute(ClipingOperations clipType, Polygon Open, WindingRules ft = WindingRules.EvenOdd)
        {
            try
            {
                if (!ExecuteInternal(clipType, ft))
                {
                    return null;
                }

                return BuildResult(Open);
            }
            finally { CleanUp(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="polytree"></param>
        /// <param name="Open"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        virtual public bool Execute(ClipingOperations clipType, PolyTree polytree, Polygon Open, WindingRules ft = WindingRules.EvenOdd)
        {
            try
            {
                if (polytree == null)
                {
                    return false;
                }

                polytree.Clear();
                if (Open != null)
                {
                    Open.Clear();
                }

                if (!ExecuteInternal(clipType, ft))
                {
                    return false;
                }

                BuildResult2(polytree, Open);
                return true;
            }
            finally { CleanUp(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topY"></param>
        private void ProcessIntersections(double topY)
        {
            BuildIntersectList(topY);
            if (IntersectList.Count == 0)
            {
                return;
            }

            try
            {
                FixupIntersectionOrder();
                ProcessIntersectList();
            }
            finally
            {
                IntersectList.Clear(); //clean up only needed if there's been an error
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="topY"></param>
        private void InsertNewIntersectNode(Edge e1, Edge e2, double topY)
        {
            var pt = Edge.GetIntersectPoint(e1, e2);

            //Rounding errors can occasionally place the calculated intersection
            //point either below or above the scan-beam, so check and correct ...
            if (pt.Y > e1.Curr.Y)
            {
                pt.Y = e1.Curr.Y;      //E.Curr.Y is still the bottom of scan-beam
                                       //use the more vertical of the 2 edges to derive pt.X ...
                if (Abs(e1.Dx) < Abs(e2.Dx))
                {
                    pt.X = e1.TopX(pt.Y);
                }
                else
                {
                    pt.X = e2.TopX(pt.Y);
                }
            }
            else if (pt.Y < topY)
            {
                pt.Y = topY;          //TopY = top of scan-beam

                if (e1.Top.Y == topY)
                {
                    pt.X = e1.Top.X;
                }
                else if (e2.Top.Y == topY)
                {
                    pt.X = e2.Top.X;
                }
                else if (Abs(e1.Dx) < Abs(e2.Dx))
                {
                    pt.X = e1.Curr.X;
                }
                else
                {
                    pt.X = e2.Curr.X;
                }
            }

            var node = new IntersectNode
            {
                EdgeA = e1,
                EdgeB = e2,
                Point = pt
            };
            IntersectList.Add(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TopY"></param>
        private void BuildIntersectList(double TopY)
        {
            if (Actives == null || Actives.NextInAEL == null)
            {
                return;
            }

            CopyActivesToSELAdjustCurrX(TopY);

            // Merge sort FActives into their new positions at the top of scan-beam, and
            // create an intersection node every time an edge crosses over another ...

            var mul = 1;
            while (true)
            {
                Edge first = SEL, second = null, baseE, prevBase = null, tmp;

                // sort successive larger 'mul' count of nodes ...
                while (first != null)
                {
                    if (mul == 1)
                    {
                        second = first.NextInSEL;
                        if (second == null)
                        {
                            break;
                        }

                        first.MergeJump = second.NextInSEL;
                    }
                    else
                    {
                        second = first.MergeJump;
                        if (second == null)
                        {
                            break;
                        }

                        first.MergeJump = second.MergeJump;
                    }

                    // now sort first and second groups ...
                    baseE = first;
                    int lCnt = mul, rCnt = mul;
                    while (lCnt > 0 && rCnt > 0)
                    {
                        if (second.Curr.X < first.Curr.X)
                        {
                            // create one or more Intersect nodes
                            tmp = second.PrevInSEL;
                            for (var i = 0; i < lCnt; ++i)
                            {
                                //create a new intersect node...
                                InsertNewIntersectNode(tmp, second, TopY);
                                tmp = tmp.PrevInSEL;
                            }

                            if (first == baseE)
                            {
                                if (prevBase != null)
                                {
                                    prevBase.MergeJump = second;
                                }

                                baseE = second;
                                baseE.MergeJump = first.MergeJump;
                                if (first.PrevInSEL == null)
                                {
                                    SEL = second;
                                }
                            }
                            tmp = second.NextInSEL;
                            // now move the out of place edge to it's new position in SEL ...
                            Edge.Insert2Before1InSel(first, second);
                            second = tmp;
                            if (second == null)
                            {
                                break;
                            }

                            --rCnt;
                        }
                        else
                        {
                            first = first.NextInSEL;
                            --lCnt;
                        }
                    }
                    first = baseE.MergeJump;
                    prevBase = baseE;
                }
                if (SEL.MergeJump == null)
                {
                    break;
                }
                mul <<= 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessIntersectList()
        {
            foreach (IntersectNode iNode in IntersectList)
            {
                IntersectEdges(iNode.EdgeA, iNode.EdgeB, iNode.Point);
                SwapPositionsInAEL(iNode.EdgeA, iNode.EdgeB);
            }
            IntersectList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FixupIntersectionOrder()
        {
            var cnt = IntersectList.Count;
            if (cnt < 2)
            {
                return;
            }
            // It's important that edge intersections are processed from the bottom up,
            // but it's also crucial that intersections only occur between adjacent edges.
            // The first sort here (a quicksort), arranges intersections relative to their
            // vertical positions within the scan-beam ...
            IntersectList.Sort(IntersectNodeComparer);

            // Now we simulate processing these intersections, and as we do, we make sure
            // that the intersecting edges remain adjacent. If they aren't, this simulated
            // intersection is delayed until such time as these edges do become adjacent.
            CopyAELToSEL();
            for (var i = 0; i < cnt; i++)
            {
                if (!IntersectList[i].EdgesAdjacentInSEL())
                {
                    var j = i + 1;
                    while (!IntersectList[j].EdgesAdjacentInSEL())
                    {
                        j++;
                    }

                    var tmp = IntersectList[i];
                    IntersectList[i] = IntersectList[j];
                    IntersectList[j] = tmp;
                }
                SwapPositionsInSEL(IntersectList[i].EdgeA, IntersectList[i].EdgeB);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        internal void SwapPositionsInAEL(Edge e1, Edge e2)
        {
            Edge next, prev;
            if (e1.NextInAEL == e2)
            {
                next = e2.NextInAEL;
                if (next != null)
                {
                    next.PrevInAEL = e1;
                }

                prev = e1.PrevInAEL;
                if (prev != null)
                {
                    prev.NextInAEL = e2;
                }

                e2.PrevInAEL = prev;
                e2.NextInAEL = e1;
                e1.PrevInAEL = e2;
                e1.NextInAEL = next;
                if (e2.PrevInAEL == null)
                {
                    Actives = e2;
                }
            }
            else if (e2.NextInAEL == e1)
            {
                next = e1.NextInAEL;
                if (next != null)
                {
                    next.PrevInAEL = e2;
                }

                prev = e2.PrevInAEL;
                if (prev != null)
                {
                    prev.NextInAEL = e1;
                }

                e1.PrevInAEL = prev;
                e1.NextInAEL = e2;
                e2.PrevInAEL = e1;
                e2.NextInAEL = next;
                if (e1.PrevInAEL == null)
                {
                    Actives = e1;
                }
            }
            else
            {
                throw new EngineException("Clipping error in SwapPositionsInAEL");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        private void SwapPositionsInSEL(Edge e1, Edge e2)
        {
            Edge next, prev;
            if (e1.NextInSEL == e2)
            {
                next = e2.NextInSEL;
                if (next != null)
                {
                    next.PrevInSEL = e1;
                }

                prev = e1.PrevInSEL;
                if (prev != null)
                {
                    prev.NextInSEL = e2;
                }

                e2.PrevInSEL = prev;
                e2.NextInSEL = e1;
                e1.PrevInSEL = e2;
                e1.NextInSEL = next;
                if (e2.PrevInSEL == null)
                {
                    SEL = e2;
                }
            }
            else if (e2.NextInSEL == e1)
            {
                next = e1.NextInSEL;
                if (next != null)
                {
                    next.PrevInSEL = e2;
                }

                prev = e2.PrevInSEL;
                if (prev != null)
                {
                    prev.NextInSEL = e1;
                }

                e1.PrevInSEL = prev;
                e1.NextInSEL = e2;
                e2.PrevInSEL = e1;
                e2.NextInSEL = next;
                if (e1.PrevInSEL == null)
                {
                    SEL = e1;
                }
            }
            else
            {
                throw new EngineException("Clipping error in SwapPositionsInSEL");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="horz"></param>
        private void ProcessHorizontal(Edge horz)
        /*******************************************************************************
        * Notes: Horizontal edges (HEs) at scan-line intersections (ie at the top or    *
        * bottom of a scan-beam) are processed as if layered.The order in which HEs     *
        * are processed doesn't matter. HEs intersect with the bottom vertices of      *
        * other HEs[#] and with non-horizontal edges [*]. Once these intersections     *
        * are completed, intermediate HEs are 'promoted' to the next edge in their     *
        * bounds, and they in turn may be intersected[%] by other HEs.                 *
        *                                                                              *
        * eg: 3 horizontals at a scan-line:    /   |                     /           /  *
        *              |                     /    |     (HE3)o ========%========== o   *
        *              o ======= o(HE2)     /     |         /         /                *
        *          o ============#=========*======*========#=========o (HE1)           *
        *         /              |        /       |       /                            *
        *******************************************************************************/
        {
            Point2D pt;
            //with closed paths, simplify consecutive horizontals into a 'single' edge ...
            if (!horz.IsOpen())
            {
                pt = horz.Bot;
                while (!horz.IsMaxima() && horz.NextVertex().Point.Y == pt.Y)
                {
                    UpdateEdgeIntoAEL(ref horz);
                }

                horz.Bot = pt;
                horz.Curr = pt;
            }
            Edge maxPair = null;
            if (horz.IsMaxima() && (!horz.IsOpen() ||
                ((horz.VertTop.Flags & (VertexFlags.OpenStart | VertexFlags.OpenEnd)) == 0)))
            {
                maxPair = horz.GetMaximaPair();
            }

            (var isLeftToRight, var horzLeft, var horzRight) = horz.ResetHorzDirection(maxPair);
            if (horz.IsHotEdge())
            {
                AddOutPt(horz, horz.Curr);
            }

            while (true) // loops through consec. horizontal edges (if open)
            {
                Edge e;
                var isMax = horz.IsMaxima();
                if (isLeftToRight)
                {
                    e = horz.NextInAEL;
                }
                else
                {
                    e = horz.PrevInAEL;
                }

                while (e != null)
                {
                    //break if we've gone past the } of the horizontal ...
                    if ((isLeftToRight && (e.Curr.X > horzRight)) ||
                      (!isLeftToRight && (e.Curr.X < horzLeft)))
                    {
                        break;
                    }
                    //or if we've got to the } of an intermediate horizontal edge ...
                    if (e.Curr.X == horz.Top.X && !isMax && !e.IsHorizontal())
                    {
                        pt = horz.NextVertex().Point;
                        if (isLeftToRight && (e.TopX(pt.Y) >= pt.X) ||
                          (!isLeftToRight && (e.TopX(pt.Y) <= pt.X)))
                        {
                            break;
                        }
                    }
                    if (e == maxPair)
                    {
                        if (horz.IsHotEdge())
                        {
                            AddLocalMaxPoly(horz, e, horz.Top);
                        }

                        DeleteFromAEL(e);
                        DeleteFromAEL(horz);
                        return;
                    }
                    if (isLeftToRight)
                    {
                        pt = new Point2D(e.Curr.X, horz.Curr.Y);
                        IntersectEdges(horz, e, pt);
                    }
                    else
                    {
                        pt = new Point2D(e.Curr.X, horz.Curr.Y);
                        IntersectEdges(e, horz, pt);
                    }
                    Edge eNext;
                    if (isLeftToRight)
                    {
                        eNext = e.NextInAEL;
                    }
                    else
                    {
                        eNext = e.PrevInAEL;
                    }

                    SwapPositionsInAEL(horz, e);
                    e = eNext;
                }

                //check if we've finished with (consecutive) horizontals ...
                if (isMax || horz.NextVertex().Point.Y != horz.Top.Y)
                {
                    break;
                }

                //still more horizontals in bound to process ...
                UpdateEdgeIntoAEL(ref horz);
                (isLeftToRight, horzLeft, horzRight) = horz.ResetHorzDirection(maxPair);

                if (horz.IsOpen())
                {
                    if (horz.IsMaxima())
                    {
                        maxPair = horz.GetMaximaPair();
                    }

                    if (horz.IsHotEdge())
                    {
                        AddOutPt(horz, horz.Bot);
                    }
                }
            }

            if (horz.IsHotEdge())
            {
                AddOutPt(horz, horz.Top);
            }

            if (!horz.IsOpen())
            {
                UpdateEdgeIntoAEL(ref horz); //this is the } of an intermediate horiz.      
            }
            else if (!horz.IsMaxima())
            {
                UpdateEdgeIntoAEL(ref horz);
            }
            else if (maxPair == null)      //ie open at top
            {
                DeleteFromAEL(horz);
            }
            else if (horz.IsHotEdge())
            {
                AddLocalMaxPoly(horz, maxPair, horz.Top);
            }
            else { DeleteFromAEL(maxPair); DeleteFromAEL(horz); }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Y"></param>
        private void DoTopOfScanbeam(double Y)
        {
            var e = Actives;
            while (e != null)
            {
                //nb: E will never be horizontal at this point
                if (e.Top.Y == Y)
                {
                    e.Curr = e.Top; //needed for horizontal processing
                    if (e.IsMaxima())
                    {
                        e = DoMaxima(e); //TOP OF BOUND (MAXIMA)
                        continue;
                    }
                    else
                    {
                        //INTERMEDIATE VERTEX ...
                        UpdateEdgeIntoAEL(ref e);
                        if (e.IsHotEdge())
                        {
                            AddOutPt(e, e.Bot);
                        }

                        if (e.IsHorizontal())
                        {
                            PushHorz(e); //horizontals are processed later
                        }
                    }
                }
                else
                {
                    e.Curr.Y = Y;
                    e.Curr.X = e.TopX(Y);
                }
                e = e.NextInAEL;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Edge DoMaxima(Edge e)
        {
            Edge eMaxPair;
            var ePrev = e.PrevInAEL;
            var eNext = e.NextInAEL;
            if (e.IsOpen() && ((e.VertTop.Flags & (VertexFlags.OpenStart | VertexFlags.OpenEnd)) != 0))
            {
                if (e.IsHotEdge())
                {
                    AddOutPt(e, e.Top);
                }

                if (!e.IsHorizontal())
                {
                    if (e.IsHotEdge())
                    {
                        e.TerminateHotOpen();
                    }

                    DeleteFromAEL(e);
                }
                return eNext;
            }
            else
            {
                eMaxPair = e.GetMaximaPair();
                if (eMaxPair == null)
                {
                    return eNext; //eMaxPair is horizontal
                }
            }

            //only non-horizontal maxima here.
            //process any edges between maxima pair ...
            while (eNext != eMaxPair)
            {
                IntersectEdges(e, eNext, e.Top);
                SwapPositionsInAEL(e, eNext);
                eNext = e.NextInAEL;
            }

            if (e.IsOpen())
            {
                if (e.IsHotEdge())
                {
                    if (eMaxPair != null)
                    {
                        AddLocalMaxPoly(e, eMaxPair, e.Top);
                    }
                    else
                    {
                        AddOutPt(e, e.Top);
                    }
                }
                if (eMaxPair != null)
                {
                    DeleteFromAEL(eMaxPair);
                }

                DeleteFromAEL(e);
                return (ePrev != null ? ePrev.NextInAEL : Actives);
            }
            //here E.NextInAEL == ENext == EMaxPair ...
            if (e.IsHotEdge())
            {
                AddLocalMaxPoly(e, eMaxPair, e.Top);
            }

            DeleteFromAEL(e);
            DeleteFromAEL(eMaxPair);
            return (ePrev != null ? ePrev.NextInAEL : Actives);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openPaths"></param>
        private Polygon BuildResult(Polygon openPaths)
        {
            var closedPaths = new Polygon();
            //closedPaths.Clear();
            closedPaths.Capacity = OutRecList.Count;
            if (openPaths != null)
            {
                openPaths.Clear();
                openPaths.Capacity = OutRecList.Count;
            }

            foreach (OutRec outrec in OutRecList)
            {
                if (outrec.Pts != null)
                {
                    var op = outrec.Pts.Next;
                    var cnt = op.PointCount();
                    //fixup for duplicate start and } points ...
                    if (op.Pt == outrec.Pts.Pt)
                    {
                        cnt--;
                    }

                    if (outrec.Flag == OutrecFlag.Open)
                    {
                        if (cnt < 2 || openPaths == null)
                        {
                            continue;
                        }

                        var p = new PolygonContour
                        {
                            Capacity = cnt
                        };
                        for (var i = 0; i < cnt; i++) { p.Add(op.Pt); op = op.Next; }
                        openPaths.Add(p);
                    }
                    else
                    {
                        if (cnt < 3)
                        {
                            continue;
                        }

                        var p = new PolygonContour
                        {
                            Capacity = cnt
                        };
                        for (var i = 0; i < cnt; i++) { p.Add(op.Pt); op = op.Next; }
                        closedPaths.Add(p);
                    }
                }
            }

            return closedPaths;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="openPaths"></param>
        private void BuildResult2(PolyTree pt, Polygon openPaths)
        {
            if (pt == null)
            {
                return;
            }

            if (openPaths != null)
            {
                openPaths.Clear();
                openPaths.Capacity = OutRecList.Count;
            }

            foreach (OutRec outrec in OutRecList)
            {
                if (outrec.Pts != null)
                {
                    var op = outrec.Pts.Next;
                    var cnt = op.PointCount();
                    //fixup for duplicate start and end points ...
                    if (op.Pt == outrec.Pts.Pt)
                    {
                        cnt--;
                    }

                    if (cnt < 3)
                    {
                        if (outrec.Flag == OutrecFlag.Open || cnt < 2)
                        {
                            continue;
                        }
                    }

                    var p = new PolygonContour
                    {
                        Capacity = cnt
                    };
                    for (var i = 0; i < cnt; i++) { p.Add(op.Pt); op = op.Prev; }
                    if (outrec.Flag == OutrecFlag.Open)
                    {
                        openPaths.Add(p);
                    }
                    else
                    {
                        if (outrec.Owner != null && outrec.Owner.PolyPath != null)
                        {
                            outrec.PolyPath = outrec.Owner.PolyPath.AddChild(p);
                        }
                        else
                        {
                            outrec.PolyPath = pt.AddChild(p);
                        }
                    }
                }
            }
        }
    }
}
