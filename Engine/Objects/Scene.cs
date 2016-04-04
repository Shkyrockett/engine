// <copyright file="Scene.cs" company="Shkyrockett">
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
    public class Scene
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Backdrop> backdrops;

        /// <summary>
        /// 
        /// </summary>
        private List<Actor> actors;

        /// <summary>
        /// 
        /// </summary>
        private List<IventoryItem> items;

        /// <summary>
        /// 
        /// </summary>
        public Scene()
        {
            Name = "Scene";
            DisplayName = "Scene";
            backdrops = new List<Backdrop>();
            actors = new List<Actor>();
            items = new List<IventoryItem>();
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
        public List<Backdrop> Backdrops
        {
            get { return backdrops; }
            set { backdrops = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Actor> Actors
        {
            get { return actors; }
            set { actors = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<IventoryItem> Inventory
        {
            get { return items; }
            set { items = value; }
        }
    }
}
