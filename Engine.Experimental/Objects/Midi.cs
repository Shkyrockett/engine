// <copyright file="Midi.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// The midi class.
    /// </summary>
    public class Midi
        : IAudio
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Midi"/> class.
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
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time sync points.
        /// </summary>
        public Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// Gets or sets the lyrics.
        /// </summary>
        public List<string> Lyrics { get; set; }
    }
}