﻿// <copyright file="MidiTonality.cs" company="Shkyrockett">
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
