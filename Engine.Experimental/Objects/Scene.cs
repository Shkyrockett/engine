// <copyright file="Scene.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Scene
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public Scene()
        {
            Name = nameof(Scene);
            DisplayName = nameof(Scene);
            Backdrops = new List<Backdrop>();
            Actors = new List<Actor>();
            Inventory = new List<IventoryItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Backdrop> Backdrops { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Actor> Actors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IventoryItem> Inventory { get; set; }
    }
}
