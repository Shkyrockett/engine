internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// Options for the cache operation.
        /// </summary>
        public enum MidiCacheFlags
        {
            /// <summary>
            /// Caches all of the specified patches. If they cannot all be cached, it caches none, clears the KEYARRAY array, and returns MMSYSERR_NOMEM.
            /// </summary>
            MIDI_CACHE_ALL = 1,

            /// <summary>
            /// Caches all of the specified patches. If they cannot all be cached, it caches as many patches as possible, changes the KEYARRAY array to reflect which patches were cached, and returns MMSYSERR_NOMEM.
            /// </summary>
            MIDI_CACHE_BESTFIT = 2,

            /// <summary>
            /// Changes the KEYARRAY array to indicate which patches are currently cached.
            /// </summary>
            MIDI_CACHE_QUERY = 3,

            /// <summary>
            /// Uncaches the specified patches and clears the KEYARRAY array.
            /// </summary>
            MIDI_UNCACHE = 4
        }
    }
}
