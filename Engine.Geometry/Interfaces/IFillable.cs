// <copyright file="IFillable.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The IFillable interface.
/// </summary>
public interface IFillable
{
    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    /// <value>The <see cref="IColor"/>.</value>
    IColor Color { get; set; }

    /// <summary>
    /// Gets or sets the fill mode.
    /// </summary>
    /// <value>The <see cref="FillMode"/>.</value>
    FillMode FillMode { get; set; }
}
