using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInGetErrorText function retrieves a textual description for an error identified by the specified error code.
        /// </summary>
        /// <param name="err">Error code.</param>
        /// <param name="lpText">Pointer to the buffer to be filled with the textual error description.</param>
        /// <param name="uSize">Length, in characters, of the buffer pointed to by lpText.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.BadDeviceId"/> The specified error number is out of range.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// If the textual error description is longer than the specified buffer, the description is truncated. The returned error string is always null-terminated. If cchText is zero, nothing is copied, and the function returns zero. All error descriptions are less than MAXERRORLENGTH characters long.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiingeterrortext
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInGetErrorText", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern MmResult MidiInGetErrorText(int err, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpText, int uSize);
    }
}
