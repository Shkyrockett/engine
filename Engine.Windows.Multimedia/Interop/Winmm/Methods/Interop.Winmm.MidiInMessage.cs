using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiInMessage function sends a message to the MIDI device driver.
        /// </summary>
        /// <param name="hMidiIn">Identifier of the MIDI device that receives the message. You must cast the device ID to the HMIDIIN handle type. If you supply a handle instead of a device ID, the function fails and returns the MMSYSERR_NOSUPPORT error code.</param>
        /// <param name="msg">Message to send.</param>
        /// <param name="dw1">Message parameter 1.</param>
        /// <param name="dw2">Message parameter 2.</param>
        /// <returns>
        /// Returns the value returned by the audio device driver.
        /// </returns>
        /// <remarks>
        /// This function is used only for driver-specific messages that are not supported by the MIDI API.
        /// The DRV_QUERYDEVICEINTERFACE message queries for the device-interface name of a waveIn, waveOut, midiIn, midiOut, or mixer device.
        /// For DRV_QUERYDEVICEINTERFACE, dwParam1 is a pointer to a caller-allocated buffer into which the function writes a null-terminated Unicode string containing the device-interface name. If the device has no device interface, the string length is zero.
        /// For DRV_QUERYDEVICEINTERFACE, dwParam2 specifies the buffer size in bytes. This is an input parameter to the function. The caller should specify a size that is greater than or equal to the buffer size retrieved by the DRV_QUERYDEVICEINTERFACESIZE message.
        /// The DRV_QUERYDEVICEINTERFACE message is supported in Windows Me, and Windows 2000 and later. This message is valid only for the waveInMessage, waveOutMessage, midiInMessage, midiOutMessage, and mixerMessage functions. The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// The following two message constants are used together for the purpose of obtaining device interface names:DRV_QUERYDEVICEINTERFACESIZE, DRV_QUERYDEVICEINTERFACE.
        /// The first message obtains the size in bytes of the buffer needed to hold the string containing the device interface name. The second message retrieves the name string in a buffer of the required size.
        /// For more information, see Obtaining a Device Interface Name.
        /// The DRV_QUERYDEVICEINTERFACESIZE message queries for the size of the buffer required to hold the device-interface name.
        /// For DRV_QUERYDEVICEINTERFACESIZE, dwParam1 is a pointer to buffer size. This parameter points to a ULONG variable into which the function writes the required buffer size in bytes. The size includes storage space for the name string's terminating null. The size is zero if the device ID identifies a device that has no device interface.
        /// For DRV_QUERYDEVICEINTERFACESIZE, dwParam2 is unused. Set this parameter to zero.
        /// This message is valid only for the waveInMessage, waveOutMessage, midiInMessage, midiOutMessage, and mixerMessage functions. The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// The buffer size retrieved by this message is expressed as a byte count. It specifies the size of the buffer needed to hold the null-terminated Unicode string that contains the device-interface name. The caller allocates a buffer of the specified size and uses the DRV_QUERYDEVICEINTERFACE message to retrieve the device-interface name string.
        /// For more information, see Obtaining a Device Interface Name.
        /// The DRV_QUERYDEVNODE message queries for the devnode number assigned to the device by the Plug and Play manager.
        /// For DRV_QUERYDEVNODE, dwParam1 is a pointer to a caller-allocated DWORD variable into which the function writes the devnode number. If no devnode is assigned to the device, the function sets this variable to zero.
        /// For DRV_QUERYDEVNODE, dwParam2 is unused. Set this parameter to zero.
        /// In Windows 2000 and later, the message always returns MMSYSERR_NOTSUPPORTED. This message is valid only for the waveInMessage, waveOutMessage, midiInMessage, midiOutMessage, and mixerMessage functions. The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// The DRV_QUERYMAPPABLE message queries for whether the specified device can be used by a mapper.
        /// For DRV_QUERYMAPPABLE, dwParam1 is unused. Set this parameter to zero.
        /// For DRV_QUERYMAPPABLE, dwParam2 is unused. Set this parameter to zero.
        /// This message is valid only for the waveInMessage, waveOutMessage, midiInMessage, midiOutMessage, mixerMessage and auxOutMessage functions. The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// When an application program opens a mapper instead of a specific audio device, the system inserts a mapper between the application and the available devices. The mapper selects an appropriate device by mapping the application's requirements to one of the available devices. For more information about mappers, see the Microsoft Windows SDK documentation.
        /// The DRVM_MAPPER_CONSOLEVOICECOM_GET message retrieves the device ID of the preferred voice-communications device.
        /// For DRVM_MAPPER_CONSOLEVOICECOM_GET, dwParam1 is a pointer to device ID. This parameter points to a DWORD variable into which the function writes the device ID of the current preferred voice-communications device. The function writes the value (-1) if no device is available that qualifies as a preferred voice-communications device.
        /// For DRVM_MAPPER_CONSOLEVOICECOM_GET, dwParam2 is a pointer to status flags. This parameter points to a DWORD variable into which the function writes the device-status flags. Only one flag bit is currently defined: DRVM_MAPPER_PREFERRED_FLAGS_PREFERREDONLY.
        /// This message is valid only for the waveInMessage and waveOutMessage functions. When a caller calls these two functions with the DRVM_MAPPER_CONSOLEVOICECOM_GET message, the caller must specify the device ID as WAVE_MAPPER, and then cast this value to the appropriate handle type. For the waveInMessage, waveOutMessage, midiInMessage, midiOutMessage, or mixerMessage functions, the caller must cast the device ID to a handle of type HWAVEIN, HWAVEOUT, HMIDIIN, HMIDIOUT, or HMIXER, respectively. Note that if the caller supplies a valid handle instead of a device ID for this parameter, the function fails and returns error code MMSYSERR_NOSUPPORT.
        /// The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// This message provides a way to determine which device is preferred specifically for voice communications, in contrast to the DRVM_MAPPER_PREFERRED_GET message, which determines which device is preferred for all other audio functions.
        /// For example, the preferred waveOut device for voice communications might be the earpiece in a headset, but the preferred waveOut device for all other audio functions might be a set of stereo speakers.
        /// When the DRVM_MAPPER_PREFERRED_FLAGS_PREFERREDONLY flag bit is set in the DWORD location pointed to by dwParam2, the waveIn and waveOut APIs use only the current preferred voice-communications device and do not search for other available devices if the preferred device is unavailable. The flag that is output by either the waveInMessage or waveOutMessage call applies to the preferred voice-communications device for both the waveIn and waveOut APIs, regardless of whether the call is made to waveInMessage or waveOutMessage. For more information, see Preferred Voice-Communications Device ID.
        /// The DRVM_MAPPER_PREFERRED_GET message retrieves the device ID of the preferred audio device.
        /// For DRVM_MAPPER_PREFERRED_GET, dwParam1 is a pointer to device ID. This parameter points to a DWORD variable into which the function writes the device ID of the current preferred device. The function writes the value (-1) if no device is available that qualifies as a preferred device.
        /// For DRVM_MAPPER_PREFERRED_GET, dwParam2 is a pointer to status flags. This parameter points to a DWORD variable into which the function writes the device-status flags. Only one flag bit is currently defined (for waveInMessage and waveOutMessage calls only): DRVM_MAPPER_PREFERRED_FLAGS_PREFERREDONLY.
        /// This message is valid only for the waveInMessage, waveOutMessage and midiOutMessage functions. When the caller calls these functions with the DRVM_MAPPER_PREFERRED_GET message, the caller must first specify the device ID as WAVE_MAPPER (for waveInMessage or waveOutMessage) or MIDI_MAPPER (for midiOutMessage), and then cast this value to the appropriate handle type. For the waveInMessage, waveOutMessage, or midiOutMessage functions, the caller must cast the device ID to a handle type HWAVEIN, HWAVEOUT or HMIDIOUT, respectively. Note that if the caller supplies a valid handle instead of a device ID for this parameter, the function fails and returns error code MMSYSERR_NOSUPPORT.
        /// The system intercepts this message and returns the appropriate value without sending the message to the device driver. For general information about system-intercepted xxxMessage functions, see System-Intercepted Device Messages.
        /// This message provides a way to determine which device is preferred for audio functions in general, in contrast to the DRVM_MAPPER_CONSOLEVOICECOM_GET message, which determines which device is preferred specifically for voice communications.
        /// When the DRVM_MAPPER_PREFERRED_FLAGS_PREFERREDONLY flag bit is set in the DWORD location pointed to by dwParam2, the waveIn and waveOut APIs use only the current preferred device and do not search for other available devices if the preferred device is unavailable. Note that the midiOutMessage function does not output this flag--the midiOut API always uses only the preferred device. The flag that is output by either the waveInMessage or waveOutMessage call applies to the preferred device for both the waveIn and waveOut APIs, regardless of whether the call is made to waveInMessage or waveOutMessage.
        /// The xxxMessage functions accept this value in place of a valid device handle in order to allow an application to determine the default device ID without first having to open a device. For more information, see Accessing the Preferred Device ID.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midiinmessage
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiInMessage", ExactSpelling = true)]
        internal static extern MmResult MidiInMessage_(IntPtr hMidiIn, int msg, IntPtr dw1, IntPtr dw2);

        /// <summary>
        /// Sends a message to the MIDI device driver.
        /// </summary>
        /// <param name="midiInputHandle">The midi input handle.</param>
        /// <param name="message">The message.</param>
        /// <param name="paramerter1">The paramerter1.</param>
        /// <param name="paramerter2">The paramerter2.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// An invalid handle was provided to connect to a MIDI device.
        /// or
        /// No device driver is present.
        /// or
        /// Unable to allocate or lock memory.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiInMessage(IntPtr midiInputHandle, int message, IntPtr paramerter1, IntPtr paramerter2 )
        {
            return (MidiInMessage_(midiInputHandle, message, paramerter1, paramerter2)) switch
            {
                MmResult.NoError => true,
                // ToDo: Sift through the documentation to figure out how to correctly use this.
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
