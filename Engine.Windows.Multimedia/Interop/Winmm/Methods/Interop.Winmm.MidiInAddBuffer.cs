using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInAddBuffer function sends an input buffer to a specified opened MIDI input device. This function is used for system-exclusive messages.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <param name="lpMidiInHdr">Pointer to a MIDIHDR structure that identifies the buffer.</param>
        /// <param name="uSize">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.StillPlaying"/> The buffer pointed to by lpMidiInHdr is still in the queue.
        /// <see cref="MmResult.Unprepared"/> The buffer pointed to by lpMidiInHdr has not been prepared.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// When the buffer is filled, it is sent back to the application.
        /// The buffer must be prepared by using the midiInPrepareHeader function before it is passed to the midiInAddBuffer function.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/en-us/windows/win32/api/mmeapi/nf-mmeapi-midiinaddbuffer
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInAddBuffer", ExactSpelling = true)]
        private static extern MmResult MidiInAddBuffer(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiInHdr, int uSize);
    }
}
