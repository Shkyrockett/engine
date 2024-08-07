﻿// <copyright file="GeneralMidiPercussion.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://www.midi.org/specifications/item/gm-level-1-sound-set
// </references>

namespace Engine.File;

/// <summary>
/// General Midi percussion instruments.
/// </summary>
public enum GeneralMidiPercussion
    : byte
{
    /// <summary>
    /// Acoustic Bass Drum.
    /// </summary>
    AcousticBassDrum = 35,

    /// <summary>
    /// Bass Drum 1.
    /// </summary>
    BassDrum1 = 36,

    /// <summary>
    /// Side Stick.
    /// </summary>
    SideStick = 37,

    /// <summary>
    /// Acoustic Snare.
    /// </summary>
    AcousticSnare = 38,

    /// <summary>
    /// Hand Clap.
    /// </summary>
    HandClap = 39,

    /// <summary>
    /// Electric Snare.
    /// </summary>
    ElectricSnare = 40,

    /// <summary>
    /// Low Floor Tom.
    /// </summary>
    LowFloorTom = 41,

    /// <summary>
    /// Closed Hi Hat.
    /// </summary>
    ClosedHiHat = 42,

    /// <summary>
    /// High Floor Tom.
    /// </summary>
    HighFloorTom = 43,

    /// <summary>
    /// Pedal Hi Hat.
    /// </summary>
    PedalHiHat = 44,

    /// <summary>
    /// Low Tom.
    /// </summary>
    LowTom = 45,

    /// <summary>
    /// Open Hi Hat.
    /// </summary>
    OpenHiHat = 46,

    /// <summary>
    /// Low Mid Tom.
    /// </summary>
    LowMidTom = 47,

    /// <summary>
    /// Hi Mid Tom.
    /// </summary>
    HiMidTom = 48,

    /// <summary>
    /// Crash Cymbal 1.
    /// </summary>
    CrashCymbal1 = 49,

    /// <summary>
    /// High Tom.
    /// </summary>
    HighTom = 50,

    /// <summary>
    /// Ride Cymbal.
    /// </summary>
    RideCymbal = 51,

    /// <summary>
    /// Chinese Cymbal.
    /// </summary>
    ChineseCymbal = 52,

    /// <summary>
    /// Ride Bell.
    /// </summary>
    RideBell = 53,

    /// <summary>
    /// Tambourine instrument.
    /// </summary>
    Tambourine = 54,

    /// <summary>
    /// Splash Cymbal.
    /// </summary>
    SplashCymbal = 55,

    /// <summary>
    /// Cowbell instrument.
    /// </summary>
    Cowbell = 56,

    /// <summary>
    /// Crash Cymbal 2.
    /// </summary>
    CrashCymbal2 = 57,

    /// <summary>
    /// Vibraslap instrument.
    /// </summary>
    Vibraslap = 58,

    /// <summary>
    /// Ride Cymbal 2.
    /// </summary>
    RideCymbal2 = 59,

    /// <summary>
    /// Hi Bongo.
    /// </summary>
    HiBongo = 60,

    /// <summary>
    /// Low Bongo.
    /// </summary>
    LowBongo = 61,

    /// <summary>
    /// Mute Hi Conga.
    /// </summary>
    MuteHiConga = 62,

    /// <summary>
    /// Open Hi Conga.
    /// </summary>
    OpenHiConga = 63,

    /// <summary>
    /// Low Conga.
    /// </summary>
    LowConga = 64,

    /// <summary>
    /// High Timbale.
    /// </summary>
    HighTimbale = 65,

    /// <summary>
    /// Low Timbale.
    /// </summary>
    LowTimbale = 66,

    /// <summary>
    /// High Agogo.
    /// </summary>
    HighAgogo = 67,

    /// <summary>
    /// Low Agogo.
    /// </summary>
    LowAgogo = 68,

    /// <summary>
    /// Cabasa instrument.
    /// </summary>
    Cabasa = 69,

    /// <summary>
    /// Maracas instrument.
    /// </summary>
    Maracas = 70,

    /// <summary>
    /// Short Whistle.
    /// </summary>
    ShortWhistle = 71,

    /// <summary>
    /// Long Whistle.
    /// </summary>
    LongWhistle = 72,

    /// <summary>
    /// Short Guiro.
    /// </summary>
    ShortGuiro = 73,

    /// <summary>
    /// Long Guiro.
    /// </summary>
    LongGuiro = 74,

    /// <summary>
    /// Claves instrument.
    /// </summary>
    Claves = 75,

    /// <summary>
    /// HiWood Block.
    /// </summary>
    HiWoodBlock = 76,

    /// <summary>
    /// LowWood Block.
    /// </summary>
    LowWoodBlock = 77,

    /// <summary>
    /// Mute Cuica.
    /// </summary>
    MuteCuica = 78,

    /// <summary>
    /// Open Cuica.
    /// </summary>
    OpenCuica = 79,

    /// <summary>
    /// Mute Triangle.
    /// </summary>
    MuteTriangle = 80,

    /// <summary>
    /// Open Triangle.
    /// </summary>
    OpenTriangle = 81,
}
