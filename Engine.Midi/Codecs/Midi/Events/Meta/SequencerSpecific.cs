// <copyright file="SequencerSpecific.cs" company="Shkyrockett">
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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// Sequencer specific proprietary event.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>FF 7F len data
    /// System exclusive events and meta events cancel any running status which was in effect.
    /// Running status does not apply to and may not be used for these messages.</para>
    /// </remarks>
    [ElementName(nameof(SequencerSpecific))]
    public class SequencerSpecific
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequencerSpecific" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="data">The data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SequencerSpecific(IEventStatus status, byte[] data)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length => Data.Length;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public byte[] Data { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="SequencerSpecific" />.
        /// </returns>
        internal static new SequencerSpecific Read(BinaryReaderExtended reader, IEventStatus status) => new SequencerSpecific(status, reader.ReadVariableLengthBytes());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Sequencer Specific";
    }
}
