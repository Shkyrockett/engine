// <copyright file="SizeFConverter.cs" >
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace Engine.Geometry
{
    /// <summary>
    /// SizeFTypeConverter
    /// </summary>
    public class SizeFConverter
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
        /// Converts the specified string into a SizeF
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var str = value as string;
            if (str == null)
                return base.ConvertFrom(context, culture, value);

            string str2 = str.Trim();
            if (str2.Length == 0)
                return null;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            char ch = culture.TextInfo.ListSeparator[0];
            string[] strArray = str2.Split(new char[] { ch });
            var numArray = new float[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
            for (int i = 0; i < numArray.Length; i++)
                numArray[i] = (float)converter.ConvertFromString(context, culture, strArray[i]);

            if (numArray.Length != 2)
                throw new ArgumentException("Parse failed.");

            return new SizeF(numArray[0], numArray[1]);
        }

        /// <summary>
        /// Converts the SizeF into a string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException(nameof(destinationType));

            if (value is SizeF)
            {
                if (destinationType == typeof(string))
                {
                    var sizeF = (SizeF)value;
                    if (culture == null)
                        culture = CultureInfo.CurrentCulture;

                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
                    var strArray = new string[2];
                    int num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, sizeF.Width);
                    strArray[num++] = converter.ConvertToString(context, culture, sizeF.Height);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                {
                    var sizeF2 = (SizeF)value;
                    ConstructorInfo constructor = typeof(SizeF).GetConstructor(new Type[] { typeof(double), typeof(double) });
                    if (constructor != null)
                        return new System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { sizeF2.Width, sizeF2.Height });
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
                return new SizeF((float)propertyValues["Width"], (float)propertyValues["Height"]);
            else
                return null;
        }
    }
}
