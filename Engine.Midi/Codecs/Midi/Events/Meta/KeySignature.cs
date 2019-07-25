// <copyright file="KeySignature.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File
{
    /// <summary>
    /// Key signature.
    /// </summary>
    /// <remarks>
    /// FF 59 02  sf  mi
    ///   sf = -7: 7 flats
    ///   sf = -1: 1 flat
    ///   sf = 0: key of C
    ///   sf = 1: 1 sharp
    ///   sf = 7: 7 sharps
    ///   mi = 0: major key 
    ///   mi = 1: minor key
    ///   
    ///   sf is <see cref="MidiKeySignature"/>
    ///   mi is <see cref="MidiTonality"/>
    /// </remarks>
    [ElementName(nameof(KeySignature))]
    [DisplayName("Key Signature")]
    public class KeySignature
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeySignature"/> class.
        /// </summary>
        /// <param name="keySignature">The keySignature.</param>
        /// <param name="tonality">The tonality.</param>
        /// <param name="status">The status.</param>
        public KeySignature(MidiKeySignature keySignature, MidiTonality tonality, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Key = keySignature;
            Tonality = tonality;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public MidiKeySignature Key { get; set; }

        /// <summary>
        /// Gets or sets the tonality.
        /// </summary>
        public MidiTonality Tonality { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="KeySignature"/>.</returns>
        internal static KeySignature Read(BinaryReaderExtended reader, EventStatus status)
            => new KeySignature((MidiKeySignature)reader.ReadByte(), (MidiTonality)reader.ReadByte(), status);
    }
}
