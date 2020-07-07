// <copyright file="IVector.cs" company="Shkyrockett" >
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
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The IVector interface.
    /// </summary>
    /// <typeparam name="V">The V.</typeparam>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IVector<V>
        : IPrimitive, //IComparable<V>, //IConvertible,
        IEquatable<V> where V : /*struct,*/ IVector<V>
    {
        /// <summary>
        /// Gets the number of components in the Vector.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count { get; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="vector">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] object vector) => vector is V d && Equals(d);
    }
}
