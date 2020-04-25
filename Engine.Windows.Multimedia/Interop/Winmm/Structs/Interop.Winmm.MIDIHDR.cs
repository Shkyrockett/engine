using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
	internal static partial class Winmm
	{
        /// <summary>
        /// The MIDIHDR structure defines the header used to identify a MIDI system-exclusive or stream buffer.
        /// </summary>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/ns-mmeapi-midihdr
        /// </acknowledgment>
        [StructLayout(LayoutKind.Sequential)]
		public struct MIDIHDR
		{
			/// <summary>
			/// Pointer to MIDI data.
			/// </summary>
			public IntPtr lpData; // LPSTR

			/// <summary>
			/// Size of the buffer.
			/// </summary>
			public int dwBufferLength; // DWORD

			/// <summary>
			/// Actual amount of data in the buffer. This value should be less than or equal to the value given in the dwBufferLength member.
			/// </summary>
			public int dwBytesRecorded; // DWORD

			/// <summary>
			/// Custom user data.
			/// </summary>
			public IntPtr dwUser; // DWORD_PTR

			/// <summary>
			/// Flags giving information about the buffer.
			/// </summary>
			public int dwFlags; // DWORD

			/// <summary>
			/// Reserved; do not use.
			/// </summary>
			public IntPtr lpNext; // struct mididhdr_tag *

			/// <summary>
			/// Reserved; do not use.
			/// </summary>
			public IntPtr reserved; // DWORD_PTR

			/// <summary>
			/// Offset into the buffer when a callback is performed. (This callback is generated because the MEVT_F_CALLBACK flag is set in the dwEvent member of the MIDIEVENT structure.) This offset enables an application to determine which event caused the callback.
			/// </summary>
			public int dwOffset; // DWORD

			/// <summary>
			/// Reserved; do not use.
			/// </summary>
			/// <remarks>
			/// MSDN documentation incorrect, see mmsystem.h
			/// </remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public IntPtr[] dwReserved; // DWORD_PTR dwReserved[8]
		}
	}
}
