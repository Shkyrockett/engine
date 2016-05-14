﻿// <copyright file="Size2DConverter.cs" >
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Engine.Geometry
{
    /// <summary>
    /// Size2DTypeConverter
    /// </summary>
    public class Size2DConverter
        : ExpandableObjectConverter
    {
        /// <summary>
        /// Creates a new instance of Size2DConverter
        /// </summary>
        public Size2DConverter()
        { }

        /// <summary>
        /// Boolean, true if the source type is a string
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

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
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the specified string into a Size2D
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str == null)
            {
                return base.ConvertFrom(context, culture, value);
            }

            string str2 = str.Trim();
            if (str2.Length == 0)
            {
                return null;
            }

            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }

            char ch = culture.TextInfo.ListSeparator[0];
            string[] strArray = str2.Split(new char[] { ch });
            double[] numArray = new double[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (double)converter.ConvertFromString(context, culture, strArray[i]);
            }

            if (numArray.Length != 2)
            {
                throw new ArgumentException("Parse failed.");
            }

            return new Size2D(numArray[0], numArray[1]);
        }

        /// <summary>
        /// Converts the Size2D into a string
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (value is Size2D)
            {
                if (destinationType == typeof(string))
                {
                    Size2D size2D = (Size2D)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
                    string[] strArray = new string[2];
                    int num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, size2D.Width);
                    strArray[num++] = converter.ConvertToString(context, culture, size2D.Height);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor))
                {
                    Size2D size22D = (Size2D)value;
                    ConstructorInfo constructor = typeof(Size2D).GetConstructor(new Type[] { typeof(double), typeof(double) });
                    if (constructor != null)
                    {
                        return new System.ComponentModel.Design.Serialization.InstanceDescriptor(constructor, new object[] { size22D.Width, size22D.Height });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            if (propertyValues != null)
            {
                return new Size2D((double)propertyValues["X"], (double)propertyValues["Y"]);
            }
            else
            {
                return null;
            }
        }
    }
}