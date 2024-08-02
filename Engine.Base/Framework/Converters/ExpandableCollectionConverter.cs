// <copyright file="ExpandableCollectionConverter.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Engine;

/// <summary>
/// The expandable collection converter class.
/// </summary>
/// <seealso cref="CollectionConverter" />
/// <acknowledgment>
/// http://stackoverflow.com/questions/32582504/propertygrid-expandable-collection
/// https://stackoverflow.com/q/32582504/7004229
/// </acknowledgment>
public class ExpandableCollectionConverter
    : CollectionConverter
{
    #region Methods
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
    /// <exception cref="ArgumentNullException">destinationType</exception>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return destinationType == typeof(string) ? "(Collection)" : base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// Get the properties supported.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

    /// <summary>
    /// Get the properties.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="value">The value.</param>
    /// <param name="attributes">The attributes.</param>
    /// <returns>
    /// The <see cref="PropertyDescriptorCollection" />.
    /// </returns>
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        if (value is not IList list || list.Count == 0)
        {
            return base.GetProperties(context, value, attributes);
        }

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
    #endregion Methods
}
