using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamOut function plays or queues a stream (buffer) of MIDI data to a MIDI output device.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream. This handle must have been returned by a call to the midiStreamOpen function. This handle identifies the output device.</param>
        /// <param name="pmh">Pointer to a MIDIHDR structure that identifies the MIDI buffer.</param>
        /// <param name="cbmh">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// <see cref="MmResult.StillPlaying"/> The output buffer pointed to by lpMidiHdr is still playing or is queued from a previous call to midiStreamOut.
        /// <see cref="MmResult.Unprepared"/> The header pointed to by lpMidiHdr has not been prepared.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/>  The pointer specified by lpMidiHdr is invalid.
        /// </returns>
        /// <remarks>
        /// Before the buffer is passed to midiStreamOpen, it must be prepared by using the midiOutPrepareHeader function.
        /// Because the midiStreamOpen function opens the output device in paused mode, you must call the midiStreamRestart function before you can use midiStreamOut to start the playback.
        /// For the current implementation of this function, the buffer must be smaller than 64K.
        /// The buffer pointed to by the MIDIHDR structure contains one or more MIDI events, each of which is defined by a MIDIEVENT structure.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamout
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamOut", ExactSpelling = true)]
        private static extern MmResult MidiStreamOut(IntPtr hMidiStream, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR pmh, int cbmh);
    }
}
