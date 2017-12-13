// <copyright file="SegmentComparators.cs" >
//     Copyright © 2012 Francisco Martínez del Río. All rights reserved.
// </copyright>
// <author id="fmartin@ujaen.es">Francisco Martínez del Río</author>
// <license>
//     This code is public domain.
// </license>
// <summary></summary>
// <remarks> http://www4.ujaen.es/~fmartin/bool_op.html </remarks>

using System;
using System.Collections.Generic;
using static Engine.SegmentComparators;

namespace Engine
{
    /// <summary>
    /// This class contains methods for computing clipping operations on polygons. 
    /// It implements the algorithm for polygon intersection given by Francisco Martínez del Río.
    /// </summary>
    public class MartinezPolygonClipper
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Polygon subject;

        /// <summary>
        /// 
        /// </summary>
        private Polygon clipping;

        /// <summary>
        /// 
        /// </summary>
        private EventQueue eventQueue;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="clipping"></param>
        public MartinezPolygonClipper(Polygon subject, Polygon clipping)
        {
            this.subject = subject;
            this.clipping = clipping;
            eventQueue = new EventQueue();
        }

        #endregion

        /// <summary>
        /// Computes the polygon operation given by operation.
        /// See <see cref="ClippingOperations"/> for the operation codes.
        /// </summary>
        /// <param name="operation">A value specifying which boolean operation to compute.</param>
        /// <returns>The resulting polygon from the specified clipping operation.</returns>
        public Polygon Compute(ClippingOperations operation)
        {
            var result = new Polygon();

            // Test 1 for trivial result case
            if (subject.Contours.Count * clipping.Contours.Count == 0)
            {
                // At least one of the polygons is empty
                switch (operation)
                {
                    case ClippingOperations.Difference:
                        result = subject;
                        break;
                    case ClippingOperations.Union:
                    case ClippingOperations.Xor:
                        result = (subject.Contours.Count == 0) ? clipping : subject;
                        break;
                    default:
                        break;
                }

                return result;
            }

            var subjectBB = subject.Bounds;
            var clippingBB = clipping.Bounds;

            // Test 2 for trivial result case
            if (!subjectBB.IntersectsWith(clippingBB))
            {
                // the bounding boxes do not overlap
                switch (operation)
                {
                    case ClippingOperations.Difference:
                        result = subject;
                        break;
                    case ClippingOperations.Union:
                    case ClippingOperations.Xor:
                        result = subject;
                        foreach (var c in clipping.Contours)
                        {
                            result.Add(c);
                        }

                        break;
                    default:
                        break;
                }

                return result;
            }

            // Add each segment to the eventQueue, sorted from left to right.
            foreach (var sCont in subject.Contours)
            {
                for (var pParse1 = 0; pParse1 < sCont.Points.Count; pParse1++)
                {
                    ProcessSegment(sCont.Segment(pParse1), ClippingRelations.Subject);
                }
            }

            foreach (var cCont in clipping.Contours)
            {
                for (var pParse2 = 0; pParse2 < cCont.Points.Count; pParse2++)
                {
                    ProcessSegment(cCont.Segment(pParse2), ClippingRelations.Clipping);
                }
            }

            var connector = new Connector();

            // This is the SweepLine. That is, we go through all the polygon edges
            // by sweeping from left to right.
            var S = new SweepEventSet();

            SweepEvent e;
            var minMaxX = Math.Min(subjectBB.Right, clippingBB.Right);

            SweepEvent prev= null, next = null;

            while (!eventQueue.IsEmpty)
            {
                e = eventQueue.Dequeue();

                // Optimization 2
                if ((operation == ClippingOperations.Intersection && (e.Point.X > minMaxX)) || (operation == ClippingOperations.Difference && e.Point.X > subjectBB.Right))
                    return connector.ToPolygon();

                if (operation == ClippingOperations.Union && (e.Point.X > minMaxX))
                {
                    if (!e.IsLeft)
                        connector.Add(e.Segment());

                    while (!eventQueue.IsEmpty)
                    {
                        e = eventQueue.Dequeue();
                        if (!e.IsLeft)
                            connector.Add(e.Segment());
                    }

                    return connector.ToPolygon();
                }

                if (e.IsLeft)
                {
                    var pos = S.Insert(e);

                    prev = (pos > 0) ? S.eventSet[pos - 1] : null;
                    next = (pos < S.eventSet.Count - 1) ? S.eventSet[pos + 1] : null;

                    if (prev == null)
                    {
                        e.OtherInOut = e.InOut = false;
                    }
                    else if (prev.Contribution != EdgeContributions.Normal)
                    {
                        if (pos - 2 < 0)
                        {
                            // Not sure how to handle the case when pos - 2 < 0, but judging
                            // from the C++ implementation this looks like how it should be handled.
                            e.OtherInOut = e.InOut = false;
                            if (prev.BelongsTo != e.BelongsTo)
                                e.OtherInOut = true;
                            else
                                e.InOut = true;
                        }
                        else
                        {
                            var prevTwo = S.eventSet[pos - 2];
                            if (prev.BelongsTo == e.BelongsTo)
                            {
                                e.InOut = !prev.InOut;
                                e.OtherInOut = !prevTwo.InOut;
                            }
                            else
                            {
                                e.InOut = !prevTwo.InOut;
                                e.OtherInOut = !prev.InOut;
                            }
                        }
                    }
                    else if (e.BelongsTo == prev.BelongsTo)
                    {
                        e.OtherInOut = prev.OtherInOut;
                        e.InOut = !prev.InOut;
                    }
                    else
                    {
                        e.OtherInOut = !prev.InOut;
                        e.InOut = prev.OtherInOut;
                    }

                    if (next != null)
                        PossibleIntersection(e, next);

                    if (prev != null)
                        PossibleIntersection(e, prev);
                }
                else
                {
                    var otherPos = S.eventSet.IndexOf(e.OtherEvent);

                    if (otherPos != -1)
                    {
                        prev = (otherPos > 0) ? S.eventSet[otherPos - 1] : null;
                        next = (otherPos < S.eventSet.Count - 1) ? S.eventSet[otherPos + 1] : null;
                    }

                    switch (e.Contribution)
                    {
                        case EdgeContributions.Normal:
                            switch (operation)
                            {
                                case (ClippingOperations.Intersection):
                                    if (e.OtherEvent.OtherInOut)
                                        connector.Add(e.Segment());
                                    break;
                                case (ClippingOperations.Union):
                                    if (!e.OtherEvent.OtherInOut)
                                        connector.Add(e.Segment());
                                    break;
                                case (ClippingOperations.Difference):
                                    if (((e.BelongsTo == ClippingRelations.Subject) && (!e.OtherEvent.OtherInOut)) || (e.BelongsTo == ClippingRelations.Clipping && e.OtherEvent.OtherInOut))
                                        connector.Add(e.Segment());
                                    break;
                            }
                            break;
                        case (EdgeContributions.SameTransition):
                            if (operation == ClippingOperations.Intersection || operation == ClippingOperations.Union)
                                connector.Add(e.Segment());
                            break;
                        case (EdgeContributions.DifferentTransition):
                            if (operation == ClippingOperations.Difference)
                                connector.Add(e.Segment());
                            break;
                    }

                    if (otherPos != -1)
                        S.Remove(S.eventSet[otherPos]);
                    if (next != null && prev != null)
                        PossibleIntersection(next, prev);
                }
            }

            return connector.ToPolygon();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seg0"></param>
        /// <param name="seg1"></param>
        /// <returns></returns>
        private (int, Point2D[]) FindIntersection(LineSegment seg0, LineSegment seg1)
        {
            var pi0 = new Point2D();
            var pi1 = new Point2D();

            var p0 = seg0.A;
            var d0 = new Vector2D(seg0.B.X - p0.X, seg0.B.Y - p0.Y);
            var p1 = seg1.A;
            var d1 = new Vector2D(seg1.B.X - p1.X, seg1.B.Y - p1.Y);
            var sqrEpsilon = 0.0000001d; // Antes 0.001
            var E = new Vector2D(p1.X - p0.X, p1.Y - p0.Y);
            var cross = d0.I * d1.J - d0.J * d1.I;
            var sqrCross = cross * cross;
            var sqrLen0 = d0.Length;
            var sqrLen1 = d1.Length;

            if (sqrCross > sqrEpsilon * sqrLen0 * sqrLen1)
            {
                // lines of the segments are not parallel
                var s = (E.I * d1.J - E.J * d1.I) / cross;
                if ((s < 0) || (s > 1))
                {
                    return (0, new[] { pi0, pi1 });
                }
                var t = (E.I * d0.J - E.J * d0.I) / cross;
                if ((t < 0) || (t > 1))
                {
                    return (0, new[] { pi0, pi1 });
                }
                // intersection of lines is a point an each segment
                pi0.X = p0.X + s * d0.I;
                pi0.Y = p0.Y + s * d0.J;

                // Uncomment the block below if you're getting errors to do with precision.
                /*if (Point.distance(pi0,seg0.start) < 0.00000001) pi0 = seg0.start;
				if (Point.distance(pi0,seg0.end) < 0.00000001) pi0 = seg0.end;
				if (Point.distance(pi0,seg1.start) < 0.00000001) pi0 = seg1.start;
				if (Point.distance(pi0,seg1.end) < 0.00000001) pi0 = seg1.end;*/
                return (1, new[] { pi0, pi1 });
            }

            // lines of the segments are parallel
            var sqrLenE = E.Length;
            cross = E.I * d0.J - E.J * d0.I;
            sqrCross = cross * cross;
            if (sqrCross > sqrEpsilon * sqrLen0 * sqrLenE)
            {
                // lines of the segment are different
                return (0, new[] { pi0, pi1 });
            }

            // Lines of the segments are the same. Need to test for overlap of segments.
            var s0 = (d0.I * E.I + d0.J * E.J) / sqrLen0;  // so = Dot (D0, E) * sqrLen0
            var s1 = s0 + (d0.I * d1.I + d0.J * d1.J) / sqrLen0;  // s1 = s0 + Dot (D0, D1) * sqrLen0
            var smin = Math.Min(s0, s1);
            var smax = Math.Max(s0, s1);
            (int imax, double[] w) = FindIntersection(0.0, 1.0, smin, smax);

            if (imax > 0)
            {
                pi0.X = p0.X + w[0] * d0.I;
                pi0.Y = p0.Y + w[0] * d0.J;

                // Uncomment the block below if you're getting errors to do with precision.
                /*if (Point.distance(pi0,seg0.start) < 0.00000001) pi0 = seg0.start;
				if (Point.distance(pi0,seg0.end) < 0.00000001) pi0 = seg0.end;
				if (Point.distance(pi0,seg1.start) < 0.00000001) pi0 = seg1.start;
				if (Point.distance(pi0,seg1.end) < 0.00000001) pi0 = seg1.end;*/
                if (imax > 1)
                {
                    pi1.X = p0.X + w[1] * d0.I;
                    pi1.Y = p0.Y + w[1] * d0.J;
                }
            }

            return (imax, new[] { pi0, pi1 });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u0"></param>
        /// <param name="u1"></param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        private static (int, double[] w) FindIntersection(double u0, double u1, double v0, double v1)
        {
            var w = new double[2];
            if ((u1 < v0) || (u0 > v1))
                return (0, w);
            if (u1 > v0)
            {
                if (u0 < v1)
                {
                    w[0] = (u0 < v0) ? v0 : u0;
                    w[1] = (u1 > v1) ? v1 : u1;
                    return (2, w);
                }
                else
                {
                    // u0 == v1
                    w[0] = u0;
                    return (1, w);
                }
            }
            else
            {
                // u1 == v0
                w[0] = u1;
                return (1, w);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        private void PossibleIntersection(SweepEvent e1, SweepEvent e2)
        {
            //if ((e1.pl == e2.pl)) // Uncomment these two lines if self-intersecting polygons are not allowed
            //    return false;

            (int numIntersections, Point2D[] ip) = FindIntersection(e1.Segment(), e2.Segment());
            var ip1 = ip[0];
            var ip2 = ip[1];

            if (numIntersections == 0)
                return;

            if ((numIntersections == 1) && (e1.Point.Equals(e2.Point) || e1.OtherEvent.Point.Equals(e2.OtherEvent.Point)))
                return;

            if (numIntersections == 2 && e1.Point.Equals(e2.Point))
                return;

            if (numIntersections == 1)
            {
                if (!e1.Point.Equals(ip1) && !e1.OtherEvent.Point.Equals(ip1))
                    DivideSegment(e1, ip1);
                if (!e2.Point.Equals(ip1) && !e2.OtherEvent.Point.Equals(ip1))
                    DivideSegment(e2, ip1);
                return;
            }

            var sortedEvents = new List<SweepEvent>();
            if (e1.Point.Equals(e2.Point))
            {
                sortedEvents.Add(null); // WTF
            }
            else if (SweepEventComp(e1, e2) > 0)
            {
                sortedEvents.Add(e2);
                sortedEvents.Add(e1);
            }
            else
            {
                sortedEvents.Add(e1);
                sortedEvents.Add(e2);
            }

            if (e1.OtherEvent.Point.Equals(e2.OtherEvent.Point))
            {
                sortedEvents.Add(null);
            }
            else if (SweepEventComp(e1.OtherEvent, e2.OtherEvent) > 0)
            {
                sortedEvents.Add(e2.OtherEvent);
                sortedEvents.Add(e1.OtherEvent);
            }
            else
            {
                sortedEvents.Add(e1.OtherEvent);
                sortedEvents.Add(e2.OtherEvent);
            }

            if (sortedEvents.Count == 2)
            {
                e1.Contribution = e1.OtherEvent.Contribution = EdgeContributions.NonContributing;
                e2.Contribution = e2.OtherEvent.Contribution = ((e1.InOut == e2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition);
                return;
            }

            if (sortedEvents.Count == 3)
            {
                sortedEvents[1].Contribution = sortedEvents[1].OtherEvent.Contribution = EdgeContributions.NonContributing;
                if (sortedEvents[0] != null)         // is the right endpoint the shared point?
                    sortedEvents[0].OtherEvent.Contribution = (e1.InOut == e2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition;
                else                                // the shared point is the left endpoint
                    sortedEvents[2].OtherEvent.Contribution = (e1.InOut == e2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition;

                DivideSegment(sortedEvents[0] ?? sortedEvents[2].OtherEvent, sortedEvents[1].Point);
                return;
            }

            if (sortedEvents[0] != sortedEvents[3].OtherEvent)
            { // no segment includes totally the otherSE one
                sortedEvents[1].Contribution = EdgeContributions.NonContributing;
                sortedEvents[2].Contribution = (e1.InOut == e2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition;

                DivideSegment(sortedEvents[0], sortedEvents[1].Point);

                DivideSegment(sortedEvents[1], sortedEvents[2].Point);
                return;
            }

            sortedEvents[1].Contribution = sortedEvents[1].OtherEvent.Contribution = EdgeContributions.NonContributing;

            DivideSegment(sortedEvents[0], sortedEvents[1].Point);
            sortedEvents[3].OtherEvent.Contribution = (e1.InOut == e2.InOut) ? EdgeContributions.SameTransition : EdgeContributions.DifferentTransition;

            DivideSegment(sortedEvents[3].OtherEvent, sortedEvents[2].Point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        private void DivideSegment(SweepEvent e, Point2D p)
        {
            var left = new SweepEvent(true, p, e.OtherEvent, e.BelongsTo, e.OtherEvent.Contribution);
            var right = new SweepEvent(false, p, e, e.BelongsTo, e.Contribution);

            if (SweepEventComp(left, e.OtherEvent) > 0)
            {
                e.OtherEvent.IsLeft = true;
                e.IsLeft = false;
            }

            e.OtherEvent.OtherEvent = left;
            e.OtherEvent = right;

            eventQueue.Enqueue(left);
            eventQueue.Enqueue(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="polyType"></param>
        private void ProcessSegment(LineSegment segment, ClippingRelations polyType)
        {
            if (segment.A.Equals(segment.B)) // Possible degenerate condition.
                return;

            var e1 = new SweepEvent(true, segment.A, null, polyType);
            var e2 = new SweepEvent(true, segment.B, e1, polyType);
            e1.OtherEvent = e2;

            if (e1.Point.X < e2.Point.X)
            {
                e2.IsLeft = false;
            }
            else if (e1.Point.X > e2.Point.X)
            {
                e1.IsLeft = false;
            }
            else if (e1.Point.Y < e2.Point.Y)
            {
                // the segment isLeft vertical. The bottom endpoint isLeft the isLeft endpoint 
                e2.IsLeft = false;
            }
            else
            {
                e1.IsLeft = false;
            }

            // Pushing it so the queue is sorted from left to right, with object on the left
            // having the highest priority.
            eventQueue.Enqueue(e1);
            eventQueue.Enqueue(e2);
        }


    }
}
