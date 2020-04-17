// <copyright file="IMatrix.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
    {
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="item">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] object item) => item is M d && Equals(this, d);
    }
}
