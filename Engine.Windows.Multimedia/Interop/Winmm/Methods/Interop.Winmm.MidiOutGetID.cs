using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiOutGetID function retrieves the device identifier for the given MIDI output device.
        /// This function is supported for backward compatibility. New applications can cast a handle of the device rather than retrieving the device identifier.
        /// </summary>
        /// <param name="hMidiOut">Handle to the MIDI output device.</param>
        /// <param name="lpuDeviceID">Pointer to a variable to be filled with the device identifier.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The hmo parameter specifies an invalid handle.
        /// <see cref="MmResult.NoDriver"/> No device driver is present.
        /// <see cref="MmResult.MemoryAllocationError"/> Unable to allocate or lock memory.
        /// </returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutgetid
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutGetID", ExactSpelling = true)]
        private static extern MmResult MidiOutGetID(IntPtr hMidiOut, out int lpuDeviceID);
    }
}
