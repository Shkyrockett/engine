// <copyright file="Radians.cs" company="Shkyrockett" >
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
    /// The radians struct.
    /// </summary>
    [DataContract, Serializable]
    [DisplayName(nameof(Radians))]
    public struct Radians
        : IDirection, IFormattable
    {
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
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the degrees.
        /// </summary>
        public double Degrees
        {
            get { return Degree * Value; }
            set { Value = value / Degree; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Radians);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "rad";
        #endregion Properties

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Radians a, Radians b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
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
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Radians value)
            => Equals(this, value);

        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static implicit operator Radians(double value)
            => new Radians(value);

        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static explicit operator Radians(Degrees value)
            => value.Radians;
        #endregion Operators

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
        #endregion Factories

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
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Radians"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

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
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

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
            //if (this is null) return nameof(Radians);
            //return string.Format(provider, "{0:" + format + "} rad", value);
            IFormattable formatable = $"{Value} rad";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
