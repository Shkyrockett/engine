using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutClose function closes the specified MIDI output device.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. If the function is successful, the handle is no longer valid after the call to this function.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.StillPlaying"/> Buffers are still in the queue.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to load mapper string description.
        /// </returns>
        /// <remarks>
        /// If there are output buffers that have been sent by using the midiOutLongMsg function and have not been returned to the application, the close operation will fail. To mark all pending buffers as being done, use the midiOutReset function.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutclose
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutClose", ExactSpelling = true)]
        internal static extern MmResult MidiOutClose_(IntPtr hMidiOut);

        /// <summary>
        /// Midis the out close.
        /// </summary>
        /// <param name="midiOutputHandle">The midi output handle.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiOutClose(IntPtr midiOutputHandle)
        {
            return (MidiOutClose_(midiOutputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.StillPlaying => throw new Exception("Buffers are still in the queue."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to load mapper string description."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
