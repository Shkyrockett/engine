/*
  A port of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    ///// <summary>
    ///// The Line 2D class.
    ///// </summary>
    ///// <seealso cref="IEquatable{T}" />
    //public struct Line2D : IEquatable<Line2D>
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="Line2D" /> class.
    //    /// </summary>
    //    /// <param name="p1">The p1.</param>
    //    /// <param name="p2">The p2.</param>
    //    public Line2D(Point2D p1, Point2D p2)
    //        : this()
    //    {
    //        Location = p1;
    //        Direction = p2;
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="Line2D" /> class.
    //    /// </summary>
    //    /// <param name="x0">The x0.</param>
    //    /// <param name="y0">The y0.</param>
    //    /// <param name="x1">The x1.</param>
    //    /// <param name="y1">The y1.</param>
    //    public Line2D(double x0, double y0, double x1, double y1)
    //        : this()
    //    {
    //        Location = new Point2D(x0, y0);
    //        Direction = new Point2D(x1, y1);
    //    }

    //    /// <summary>
    //    /// Gets or sets the p1.
    //    /// </summary>
    //    /// <value>
    //    /// The p1.
    //    /// </value>
    //    public Point2D Location { get; set; }

    //    /// <summary>
    //    /// Gets or sets the p2.
    //    /// </summary>
    //    /// <value>
    //    /// The p2.
    //    /// </value>
    //    public Point2D Direction { get; set; }

    //    /// <summary>
    //    /// Implements the operator ==.
    //    /// </summary>
    //    /// <param name="left">The left.</param>
    //    /// <param name="right">The right.</param>
    //    /// <returns>
    //    /// The result of the operator.
    //    /// </returns>
    //    public static bool operator ==(Line2D left, Line2D right) => left.Equals(right);

    //    /// <summary>
    //    /// Implements the operator !=.
    //    /// </summary>
    //    /// <param name="left">The left.</param>
    //    /// <param name="right">The right.</param>
    //    /// <returns>
    //    /// The result of the operator.
    //    /// </returns>
    //    public static bool operator !=(Line2D left, Line2D right) => !(left == right);

    //    /// <summary>
    //    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    //    /// </summary>
    //    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    //    /// <returns>
    //    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    //    /// </returns>
    //    public override bool Equals(object obj) => obj is Line2D d && Equals(d);

    //    /// <summary>
    //    /// Indicates whether the current object is equal to another object of the same type.
    //    /// </summary>
    //    /// <param name="other">An object to compare with this object.</param>
    //    /// <returns>
    //    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    //    /// </returns>
    //    public bool Equals([AllowNull] Line2D other) => Location.Equals(other.Location) && Direction.Equals(other.Direction);

    //    /// <summary>
    //    /// Returns a hash code for this instance.
    //    /// </summary>
    //    /// <returns>
    //    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    //    /// </returns>
    //    public override int GetHashCode() => HashCode.Combine(Location, Direction);
    //}
}
