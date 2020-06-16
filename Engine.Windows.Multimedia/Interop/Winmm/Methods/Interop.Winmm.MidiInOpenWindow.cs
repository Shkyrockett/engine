using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInOpen function opens a specified MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Pointer to an HMIDIIN handle. This location is filled with a handle identifying the opened MIDI input device. The handle is used to identify the device in calls to other MIDI input functions.</param>
        /// <param name="uDeviceID">Identifier of the MIDI input device to be opened.</param>
        /// <param name="callbackWindowHandle">Pointer to a callback function, a thread identifier, or a handle of a window called with information about incoming MIDI messages. For more information on the callback function, see MidiInProc.</param>
        /// <param name="dwInstance">User instance data passed to the callback function. This parameter is not used with window callback functions or threads.</param>
        /// <param name="dwFlags">Callback flag for opening the device and, optionally, a status flag that helps regulate rapid data transfers. It can be the following values.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following
        /// <see cref="MmResult.AlreadyAllocated"/> The specified resource is already allocated.
        /// <see cref="MmResult.BadDeviceId"/> The specified device identifier is out of range.
        /// <see cref="MmResult.InvalidFlag"/> The flags specified by dwFlags are invalid.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/>MMSYSERR_NOMEM The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// <para>To determine the number of MIDI input devices present in the system, use the midiInGetNumDevs function. The device identifier specified by wDeviceID varies from zero to one less than the number of devices present.
        /// If a window or thread is chosen to receive callback information, the following messages are sent to the window procedure or thread to indicate the progress of MIDI input: MM_MIM_OPEN, MM_MIM_CLOSE, MM_MIM_DATA, MM_MIM_LONGDATA, MM_MIM_ERROR, MM_MIM_LONGERROR, and MM_MIM_MOREDATA.
        /// If a function is chosen to receive callback information, the following messages are sent to the function to indicate the progress of MIDI input: MIM_OPEN, MIM_CLOSE, MIM_DATA, MIM_LONGDATA, MIM_ERROR, MIM_LONGERROR, and MIM_MOREDATA.</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinopen
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInOpen", ExactSpelling = true)]
        private static extern MmResult MidiInOpenWindow(out IntPtr hMidiIn, IntPtr uDeviceID, IntPtr callbackWindowHandle, IntPtr dwInstance, MidiInOpenFlags dwFlags);
    }
}
