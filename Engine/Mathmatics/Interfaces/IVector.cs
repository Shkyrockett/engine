// <copyright file="IVector.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public interface IVector<V>
        : IFormattable, //IComparable<V>, //IConvertible,
        IEquatable<V> where V : struct, IVector<V>
    { }
}
