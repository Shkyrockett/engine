﻿// <copyright file="MidiFileTrackFormats.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File;

/// <summary>
/// Midi file Track format.
/// </summary>
public enum MidiFileTrackFormat
    : ushort // (2 bytes)
{
    /// <summary>
    /// Single track Midi file.
    /// </summary>
    SingleTrack = 0x00,

    /// <summary>
    /// Multiple synchronous track Midi file.
    /// </summary>
    MultipleTracksSynchronous = 0x01,

    /// <summary>
    /// Multiple asynchronous track Midi file.
    /// </summary>
    MultipleTracksAsynchronous = 0x02,
}
