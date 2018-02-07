/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using static System.Math;
using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// The edge class.
    /// </summary>
    public class Edge
    {
        #region Fields
        /// <summary>
        /// The bot.
        /// </summary>
        internal Point2D Bot;

        /// <summary>
        /// The current (updated for every new Scan-line)
        /// </summary>
        internal Point2D Curr;

        /// <summary>
        /// The top.
        /// </summary>
        internal Point2D Top;

        /// <summary>
        /// The dx.
        /// </summary>
        internal double Dx;

        /// <summary>
        /// winding direction (ascending: +1; descending: -1)
        /// </summary>
        internal int WindDx;

        /// <summary>
        /// The current wind count
        /// </summary>
        internal int WindCnt;

        /// <summary>
        /// The current wind count of opposite TPathType
        /// </summary>
        internal int WindCnt2;

        /// <summary>
        /// The out rec.
        /// </summary>
        internal OutRec OutRec;

        /// <summary>
        /// The next in AEL.
        /// </summary>
        internal Edge NextInAEL;

        /// <summary>
        /// The prev in AEL.
        /// </summary>
        internal Edge PrevInAEL;

        /// <summary>
        /// The next in SEL.
        /// </summary>
        internal Edge NextInSEL;

        /// <summary>
        /// The prev in SEL.
        /// </summary>
        internal Edge PrevInSEL;

        /// <summary>
        /// The merge jump.
        /// </summary>
        internal Edge MergeJump;

        /// <summary>
        /// The vert top.
        /// </summary>
        internal Vertex VertTop;

        /// <summary>
        /// The local min.
        /// </summary>
        internal LocalMinima LocalMin;
        #endregion Fields

        #region Getters
        /// <summary>
        /// Get the owner.
        /// </summary>
        /// <returns>The <see cref="OutRec"/>.</returns>
        public OutRec GetOwner()
        {
            var e = this;
            if (e.IsHorizontal() && e.Top.X < e.Bot.X)
            {
                e = e.NextInAEL;
                while (e != null && (!e.IsHotEdge() || e.IsOpen()))
                {
                    e = e.NextInAEL;
                }

                if (e == null)
                {
                    return null;
                }
                if ((e.OutRec.Flag == OutrecFlag.Outer) == (e.OutRec.StartEdge == e))
                {
                    return e.OutRec.Owner;
                }
                return e.OutRec;
            }
            e = e.PrevInAEL;
            while (e != null && (!e.IsHotEdge() || e.IsOpen()))
            {
                e = e.PrevInAEL;
            }

            if (e == null)
            {
                return null;
            }
            if ((e.OutRec.Flag == OutrecFlag.Outer) == (e.OutRec.EndEdge == e))
            {
                return e.OutRec.Owner;
            }
            return e.OutRec;
        }

        /// <summary>
        /// Get the out pt.
        /// </summary>
        /// <returns>The <see cref="LinkedPoint"/>.</returns>
        public LinkedPoint GetOutPt()
            => (IsStartSide()) ? OutRec.Points : OutRec.Points.Next;

        /// <summary>
        /// Get the left adjacent hot edge.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        public Edge GetLeftAdjacentHotEdge()
        {
            var result = PrevInAEL;
            while (result != null && !result.IsHotEdge())
            {
                result = result.PrevInAEL;
            }

            return result;
        }

        /// <summary>
        /// Get the right adjacent hot edge.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        public Edge GetRightAdjacentHotEdge()
        {
            var result = NextInAEL;
            while (result != null && !result.IsHotEdge())
            {
                result = result.NextInAEL;
            }

            return result;
        }

        /// <summary>
        /// Get the maxima pair.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        public Edge GetMaximaPair()
        {
            Edge e2;
            if (IsHorizontal())
            {
                //we can't be sure whether the MaximaPair is on the left or right, so ...
                e2 = PrevInAEL;
                while (e2 != null && e2.Curr.X >= Top.X)
                {
                    if (e2.VertTop == VertTop)
                    {
                        return e2;  //Found!
                    }

                    e2 = e2.PrevInAEL;
                }
                e2 = NextInAEL;
                while (e2 != null && e2.TopX(Top.Y) <= Top.X)
                {
                    if (e2.VertTop == VertTop)
                    {
                        return e2;  //Found!
                    }

                    e2 = e2.NextInAEL;
                }
            }
            else
            {
                e2 = NextInAEL;
                while (e2 != null)
                {
                    if (e2.VertTop == VertTop)
                    {
                        return e2; //Found!
                    }

                    e2 = e2.NextInAEL;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the path type.
        /// </summary>
        /// <returns>The <see cref="ClippingRelations"/>.</returns>
        public ClippingRelations GetPathType()
            => LocalMin.ClippingRelation;

        /// <summary>
        /// The next vertex.
        /// </summary>
        /// <returns>The <see cref="Vertex"/>.</returns>
        public Vertex NextVertex()
            => (WindDx > 0 ? VertTop.NextVertex : VertTop.PreviousVertex);

        /// <summary>
        /// Reset the horz direction.
        /// </summary>
        /// <param name="maxPair">The maxPair.</param>
        /// <returns>The <see cref="(bool, double horzLeft, double horzRight)"/>.</returns>
        public (bool, double horzLeft, double horzRight) ResetHorzDirection(Edge maxPair)
        {
            double horzLeft;
            double horzRight;
            if (Bot.X == Top.X)
            {
                //the horizontal edge is going nowhere ...
                horzLeft = Curr.X;
                horzRight = Curr.X;
                var e = NextInAEL;
                while (e != null && e != maxPair)
                {
                    e = e.NextInAEL;
                }

                return (e != null, horzLeft, horzRight);
            }
            if (Curr.X < Top.X)
            {
                horzLeft = Curr.X;
                horzRight = Top.X;
                return (true, horzLeft, horzRight);
            }
            horzLeft = Top.X;
            horzRight = Curr.X;
            return (false, horzLeft, horzRight); //right to left
        }
        #endregion Getters

        #region Setters
        /// <summary>
        /// Set the dx.
        /// </summary>
        public void SetDx()
        {
            var dy = (Top.Y - Bot.Y);
            Dx = (dy == 0 ? horizontal : (Top.X - Bot.X) / dy);
        }
        #endregion Setters

        #region Mutators
        /// <summary>
        /// The fix orientation.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool FixOrientation()
        {
            var result = true;
            var e2 = this;
            while (e2.PrevInAEL != null)
            {
                e2 = e2.PrevInAEL;
                if (e2.OutRec != null && !e2.IsOpen())
                {
                    result = !result;
                }
            }
            if (result != IsStartSide())
            {
                if (result)
                {
                    OutRec.Flag = OutrecFlag.Outer;
                }
                else
                {
                    OutRec.Flag = OutrecFlag.Inner;
                }

                OutRec.SwapSides();
                return true; //all fixed
            }
            else
            {
                return false; //no fix needed
            }
        }

        /// <summary>
        /// The terminate hot open.
        /// </summary>
        public void TerminateHotOpen()
        {
            if (OutRec.StartEdge == this)
            {
                OutRec.StartEdge = null;
            }
            else
            {
                OutRec.EndEdge = null;
            }

            OutRec = null;
        }

        /// <summary>
        /// Move the edge to follow left in AEL.
        /// </summary>
        /// <param name="eLeft">The eLeft.</param>
        public void MoveEdgeToFollowLeftInAEL(Edge eLeft)
        {
            Edge aelPrev, aelNext;
            //extract first ...
            aelPrev = PrevInAEL;
            aelNext = NextInAEL;
            aelPrev.NextInAEL = aelNext;
            if (aelNext != null)
            {
                aelNext.PrevInAEL = aelPrev;
            }
            //now reinsert ...
            NextInAEL = eLeft.NextInAEL;
            eLeft.NextInAEL.PrevInAEL = this;
            PrevInAEL = eLeft;
            eLeft.NextInAEL = this;
        }
        #endregion Mutators

        #region Queries
        /// <summary>
        /// The is maxima.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMaxima()
            => (VertexFlags.LocMax & VertTop.Flags) != 0;

        /// <summary>
        /// The is hot edge.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHotEdge()
            => OutRec != null;

        /// <summary>
        /// The is start side.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsStartSide()
            => (this == OutRec.StartEdge);

        /// <summary>
        /// The is horizontal.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHorizontal()
            => Dx == horizontal;

        /// <summary>
        /// The is open.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsOpen()
            => LocalMin.IsOpen;

        /// <summary>
        /// The is same path type.
        /// </summary>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSamePathType(Edge e2)
            => (LocalMin.ClippingRelation == e2.LocalMin.ClippingRelation);
        #endregion Queries

        /// <summary>
        /// The top x.
        /// </summary>
        /// <param name="currentY">The currentY.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public double TopX(double currentY)
        {
            if (currentY == Top.Y)
            {
                return Top.X;
            }

            return Bot.X + Round(Dx * (currentY - Bot.Y));
        }

        /// <summary>
        /// The join outrec paths.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <exception cref="EngineException">Error in JoinOutrecPaths</exception>
        public static void JoinOutrecPaths(Edge e1, Edge e2)
        {
            if (e1.IsStartSide() == e2.IsStartSide())
            {
                //one or other edge orientation is wrong...
                if (e1.IsOpen())
                {
                    e2.OutRec.SwapSides();
                }
                else if (!e1.FixOrientation() && !e2.FixOrientation())
                {
                    throw new EngineException("Error in JoinOutrecPaths");
                }

                if (e1.OutRec.Owner == e2.OutRec)
                {
                    e1.OutRec.Owner = e2.OutRec.Owner;
                }
            }

            //join E2 outrec path onto E1 outrec path and then delete E2 outrec path
            //pointers. (nb: Only very rarely do the joining ends share the same coords.)
            var P1_st = e1.OutRec.Points;
            var P2_st = e2.OutRec.Points;
            var P1_end = P1_st.Next;
            var P2_end = P2_st.Next;
            if (e1.IsStartSide())
            {
                P2_end.Prev = P1_st;
                P1_st.Next = P2_end;
                P2_st.Next = P1_end;
                P1_end.Prev = P2_st;
                e1.OutRec.Points = P2_st;
                e1.OutRec.StartEdge = e2.OutRec.StartEdge;
                if (e1.OutRec.StartEdge != null) //ie closed path
                {
                    e1.OutRec.StartEdge.OutRec = e1.OutRec;
                }
            }
            else
            {
                P1_end.Prev = P2_st;
                P2_st.Next = P1_end;
                P1_st.Next = P2_end;
                P2_end.Prev = P1_st;
                e1.OutRec.EndEdge = e2.OutRec.EndEdge;
                if (e1.OutRec.EndEdge != null) //ie closed path
                {
                    e1.OutRec.EndEdge.OutRec = e1.OutRec;
                }
            }

            e2.OutRec.StartEdge = null;
            e2.OutRec.EndEdge = null;
            e2.OutRec.Points = null;
            e2.OutRec.Owner = e1.OutRec; //this may be redundant

            e1.OutRec = null;
            e2.OutRec = null;
        }

        /// <summary>
        /// The swap outrecs.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        public static void SwapOutrecs(Edge e1, Edge e2)
        {
            var or1 = e1.OutRec;
            var or2 = e2.OutRec;
            if (or1 == or2)
            {
                var e = or1.StartEdge;
                or1.StartEdge = or1.EndEdge;
                or1.EndEdge = e;
                return;
            }
            if (or1 != null)
            {
                if (e1 == or1.StartEdge)
                {
                    or1.StartEdge = e2;
                }
                else
                {
                    or1.EndEdge = e2;
                }
            }
            if (or2 != null)
            {
                if (e2 == or2.StartEdge)
                {
                    or2.StartEdge = e1;
                }
                else
                {
                    or2.EndEdge = e1;
                }
            }
            e1.OutRec = or2;
            e2.OutRec = or1;
        }

        /// <summary>
        /// The insert2before1in sel.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public static void Insert2Before1InSel(Edge first, Edge second)
        {
            //remove second from list ...
            var prev = second.PrevInSEL;
            var next = second.NextInSEL;
            prev.NextInSEL = next; //always a prev since we're moving from right to left
            if (next != null)
            {
                next.PrevInSEL = prev;
            }
            //insert back into list ...
            prev = first.PrevInSEL;
            if (prev != null)
            {
                prev.NextInSEL = second;
            }

            first.PrevInSEL = second;
            second.PrevInSEL = prev;
            second.NextInSEL = first;
        }

        /// <summary>
        /// Get the top delta x.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetTopDeltaX(Edge e1, Edge e2)
        {
            if (e1.Top.Y > e2.Top.Y)
            {
                return e2.TopX(e1.Top.Y) - e1.Top.X;
            }
            return e2.Top.X - e1.TopX(e2.Top.Y);
        }

        /// <summary>
        /// Get the intersect point.
        /// </summary>
        /// <param name="edge1">The edge1.</param>
        /// <param name="edge2">The edge2.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D GetIntersectPoint(Edge edge1, Edge edge2)
        {
            var ip = new Point2D();
            double b1, b2;
            //nb: with very large coordinate values, it's possible for SlopesEqual() to
            //return false but for the edge.Dx value be equal due to double precision rounding.
            if (edge1.Dx == edge2.Dx)
            {
                ip.Y = edge1.Curr.Y;
                ip.X = edge1.TopX(ip.Y);
                return ip;
            }

            if (edge1.Dx == 0)
            {
                ip.X = edge1.Bot.X;
                if (edge2.IsHorizontal())
                {
                    ip.Y = edge2.Bot.Y;
                }
                else
                {
                    b2 = edge2.Bot.Y - (edge2.Bot.X / edge2.Dx);
                    ip.Y = Round(ip.X / edge2.Dx + b2);
                }
            }
            else if (edge2.Dx == 0)
            {
                ip.X = edge2.Bot.X;
                if (edge1.IsHorizontal())
                {
                    ip.Y = edge1.Bot.Y;
                }
                else
                {
                    b1 = edge1.Bot.Y - (edge1.Bot.X / edge1.Dx);
                    ip.Y = Round(ip.X / edge1.Dx + b1);
                }
            }
            else
            {
                b1 = edge1.Bot.X - edge1.Bot.Y * edge1.Dx;
                b2 = edge2.Bot.X - edge2.Bot.Y * edge2.Dx;
                var q = (b2 - b1) / (edge1.Dx - edge2.Dx);
                ip.Y = Round(q);
                if (Abs(edge1.Dx) < Abs(edge2.Dx))
                {
                    ip.X = Round(edge1.Dx * q + b1);
                }
                else
                {
                    ip.X = Round(edge2.Dx * q + b2);
                }
            }
            return ip;
        }

        /// <summary>
        /// The e2inserts before e1.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool E2InsertsBeforeE1(Edge e1, Edge e2)
            => (e2.Curr.X == e1.Curr.X) ?
            GetTopDeltaX(e1, e2) < 0 :
            e2.Curr.X < e1.Curr.X;

        /// <summary>
        /// The swap actives.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        public static void SwapActives(ref Edge e1, ref Edge e2)
        {
            var e = e1;
            e1 = e2; e2 = e;
        }
    }
}
