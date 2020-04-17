// <copyright file="HSIA.cs" company="Shkyrockett" >
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

namespace Engine.Colorspace
{
    /// <summary>
    ///   <see cref="HSIA" /> color structure.
    /// </summary>
    /// <seealso cref="IColor" />
    /// <seealso cref="IEquatable{T}" />
    [DebuggerDisplay("{ToString()}")]
    public struct HSIA
        : IColor, IEquatable<HSIA>
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new AHSI().
        /// </summary>
        public static readonly HSIA Empty = new HSIA();
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HSIA" /> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public HSIA(RGBA color)
        {
            (Hue, Saturation, Intensity, Alpha) = Colorspaces.RGBAColorToHSIAColor(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSIA" /> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        public HSIA(double hue, double saturation, double intensity)
            : this(hue, saturation, intensity, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSIA" /> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public HSIA(double hue, double saturation, double intensity, double alpha)
        {
            Hue = hue;
            Saturation = saturation;
            Intensity = intensity;
            Alpha = alpha;
        }
        #endregion Constructors

        #region Properties
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
        /// Gets or sets the intensity color value.
        /// </summary>
        /// <value>
        /// The intensity.
        /// </value>
        public double Intensity { get; set; }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public double Alpha { get; set; }
        #endregion Properties

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(HSIA left, HSIA right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(HSIA left, HSIA right) => !(left == right);

        #region Methods
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
        public override bool Equals(object obj) => obj is HSIA color && Equals(color);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(HSIA other) => Hue == other.Hue && Saturation == other.Saturation && Intensity == other.Intensity && Alpha == other.Alpha;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 230637231;
            hashCode = hashCode * -1521134295 + Hue.GetHashCode();
            hashCode = hashCode * -1521134295 + Saturation.GetHashCode();
            hashCode = hashCode * -1521134295 + Intensity.GetHashCode();
            hashCode = hashCode * -1521134295 + Alpha.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// The to color.
        /// </summary>
        /// <returns>
        /// The <see cref="RGBA" />.
        /// </returns>
        public RGBA ToColor()
            => new RGBA(ToRGBATuple());

        /// <summary>
        /// Converts the <see cref="HSIA" /> class to a <see cref="RGBA" /> class.
        /// </summary>
        /// <returns>
        /// <para>The <see cref="ValueTuple{T1, T2, T3, T4}" />.</para>
        /// </returns>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.HSIAColorToRGBAColor(Hue, Saturation, Intensity, Alpha);

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
        #endregion Methods
    }
}
