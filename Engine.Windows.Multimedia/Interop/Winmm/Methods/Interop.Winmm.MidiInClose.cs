using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInClose function closes the specified MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device. If the function is successful, the handle is no longer valid after the call to this function.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.StillPlaying"/> Buffers are still in the queue.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// If there are input buffers that have been sent by using the midiInAddBuffer function and have not been returned to the application, the close operation will fail. To return all pending buffers through the callback function, use the midiInReset function.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinclose
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInClose", ExactSpelling = true)]
        internal static extern MmResult MidiInClose_(IntPtr hMidiIn);

        /// <summary>
        /// Midis the in close.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// The Midi buffer is still in the queue.
        /// or
        /// The specified device handle is invalid.
        /// or
        /// The system is unable to allocate or lock memory.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiInClose(IntPtr midiInputHandle)
        {
            return (MidiInClose_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
				MmResult.StillPlaying => throw new Exception("The Midi buffer is still in the queue."),
				MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
				MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
