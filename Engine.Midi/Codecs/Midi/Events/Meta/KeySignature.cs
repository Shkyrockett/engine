// <copyright file="KeySignature.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// Key signature.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>FF 59 02  sf  mi
    /// sf = -7: 7 flats
    /// sf = -1: 1 flat
    /// sf = 0: key of C
    /// sf = 1: 1 sharp
    /// sf = 7: 7 sharps
    /// mi = 0: major key
    /// mi = 1: minor key
    /// sf is <see cref="MidiKeySignature" />
    /// mi is <see cref="MidiTonality" /></para>
    /// </remarks>
    [ElementName(nameof(KeySignature))]
    public class KeySignature
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeySignature" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="keySignature">The key signature.</param>
        /// <param name="tonality">The tonality.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeySignature(IEventStatus status, MidiKeySignature keySignature, MidiTonality tonality)
            : this(status, 2, keySignature, tonality)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeySignature" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="keySignature">The keySignature.</param>
        /// <param name="tonality">The tonality.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal KeySignature(IEventStatus status, int length, MidiKeySignature keySignature, MidiTonality tonality)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Key, Tonality) = (length, keySignature, tonality);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public object Length { get; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public MidiKeySignature Key { get; set; }

        /// <summary>
        /// Gets or sets the tonality.
        /// </summary>
        /// <value>
        /// The tonality.
        /// </value>
        public MidiTonality Tonality { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="KeySignature" />.
        /// </returns>
        internal static new KeySignature Read(BinaryReaderExtended reader, IEventStatus status) => new KeySignature(status, reader.ReadVariableLengthInt(), (MidiKeySignature)reader.ReadByte(), (MidiTonality)reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Key Signature";
    }
}
