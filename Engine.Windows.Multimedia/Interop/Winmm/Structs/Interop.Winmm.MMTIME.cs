using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <acknowledgment>
        /// https://docs.microsoft.com/previous-versions//dd757347(v=vs.85)
        /// </acknowledgment>
        [StructLayout(LayoutKind.Sequential)]
        public struct MMTIME
        {
            /// <summary>
            /// The w type
            /// </summary>
            public int wType;

            /// <summary>
            /// The u
            /// </summary>
            public int u;
        }
    }
}
