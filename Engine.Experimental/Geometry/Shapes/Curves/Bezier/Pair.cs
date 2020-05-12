/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The pair class.
    /// </summary>
    public class Pair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public Pair(Bezier left, Bezier right)
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="span">The span.</param>
        public Pair(Bezier left, Bezier right, List<Point2D> span)
            : this(left, right)
        {
            Span = span;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> class.
        /// </summary>
        public Pair()
            : this(null, null)
        { }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public Bezier Left { get; internal set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public Bezier Right { get; internal set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// Gets or sets the s1.
        /// </summary>
        public Shape1 S1 { get; internal set; }

        /// <summary>
        /// Gets or sets the s2.
        /// </summary>
        public Shape1 S2 { get; internal set; }

        /// <summary>
        /// Gets or sets the span.
        /// </summary>
        public List<Point2D> Span { get; internal set; }

        /// <summary>
        /// Gets or sets the t1.
        /// </summary>
        public double T1 { get; internal set; }

        /// <summary>
        /// Gets or sets the t2.
        /// </summary>
        public double T2 { get; internal set; }

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Pair left, Pair right) => left?.Equals(right) ?? right is null;

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Pair left, Pair right) => !(left == right);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool Equals(Pair left, Pair right) => left?.Left == right?.Left && right.Right == left.Right;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj) => obj is Pair pair && Equals(this, pair);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode() => Left.GetHashCode() ^ Right.GetHashCode();
    }
}
