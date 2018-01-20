// <copyright file="RGBA.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
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

namespace Engine.Colorspace
{
    /// <summary>
    /// Red Green Blue Alpha color class.
    /// </summary>
    public struct RGBA
        : IColor
    {
        #region Implementations

        /// <summary>
        /// The empty Value: new RGBA(0, 0, 0, 0).
        /// </summary>
        public static readonly RGBA Empty = new RGBA(0, 0, 0, 0);

        #endregion

        #region Constants

        /// <summary>
        /// The red shift (const). Value: 0x10.
        /// </summary>
        private const int RedShift = 0x10;

        /// <summary>
        /// The green shift (const). Value: 0x8.
        /// </summary>
        private const int GreenShift = 0x8;

        /// <summary>
        /// The blue shift (const). Value: 0x0.
        /// </summary>
        private const int BlueShift = 0x0;

        /// <summary>
        /// The alpha shift (const). Value: 0x18.
        /// </summary>
        private const int AlphaShift = 0x18;

        #endregion

        #region Fields

        /// <summary>
        /// The integer value of the color.
        /// </summary>
        private int value;

        /// <summary>
        /// The name of the color.
        /// </summary>
        private string name;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        /// <param name="name">The name of the color.</param>
        public RGBA(int value, string name = "")
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> class.
        /// </summary>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="name">The name of the color.</param>
        public RGBA(byte red, byte green, byte blue, string name = "")
            : this(red, green, blue, 0, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> class.
        /// </summary>
        /// <param name="tuple"><see cref="ValueTuple{T1, T2, T3, T4}"/> representing the Red, Green, Blue, and Alpha components in an RGBA color.</param>
        /// <param name="name">The name of the color.</param>
        public RGBA((byte red, byte green, byte blue, byte alpha) tuple, string name = "")
            : this(tuple.red, tuple.green, tuple.blue, tuple.alpha, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> class.
        /// </summary>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="name">The name of the color.</param>
        public RGBA(byte red, byte green, byte blue, byte alpha, string name = "")
        {
            this.name = name;
            value =
                (red << RedShift
                | green << GreenShift
                | blue << BlueShift
                | alpha << AlphaShift);// & 0xffffffff;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the red color value.
        /// </summary>
        public byte Red
        {
            get { return (byte)((Value >> RedShift) & 0xFF); }
            set { this.value |= value << RedShift; }
        }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Green
        {
            get { return (byte)((Value >> GreenShift) & 0xFF); }
            set { this.value |= value << GreenShift; }
        }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Blue
        {
            get { return (byte)((Value >> BlueShift) & 0xFF); }
            set { this.value |= value << BlueShift; }
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha
        {
            get { return (byte)((Value >> AlphaShift) & 0xFF); }
            set { this.value |= value << AlphaShift; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="RGBA"/> objects.
        /// The result specifies whether the color values of the two <see cref="RGBA"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGBA left, RGBA right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="RGBA"/> objects.
        /// The result specifies whether the color values of the two <see cref="RGBA"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGBA left, RGBA right)
            => !Equals(left, right);

        #endregion

        #region Methods

        /// <summary>
        /// Get the brightness.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public double GetBrightness()
            => Colorspaces.GetLuminance(Red, Green, Blue);

        /// <summary>
        /// Get the hue.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public double GetHue()
            => Colorspaces.GetHue(Red, Green, Blue);

        /// <summary>
        /// Get the saturation.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        public double GetSaturation()
            => Colorspaces.GetSaturation(Red, Green, Blue);

        #endregion

        #region Standard Methods

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => value.GetHashCode();

        /// <summary>
        /// Compares two <see cref="RGBA"/> colors
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(RGBA a, RGBA b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(RGBA a, RGBA b)
            => (a.value == b.value);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is RGBA && Equals(this, (RGBA)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IColor value)
            => Equals(this, value);

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => (Red, Green, Blue,Alpha);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="RGBA"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="RGBA"/> struct based on the IFormatProvider
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
        /// Creates a string representation of this <see cref="RGBA"/> class based on the format string
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
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="RGBA"/> struct based on the format string
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
            return $"{nameof(RGBA)}{{{nameof(Red)}={Red.ToString(format, provider)}{sep}{nameof(Green)}={Green.ToString(format, provider)}{sep}{nameof(Blue)}={Blue.ToString(format, provider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, provider)}}}";
        }

        #endregion
    }
}
