using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamProperty function sets or retrieves properties of a MIDI data stream associated with a MIDI output device.
        /// </summary>
        /// <param name="hMidiStream">Handle to the MIDI device that the property is associated with.</param>
        /// <param name="lppropdata">Pointer to the property data.</param>
        /// <param name="dwProperty">Flags that specify the action to perform and identify the appropriate property of the MIDI data stream. The midiStreamProperty function requires setting two flags in each use. One flag (either MIDIPROP_GET or MIDIPROP_SET) specifies an action, and the other identifies a specific property to examine or edit.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified handle is not a stream handle.
        /// <see cref="MmResult.InvalidParameter"/> The given handle or flags parameter is invalid.
        /// </returns>
        /// <remarks>
        /// <para>These properties are the default properties defined by the system. Driver writers can implement and document their own properties.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamproperty
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamProperty", ExactSpelling = true)]
        private static extern MmResult MidiStreamProperty_(IntPtr hMidiStream, IntPtr lppropdata, StreamPropertFlags dwProperty);
    }
}
