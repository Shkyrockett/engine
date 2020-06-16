using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutShortMsg function sends a short MIDI message to the specified MIDI output device.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device. This parameter can also be the handle of a MIDI stream cast to HMIDIOUT.</param>
        /// <param name="dwMsg">
        /// MIDI message. The message is packed into a DWORD value with the first byte of the message in the low-order byte. The message is packed into this parameter as follows.
        /// The two MIDI data bytes are optional, depending on the MIDI status byte. When a series of messages have the same status byte, the status byte can be omitted from messages after the first one in the series, creating a running status. Pack a message for running status as follows:
        /// </param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following:
        /// <see cref="MmResult.BadOpenMode"/> The application sent a message without a status byte to a stream handle.
        /// <see cref="MmResult.NotReady"/> The hardware is busy with other data.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// <para>This function is used to send any MIDI message except for system-exclusive or stream messages.
        /// This function might not return until the message has been sent to the output device. You can send short messages while streams are playing on the same device (although you cannot use a running status in this case).</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutshortmsg
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutShortMsg", ExactSpelling = true)]
        private static extern MmResult MidiOutShortMsg_(IntPtr hMidiOut, int dwMsg);
    }
}
