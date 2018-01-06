// <copyright file="Midi.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class Midi
        : IAudio
    {
        /// <summary>
        /// 
        /// </summary>
        public Midi()
        {
            DisplayName = nameof(Midi);
            Name = nameof(Midi);
            Filename = "Midi.mid";
            TimeSyncPoints = new Dictionary<int, DateTimeOffset>();
            Lyrics = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Lyrics { get; set; }
    }
}