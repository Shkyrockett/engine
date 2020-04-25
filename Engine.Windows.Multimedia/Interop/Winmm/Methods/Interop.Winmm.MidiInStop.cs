using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInStop function stops MIDI input on the specified MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidParameter"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// If there are any system-exclusive messages or stream buffers in the queue, the current buffer is marked as done (the dwBytesRecorded member of the MIDIHDR structure will contain the actual length of data), but any empty buffers in the queue remain there and are not marked as done.
        /// Calling this function when input is not started has no effect, and the function returns zero.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinstop
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInStop", ExactSpelling = true)]
        internal static extern MmResult MidiInStop_(IntPtr hMidiIn);

        /// <summary>
        /// Midis the in stop.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiInStop(IntPtr midiInputHandle)
        {
            return (MidiInStop_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
