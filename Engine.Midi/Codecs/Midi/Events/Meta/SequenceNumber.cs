// <copyright file="SequenceNumber.cs" company="Shkyrockett">
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
    /// Sequence Number.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>FF 00 02  SS SS or 00</para>
    /// </remarks>
    [ElementName(nameof(SequenceNumber))]
    public class SequenceNumber
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNumber" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SequenceNumber(IEventStatus status, short value)
            : this(status, 2, value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceNumber" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="value">The value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal SequenceNumber(IEventStatus status, int length, short value)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Value) = (length, value);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length. Should always be 2.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public short Value { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="SequenceNumber" />.
        /// </returns>
        internal static new SequenceNumber Read(BinaryReaderExtended reader, IEventStatus status) => new SequenceNumber(status, reader.ReadVariableLengthInt(), reader.ReadNetworkInt16());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Sequence Number";
    }
}
