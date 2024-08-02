internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// Flags that specify the action to perform and identify the appropriate property of the MIDI data stream. The midiStreamProperty function requires setting two flags in each use. One flag (either MIDIPROP_GET or MIDIPROP_SET) specifies an action, and the other identifies a specific property to examine or edit.
        /// </summary>
        [Flags]
        public enum StreamPropertFlags
            : uint
        {
            /// <summary>
            /// Retrieves the tempo property.The lppropdata parameter points to a MIDIPROPTEMPO structure.The current tempo value can be retrieved at any time.Output devices set the tempo by inserting MEVT_TEMPO events into the MIDI data.
            /// </summary>
            MIDIPROP_TEMPO = 0x00000002,

            /// <summary>
            /// Specifies the time division property. You can get or set this property.The lppropdata parameter points to a MIDIPROPTIMEDIV structure. This property can be set only when the device is stopped.
            /// </summary>
            MIDIPROP_TIMEDIV = 0x00000001,

            /// <summary>
            /// Retrieves the current setting of the given property.
            /// </summary>
            MIDIPROP_GET = 0x40000000,

            /// <summary>
            /// Sets the given property.
            /// </summary>
            MIDIPROP_SET = 0x80000000,
        }
    }
}
