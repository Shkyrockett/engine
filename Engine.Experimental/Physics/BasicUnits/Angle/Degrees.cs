// <copyright file="Degrees.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// The degrees struct.
    /// </summary>
    /// <seealso cref="IDirection" />
    /// <seealso cref="IFormattable" />
    /// <seealso cref="IEquatable{T}" />
    [DataContract, Serializable]
    public struct Degrees
        : IDirection, IFormattable, IEquatable<Degrees>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees" /> struct.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        public Degrees(double degrees)
        {
            Value = degrees;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees" /> struct as a clone.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        public Degrees(Degrees degrees)
        {
            Value = degrees.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Degrees" /> struct from a <see cref="Radians" />.
        /// </summary>
        /// <param name="radians">The radians.</param>
        public Degrees(Radians radians)
        {
            Value = radians.Value.RadiansToDegrees();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the radians.
        /// </summary>
        /// <value>
        /// The radians.
        /// </value>
        public double Radians
        {
            get { return Radian * Value; }
            set { Value = value / Radian; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Degrees);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "deg";
        #endregion Properties

        #region Operators
        /// <summary>
        /// Compares two <see cref="Degrees" /> objects.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Degrees left, Degrees right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Degrees" /> objects.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Degrees left, Degrees right)
            => !Equals(left, right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Degrees"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        public static implicit operator Degrees(double value)
            => new Degrees(value);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Radians"/> to <see cref="Degrees"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
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
        /// Compares two <see cref="Degrees" /> objects.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Degrees a, Degrees b) => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Degrees a, Degrees b) => (a.Value == b.Value) & (a.Value == b.Value);

        /// <summary>
        /// override object.Equals
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        //
        // See the full list of guidelines at
        //   https://docs.microsoft.com/en-us/previous-versions/ms173147(v=vs.90)
        // and also the guidance for operator== at
        //   https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators#equality-operator-
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Degrees d ? Equals(d) : obj is Radians r && Equals(this, r.ToDegrees());

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Degrees value) => Equals(this, value);

        /// <summary>
        /// override object.GetHashCode
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <returns></returns>
        public Radians ToRadian() => Value.DegreesToRadians();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Degrees" /> struct.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider) => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(string format, IFormatProvider provider) => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Degrees" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
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
