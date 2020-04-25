using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// The midiStreamProperty function sets or retrieves properties of a MIDI data stream associated with a MIDI output device.
        /// </summary>
        /// <param name="hMidiStream">Handle to the MIDI device that the property is associated with.</param>
        /// <param name="lppropdata">Pointer to the property data.</param>
        /// <param name="dwProperty">Flags that specify the action to perform and identify the appropriate property of the MIDI data stream. The midiStreamProperty function requires setting two flags in each use. One flag (either MIDIPROP_GET or MIDIPROP_SET) specifies an action, and the other identifies a specific property to examine or edit.</param>
        /// <returns>
        /// Returns <see cref="MmResult.NoError"/> if successful or an error otherwise. Possible error values include the following.
        /// <see cref="MmResult.InvalidHandle"/> The specified handle is not a stream handle.
        /// <see cref="MmResult.InvalidParameter"/> The given handle or flags parameter is invalid.
        /// </returns>
        /// <remarks>
        /// These properties are the default properties defined by the system. Driver writers can implement and document their own properties.
        /// </remarks>
        /// <acknowledgment>
        /// https://docs.microsoft.com/windows/win32/api/mmeapi/nf-mmeapi-midistreamproperty
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DllImport(Libraries.Winmm, EntryPoint = "midiStreamProperty", ExactSpelling = true)]
        internal static extern MmResult MidiStreamProperty_(IntPtr hMidiStream, IntPtr lppropdata, StreamPropertFlags dwProperty);

        /// <summary>
        /// Midis the stream property.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MidiStreamProperty(IntPtr midiStream, IntPtr propData, StreamPropertFlags propertyFlags)
        {
            return MidiStreamProperty_(midiStream, propData, propertyFlags) switch
            {
                MmResult.NoError => true,
                MmResult.InvalidHandle => throw new Exception("The specified device handle is invalid."),
                MmResult.InvalidParameter => throw new Exception("The specified pointer or structure is invalid."),
                _ => throw new Exception("Unspecified Error"),
            };
        }
    }
}
