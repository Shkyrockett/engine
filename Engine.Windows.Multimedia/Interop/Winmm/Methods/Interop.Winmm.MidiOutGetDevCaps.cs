using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutGetDevCaps function queries a specified MIDI output device to determine its capabilities.
        /// </summary>
        /// <param name="deviceNumber">Identifier of the MIDI output device. The device identifier specified by this parameter varies from zero to one less than the number of devices present. The MIDI_MAPPER constant is also a valid device identifier.
        /// This parameter can also be a properly cast device handle.</param>
        /// <param name="caps">Pointer to a MIDIOUTCAPS structure. This structure is filled with information about the capabilities of the device.</param>
        /// <param name="uSize">Size, in bytes, of the MIDIOUTCAPS structure. Only cbMidiOutCaps bytes (or less) of information is copied to the location pointed to by lpMidiOutCaps. If cbMidiOutCaps is zero, nothing is copied, and the function returns MMSYSERR_NOERROR.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.BadDeviceId"/> The specified device identifier is out of range.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.NoDriver"/> The driver is not installed.
        /// <see cref="MmResult.MemoryAllocationError"/>MMSYSERR_NOMEM The system is unable to load mapper string description.
        /// </returns>
        /// <remarks>
        /// To determine the number of MIDI output devices present in the system, use the midiOutGetNumDevs function.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutgetdevcaps
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutGetDevCaps", ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern MmResult MidiOutGetDevCaps(IntPtr deviceNumber, out MidiOutCapabilities caps, int uSize);

        /// <summary>
        /// Midis the out get dev caps.
        /// </summary>
        /// <param name="midiOutDeviceNumber">The midi out device number.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MidiOutCapabilities MidiOutGetDevCaps(int midiOutDeviceNumber)
        {
            var caps = new MidiOutCapabilities();
            var structSize = Marshal.SizeOf(caps);
            return MidiOutGetDevCaps((IntPtr)midiOutDeviceNumber, out caps, structSize) switch
            {
                MmResult.NoError => caps,
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.NoDriver => throw new Exception("The driver is not installed."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
