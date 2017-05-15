// <copyright file="MidiRegisreredParameterNumbers.cs" company="Shkyrockett">
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
    /// Registered Parameter Numbers
    /// </summary>
    public enum MidiRegisreredParameterNumbers
        : short
    {
        /// <summary>
        /// Pitch Bend Sensitivity.
        /// </summary>
        PitchBendSensitivity = 0x0000,

        /// <summary>
        /// Channel Fine Tuning
        /// (formerly Fine Tuning - see MMA RP-022).
        /// </summary>
        ChannelFineTuning = 0x0001,

        /// <summary>
        /// Channel Coarse Tuning
        /// (formerly Coarse Tuning - see MMA RP-022)
        /// </summary>
        ChannelCoarseTuning = 0x0002,

        /// <summary>
        /// Tuning Program Change.
        /// </summary>
        TuningProgramChange = 0x0003,

        /// <summary>
        /// Tuning Bank Select.
        /// </summary>
        TuningBankSelect = 0x0004,

        /// <summary>
        /// Modulation Depth Range 
        /// (see MMA General MIDI Level 2 Specification).
        /// </summary>
        ModulationDepthRange = 0x0005,

        /// <summary>
        /// Azimuth Angle. 
        /// </summary>
        AzimuthAngle = 0x3D00,

        /// <summary>
        /// Elevation Angle.
        /// </summary>
        ElevationAngle = 0x3D01,

        /// <summary>
        /// Gain.
        /// </summary>
        Gain = 0x3D02,

        /// <summary>
        /// Distance Ratio.
        /// </summary>
        DistanceRatio = 0x3D03,

        /// <summary>
        /// Maximum Distance 
        /// </summary>
        MaximumDistance = 0x3D04,

        /// <summary>
        /// Gain at Maximum Distance. 
        /// </summary>
        GainAtMaximumDistance = 0x3D05,

        /// <summary>
        /// Reference Distance Ratio. 
        /// </summary>
        ReferenceDistanceRatio = 0x3D06,

        /// <summary>
        /// Pan Spread Angle.
        /// </summary>
        PanSpreadAngle = 0x3D07,

        /// <summary>
        /// Roll Angle.
        /// </summary>
        RollAngle = 0x3D08,

        /// <summary>
        /// Null Function Number for RPN/NRPN 
        /// </summary>
        NullFunctionNumber = 0x7F7F,
    }
}
