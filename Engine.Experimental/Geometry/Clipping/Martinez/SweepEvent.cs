// <copyright file="SegmentComparators.cs" >
// Copyright © 2012 Francisco Martínez del Río. All rights reserved.
// </copyright>
// <author id="fmartin@ujaen.es">Francisco Martínez del Río</author>
// <license>
//     This code is public domain.
// </license>
// <summary></summary>
// <remarks> http://www4.ujaen.es/~fmartin/bool_op.html </remarks>

using static Engine.Measurements;
using static Engine.SegmentComparators;

namespace Engine;

/// <summary>
/// A container for SweepEvent data. A SweepEvent represents a location of interest (vertex between two polygon edges)
/// as the sweep line passes through the polygons.
/// </summary>
/// <seealso cref="IComparable{T}" />
/// <seealso cref="IEquatable{T}" />
public class SweepEvent
    : IComparable<SweepEvent>
{
    #region Fields
    /// <summary>
    /// point associated with the event
    /// </summary>
    private Point2D point;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="SweepEvent" /> class.
    /// </summary>
    public SweepEvent()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SweepEvent" /> class.
    /// </summary>
    /// <param name="b">The b.</param>
    /// <param name="p">The p.</param>
    /// <param name="other">The other.</param>
    /// <param name="pt">The pt.</param>
    /// <param name="et">The et.</param>
    public SweepEvent(bool b, Point2D p, SweepEvent other, ClippingRelation pt, EdgeContribution et = EdgeContribution.Normal)
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
    /// <value>
    ///   <see langword="true" /> if this instance is vertical; otherwise, <see langword="false" />.
    /// </value>
    public bool IsVertical => Point.X == OtherEvent.Point.X;

    /// <summary>
    /// is point the left endpoint of the edge (point, otherEvent.point)?
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if this instance is left; otherwise, <see langword="false" />.
    /// </value>
    public bool IsLeft { get; set; }

    /// <summary>
    /// point associated with the event
    /// </summary>
    /// <value>
    /// The point.
    /// </value>
    public Point2D Point { get { return point; } set { point = value; } }

    /// <summary>
    /// Polygon to which the associated segment belongs to
    /// </summary>
    /// <value>
    /// The belongs to.
    /// </value>
    public ClippingRelation BelongsTo { get; set; }

    /// <summary>
    /// Event associated to the other endpoint of the edge.
    /// </summary>
    /// <value>
    /// The other event.
    /// </value>
    public SweepEvent OtherEvent { get; set; }

    /// <summary>
    /// Gets or sets the contribution.
    /// </summary>
    /// <value>
    /// The contribution.
    /// </value>
    public EdgeContribution Contribution { get; set; }

    // The following properties are only used in "left" events.

    /// <summary>
    /// Does segment (point, otherEvent.p) represent an inside-outside transition in the polygon for a vertical ray from (p.x, -infinite)?
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if [in out]; otherwise, <see langword="false" />.
    /// </value>
    public bool InOut { get; set; }

    /// <summary>
    /// inOut transition for the segment from the other polygon preceding this segment in sl
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if [other in out]; otherwise, <see langword="false" />.
    /// </value>
    public bool OtherInOut { get; set; }

    /// <summary>
    /// Position of the event (line segment) in sl.
    /// </summary>
    /// <value>
    /// The position sl.
    /// </value>
    public SortedSet<SweepEvent> PosSL { get; set; }

    /// <summary>
    /// previous segment in sl belonging to the result of the boolean operation.
    /// </summary>
    /// <value>
    /// The previous in result.
    /// </value>
    public SweepEvent PrevInResult { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if [in result]; otherwise, <see langword="false" />.
    /// </value>
    public bool InResult { get; set; } = false;

    /// <summary>
    /// Gets or sets the pos.
    /// </summary>
    /// <value>
    /// The position.
    /// </value>
    public int Pos { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if [result in out]; otherwise, <see langword="false" />.
    /// </value>
    public bool ResultInOut { get; set; }

    /// <summary>
    /// Gets or sets the contour id.
    /// </summary>
    /// <value>
    /// The contour identifier.
    /// </value>
    public int ContourId { get; set; }
    #endregion Properties

    #region Operators
    /// <summary>
    /// The operator Less than.
    /// </summary>
    /// <param name="left">The a.</param>
    /// <param name="right">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public static bool operator <(SweepEvent left, SweepEvent right) => left is null || SweepEventComp(left, right) < 0;

    /// <summary>
    /// The operator Greater than.
    /// </summary>
    /// <param name="left">The a.</param>
    /// <param name="right">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public static bool operator >(SweepEvent left, SweepEvent right) => left is null ? right is null : SweepEventComp(left, right) > 0;

    /// <summary>
    /// Implements the operator &lt;=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator <=(SweepEvent left, SweepEvent right) => left is null || SweepEventComp(left, right) <= 0;

    /// <summary>
    /// Implements the operator &gt;=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator >=(SweepEvent left, SweepEvent right) => left is null ? right is null : SweepEventComp(left, right) >= 0;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(SweepEvent left, SweepEvent right) => left is null ? right is null : left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(SweepEvent left, SweepEvent right) => !(left == right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// The compare to.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    public int CompareTo(SweepEvent other) => SweepEventComp(this, other);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode() => HashCode.Combine(IsLeft, point, OtherEvent, BelongsTo, Contribution, PrevInResult, InResult);

    /// <summary>
    /// Is the line segment (point, otherEvent.point) below point p
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified p is below; otherwise, <see langword="false" />.
    /// </returns>
    public bool IsBelow(Point2D p) => IsLeft ? SignedTriangleArea(Point, OtherEvent.Point, p) > 0 : SignedTriangleArea(OtherEvent.Point, Point, p) > 0;

    /// <summary>
    /// Is the line segment (point, otherEvent.point) above point p
    /// </summary>
    /// <param name="p">ToDo: describe p parameter on IsAbove</param>
    /// <returns>
    ///   <see langword="true" /> if the specified p is above; otherwise, <see langword="false" />.
    /// </returns>
    public bool IsAbove(Point2D p) => !IsBelow(p);

    /// <summary>
    /// Return the line segment associated to the SweepEvent
    /// </summary>
    /// <returns></returns>
    public LineSegment2D Segment() => new(Point, OtherEvent.Point);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        var oss = "";
        oss += $"({Point.X},{Point.Y})";
        var leftRight = IsLeft ? "left" : "right";
        oss += $" ({leftRight})";
        var s = new LineSegment2D(Point, OtherEvent.Point);
        oss += $" S:[({s.Min.X},{s.Min.Y}) - ({s.Max.X},{s.Max.Y})]";
        oss += $" ({BelongsTo})";
        oss += $" ({Contribution})";
        var inOutOutIn = InOut ? nameof(InOut) : "outIn";
        oss += $" ({inOutOutIn})";
        var otherInOutOutIn = OtherInOut ? nameof(InOut) : "outIn";
        oss += $" otherInOut: ({otherInOutOutIn})";
        return oss;
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj) => base.Equals(obj);
    #endregion Methods
}