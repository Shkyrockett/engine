// <copyright file="Rectangle2DConverter.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
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
using System.Globalization;

namespace Engine
{
    /// <summary>
    /// Converts instances of other types to and from instances of <see cref="Rectangle2D" />.
    /// </summary>
    public class Rectangle2DConverter
        : ExpandableObjectConverter
    {
        /// <summary>
        /// Determines whether an object can be converted from a given type to an instance of <see cref="Rectangle2D" />.
        /// </summary>
        /// <returns>true if the type can be converted to a <see cref="Rectangle2D" />; otherwise, false.</returns>
        /// <param name="context">
        /// Provides contextual information required for conversion.
        /// This object can be used to get additional information about the environment this converter is being called from. 
        /// This may be null, so you should always check. 
        /// Also, properties on the context object may also return null. 
        /// </param>
        /// <param name="sourceType">The type of the source that is being evaluated for conversion.</param>
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        /// <summary>
        /// Determines whether a <see cref="Rectangle2D" /> can be converted to the specified type. 
        /// </summary>
        /// <returns>true if a <see cref="Rectangle2D" /> can be converted to <paramref name="destinationType" />; otherwise, false.</returns>
        /// <param name="context">An <see cref="ITypeDescriptorContext" /> provides contextual information required for conversion.
        /// This can be null, so you should always check. 
        /// Also, properties on the context object can also return null.
        /// </param>
        /// <param name="destinationType">The desired type this <see cref="Rectangle2D" /> is being evaluated for conversion.</param>
        /// <filterpriority>1</filterpriority>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        /// <summary>Attempts to convert the specified object to a <see cref="Rectangle2D" />. </summary>
        /// <returns>The <see cref="Rectangle2D" /> created from converting <paramref name="value" />.</returns>
        /// <param name="context">Provides contextual information required for conversion.</param>
        /// <param name="culture">Cultural information which is respected when converting.</param>
        /// <param name="value">The object being converted.</param>
        /// <exception cref="NotSupportedException">Thrown if the specified object is NULL or is a type that cannot be converted to a <see cref="Rectangle2D" />.</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string str))
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
            var numArray = new double[strArray.Length];
            var converter = TypeDescriptor.GetConverter(typeof(double));
            for (var i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (double)converter.ConvertFromString(context, culture, strArray[i].Trim());
            }

            if (numArray.Length != 4)
            {
                throw new ArgumentException("Parse failed.");
            }

            return new Rectangle2D(numArray[0], numArray[1], numArray[2], numArray[3]);
        }

        /// <summary> Attempts to convert a <see cref="Rectangle2D" /> to the specified type. </summary>
        /// <returns>The object created from converting this <see cref="Rectangle2D" />.</returns>
        /// <param name="context">Provides contextual information required for conversion.</param>
        /// <param name="culture">Cultural information which is respected during conversion.</param>
        /// <param name="value">The <see cref="Rectangle2D" /> to convert.</param>
        /// <param name="destinationType">The type to convert this <see cref="Rectangle2D" /> to.</param>
        /// <exception cref="NotSupportedException">
        ///   <paramref name="value" /> is null.- or - <paramref name="value" /> is not a <see cref="Rectangle2D" />.- or - The <paramref name="destinationType" /> is not one of the valid types for conversion.</exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (value is Rectangle2D rectangle2D)
            {
                if (destinationType == typeof(string))
                {
                    if (culture is null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    var separator = $"{culture.TextInfo.ListSeparator} ";
                    var converter = TypeDescriptor.GetConverter(typeof(double));
                    var strArray = new string[4];
                    var num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, rectangle2D.X);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangle2D.Y);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangle2D.Width);
                    strArray[num++] = converter.ConvertToString(context, culture, rectangle2D.Height);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(Rectangle2D).GetConstructor(new Type[] { typeof(double), typeof(double), typeof(double), typeof(double) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { rectangle2D.X, rectangle2D.Y, rectangle2D.Width, rectangle2D.Height });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
