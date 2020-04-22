// <copyright file="MusicLiveTag.cs" company="Shkyrockett">
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    public enum MusicLiveTag
        : byte
    {
        /// <summary>
        /// The genre
        /// </summary>
        Genre = 01,

        /// <summary>
        /// The artist
        /// </summary>
        Artist = 02,

        /// <summary>
        /// The composer
        /// </summary>
        Composer = 03,

        /// <summary>
        /// The duration in seconds.
        /// </summary>
        Duration = 04,

        /// <summary>
        /// The BPM (tempo)
        /// </summary>
        Bpm = 05,
    }
}
