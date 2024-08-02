// <copyright file="IPropertyCaching.cs" company="Shkyrockett" >
// Copyright © 2019 - 2024 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// 
/// </summary>
/// <seealso cref="IFormattable" />
/// <seealso cref="INotifyPropertyChanging" />
/// <seealso cref="INotifyPropertyChanged" />
public interface IPropertyCaching
    : IFormattable, INotifyPropertyChanging, INotifyPropertyChanged
{
    /// <summary>
    /// Property cache for commonly used properties that may take time to calculate.
    /// </summary>
    [Browsable(false)]
    [field: NonSerialized]
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    protected Dictionary<object, object> PropertyCache { get; set; }

    /// <summary>
    /// Protected method for caching computationally and memory intensive properties of child objects
    /// so that the intensive properties only get recalculated and stored when necessary.
    /// </summary>
    /// <param name="propertyLambda">The property.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public object CachingProperty(Func<object> propertyLambda, [CallerMemberName] string name = "")
    {
        if (!PropertyCache.ContainsKey(name))
        {
            var value = propertyLambda?.Invoke();
            PropertyCache.Add(name, value);
            return value;
        }

        return PropertyCache[name];
    }

    /// <summary>
    /// This should be run anytime a property of the item is modified.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ClearCache() => PropertyCache.Clear();

    /// <summary>
    /// Called when [property changing].
    /// </summary>
    /// <param name="name">The name.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void OnPropertyChanging([CallerMemberName] string name = "");

    /// <summary>
    /// Called when [property changed].
    /// </summary>
    /// <param name="name">The name.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void OnPropertyChanged([CallerMemberName] string name = "");
}
