// <copyright file="YIQA.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;

namespace Engine.Colorspace
{
    /// <summary>
    /// The YIQA struct.
    /// </summary>
    /// <seealso cref="IColor" />
    /// <seealso cref="IEquatable{T}" />
    [DebuggerDisplay("{ToString()}")]
    public struct YIQA
        : IColor, IEquatable<YIQA>
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new YIQ().
        /// </summary>
        public static readonly YIQA Empty = new YIQA();
        #endregion Implementations

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public YIQA(RGBA color)
        {
            Alpha = color.Alpha;
            Y = (0.299900d * color.Red) + (0.587000d * color.Green) + (0.114000d * color.Blue);
            I = (0.595716d * color.Red) - (0.274453d * color.Green) - (0.321264d * color.Blue);
            Q = (0.211456d * color.Red) - (0.522591d * color.Green) + (0.311350d * color.Blue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA" /> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQA(double y, double i, double q)
            : this(0, y, i, q)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA" /> class.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQA(byte alpha, double y, double i, double q)
        {
            Alpha = alpha;
            Y = y;
            I = i;
            Q = q;
        }

        /// <summary>
        /// Gets or sets the alpha.
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public byte Alpha { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the I.
        /// </summary>
        /// <value>
        /// The i.
        /// </value>
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the q.
        /// </summary>
        /// <value>
        /// The q.
        /// </value>
        public double Q { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(YIQA left, YIQA right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(YIQA left, YIQA right) => !(left == right);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = other.ToRGBATuple();
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is YIQA color && Equals(color);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(YIQA other) => Alpha == other.Alpha && Y == other.Y && I == other.I && Q == other.Q;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 1361233287;
            hashCode = hashCode * -1521134295 + Alpha.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + I.GetHashCode();
            hashCode = hashCode * -1521134295 + Q.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// The to color.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <returns>
        /// The <see cref="RGBA" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public static RGBA ToColor(byte a, double y, double i, double q)
        {
            var r = (byte)(y + (0.9563d * i) + (0.6210d * q));
            var g = (byte)(y - (0.2721d * i) - (0.6474d * q));
            var b = (byte)(y - (1.1070d * i) + (1.7046d * q));
            return new RGBA(r, g, b, a);
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.RGBAFColorToRGBAColor(Colorspaces.YIQAColorToRGBAFColor(Y, I, Q, Alpha));

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ToString(string format, IFormatProvider formatProvider)
            => throw new NotImplementedException();
    }
}
