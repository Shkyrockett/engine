/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The line1 class.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    public struct Line3D : IEquatable<Line3D>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line3D" /> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line3D(Point3D p1, Point3D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Gets or sets the p1.
        /// </summary>
        /// <value>
        /// The p1.
        /// </value>
        public Point3D P1 { get; set; }

        /// <summary>
        /// Gets or sets the p2.
        /// </summary>
        /// <value>
        /// The p2.
        /// </value>
        public Point3D P2 { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Line3D left, Line3D right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Line3D left, Line3D right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Line3D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Line3D other) => P1.Equals(other.P1) && P2.Equals(other.P2);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 162377905;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point3D>.Default.GetHashCode(P1);
            hashCode = hashCode * -1521134295 + EqualityComparer<Point3D>.Default.GetHashCode(P2);
            return hashCode;
        }
    }
}
