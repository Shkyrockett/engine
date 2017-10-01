// <copyright file="ExpandableCollectionConverter.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// https://stackoverflow.com/q/32582504/7004229
    /// </acknowledgment>
    public class ExpandableCollectionConverter
        : CollectionConverter
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ExpandableCollectionConverter()
        { }

        #endregion

        #region Methods

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
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if (destinationType == typeof(string))
            {
                return "(Collection)";
            }

            return base.ConvertTo(context, culture, value, destinationType);
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
            var list = value as IList;
            if (list == null || list.Count == 0)
                return base.GetProperties(context, value, attributes);

            var propertyDescriptors = new PropertyDescriptorCollection(null);

            for (var i = 0; i < list.Count; i++)
            {
                var propertyDescriptor = new ExpandableCollectionPropertyDescriptor(list, i);
                //propertyDescriptor.RefreshRequired += (sender, args) =>
                //{
                //    var notifyValueGivenParentMethod = context.GetType().GetMethod("NotifyValueGivenParent", BindingFlags.NonPublic | BindingFlags.Instance);
                //    notifyValueGivenParentMethod.Invoke(context, new object[] { context.Instance, 1 });
                //};
                propertyDescriptors.Add(propertyDescriptor);
            }

            // return the property descriptor Collection
            return propertyDescriptors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            => true;

        #endregion
    }
}
