// <copyright file="IGameElement.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
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
