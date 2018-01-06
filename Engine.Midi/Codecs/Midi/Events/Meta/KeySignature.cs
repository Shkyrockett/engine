// <copyright file="KeySignature.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    ///   sf is <see cref="MidiKeySignatures"/>
    ///   mi is <see cref="MidiTonality"/>
    /// </remarks>
    [ElementName(nameof(KeySignature))]
    [DisplayName("Key Signature")]
    public class KeySignature
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySignature"></param>
        /// <param name="tonality"></param>
        /// <param name="status"></param>
        public KeySignature(MidiKeySignatures keySignature, MidiTonality tonality, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Key = keySignature;
            Tonality = tonality;
        }

        /// <summary>
        /// 
        /// </summary>
        public MidiKeySignatures Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MidiTonality Tonality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static KeySignature Read(BinaryReaderExtended reader, EventStatus status)
            => new KeySignature((MidiKeySignatures)reader.ReadByte(), (MidiTonality)reader.ReadByte(), status);
    }
}
