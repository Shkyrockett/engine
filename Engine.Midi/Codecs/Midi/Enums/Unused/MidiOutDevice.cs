// <copyright file="MidiOutDevices.cs" company="Shkyrockett">
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
/// Midi output devices.
/// </summary>
public enum MidiOutDevice
{
    /// <summary>
    /// The device is a MIDI port.
    /// </summary>
    MidiPort = 1,

    /// <summary>
    /// The device is a MIDI synth.
    /// </summary>
    Synth = 2,

    /// <summary>
    /// The device is a square wave synth.
    /// </summary>
    SquareWaveSynth = 3,

    /// <summary>
    /// The device is an FM synth.
    /// </summary>
    FMSynth = 4,

    /// <summary>
    /// The device is a MIDI mapper.
    /// </summary>
    MidiMapper = 5,

    /// <summary>
    /// The device is a WaveTable synth.
    /// </summary>
    WaveTableSynth = 6,

    /// <summary>
    /// The device is a software synth.
    /// </summary>
    SoftwareSynth = 7
}
