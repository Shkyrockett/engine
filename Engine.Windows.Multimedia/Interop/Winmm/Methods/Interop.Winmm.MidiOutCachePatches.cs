using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutCachePatches function requests that an internal MIDI synthesizer device preload and cache a specified set of patches.
        /// </summary>
        /// <param name="hMidiOut">Handle to the opened MIDI output device. This device must be an internal MIDI synthesizer. This parameter can also be the handle of a MIDI stream, cast to HMIDIOUT.</param>
        /// <param name="uBank">Bank of patches that should be used. This parameter should be set to zero to cache the default patch bank.</param>
        /// <param name="lpPatchArray">Pointer to a PATCHARRAY array indicating the patches to be cached or uncached.</param>
        /// <param name="uFlags">Options for the cache operation. It can be one of the following flags.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidFlag"/> The flag specified by wFlags is invalid.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The array pointed to by lpPatchArray is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The device does not have enough memory to cache all of the requested patches.
        /// <see cref="MmResult.NotSupported"/> The specified device does not support patch caching.
        /// </returns>
        /// <remarks>
        /// Some synthesizers are not capable of keeping all patches loaded simultaneously and must load data from disk when they receive MIDI program change messages. Caching patches ensures that the specified patches are immediately available.
        /// Each element of the PATCHARRAY array represents one of the 128 patches and has bits set for each of the 16 MIDI channels that use the particular patch. The least-significant bit represents physical channel 0, and the most-significant bit represents physical channel 15 (0x0F). For example, if patch 0 is used by physical channels 0 and 8, element 0 would be set to 0x0101.
        /// This function applies only to internal MIDI synthesizer devices. Not all internal synthesizers support patch caching. To see if a device supports patch caching, use the MIDICAPS_CACHE flag to test the dwSupport member of the MIDIOUTCAPS structure filled by the midiOutGetDevCaps function.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutcachepatches
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutCachePatches", ExactSpelling = true)]
        private static extern MmResult MidiOutCachePatches_(IntPtr hMidiOut, int uBank, IntPtr lpPatchArray, MidiCacheFlags uFlags);
    }
}
