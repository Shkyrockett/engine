// <copyright file="IPressure.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine;

/// <summary>
/// The IPressure interface.
/// </summary>
public interface IPressure<T>
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

    ///// <summary>
    ///// 
    ///// </summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //string Abbreviation { get; }

    /// <returns></returns>
    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    string ToString();
}
