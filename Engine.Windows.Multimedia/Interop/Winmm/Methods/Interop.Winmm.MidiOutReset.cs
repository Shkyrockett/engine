using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutReset function turns off all notes on all MIDI channels for the specified MIDI output device.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. This parameter can also be the handle of a MIDI stream cast to HMIDIOUT.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// <para>Any pending system-exclusive or stream output buffers are returned to the callback function and the MHDR_DONE flag is set in the dwFlags member of the MIDIHDR structure.
        /// Terminating a system-exclusive message without sending an EOX (end-of-exclusive) byte might cause problems for the receiving device. The midiOutReset function does not send an EOX byte when it terminates a system-exclusive message applications are responsible for doing this.
        /// To turn off all notes, a note-off message for each note in each channel is sent. In addition, the sustain controller is turned off for each channel.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutreset
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutReset", ExactSpelling = true)]
        private static extern MmResult MidiOutReset_(IntPtr hMidiOut);
    }
}
