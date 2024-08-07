﻿// <copyright file="LineDashStyle.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// The line dash style struct.
/// </summary>
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
[DataContract, Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public struct LineDashStyle
    : IFormattable, IEquatable<LineDashStyle>
{
    /// <summary>
    /// The solid (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1 }).
    /// </summary>
    public static readonly LineDashStyle Solid = new(/*DashStyle.Solid,*/ [1]);

    /// <summary>
    /// The dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 1, 1 }).
    /// </summary>
    public static readonly LineDashStyle Dot = new(/*DashStyle.Solid,*/ [1, 1]);

    /// <summary>
    /// The dash (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1 }).
    /// </summary>
    public static readonly LineDashStyle Dash = new(/*DashStyle.Solid,*/ [3, 1]);

    /// <summary>
    /// The dash dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1 }).
    /// </summary>
    public static readonly LineDashStyle DashDot = new(/*DashStyle.Solid,*/ [3, 1, 1, 1]);

    /// <summary>
    /// The dash dot dot (readonly). Value: new LineDashStyle(/*DashStyle.Solid,*/ new float[] { 3, 1, 1, 1, 1, 1 }).
    /// </summary>
    public static readonly LineDashStyle DashDotDot = new(/*DashStyle.Solid,*/ [3, 1, 1, 1, 1, 1]);

    ///// <summary>
    /////
    ///// </summary>
    //[NonSerialized()]
    //private DashStyle dashStyle;

    /// <summary>
    /// The dash pattern.
    /// </summary>
    private readonly float[] dashPattern;

    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="dashPattern"></param>
    ///// <param name="dashOffset"></param>
    //public LineDashStyle(float[] dashPattern, float dashOffset = 0)
    //    : this(/*DashStyle.Custom,*/ dashPattern, dashOffset)
    //{ }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineDashStyle" /> class.
    /// </summary>
    /// <param name="dashPattern">The dashPattern.</param>
    /// <param name="dashOffset">The dashOffset.</param>
    internal LineDashStyle(/*DashStyle dashStyle,*/ float[] dashPattern, float dashOffset = 0)
        : this()
    {
        //this.dashStyle = dashStyle;
        this.dashPattern = dashPattern;
        DashOffset = dashOffset;
    }

    ///// <summary>
    /////
    ///// </summary>
    //[IgnoreDataMember, XmlIgnore, SoapIgnore]
    //internal DashStyle DashStyle { get { return dashStyle; } set { dashStyle = value; } }

    ///// <summary>
    /////
    ///// </summary>
    //[IgnoreDataMember, XmlIgnore, SoapIgnore]
    //public float[] DashPattern
    //{
    //    get { return dashPattern; }
    //    set
    //    {
    //        dashPattern = value;
    //        if (dashPattern is null) dashStyle = DashStyle.Solid;
    //        else if (dashPattern == Solid.dashPattern) dashStyle = DashStyle.Solid;
    //        else if (dashPattern == Dot.dashPattern) dashStyle = DashStyle.Dot;
    //        else if (dashPattern == Dash.dashPattern) dashStyle = DashStyle.Dash;
    //        else if (dashPattern == DashDot.dashPattern) dashStyle = DashStyle.DashDot;
    //        else if (dashPattern == DashDotDot.dashPattern) dashStyle = DashStyle.DashDotDot;
    //        else dashStyle = DashStyle.Custom;
    //    }
    //}

    /// <summary>
    /// Gets or sets the dash pattern text.
    /// </summary>
    /// <value>
    /// The dash pattern text.
    /// </value>
    [Browsable(false)]
    [XmlAttribute("d")]
    [RefreshProperties(RefreshProperties.All)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DashPatternText { get { return ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */); } readonly set { Parse(value); } }

    /// <summary>
    /// Gets or sets the dash offset.
    /// </summary>
    /// <value>
    /// The dash offset.
    /// </value>
    public float DashOffset { get; set; }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(LineDashStyle left, LineDashStyle right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(LineDashStyle left, LineDashStyle right) => !(left == right);

    /// <summary>
    /// Parse.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    private static float[] Parse(string text)
    {
        const string argSeparators = @"[\s,]|(?=-)";
        return Regex.Split(text, argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => float.Parse(arg, NumberStyles.Float, CultureInfo.InvariantCulture)).ToArray();
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => obj is LineDashStyle style && Equals(style);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] LineDashStyle other) => EqualityComparer<float[]>.Default.Equals(dashPattern, other.dashPattern) && DashOffset == other.DashOffset;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(dashPattern, DashOffset);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="LineDashStyle" /> struct.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="LineDashStyle" /> struct based on the IFormatProvider
    /// passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ConvertToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="LineDashStyle" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider) => ConvertToString(format /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="LineDashStyle" /> inherited class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this LineStyle object.
    /// </returns>
    public string ConvertToString(string format, IFormatProvider formatProvider)
    {
        //if (this is null)
        //    return nameof(GraphicsObject);
        var output = new StringBuilder();
        foreach (var item in dashPattern)
        {
            output.Append($"{item.ToString(format, formatProvider)},");
        }

        output.Replace(",-", "-");
        return output.ToString().Trim(',');
    }
}
