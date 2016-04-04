// <copyright file="Actor.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

using System.Collections.Generic;

namespace Engine.Objects
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
            DisplayName = "Actor";
            Name = "Actor";
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
