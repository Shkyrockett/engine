﻿// <copyright file="Matrix2DConverter.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Globalization;

namespace Engine;

/// <summary>
/// The matrix2d converter class.
/// </summary>
/// <remarks>
/// <para>http://referencesource.microsoft.com</para>
/// </remarks>
public sealed class Matrix2DConverter
    : TypeConverter
{
    /// <summary>
    /// Returns true if this type converter can convert from a given type.
    /// </summary>
    /// <returns>
    /// bool - True if this converter can convert from the provided type, false if not.
    /// </returns>
    /// <param name="context"> The ITypeDescriptorContext for this call. </param>
    /// <param name="sourceType"> The Type being queried for support. </param>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// Returns true if this type converter can convert to the given type.
    /// </summary>
    /// <returns>
    /// bool - True if this converter can convert to the provided type, false if not.
    /// </returns>
    /// <param name="context"> The ITypeDescriptorContext for this call. </param>
    /// <param name="destinationType"> The Type being queried for support. </param>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return true;
        }

        return base.CanConvertTo(context, destinationType);
    }

    /// <summary>
    /// Attempts to convert to a Matrix from the given object.
    /// </summary>
    /// <returns>
    /// The Matrix which was constructed.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// A NotSupportedException is thrown if the example object is null or is not a valid type
    /// which can be converted to a Matrix.
    /// </exception>
    /// <param name="context"> The ITypeDescriptorContext for this call. </param>
    /// <param name="culture"> The requested CultureInfo.  Note that conversion uses "en-US" rather than this parameter. </param>
    /// <param name="value"> The object to convert to an instance of Matrix. </param>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is null)
        {
            throw GetConvertFromException(value);
        }

        if (value is string source)
        {
            return Matrix3x2D.Parse(source);
        }

        return base.ConvertFrom(context, culture, value);
    }

    /// <summary>
    /// ConvertTo - Attempt to convert an instance of Matrix to the given type
    /// </summary>
    /// <returns>
    /// The object which was constructed.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// A NotSupportedException is thrown if "value" is null or not an instance of Matrix,
    /// or if the destinationType isn't one of the valid destination types.
    /// </exception>
    /// <param name="context"> The ITypeDescriptorContext for this call. </param>
    /// <param name="culture"> The CultureInfo which is respected when converting. </param>
    /// <param name="value"> The object to convert to an instance of "destinationType". </param>
    /// <param name="destinationType"> The type to which this will convert the Matrix instance. </param>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType is not null && value is Matrix3x2D d)
        {
            var instance = d;

            if (destinationType == typeof(string))
            {
                // Delegate to the formatting/culture-aware ConvertToString method.
                return instance.ToString(string.Empty, culture);
            }
        }

        // Pass unhandled cases to base class (which will throw exceptions for null value or destinationType.)
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
