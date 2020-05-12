// <copyright file="EndOfTrack.cs" company="Shkyrockett">
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

using System;
using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// End of track.
    /// </summary>
    /// <remarks>
    /// <para>FF 2F 00</para>
    /// </remarks>
    /// <seealso cref="Engine.File.EventStatus" />
    [ElementName(nameof(EndOfTrack))]
    public class EndOfTrack
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndOfTrack" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EndOfTrack(IEventStatus status)
            : this(status, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndOfTrack" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The len.</param>
        /// <exception cref="FormatException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EndOfTrack(IEventStatus status, int length)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            Length = length;
            if (length != 0)
            {
                throw new FormatException($"{nameof(EndOfTrack)} Malformed");
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="EndOfTrack" />.
        /// </returns>
        internal static new EndOfTrack Read(BinaryReaderExtended reader, IEventStatus status) => new EndOfTrack(status, reader.Position == reader.Length ? 0 : reader.ReadVariableLengthInt());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "End Of Track";
    }
}
