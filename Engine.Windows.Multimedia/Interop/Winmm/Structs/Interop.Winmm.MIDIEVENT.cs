using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MIDIEVENT
        {
            /// <summary>
            /// The dw delta time
            /// </summary>
            public int dwDeltaTime;

            /// <summary>
            /// The dw stream identifier
            /// </summary>
            public int dwStreamID;

            /// <summary>
            /// The dw event
            /// </summary>
            public int dwEvent;

            /// <summary>
            /// The dw parms
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public int dwParms;
        }
    }
}
