using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutGetNumDevs function retrieves the number of MIDI output devices present in the system.
        /// </summary>
        /// <returns>
        /// Returns the number of MIDI output devices. A return value of zero means that there are no devices (not that there is no error).
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutgetnumdevs
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutGetNumDevs", ExactSpelling = true)]
        private static extern int MidiOutGetNumDevs_();
    }
}
