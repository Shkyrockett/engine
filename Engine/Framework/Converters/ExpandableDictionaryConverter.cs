// <copyright file="ExpandableDictionaryConverter.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/32582504/propertygrid-expandable-collection
    /// </acknowledgment>
    public class ExpandableDictionaryConverter
        : CollectionConverter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ExpandableDictionaryConverter()
        { }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string))
            {
                return "[Key, Value]";
            }

            return base.ConvertTo(context, culture, value, destType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var dictionary = value as IDictionary;
            if (dictionary == null || dictionary.Count == 0)
                return base.GetProperties(context, value, attributes);

            var items = new PropertyDescriptorCollection(null);

            foreach (var key in dictionary.Keys)
            {
                items.Add(new ExpandableDictionaryPropertyDescriptor(dictionary, key));
            }

            return items;
        }

        #endregion
    }
}
