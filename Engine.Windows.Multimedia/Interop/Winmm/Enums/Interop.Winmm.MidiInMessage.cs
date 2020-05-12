internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        public enum MidiInMessages
        {
            /// <summary>
            /// MIM_OPEN
            /// </summary>
            Open = 0x3C1,

            /// <summary>
            /// MIM_CLOSE
            /// </summary>
            Close = 0x3C2,

            /// <summary>
            /// MIM_DATA
            /// </summary>
            Data = 0x3C3,

            /// <summary>
            /// MIM_LONGDATA
            /// </summary>
            LongData = 0x3C4,

            /// <summary>
            /// MIM_ERROR
            /// </summary>
            Error = 0x3C5,

            /// <summary>
            /// MIM_LONGERROR
            /// </summary>
            LongError = 0x3C6,

            /// <summary>
            /// MIM_MOREDATA
            /// </summary>
            MoreData = 0x3CC,
        }
    }
}
