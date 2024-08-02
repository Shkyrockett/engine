// <copyright file="IMatrix.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

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
    /// Gets the number of rows.
    /// </summary>
    /// <value>
    /// The rows.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public int Rows { get; }

    /// <summary>
    /// Gets the number of columns.
    /// </summary>
    /// <value>
    /// The columns.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public int Columns { get; }

    /// <summary>
    /// Gets the number of cells in the Matrix.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public int Count => Rows * Columns;

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="item">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals([AllowNull] object item) => item is M d && Equals(this, d);
}
