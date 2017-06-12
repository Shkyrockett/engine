// <copyright file="Radians.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Engine.Maths;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    //[DataContract, Serializable]
    //[DisplayName(nameof(Radians))]
    public struct Radians
        : IDirection, IFormattable
    {
        #region Constants

        ///// <summary>
        /////
        ///// </summary>
        //public const double Radien = Maths.Radien;

        ///// <summary>
        /////
        ///// </summary>
        //public const double Degree = Maths.Radien;

        #endregion

        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct.
        /// </summary>
        /// <param name="value"></param>
        public Radians(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct as a clone.
        /// </summary>
        /// <param name="radians"></param>
        public Radians(Radians radians)
        {
            Value = radians.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radians"/> struct from a <see cref="Degrees"/>.
        /// </summary>
        /// <param name="degrees"></param>
        public Radians(Degrees degrees)
        {
            Value = degrees.Value.ToRadians();
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Degrees
        {
            get { return Degree * Value; }
            set { Value = value / Degree; }
        }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Radians);

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "rad";

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="Radians"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Radians left, Radians right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Radians"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Radians left, Radians right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Radians"/> objects.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Radians a, Radians b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Radians a, Radians b)
            => (a.Value == b.Value) & (a.Value == b.Value);

        /// <summary>
        /// override object.Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //
        // See the full list of guidelines at
        //   https://msdn.microsoft.com/en-us/library/ms173147.aspx
        // and also the guidance for operator== at
        //   https://msdn.microsoft.com/en-us/library/53k8ybth.aspx
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => (obj is Radians || obj is Degrees) && obj is Radians ? Equals(this, (Radians)obj) : Equals(this, ((Degrees)obj).ToRadian());

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Radians value)
            => Equals(this, value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Radians(double value)
            => new Radians(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static explicit operator Radians(Degrees value)
            => value.Radians;

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="Radians"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Radians"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Radians"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Radians Parse(string source)
        {
            var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);

            var firstToken = tokenizer.NextTokenRequired();

            var value = new Radians(Convert.ToDouble(firstToken, CultureInfo.InvariantCulture));

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
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <returns></returns>
        public Radians ToDegrees()
            => Value.ToDegrees();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Radians"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Radians);
            //return string.Format(provider, "{0:" + format + "} rad", value);
            IFormattable formatable = $"{Value} rad";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
