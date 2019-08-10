﻿// <copyright file="IVector.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    /// The IVector interface.
    /// </summary>
    /// <typeparam name="V">The V.</typeparam>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IVector<V>
        : IPrimitive, //IComparable<V>, //IConvertible,
        IEquatable<V> where V : struct, IVector<V>
    { }
}