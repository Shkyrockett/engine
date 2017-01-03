// <copyright file="ExpandableCollectionPropertyDescriptor.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// http://stackoverflow.com/questions/32582504/propertygrid-expandable-collection
    /// </summary>
    public class ExpandableCollectionPropertyDescriptor
        : PropertyDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        private IList collection;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _index;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="idx"></param>
        public ExpandableCollectionPropertyDescriptor(IList coll, int idx)
            : base(GetDisplayName(coll, idx), null)
        {
            collection = coll;
            _index = idx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetDisplayName(IList list, int index)
            => $"[{index,4}] {CSharpName(list[index].GetType())}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string CSharpName(Type type)
        {
            var name = type.Name;
            if (!type.IsGenericType)
                return name;
            return $"{name.Substring(0, name.IndexOf('`'))}<{string.Join(", ", type.GetGenericArguments().Select(CSharpName))}>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
            => true;

        /// <summary>
        /// 
        /// </summary>
        public override Type ComponentType
            => collection.GetType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override object GetValue(object component)
            => collection[_index];

        /// <summary>
        /// 
        /// </summary>
        public override bool IsReadOnly
            => false;

        /// <summary>
        /// 
        /// </summary>
        public override string Name
            => _index.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        public override Type PropertyType
            => collection[_index].GetType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(object component) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
            => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
            => collection[_index] = value;
    }
}
