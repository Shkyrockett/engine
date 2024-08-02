using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutUnprepareHeader function cleans up the preparation performed by the midiOutPrepareHeader function.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. This parameter can also be the handle of a MIDI stream cast to HMIDIOUT.</param>
        /// <param name="lpMidiOutHdr">Pointer to a MIDIHDR structure identifying the buffer to be cleaned up.</param>
        /// <param name="uSize">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.StillPlaying"/> The buffer pointed to by lpMidiOutHdr is still in the queue.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// </returns>
        /// <remarks>
        /// <para>This function is complementary to the midiOutPrepareHeader function. You must call midiOutUnprepareHeader before freeing the buffer. After passing a buffer to the device driver with the midiOutLongMsg function, you must wait until the device driver is finished with the buffer before calling midiOutUnprepareHeader.
        /// Unpreparing a buffer that has not been prepared has no effect, and the function returns MMSYSERR_NOERROR.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutunprepareheader
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutUnprepareHeader", ExactSpelling = true)]
        private static extern MmResult MidiOutUnprepareHeader(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiOutHdr, int uSize);
    }
}
