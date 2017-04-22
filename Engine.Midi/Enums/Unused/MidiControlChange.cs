// <copyright file="MidiControlChange.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// https://www.midi.org/specifications/item/table-3-control-change-messages-data-bytes-2
// </references>

namespace Engine.Midi
{
    /// <summary>
    /// Midi Controllers.
    /// </summary>
    public enum MidiControlChange
        : byte
    {
        /// <summary>
        /// Bank Select.
        /// </summary>
        BankSelect = 0x00,

        /// <summary>
        /// Modulation Wheel or Lever.
        /// </summary>
        Modulation = 0x01,

        /// <summary>
        /// Breath Controller.
        /// </summary>
        BreathController = 0x02,

        // Undefined 0x03.

        /// <summary>
        /// Foot Controller.
        /// </summary>
        FootController = 0x04,

        /// <summary>
        /// Portamento Time.
        /// </summary>
        PortamentoTime = 0x05,

        /// <summary>
        /// Data Entry MSB.
        /// </summary>
        DataEntry = 0x06,

        /// <summary>
        /// Channel Volume (formerly Main Volume).
        /// </summary>
        ChannelVolume = 0x07,

        /// <summary>
        /// Balance controller.
        /// </summary>
        Balance = 0x08,

        // Undefined 0x09.

        /// <summary>
        /// Pan controller.
        /// </summary>
        Pan = 0x0A,

        /// <summary>
        /// Expression Controller.
        /// </summary>
        ExpressionController = 0x0B,

        /// <summary>
        /// Effect Control 1.
        /// </summary>
        EffectControl1 = 0x0C,

        /// <summary>
        /// Effect Control 2.
        /// </summary>
        EffectControl2 = 0x0D,

        // Undefined 0x0E.
        // Undefined 0x0F.

        /// <summary>
        /// General Purpose Controller 1.
        /// </summary>
        GeneralPurposeController1 = 0x10,

        /// <summary>
        /// General Purpose Controller 2.
        /// </summary>
        GeneralPurposeController2 = 0x11,

        /// <summary>
        /// General Purpose Controller 3.
        /// </summary>
        GeneralPurposeController3 = 0x12,

        /// <summary>
        /// General Purpose Controller 4.
        /// </summary>
        GeneralPurposeController4 = 0x13,

        // Undefined 0x14.
        // Undefined 0x15.
        // Undefined 0x16.
        // Undefined 0x17.
        // Undefined 0x18.
        // Undefined 0x19.
        // Undefined 0x1A.
        // Undefined 0x1B.
        // Undefined 0x1C.
        // Undefined 0x1D.
        // Undefined 0x1E.
        // Undefined 0x1F.

        /// <summary>
        /// LSB For Control 0 Bank Select.
        /// </summary>
        LSBForControl0 = 0x20,

        /// <summary>
        /// LSB For Control 1 Modulation Wheel or Lever.
        /// </summary>
        LSBForControl1 = 0x21,

        /// <summary>
        /// LSB For Control 2 Breath Controller.
        /// </summary>
        LSBForControl2 = 0x22,

        /// <summary>
        /// LSB For Control 3 Undefined.
        /// </summary>
        LSBForControl3 = 0x23,

        /// <summary>
        /// LSB For Control 4 Foot Controller.
        /// </summary>
        LSBForControl4 = 0x24,

        /// <summary>
        /// LSB For Control 5 Portamento Time.
        /// </summary>
        LSBForControl5 = 0x25,

        /// <summary>
        /// LSB For Control 6 LSB For Control 6 Data Entry.
        /// </summary>
        LSBForControl6 = 0x26,

        /// <summary>
        /// LSB For Control 7 Channel Volume, formerly Main Volume.
        /// </summary>
        LSBForControl7 = 0x27,

        /// <summary>
        /// LSB For Control 8 Balance.
        /// </summary>
        LSBForControl8 = 0x28,

        /// <summary>
        /// LSB For Control 9 Undefined.
        /// </summary>
        LSBForControl9 = 0x29,

        /// <summary>
        /// LSB For Control 10 Pan.
        /// </summary>
        LSBForControl10 = 0x2A,

        /// <summary>
        /// LSB For Control 11 Expression Controller.
        /// </summary>
        LSBForControl11 = 0x2B,

        /// <summary>
        /// LSB For Control 12 Effect control 1.
        /// </summary>
        LSBForControl12 = 0x2C,

        /// <summary>
        /// LSB For Control 13 Effect control 2.
        /// </summary>
        LSBForControl13 = 0x2D,

        /// <summary>
        /// LSB For Control 14 Undefined.
        /// </summary>
        LSBForControl14 = 0x2E,

        /// <summary>
        /// LSB For Control 15 Undefined.
        /// </summary>
        LSBForControl15 = 0x2F,

        /// <summary>
        /// LSB For Control 16 General Purpose Controller 1.
        /// </summary>
        LSBForControl16 = 0x30,

        /// <summary>
        /// LSB For Control 17 General Purpose Controller 2.
        /// </summary>
        LSBForControl17 = 0x31,

        /// <summary>
        /// LSB For Control 18 General Purpose Controller 3.
        /// </summary>
        LSBForControl18 = 0x32,

        /// <summary>
        /// LSB For Control 19 General Purpose Controller 4.
        /// </summary>
        LSBForControl19 = 0x33,

        /// <summary>
        /// LSB For Control 20 Undefined.
        /// </summary>
        LSBForControl20 = 0x34,

        /// <summary>
        /// LSB For Control 21 Undefined.
        /// </summary>
        LSBForControl21 = 0x35,

        /// <summary>
        /// LSB For Control 22 Undefined.
        /// </summary>
        LSBForControl22 = 0x36,

        /// <summary>
        /// LSB For Control 23 Undefined.
        /// </summary>
        LSBForControl23 = 0x37,

        /// <summary>
        /// LSB For Control 24 Undefined.
        /// </summary>
        LSBForControl24 = 0x38,

        /// <summary>
        /// LSB For Control 25 Undefined.
        /// </summary>
        LSBForControl25 = 0x39,

        /// <summary>
        /// LSB For Control 26 Undefined.
        /// </summary>
        LSBForControl26 = 0x3A,

        /// <summary>
        /// LSB For Control 27 Undefined.
        /// </summary>
        LSBForControl27 = 0x3B,

        /// <summary>
        /// LSB For Control 28 Undefined.
        /// </summary>
        LSBForControl28 = 0x3C,

        /// <summary>
        /// LSB For Control 29 Undefined.
        /// </summary>
        LSBForControl29 = 0x3D,

        /// <summary>
        /// LSB For Control 30 Undefined.
        /// </summary>
        LSBForControl30 = 0x3E,

        /// <summary>
        /// LSBFor Control 31 Undefined.
        /// </summary>
        LSBForControl31 = 0x3F,

        /// <summary>
        /// Sustain Damper-Pedal.
        /// </summary>
        DamperPedal = 0x40,

        /// <summary>
        /// Portamento On/Off.
        /// </summary>
        Portamento = 0x41,

        /// <summary>
        /// Sostenuto On/Off.
        /// </summary>
        Sostenuto = 0x42,

        /// <summary>
        /// Soft Pedal On/Off.
        /// </summary>
        SoftPedal = 0x43,

        /// <summary>
        /// Legato Footswitch.
        /// </summary>
        LegatoFootswitch = 0x44,

        /// <summary>
        /// Hold 2.
        /// </summary>
        Hold2 = 0x45,

        /// <summary>
        /// Sound Controller 1 default: Sound Variation.
        /// </summary>
        SoundController1 = 0x46,

        /// <summary>
        /// Sound Controller 2 default: Timbre/Harmonic Intens.
        /// </summary>
        SoundController2 = 0x47,

        /// <summary>
        /// default: Release Time.
        /// </summary>
        SoundController3 = 0x48,

        /// <summary>
        /// Sound Controller 4 default: Attack Time.
        /// </summary>
        SoundController4 = 0x49,

        /// <summary>
        /// Sound Controller 5 default: Brightness.
        /// </summary>
        SoundController5 = 0x4A,

        /// <summary>
        /// Sound Controller 6 default: Decay Time - see MMA RP-021.
        /// </summary>
        SoundController6 = 0x4B,

        /// <summary>
        /// Sound Controller 7 default: Vibrato Rate - see MMA RP-021.
        /// </summary>
        SoundController7 = 0x4C,

        /// <summary>
        /// Sound Controller 8 default: Vibrato Depth - see MMA RP-021.
        /// </summary>
        SoundController8 = 0x4D,

        /// <summary>
        /// Sound Controller 9 default: Vibrato Delay - see MMA RP-021.
        /// </summary>
        SoundController9 = 0x4E,

        /// <summary>
        /// Sound Controller 10 default undefined - see MMA RP-021.
        /// </summary>
        SoundController10 = 0x4F,

        /// <summary>
        /// General Purpose Controller 5.
        /// </summary>
        GeneralPurposeController5 = 0x50,

        /// <summary>
        /// General Purpose Controller 6.
        /// </summary>
        GeneralPurposeController6 = 0x51,

        /// <summary>
        /// General Purpose Controller 7.
        /// </summary>
        GeneralPurposeController7 = 0x52,

        /// <summary>
        /// General Purpose Controller 8.
        /// </summary>
        GeneralPurposeController8 = 0x53,

        /// <summary>
        /// Portamento Control.
        /// </summary>
        PortamentoControl = 0x54,

        // Undefined 0x55.
        // Undefined 0x56.
        // Undefined 0x57.

        /// <summary>
        /// High Resolution Velocity Prefix.
        /// </summary>
        HighResolutionVelocityPrefix = 0x58,

        // Undefined 0x59.
        // Undefined 0x5A.

        /// <summary>
        /// Effects Depth  1default: Reverb Send Level. - see MMA RP-023 formerly External Effects Depth.
        /// </summary>
        Effects1Depth = 0x5B,

        /// <summary>
        /// Effects Depth 2 formerly Tremolo Depth.
        /// </summary>
        Effects2Depth = 0x5C,

        /// <summary>
        /// Effects Depth 3 default: Chorus Send Level. - see MMA RP-023 formerly Chorus Depth.
        /// </summary>
        Effects3Depth = 0x5D,

        /// <summary>
        /// Effects Depth 4 formerly Celeste Detune Depth.
        /// </summary>
        Effects4Depth = 0x5E,

        /// <summary>
        /// Effects Depth 5 formerly Phaser Depth.
        /// </summary>
        Effects5Depth = 0x5F,

        /// <summary>
        /// Data Entry +1 see MMA RP-018.
        /// </summary>
        DataIncrement = 0x60,

        /// <summary>
        /// Data Entry -1 see MMA RP-018.
        /// </summary>
        DataDecrement = 0x61,

        /// <summary>
        /// Non Registered Parameter LSB.
        /// </summary>
        NonRegisteredParameterLSB = 0x62,

        /// <summary>
        /// Non Registered Parameter MSB.
        /// </summary>
        NonRegisteredParameterMSB = 0x63,

        /// <summary>
        /// Registered Parameter LSB.
        /// </summary>
        RegisteredParameterLSB = 0x64,

        /// <summary>
        /// Registered Parameter MSB.
        /// </summary>
        RegisteredParameterMSB = 0x65,

        // Undefined 0x66 - 0x77.

        // 0x78-0x7F Mode Messages.

        /// <summary>
        /// All Sound Off.
        /// </summary>
        AllSoundOff = 0x78,

        /// <summary>
        /// Reset all controllers.
        /// </summary>
        ResetAllControllers = 0x79,

        /// <summary>
        /// Local Control Mode On/Off.
        /// </summary>
        LocalControlMode = 0x7A,

        /// <summary>
        /// All notes off.
        /// </summary>
        AllNotesOff = 0x7B,

        /// <summary>
        /// Omni Mode Off + all notes off.
        /// </summary>
        OmniModeOff = 0x7C,

        /// <summary>
        /// Omni Mode On + all notes off.
        /// </summary>
        OmniModeOn = 0x7D,

        /// <summary>
        /// Poly mode off + all notes off.
        /// </summary>
        MonoModeOn = 0x7E,

        /// <summary>
        /// Mono mode off + all notes off.
        /// </summary>
        PolyModeOn = 0x7F,

        /// <summary>
        /// Unknown controller.
        /// </summary>
        Unknown
    }
}
