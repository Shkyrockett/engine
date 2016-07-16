// <copyright file="Item.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class IventoryItem
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}