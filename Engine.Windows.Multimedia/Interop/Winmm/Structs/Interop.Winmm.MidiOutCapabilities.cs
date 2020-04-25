using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Winmm
    {
        /// <summary>
        /// class representing the capabilities of a MIDI out device
        /// MIDIOUTCAPS: http://msdn.microsoft.com/en-us/library/dd798467%28VS.85%29.aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MidiOutCapabilities
        {
            /// <summary>
            /// The manufacturer identifier
            /// </summary>
            private short manufacturerId;

            /// <summary>
            /// The driver version
            /// </summary>
            private int driverVersion;

            /// <summary>
            /// The product name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxProductNameLength)]
            private string productName;

            /// <summary>
            /// The w technology
            /// </summary>
            private short wTechnology;

            /// <summary>
            /// The w voices
            /// </summary>
            private short wVoices;

            /// <summary>
            /// The w notes
            /// </summary>
            private short wNotes;

            /// <summary>
            /// The w channel mask
            /// </summary>
            private ushort wChannelMask;

            /// <summary>
            /// The dw support
            /// </summary>
            private MidiOutCapabilityFlags dwSupport;

            /// <summary>
            /// The maximum product name length
            /// </summary>
            private const int MaxProductNameLength = 32; // max product name length (including NULL)

            /// <summary>
            /// 
            /// </summary>
            [Flags]
            private enum MidiOutCapabilityFlags
            {
                /// <summary>
                /// MIDICAPS_VOLUME
                /// </summary>
                Volume = 1,

                /// <summary>
                /// separate left-right volume control
                /// MIDICAPS_LRVOLUME
                /// </summary>
                LeftRightVolume = 2,

                /// <summary>
                /// MIDICAPS_CACHE
                /// </summary>
                PatchCaching = 4,

                /// <summary>
                /// MIDICAPS_STREAM
                /// driver supports midiStreamOut directly
                /// </summary>
                Stream = 8,
            }

            /// <summary>
            /// Gets the manufacturer of this device
            /// </summary>
            /// <value>
            /// The manufacturer.
            /// </value>
            public Manufacturers Manufacturer => (Manufacturers)manufacturerId;

            /// <summary>
            /// Gets the product identifier (manufacturer specific)
            /// </summary>
            /// <value>
            /// The product identifier.
            /// </value>
            public short ProductId { get; }

            /// <summary>
            /// Gets the product name
            /// </summary>
            /// <value>
            /// The name of the product.
            /// </value>
            public string ProductName => productName;

            /// <summary>
            /// Returns the number of supported voices
            /// </summary>
            /// <value>
            /// The voices.
            /// </value>
            public int Voices => wVoices;

            /// <summary>
            /// Gets the polyphony of the device
            /// </summary>
            /// <value>
            /// The notes.
            /// </value>
            public int Notes => wNotes;

            /// <summary>
            /// Returns true if the device supports all channels
            /// </summary>
            /// <value>
            ///   <c>true</c> if [supports all channels]; otherwise, <c>false</c>.
            /// </value>
            public bool SupportsAllChannels => wChannelMask == 0xFFFF;

            /// <summary>
            /// Queries whether a particular channel is supported
            /// </summary>
            /// <param name="channel">Channel number to test</param>
            /// <returns>
            /// True if the channel is supported
            /// </returns>
            public bool SupportsChannel(int channel) => (wChannelMask & (1 << (channel - 1))) > 0;

            /// <summary>
            /// Returns true if the device supports patch caching
            /// </summary>
            /// <value>
            ///   <c>true</c> if [supports patch caching]; otherwise, <c>false</c>.
            /// </value>
            public bool SupportsPatchCaching => (dwSupport & MidiOutCapabilityFlags.PatchCaching) != 0;

            /// <summary>
            /// Returns true if the device supports separate left and right volume
            /// </summary>
            /// <value>
            ///   <c>true</c> if [supports separate left and right volume]; otherwise, <c>false</c>.
            /// </value>
            public bool SupportsSeparateLeftAndRightVolume => (dwSupport & MidiOutCapabilityFlags.LeftRightVolume) != 0;

            /// <summary>
            /// Returns true if the device supports MIDI stream out
            /// </summary>
            /// <value>
            ///   <c>true</c> if [supports midi stream out]; otherwise, <c>false</c>.
            /// </value>
            public bool SupportsMidiStreamOut => (dwSupport & MidiOutCapabilityFlags.Stream) != 0;

            /// <summary>
            /// Returns true if the device supports volume control
            /// </summary>
            /// <value>
            ///   <c>true</c> if [supports volume control]; otherwise, <c>false</c>.
            /// </value>
            public bool SupportsVolumeControl => (dwSupport & MidiOutCapabilityFlags.Volume) != 0;

            /// <summary>
            /// Returns the type of technology used by this MIDI out device
            /// </summary>
            /// <value>
            /// The technology.
            /// </value>
            public MidiOutTechnology Technology => (MidiOutTechnology)wTechnology;
        }
    }
}
