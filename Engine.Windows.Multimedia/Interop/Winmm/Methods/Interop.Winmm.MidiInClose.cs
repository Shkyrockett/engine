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
        /// <para>If there are input buffers that have been sent by using the midiInAddBuffer function and have not been returned to the application, the close operation will fail. To return all pending buffers through the callback function, use the midiInReset function.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinclose
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInClose", ExactSpelling = true)]
        private static extern MmResult MidiInClose_(IntPtr hMidiIn);
    }
}
