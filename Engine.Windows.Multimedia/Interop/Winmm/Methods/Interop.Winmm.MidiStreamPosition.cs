using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamPosition function retrieves the current position in a MIDI stream.
        /// </summary>
        /// <param name="hMidiStream">Handle to a MIDI stream. This handle must have been returned by a call to the midiStreamOpen function. This handle identifies the output device.</param>
        /// <param name="lpmmt">Pointer to an MMTIME structure.</param>
        /// <param name="cbmmt">Size, in bytes, of the MMTIME structure.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> Specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> Specified pointer or structure is invalid.
        /// </returns>
        /// <remarks>
        /// Before calling midiStreamPosition, set the wType member of the MMTIME structure to indicate the time format you desire. After calling midiStreamPosition, check the wType member to determine if the desired time format is supported. If the desired format is not supported, wType will specify an alternative format.
        /// The position is set to zero when the device is opened or reset.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamposition
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamPosition", ExactSpelling = true)]
        private static extern MmResult MidiStreamPosition(IntPtr hMidiStream, [MarshalAs(UnmanagedType.Struct)] ref MMTIME lpmmt, int cbmmt);
    }
}
