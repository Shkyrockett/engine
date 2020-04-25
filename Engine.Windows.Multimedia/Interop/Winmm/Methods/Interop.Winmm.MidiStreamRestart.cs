using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamRestart function restarts a paused MIDI stream.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream. This handle must have been returned by a call to the midiStreamOpen function. This handle identifies the output device.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidParameter"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// Calling this function when the output is not paused has no effect, and the function returns MMSYSERR_NOERROR.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamrestart
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamRestart", ExactSpelling = true)]
        internal static extern MmResult MidiStreamRestart_(IntPtr hMidiStream);

        /// <summary>
        /// Restarts a paused MIDI stream.
        /// </summary>
        /// <param name="midiStream">The midi stream.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiStreamRestart(IntPtr midiStream)
        {
            return MidiStreamRestart_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
