// <copyright file="ARGB.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
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
    /// Alpha Red Green Blue color class.
    /// </summary>
    public struct ARGB
        : IColor
    {
        #region Implementations

        /// <summary>
        ///
        /// </summary>
        public static readonly ARGB Empty = new ARGB(0, 0, 0, 0);

        #endregion

        #region Constants

        private const int AlphaShift = 0x18;
        private const int RedShift = 0x10;
        private const int GreenShift = 0x8;
        private const int BlueShift = 0x0;

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
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(int value, string name = "")
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(byte red, byte green, byte blue, string name = "")
            : this(0, red, green, blue, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="tuple"><see cref="ValueTuple"/> representing the Alpha, Red, Green, and Blue components in an ARGB color.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB((byte A, byte R, byte G, byte B) tuple, string name = "")
            : this(tuple.A, tuple.R, tuple.G, tuple.B, name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ARGB"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="red">Red color component.</param>
        /// <param name="green">Green color component.</param>
        /// <param name="blue">Blue color component.</param>
        /// <param name="name">The name of the color.</param>
        public ARGB(byte alpha, byte red, byte green, byte blue, string name = "")
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
        ///
        /// </summary>
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="ARGB"/> objects.
        /// The result specifies whether the color values of the two <see cref="ARGB"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ARGB left, ARGB right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="ARGB"/> objects.
        /// The result specifies whether the color values of the two <see cref="ARGB"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ARGB left, ARGB right)
            => !Equals(left, right);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs
        /// </remarks>
        public float GetBrightness()
        {
            var r = Red / 255.0f;
            var g = Green / 255.0f;
            var b = Blue / 255.0f;

            float max, min;

            max = r; min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            return (max + min) / 2f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs
        /// </remarks>
        public float GetHue()
        {
            if (Red == Green && Green == Blue)
                return 0; // 0 makes as good an UNDEFINED value as any

            var r = Red / 255.0f;
            var g = Green / 255.0f;
            var b = Blue / 255.0f;

            var hue = 0.0f;

            var max = r;
            var min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            var delta = max - min;

            if (r == max)
            {
                hue = (g - b) / delta;
            }
            else if (g == max)
            {
                hue = 2 + (b - r) / delta;
            }
            else if (b == max)
            {
                hue = 4 + (r - g) / delta;
            }
            hue *= 60;

            if (hue < 0.0f)
            {
                hue += 360.0f;
            }
            return hue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs
        /// </remarks>
        public float GetSaturation()
        {
            var r = Red / 255.0f;
            var g = Green / 255.0f;
            var b = Blue / 255.0f;

            var l = 0f;
            var s = 0f;

            var max = r;
            var min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            // if max == min, then there is no color and
            // the saturation is zero.
            if (max != min)
            {
                l = (max + min) / 2;

                if (l <= .5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = (max - min) / (2 - max - min);
                }
            }
            return s;
        }

        #endregion

        #region Standard Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => value.GetHashCode();

        /// <summary>
        /// Compares two <see cref="ARGB"/> colors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(ARGB a, ARGB b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(ARGB a, ARGB b)
            => (a.value == b.value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is ARGB && Equals(this, (ARGB)obj);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IColor value)
            => Equals(this, value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple()
            => (Alpha, Red, Green, Blue);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="ARGB"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="ARGB"/> struct based on the IFormatProvider
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
        /// Creates a string representation of this <see cref="ARGB"/> class based on the format string
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
        /// Creates a string representation of this <see cref="ARGB"/> struct based on the format string
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
            if (this == null) return nameof(ARGB);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(ARGB)}{{{nameof(Alpha)}={Alpha.ToString(format, provider)}{sep}{nameof(Red)}={Red.ToString(format, provider)}{sep}{nameof(Green)}={Green.ToString(format, provider)}{sep}{nameof(Blue)}={Blue.ToString(format, provider)}}}";
        }

        #endregion
    }
}
