﻿// <copyright file="Actor.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    public class Actor
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public Actor()
        {
            DisplayName = nameof(Actor);
            Name = nameof(Actor);
            Inventory = new List<IventoryItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IventoryItem> Inventory { get; set; }
    }
}
