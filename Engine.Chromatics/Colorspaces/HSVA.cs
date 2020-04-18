// <copyright file="AHSV.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static System.Math;

namespace Engine.Colorspace
{
    /// <summary>
    /// Alpha Hue Saturation Value color.
    /// </summary>
    /// <seealso cref="IColor" />
    /// <seealso cref="IEquatable{T}" />
    [DebuggerDisplay("{ToString()}")]
    public struct HSVA
        : IColor, IEquatable<HSVA>
    {
        /// <summary>
        /// The empty Value: new AHSV().
        /// </summary>
        public static readonly HSVA Empty = new HSVA();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HSVA" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <remarks>
        /// <para>h = [0,360], s = [0,1], v = [0,1]
        /// if s == 0, then h = -1 (undefined)</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        public HSVA(RGBA color)
        {
            var red = 1.0 - (color.Red / 255.0);
            var green = 1.0 - (color.Green / 255.0);
            var blue = 1.0 - (color.Blue / 255.0);

            var min = Min(red, green);
            min = Min(min, blue);
            var max = Max(red, green);
            max = Max(max, blue);
            double h;
            double s;
            var v = max;               // v
            var delta = max - min;
            if (max != 0)
            {
                s = delta / max;       // s
            }
            else
            {
                // r = g = b = 0		// s = 0, v is undefined
                s = 0;
                h = -1;

                Alpha = color.Alpha;
                Hue = h;
                Saturation = s;
                Value = v;
            }

            if (red == max)
            {
                h = (green - blue) / delta;       // between yellow & magenta
            }
            else
            {
                h = green == max ? 2 + ((blue - red) / delta) : 4 + ((red - green) / delta);   // between magenta & cyan
            }

            h *= 60;               // degrees
            if (h < 0)
            {
                h += 360;
            }

            Alpha = color.Alpha;
            Hue = h;
            Saturation = s;
            Value = v;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSVA" /> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        public HSVA(double hue, double saturation, double value)
            : this(hue, saturation, value, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSVA" /> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public HSVA(double hue, double saturation, double value, byte alpha)
        {
            Alpha = alpha;
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public double Alpha { get; set; }

        /// <summary>
        /// Gets or sets the hue color value.
        /// </summary>
        /// <value>
        /// The hue.
        /// </value>
        public double Hue { get; set; }

        /// <summary>
        /// Gets or sets the saturation color value.
        /// </summary>
        /// <value>
        /// The saturation.
        /// </value>
        public double Saturation { get; set; }

        /// <summary>
        /// Gets or sets the value color value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
        #endregion Properties

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(HSVA left, HSVA right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(HSVA left, HSVA right) => !(left == right);

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
            var (r1, g1, b1, a1) = (other?.ToRGBATuple()).Value;
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is HSVA color && Equals(color);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] HSVA other) => Alpha == other.Alpha && Hue == other.Hue && Saturation == other.Saturation && Value == other.Value;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Alpha, Hue, Saturation, Value);

        /// <summary>
        /// The to color.
        /// </summary>
        /// <returns>
        /// The <see cref="RGBA" />.
        /// </returns>
        public RGBA ToColor()
            => new RGBA(ToRGBATuple());

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </returns>
        /// <remarks>
        /// <para>h = [0,360], s = [0,1], v = [0,1]
        /// if s == 0, then h = -1 (undefined)</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.HSVAColorToRGBAColor(Hue, Saturation, Value, Alpha);

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
