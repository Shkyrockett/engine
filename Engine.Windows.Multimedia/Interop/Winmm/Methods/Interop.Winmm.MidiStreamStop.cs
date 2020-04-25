using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamStop function turns off all notes on all MIDI channels for the specified MIDI output device.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream. This handle must have been returned by a call to the midiStreamOpen function. This handle identifies the output device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// When you call this function, any pending system-exclusive or stream output buffers are returned to the callback mechanism and the MHDR_DONE bit is set in the dwFlags member of the MIDIHDR structure.
        /// While the midiOutReset function turns off all notes, midiStreamStop turns off only those notes that have been turned on by a MIDI note-on message.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamstop
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamStop", ExactSpelling = true)]
        internal static extern MmResult MidiStreamStop_(IntPtr hMidiStream);

        /// <summary>
        /// Midis the stream stop.
        /// </summary>
        /// <param name="midiStream">The midi stream.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiStreamStop(IntPtr midiStream)
        {
            return MidiStreamStop_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
