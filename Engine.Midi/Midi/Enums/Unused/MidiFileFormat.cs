// <copyright file="MidiFileFormat.cs" company="Shkyrockett">
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

namespace Engine.Midi
{
    /// <summary>
    /// Midi File Format Enumeration.
    /// </summary>
    public enum MidiFileFormat
    {
        /// <summary>
        /// Format has not been set.
        /// </summary>
        None,

        /// <summary>
        /// Midi karaoke file.
        /// </summary>
        Kar,

        /// <summary>
        /// Midi file.
        /// </summary>
        Midi,

        /// <summary>
        /// RMI, Riff based Midi File.
        /// </summary>
        RiffMidi,

        /// <summary>
        /// Unknown filetype.
        /// </summary>
        Unknown,
    }
}
