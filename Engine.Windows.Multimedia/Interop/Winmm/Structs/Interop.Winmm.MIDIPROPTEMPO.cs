using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIPROPTEMPO
        {
            /// <summary>
            /// The cb structure
            /// </summary>
            public int cbStruct;

            /// <summary>
            /// The dw tempo
            /// </summary>
            public int dwTempo;
        }
    }
}
