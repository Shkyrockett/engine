// <copyright file="CubicControlPoint.cs" company="Shkyrockett" >
//     Copyright © 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The control point class.
    /// </summary>
    public struct CubicControlPoint
        : IPrimitive
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicControlPoint"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="anchorA">The anchorA.</param>
        /// <param name="anchorB">The anchorB.</param>
        public CubicControlPoint(Point2D point, Point2D anchorA, Point2D anchorB)
        {
            Point = point;
            AnchorA = anchorA;
            AnchorB = anchorB;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// Gets or sets the horizontal anchor.
        /// </summary>
        public Point2D AnchorA { get; set; }

        /// <summary>
        /// Gets or sets the vertical anchor.
        /// </summary>
        public Point2D AnchorB { get; set; }

        /// <summary>
        /// Gets or sets the global horizontal anchor.
        /// </summary>
        public Point2D AnchorAGlobal { get { return LocalToGlobal(AnchorA); } set { AnchorA = GlobalToLocal(value); } }

        /// <summary>
        /// Gets or sets the global vertical anchor.
        /// </summary>
        public Point2D AnchorBGlobal { get { return LocalToGlobal(AnchorB); } set { AnchorB = GlobalToLocal(value); } }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(CubicControlPoint left, CubicControlPoint right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(CubicControlPoint left, CubicControlPoint right)
            => !(left == right);
        #endregion Operators

        #region Methods
        /// <summary>
        /// The local to global method.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Point2D LocalToGlobal(Point2D point)
            => new Point2D(point.X + Point.X, point.Y + Point.Y);

        /// <summary>
        /// The global to local method.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Point2D GlobalToLocal(Point2D point)
            => new Point2D(Point.X - point.X, Point.Y - point.Y);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => Point.GetHashCode()
            ^ AnchorA.GetHashCode()
            ^ AnchorB.GetHashCode();

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(CubicControlPoint a, CubicControlPoint b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(CubicControlPoint a, CubicControlPoint b)
            => (a.Point == b.Point) & (a.AnchorA == b.AnchorA) & (a.AnchorB == b.AnchorB);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is CubicControlPoint && Equals(this, (CubicControlPoint)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CubicControlPoint value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="CubicControlPoint"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="CubicControlPoint"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="CubicControlPoint"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="CubicControlPoint"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Point2D)}{{{nameof(Point)}={Point.ToString(format, provider)}{sep}{nameof(AnchorA)}={AnchorA.ToString(format, provider)}{sep}{nameof(AnchorB)}={AnchorB.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
