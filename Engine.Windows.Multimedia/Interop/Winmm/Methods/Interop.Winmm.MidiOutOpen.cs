using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The MidiOutProc function is the callback function for handling outgoing MIDI messages. MidiOutProc is a placeholder for the application-supplied function name. The address of the function can be specified in the callback-address parameter of the midiOutOpen function.
        /// </summary>
        /// <param name="midiInHandle">Handle to the MIDI device associated with the callback function.</param>
        /// <param name="message">MIDI output message.</param>
        /// <param name="userData">Instance data supplied by using the midiOutOpen function.</param>
        /// <param name="messageParameter1">The Message parameter 1.</param>
        /// <param name="messageParameter2">The Message parameter 2.</param>
        /// <remarks>
        /// Applications should not call any multimedia functions from inside the callback function, as doing so can cause a deadlock. Other system functions can safely be called from the callback.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/previous-versions//dd798478(v=vs.85)
        /// </acknowledgment>
        public delegate void MidiOutCallback(IntPtr midiInHandle, MidiOutMessages message, IntPtr userData, IntPtr messageParameter1, IntPtr messageParameter2);

        /// <summary>
        /// The midiOutOpen function opens a MIDI output device for playback.
        /// </summary>
        /// <param name="lphMidiOut">Pointer to an HMIDIOUT handle. This location is filled with a handle identifying the opened MIDI output device. The handle is used to identify the device in calls to other MIDI output functions.</param>
        /// <param name="uDeviceID">Identifier of the MIDI output device that is to be opened.</param>
        /// <param name="dwCallback">Pointer to a callback function, an event handle, a thread identifier, or a handle of a window or thread called during MIDI playback to process messages related to the progress of the playback. If no callback is desired, specify NULL for this parameter. For more information on the callback function, see MidiOutProc.</param>
        /// <param name="dwInstance">User instance data passed to the callback. This parameter is not used with window callbacks or threads.</param>
        /// <param name="dwFlags">Callback flag for opening the device. It can be the following values.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.NoDevice"/> No MIDI port was found. This error occurs only when the mapper is opened.
        /// <see cref="MmResult.AlreadyAllocated"/> The specified resource is already allocated.
        /// <see cref="MmResult.BadDeviceId"/> The specified device identifier is out of range.
        /// <see cref="MmResult.InvalidParameter"/> The specified pointer or structure is invalid.
        /// <see cref="MmResult.MemoryAllocationError"/> The system is unable to allocate or lock memory.
        /// </returns>
        /// <remarks>
        /// To determine the number of MIDI output devices present in the system, use the midiOutGetNumDevs function. The device identifier specified by wDeviceID varies from zero to one less than the number of devices present. MIDI_MAPPER can also be used as the device identifier.
        /// If a window or thread is chosen to receive callback information, the following messages are sent to the window procedure or thread to indicate the progress of MIDI output: MM_MOM_OPEN, MM_MOM_CLOSE, and MM_MOM_DONE.
        /// If a function is chosen to receive callback information, the following messages are sent to the function to indicate the progress of MIDI output: MOM_OPEN, MOM_CLOSE, and MOM_DONE.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midioutopen
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiOutOpen", ExactSpelling = true)]
        public static extern MmResult MidiOutOpen(out IntPtr lphMidiOut, IntPtr uDeviceID, MidiOutCallback dwCallback, IntPtr dwInstance, MidiOutOpenFlags dwFlags);

        /// <summary>
        /// Opens a MIDI output device for playback.
        /// </summary>
        /// <param name="deviceNo">The device no.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="openFlag">The open flag.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// No MIDI port was found. This error occurs only when the mapper is opened.
        /// or
        /// The specified resource is already allocated.
        /// or
        /// The specified device identifier is out of range.
        /// or
        /// The specified pointer or structure is invalid.
        /// or
        /// Unable to allocate or lock memory.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr MidiOutOpen(int deviceNo, MidiOutCallback callback, MidiOutOpenFlags openFlag = MidiOutOpenFlags.CALLBACK_FUNCTION)
        {
            return MidiOutOpen(out var midiOutputHandle, (IntPtr)deviceNo, callback, IntPtr.Zero, openFlag) switch
            {
                MmResult.NoError => midiOutputHandle,
                MmResult.NoDevice => throw new Exception("No MIDI port was found. This error occurs only when the mapper is opened."),
                MmResult.AlreadyAllocated => throw new Exception("The specified resource is already allocated."),
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
