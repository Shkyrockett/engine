// <copyright file="IForce.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

using System.ComponentModel;
using System.Numerics;

/// <summary>
/// The IForce interface.
/// </summary>
public interface IForce<T>
    where T : INumber<T>
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The <see cref="T"/>.</value>
    T Value { get; /*set;*/ }

    ///// <summary>
    ///// 
    ///// </summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //string Name { get; }

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>The <see cref="string"/>.</value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    string Abbreviation { get; }

    /// <returns></returns>
    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    string ToString();
}
