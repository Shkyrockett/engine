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

namespace Engine.File
{
    /// <summary>
    /// End of track.
    /// </summary>
    /// <remarks>
    /// <para>FF 2F 00</para>
    /// </remarks>
    [ElementName(nameof(EndOfTrack))]
    public class EndOfTrack
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndOfTrack"/> class.
        /// </summary>
        /// <param name="len">The len.</param>
        /// <param name="status">The status.</param>
        /// <exception cref="FormatException"></exception>
        public EndOfTrack(byte len, EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        {
            if (len != 0)
            {
                throw new FormatException($"{nameof(EndOfTrack)} Malformed");
            }
        }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="EndOfTrack"/>.</returns>
        internal static EndOfTrack Read(BinaryReaderExtended reader, EventStatus status) => new EndOfTrack(reader.Position == reader.Length ? (byte)0 : reader.ReadByte(), status);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "End Of Track";
    }
}
