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
        /// event associated to the other endpoint of the edge
        /// </summary>
        private SweepEvent otherEvent;

        /// <summary>
        /// Polygon to which the associated segment belongs to
        /// </summary>
        private ClippingRelations belongsTo;

        /// <summary>
        /// The contribution.
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
        /// The in result.
        /// </summary>
        private bool inResult;

        /// <summary>
        /// The pos.
        /// </summary>
        private int pos;

        /// <summary>
        /// The result in out.
        /// </summary>
        private bool resultInOut;

        /// <summary>
        /// The contour id.
        /// </summary>
        private int contourId;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SweepEvent"/> class.
        /// </summary>
        public SweepEvent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SweepEvent"/> class.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="p">The p.</param>
        /// <param name="other">The other.</param>
        /// <param name="pt">The pt.</param>
        /// <param name="et">The et.</param>
        public SweepEvent(bool b, Point2D p, SweepEvent other, ClippingRelations pt, EdgeContributions et = EdgeContributions.Normal)
        {
            IsLeft = b;
            Point = p;
            OtherEvent = other;
            BelongsTo = pt;
            Contribution = et;
            PrevInResult = null;
            InResult = false;
        }
        #endregion Constructors

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
        public ClippingRelations BelongsTo { get { return belongsTo; } set { belongsTo = value; } }

        /// <summary>
        /// Event associated to the other endpoint of the edge.
        /// </summary>
        public SweepEvent OtherEvent { get { return otherEvent; } set { otherEvent = value; } }

        /// <summary>
        /// Gets or sets the contribution.
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
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool InResult { get { return inResult; } set { inResult = value; } }

        /// <summary>
        /// Gets or sets the pos.
        /// </summary>
        public int Pos { get { return pos; } set { pos = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool ResultInOut { get { return resultInOut; } set { resultInOut = value; } }

        /// <summary>
        /// Gets or sets the contour id.
        /// </summary>
        public int ContourId { get { return contourId; } set { contourId = value; } }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator Less than.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator <(SweepEvent a, SweepEvent b)
            => SweepEventComp(a, b) < 0;

        /// <summary>
        /// The operator Greater than.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator >(SweepEvent a, SweepEvent b)
            => SweepEventComp(a, b) > 0;
        #endregion Operators

        #region Methods
        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(SweepEvent other)
            => SweepEventComp(this, other);

        /// <summary>
        /// Is the line segment (point, otherEvent.point) below point p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsBelow(Point2D p)
            => IsLeft ? SignedTriangleArea(Point, OtherEvent.Point, p) > 0 : SignedTriangleArea(OtherEvent.Point, Point, p) > 0;

        /// <summary>
        /// Is the line segment (point, otherEvent.point) above point p
        /// </summary>
        /// <param name="p">todo: describe p parameter on IsAbove</param>
        /// <returns></returns>
        public bool IsAbove(Point2D p)
            => !IsBelow(p);

        /// <summary>
        /// Return the line segment associated to the SweepEvent
        /// </summary>
        /// <returns></returns>
        public LineSegment Segment()
            => new LineSegment(Point, OtherEvent.Point);

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
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
            var inOutOutIn = InOut ? nameof(inOut) : "outIn";
            oss += $" ({inOutOutIn})";
            var otherInOutOutIn = OtherInOut ? nameof(inOut) : "outIn";
            oss += $" otherInOut: ({otherInOutOutIn})";
            return oss;
        }
        #endregion Methods
    }
}