// <copyright file="Vector2DConverter.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Engine
{
    /// <summary>
    /// General Converter class for converting instances of other types to and from struct instances
    /// </summary>
    public sealed class StructConverter<TType>
        : ExpandableObjectConverter
    {
        /// <summary>
        /// 
        /// </summary>
        ConstructorInfo instanceCtor;

        /// <summary>
        /// 
        /// </summary>
        MethodInfo parse;

        /// <summary>
        /// 
        /// </summary>
        PropertyDescriptorCollection descriptions;

        /// <summary>
        /// 
        /// </summary>
        string[] instanceCtorParamNames;

        /// <summary>
        /// 
        /// </summary>
        public StructConverter()
        {
            Type t = typeof(TType);
            parse = ParseMethodAttribute.GetParseMethod(t);
            descriptions = AdvBrowsableAttribute.GetDispMembers(t);
            if (descriptions != null)
            {
                instanceCtor = InstanceConstructorAttribute.GetConstructor(t, out instanceCtorParamNames);
                if (instanceCtor != null)
                {
                    var paraminfos = instanceCtor.GetParameters();
                    if (paraminfos.Length == instanceCtorParamNames.Length)
                    {
                        for (var index = 0; index < instanceCtorParamNames.Length; ++index)
                        {
                            var name = instanceCtorParamNames[index];
                            var descriptor = descriptions.Find(name, false);
                            if (descriptor == null || descriptor.PropertyType != paraminfos[index].ParameterType)
                            {
                                instanceCtor = null;
                                break;
                            }
                        }
                    }
                    else
                    {
                        instanceCtor = null;
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => (parse != null && sourceType == typeof(string)) ||
                base.CanConvertFrom(context, sourceType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => (instanceCtor != null && destinationType == typeof(InstanceDescriptor)) ||
                base.CanConvertTo(context, destinationType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            => instanceCtor != null ||
                base.GetCreateInstanceSupported(context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            => descriptions != null ||
                base.GetPropertiesSupported(context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (parse != null && value is string)
            {
                try
                {
                    return parse.Invoke(null, new object[] { value });
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (instanceCtor != null &&
                value is TType &&
                destinationType == typeof(InstanceDescriptor))
            {

                return new InstanceDescriptor(instanceCtor, GetInstanceDescriptorObjects(value));
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (instanceCtor != null)
            {
                try
                {
                    return instanceCtor.Invoke(GetInstanceDescriptorObjects(propertyValues));
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
            return base.CreateInstance(context, propertyValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (descriptions != null)
            {
                return descriptions;
            }
            return base.GetProperties(context, value, attributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        private object[] GetInstanceDescriptorObjects(IDictionary propertyValues)
        {
            object[] rv = new object[instanceCtorParamNames.Length];
            for (var index = 0; index < instanceCtorParamNames.Length; ++index)
            {
                rv[index] = propertyValues[instanceCtorParamNames[index]];
            }
            return rv;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private object[] GetInstanceDescriptorObjects(object value)
        {
            object[] rv = new object[instanceCtorParamNames.Length];
            for (var index = 0; index < instanceCtorParamNames.Length; ++index)
            {
                var descriptor = descriptions.Find(instanceCtorParamNames[index], false);
                rv[index] = descriptor.GetValue(value);
            }
            return rv;
        }
    }
}
