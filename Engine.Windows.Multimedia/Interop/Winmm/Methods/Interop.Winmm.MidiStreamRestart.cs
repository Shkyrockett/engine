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
        /// <para>Calling this function when the output is not paused has no effect, and the function returns MMSYSERR_NOERROR.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamrestart
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamRestart", ExactSpelling = true)]
        private static extern MmResult MidiStreamRestart_(IntPtr hMidiStream);
    }
}
