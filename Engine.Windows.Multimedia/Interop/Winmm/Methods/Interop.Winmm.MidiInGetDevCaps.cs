using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInGetDevCaps function determines the capabilities of a specified MIDI input device.
        /// </summary>
        /// <param name="deviceId">Identifier of the MIDI input device. The device identifier varies from zero to one less than the number of devices present. This parameter can also be a properly cast device handle.</param>
        /// <param name="capabilities">Pointer to a MIDIINCAPS structure that is filled with information about the capabilities of the device.</param>
        /// <param name="size">Size, in bytes, of the MIDIINCAPS structure. Only cbMidiInCaps bytes (or less) of information is copied to the location pointed to by lpMidiInCaps. If cbMidiInCaps is zero, nothing is copied, and the function returns MMSYSERR_NOERROR.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.BadDeviceId"/> The specified device identifier is out of range.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.NoDriver"/> The driver is not installed.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// <para>To determine the number of MIDI input devices present on the system, use the midiInGetNumDevs function.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/en-us/windows/win32/api/mmeapi/nf-mmeapi-midiingetdevcaps
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInGetDevCaps", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern MmResult MidiInGetDevCaps(IntPtr deviceId, out MidiInCapabilities capabilities, int size);
    }
}
