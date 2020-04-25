﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInUnprepareHeader function cleans up the preparation performed by the midiInPrepareHeader function.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <param name="lpMidiInHdr">Pointer to a MIDIHDR structure identifying the buffer to be cleaned up.</param>
        /// <param name="uSize">Size of the MIDIHDR structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.StillPlaying"/> The buffer pointed to by lpMidiInHdr is still in the queue.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// This function is complementary to midiInPrepareHeader. You must use this function before freeing the buffer. After passing a buffer to the device driver by using the midiInAddBuffer function, you must wait until the driver is finished with the buffer before using midiInUnprepareHeader. Unpreparing a buffer that has not been prepared has no effect, and the function returns MMSYSERR_NOERROR.
        /// </remarks>
		/// <acknowledgment>
		/// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinunprepareheader
		/// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInUnprepareHeader", ExactSpelling = true)]
        internal static extern MmResult MidiInUnprepareHeader(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiInHdr, int uSize);

        /// <summary>
        /// Midis the in unprepare header.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <param name="midiInHeader">The midi in header.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// The specified device handle is invalid.
        /// or
        /// The specified pointer or structure is invalid.
        /// or
        /// The system is unable to allocate or lock memory.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MIDIHDR MidiInUnprepareHeader(IntPtr midiInputHandle, MIDIHDR midiInHeader)
        {
			return (MidiInPrepareHeader(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
			{
				MmResult.NoError => midiInHeader,
				MmResult.StillPlaying => throw new Exception("The buffer pointed to by lpMidiInHdr is still in the queue."),
				MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
				MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
				_ => throw new Exception("Unspecified Error"),
			};
        }
    }
}
