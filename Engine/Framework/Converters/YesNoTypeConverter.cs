// <copyright file="YesNoTypeConverter.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using static System.ComponentModel.TypeConverter;

namespace Engine.Tools
{
    /// <summary>
    /// The yes no type converter class.
    /// </summary>
    public class YesNoTypeConverter
        : TypeConverter
    {
        /// <summary>
        /// The can convert from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The sourceType.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// The can convert to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Convert the from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="Exception">Values must be "Yes" or "No"</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (((string)value).ToLower() == "yes")
                {
                    return true;
                }

                if (((string)value).ToLower() == "no")
                {
                    return false;
                }

                throw new Exception("Values must be \"Yes\" or \"No\"");
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Convert the to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((bool)value) ? "Yes" : "No";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Get the standard values supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Get the standard values.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="StandardValuesCollection"/>.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var bools = new bool[] { true, false };
            var svc = new StandardValuesCollection(bools);
            return svc;
        }
    }
}
