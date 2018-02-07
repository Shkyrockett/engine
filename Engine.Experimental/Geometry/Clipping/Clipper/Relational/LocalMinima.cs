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
using System.Runtime.CompilerServices;

namespace Engine.Experimental
{
    /// <summary>
    /// The local minima class.
    /// </summary>
    public struct LocalMinima
        : IComparable<LocalMinima>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the vertex of the local minima.
        /// </summary>
        public Vertex Vertex { get; set; }

        /// <summary>
        /// Gets or sets the polygon's clipping relation.
        /// </summary>
        public ClippingRelations ClippingRelation { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the polygon is an open polyline, or a closed polygon.
        /// </summary>
        public bool IsOpen { get; set; }
        #endregion Properties

        #region MyRegion
        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(LocalMinima left, LocalMinima right) => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(LocalMinima left, LocalMinima right) => !(left == right);

        /// <summary>
        /// The operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator <(LocalMinima left, LocalMinima right) => left.CompareTo(right) < 0;

        /// <summary>
        /// The operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator <=(LocalMinima left, LocalMinima right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// The operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator >(LocalMinima left, LocalMinima right) => left.CompareTo(right) > 0;

        /// <summary>
        /// The operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator >=(LocalMinima left, LocalMinima right) => left.CompareTo(right) >= 0;
        #endregion MyRegion

        #region Methods
        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(LocalMinima other)
            => Compare(this, other);

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="lm1">The lm1.</param>
        /// <param name="lm2">The lm2.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Compare(LocalMinima lm1, LocalMinima lm2)
            => lm2.Vertex.Point.Y.CompareTo(lm1.Vertex.Point.Y); // Soft descending sort

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj) => CompareTo((LocalMinima)obj) == 0;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode() => base.GetHashCode();
        #endregion Methods
    }
}
