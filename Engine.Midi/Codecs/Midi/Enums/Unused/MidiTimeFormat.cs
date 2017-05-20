// <copyright file="MidiTimeFormat.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// Midi file Time Format.
    /// </summary>
    public enum MidiTimeFormat
    {
        /// <summary>
        /// Time is based on Ticks per beat.
        /// </summary>
        TicksPerBeat = 0,

        /// <summary>
        /// Time is based on Frames per second.
        /// </summary>
        FamesPerSecond = 1
    }
}
