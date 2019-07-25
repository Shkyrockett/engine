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
using static System.Math;
using static Engine.Mathematics;

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
        internal Point2D bot;

        /// <summary>
        /// The current (updated for every new Scan-line)
        /// </summary>
        internal Point2D curr;

        /// <summary>
        /// The top.
        /// </summary>
        internal Point2D top;

        /// <summary>
        /// The dx.
        /// </summary>
        internal double dx;

        /// <summary>
        /// winding direction (ascending: +1; descending: -1)
        /// </summary>
        internal int windDx;

        /// <summary>
        /// The current wind count
        /// </summary>
        internal int windCnt;

        /// <summary>
        /// The current wind count of opposite TPathType
        /// </summary>
        internal int windCnt2;

        /// <summary>
        /// The out rec.
        /// </summary>
        internal OutRec outRec;

        /// <summary>
        /// The next in AEL.
        /// </summary>
        internal Edge nextInAEL;

        /// <summary>
        /// The prev in AEL.
        /// </summary>
        internal Edge prevInAEL;

        /// <summary>
        /// The next in SEL.
        /// </summary>
        internal Edge nextInSEL;

        /// <summary>
        /// The prev in SEL.
        /// </summary>
        internal Edge prevInSEL;

        /// <summary>
        /// The merge jump.
        /// </summary>
        internal Edge mergeJump;

        /// <summary>
        /// The vert top.
        /// </summary>
        internal Vertex vertTop;

        /// <summary>
        /// The local min.
        /// </summary>
        internal LocalMinima localMin;
        #endregion Fields

        #region Getters
        /// <summary>
        /// Get the owner.
        /// </summary>
        /// <returns>The <see cref="outRec"/>.</returns>
        public OutRec GetOwner()
        {
            var e = this;
            if (e.IsHorizontal() && e.top.X < e.bot.X)
            {
                e = e.nextInAEL;
                while (e != null && (!e.IsHotEdge() || e.IsOpen()))
                {
                    e = e.nextInAEL;
                }

                if (e is null)
                {
                    return null;
                }
                if (e.outRec.Flag == OutrecFlag.Outer == (e.outRec.StartEdge == e))
                {
                    return e.outRec.Owner;
                }
                return e.outRec;
            }
            e = e.prevInAEL;
            while (e != null && (!e.IsHotEdge() || e.IsOpen()))
            {
                e = e.prevInAEL;
            }

            if (e is null)
            {
                return null;
            }
            if (e.outRec.Flag == OutrecFlag.Outer == (e.outRec.EndEdge == e))
            {
                return e.outRec.Owner;
            }
            return e.outRec;
        }

        /// <summary>
        /// Get the out pt.
        /// </summary>
        /// <returns>The <see cref="LinkedPoint"/>.</returns>
        public LinkedPoint GetOutPt()
            => IsStartSide() ? outRec.Points : outRec.Points.Next;

        /// <summary>
        /// Get the left adjacent hot edge.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        public Edge GetLeftAdjacentHotEdge()
        {
            var result = prevInAEL;
            while (result != null && !result.IsHotEdge())
            {
                result = result.prevInAEL;
            }

            return result;
        }

        /// <summary>
        /// Get the right adjacent hot edge.
        /// </summary>
        /// <returns>The <see cref="Edge"/>.</returns>
        public Edge GetRightAdjacentHotEdge()
        {
            var result = nextInAEL;
            while (result != null && !result.IsHotEdge())
            {
                result = result.nextInAEL;
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
                e2 = prevInAEL;
                while (e2 != null && e2.curr.X >= top.X)
                {
                    if (e2.vertTop == vertTop)
                    {
                        return e2;  //Found!
                    }

                    e2 = e2.prevInAEL;
                }
                e2 = nextInAEL;
                while (e2 != null && e2.TopX(top.Y) <= top.X)
                {
                    if (e2.vertTop == vertTop)
                    {
                        return e2;  //Found!
                    }

                    e2 = e2.nextInAEL;
                }
            }
            else
            {
                e2 = nextInAEL;
                while (e2 != null)
                {
                    if (e2.vertTop == vertTop)
                    {
                        return e2; //Found!
                    }

                    e2 = e2.nextInAEL;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the path type.
        /// </summary>
        /// <returns>The <see cref="ClippingRelation"/>.</returns>
        public ClippingRelation GetPathType()
            => localMin.ClippingRelation;

        /// <summary>
        /// The next vertex.
        /// </summary>
        /// <returns>The <see cref="Vertex"/>.</returns>
        public Vertex NextVertex()
            => windDx > 0 ? vertTop.NextVertex : vertTop.PreviousVertex;

        /// <summary>
        /// Reset the horz direction.
        /// </summary>
        /// <param name="maxPair">The maxPair.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        public (bool, double horzLeft, double horzRight) ResetHorzDirection(Edge maxPair)
        {
            double horzLeft;
            double horzRight;
            if (bot.X == top.X)
            {
                //the horizontal edge is going nowhere ...
                horzLeft = curr.X;
                horzRight = curr.X;
                var e = nextInAEL;
                while (e != null && e != maxPair)
                {
                    e = e.nextInAEL;
                }

                return (e != null, horzLeft, horzRight);
            }
            if (curr.X < top.X)
            {
                horzLeft = curr.X;
                horzRight = top.X;
                return (true, horzLeft, horzRight);
            }
            horzLeft = top.X;
            horzRight = curr.X;
            return (false, horzLeft, horzRight); //right to left
        }
        #endregion Getters

        #region Setters
        /// <summary>
        /// Set the dx.
        /// </summary>
        public void SetDx()
        {
            var dy = top.Y - bot.Y;
            dx = dy == 0 ? horizontal : (top.X - bot.X) / dy;
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
            while (e2.prevInAEL != null)
            {
                e2 = e2.prevInAEL;
                if (e2.outRec != null && !e2.IsOpen())
                {
                    result = !result;
                }
            }
            if (result != IsStartSide())
            {
                if (result)
                {
                    outRec.Flag = OutrecFlag.Outer;
                }
                else
                {
                    outRec.Flag = OutrecFlag.Inner;
                }

                outRec.SwapSides();
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
            if (outRec.StartEdge == this)
            {
                outRec.StartEdge = null;
            }
            else
            {
                outRec.EndEdge = null;
            }

            outRec = null;
        }

        /// <summary>
        /// Move the edge to follow left in AEL.
        /// </summary>
        /// <param name="eLeft">The eLeft.</param>
        public void MoveEdgeToFollowLeftInAEL(Edge eLeft)
        {
            Edge aelPrev, aelNext;
            //extract first ...
            aelPrev = prevInAEL;
            aelNext = nextInAEL;
            aelPrev.nextInAEL = aelNext;
            if (aelNext != null)
            {
                aelNext.prevInAEL = aelPrev;
            }
            //now reinsert ...
            nextInAEL = eLeft.nextInAEL;
            eLeft.nextInAEL.prevInAEL = this;
            prevInAEL = eLeft;
            eLeft.nextInAEL = this;
        }
        #endregion Mutators

        #region Queries
        /// <summary>
        /// The is maxima.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMaxima()
            => (VertexFlags.LocMax & vertTop.Flags) != 0;

        /// <summary>
        /// The is hot edge.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHotEdge()
            => outRec != null;

        /// <summary>
        /// The is start side.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsStartSide()
            => this == outRec.StartEdge;

        /// <summary>
        /// The is horizontal.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHorizontal()
            => dx == horizontal;

        /// <summary>
        /// The is open.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsOpen()
            => localMin.IsOpen;

        /// <summary>
        /// The is same path type.
        /// </summary>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSamePathType(Edge e2)
            => localMin.ClippingRelation == e2.localMin.ClippingRelation;
        #endregion Queries

        /// <summary>
        /// The top x.
        /// </summary>
        /// <param name="currentY">The currentY.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public double TopX(double currentY)
        {
            if (currentY == top.Y)
            {
                return top.X;
            }

            return bot.X + Round(dx * (currentY - bot.Y));
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
                    e2.outRec.SwapSides();
                }
                else if (!e1.FixOrientation() && !e2.FixOrientation())
                {
                    throw new EngineException("Error in JoinOutrecPaths");
                }

                if (e1.outRec.Owner == e2.outRec)
                {
                    e1.outRec.Owner = e2.outRec.Owner;
                }
            }

            //join E2 outrec path onto E1 outrec path and then delete E2 outrec path
            //pointers. (nb: Only very rarely do the joining ends share the same coords.)
            var P1_st = e1.outRec.Points;
            var P2_st = e2.outRec.Points;
            var P1_end = P1_st.Next;
            var P2_end = P2_st.Next;
            if (e1.IsStartSide())
            {
                P2_end.Prev = P1_st;
                P1_st.Next = P2_end;
                P2_st.Next = P1_end;
                P1_end.Prev = P2_st;
                e1.outRec.Points = P2_st;
                e1.outRec.StartEdge = e2.outRec.StartEdge;
                if (e1.outRec.StartEdge != null) //ie closed path
                {
                    e1.outRec.StartEdge.outRec = e1.outRec;
                }
            }
            else
            {
                P1_end.Prev = P2_st;
                P2_st.Next = P1_end;
                P1_st.Next = P2_end;
                P2_end.Prev = P1_st;
                e1.outRec.EndEdge = e2.outRec.EndEdge;
                if (e1.outRec.EndEdge != null) //ie closed path
                {
                    e1.outRec.EndEdge.outRec = e1.outRec;
                }
            }

            e2.outRec.StartEdge = null;
            e2.outRec.EndEdge = null;
            e2.outRec.Points = null;
            e2.outRec.Owner = e1.outRec; //this may be redundant

            e1.outRec = null;
            e2.outRec = null;
        }

        /// <summary>
        /// The swap outrecs.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        public static void SwapOutrecs(Edge e1, Edge e2)
        {
            var or1 = e1.outRec;
            var or2 = e2.outRec;
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
            e1.outRec = or2;
            e2.outRec = or1;
        }

        /// <summary>
        /// The insert2before1in sel.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public static void Insert2Before1InSel(Edge first, Edge second)
        {
            //remove second from list ...
            var prev = second.prevInSEL;
            var next = second.nextInSEL;
            prev.nextInSEL = next; //always a prev since we're moving from right to left
            if (next != null)
            {
                next.prevInSEL = prev;
            }
            //insert back into list ...
            prev = first.prevInSEL;
            if (prev != null)
            {
                prev.nextInSEL = second;
            }

            first.prevInSEL = second;
            second.prevInSEL = prev;
            second.nextInSEL = first;
        }

        /// <summary>
        /// Get the top delta x.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetTopDeltaX(Edge e1, Edge e2)
        {
            if (e1.top.Y > e2.top.Y)
            {
                return e2.TopX(e1.top.Y) - e1.top.X;
            }
            return e2.top.X - e1.TopX(e2.top.Y);
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
            if (edge1.dx == edge2.dx)
            {
                ip.Y = edge1.curr.Y;
                ip.X = edge1.TopX(ip.Y);
                return ip;
            }

            if (edge1.dx == 0)
            {
                ip.X = edge1.bot.X;
                if (edge2.IsHorizontal())
                {
                    ip.Y = edge2.bot.Y;
                }
                else
                {
                    b2 = edge2.bot.Y - (edge2.bot.X / edge2.dx);
                    ip.Y = Round((ip.X / edge2.dx) + b2);
                }
            }
            else if (edge2.dx == 0)
            {
                ip.X = edge2.bot.X;
                if (edge1.IsHorizontal())
                {
                    ip.Y = edge1.bot.Y;
                }
                else
                {
                    b1 = edge1.bot.Y - (edge1.bot.X / edge1.dx);
                    ip.Y = Round((ip.X / edge1.dx) + b1);
                }
            }
            else
            {
                b1 = edge1.bot.X - (edge1.bot.Y * edge1.dx);
                b2 = edge2.bot.X - (edge2.bot.Y * edge2.dx);
                var q = (b2 - b1) / (edge1.dx - edge2.dx);
                ip.Y = Round(q);
                if (Abs(edge1.dx) < Abs(edge2.dx))
                {
                    ip.X = Round((edge1.dx * q) + b1);
                }
                else
                {
                    ip.X = Round((edge2.dx * q) + b2);
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
            => (e2.curr.X == e1.curr.X) ?
            GetTopDeltaX(e1, e2) < 0 :
            e2.curr.X < e1.curr.X;

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
