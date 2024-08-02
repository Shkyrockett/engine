// <copyright file="IBitmap.cs" company="Shkyrockett" >
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
/// The IBitmap interface.
/// </summary>
public interface IBitmap
{
    /// <summary>
    /// Gets or sets the stream.
    /// </summary>
    /// <value>The <see cref="Stream"/>.</value>
    Stream Stream { get; set; }
}
