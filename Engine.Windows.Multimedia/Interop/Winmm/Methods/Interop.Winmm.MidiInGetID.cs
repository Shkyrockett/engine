using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInGetID function gets the device identifier for the given MIDI input device.
        /// This function is supported for backward compatibility. New applications can cast a handle of the device rather than retrieving the device identifier.
        /// </summary>
        /// <param name="hMidiIn">Handle to the MIDI input device.</param>
        /// <param name="lpuDeviceId">Pointer to a variable to be filled with the device identifier.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The hwi parameter specifies an invalid handle.
        /// <see cref="MmResult.NoDriver"/> No device driver is present.
        /// <see cref="MmResult.MemoryAllocationError"/> Unable to allocate or lock memory.
        /// </returns>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiconnect
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInGetID", ExactSpelling = true)]
        internal static extern MmResult MidiInGetID(IntPtr hMidiIn, out int lpuDeviceId);

        /// <summary>
        /// Gets the device identifier for the given MIDI input device.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The Midi Device is not ready to connect to.
        /// or
        /// An invalid handle was provided to connect to a MIDI device.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MidiInGetID(IntPtr midiInputHandle)
        {
            return (MidiInGetID(midiInputHandle, out var deviceID)) switch
            {
                MmResult.NoError => deviceID,
                MmResult.InvalidHandle => throw new Exception("An invalid handle was provided to connect to a MIDI device."),
                MmResult.NoDriver => throw new Exception("No device driver is present."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
