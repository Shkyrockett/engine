using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutPrepareHeader function prepares a MIDI system-exclusive or stream buffer for output.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. To get the device handle, call midiOutOpen. This parameter can also be the handle of a MIDI stream cast to a HMIDIOUT type.</param>
        /// <param name="lpMidiOutHdr">
        /// Pointer to a MIDIHDR structure that identifies the buffer to be prepared.
        /// Before calling the function, set the lpData, dwBufferLength, and dwFlags members of the MIDIHDR structure. The dwFlags member must be set to zero.
        /// </param>
        /// <param name="uSize">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified address is invalid or the given stream buffer is greater than 64K.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// <para>Before you pass a MIDI data block to a device driver, you must prepare the buffer by passing it to the midiOutPrepareHeader function. After the header has been prepared, do not modify the buffer. After the driver is done using the buffer, call the midiOutUnprepareHeader function.
        /// The application can re-use the same buffer, or allocate multiple buffers and call midiOutPrepareHeader for each buffer. If you re-use the same buffer, it is not necessary to prepare the buffer each time. You can call midiOutPrepareHeader once at the beginning and then call midiOutUnprepareHeader once at the end.
        /// A stream buffer cannot be larger than 64K.
        /// Preparing a header that has already been prepared has no effect, and the function returns MMSYSERR_NOERROR.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutprepareheader
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutPrepareHeader", ExactSpelling = true)]
        private static extern MmResult MidiOutPrepareHeader(IntPtr hMidiOut, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiOutHdr, int uSize);
    }
}
