// <copyright file="Size3DConverter.cs" company="Shkyrockett" >
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
using System.Globalization;

namespace Engine
{
    /// <summary>
    /// Size3DTypeConverter
    /// </summary>
    public class Size3DConverter
        : ExpandableObjectConverter
    {
        /// <summary>
        /// Boolean, true if the source type is a string
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string) ? true : base.CanConvertFrom(context, sourceType);

        /// <summary>
        /// The can convert to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(string) ? true : base.CanConvertTo(context, destinationType);

        /// <summary>
        /// Converts the specified string into a Size3D.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="ArgumentException">Parse failed.</exception>
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

            var ch = culture.TextInfo.ListSeparator[0];
            var strArray = str2.Split(new char[] { ch });
            var numArray = new double[strArray.Length];
            var converter = TypeDescriptor.GetConverter(typeof(double));
            for (var i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (double)converter.ConvertFromString(context, culture, strArray[i]);
            }

            if (numArray.Length != 3)
            {
                throw new ArgumentException("Parse failed.");
            }

            return new Size3D(numArray[0], numArray[1], numArray[2]);
        }

        /// <summary>
        /// Converts the Size3D into a string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (value is Size3D)
            {
                if (destinationType == typeof(string))
                {
                    var Size3D = (Size3D)value;
                    if (culture is null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    var separator = culture.TextInfo.ListSeparator + " ";
                    var converter = TypeDescriptor.GetConverter(typeof(double));
                    var strArray = new string[2];
                    var num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, Size3D.Width);
                    strArray[num++] = converter.ConvertToString(context, culture, Size3D.Height);
                    strArray[num++] = converter.ConvertToString(context, culture, Size3D.Depth);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                {
                    var size22D = (Size3D)value;
                    var constructor = typeof(Size3D).GetConstructor(new Type[] { typeof(double), typeof(double) });
                    if (constructor != null)
                    {
                        return new System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { size22D.Width, size22D.Height, size22D.Depth });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Get the create instance supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Create the instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="propertyValues">The propertyValues.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues) => propertyValues != null
                ? new Size3D((double)propertyValues["Width"], (double)propertyValues["Height"], (double)propertyValues["Depth"])
                : (object)null;
    }
}
