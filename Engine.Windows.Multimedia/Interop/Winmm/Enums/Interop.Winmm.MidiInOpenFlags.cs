internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// Callback flag for opening the device and, optionally, a status flag that helps regulate rapid data transfers.
        /// </summary>
        [Flags]
        public enum MidiInOpenFlags
            : uint
        {
            /// <summary>
            /// There is no callback mechanism. This value is the default setting.
            /// </summary>
            CALLBACK_NULL = 0x00000,

            /// <summary>
            /// The dwCallback parameter is a callback procedure address.
            /// </summary>
            CALLBACK_FUNCTION = 0x30000,

            /// <summary>
            /// The callback event
            /// </summary>
            CALLBACK_EVENT = 0x50000,

            /// <summary>
            /// The dwCallback parameter is a window handle.
            /// </summary>
            CALLBACK_WINDOW = 0x10000,

            /// <summary>
            /// The dwCallback parameter is a thread identifier.
            /// </summary>
            CALLBACK_THREAD = 0x20000,

            /// <summary>
            /// When this parameter also specifies CALLBACK_FUNCTION, MIM_MOREDATA messages are sent to the callback function as well as MIM_DATA messages. Or, if this parameter also specifies CALLBACK_WINDOW, MM_MIM_MOREDATA messages are sent to the window as well as MM_MIM_DATA messages. This flag does not affect event or thread callbacks.
            /// </summary>
            MIDI_IO_STATUS = 0x20,
        }
    }
}
