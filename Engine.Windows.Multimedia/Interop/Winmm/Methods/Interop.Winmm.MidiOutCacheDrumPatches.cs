using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutCacheDrumPatches function requests that an internal MIDI synthesizer device preload and cache a specified set of key-based percussion patches.
        /// </summary>
        /// <param name="hMidiOut">Handle to the opened MIDI output device. This device should be an internal MIDI synthesizer. This parameter can also be the handle of a MIDI stream, cast to HMIDIOUT.</param>
        /// <param name="uPatch">Drum patch number that should be used. This parameter should be set to zero to cache the default drum patch.</param>
        /// <param name="lpKeyArray">Pointer to a KEYARRAY array indicating the key numbers of the specified percussion patches to be cached or uncached.</param>
        /// <param name="uFlags">Options for the cache operation.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidFlag"/> The flag specified by wFlags is invalid.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The array pointed to by the lpKeyArray array is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The device does not have enough memory to cache all of the requested patches.
        /// <see cref="MmResult.NotSupported"/> The specified device does not support patch caching.
        /// </returns>
        /// <remarks>
        /// <para>Some synthesizers are not capable of keeping all percussion patches loaded simultaneously. Caching patches ensures that the specified patches are available.
        /// Each element of the KEYARRAY array represents one of the 128 key-based percussion patches and has bits set for each of the 16 MIDI channels that use the particular patch. The least-significant bit represents physical channel 0, and the most-significant bit represents physical channel 15. For example, if the patch on key number 60 is used by physical channels 9 and 15, element 60 would be set to 0x8200.
        /// This function applies only to internal MIDI synthesizer devices. Not all internal synthesizers support patch caching. To see if a device supports patch caching, use the MIDICAPS_CACHE flag to test the dwSupport member of the MIDIOUTCAPS structure filled by the midiOutGetDevCaps function.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutcachedrumpatches
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutCacheDrumPatches", ExactSpelling = true)]
        private static extern MmResult MidiOutCacheDrumPatches_(IntPtr hMidiOut, int uPatch, IntPtr lpKeyArray, MidiCacheFlags uFlags);
    }
}
