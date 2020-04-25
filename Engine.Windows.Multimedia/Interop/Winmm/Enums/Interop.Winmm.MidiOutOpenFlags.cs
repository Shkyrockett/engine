using System;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// Callback flag for opening the device and, optionally, a status flag that helps regulate rapid data transfers.
        /// </summary>
        [Flags]
        public enum MidiOutOpenFlags
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
        }
    }
}
