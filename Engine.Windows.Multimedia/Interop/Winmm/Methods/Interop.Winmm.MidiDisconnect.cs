using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiDisconnect function disconnects a MIDI input device from a MIDI thru or output device, or disconnects a MIDI thru device from a MIDI output device.
        /// </summary>
        /// <param name="hMidiIn">Handle to a MIDI input device or a MIDI thru device.</param>
        /// <param name="hMidiOut">Handle to the MIDI output device to be disconnected.</param>
        /// <param name="pReserved">Reserved; must be NULL.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following:
        /// <see cref="MmResult.InvalidHandle"/> Specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// MIDI input, output, and thru devices can be connected by using the midiConnect function. Thereafter, whenever the MIDI input device receives event data in an MIM_DATA message, a message with the same event data is sent to the output device driver (or through the thru driver to the output drivers).
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-mididisconnect
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiDisconnect", ExactSpelling = true)]
        private static extern MmResult MidiDisconnect(IntPtr hMidiIn, out IntPtr hMidiOut, IntPtr pReserved);
    }
}
