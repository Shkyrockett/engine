// <copyright file="Actor.cs" company="Shkyrockett" >
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
/// The actor class.
/// </summary>
public class Actor
    : IGameElement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Actor"/> class.
    /// </summary>
    public Actor()
    {
        DisplayName = nameof(Actor);
        Name = nameof(Actor);
        Inventory = [];
    }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the inventory.
    /// </summary>
    public List<IventoryItem> Inventory { get; set; }
}
