// <copyright file="IPropertyCaching.cs" company="Shkyrockett" >
//     Copyright © 2019 - 2020 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IFormattable" />
    /// <seealso cref="INotifyPropertyChanging" />
    /// <seealso cref="INotifyPropertyChanged" />
    public interface IPropertyCaching
        : IFormattable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
#pragma warning disable CS0657 // Not a valid attribute location for this declaration
        [field: NonSerialized]
#pragma warning restore CS0657 // Not a valid attribute location for this declaration
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        protected Dictionary<object, object> PropertyCache { get; set; }

        /// <summary>
        /// Protected method for caching computationally and memory intensive properties of child objects
        /// so that the intensive properties only get recalculated and stored when necessary.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object CachingProperty(Func<object> property, [CallerMemberName] string name = "")
        {
            if (!PropertyCache.ContainsKey(name))
            {
                var value = property?.Invoke();
                PropertyCache.Add(name, value);
                return value;
            }

            return PropertyCache[name];
        }

        /// <summary>
        /// This should be run anytime a property of the item is modified.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearCache() => PropertyCache.Clear();

        /// <summary>
        /// Called when [property changing].
        /// </summary>
        /// <param name="name">The name.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "");

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "");
    }
}
