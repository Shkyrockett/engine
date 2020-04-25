using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamClose function closes an open MIDI stream.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream, as retrieved by using the midiStreamOpen function.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamclose
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamClose", ExactSpelling = true)]
        internal static extern MmResult MidiStreamClose_(IntPtr hMidiStream);

        /// <summary>
        /// Closes an open MIDI stream.
        /// </summary>
        /// <param name="midiStream">The midi stream.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiStreamClose(IntPtr midiStream)
        {
            return MidiStreamClose_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
