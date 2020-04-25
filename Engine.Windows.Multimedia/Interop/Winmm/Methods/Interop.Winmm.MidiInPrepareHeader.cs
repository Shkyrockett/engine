using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInPrepareHeader function prepares a buffer for MIDI input.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device. To get the device handle, call midiInOpen.</param>
        /// <param name="lpMidiInHdr">
        /// Pointer to a MIDIHDR structure that identifies the buffer to be prepared.
        /// Before calling the function, set the lpData, dwBufferLength, and dwFlags members of the MIDIHDR structure. The dwFlags member must be set to zero.
        /// </param>
        /// <param name="uSize">Size, in bytes, of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified address is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// Before you pass a MIDI data block to a device driver, you must prepare the buffer by passing it to the midiInPrepareHeader function. After the header has been prepared, do not modify the buffer. After the driver is done using the buffer, call the midiInUnprepareHeader function.
        /// The application can re-use the same buffer, or allocate multiple buffers and call midiInPrepareHeader for each buffer. If you re-use the same buffer, it is not necessary to prepare the buffer each time. You can call midiInPrepareHeader once at the beginning and then call midiInUnprepareHeader once at the end.
        /// Preparing a header that has already been prepared has no effect, and the function returns zero.
        /// </remarks>
		/// <acknowledgment>
		/// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinprepareheader
		/// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInPrepareHeader", ExactSpelling = true)]
        internal static extern MmResult MidiInPrepareHeader(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiInHdr, int uSize);

        /// <summary>
        /// Midis the in prepare header.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <param name="midiInHeader">The midi in header.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The Midi buffer is still in the queue.
        /// or
        /// The Midi buffer has not been prepared.
        /// or
        /// The specified device handle is invalid.
        /// or
        /// The specified pointer or structure is invalid.
        /// or
        /// The system is unable to allocate or lock memory.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MIDIHDR MidiInPrepareHeader(IntPtr midiInputHandle, MIDIHDR midiInHeader)
        {
			return (MidiInPrepareHeader(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
			{
				MmResult.NoError => midiInHeader,
				MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
				MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
				MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
				_ => throw new Exception("Unspecified Error"),
			};
		}
	}
}
