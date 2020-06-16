using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiConnect function connects a MIDI input device to a MIDI thru or output device, or connects a MIDI thru device to a MIDI output device.
        /// </summary>
        /// <param name="hMidiIn">Handle to a MIDI input device or a MIDI thru device. (For thru devices, this handle must have been returned by a call to the midiOutOpen function.)</param>
        /// <param name="hMidiOut">Handle to the MIDI output or thru device.</param>
        /// <param name="pReserved">Reserved; must be NULL.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. 
        /// Possible error values include the following.
        /// <see cref="MmResult.NotReady"/> Specified input device is already connected to an output device.                
        /// <see cref="MmResult.InvalidHandle"/> Specified device handle is invalid.
        /// </returns>
        /// <remarks>
        /// <para>After calling this function, the MIDI input device receives event data in an MIM_DATA message whenever a message with the same event data is sent to the output device driver.
        /// A thru driver is a special form of MIDI output driver. The system will allow only one MIDI output device to be connected to a MIDI input device, but multiple MIDI output devices can be connected to a MIDI thru device. Whenever the given MIDI input device receives event data in an MIM_DATA message, a message with the same event data is sent to the given output device driver (or through the thru driver to the output drivers).</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiconnect
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiConnect", ExactSpelling = true)]
        private static extern MmResult MidiConnect(IntPtr hMidiIn, out IntPtr hMidiOut, IntPtr pReserved);
    }
}
