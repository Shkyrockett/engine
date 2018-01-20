﻿// <copyright file="Game.cs" company="Shkyrockett" >
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
    /// Root game object.
    /// </summary>
    public class Game
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public Game()
        {
            Name = nameof(Game);
            DisplayName = nameof(Game);
            Scenes = new List<Scene>();
            Actors = new List<Actor>();
            ActorsDialog = new List<ActorDialog>();
            Audio = new List<IAudio>();
            Graphics = new List<IBitmap>();
            Variables = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name for the editor designer.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the list of scenes 
        /// </summary>
        public List<Scene> Scenes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Actor> Actors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ActorDialog> ActorsDialog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IAudio> Audio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IBitmap> Graphics { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> Variables { get; set; }
    }
}