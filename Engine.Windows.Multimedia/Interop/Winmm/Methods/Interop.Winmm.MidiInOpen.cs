using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The MidiInProc function is the callback function for handling incoming MIDI messages. MidiInProc is a placeholder for the application-supplied function name. The address of this function can be specified in the callback-address parameter of the midiInOpen function.
        /// </summary>
        /// <param name="midiInHandle">Handle to the MIDI input device.</param>
        /// <param name="message">MIDI input message.</param>
        /// <param name="userData">Instance data supplied with the midiInOpen function.</param>
        /// <param name="messageParameter1">Message parameter.</param>
        /// <param name="messageParameter2">Message parameter.</param>
        /// <remarks>
        /// The meaning of the dwParam1 and dwParam2 parameters is specific to the message type. For more information, see the topics for messages, such as MIM_DATA.
        /// Applications should not call any multimedia functions from inside the callback function, as doing so can cause a deadlock. Other system functions can safely be called from the callback.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/previous-versions//dd798460(v=vs.85)
        /// </acknowledgment>
        public delegate void MidiInCallback(IntPtr midiInHandle, MidiInMessages message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

        /// <summary>
        /// The midiInOpen function opens a specified MIDI input device.
        /// </summary>
        /// <param name="hMidiIn">Pointer to an HMIDIIN handle. This location is filled with a handle identifying the opened MIDI input device. The handle is used to identify the device in calls to other MIDI input functions.</param>
        /// <param name="uDeviceID">Identifier of the MIDI input device to be opened.</param>
        /// <param name="callback">Pointer to a callback function, a thread identifier, or a handle of a window called with information about incoming MIDI messages. For more information on the callback function, see MidiInProc.</param>
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
        /// To determine the number of MIDI input devices present in the system, use the midiInGetNumDevs function. The device identifier specified by wDeviceID varies from zero to one less than the number of devices present.
        /// If a window or thread is chosen to receive callback information, the following messages are sent to the window procedure or thread to indicate the progress of MIDI input: MM_MIM_OPEN, MM_MIM_CLOSE, MM_MIM_DATA, MM_MIM_LONGDATA, MM_MIM_ERROR, MM_MIM_LONGERROR, and MM_MIM_MOREDATA.
        /// If a function is chosen to receive callback information, the following messages are sent to the function to indicate the progress of MIDI input: MIM_OPEN, MIM_CLOSE, MIM_DATA, MIM_LONGDATA, MIM_ERROR, MIM_LONGERROR, and MIM_MOREDATA.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinopen
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInOpen", ExactSpelling = true)]
        internal static extern MmResult MidiInOpen(out IntPtr hMidiIn, IntPtr uDeviceID, MidiInCallback callback, IntPtr dwInstance, MidiInOpenFlags dwFlags);

        /// <summary>
        /// Midis the in open.
        /// </summary>
        /// <param name="deviceNo">The device no.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="openFlags">The open flags.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// The specified resource is already allocated.
        /// or
        /// The specified device identifier is out of range.
        /// or
        /// The flags specified by dwFlags are invalid.
        /// or
        /// The specified pointer or structure is invalid.
        /// or
        /// Unable to allocate or lock memory.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr MidiInOpen(int deviceNo, MidiInCallback callback, MidiInOpenFlags openFlags = MidiInOpenFlags.CALLBACK_FUNCTION)
        {
            return MidiInOpen(out var hMidiIn, (IntPtr)deviceNo, callback, IntPtr.Zero, openFlags) switch
            {
                MmResult.NoError => hMidiIn,
                MmResult.AlreadyAllocated => throw new Exception("The specified resource is already allocated."),
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidFlag => throw new Exception("The flags specified by dwFlags are invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
