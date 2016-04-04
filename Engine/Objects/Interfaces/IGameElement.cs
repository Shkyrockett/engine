// <copyright file="IGameElement.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author>Shkyrockett</author>

namespace Engine.Objects
{
    /// <summary>
    /// Interface for game elements that should show up in the game editor designer tree.
    /// </summary>
    public interface IGameElement
    {
        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name for the editor designer.
        /// </summary>
        string DisplayName { get; set; }
    }
}
