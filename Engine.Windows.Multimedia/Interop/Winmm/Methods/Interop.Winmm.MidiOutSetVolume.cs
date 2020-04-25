using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutSetVolume function sets the volume of a MIDI output device.
        /// </summary>
        /// <param name="hMidiOut">Handle to an open MIDI output device. This parameter can also contain the handle of a MIDI stream, as long as it is cast to HMIDIOUT. This parameter can also be a device identifier.</param>
        /// <param name="dwVolume">
        /// New volume setting. The low-order word contains the left-channel volume setting, and the high-order word contains the right-channel setting. A value of 0xFFFF represents full volume, and a value of 0x0000 is silence.
        /// If a device does not support both left and right volume control, the low-order word of dwVolume specifies the mono volume level, and the high-order word is ignored.
        /// </param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// <see cref="MmResult.NotSupported"/> The function is not supported.
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutsetvolume
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutSetVolume", ExactSpelling = true)]
        internal static extern MmResult MidiOutSetVolume(IntPtr hMidiOut, int dwVolume);

        /// <summary>
        /// Sets the volume of a MIDI output device.
        /// </summary>
        /// <param name="midiOutputHandle">The midi output handle.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The specified device handle is invalid.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiOutSetVolume(IntPtr midiOutputHandle, short left, short right)
        {
            return MidiOutSetVolume(midiOutputHandle, right & 0x0000FFFF | left << 16) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.NotSupported => throw new Exception("The function is not supported."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
