using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutGetErrorText function retrieves a textual description for an error identified by the specified error code.
        /// </summary>
        /// <param name="err">Error code.</param>
        /// <param name="lpText">Pointer to a buffer to be filled with the textual error description.</param>
        /// <param name="uSize">Length, in characters, of the buffer pointed to by lpText.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.BadErrorNumber"/> The specified error number is out of range.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutgeterrortext
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutGetErrorText", ExactSpelling = true)]
        internal static extern MmResult MidiOutGetErrorText(IntPtr err, StringBuilder lpText, int uSize);

        /// <summary>
        /// Retrieves a textual description for an error identified by the specified error code.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// The specified error number is out of range.
        /// or
        /// The specified pointer or structure is invalid.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MidiOutGetErrorText(IntPtr error)
        {
            var sb = new StringBuilder(MaxErrorLength);
            return MidiOutGetErrorText(error, sb, MaxErrorLength) switch
            {
                MmResult.NoError => sb.ToString().Trim(),
                MmResult.BadErrorNumber => throw new Exception("The specified error number is out of range."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
