using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInReset function stops input on a given MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidParameter"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// This function returns all pending input buffers to the callback function and sets the MHDR_DONE flag in the dwFlags member of the MIDIHDR structure.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinreset
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInReset", ExactSpelling = true)]
        internal static extern MmResult MidiInReset_(IntPtr hMidiIn);

        /// <summary>
        /// Stops input on a given MIDI input device.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiInReset(IntPtr midiInputHandle)
        {
            return (MidiInReset_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
