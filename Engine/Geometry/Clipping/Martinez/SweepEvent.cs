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
using static Engine.Measurements;
using static Engine.SegmentComparators;

namespace Engine
{
    /// <summary>
    /// A container for SweepEvent data. A SweepEvent represents a location of interest (vertex between two polygon edges)
    /// as the sweep line passes through the polygons.
    /// </summary>
    public class SweepEvent
        : IComparable<SweepEvent>
    {
        #region Fields

        /// <summary>
        /// is point the left endpoint of the edge (point, otherEvent.point)?
        /// </summary>
        private bool isLeft;

        /// <summary>
        /// point associated with the event
        /// </summary>
        private Point2D point;

        /// <summary>
        /// Polygon to which the associated segment belongs to
        /// </summary>
        private PolygonRelations belongsTo;

        /// <summary>
        /// event associated to the other endpoint of the edge
        /// </summary>
        private SweepEvent otherEvent;

        /// <summary>
        /// 
        /// </summary>
        private EdgeContributions contribution;

        //The following fields are only used in "left" events

        /// <summary>
        /// Does segment (point, otherEvent.p) represent an inside-outside transition in the polygon for a vertical ray from (p.x, -infinite)?
        /// </summary>
        private bool inOut;

        /// <summary>
        /// inOut transition for the segment from the other polygon preceding this segment in sl
        /// </summary>
        private bool otherInOut;

        /// <summary>
        /// Position of the event (line segment) in sl
        /// </summary>
        private SortedSet<SweepEvent> posSL;

        /// <summary>
        /// previous segment in sl belonging to the result of the boolean operation
        /// </summary>
        private SweepEvent prevInResult;

        /// <summary>
        /// 
        /// </summary>
        private bool inResult;

        /// <summary>
        /// 
        /// </summary>
        private int pos;

        /// <summary>
        /// 
        /// </summary>
        private bool resultInOut;

        /// <summary>
        /// 
        /// </summary>
        private uint contourId;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SweepEvent()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="other"></param>
        /// <param name="pt"></param>
        /// <param name="et"></param>
        public SweepEvent(bool b, Point2D p, SweepEvent other, PolygonRelations pt, EdgeContributions et = EdgeContributions.Normal)
        {
            IsLeft = b;
            Point = p;
            OtherEvent = other;
            BelongsTo = pt;
            Contribution = et;
            PrevInResult = null;
            InResult = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Is the line segment (point, otherEvent.point) a vertical line segment
        /// </summary>
        /// <returns></returns>
        public bool IsVertical
            => Point.X == OtherEvent.Point.X;

        /// <summary>
        /// is point the left endpoint of the edge (point, otherEvent.point)?
        /// </summary>
        public bool IsLeft { get { return isLeft; } set { isLeft = value; } }

        /// <summary>
        /// point associated with the event
        /// </summary>
        public Point2D Point { get { return point; } set { point = value; } }

        /// <summary>
        /// Polygon to which the associated segment belongs to
        /// </summary>
        public PolygonRelations BelongsTo { get { return belongsTo; } set { belongsTo = value; } }

        /// <summary>
        /// Event associated to the other endpoint of the edge.
        /// </summary>
        public SweepEvent OtherEvent { get { return otherEvent; } set { otherEvent = value; } }

        /// <summary>
        /// 
        /// </summary>
        public EdgeContributions Contribution { get { return contribution; } set { contribution = value; } }

        // The following properties are only used in "left" events.

        /// <summary>
        /// Does segment (point, otherEvent.p) represent an inside-outside transition in the polygon for a vertical ray from (p.x, -infinite)?
        /// </summary>
        public bool InOut { get { return inOut; } set { inOut = value; } }

        /// <summary>
        /// inOut transition for the segment from the other polygon preceding this segment in sl
        /// </summary>
        public bool OtherInOut { get { return otherInOut; } set { otherInOut = value; } }

        /// <summary>
        /// Position of the event (line segment) in sl.
        /// </summary>
        public SortedSet<SweepEvent> PosSL { get { return posSL; } set { posSL = value; } }

        /// <summary>
        /// previous segment in sl belonging to the result of the boolean operation.
        /// </summary>
        public SweepEvent PrevInResult { get { return prevInResult; } set { prevInResult = value; } }

        /// <summary>
        /// 
        /// </summary>
        public bool InResult { get { return inResult; } set { inResult = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int Pos { get { return pos; } set { pos = value; } }

        /// <summary>
        /// 
        /// </summary>
        public bool ResultInOut { get { return resultInOut; } set { resultInOut = value; } }

        /// <summary>
        /// 
        /// </summary>
        public uint ContourId { get { return contourId; } set { contourId = value; } }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(SweepEvent a, SweepEvent b)
            => SweepEventComp(a, b) < 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(SweepEvent a, SweepEvent b)
            => SweepEventComp(a, b) > 0;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(SweepEvent other)
            => SweepEventComp(this, other);

        /// <summary>
        /// Is the line segment (point, otherEvent.point) below point p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsBelow(Point2D p)
            => (IsLeft) ? SignedTriangleArea(Point, OtherEvent.Point, p) > 0 : SignedTriangleArea(OtherEvent.Point, Point, p) > 0;

        /// <summary>
        /// Is the line segment (point, otherEvent.point) above point p
        /// </summary>
        /// <returns></returns>
        public bool IsAbove(Point2D p)
            => !IsBelow(p);

        /// <summary>
        /// Return the line segment associated to the SweepEvent
        /// </summary>
        /// <returns></returns>
        public LineSegment Segment()
            => new LineSegment(Point, OtherEvent.Point);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var oss = "";
            oss += $"({Point.X},{Point.Y})";
            var leftRight = IsLeft ? "left" : "right";
            oss += $" ({leftRight})";
            var s = new LineSegment(Point, OtherEvent.Point);
            oss += $" S:[({s.Min.X},{s.Min.Y}) - ({s.Max.X},{s.Max.Y})]";
            oss += $" ({BelongsTo.ToString()})";
            oss += $" ({Contribution.ToString()})";
            var inOutOutIn = InOut ? "inOut" : "outIn";
            oss += $" ({inOutOutIn})";
            var otherInOutOutIn = OtherInOut ? "inOut" : "outIn";
            oss += $" otherInOut: ({otherInOutOutIn})";
            return oss;
        }

        #endregion
    }
}