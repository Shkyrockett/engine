﻿// <copyright file="ITweenable.cs" company="Shkyrockett" >
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
/// The ITweenable interface.
/// </summary>
public interface ITweenable
{
    /// <summary>
    /// Gets or sets the values.
    /// </summary>
    /// <value>The <see cref="List{T}"/>.</value>
    List<double> Values { get; set; }
}
