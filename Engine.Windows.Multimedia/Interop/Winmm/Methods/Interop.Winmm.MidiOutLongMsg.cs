using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutLongMsg function sends a system-exclusive MIDI message to the specified MIDI output device.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. This parameter can also be the handle of a MIDI stream cast to HMIDIOUT.</param>
        /// <param name="lpMidiOutHdr">Pointer to a MIDIHDR structure that identifies the MIDI buffer.</param>
        /// <param name="uSize">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.NotReady"/> The hardware is busy with other data.
        /// <see cref="MmResult.Unprepared"/> The buffer pointed to by lpMidiOutHdr has not been prepared.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// </returns>
        /// <remarks>
        /// Before the buffer is passed to midiOutLongMsg, it must be prepared by using the midiOutPrepareHeader function. The MIDI output device driver determines whether the data is sent synchronously or asynchronously.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutlongmsg
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutLongMsg", ExactSpelling = true)]
        private static extern MmResult MidiOutLongMsg(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiOutHdr, int uSize);
    }
}
