using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInStart function starts MIDI input on the specified MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following
        /// <see cref="MmResult.InvalidParameter"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// <para>This function resets the time stamp to zero; time stamp values for subsequently received messages are relative to the time that this function was called.
        /// All messages except system-exclusive messages are sent directly to the client when they are received. System-exclusive messages are placed in the buffers supplied by the midiInAddBuffer function. If there are no buffers in the queue, the system-exclusive data is thrown away without notification to the client and input continues. Buffers are returned to the client when they are full, when a complete system-exclusive message has been received, or when the midiInReset function is used. The dwBytesRecorded member of the MIDIHDR structure will contain the actual length of data received.
        /// Calling this function when input is already started has no effect, and the function returns zero.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinstart
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInStart", ExactSpelling = true)]
        private static extern MmResult MidiInStart_(IntPtr hMidiIn);
    }
}
