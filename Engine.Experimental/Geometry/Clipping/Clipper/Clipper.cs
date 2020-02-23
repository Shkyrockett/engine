/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine.Experimental
{
    /// <summary>
    /// Clipper
    /// </summary>
    public class Clipper
    {
        #region Fields
        /// <summary>
        /// The active edge link.
        /// </summary>
        private Edge activeEdgeLink;

        /// <summary>
        /// The selected edge link.
        /// </summary>
        private Edge selectedEdgeLink;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets the clip type.
        /// </summary>
        private ClippingOperation ClipType { get; set; }

        /// <summary>
        /// Gets or sets the fill type.
        /// </summary>
        private WindingRule FillType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// </summary>
        private bool HasOpenPaths { get; set; }

        /// <summary>
        /// Gets or sets the scanline.
        /// </summary>
        private ScanLine Scanline { get; set; }

        /// <summary>
        /// Gets or sets the current loc min idx.
        /// </summary>
        private int CurrentLocMinIdx { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// </summary>
        private bool LocMinListSorted { get; set; }

        /// <summary>
        /// Gets or sets the vertex list.
        /// </summary>
        private List<List<Vertex>> VertexList { get; set; } = new List<List<Vertex>>();

        /// <summary>
        /// Gets or sets the out rec list.
        /// </summary>
        private List<OutRec> OutRecList { get; set; } = new List<OutRec>();

        /// <summary>
        /// Gets or sets the loc minima list.
        /// </summary>
        private List<LocalMinima> LocMinimaList { get; set; } = new List<LocalMinima>();

        /// <summary>
        /// Gets or sets the intersect list.
        /// </summary>
        private List<IntersectNode> IntersectList { get; set; } = new List<IntersectNode>();
        #endregion Properties

        #region Virtual Methods
        /// <summary>
        /// Clean the up.
        /// </summary>
        protected virtual void CleanUp()
        {
            while (activeEdgeLink != null)
            {
                DeleteFromAEL(activeEdgeLink);
            }

            DisposeScanLineList();
            OutRecList.Clear();
        }

        /// <summary>
        /// Add the local min poly.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="pt">The pt.</param>
        protected virtual void AddLocalMinPoly(Edge e1, Edge e2, Point2D pt)
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
            else if (outRec.Owner is null || (outRec.Owner.Flag == OutrecFlag.Inner))
            {
                outRec.Flag = OutrecFlag.Outer;
            }
            else
            {
                outRec.Flag = OutrecFlag.Inner;
            }

            //now set orientation ...
            var swapSideNeeded = false;    //ToDo: recheck this with open paths
            if (e1.IsHorizontal())
            {
                swapSideNeeded |= e1.top.X > e1.bot.X;
            }
            else if (e2.IsHorizontal())
            {
                swapSideNeeded |= e2.top.X < e2.bot.X;
            }
            else
            {
                swapSideNeeded |= e1.dx < e2.dx;
            }

            if (outRec.Flag == OutrecFlag.Inner == swapSideNeeded)
            {
                outRec.SetOrientation(e1, e2);
            }
            else
            {
                outRec.SetOrientation(e2, e1);
            }

            var op = CreateOutPoint();
            op.Pt = pt;
            op.Next = op;
            op.Prev = op;
            outRec.Points = op;
        }

        /// <summary>
        /// Add the local max poly.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="Pt">The Pt.</param>
        /// <exception cref="EngineException">Error in AddLocalMaxPoly().</exception>
        protected virtual void AddLocalMaxPoly(Edge e1, Edge e2, Point2D Pt)
        {
            if (!e2.IsHotEdge())
            {
                throw new EngineException("Error in AddLocalMaxPoly().");
            }

            AddOutPoint(e1, Pt);
            if (e1.outRec == e2.outRec)
            {
                e1.outRec.EndOutRec();
            }
            //and to preserve the winding orientation of Outrec ...
            else if (e1.outRec.IDx < e2.outRec.IDx)
            {
                Edge.JoinOutrecPaths(e1, e2);
            }
            else
            {
                Edge.JoinOutrecPaths(e2, e1);
            }
        }

        /// <summary>
        /// This is a virtual method as descendant classes may need to produce descendant classes of OutPt ...
        /// </summary>
        /// <returns></returns>
        protected virtual LinkedPoint CreateOutPoint()
            => new LinkedPoint();

        /// <summary>
        /// This is a virtual method as descendant classes may need to produce descendant classes of OutRec ...
        /// </summary>
        /// <returns></returns>
        protected virtual OutRec CreateOutRec()
            => new OutRec();

        /// <summary>
        /// Add the out point.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="pt">The pt.</param>
        /// <returns>The <see cref="LinkedPoint"/>.</returns>
        protected virtual LinkedPoint AddOutPoint(Edge e, Point2D pt)
        {
            //Outrec.Pts: a circular double-linked-list of POutPt.
            var toStart = e.IsStartSide();
            var opStart = e.outRec.Points;
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

            var opNew = CreateOutPoint();
            opNew.Pt = pt;
            opEnd.Prev = opNew;
            opNew.Prev = opStart;
            opNew.Next = opEnd;
            opStart.Next = opNew;
            if (toStart)
            {
                e.outRec.Points = opNew;
            }

            return opNew;
        }

        /// <summary>
        /// The execute internal.
        /// </summary>
        /// <param name="ct">The ct.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected virtual bool ExecuteInternal(ClippingOperation ct, WindingRule ft)
        {
            if (ct == ClippingOperation.None)
            {
                return true;
            }

            FillType = ft;
            ClipType = ct;
            Reset();
            double? y;
            if ((y = PopScanline()) is null)
            {
                return false;
            }

            while (true)
            {
                InsertLocalMinimaIntoAEL(y.Value);
                Edge e;
                while ((e = PopHorz()) != null)
                {
                    ProcessHorizontal(e);
                }

                if ((y = PopScanline()) is null)
                {
                    break;   // Y is now at the top of the scan-beam
                }

                ProcessIntersections(y.Value);
                selectedEdgeLink = null;                       // SEL reused to flag horizontals
                DoTopOfScanbeam(y.Value);
            }
            return true;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public virtual Polygon Execute(ClippingOperation clipType, WindingRule ft = WindingRule.EvenOdd)
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
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="Open">The Open.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public virtual Polygon Execute(ClippingOperation clipType, Polygon Open, WindingRule ft = WindingRule.EvenOdd)
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
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="polytree">The polytree.</param>
        /// <param name="Open">The Open.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool Execute(ClippingOperation clipType, PolyTree polytree, Polygon Open, WindingRule ft = WindingRule.EvenOdd)
        {
            try
            {
                if (polytree is null)
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

                BuildResult(polytree, Open);
                return true;
            }
            finally { CleanUp(); }
        }
        #endregion Virtual Methods

        /// <summary>
        /// Reset.
        /// </summary>
        private void Reset()
        {
            if (!LocMinListSorted)
            {
                LocMinimaList.Sort();
                LocMinListSorted = true;
            }
            foreach (var locMin in LocMinimaList)
            {
                InsertScanline(locMin.Vertex.Point.Y);
            }

            CurrentLocMinIdx = 0;
            activeEdgeLink = null;
            selectedEdgeLink = null;
        }

        /// <summary>
        /// Insert the scanline.
        /// </summary>
        /// <param name="y">The y.</param>
        private void InsertScanline(double y)
        {
            // single-linked list: sorted descending, ignoring dupes.
            if (Scanline is null)
            {
                Scanline = new ScanLine
                {
                    NextScanLine = null,
                    Y = y
                };
            }
            else if (y > Scanline.Y)
            {
                var newSb = new ScanLine
                {
                    Y = y,
                    NextScanLine = Scanline
                };
                Scanline = newSb;
            }
            else
            {
                var sb2 = Scanline;
                while (sb2.NextScanLine != null && (y <= sb2.NextScanLine.Y))
                {
                    sb2 = sb2.NextScanLine;
                }

                if (y == sb2.Y)
                {
                    return; // IE ignores duplicates
                }

                var newSb = new ScanLine
                {
                    Y = y,
                    NextScanLine = sb2.NextScanLine
                };
                sb2.NextScanLine = newSb;
            }
        }

        /// <summary>
        /// The pop scanline.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        private double? PopScanline()
        {
            if (Scanline is null)
            {
                return null;
            }
            var y = Scanline.Y;
            var tmp = Scanline.NextScanLine;
            Scanline = null;
            Scanline = tmp;
            return y;
        }

        /// <summary>
        /// Dispose the scan line list.
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
        /// The pop local minima.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <returns>The <see cref="LocalMinima"/>.</returns>
        private LocalMinima? PopLocalMinima(double y)
        {
            if (CurrentLocMinIdx == LocMinimaList.Count)
            {
                return null;
            }

            var locMin = LocMinimaList[CurrentLocMinIdx];
            if (locMin.Vertex.Point.Y == y)
            {
                CurrentLocMinIdx++;
                return locMin;
            }
            return null;
        }

        /// <summary>
        /// Add the loc min.
        /// </summary>
        /// <param name="vert">The vert.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="isOpen">The isOpen.</param>
        private void AddLocMin(Vertex vert, ClippingRelation relation, bool isOpen)
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
                ClippingRelation = relation,
                IsOpen = isOpen
            };
            LocMinimaList.Add(lm);
        }

        /// <summary>
        /// Add the path to vertex list.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="isOpen">The isOpen.</param>
        private void AddPathToVertexList(PolygonContour path, ClippingRelation relation, bool isOpen)
        {
            var pathLen = path.Count;
            while (pathLen > 1 && path[pathLen - 1] == path[0])
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
            // find the first non-horizontal segment in the path ...
            while (i < pathLen && path[i].Y == path[0].Y)
            {
                i++;
            }

            if (i == pathLen) // it's a totally flat path
            {
                if (!isOpen)
                {
                    return;       // Ignore closed paths that have ZERO area.
                }
            }
            else
            {
                goingUp = path[i].Y < path[0].Y; // because I'm using an inverted Y-axis display
                if (goingUp)
                {
                    i = pathLen - 1;
                    while (path[i].Y == path[0].Y)
                    {
                        i--;
                    }

                    P0IsMinima = path[i].Y < path[0].Y; // p[0].Y == a minima
                }
                else
                {
                    i = pathLen - 1;
                    while (path[i].Y == path[0].Y)
                    {
                        i--;
                    }

                    P0IsMaxima = path[i].Y > path[0].Y; // p[0].Y == a maxima
                }
            }

            var va = new List<Vertex>(pathLen);
            VertexList.Add(va);
            var v = new Vertex(path[0]);
            if (isOpen)
            {
                v.Flags = VertexFlags.OpenStart;
                if (goingUp)
                {
                    AddLocMin(v, relation, isOpen);
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
                if (path[j] == v.Point)
                {
                    continue; //ie skips duplicates
                }

                var v2 = new Vertex(path[j]);
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
                    AddLocMin(v, relation, isOpen);
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
                    AddLocMin(v, relation, isOpen);
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
                    AddLocMin(va[0], relation, isOpen); //ie just turned to going up
                }
            }
            else
            {
                //going down so find local minima ...
                while (v.NextVertex.Point.Y >= v.Point.Y)
                {
                    v = v.NextVertex;
                }

                AddLocMin(v, relation, isOpen);
                if (P0IsMaxima)
                {
                    va[0].Flags |= VertexFlags.LocMax;
                }
            }
        }

        /// <summary>
        /// Add the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="isOpen">The isOpen.</param>
        /// <exception cref="EngineException"></exception>
        public void AddPath(PolygonContour path, ClippingRelation relation, bool isOpen = false)
        {
            if (isOpen)
            {
                if (relation == ClippingRelation.Clipping)
                {
                    throw new EngineException($"{nameof(AddPath)}: Only {nameof(ClippingRelation.Subject)} paths can be open.");
                }

                HasOpenPaths = true;
            }
            AddPathToVertexList(path, relation, isOpen);
            LocMinListSorted = false;
        }

        /// <summary>
        /// Add the paths.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <param name="pt">The pt.</param>
        /// <param name="isOpen">The isOpen.</param>
        public void AddPaths(Polygon paths, ClippingRelation pt, bool isOpen = false)
        {
            foreach (var path in paths)
            {
                AddPath(path, pt, isOpen);
            }
        }

        /// <summary>
        /// The is contributing closed.
        /// </summary>
        /// <param name="fillType">The fillType.</param>
        /// <param name="clipType">The clipType.</param>
        /// <param name="e">The e.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsContributingClosed(WindingRule fillType, ClippingOperation clipType, Edge e)
        {
            switch (fillType)
            {
                case WindingRule.NonZero:
                    if (Abs(e.windCnt) != 1)
                    {
                        return false;
                    }

                    break;
                case WindingRule.Positive:
                    if (e.windCnt != 1)
                    {
                        return false;
                    }

                    break;
                case WindingRule.Negative:
                    if (e.windCnt != -1)
                    {
                        return false;
                    }

                    break;
            }

            switch (clipType)
            {
                case ClippingOperation.Intersection:
                    switch (fillType)
                    {
                        case WindingRule.EvenOdd:
                        case WindingRule.NonZero:
                            return e.windCnt2 != 0;
                        case WindingRule.Positive:
                            return e.windCnt2 > 0;
                        case WindingRule.Negative:
                            return e.windCnt2 < 0;
                    }
                    break;
                case ClippingOperation.Union:
                    switch (fillType)
                    {
                        case WindingRule.EvenOdd:
                        case WindingRule.NonZero:
                            return e.windCnt2 == 0;
                        case WindingRule.Positive:
                            return e.windCnt2 <= 0;
                        case WindingRule.Negative:
                            return e.windCnt2 >= 0;
                    }
                    break;
                case ClippingOperation.Difference:
                    if (e.GetPathType() == ClippingRelation.Subject)
                    {
                        switch (fillType)
                        {
                            case WindingRule.EvenOdd:
                            case WindingRule.NonZero:
                                return e.windCnt2 == 0;
                            case WindingRule.Positive:
                                return e.windCnt2 <= 0;
                            case WindingRule.Negative:
                                return e.windCnt2 >= 0;
                        }
                    }
                    else
                    {
                        switch (fillType)
                        {
                            case WindingRule.EvenOdd:
                            case WindingRule.NonZero:
                                return e.windCnt2 != 0;
                            case WindingRule.Positive:
                                return e.windCnt2 > 0;
                            case WindingRule.Negative:
                                return e.windCnt2 < 0;
                        }
                    }; break;
                case ClippingOperation.Xor:
                    return true; //XOr is always contributing unless open
            }
            return false; //we never get here but this stops a compiler issue.
        }

        /// <summary>
        /// The is contributing open.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="e">The e.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsContributingOpen(ClippingOperation clipType, Edge e)
        {
            return clipType switch
            {
                ClippingOperation.Intersection => e.windCnt2 != 0,
                ClippingOperation.Union => e.windCnt == 0 && e.windCnt2 == 0,
                ClippingOperation.Difference => e.windCnt2 == 0,
                ClippingOperation.Xor => e.windCnt != 0 != (e.windCnt2 != 0),
                _ => false,
            };
        }

        /// <summary>
        /// Set the winding left edge open.
        /// </summary>
        /// <param name="e">The e.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWindingLeftEdgeOpen(Edge e)
        {
            var e2 = activeEdgeLink;
            if (FillType == WindingRule.EvenOdd)
            {
                var cnt1 = 0;
                var cnt2 = 0;
                while (e2 != e)
                {
                    if (e2.GetPathType() == ClippingRelation.Clipping)
                    {
                        cnt2++;
                    }
                    else if (!e2.IsOpen())
                    {
                        cnt1++;
                    }

                    e2 = e2.nextInAEL;
                }
                e.windCnt = IsOdd(cnt1) ? 1 : 0;
                e.windCnt2 = IsOdd(cnt2) ? 1 : 0;
            }
            else
            {
                //if FClipType in [ctUnion, ctDifference] then e.WindCnt := e.WindDx;
                while (e2 != e)
                {
                    if (e2.GetPathType() == ClippingRelation.Clipping)
                    {
                        e.windCnt2 += e2.windDx;
                    }
                    else if (!e2.IsOpen())
                    {
                        e.windCnt += e2.windDx;
                    }

                    e2 = e2.nextInAEL;
                }
            }
        }

        /// <summary>
        /// Set the winding left edge closed.
        /// </summary>
        /// <param name="leftE">The leftE.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetWindingLeftEdgeClosed(Edge leftE)
        {
            //Wind counts generally refer to polygon regions not edges, so here an edge's
            //WindCnt indicates the higher of the two wind counts of the regions touching
            //the edge. (Note also that adjacent region wind counts only ever differ
            //by one, and open paths have no meaningful wind directions or counts.)

            var e = leftE.prevInAEL;
            //find the nearest closed path edge of the same PathType in AEL (heading left)
            var pt = leftE.GetPathType();
            while (e != null && (e.GetPathType() != pt || e.IsOpen()))
            {
                e = e.prevInAEL;
            }

            if (e is null)
            {
                leftE.windCnt = leftE.windDx;
                e = activeEdgeLink;
            }
            else if (FillType == WindingRule.EvenOdd)
            {
                leftE.windCnt = leftE.windDx;
                leftE.windCnt2 = e.windCnt2;
                e = e.nextInAEL;
            }
            else
            {
                //NonZero, Positive, or Negative filling here ...
                //if e's WindCnt is in the SAME direction as its WindDx, then e is either
                //an outer left or a hole right boundary, so leftE must be inside 'e'.
                //(neither e.WindCnt nor e.WindDx should ever be 0)
                if (e.windCnt * e.windDx < 0)
                {
                    //opposite directions so leftE is outside 'e' ...
                    if (Abs(e.windCnt) > 1)
                    {
                        //outside prev poly but still inside another.
                        if (e.windDx * leftE.windDx < 0)
                        {
                            //reversing direction so use the same WC
                            leftE.windCnt = e.windCnt;
                        }
                        else
                        {
                            //otherwise keep 'reducing' the WC by 1 (ie towards 0) ...
                            leftE.windCnt = e.windCnt + leftE.windDx;
                        }
                    }
                    else
                    {
                        //now outside all polys of same PathType so set own WC ...
                        leftE.windCnt = leftE.IsOpen() ? 1 : leftE.windDx;
                    }
                }
                else
                {
                    //leftE must be inside 'e'
                    if (e.windDx * leftE.windDx < 0)
                    {
                        //reversing direction so use the same WC
                        leftE.windCnt = e.windCnt;
                    }
                    else
                    {
                        //otherwise keep 'increasing' the WC by 1 (ie away from 0) ...
                        leftE.windCnt = e.windCnt + leftE.windDx;
                    }
                }
                leftE.windCnt2 = e.windCnt2;
                e = e.nextInAEL; //ie get ready to calc WindCnt2
            }

            //update WindCnt2 ...
            if (FillType == WindingRule.EvenOdd)
            {
                while (e != leftE)
                {
                    if (e.GetPathType() != pt && !e.IsOpen())
                    {
                        leftE.windCnt2 = leftE.windCnt2 == 0 ? 1 : 0;
                    }

                    e = e.nextInAEL;
                }
            }
            else
            {
                while (e != leftE)
                {
                    if (e.GetPathType() != pt && !e.IsOpen())
                    {
                        leftE.windCnt2 += e.windDx;
                    }

                    e = e.nextInAEL;
                }
            }
        }

        /// <summary>
        /// Insert the edge into AEL.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="startEdge">The startEdge.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertEdgeIntoAEL(Edge edge, Edge startEdge)
        {
            if (activeEdgeLink is null)
            {
                edge.prevInAEL = null;
                edge.nextInAEL = null;
                activeEdgeLink = edge;
            }
            else if (startEdge is null && Edge.E2InsertsBeforeE1(activeEdgeLink, edge))
            {
                edge.prevInAEL = null;
                edge.nextInAEL = activeEdgeLink;
                activeEdgeLink.prevInAEL = edge;
                activeEdgeLink = edge;
            }
            else
            {
                if (startEdge is null)
                {
                    startEdge = activeEdgeLink;
                }

                while (startEdge.nextInAEL != null &&
                  !Edge.E2InsertsBeforeE1(startEdge.nextInAEL, edge))
                {
                    startEdge = startEdge.nextInAEL;
                }

                edge.nextInAEL = startEdge.nextInAEL;
                if (startEdge.nextInAEL != null)
                {
                    startEdge.nextInAEL.prevInAEL = edge;
                }

                edge.prevInAEL = startEdge;
                startEdge.nextInAEL = edge;
            }
        }

        /// <summary>
        /// Insert the local minima into AEL.
        /// </summary>
        /// <param name="BotY">The BotY.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertLocalMinimaIntoAEL(double BotY)
        {
            Edge leftB;
            Edge rightB;
            LocalMinima? locMin;
            // Add any local minima at BotY ...
            while ((locMin = PopLocalMinima(BotY)) != null)
            {
                if ((locMin?.Vertex.Flags & VertexFlags.OpenStart) > 0)
                {
                    leftB = null;
                }
                else
                {
                    leftB = new Edge
                    {
                        bot = locMin.Value.Vertex.Point
                    };
                    leftB.curr = leftB.bot;
                    leftB.vertTop = locMin?.Vertex.PreviousVertex; //ie descending
                    leftB.top = leftB.vertTop.Point;
                    leftB.windDx = -1;
                    leftB.localMin = locMin.Value;
                    leftB.SetDx();
                }

                if ((locMin.Value.Vertex.Flags & VertexFlags.OpenEnd) > 0)
                {
                    rightB = null;
                }
                else
                {
                    rightB = new Edge
                    {
                        bot = locMin.Value.Vertex.Point
                    };
                    rightB.curr = rightB.bot;
                    rightB.vertTop = locMin.Value.Vertex.NextVertex; //ie ascending
                    rightB.top = rightB.vertTop.Point;
                    rightB.windDx = 1;
                    rightB.localMin = locMin.Value;
                    rightB.SetDx();
                }

                // Currently LeftB is just the descending bound and RightB is the ascending.
                // Now if the LeftB isn't on the left of RightB then we need swap them.
                if (leftB != null && rightB != null)
                {
                    if (leftB.IsHorizontal())
                    {
                        if (leftB.top.X > leftB.bot.X)
                        {
                            Edge.SwapActives(ref leftB, ref rightB);
                        }
                    }
                    else if (rightB.IsHorizontal())
                    {
                        if (rightB.top.X < rightB.bot.X)
                        {
                            Edge.SwapActives(ref leftB, ref rightB);
                        }
                    }
                    else if (leftB.dx < rightB.dx)
                    {
                        Edge.SwapActives(ref leftB, ref rightB);
                    }
                }
                else if (leftB is null)
                {
                    leftB = rightB;
                    rightB = null;
                }

                bool contributing;
                InsertEdgeIntoAEL(leftB, null);      // Insert left edge
                if (leftB.IsOpen())
                {
                    SetWindingLeftEdgeOpen(leftB);
                    contributing = IsContributingOpen(ClipType, leftB);
                }
                else
                {
                    SetWindingLeftEdgeClosed(leftB);
                    contributing = IsContributingClosed(FillType, ClipType, leftB);
                }

                if (rightB != null)
                {
                    rightB.windCnt = leftB.windCnt;
                    rightB.windCnt2 = leftB.windCnt2;
                    InsertEdgeIntoAEL(rightB, leftB); // Insert right edge
                    if (contributing)
                    {
                        AddLocalMinPoly(leftB, rightB, leftB.bot);
                    }

                    if (rightB.IsHorizontal())
                    {
                        PushHorz(rightB);
                    }
                    else
                    {
                        InsertScanline(rightB.top.Y);
                    }
                }
                else if (contributing)
                {
                    StartOpenPath(leftB, leftB.bot);
                }

                if (leftB.IsHorizontal())
                {
                    PushHorz(leftB);
                }
                else
                {
                    InsertScanline(leftB.top.Y);
                }

                if (rightB != null && leftB.nextInAEL != rightB)
                {
                    // intersect edges that are between left and right bounds ...
                    var e = rightB.nextInAEL;
                    rightB.MoveEdgeToFollowLeftInAEL(leftB);
                    while (rightB.nextInAEL != e)
                    {
                        // nb: For calculating winding counts etc, IntersectEdges() assumes
                        // that rightB will be to the right of e ABOVE the intersection ...
                        IntersectEdges(rightB, rightB.nextInAEL, rightB.bot);
                        SwapPositionsInAEL(ref activeEdgeLink, rightB, rightB.nextInAEL);
                    }
                }
            }
        }

        /// <summary>
        /// Push the horz.
        /// </summary>
        /// <param name="e">The e.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PushHorz(Edge e)
        {
            e.nextInSEL = selectedEdgeLink ?? null;
            selectedEdgeLink = e;
        }

        /// <summary>
        /// The pop horz.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Edge PopHorz()
        {
            var e = selectedEdgeLink;
            if (e is null)
            {
                return null;
            }

            selectedEdgeLink = selectedEdgeLink.nextInSEL;
            return e;
        }

        /// <summary>
        /// Start the open path.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="pt">The pt.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void StartOpenPath(Edge e, Point2D pt)
        {
            var outRec = CreateOutRec();
            outRec.IDx = OutRecList.Count;
            OutRecList.Add(outRec);
            outRec.Flag = OutrecFlag.Open;
            e.outRec = outRec;

            var op = CreateOutPoint();
            op.Pt = pt;
            op.Next = op;
            op.Prev = op;
            outRec.Points = op;
        }

        /// <summary>
        /// The intersect edges.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="pt">The pt.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IntersectEdges(Edge e1, Edge e2, Point2D pt)
        {
            e1.curr = pt;
            e2.curr = pt;

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
                    case ClippingOperation.Intersection:
                    case ClippingOperation.Difference:
                        if (e1.IsSamePathType(e2) || (Abs(e2.windCnt) != 1))
                        {
                            return;
                        }

                        break;
                    case ClippingOperation.Union:
                        if (e1.IsHotEdge() != ((Abs(e2.windCnt) != 1) ||
                          (e1.IsHotEdge() != (e2.windCnt2 != 0))))
                        {
                            return; //just works!
                        }

                        break;
                    case ClippingOperation.Xor:
                        if (Abs(e2.windCnt) != 1)
                        {
                            return;
                        }

                        break;
                    case ClippingOperation.None:
                        break;
                }
                //toggle contribution ...
                if (e1.IsHotEdge())
                {
                    AddOutPoint(e1, pt);
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
            if (e1.localMin.ClippingRelation == e2.localMin.ClippingRelation)
            {
                if (FillType == WindingRule.EvenOdd)
                {
                    oldE1WindCnt = e1.windCnt;
                    e1.windCnt = e2.windCnt;
                    e2.windCnt = oldE1WindCnt;
                }
                else
                {
                    if (e1.windCnt + e2.windDx == 0)
                    {
                        e1.windCnt = -e1.windCnt;
                    }
                    else
                    {
                        e1.windCnt += e2.windDx;
                    }

                    if (e2.windCnt - e1.windDx == 0)
                    {
                        e2.windCnt = -e2.windCnt;
                    }
                    else
                    {
                        e2.windCnt -= e1.windDx;
                    }
                }
            }
            else
            {
                if (FillType != WindingRule.EvenOdd)
                {
                    e1.windCnt2 += e2.windDx;
                }
                else
                {
                    e1.windCnt2 = (e1.windCnt2 == 0) ? 1 : 0;
                }

                if (FillType != WindingRule.EvenOdd)
                {
                    e2.windCnt2 -= e1.windDx;
                }
                else
                {
                    e2.windCnt2 = (e2.windCnt2 == 0) ? 1 : 0;
                }
            }

            switch (FillType)
            {
                case WindingRule.Positive:
                    oldE1WindCnt = e1.windCnt;
                    oldE2WindCnt = e2.windCnt;
                    break;
                case WindingRule.Negative:
                    oldE1WindCnt = -e1.windCnt;
                    oldE2WindCnt = -e2.windCnt;
                    break;
                case WindingRule.NonZero:
                case WindingRule.EvenOdd:
                default:
                    oldE1WindCnt = Abs(e1.windCnt);
                    oldE2WindCnt = Abs(e2.windCnt);
                    break;
            }

            if (e1.IsHotEdge() && e2.IsHotEdge())
            {
                if ((oldE1WindCnt != 0 && oldE1WindCnt != 1) || (oldE2WindCnt != 0 && oldE2WindCnt != 1) ||
                  (e1.localMin.ClippingRelation != e2.localMin.ClippingRelation && ClipType != ClippingOperation.Xor))
                {
                    AddLocalMaxPoly(e1, e2, pt);
                }
                else if (e1.outRec == e2.outRec) //optional
                {
                    AddLocalMaxPoly(e1, e2, pt);
                    AddLocalMinPoly(e1, e2, pt);
                }
                else
                {
                    AddOutPoint(e1, pt);
                    AddOutPoint(e2, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if (e1.IsHotEdge())
            {
                if (oldE2WindCnt == 0 || oldE2WindCnt == 1)
                {
                    AddOutPoint(e1, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if (e2.IsHotEdge())
            {
                if (oldE1WindCnt == 0 || oldE1WindCnt == 1)
                {
                    AddOutPoint(e2, pt);
                    Edge.SwapOutrecs(e1, e2);
                }
            }
            else if ((oldE1WindCnt == 0 || oldE1WindCnt == 1) &&
              (oldE2WindCnt == 0 || oldE2WindCnt == 1))
            {
                //neither edge is currently contributing ...
                long e1Wc2, e2Wc2;
                switch (FillType)
                {
                    case WindingRule.Positive:
                        e1Wc2 = e1.windCnt2;
                        e2Wc2 = e2.windCnt2;
                        break;
                    case WindingRule.Negative:
                        e1Wc2 = -e1.windCnt2;
                        e2Wc2 = -e2.windCnt2;
                        break;
                    case WindingRule.NonZero:
                    case WindingRule.EvenOdd:
                    default:
                        e1Wc2 = Abs(e1.windCnt2);
                        e2Wc2 = Abs(e2.windCnt2);
                        break;
                }

                if (e1.localMin.ClippingRelation != e2.localMin.ClippingRelation)
                {
                    AddLocalMinPoly(e1, e2, pt);
                }
                else if (oldE1WindCnt == 1 && oldE2WindCnt == 1)
                {
                    switch (ClipType)
                    {
                        case ClippingOperation.Intersection:
                            if (e1Wc2 > 0 && e2Wc2 > 0)
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClippingOperation.Union:
                            if (e1Wc2 <= 0 && e2Wc2 <= 0)
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClippingOperation.Difference:
                            if (((e1.GetPathType() == ClippingRelation.Clipping) && (e1Wc2 > 0) && (e2Wc2 > 0)) ||
                                ((e1.GetPathType() == ClippingRelation.Subject) && (e1Wc2 <= 0) && (e2Wc2 <= 0)))
                            {
                                AddLocalMinPoly(e1, e2, pt);
                            }

                            break;
                        case ClippingOperation.Xor:
                            AddLocalMinPoly(e1, e2, pt);
                            break;
                        case ClippingOperation.None:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Delete the from AEL.
        /// </summary>
        /// <param name="e">The e.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DeleteFromAEL(Edge e)
        {
            var AelPrev = e.prevInAEL;
            var AelNext = e.nextInAEL;
            if (AelPrev is null && AelNext is null && (e != activeEdgeLink))
            {
                return; //already deleted
            }

            if (AelPrev != null)
            {
                AelPrev.nextInAEL = AelNext;
            }
            else
            {
                activeEdgeLink = AelNext;
            }

            if (AelNext != null)
            {
                AelNext.prevInAEL = AelPrev;
            }

            e.nextInAEL = null;
            e.prevInAEL = null;
        }

        /// <summary>
        /// Copy the AEL to SEL.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CopyAELToSEL()
        {
            var e = activeEdgeLink;
            selectedEdgeLink = e;
            while (e != null)
            {
                e.prevInSEL = e.prevInAEL;
                e.nextInSEL = e.nextInAEL;
                e = e.nextInAEL;
            }
        }

        /// <summary>
        /// Update the edge into AEL.
        /// </summary>
        /// <param name="e">The e.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateEdgeIntoAEL(ref Edge e)
        {
            e.bot = e.top;
            e.vertTop = e.NextVertex();
            e.top = e.vertTop.Point;
            e.curr = e.bot;
            e.SetDx();
            if (!e.IsHorizontal())
            {
                InsertScanline(e.top.Y);
            }
        }

        /// <summary>
        /// The swap positions in AEL.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <exception cref="EngineException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapPositionsInAEL(ref Edge reference, Edge e1, Edge e2)
        {
            Edge next;
            Edge prev;
            if (e1.nextInAEL == e2)
            {
                next = e2.nextInAEL;
                if (next != null)
                {
                    next.prevInAEL = e1;
                }

                prev = e1.prevInAEL;
                if (prev != null)
                {
                    prev.nextInAEL = e2;
                }

                e2.prevInAEL = prev;
                e2.nextInAEL = e1;
                e1.prevInAEL = e2;
                e1.nextInAEL = next;
                if (e2.prevInAEL is null)
                {
                    reference = e2;
                }
            }
            else if (e2.nextInAEL == e1)
            {
                next = e1.nextInAEL;
                if (next != null)
                {
                    next.prevInAEL = e2;
                }

                prev = e2.prevInAEL;
                if (prev != null)
                {
                    prev.nextInAEL = e1;
                }

                e1.prevInAEL = prev;
                e1.nextInAEL = e2;
                e2.prevInAEL = e1;
                e2.nextInAEL = next;
                if (e1.prevInAEL is null)
                {
                    reference = e1;
                }
            }
            else
            {
                throw new EngineException($"Clipping error in {nameof(SwapPositionsInAEL)}.");
            }
        }

        /// <summary>
        /// The swap positions in SEL.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <exception cref="EngineException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapPositionsInSEL(ref Edge reference, Edge e1, Edge e2)
        {
            Edge next;
            Edge prev;
            if (e1.nextInSEL == e2)
            {
                next = e2.nextInSEL;
                if (next != null)
                {
                    next.prevInSEL = e1;
                }

                prev = e1.prevInSEL;
                if (prev != null)
                {
                    prev.nextInSEL = e2;
                }

                e2.prevInSEL = prev;
                e2.nextInSEL = e1;
                e1.prevInSEL = e2;
                e1.nextInSEL = next;
                if (e2.prevInSEL is null)
                {
                    reference = e2;
                }
            }
            else if (e2.nextInSEL == e1)
            {
                next = e1.nextInSEL;
                if (next != null)
                {
                    next.prevInSEL = e2;
                }

                prev = e2.prevInSEL;
                if (prev != null)
                {
                    prev.nextInSEL = e1;
                }

                e1.prevInSEL = prev;
                e1.nextInSEL = e2;
                e2.prevInSEL = e1;
                e2.nextInSEL = next;
                if (e1.prevInSEL is null)
                {
                    reference = e1;
                }
            }
            else
            {
                throw new EngineException($"Clipping error in {nameof(SwapPositionsInSEL)}.");
            }
        }

        /// <summary>
        /// Copy the actives to SEL adjust curr x.
        /// </summary>
        /// <param name="topY">The topY.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CopyActivesToSELAdjustCurrX(double topY)
        {
            var e = activeEdgeLink;
            selectedEdgeLink = e;
            while (e != null)
            {
                e.prevInSEL = e.prevInAEL;
                e.nextInSEL = e.nextInAEL;
                e.curr.X = e.TopX(topY);
                e = e.nextInAEL;
            }
        }

        /// <summary>
        /// Process the intersections.
        /// </summary>
        /// <param name="topY">The topY.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        /// Insert the new intersect node.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="topY">The topY.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertNewIntersectNode(Edge e1, Edge e2, double topY)
        {
            var pt = Edge.GetIntersectPoint(e1, e2);

            //Rounding errors can occasionally place the calculated intersection
            //point either below or above the scan-beam, so check and correct ...
            if (pt.Y > e1.curr.Y)
            {
                pt.Y = e1.curr.Y;      //E.Curr.Y is still the bottom of scan-beam
                                       //use the more vertical of the 2 edges to derive pt.X ...
                if (Abs(e1.dx) < Abs(e2.dx))
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

                if (e1.top.Y == topY)
                {
                    pt.X = e1.top.X;
                }
                else if (e2.top.Y == topY)
                {
                    pt.X = e2.top.X;
                }
                else if (Abs(e1.dx) < Abs(e2.dx))
                {
                    pt.X = e1.curr.X;
                }
                else
                {
                    pt.X = e2.curr.X;
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
        /// Build the intersect list.
        /// </summary>
        /// <param name="TopY">The TopY.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BuildIntersectList(double TopY)
        {
            if (activeEdgeLink is null || activeEdgeLink.nextInAEL is null)
            {
                return;
            }

            CopyActivesToSELAdjustCurrX(TopY);

            // Merge sort FActives into their new positions at the top of scan-beam, and
            // create an intersection node every time an edge crosses over another ...

            var mul = 1;
            while (true)
            {
                var first = selectedEdgeLink;
                Edge baseE;
                Edge prevBase = null;

                // sort successive larger 'mul' count of nodes ...
                while (first != null)
                {
                    Edge second;
                    if (mul == 1)
                    {
                        second = first.nextInSEL;
                        if (second is null)
                        {
                            break;
                        }

                        first.mergeJump = second.nextInSEL;
                    }
                    else
                    {
                        second = first.mergeJump;
                        if (second is null)
                        {
                            break;
                        }

                        first.mergeJump = second.mergeJump;
                    }

                    // now sort first and second groups ...
                    baseE = first;
                    var lCnt = mul;
                    var rCnt = mul;
                    while (lCnt > 0 && rCnt > 0)
                    {
                        if (second.curr.X < first.curr.X)
                        {
                            // create one or more Intersect nodes
                            var tmp = second.prevInSEL;
                            for (var i = 0; i < lCnt; ++i)
                            {
                                //create a new intersect node...
                                InsertNewIntersectNode(tmp, second, TopY);
                                tmp = tmp.prevInSEL;
                            }

                            if (first == baseE)
                            {
                                if (prevBase != null)
                                {
                                    prevBase.mergeJump = second;
                                }

                                baseE = second;
                                baseE.mergeJump = first.mergeJump;
                                if (first.prevInSEL is null)
                                {
                                    selectedEdgeLink = second;
                                }
                            }
                            tmp = second.nextInSEL;
                            // now move the out of place edge to it's new position in SEL ...
                            Edge.Insert2Before1InSel(first, second);
                            second = tmp;
                            if (second is null)
                            {
                                break;
                            }

                            --rCnt;
                        }
                        else
                        {
                            first = first.nextInSEL;
                            --lCnt;
                        }
                    }
                    first = baseE.mergeJump;
                    prevBase = baseE;
                }
                if (selectedEdgeLink.mergeJump is null)
                {
                    break;
                }
                mul <<= 1;
            }
        }

        /// <summary>
        /// Process the intersect list.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessIntersectList()
        {
            foreach (var iNode in IntersectList)
            {
                IntersectEdges(iNode.EdgeA, iNode.EdgeB, iNode.Point);
                SwapPositionsInAEL(ref activeEdgeLink, iNode.EdgeA, iNode.EdgeB);
            }
            IntersectList.Clear();
        }

        /// <summary>
        /// The fixup intersection order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            IntersectList.Sort();

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
                SwapPositionsInSEL(ref selectedEdgeLink, IntersectList[i].EdgeA, IntersectList[i].EdgeB);
            }
        }

        /// <summary>
        /// Process the horizontal.
        /// </summary>
        /// <param name="horz">The horz.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessHorizontal(Edge horz)
        /*******************************************************************************
        * Notes: Horizontal edges (HEs) at scan-line intersections (I.E. at the top or   *
        * bottom of a scan-beam) are processed as if layered.The order in which HEs    *
        * are processed doesn't matter. HEs intersect with the bottom vertices of      *
        * other HEs[#] and with non-horizontal edges [*]. Once these intersections     *
        * are completed, intermediate HEs are 'promoted' to the next edge in their     *
        * bounds, and they in turn may be intersected[%] by other HEs.                 *
        *                                                                              *
        * E.G.: 3 horizontals at a scan-line: /   |                     /           /  *
        *              |                     /    |     (HE3)o ========%========== o   *
        *              o ======= o(HE2)     /     |         /         /                *
        *          o ============#=========*======*========#=========o (HE1)           *
        *         /              |        /       |       /                            *
        *******************************************************************************/
        {
            Point2D pt;
            // with closed paths, simplify consecutive horizontals into a 'single' edge ...
            if (!horz.IsOpen())
            {
                pt = horz.bot;
                while (!horz.IsMaxima() && horz.NextVertex().Point.Y == pt.Y)
                {
                    UpdateEdgeIntoAEL(ref horz);
                }

                horz.bot = pt;
                horz.curr = pt;
            }
            Edge maxPair = null;
            if (horz.IsMaxima() && (!horz.IsOpen() ||
                ((horz.vertTop.Flags & (VertexFlags.OpenStart | VertexFlags.OpenEnd)) == 0)))
            {
                maxPair = horz.GetMaximaPair();
            }

            (var isLeftToRight, var horzLeft, var horzRight) = horz.ResetHorzDirection(maxPair);
            if (horz.IsHotEdge())
            {
                AddOutPoint(horz, horz.curr);
            }

            while (true) // loops through consecutive. horizontal edges (if open)
            {
                Edge e;
                var isMax = horz.IsMaxima();
                e = isLeftToRight ? horz.nextInAEL : horz.prevInAEL;

                while (e != null)
                {
                    // break if we've gone past the } of the horizontal ...
                    if ((isLeftToRight && (e.curr.X > horzRight)) ||
                      (!isLeftToRight && (e.curr.X < horzLeft)))
                    {
                        break;
                    }
                    // or if we've got to the } of an intermediate horizontal edge ...
                    if (e.curr.X == horz.top.X && !isMax && !e.IsHorizontal())
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
                            AddLocalMaxPoly(horz, e, horz.top);
                        }

                        DeleteFromAEL(e);
                        DeleteFromAEL(horz);
                        return;
                    }
                    if (isLeftToRight)
                    {
                        pt = new Point2D(e.curr.X, horz.curr.Y);
                        IntersectEdges(horz, e, pt);
                    }
                    else
                    {
                        pt = new Point2D(e.curr.X, horz.curr.Y);
                        IntersectEdges(e, horz, pt);
                    }
                    Edge eNext;
                    eNext = isLeftToRight ? e.nextInAEL : e.prevInAEL;

                    SwapPositionsInAEL(ref activeEdgeLink, horz, e);
                    e = eNext;
                }

                // check if we've finished with (consecutive) horizontals ...
                if (isMax || horz.NextVertex().Point.Y != horz.top.Y)
                {
                    break;
                }

                // still more horizontals in bound to process ...
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
                        AddOutPoint(horz, horz.bot);
                    }
                }
            }

            if (horz.IsHotEdge())
            {
                AddOutPoint(horz, horz.top);
            }

            if (!horz.IsOpen())
            {
                UpdateEdgeIntoAEL(ref horz); // this is the } of an intermediate horiz.
            }
            else if (!horz.IsMaxima())
            {
                UpdateEdgeIntoAEL(ref horz);
            }
            else if (maxPair is null)      // ie open at top
            {
                DeleteFromAEL(horz);
            }
            else if (horz.IsHotEdge())
            {
                AddLocalMaxPoly(horz, maxPair, horz.top);
            }
            else { DeleteFromAEL(maxPair); DeleteFromAEL(horz); }

        }

        /// <summary>
        /// Do the top of scanbeam.
        /// </summary>
        /// <param name="Y">The Y.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DoTopOfScanbeam(double Y)
        {
            var e = activeEdgeLink;
            while (e != null)
            {
                // nb: E will never be horizontal at this point
                if (e.top.Y == Y)
                {
                    e.curr = e.top; // needed for horizontal processing
                    if (e.IsMaxima())
                    {
                        e = DoMaxima(e); // TOP OF BOUND (MAXIMA)
                        continue;
                    }
                    else
                    {
                        // INTERMEDIATE VERTEX ...
                        UpdateEdgeIntoAEL(ref e);
                        if (e.IsHotEdge())
                        {
                            AddOutPoint(e, e.bot);
                        }

                        if (e.IsHorizontal())
                        {
                            PushHorz(e); // horizontals are processed later
                        }
                    }
                }
                else
                {
                    e.curr.Y = Y;
                    e.curr.X = e.TopX(Y);
                }
                e = e.nextInAEL;
            }
        }

        /// <summary>
        /// Do the maxima.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>The <see cref="Edge"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Edge DoMaxima(Edge e)
        {
            Edge eMaxPair;
            var ePrev = e.prevInAEL;
            var eNext = e.nextInAEL;
            if (e.IsOpen() && ((e.vertTop.Flags & (VertexFlags.OpenStart | VertexFlags.OpenEnd)) != 0))
            {
                if (e.IsHotEdge())
                {
                    AddOutPoint(e, e.top);
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
                if (eMaxPair is null)
                {
                    return eNext; // eMaxPair is horizontal
                }
            }

            // only non-horizontal maxima here.
            // process any edges between maxima pair ...
            while (eNext != eMaxPair)
            {
                IntersectEdges(e, eNext, e.top);
                SwapPositionsInAEL(ref activeEdgeLink, e, eNext);
                eNext = e.nextInAEL;
            }

            if (e.IsOpen())
            {
                if (e.IsHotEdge())
                {
                    if (eMaxPair != null)
                    {
                        AddLocalMaxPoly(e, eMaxPair, e.top);
                    }
                    else
                    {
                        AddOutPoint(e, e.top);
                    }
                }
                if (eMaxPair != null)
                {
                    DeleteFromAEL(eMaxPair);
                }

                DeleteFromAEL(e);
                return ePrev != null ? ePrev.nextInAEL : activeEdgeLink;
            }
            // here E.NextInAEL == ENext == EMaxPair ...
            if (e.IsHotEdge())
            {
                AddLocalMaxPoly(e, eMaxPair, e.top);
            }

            DeleteFromAEL(e);
            DeleteFromAEL(eMaxPair);
            return ePrev != null ? ePrev.nextInAEL : activeEdgeLink;
        }

        /// <summary>
        /// Build the result.
        /// </summary>
        /// <param name="openPaths">The openPaths.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Polygon BuildResult(Polygon openPaths)
        {
            var closedPaths = new Polygon
            {
                // closedPaths.Clear();
                Capacity = OutRecList.Count
            };
            if (openPaths != null)
            {
                openPaths.Clear();
                openPaths.Capacity = OutRecList.Count;
            }

            foreach (var outrec in OutRecList)
            {
                if (outrec.Points != null)
                {
                    var op = outrec.Points.Next;
                    var count = op.PointCount();
                    // fixup for duplicate start and } points ...
                    if (op.Pt == outrec.Points.Pt)
                    {
                        count--;
                    }

                    if (outrec.Flag == OutrecFlag.Open)
                    {
                        if (count < 2 || openPaths is null)
                        {
                            continue;
                        }

                        var p = new PolygonContour
                        {
                            Capacity = count
                        };
                        for (var i = 0; i < count; i++) { p.Add(op.Pt); op = op.Next; }
                        openPaths.Add(p);
                    }
                    else
                    {
                        if (count < 3)
                        {
                            continue;
                        }

                        var p = new PolygonContour
                        {
                            Capacity = count
                        };
                        for (var i = 0; i < count; i++) { p.Add(op.Pt); op = op.Next; }
                        closedPaths.Add(p);
                    }
                }
            }

            return closedPaths;
        }

        /// <summary>
        /// Build the result.
        /// </summary>
        /// <param name="polyTree">The pt.</param>
        /// <param name="openPaths">The openPaths.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BuildResult(PolyTree polyTree, Polygon openPaths)
        {
            if (polyTree is null)
            {
                return;
            }

            if (openPaths != null)
            {
                openPaths.Clear();
                openPaths.Capacity = OutRecList.Count;
            }

            foreach (var outrec in OutRecList)
            {
                if (outrec.Points != null)
                {
                    var op = outrec.Points.Next;
                    var count = op.PointCount();
                    // fix-up for duplicate start and end points ...
                    if (op.Pt == outrec.Points.Pt)
                    {
                        count--;
                    }

                    if (count < 3)
                    {
                        if (outrec.Flag == OutrecFlag.Open || count < 2)
                        {
                            continue;
                        }
                    }

                    var p = new PolygonContour
                    {
                        Capacity = count
                    };
                    for (var i = 0; i < count; i++) { p.Add(op.Pt); op = op.Prev; }
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
                            outrec.PolyPath = polyTree.AddChild(p);
                        }
                    }
                }
            }
        }
    }
}
