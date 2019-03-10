﻿// <copyright file="RectangleExtensions.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;

namespace Engine
{
    /// <summary>
    /// Converts instances of other types to and from instances of <see cref="RectangleF" />.
    /// </summary>
    public class RectangleFConverter
        : ExpandableObjectConverter
    {
        /// <summary>
        /// Determines whether an object can be converted from a given type to an instance of <see cref="RectangleF" />.
        /// </summary>
        /// <returns>true if the type can be converted to a <see cref="RectangleF" />; otherwise, false.</returns>
        /// <param name="context">
        /// Provides contextual information required for conversion.
        /// This object can be used to get additional information about the environment this converter is being called from. 
        /// This may be null, so you should always check. 
        /// Also, properties on the context object may also return null. 
        /// </param>
        /// <param name="sourceType">The type of the source that is being evaluated for conversion.</param>
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        /// <summary>
        /// Determines whether a <see cref="RectangleF" /> can be converted to the specified type. 
        /// </summary>
        /// <returns>true if a <see cref="RectangleF" /> can be converted to <paramref name="destinationType" />; otherwise, false.</returns>
        /// <param name="context">An <see cref="ITypeDescriptorContext" /> provides contextual information required for conversion.
        /// This can be null, so you should always check. 
        /// Also, properties on the context object can also return null.
        /// </param>
        /// <param name="destinationType">The desired type this <see cref="RectangleF" /> is being evaluated for conversion.</param>
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        /// <summary>Attempts to convert the specified object to a <see cref="T:System.Windows.Rect" />. </summary>
        /// <returns>The <see cref="T:System.Windows.Rect" /> created from converting <paramref name="value" />.</returns>
        /// <param name="context">Provides contextual information required for conversion.</param>
        /// <param name="culture">Cultural information which is respected when converting.</param>
        /// <param name="value">The object being converted.</param>
        /// <exception cref="T:System.NotSupportedException">Thrown if the specified object is NULL or is a type that cannot be converted to a <see cref="T:System.Windows.Rect" />.</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (str is null)
            {
                return base.ConvertFrom(context, culture, value);
            }

            var str2 = str.Trim();
            if (str2.Length == 0)
            {
                return null;
            }

            if (culture is null)
            {
                culture = CultureInfo.CurrentCulture;
            }

            var strArray = str2.Split(new char[] { culture.TextInfo.ListSeparator[0] });
            var numArray = new float[strArray.Length];
            var converter = TypeDescriptor.GetConverter(typeof(float));
            for (var i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (float)converter.ConvertFromString(context, culture, strArray[i].Trim());
            }

            if (numArray.Length != 4)
            {
                throw new ArgumentException("Parse failed.");
            }

            return new RectangleF(numArray[0], numArray[1], numArray[2], numArray[3]);
        }

        /// <summary> Attempts to convert a <see cref="T:System.Windows.Rect" /> to the specified type. </summary>
        /// <returns>The object created from converting this <see cref="T:System.Windows.Rect" />.</returns>
        /// <param name="context">Provides contextual information required for conversion.</param>
        /// <param name="culture">Cultural information which is respected during conversion.</param>
        /// <param name="value">The <see cref="T:System.Windows.Rect" /> to convert.</param>
        /// <param name="destinationType">The type to convert this <see cref="T:System.Windows.Rect" /> to.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///   <paramref name="value" /> is null.- or - <paramref name="value" /> is not a <see cref="T:System.Windows.Rect" />.- or - The <paramref name="destinationType" /> is not one of the valid types for conversion.</exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (value is RectangleF)
            {
                if (destinationType == typeof(string))
                {
                    var rectangleF = (RectangleF)value;
                    if (culture is null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    var separator = culture.TextInfo.ListSeparator + " ";
                    var converter = TypeDescriptor.GetConverter(typeof(float));
                    var strArray = new string[4];
                    var num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, rectangleF.X);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangleF.Y);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangleF.Width);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangleF.Height);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var rectangle2 = (RectangleF)value;
                    var constructor = typeof(RectangleF).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float), typeof(float) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
