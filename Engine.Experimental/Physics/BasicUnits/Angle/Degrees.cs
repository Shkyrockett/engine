// <copyright file="Degrees.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine;

/// <summary>
/// The degrees struct.
/// </summary>
/// <seealso cref="IDirection{T}" />
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
[DataContract, Serializable]
public struct Degrees<T>
    : IDirection<T>, IFormattable, IEquatable<Degrees<T>>
    where T : INumber<T>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees{T}" /> struct.
    /// </summary>
    /// <param name="degrees">The degrees.</param>
    public Degrees(T degrees)
    {
        Value = degrees;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees{T}" /> struct as a clone.
    /// </summary>
    /// <param name="degrees">The degrees.</param>
    public Degrees(Degrees<T> degrees)
    {
        Value = degrees.Value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Degrees{T}" /> struct from a <see cref="Radians" />.
    /// </summary>
    /// <param name="radians">The radians.</param>
    public Degrees(Radians<T> radians)
    {
        Value = T.CreateSaturating(double.CreateSaturating(radians.Value).RadiansToDegrees());
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public T Value { get; set; }

    /// <summary>
    /// Gets or sets the radians.
    /// </summary>
    /// <value>
    /// The radians.
    /// </value>
    public T Radians
    {
        readonly get { return T.CreateSaturating(Floats<double>.Radian) * Value; }
        set { Value = value / T.CreateSaturating(Floats<double>.Radian); }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name
        => nameof(Degrees<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
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
    public static bool operator ==(Degrees<T> left, Degrees<T> right)
        => Equals(left, right);

    /// <summary>
    /// Compares two <see cref="Degrees" /> objects.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Degrees<T> left, Degrees<T> right)
        => !Equals(left, right);

    /// <summary>
    /// Performs an implicit conversion from <see cref="double"/> to <see cref="Degrees"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    public static implicit operator Degrees<T>(T value)
        => new(value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Radians"/> to <see cref="Degrees"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    public static explicit operator Degrees<T>(Radians<T> value)
        => new(value.Degrees);
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
    public static Degrees<T> Parse(string source)
    {
        var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
        var value = new Degrees<T>(T.CreateSaturating(Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture)));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(Degrees<T> a, Degrees<T> b) => Equals(a, b);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(Degrees<T> a, Degrees<T> b) => (a.Value == b.Value) & (a.Value == b.Value);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly bool Equals(object? obj) => obj is Degrees<T> d ? Equals(d) : obj is Radians<T> r && Equals(this, r.ToDegrees());

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals(Degrees<T> value) => Equals(this, value);

    /// <summary>
    /// override object.GetHashCode
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Convert Degrees to Radians.
    /// </summary>
    /// <returns></returns>
    public readonly Radians<T> ToRadian() => new(T.CreateSaturating(double.CreateSaturating(Value).DegreesToRadians()));

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Degrees" /> struct.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Degrees" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public readonly string ToString(IFormatProvider provider) => ConvertToString(string.Empty /* format string */, provider);

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
    public readonly string ToString(string? format, IFormatProvider? provider) => ConvertToString(format /* format string */, provider /* format provider */);

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
    internal readonly string ConvertToString(string? format, IFormatProvider? provider)
    {
        //if (this is null) return nameof(Degrees);
        //return string.Format(provider, "{0:" + format + "}°", value);
        IFormattable formatable = $"{Value}°";
        return formatable.ToString(format, provider);
    }
    #endregion Methods
}
