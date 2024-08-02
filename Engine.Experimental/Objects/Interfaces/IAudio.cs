// <copyright file="IAudio.cs" company="Shkyrockett" >
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
/// The IAudio interface.
/// </summary>
public interface IAudio
    : IGameElement
{
    /// <summary>
    /// Gets or sets the filename.
    /// </summary>
    /// <value>The <see cref="string"/>.</value>
    string Filename { get; set; }

    /// <summary>
    /// Gets or sets the time sync points.
    /// </summary>
    /// <value>The <see cref="Dictionary{TKey, TValue}"/>.</value>
    Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

    /// <summary>
    /// Gets or sets the lyrics.
    /// </summary>
    /// <value>The <see cref="List{T}"/>.</value>
    List<string> Lyrics { get; set; }
}
