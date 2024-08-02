using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamOpen function opens a MIDI stream for output. By default, the device is opened in paused mode. The stream handle retrieved by this function must be used in all subsequent references to the stream.
        /// </summary>
        /// <param name="hMidiStream">Pointer to a variable to contain the stream handle when the function returns.</param>
        /// <param name="puDeviceID">Pointer to a device identifier. The device is opened on behalf of the stream and closed again when the stream is closed.</param>
        /// <param name="cMidi">Reserved; must be 1.</param>
        /// <param name="dwCallback">Pointer to a callback function, an event handle, a thread identifier, or a handle of a window or thread called during MIDI playback to process messages related to the progress of the playback. If no callback mechanism is desired, specify NULL for this parameter.</param>
        /// <param name="dwInstance">Application-specific instance data that is returned to the application with every callback function.</param>
        /// <param name="fdwOpen">Callback flag for opening the device. One of the following callback flags must be specified.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.BadDeviceId"/> The specified device identifier is out of range.
        /// <see cref="MmResult.InvalidHandle"/> The given handle or flags parameter is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamopen
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamOpen", ExactSpelling = true)]
        private static extern MmResult MidiStreamOpen(out IntPtr hMidiStream, IntPtr puDeviceID, int cMidi, IntPtr dwCallback, IntPtr dwInstance, MidiOutOpenFlags fdwOpen);
    }
}
