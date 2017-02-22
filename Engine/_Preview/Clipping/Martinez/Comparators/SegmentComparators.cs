// <copyright file="SegmentComparators.cs" >
//     Copyright (c) 2012 Francisco Martínez del Río. All rights reserved.
// </copyright>
// <author id="fmartin@ujaen.es">Francisco Martínez del Río</author>
// <license>
//     This code is public domain.
// </license>
// <summary></summary>
// <remarks> http://www4.ujaen.es/~fmartin/bool_op.html </remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Measurements;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class SegmentComparators
    {
        /// <summary>
        /// le1 and le2 are the left events of line segments (le1.point, le1.otherEvent.point) and (le2.point, le2.otherEvent.point)
        /// </summary>
        /// <param name="le1"></param>
        /// <param name="le2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SegmentComp(SweepEvent le1, SweepEvent le2)
        {
            if (le1 == le2)
                return 0;
            if (SignedTriangleArea(le1.Point, le1.OtherEvent.Point, le2.Point) != 0 ||
                SignedTriangleArea(le1.Point, le1.OtherEvent.Point, le2.OtherEvent.Point) != 0)
            {
                // Segments are not collinear
                // If they share their left endpoint use the right endpoint to sort
                if (le1.Point == le2.Point)
                    return le1.IsBelow(le2.OtherEvent.Point) ? 1 : -1;
                // Different left endpoint: use the left endpoint to sort
                if (le1.Point.X == le2.Point.X)
                    return le1.Point.Y < le2.Point.Y ? 1 : -1;
                if (SweepEventComp(le1, le2) != 0)  // has the line segment associated to e1 been inserted into S after the line segment associated to e2 ?
                    return le2.IsAbove(le1.Point) ? 1 : -1;
                // The line segment associated to e2 has been inserted into S after the line segment associated to e1
                return le1.IsBelow(le2.Point) ? 1 : -1;
            }
            // Segments are collinear
            if (le1.BelongsTo != le2.BelongsTo)
                return le1.BelongsTo < le2.BelongsTo ? 1 : -1;
            // Just a consistent criterion is used
            if (le1.Point == le2.Point)
                return le1 < le2 ? 1 : -1;
            return SweepEventComp(le1, le2);
        }

        /// <summary>
        /// Compare two sweep events
        /// Return true means that e1 is placed at the event queue after e2, i.e,, e1 is processed by the algorithm after e2
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SweepEventComp(SweepEvent e1, SweepEvent e2)
        {
            if (e1 == e2)
                return 0;
            if (e1.Point.X > e2.Point.X) // Different x-coordinate
                return 1;
            if (e2.Point.X > e1.Point.X) // Different x-coordinate
                return -1;
            if (e1.Point.Y != e2.Point.Y) // Different points, but same x-coordinate. The event with lower y-coordinate is processed first
                return (e1.Point.Y > e2.Point.Y) ? 1 : -1;
            if (e1.IsLeft != e2.IsLeft) // Same point, but one is a left endpoint and the other a right endpoint. The right endpoint is processed first
                return e1.IsLeft ? 1 : -1;
            // Same point, both events are left endpoints or both are right endpoints.
            if (SignedTriangleArea(e1.Point, e1.OtherEvent.Point, e2.OtherEvent.Point) != 0) // not collinear
                return e1.IsAbove(e2.OtherEvent.Point) ? 1 : -1; // the event associate to the bottom segment is processed first
            return e1.BelongsTo > e2.BelongsTo ? 1 : -1;
        }

        /// <summary>
        /// IMPORTANT NOTE: This is not the same as the function in Sweep elements.
        /// The ordering is reversed because push and pop are faster.
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseSweepEventComp(SweepEvent e1, SweepEvent e2)
        {
            if (e1 == e2)
                return 0;
            if (e1.Point.X > e2.Point.X) // Different x-coordinate
                return -1;
            if (e2.Point.X > e1.Point.X) // Different x-coordinate
                return 1;
            if (e1.Point != e2.Point) // Different points, but same x-coordinate. The event with lower y-coordinate is processed first
                return (e1.Point.Y > e2.Point.Y) ? -1 : 1;
            if (e1.IsLeft != e2.IsLeft) // Same point, but one is a left endpoint and the other a right endpoint. The right endpoint is processed first
                return (e1.IsLeft) ? -1 : 1;
            // Same point, both events are left endpoints or both are right endpoints. The event associate to the bottom segment is processed first
            if (SignedTriangleArea(e1.Point, e1.OtherEvent.Point, e2.OtherEvent.Point) != 0) // not collinear
                return e1.IsAbove(e2.OtherEvent.Point) ? -1 : 1;
            return e1.BelongsTo > e2.BelongsTo ? -1 : 1;
        }
    }
}
