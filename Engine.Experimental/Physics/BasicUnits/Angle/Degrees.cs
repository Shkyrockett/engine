// <copyright file="Degrees.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;

namespace Engine.Physics
{
    /// <summary>
    /// The degrees struct.
    /// </summary>
    [DataContract, Serializable]
    [DisplayName(nameof(Degrees))]
    public struct Degrees
        : IDirection, IFormattable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct.
        /// </summary>
        /// <param name="degrees"></param>
        public Degrees(double degrees)
        {
            Value = degrees;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct as a clone.
        /// </summary>
        /// <param name="degrees"></param>
        public Degrees(Degrees degrees)
        {
            Value = degrees.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees"/> struct from a <see cref="Radians"/>.
        /// </summary>
        /// <param name="radians"></param>
        public Degrees(Radians radians)
        {
            Value = radians.Value.ToDegrees();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the radians.
        /// </summary>
        public double Radians
        {
            get { return Radian * Value; }
            set { Value = value / Radian; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Degrees);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "deg";
        #endregion Properties

        #region Operators
        /// <summary>
        /// Compares two <see cref="Degrees"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Degrees left, Degrees right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Degrees"/> objects.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Degrees left, Degrees right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Degrees"/> objects.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Degrees a, Degrees b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Degrees a, Degrees b)
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
            => (obj is Degrees || obj is Radians) && obj is Degrees ? Equals(this, (Degrees)obj) : Equals(this, ((Radians)obj).ToDegrees());

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Degrees value)
            => Equals(this, value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static implicit operator Degrees(double value)
            => new Degrees(value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        public static explicit operator Degrees(Radians value)
            => value.Degrees;
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Degrees"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Degrees"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Degrees"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Degrees Parse(string source)
        {
            var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            var value = new Degrees(Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// override object.GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <returns></returns>
        public Radians ToRadian() => Value.ToRadians();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Degrees"/> struct.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
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
        public string ToString(string format, IFormatProvider provider)
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
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            //if (this is null) return nameof(Degrees);
            //return string.Format(provider, "{0:" + format + "}°", value);
            IFormattable formatable = $"{Value}°";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
