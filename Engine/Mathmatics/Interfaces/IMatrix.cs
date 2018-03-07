// <copyright file="IMatrix.cs" company="Shkyrockett" >
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
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The IMatrix interface.
    /// </summary>
    /// <typeparam name="M">The M.</typeparam>
    /// <typeparam name="V">The V.</typeparam>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IMatrix<M, V>
        : IPrimitive, 
        IEnumerable<IEnumerable<double>>,
        IEquatable<M> where M : struct, IMatrix<M, V> where V : struct, IVector<V>
    { }
}
