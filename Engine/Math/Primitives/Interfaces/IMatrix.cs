// <copyright file="IMatrix.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="V"></typeparam>
    public interface IMatrix<M, V>
        : IFormattable,
        IEnumerable<IEnumerable<double>>,
        IEquatable<M> where M : struct, IMatrix<M, V> where V : struct, IVector<V>
    {
    }
}
