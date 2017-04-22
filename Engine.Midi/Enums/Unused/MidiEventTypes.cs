// <copyright file="MidiEventTypes.cs" company="Shkyrockett">
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
    /// Midi Events.
    /// </summary>
    public enum MidiEventTypes
        : byte
    {
        /// <summary>
        /// No event.
        /// </summary>
        None,

        #region Channel Voice Messages

        /// <summary>
        /// 0x80 Note Off event.
        /// </summary>
        NoteOff = 0x80,

        /// <summary>
        /// 0x90 Note On event.
        /// </summary>
        NoteOn = 0x90,

        /// <summary>
        /// 0xA0 Polyphonic Key Pressure After-touch event.
        /// </summary>
        NoteKeyPressureAfterTouch = 0xA0,

        /// <summary>
        /// 0xB0 Control change event.
        /// </summary>
        ControlChange = 0xB0,

        /// <summary>
        /// 0xC0 Program change event.
        /// </summary>
        ProgramChange = 0xC0,

        /// <summary>
        /// 0xD0 Channel Preasure after-touch event.
        /// </summary>
        ChannelKeyPressure = 0xD0,

        /// <summary>
        /// 0xE0 Pitch wheel change event.
        /// </summary>
        PitchWheelChange = 0xE0,

        #endregion
        #region System Common Messages

        /// <summary>
        /// 0xF0 Sysex message event.
        /// </summary>
        Sysex = 0xF0,

        /// <summary>
        /// 0xF1 MIDI Time Code Quarter Frame event.
        /// </summary>
        TimeCode = 0xF1,

        /// <summary>
        /// 0xF2 Song Position Pointer event.
        /// </summary>
        SongPosition = 0xF2,

        /// <summary>
        /// 0xF3 Song Select event.
        /// </summary>
        SongSelect = 0xF3,

        /// <summary>
        /// 0xF6 Tune Request event.
        /// </summary>
        TuneRequest = 0xF6,

        /// <summary>
        /// 0xF7 End of Exclusive event.
        /// </summary>
        Eox = 0xF7,

        #endregion
        #region System Real-Time Messages

        /// <summary>
        /// 0xF8 Midi Sync Timing Clock event.
        /// </summary>
        TimingClock = 0xF8,

        /// <summary
        /// >0xF9 Tick Clock event.
        /// </summary>
        TimingTick = 0xF9,

        /// <summary>
        /// 0xFA Start sequence event.
        /// </summary>
        StartSequence = 0xFA,

        /// <summary>
        /// 0xFB Continue sequence event.
        /// </summary>
        ContinueSequence = 0xFB,

        /// <summary>
        /// 0xFC Stop sequence event event.
        /// </summary>
        StopSequence = 0xFC,

        /// <summary>
        /// 0xFE Active-Sensing event event.
        /// </summary>
        ActiveSensing = 0xFE,

        /// <summary>
        /// 0xFF Meta-event event event.
        /// </summary>
        MetaEvent = 0xFF, // Meta event.

        ///// <summary>
        ///// 0xFF System Reset event.
        ///// </summary>
        //SystemReset = 0xFF,

        #endregion
    }
}
