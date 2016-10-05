﻿// <copyright file="Scene.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;

namespace Engine.Objects
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
            Name = "Scene";
            DisplayName = "Scene";
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
