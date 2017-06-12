// <copyright file="PointFConverter.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// PointFTypeConverter
    /// </summary>
    public class PointFConverter
        : ExpandableObjectConverter
    {
        /// <summary>
        /// Boolean, true if the source type is a string
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the specified string into a PointF
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (str == null)
                return base.ConvertFrom(context, culture, value);

            var str2 = str.Trim();
            if (str2.Length == 0)
                return null;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            var ch = culture.TextInfo.ListSeparator[0];
            string[] strArray = str2.Split(new char[] { ch });
            var numArray = new float[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
            for (var i = 0; i < numArray.Length; i++)
                numArray[i] = (float)converter.ConvertFromString(context, culture, strArray[i]);

            if (numArray.Length != 2)
                throw new ArgumentException("Parse failed.");

            return new PointF(numArray[0], numArray[1]);
        }

        /// <summary>
        /// Converts the PointF into a string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException(nameof(destinationType));

            if (value is PointF)
            {
                if (destinationType == typeof(string))
                {
                    var point = (PointF)value;
                    if (culture == null)
                        culture = CultureInfo.CurrentCulture;

                    var separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
                    var strArray = new string[2];
                    var num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, point.X);
                    strArray[num++] = converter.ConvertToString(context, culture, point.Y);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                {
                    var point2 = (PointF)value;
                    ConstructorInfo constructor = typeof(PointF).GetConstructor(new Type[] { typeof(float), typeof(float) });
                    if (constructor != null)
                        return new System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { point2.X, point2.Y });
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            if (propertyValues != null)
                return new PointF((float)propertyValues["X"], (float)propertyValues["Y"]);
            else
                return null;
        }
    }
}
