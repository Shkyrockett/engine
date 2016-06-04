﻿using Engine.Geometry;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DisplayName(nameof(Degrees))]
    public struct Degrees
        : IDirection, IFormattable
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Radien = Math.PI / 180d;

        /// <summary>
        /// 
        /// </summary>
        public const double Degree = 180d / Math.PI;

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct.
        /// </summary>
        /// <param name="degrees"></param>
        public Degrees(double degrees)
        {
            value = degrees;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct as a clone.
        /// </summary>
        /// <param name="degrees"></param>
        public Degrees(Degrees degrees)
        {
            value = degrees.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct from a <see cref="Radians"/>.
        /// </summary>
        /// <param name="radians"></param>
        public Degrees(Radians radians)
        {
            value = radians.Value.ToDegrees();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Radiens
        {
            get { return Radien * value; }
            set { this.value = value / Radien; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Degrees"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "deg"; } }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="Degrees"/> objects. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Degrees left, Degrees right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Compares two <see cref="Degrees"/> objects. 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Degrees left, Degrees right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares two <see cref="Degrees"/> objects.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Degrees a, Degrees b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Degrees a, Degrees b)
        {
            return (a.Value == b.Value) & (a.Value == b.Value);
        }

        /// <summary>
        /// override object.Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   https://msdn.microsoft.com/en-us/library/ms173147.aspx 
            // and also the guidance for operator== at
            //   https://msdn.microsoft.com/en-us/library/53k8ybth.aspx
            //
            return (obj is Degrees || obj is Radians) && obj is Degrees ? Equals(this, (Degrees)obj) : Equals(this, ((Radians)obj).ToDegrees());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Degrees value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Degrees(double value)
        {
            return new Degrees(value);
        }

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="Degrees"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Degrees"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Degrees"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [Pure]
        public static Degrees Parse(string source)
        {
            Tokenizer tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            Degrees value = new Degrees(Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// override object.GetHashCode
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public Radians ToRadian()
        {
            return value.ToRadians();
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Degrees"/> struct.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Degrees);
            //return string.Format(provider, "{0:" + format + "}°", value);
            IFormattable formatable = $"{value}°";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}