// <copyright file="Scene.cs" company="Shkyrockett" >
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
/// The scene class.
/// </summary>
public class Scene
    : IGameElement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scene"/> class.
    /// </summary>
    public Scene()
    {
        Name = nameof(Scene);
        DisplayName = nameof(Scene);
        Backdrops = [];
        Actors = [];
        Inventory = [];
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the display name.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the backdrops.
    /// </summary>
    public List<Backdrop> Backdrops { get; set; }

    /// <summary>
    /// Gets or sets the actors.
    /// </summary>
    public List<Actor> Actors { get; set; }

    /// <summary>
    /// Gets or sets the inventory.
    /// </summary>
    public List<IventoryItem> Inventory { get; set; }
}
