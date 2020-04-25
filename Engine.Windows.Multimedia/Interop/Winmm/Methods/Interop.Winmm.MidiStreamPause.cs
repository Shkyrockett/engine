using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamPause function pauses playback of a specified MIDI stream.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream. This handle must have been returned by a call to the MIDIEVENT function. This handle identifies the output device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// The current playback position is saved when playback is paused. To resume playback from the current position, use the midiStreamRestart function.
        /// Calling this function when the output is already paused has no effect, and the function returns MMSYSERR_NOERROR.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreampause
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamPause", ExactSpelling = true)]
        internal static extern MmResult MidiStreamPause_(IntPtr hMidiStream);

        /// <summary>
        /// Pauses playback of a specified MIDI stream.
        /// </summary>
        /// <param name="midiStream">The midi stream.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiStreamPause(IntPtr midiStream)
        {
            return MidiStreamPause_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
