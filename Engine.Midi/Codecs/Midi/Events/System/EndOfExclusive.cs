// <copyright file="EndOfExclusive.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// End of Exclusive. Used to terminate a System Exclusive dump (see above).
    /// </summary>
    /// <remarks>
    /// nF 07 
    /// Used to terminate a System Exclusive dump (see above).
    /// </remarks>
    [ElementName(nameof(EndOfExclusive))]
    [DisplayName("End Of Exclusive")]
    public class EndOfExclusive
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public EndOfExclusive(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static EndOfExclusive Read(BinaryReaderExtended reader, EventStatus status)
            => new EndOfExclusive(status);
    }
}
