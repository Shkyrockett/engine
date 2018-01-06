// <copyright file="IVector.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IVector<V>
        : IFormattable, //IComparable<V>, //IConvertible,
        IEquatable<V> where V : struct, IVector<V>
    { }
}
