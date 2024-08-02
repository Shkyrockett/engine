// <copyright file="Radians.cs" company="Shkyrockett" >
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
/// The radians struct.
/// </summary>
/// <seealso cref="IDirection{T}" />
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
[DataContract, Serializable]
public struct Radians<T>
    : IDirection<T>, IFormattable, IEquatable<Radians<T>>
    where T : INumber<T>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Radians" /> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    public Radians(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Radians" /> struct as a clone.
    /// </summary>
    /// <param name="radians">The radians.</param>
    public Radians(Radians<T> radians)
    {
        Value = radians.Value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Radians" /> struct from a <see cref="Degrees" />.
    /// </summary>
    /// <param name="degrees">The degrees.</param>
    public Radians(Degrees<T> degrees)
    {
        Value = T.CreateSaturating(double.CreateSaturating(degrees.Value).DegreesToRadians());
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
    /// Gets or sets the degrees.
    /// </summary>
    /// <value>
    /// The degrees.
    /// </value>
    public T Degrees
    {
        readonly get { return T.CreateSaturating(Floats<double>.Degree) * Value; }
        set { Value = value / T.CreateSaturating(Floats<double>.Degree); }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name
        => nameof(Radians<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
        => "rad";
    #endregion Properties

    #region Operators
    /// <summary>
    /// Compares two <see cref="Radians" /> objects.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Radians<T> left, Radians<T> right)
        => Equals(left, right);

    /// <summary>
    /// Compares two <see cref="Radians" /> objects.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Radians<T> left, Radians<T> right)
        => !Equals(left, right);

    /// <summary>
    /// Performs an implicit conversion from <see cref="double"/> to <see cref="Radians"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    public static implicit operator Radians<T>(T value)
        => new(value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Degrees"/> to <see cref="Radians"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    public static explicit operator Radians<T>(Degrees<T> value)
        => value.Radians;
    #endregion Operators

    #region Factories
    /// <summary>
    /// Parse a string for a <see cref="Radians" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Radians" /> data</param>
    /// <returns>
    /// Returns an instance of the <see cref="Radians" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    public static Radians<T> Parse(string source)
    {
        var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);

        var firstToken = tokenizer.NextTokenRequired();

        var value = new Radians<T>(T.CreateSaturating(Convert.ToDouble(firstToken, CultureInfo.InvariantCulture)));

        // There should be no more tokens in this string.
        tokenizer.LastTokenRequired();

        return value;
    }
    #endregion Factories

    #region Methods
    /// <summary>
    /// Compares two <see cref="Radians" /> objects.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(Radians<T> a, Radians<T> b) => Equals(a, b);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(Radians<T> a, Radians<T> b) => (a.Value == b.Value) & (a.Value == b.Value);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly bool Equals(object? obj) => obj is Radians<T> r ? Equals(r) : obj is Degrees<T> d && Equals(d.ToRadian());

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals(Radians<T> value) => Equals(this, value);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Convert Radians to Degrees.
    /// </summary>
    /// <returns></returns>
    public readonly Radians<T> ToDegrees() => new(T.CreateSaturating(double.CreateSaturating(Value).RadiansToDegrees()));

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Radians" /> struct.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Radians" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    public readonly string ToString(IFormatProvider provider) => ConvertToString(string.Empty /* format string */, provider);

    /// <summary>
    /// Creates a string representation of this <see cref="Radians" /> struct based on the format string
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
    /// Creates a string representation of this <see cref="Radians" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    internal readonly string ConvertToString(string? format, IFormatProvider? provider) => this == null ? nameof(Radians<T>) : $"{Value.ToString(format, provider)} rad";
    #endregion Methods
}
