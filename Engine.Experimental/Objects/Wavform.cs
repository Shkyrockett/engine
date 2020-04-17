// <copyright file="Wavform.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The wavform class.
    /// </summary>
    public class Wavform
        : IAudio
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wavform"/> class.
        /// </summary>
        public Wavform()
        {
            DisplayName = nameof(Wavform);
            Name = nameof(Wavform);
            Filename = "Wavform.wav";
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