// <copyright file="MidiTonality.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    /// Midi Tonality. Major/Minor.
    /// </summary>
    public enum MidiTonality
        : byte
    {
        /// <summary>
        /// Major key.
        /// </summary>
        Major = 0x00,

        /// <summary>
        /// Minor key.
        /// </summary>
        Minor = 0x01,
    }
}
