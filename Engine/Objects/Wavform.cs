// <copyright file="Wavform.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;

namespace Engine.Audio
{
    /// <summary>
    /// 
    /// </summary>
    public class Wavform
        : IAudio
    {
        /// <summary>
        /// 
        /// </summary>
        public Wavform()
        {
            DisplayName = "Wavform";
            Name = "Wavform";
            Filename = "Wavform.wav";
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