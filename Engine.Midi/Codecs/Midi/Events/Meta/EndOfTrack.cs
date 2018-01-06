// <copyright file="EndOfTrack.cs" company="Shkyrockett">
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

using System;

namespace Engine.File
{
    /// <summary>
    /// End of track.
    /// </summary>
    /// <remarks>
    /// FF 2F 00
    /// </remarks>
    [ElementName(nameof(EndOfTrack))]
    [DisplayName("End Of Track")]
    public class EndOfTrack
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="len"></param>
        /// <param name="status"></param>
        public EndOfTrack(byte len, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            if (len != 0)
                throw new FormatException($"{nameof(EndOfTrack)} Malformed");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static EndOfTrack Read(BinaryReaderExtended reader, EventStatus status)
            => new EndOfTrack(reader.Position == reader.Length ? (byte)0 : reader.ReadByte(), status);
    }
}
