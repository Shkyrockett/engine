// <copyright file="StructConverter.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <seealso cref="System.ComponentModel.ExpandableObjectConverter" />
    public sealed class StructConverter<TType>
        : ExpandableObjectConverter
    {
        /// <summary>
        /// The instance ctor.
        /// </summary>
        private readonly ConstructorInfo instanceCtor;

        /// <summary>
        /// The parse.
        /// </summary>
        private readonly MethodInfo parse;

        /// <summary>
        /// The descriptions.
        /// </summary>
        private readonly PropertyDescriptorCollection descriptions;

        /// <summary>
        /// The instance ctor param names.
        /// </summary>
        private readonly string[] instanceCtorParamNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructConverter{TType}" /> class.
        /// </summary>
        public StructConverter()
        {
            var t = typeof(TType);
            parse = ParseMethodAttribute.GetParseMethod(t);
            descriptions = AdvBrowsableAttribute.GetDispMembers(t);
            if (descriptions is null)
            {
                instanceCtor = InstanceConstructorAttribute.GetConstructor(t, out instanceCtorParamNames);
                if (instanceCtor is null)
                {
                    var paraminfos = instanceCtor.GetParameters();
                    if (paraminfos.Length == instanceCtorParamNames.Length)
                    {
                        for (var index = 0; index < instanceCtorParamNames.Length; ++index)
                        {
                            var name = instanceCtorParamNames[index];
                            var descriptor = descriptions.Find(name, false);
                            if (descriptor is null || descriptor.PropertyType != paraminfos[index].ParameterType)
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
        /// The can convert from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The sourceType.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => (!(parse is null) && sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        /// <summary>
        /// The can convert to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => (!(instanceCtor is null) && destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);

        /// <summary>
        /// Get the create instance supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => !(instanceCtor is null) || base.GetCreateInstanceSupported(context);

        /// <summary>
        /// Get the properties supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => !(descriptions is null) || base.GetPropertiesSupported(context);

        /// <summary>
        /// Convert the from.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(parse is null) && value is string s)
            {
                try
                {
                    return parse.Invoke(null, new object[] { s });
                }
                catch (TargetInvocationException)
                {
                    throw;// ex.InnerException;
                }
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
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(instanceCtor is null) && value is TType v && destinationType == typeof(InstanceDescriptor))
            {
                return new InstanceDescriptor(instanceCtor, GetInstanceDescriptorObjects(v));
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Create the instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="propertyValues">The propertyValues.</param>
        /// <returns>
        /// The <see cref="object" />.
        /// </returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (!(instanceCtor is null) && !(propertyValues is null))
            {
                try
                {
                    return instanceCtor.Invoke(GetInstanceDescriptorObjects(propertyValues));
                }
                catch (TargetInvocationException)
                {
                    throw;// ex.InnerException;
                }
            }

            return base.CreateInstance(context, propertyValues);
        }

        /// <summary>
        /// Get the properties.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>
        /// The <see cref="PropertyDescriptorCollection" />.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (!(descriptions is null))
            {
                return descriptions;
            }

            return base.GetProperties(context, value, attributes);
        }

        /// <summary>
        /// Get the instance descriptor objects.
        /// </summary>
        /// <param name="propertyValues">The propertyValues.</param>
        /// <returns>
        /// The <see cref="object[]" />.
        /// </returns>
        private object[] GetInstanceDescriptorObjects(IDictionary propertyValues)
        {
            if (instanceCtorParamNames is null)
            {
                return null;
            }

            var rv = new object[instanceCtorParamNames.Length];
            for (var index = 0; index < instanceCtorParamNames.Length; ++index)
            {
                rv[index] = propertyValues[instanceCtorParamNames[index]];
            }

            return rv;
        }

        /// <summary>
        /// Get the instance descriptor objects.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="object[]" />.
        /// </returns>
        private object[] GetInstanceDescriptorObjects(object value)
        {
            var rv = new object[instanceCtorParamNames.Length];
            for (var index = 0; index < instanceCtorParamNames.Length; ++index)
            {
                var descriptor = descriptions.Find(instanceCtorParamNames[index], false);
                rv[index] = descriptor.GetValue(value);
            }

            return rv;
        }
    }
}
