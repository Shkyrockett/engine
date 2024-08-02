using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// 
        /// </summary>
        public static class Midi
        {
            /// <summary>
            /// Connects a MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The MIDI input handle.</param>
            /// <returns>
            /// The MIDI output handle.
            /// </returns>
            /// <exception cref="System.Exception">
            /// The Midi Device is not ready to connect to.
            /// or
            /// An invalid handle was provided to connect to a MIDI device.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiConnect(IntPtr midiInputHandle) => (Winmm.MidiConnect(midiInputHandle, out IntPtr midiOutputHandle, IntPtr.Zero)) switch
            {
                MmResult.NoError => midiOutputHandle,
                MmResult.NotReady => throw new Exception("The Midi Device is not ready to connect to."),
                MmResult.InvalidHandle => throw new Exception("An invalid handle was provided to connect to a MIDI device."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Disconnects the specified midi input handle.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns>
            /// The MIDI output handle.
            /// </returns>
            /// <exception cref="System.Exception">An invalid handle was provided to disconnect a MIDI device.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiDisconnect(IntPtr midiInputHandle) => (Winmm.MidiDisconnect(midiInputHandle, out var midiOutputHandle, IntPtr.Zero)) switch
            {
                MmResult.NoError => midiOutputHandle,
                MmResult.InvalidHandle => throw new Exception("An invalid handle was provided to disconnect a MIDI device."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sends an input buffer to a specified opened MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <param name="midiInHeader">The midi in header.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The Midi buffer is still in the queue.
            /// or
            /// The Midi buffer has not been prepared.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInAddBuffer(IntPtr midiInputHandle, MIDIHDR midiInHeader) => (Winmm.MidiInAddBuffer(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
            {
                MmResult.NoError => true,
                MmResult.StillPlaying => throw new Exception("The Midi buffer is still in the queue."),
                MmResult.Unprepared => throw new Exception("The Midi buffer has not been prepared."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Midis the in close.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns></returns>
            /// <exception cref="System.Exception">
            /// The Midi buffer is still in the queue.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInClose(IntPtr midiInputHandle) => (MidiInClose_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.StillPlaying => throw new Exception("The Midi buffer is still in the queue."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Determines the capabilities of a specified MIDI input device.
            /// </summary>
            /// <param name="midiInDeviceNumber">The midi in device number.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public unsafe static MidiInCapabilities MidiInGetDevCaps(int midiInDeviceNumber)
            {
                var capabilities = new MidiInCapabilities();
                var structSize = Marshal.SizeOf(capabilities);
                return (Winmm.MidiInGetDevCaps((IntPtr)midiInDeviceNumber, out capabilities, structSize)) switch
                {
                    MmResult.NoError => capabilities,
                    MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                    MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                    MmResult.NoDriver => throw new Exception("The driver is not installed."),
                    MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                    _ => throw new Exception("Unspecified Error"),
                };
            }

            /// <summary>
            /// Midis the in get error text.
            /// </summary>
            /// <param name="error">The error.</param>
            /// <returns></returns>
            /// <exception cref="System.Exception">
            /// The specified device identifier is out of range.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The driver is not installed.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static string MidiInGetErrorText(int error)
            {
                var sb = new StringBuilder(MaxErrorLength);
                return (Winmm.MidiInGetErrorText(error, sb, MaxErrorLength)) switch
                {
                    MmResult.NoError => sb.ToString().Trim(),
                    MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                    MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                    MmResult.NoDriver => throw new Exception("The driver is not installed."),
                    MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                    _ => throw new Exception("Unspecified Error"),
                };
            }

            /// <summary>
            /// Gets the device identifier for the given MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The Midi Device is not ready to connect to.
            /// or
            /// An invalid handle was provided to connect to a MIDI device.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static int MidiInGetID(IntPtr midiInputHandle) => (Winmm.MidiInGetID(midiInputHandle, out var deviceID)) switch
            {
                MmResult.NoError => deviceID,
                MmResult.InvalidHandle => throw new Exception("An invalid handle was provided to connect to a MIDI device."),
                MmResult.NoDriver => throw new Exception("No device driver is present."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sends a message to the MIDI device driver.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <param name="message">The message.</param>
            /// <param name="paramerter1">The paramerter1.</param>
            /// <param name="paramerter2">The paramerter2.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInMessage(IntPtr midiInputHandle, int message, IntPtr paramerter1, IntPtr paramerter2) => (MidiInMessage_(midiInputHandle, message, paramerter1, paramerter2)) switch
            {
                MmResult.NoError => true,
                // ToDo: Sift through the documentation to figure out how to correctly use this.
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Opens a specified MIDI input device.
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
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiInOpen(int deviceNo, MidiInCallback callback, MidiInOpenFlags openFlags = MidiInOpenFlags.CALLBACK_FUNCTION) => Winmm.MidiInOpen(out var hMidiIn, (IntPtr)deviceNo, callback, IntPtr.Zero, openFlags) switch
            {
                MmResult.NoError => hMidiIn,
                MmResult.AlreadyAllocated => throw new Exception("The specified resource is already allocated."),
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidFlag => throw new Exception("The flags specified by dwFlags are invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Midis the in open window.
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
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiInOpenWindow(int deviceNo, IntPtr callback, MidiInOpenFlags openFlags = MidiInOpenFlags.CALLBACK_WINDOW) => Winmm.MidiInOpenWindow(out var hMidiIn, (IntPtr)deviceNo, callback, IntPtr.Zero, openFlags) switch
            {
                MmResult.NoError => hMidiIn,
                MmResult.AlreadyAllocated => throw new Exception("The specified resource is already allocated."),
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidFlag => throw new Exception("The flags specified by dwFlags are invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Prepares a buffer for MIDI input.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <param name="midiInHeader">The midi in header.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The Midi buffer is still in the queue.
            /// or
            /// The Midi buffer has not been prepared.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MIDIHDR MidiInPrepareHeader(IntPtr midiInputHandle, MIDIHDR midiInHeader) => (Winmm.MidiInPrepareHeader(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
            {
                MmResult.NoError => midiInHeader,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Stops input on a given MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInReset(IntPtr midiInputHandle) => (MidiInReset_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Starts MIDI input on the specified MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInStart(IntPtr midiInputHandle) => (MidiInStart_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Stops MIDI input on the specified MIDI input device.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiInStop(IntPtr midiInputHandle) => (MidiInStop_(midiInputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Cleans up the preparation performed by the midiInPrepareHeader function.
            /// </summary>
            /// <param name="midiInputHandle">The midi input handle.</param>
            /// <param name="midiInHeader">The midi in header.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MIDIHDR MidiInUnprepareHeader(IntPtr midiInputHandle, MIDIHDR midiInHeader) => (Winmm.MidiInPrepareHeader(midiInputHandle, ref midiInHeader, Marshal.SizeOf(midiInHeader))) switch
            {
                MmResult.NoError => midiInHeader,
                MmResult.StillPlaying => throw new Exception("The buffer pointed to by lpMidiInHdr is still in the queue."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Requests that an internal MIDI synthesizer device preload and cache a specified set of key-based percussion patches.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="patch">The patch.</param>
            /// <param name="keyArray">The key array.</param>
            /// <param name="flags">The flags.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutCacheDrumPatches(IntPtr midiOutputHandle, int patch, IntPtr keyArray, MidiCacheFlags flags) => (MidiOutCacheDrumPatches_(midiOutputHandle, patch, keyArray, flags)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidFlag => throw new Exception("The flag specified by wFlags is invalid."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.NotSupported => throw new Exception("The specified device does not support patch caching."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Requests that an internal MIDI synthesizer device preload and cache a specified set of patches.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="bank">The bank.</param>
            /// <param name="patchArray">The patch array.</param>
            /// <param name="flags">The flags.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The flag specified by wFlags is invalid.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// or
            /// The specified device does not support patch caching.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutCachePatches(IntPtr midiOutputHandle, int bank, IntPtr patchArray, MidiCacheFlags flags) => (MidiOutCachePatches_(midiOutputHandle, bank, patchArray, flags)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidFlag => throw new Exception("The flag specified by wFlags is invalid."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.NotSupported => throw new Exception("The specified device does not support patch caching."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Closes the specified MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutClose(IntPtr midiOutputHandle) => (MidiOutClose_(midiOutputHandle)) switch
            {
                MmResult.NoError => true,
                MmResult.StillPlaying => throw new Exception("Buffers are still in the queue."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to load mapper string description."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Queries a specified MIDI output device to determine its capabilities.
            /// </summary>
            /// <param name="midiOutDeviceNumber">The midi out device number.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MidiOutCapabilities MidiOutGetDevCaps(int midiOutDeviceNumber)
            {
                var caps = new MidiOutCapabilities();
                var structSize = Marshal.SizeOf(caps);
                return Winmm.MidiOutGetDevCaps((IntPtr)midiOutDeviceNumber, out caps, structSize) switch
                {
                    MmResult.NoError => caps,
                    MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                    MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                    MmResult.NoDriver => throw new Exception("The driver is not installed."),
                    MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                    _ => throw new Exception("Unspecified Error"),
                };
            }

            /// <summary>
            /// Retrieves a textual description for an error identified by the specified error code.
            /// </summary>
            /// <param name="error">The error.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified error number is out of range.
            /// or
            /// The specified pointer or structure is invalid.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static string MidiOutGetErrorText(IntPtr error)
            {
                var sb = new StringBuilder(MaxErrorLength);
                return Winmm.MidiOutGetErrorText(error, sb, MaxErrorLength) switch
                {
                    MmResult.NoError => sb.ToString().Trim(),
                    MmResult.BadErrorNumber => throw new Exception("The specified error number is out of range."),
                    MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                    _ => throw new Exception("Unspecified Error"),
                };
            }

            /// <summary>
            /// Retrieves the device identifier for the given MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static int MidiOutGetID(IntPtr midiOutputHandle) => Winmm.MidiOutGetID(midiOutputHandle, out var deviceID) switch
            {
                MmResult.NoError => deviceID,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.NoDriver => throw new Exception("No device driver is present."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Retrieves the number of MIDI output devices present in the system.
            /// </summary>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static int MidiOutGetNumDevs() => MidiOutGetNumDevs_();

            /// <summary>
            /// Retrieves the current volume setting of a MIDI output device.
            /// </summary>
            /// <param name="midiInDeviceNumber">The midi in device number.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// or
            /// The function is not supported.
            /// </exception>
            public static (short left, short right) MidiOutGetVolume(int midiInDeviceNumber) => (Winmm.MidiOutGetVolume((IntPtr)midiInDeviceNumber, out var volume)) switch
            {
                MmResult.NoError => ((short)(volume & 0x0000FFFF), (short)(volume >> 16)),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.NotSupported => throw new Exception("The function is not supported."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sends a system-exclusive MIDI message to the specified MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="midiOutHeader">The midi out header.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MIDIHDR MidiOutLongMsg(IntPtr midiOutputHandle, ref MIDIHDR midiOutHeader) => Winmm.MidiOutLongMsg(midiOutputHandle, ref midiOutHeader, Marshal.SizeOf(midiOutHeader)) switch
            {
                MmResult.NoError => midiOutHeader,
                MmResult.NotReady => throw new Exception("The hardware is busy with other data."),
                MmResult.Unprepared => throw new Exception("The buffer pointed to by lpMidiOutHdr has not been prepared."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sends a message to the MIDI device drivers. This function is used only for driver-specific messages that are not supported by the MIDI API.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="message">The message.</param>
            /// <param name="param1">The param1.</param>
            /// <param name="param2">The param2.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutMessage(IntPtr midiOutputHandle, int message, int param1, int param2) => Winmm.MidiOutMessage(midiOutputHandle, message, (IntPtr)param1, (IntPtr)param2) switch
            {
                MmResult.NoError => true,
                _ => throw new Exception("Unspecified Error"),
            };

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
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiOutOpen(int deviceNo, MidiOutCallback callback, MidiOutOpenFlags openFlag = MidiOutOpenFlags.CALLBACK_FUNCTION) => Winmm.MidiOutOpen(out var midiOutputHandle, (IntPtr)deviceNo, callback, IntPtr.Zero, openFlag) switch
            {
                MmResult.NoError => midiOutputHandle,
                MmResult.NoDevice => throw new Exception("No MIDI port was found. This error occurs only when the mapper is opened."),
                MmResult.AlreadyAllocated => throw new Exception("The specified resource is already allocated."),
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("Unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Prepares a MIDI system-exclusive or stream buffer for output.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="lpMidiOutHdr">The lp midi out HDR.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MIDIHDR MidiOutPrepareHeader(IntPtr midiOutputHandle, ref MIDIHDR midiOutHeader) => Winmm.MidiOutPrepareHeader(midiOutputHandle, ref midiOutHeader, Marshal.SizeOf(midiOutHeader)) switch
            {
                MmResult.NoError => midiOutHeader,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Turns off all notes on all MIDI channels for the specified MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutReset(IntPtr midiOutputHandle) => MidiOutReset_(midiOutputHandle) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sets the volume of a MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutSetVolume(IntPtr midiOutputHandle, short left, short right) => Winmm.MidiOutSetVolume(midiOutputHandle, (right & 0x0000FFFF) | (left << 16)) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.NotSupported => throw new Exception("The function is not supported."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Sends a short MIDI message to the specified MIDI output device.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="message">The message.</param>
            /// <returns></returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutShortMsg(IntPtr midiOutputHandle, int message) => Winmm.MidiOutSetVolume(midiOutputHandle, message) switch
            {
                MmResult.NoError => true,
                MmResult.BadOpenMode => throw new Exception("The application sent a message without a status byte to a stream handle."),
                MmResult.NotReady => throw new Exception("The hardware is busy with other data."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Cleans up the preparation performed by the midiOutPrepareHeader function.
            /// </summary>
            /// <param name="midiOutputHandle">The midi output handle.</param>
            /// <param name="midiOutHdr">The midi out HDR.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The buffer pointed to by lpMidiOutHdr is still in the queue.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiOutUnprepareHeader(IntPtr midiOutputHandle, ref MIDIHDR midiOutHdr) => Winmm.MidiOutUnprepareHeader(midiOutputHandle, ref midiOutHdr, Marshal.SizeOf(midiOutHdr)) switch
            {
                MmResult.NoError => true,
                MmResult.StillPlaying => throw new Exception("The buffer pointed to by lpMidiOutHdr is still in the queue."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Closes an open MIDI stream.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamClose(IntPtr midiStream) => MidiStreamClose_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Opens a MIDI stream for output. By default, the device is opened in paused mode. The stream handle retrieved by this function must be used in all subsequent references to the stream.
            /// </summary>
            /// <param name="deviceID">The device identifier.</param>
            /// <param name="midi">The midi.</param>
            /// <param name="callback">The callback.</param>
            /// <param name="instance">The instance.</param>
            /// <param name="midiOpenFlags">The midi open flags.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device identifier is out of range.
            /// or
            /// The specified pointer or structure is invalid.
            /// or
            /// The system is unable to allocate or lock memory.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static IntPtr MidiStreamOpen(IntPtr deviceID, int midi, IntPtr callback, IntPtr instance, MidiOutOpenFlags midiOpenFlags) => Winmm.MidiStreamOpen(out var midiOutputHandle, deviceID, midi, callback, instance, midiOpenFlags) switch
            {
                MmResult.NoError => midiOutputHandle,
                MmResult.BadDeviceId => throw new Exception("The specified device identifier is out of range."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Plays or queues a stream (buffer) of MIDI data to a MIDI output device.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <param name="midiHeader">The midi header.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The system is unable to allocate or lock memory.
            /// or
            /// The buffer pointed to by lpMidiOutHdr is still in the queue.
            /// or
            /// The buffer pointed to by lpMidiOutHdr has not been prepared.
            /// or
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamOut(IntPtr midiStream, ref MIDIHDR midiHeader) => Winmm.MidiStreamOut(midiStream, ref midiHeader, Marshal.SizeOf(midiHeader)) switch
            {
                MmResult.NoError => true,
                MmResult.MemoryAllocationError => throw new Exception("The system is unable to allocate or lock memory."),
                MmResult.StillPlaying => throw new Exception("The buffer pointed to by lpMidiOutHdr is still in the queue."),
                MmResult.Unprepared => throw new Exception("The buffer pointed to by lpMidiOutHdr has not been prepared."),
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Pauses playback of a specified MIDI stream.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamPause(IntPtr midiStream) => MidiStreamPause_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Retrieves the current position in a MIDI stream.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static MMTIME MidiStreamPosition(IntPtr midiStream)
            {
                var lpmmt = new MMTIME();
                return Winmm.MidiStreamPosition(midiStream, ref lpmmt, Marshal.SizeOf(lpmmt)) switch
                {
                    MmResult.NoError => lpmmt,
                    MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                    MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                    _ => throw new Exception("Unspecified Error"),
                };
            }

            /// <summary>
            /// Sets or retrieves properties of a MIDI data stream associated with a MIDI output device.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <param name="propData">The property data.</param>
            /// <param name="propertyFlags">The property flags.</param>
            /// <returns></returns>
            /// <exception cref="Exception">
            /// The specified device handle is invalid.
            /// or
            /// The specified pointer or structure is invalid.
            /// </exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamProperty(IntPtr midiStream, IntPtr propData, StreamPropertFlags propertyFlags) => MidiStreamProperty_(midiStream, propData, propertyFlags) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Restarts a paused MIDI stream.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamRestart(IntPtr midiStream) => MidiStreamRestart_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };

            /// <summary>
            /// Turns off all notes on all MIDI channels for the specified MIDI output device.
            /// </summary>
            /// <param name="midiStream">The midi stream.</param>
            /// <returns></returns>
            /// <exception cref="Exception">The specified device handle is invalid.</exception>
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static bool MidiStreamStop(IntPtr midiStream) => MidiStreamStop_(midiStream) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
