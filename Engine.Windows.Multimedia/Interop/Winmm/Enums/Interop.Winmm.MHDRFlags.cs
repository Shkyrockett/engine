using System;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// Flags giving information about the buffer.
        /// </summary>
        [Flags]
        public enum MHDRFlags
            : int
        {
            /// <summary>
            /// Set by the device driver to indicate that it is finished with the buffer and is returning it to the application. MHDR_DONE
            /// </summary>
            MHDR_DONE = 1,

            /// <summary>
            /// Set by Windows to indicate that the buffer has been prepared by using the midiInPrepareHeader or midiOutPrepareHeader function. MHDR_PREPARED
            /// </summary>
            MHDR_PREPARED = 2,

            /// <summary>
            /// Set by Windows to indicate that the buffer is queued for playback. MHDR_INQUEUE
            /// </summary>
            MHDR_INQUEUE = 4,

            /// <summary>
            /// Set to indicate that the buffer is a stream buffer. MHDR_ISSTRM
            /// </summary>
            MHDR_ISSTRM = 8,
        }
    }
}
