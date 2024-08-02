using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutGetVolume function retrieves the current volume setting of a MIDI output device.
        /// </summary>
        /// <param name="uDeviceID">Handle to an open MIDI output device. This parameter can also contain the handle of a MIDI stream, as long as it is cast to HMIDIOUT. This parameter can also be a device identifier.</param>
        /// <param name="lpdwVolume">
        /// Pointer to the location to contain the current volume setting. The low-order word of this location contains the left-channel volume setting, and the high-order word contains the right-channel setting. A value of 0xFFFF represents full volume, and a value of 0x0000 is silence.
        /// If a device does not support both left and right volume control, the low-order word of the specified location contains the mono volume level.
        /// Any value set by using the midiOutSetVolume function is returned, regardless of whether the device supports that value.
        /// </param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified device handle is invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// <see cref="MmResult.NotSupported"/> The function is not supported.
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutgetvolume
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutGetVolume", ExactSpelling = true)]
        private static extern MmResult MidiOutGetVolume(IntPtr uDeviceID, out int lpdwVolume);
    }
}
