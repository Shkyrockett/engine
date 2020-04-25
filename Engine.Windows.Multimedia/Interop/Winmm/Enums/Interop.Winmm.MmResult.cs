internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The maxerrorlength
        /// </summary>
        internal const int MaxErrorLength = 256;

        /// <summary>
        /// The mm system error base, MMSYSERR_BASE 
        /// </summary>
        internal const uint MMSysErrorBase = 0;

        /// <summary>
        /// The midi error base, MIDIERR_BASE 
        /// </summary>
        internal const uint MidiErrorBase = 64;

        /// <summary>
        /// Windows multimedia error codes from mmsystem.h.
        /// </summary>
        public enum MmResult
            : uint
        {
            #region General return codes
            /// <summary>
            /// no error, MMSYSERR_NOERROR
            /// </summary>
            NoError = MMSysErrorBase + 0,

            /// <summary>
            /// unspecified error, MMSYSERR_ERROR
            /// </summary>
            UnspecifiedError = MMSysErrorBase + 1,

            /// <summary>
            /// device ID out of range, MMSYSERR_BADDEVICEID
            /// </summary>
            BadDeviceId = MMSysErrorBase + 2,

            /// <summary>
            /// driver failed enable, MMSYSERR_NOTENABLED
            /// </summary>
            NotEnabled = MMSysErrorBase + 3,

            /// <summary>
            /// device already allocated, MMSYSERR_ALLOCATED
            /// </summary>
            AlreadyAllocated = MMSysErrorBase + 4,

            /// <summary>
            /// device handle is invalid, MMSYSERR_INVALHANDLE
            /// </summary>
            InvalidHandle = MMSysErrorBase + 5,

            /// <summary>
            /// no device driver present, MMSYSERR_NODRIVER
            /// </summary>
            NoDriver = MMSysErrorBase + 6,

            /// <summary>
            /// memory allocation error, MMSYSERR_NOMEM
            /// </summary>
            MemoryAllocationError = MMSysErrorBase + 7,

            /// <summary>
            /// function isn't supported, MMSYSERR_NOTSUPPORTED
            /// </summary>
            NotSupported = MMSysErrorBase + 8,

            /// <summary>
            /// error value out of range, MMSYSERR_BADERRNUM
            /// </summary>
            BadErrorNumber = MMSysErrorBase + 9,

            /// <summary>
            /// invalid flag passed, MMSYSERR_INVALFLAG
            /// </summary>
            InvalidFlag = MMSysErrorBase + 10,

            /// <summary>
            /// invalid parameter passed, MMSYSERR_INVALPARAM
            /// </summary>
            InvalidParameter = MMSysErrorBase + 11,

            /// <summary>
            /// handle being used simultaneously on another thread (eg callback),MMSYSERR_HANDLEBUSY
            /// </summary>
            HandleBusy = MMSysErrorBase + 12,

            /// <summary>
            /// specified alias not found, MMSYSERR_INVALIDALIAS
            /// </summary>
            InvalidAlias = MMSysErrorBase + 13,

            /// <summary>
            /// bad registry database, MMSYSERR_BADDB
            /// </summary>
            BadRegistryDatabase = MMSysErrorBase + 14,

            /// <summary>
            /// registry key not found, MMSYSERR_KEYNOTFOUND
            /// </summary>
            RegistryKeyNotFound = MMSysErrorBase + 15,

            /// <summary>
            /// registry read error, MMSYSERR_READERROR
            /// </summary>
            RegistryReadError = MMSysErrorBase + 16,

            /// <summary>
            /// registry write error, MMSYSERR_WRITEERROR
            /// </summary>
            RegistryWriteError = MMSysErrorBase + 17,

            /// <summary>
            /// registry delete error, MMSYSERR_DELETEERROR
            /// </summary>
            RegistryDeleteError = MMSysErrorBase + 18,

            /// <summary>
            /// registry value not found, MMSYSERR_VALNOTFOUND
            /// </summary>
            RegistryValueNotFound = MMSysErrorBase + 19,

            /// <summary>
            /// driver does not call DriverCallback, MMSYSERR_NODRIVERCB
            /// </summary>
            NoDriverCallback = MMSysErrorBase + 20,

            /// <summary>
            /// more data to be returned, MMSYSERR_MOREDATA
            /// </summary>
            MoreData = MMSysErrorBase + 21,
            #endregion

            #region Wave-specific return codes
            /// <summary>
            /// unsupported wave format, WAVERR_BADFORMAT
            /// </summary>
            WaveBadFormat = 32,

            /// <summary>
            /// still something playing, WAVERR_STILLPLAYING
            /// </summary>
            WaveStillPlaying = 33,

            /// <summary>
            /// header not prepared, WAVERR_UNPREPARED
            /// </summary>
            WaveHeaderUnprepared = 34,

            /// <summary>
            /// device is synchronous, WAVERR_SYNC
            /// </summary>
            WaveSync = 35,
            #endregion

            #region MIDI-specific return codes
            /// <summary>
            /// The unprepared, MIDIERR_UNPREPARED
            /// </summary>
            Unprepared = MidiErrorBase + 0,

            /// <summary>
            /// The still playing, MIDIERR_STILLPLAYING 
            /// </summary>
            StillPlaying = MidiErrorBase + 1,

            /// <summary>
            /// The no map, MIDIERR_NOMAP 
            /// </summary>
            NoMap = MidiErrorBase + 2,

            /// <summary>
            /// The not ready, MIDIERR_NOTREADY 
            /// </summary>
            NotReady = MidiErrorBase + 3,

            /// <summary>
            /// The no device, MIDIERR_NODEVICE 
            /// </summary>
            NoDevice = MidiErrorBase + 4,

            /// <summary>
            /// The invalid setup, MIDIERR_INVALIDSETUP 
            /// </summary>
            InvalidSetup = MidiErrorBase + 5,

            /// <summary>
            /// The bad open mode, MIDIERR_BADOPENMODE 
            /// </summary>
            BadOpenMode = MidiErrorBase + 6,

            /// <summary>
            /// The dont continue, MIDIERR_DONT_CONTINUE 
            /// </summary>
            DontContinue = MidiErrorBase + 7,
            #endregion

            #region ACM-specific return codes, found in msacm.h
            /// <summary>
            /// Conversion not possible (ACMERR_NOTPOSSIBLE)
            /// </summary>
            AcmNotPossible = 512,

            /// <summary>
            /// Busy (ACMERR_BUSY)
            /// </summary>
            AcmBusy = 513,

            /// <summary>
            /// Header Unprepared (ACMERR_UNPREPARED)
            /// </summary>
            AcmHeaderUnprepared = 514,

            /// <summary>
            /// Canceled (ACMERR_CANCELED)
            /// </summary>
            AcmCancelled = 515,
            #endregion

            #region Mixer-specific return codes, found in mmresult.h
            /// <summary>
            /// invalid line (MIXERR_INVALLINE)
            /// </summary>
            MixerInvalidLine = 1024,

            /// <summary>
            /// invalid control (MIXERR_INVALCONTROL)
            /// </summary>
            MixerInvalidControl = 1025,

            /// <summary>
            /// invalid value (MIXERR_INVALVALUE)
            /// </summary>
            MixerInvalidValue = 1026,
            #endregion
        }
    }
}
