// <copyright file="Envelope.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
using static Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// The envelope distort class.
    /// </summary>
    public struct Envelope : IEquatable<Envelope>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Envelope"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Envelope(double x, double y, double width, double height)
        {
            var w3 = width * OneThird;
            var h3 = height * OneThird;

            //  Top Left
            ControlPointTopLeft = new CubicControlPoint
            {
                Point = new Point2D(x, y),
                AnchorA = new Point2D(w3, 0),
                AnchorB = new Point2D(0, h3)
            };

            //  Top Right
            ControlPointTopRight = new CubicControlPoint
            {
                Point = new Point2D(x + width, y),
                AnchorA = new Point2D(-w3, 0),
                AnchorB = new Point2D(0, h3)
            };

            //  Bottom Left
            ControlPointBottomLeft = new CubicControlPoint
            {
                Point = new Point2D(x, y + height),
                AnchorA = new Point2D(w3, 0),
                AnchorB = new Point2D(0, -h3)
            };

            //  Bottom Right
            ControlPointBottomRight = new CubicControlPoint
            {
                Point = new Point2D(x + width, y + height),
                AnchorA = new Point2D(-w3, 0),
                AnchorB = new Point2D(0, -h3)
            };

            //Update();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the control point top left.
        /// </summary>
        public CubicControlPoint ControlPointTopLeft { get; set; }

        /// <summary>
        /// Gets or sets the control point top right.
        /// </summary>
        public CubicControlPoint ControlPointTopRight { get; set; }

        /// <summary>
        /// Gets or sets the control point bottom left.
        /// </summary>
        public CubicControlPoint ControlPointBottomLeft { get; set; }

        /// <summary>
        /// Gets or sets the control point bottom right.
        /// </summary>
        public CubicControlPoint ControlPointBottomRight { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Envelope left, Envelope right)
            => left.Equals(right);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Envelope left, Envelope right)
            => !(left == right);
        #endregion Operators

        #region Methods
        /// <summary>
        /// The to polycurve.
        /// </summary>
        /// <returns>The <see cref="PolycurveContour"/>.</returns>
        public PolycurveContour ToPolycurve()
        {
            var curve = new PolycurveContour(ControlPointTopLeft.Point);
            curve.AddCubicBezier(ControlPointTopLeft.AnchorAGlobal, ControlPointTopRight.AnchorAGlobal, ControlPointTopRight.Point);
            curve.AddCubicBezier(ControlPointTopRight.AnchorBGlobal, ControlPointBottomRight.AnchorBGlobal, ControlPointBottomRight.Point);
            curve.AddCubicBezier(ControlPointBottomRight.AnchorAGlobal, ControlPointBottomLeft.AnchorAGlobal, ControlPointBottomLeft.Point);
            curve.AddCubicBezier(ControlPointBottomLeft.AnchorBGlobal, ControlPointTopLeft.AnchorBGlobal, ControlPointTopLeft.Point);
            return curve;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
            => ControlPointTopLeft.GetHashCode() | ControlPointTopRight.GetHashCode() | ControlPointBottomLeft.GetHashCode() | ControlPointBottomRight.GetHashCode();

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Envelope && Equals((Envelope)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="envelope">The envelope.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Envelope envelope)
            => ControlPointTopLeft == envelope.ControlPointTopLeft
                && ControlPointTopRight == envelope.ControlPointTopRight
                && ControlPointBottomLeft == envelope.ControlPointBottomLeft
                && ControlPointBottomRight == envelope.ControlPointBottomRight;

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Envelope"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Envelope"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Envelope"/> class based on the format string
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
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Envelope"/> class based on the format string
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
        private string ConvertToString(string format, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Envelope)}{{{nameof(ControlPointTopLeft)}={ControlPointTopLeft.ToString(format, provider)}" +
                $"{sep}{nameof(ControlPointTopRight)}={ControlPointTopRight.ToString(format, provider)}" +
                $"{sep}{nameof(ControlPointBottomLeft)}={ControlPointBottomLeft.ToString(format, provider)}" +
                $"{sep}{nameof(ControlPointBottomRight)}={ControlPointBottomRight.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
