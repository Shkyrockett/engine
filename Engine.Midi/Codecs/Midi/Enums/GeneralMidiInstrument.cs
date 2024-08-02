// <copyright file="GeneralMidiInstruments.cs" company="Shkyrockett">
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
/// General Midi instruments.
/// </summary>
public enum GeneralMidiInstrument
    : byte
{
    #region Piano
    /// <summary>
    /// Acoustic Grand Piano.
    /// </summary>
    AcousticGrand = 0,

    /// <summary>
    /// Bright Acoustic Piano.
    /// </summary>
    BrightAcoustic = 1,

    /// <summary>
    /// Electric Grand Piano.
    /// </summary>
    ElectricGrand = 2,

    /// <summary>
    /// Honky Tonk Piano.
    /// </summary>
    HonkyTonk = 3,

    /// <summary>
    /// Electric Piano 1.
    /// </summary>
    ElectricPiano1 = 4,

    /// <summary>
    /// Electric Piano 2.
    /// </summary>
    ElectricPiano2 = 5,

    /// <summary>
    /// Harpsichord instrument.
    /// </summary>
    Harpsichord = 6,

    /// <summary>
    /// Clavinet instrument.
    /// </summary>
    Clavinet = 7,
    #endregion Piano

    #region Chromatic Percussion
    /// <summary>
    /// Celesta instrument.
    /// </summary>
    Celesta = 8,

    /// <summary>
    /// Glockenspiel instrument.
    /// </summary>
    Glockenspiel = 9,

    /// <summary>
    /// Music Box
    /// </summary>
    MusicBox = 10,

    /// <summary>
    /// Vibraphone instrument.
    /// </summary>
    Vibraphone = 11,

    /// <summary>
    /// Marimba instrument.
    /// </summary>
    Marimba = 12,

    /// <summary>
    /// Xylophone instrument.
    /// </summary>
    Xylophone = 13,

    /// <summary>
    /// Tubular Bells.
    /// </summary>
    TubularBells = 14,

    /// <summary>
    /// Dulcimer instrument.
    /// </summary>
    Dulcimer = 15,
    #endregion Chromatic Percussion

    #region Organ
    /// <summary>
    /// Drawbar Organ.
    /// </summary>
    DrawbarOrgan = 16,

    /// <summary>
    /// Percussive Organ.
    /// </summary>
    PercussiveOrgan = 17,

    /// <summary>
    /// Rock Organ.
    /// </summary>
    RockOrgan = 18,

    /// <summary>
    /// Church Organ.
    /// </summary>
    ChurchOrgan = 19,

    /// <summary>
    /// Reed Organ.
    /// </summary>
    ReedOrgan = 20,

    /// <summary>
    /// Accoridan instrument.
    /// </summary>
    Accoridan = 21,

    /// <summary>
    /// Harmonica instrument.
    /// </summary>
    Harmonica = 22,

    /// <summary>
    /// Tango Accordian.
    /// </summary>
    TangoAccordian = 23,
    #endregion Organ

    #region Guitar
    /// <summary>
    /// Nylon String Acoustic Guitar.
    /// </summary>
    NylonStringAcousticGuitar = 24,

    /// <summary>
    /// Steel String Acoustic Guitar.
    /// </summary>
    SteelStringAcousticGuitar = 25,

    /// <summary>
    /// Jazz Electric Guitar.
    /// </summary>
    JazzElectricGuitar = 26,

    /// <summary>
    /// Clean Electric Guitar.
    /// </summary>
    CleanElectricGuitar = 27,

    /// <summary>
    /// Muted Electric Guitar.
    /// </summary>
    MutedElectricGuitar = 28,

    /// <summary>
    /// Overdriven Guitar.
    /// </summary>
    OverdrivenGuitar = 29,

    /// <summary>
    /// Distortion Guitar.
    /// </summary>
    DistortionGuitar = 30,

    /// <summary>
    /// Guitar Harmonics.
    /// </summary>
    GuitarHarmonics = 31,
    #endregion Guitar

    #region Bass
    /// <summary>
    /// Acoustic Bass.
    /// </summary>
    AcousticBass = 32,

    /// <summary>
    /// Finger Electric Bass.
    /// </summary>
    FingerElectricBass = 33,

    /// <summary>
    /// Pick Electric Bass.
    /// </summary>
    PickElectricBass = 34,

    /// <summary>
    /// Fret-less Bass.
    /// </summary>
    FretlessBass = 35,

    /// <summary>
    /// Slap Bass 1.
    /// </summary>
    SlapBass1 = 36,

    /// <summary>
    /// Slap Bass 2.
    /// </summary>
    SlapBass2 = 37,

    /// <summary>
    /// Synth Bass 1.
    /// </summary>
    SynthBass1 = 38,

    /// <summary>
    /// Synth Bass 2.
    /// </summary>
    SynthBass2 = 39,
    #endregion Bass

    #region Strings
    /// <summary>
    /// Violin instrument.
    /// </summary>
    Violin = 40,

    /// <summary>
    /// Viola instrument.
    /// </summary>
    Viola = 41,

    /// <summary>
    /// Cello instrument.
    /// </summary>
    Cello = 42,

    /// <summary>
    /// Contrabass instrument.
    /// </summary>
    Contrabass = 43,

    /// <summary>
    /// Tremolo Strings.
    /// </summary>
    TremoloStrings = 44,

    /// <summary>
    /// Pizzicato Strings.
    /// </summary>
    PizzicatoStrings = 45,

    /// <summary>
    /// Orchestral Strings.
    /// </summary>
    OrchestralStrings = 46,

    /// <summary>
    /// Timpani instrument.
    /// </summary>
    Timpani = 47,
    #endregion Strings

    #region Ensemble
    /// <summary>
    /// FastString Ensemble.
    /// </summary>
    FastStringEnsemble = 48,

    /// <summary>
    /// String Ensemble.
    /// </summary>
    SlowStringEnsemble = 49,

    /// <summary>
    /// Synth Strings 1.
    /// </summary>
    SynthStrings1 = 50,

    /// <summary>
    /// Synth Strings 2.
    /// </summary>
    SynthStrings2 = 51,

    /// <summary>
    /// Choir Aahs.
    /// </summary>
    ChoirAahs = 52,

    /// <summary>
    /// Voice Oohs.
    /// </summary>
    VoiceOohs = 53,

    /// <summary>
    /// Synth Choir Voice.
    /// </summary>
    SynthChoirVoice = 54,

    /// <summary>
    /// Orchestra Hit.
    /// </summary>
    OrchestraHit = 55,
    #endregion Ensemble

    #region Brass
    /// <summary>
    /// Trumpet instrument.
    /// </summary>
    Trumpet = 56,

    /// <summary>
    /// Trombone instrument.
    /// </summary>
    Trombone = 57,

    /// <summary>
    /// Tuba instrument.
    /// </summary>
    Tuba = 58,

    /// <summary>
    /// Muted Trumpet.
    /// </summary>
    MutedTrumpet = 59,

    /// <summary>
    /// French Horn.
    /// </summary>
    FrenchHorn = 60,

    /// <summary>
    /// Brass Section.
    /// </summary>
    BrassSection = 61,

    /// <summary>
    /// Synth Brass 1.
    /// </summary>
    SynthBrass1 = 62,

    /// <summary>
    /// Synth Brass 2.
    /// </summary>
    SynthBrass2 = 63,
    #endregion Brass

    #region Reed
    /// <summary>
    /// Soprano Sax.
    /// </summary>
    SopranoSax = 64,

    /// <summary>
    /// Alto Sax.
    /// </summary>
    AltoSax = 65,

    /// <summary>
    /// Tenor Sax.
    /// </summary>
    TenorSax = 66,

    /// <summary>
    /// Baritone Sax.
    /// </summary>
    BaritoneSax = 67,

    /// <summary>
    /// Oboe instrument.
    /// </summary>
    Oboe = 68,

    /// <summary>
    /// English Horn.
    /// </summary>
    EnglishHorn = 69,

    /// <summary>
    /// Bassoon instrument.
    /// </summary>
    Bassoon = 70,

    /// <summary>
    /// Clarinet instrument.
    /// </summary>
    Clarinet = 71,
    #endregion Reed

    #region Pipe
    /// <summary>
    /// Piccolo instrument.
    /// </summary>
    Piccolo = 72,

    /// <summary>
    /// Flute instrument.
    /// </summary>
    Flute = 73,

    /// <summary>
    /// Recorder instrument.
    /// </summary>
    Recorder = 74,

    /// <summary>
    /// Pan Flute.
    /// </summary>
    PanFlute = 75,

    /// <summary>
    /// Blown Bottle.
    /// </summary>
    BlownBottle = 76,

    /// <summary>
    /// Skakuhachi instrument.
    /// </summary>
    Shakuhachi = 77,

    /// <summary>
    /// Whistle instrument.
    /// </summary>
    Whistle = 78,

    /// <summary>
    /// Ocarina instrument.
    /// </summary>
    Ocarina = 79,
    #endregion Pipe

    #region Synth Lead
    /// <summary>
    /// Square Wave Lead.
    /// </summary>
    SquareWaveLead = 80,

    /// <summary>
    /// Sawtooth Wave Lead.
    /// </summary>
    SawtoothWaveLead = 81,

    /// <summary>
    /// Synth Calliope Lead.
    /// </summary>
    SynthCalliopeLead = 82,

    /// <summary>
    /// Chiffer Lead.
    /// </summary>
    ChifferLead = 83,

    /// <summary>
    /// Charang Lead.
    /// </summary>
    CharangLead = 84,

    /// <summary>
    /// Solo Voice Lead.
    /// </summary>
    SoloVoiceLead = 85,

    /// <summary>
    /// Fifths Saw Wave Lead.
    /// </summary>
    FifthsSawWaveLead = 86,

    /// <summary>
    /// Base Lead.
    /// </summary>
    BaseLead = 87,
    #endregion Synth Lead

    #region Synth Pad
    /// <summary>
    /// New Age Fantasia Pad.
    /// </summary>
    NewAgeFantasiaPad = 88,

    /// <summary>
    /// Warm Pad.
    /// </summary>
    WarmPad = 89,

    /// <summary>
    /// Polysynth Pad.
    /// </summary>
    PolysynthPad = 90,

    /// <summary>
    /// Space Voice Choir Pad.
    /// </summary>
    SpaceVoiceChoirPad = 91,

    /// <summary>
    /// Bowed Glass Pad.
    /// </summary>
    BowedGlassPad = 92,

    /// <summary>
    /// Metallic Pad.
    /// </summary>
    MetallicPad = 93,

    /// <summary>
    /// Halo Pad.
    /// </summary>
    HaloPad = 94,

    /// <summary>
    /// Sweep Pad.
    /// </summary>
    SweepPad = 95,
    #endregion Synth Pad

    #region Synth Effects
    /// <summary>
    /// Ice Rain FX sounds.
    /// </summary>
    IceRainFX = 96,

    /// <summary>
    /// Soundtrack FX sounds.
    /// </summary>
    SoundtrackFX = 97,

    /// <summary>
    /// Crystal FX sounds.
    /// </summary>
    CrystalFX = 98,

    /// <summary>
    /// Atmosphere FX sounds.
    /// </summary>
    AtmosphereFX = 99,

    /// <summary>
    /// Brightness FX sounds.
    /// </summary>
    BrightnessFX = 100,

    /// <summary>
    /// Goblin FX sounds.
    /// </summary>
    GoblinFX = 101,

    /// <summary>
    /// Echo FX sounds.
    /// </summary>
    EchosFX = 102,

    /// <summary>
    /// SciFi Star Theme FX sounds.
    /// </summary>
    SciFiStarThemeFX = 103,
    #endregion Synth Effects

    #region Ethnic
    /// <summary>
    /// Sitar instrument.
    /// </summary>
    Sitar = 104,

    /// <summary>
    /// Banjo instrument.
    /// </summary>
    Banjo = 105,

    /// <summary>
    /// Shamisen instrument.
    /// </summary>
    Shamisen = 106,

    /// <summary>
    /// Koto instrument.
    /// </summary>
    Koto = 107,

    /// <summary>
    /// Kalimba instrument.
    /// </summary>
    Kalimba = 108,

    /// <summary>
    /// Bagpipe instrument.
    /// </summary>
    Bagpipe = 109,

    /// <summary>
    /// Fiddle instrument.
    /// </summary>
    Fiddle = 110,

    /// <summary>
    /// Shanai instrument.
    /// </summary>
    Shanai = 111,
    #endregion Ethnic

    #region Percussive
    /// <summary>
    /// Tinkle Bell.
    /// </summary>
    TinkleBell = 112,

    /// <summary>
    /// Agogo sound.
    /// </summary>
    Agogo = 113,

    /// <summary>
    /// Steel Drums.
    /// </summary>
    SteelDrums = 114,

    /// <summary>
    /// Woodblock sound.
    /// </summary>
    Woodblock = 115,

    /// <summary>
    /// Taiko Drum.
    /// </summary>
    TaikoDrum = 116,

    /// <summary>
    /// Melodic Tom.
    /// </summary>
    MelodicTom = 117,

    /// <summary>
    /// Synth Drum.
    /// </summary>
    SynthDrum = 118,

    /// <summary>
    /// Reverse Cymbal.
    /// </summary>
    ReverseCymbal = 119,
    #endregion Percussive

    #region Sound Effects
    /// <summary>
    /// Guitar Fret Noise.
    /// </summary>
    GuitarFretNoise = 120,

    /// <summary>
    /// Breath Noise.
    /// </summary>
    BreathNoise = 121,

    /// <summary>
    /// Seashore sounds.
    /// </summary>
    Seashore = 122,

    /// <summary>
    /// Bird Tweet.
    /// </summary>
    BirdTweet = 123,

    /// <summary>
    /// Telephone Ring.
    /// </summary>
    TelephoneRing = 124,

    /// <summary>
    /// Helicopter sound.
    /// </summary>
    Helicopter = 125,

    /// <summary>
    /// Applause sounds.
    /// </summary>
    Applause = 126,

    /// <summary>
    /// Gunshot noise.
    /// </summary>
    Gunshot = 127,
    #endregion Sound Effects
}
