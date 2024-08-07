﻿// <copyright file="IArea.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The IArea interface.
/// </summary>
public interface IArea
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The <see cref="double"/>.</value>
    double Value { get; /*set;*/ }

    /// <returns></returns>
    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    string ToString();
}
