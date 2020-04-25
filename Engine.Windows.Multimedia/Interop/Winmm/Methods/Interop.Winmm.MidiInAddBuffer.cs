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
		internal static extern MmResult MidiInAddBuffer(IntPtr hMidiIn, [MarshalAs(UnmanagedType.Struct)] ref MIDIHDR lpMidiInHdr, int uSize);

		/// <summary>
		/// Sends an input buffer to a specified opened MIDI input device.
		/// </summary>
		/// <param name="midiInputHandle">The midi input handle.</param>
		/// <param name="midiInHeader">The midi in header.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">
		/// The Midi buffer is still in the queue.
		/// or
		/// The Midi buffer has not been prepared.
		/// or
		/// The specified device handle is invalid.
		/// or
		/// The specified pointer or structure is invalid.
		/// or
		/// The system is unable to allocate or lock memory.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool MidiInAddBuffer(IntPtr midiInputHandle, MIDIHDR midiInHeader)
		{
			return (MidiInAddBuffer(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
			{
				MmResult.NoError => true,
				MmResult.StillPlaying => throw new Exception("The Midi buffer is still in the queue."),
				MmResult.Unprepared => throw new Exception("The Midi buffer has not been prepared."),
				MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
				MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
				MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
				_ => throw new Exception("Unspecified Error"),
			};
		}
	}
}
