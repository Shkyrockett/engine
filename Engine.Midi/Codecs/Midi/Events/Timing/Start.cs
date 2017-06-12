// <copyright file="Start.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// Start. Start the current sequence playing. (This message will be followed with Timing Clocks).
    /// </summary>
    /// <remarks>
    /// nF 0A 
    /// Start the current sequence playing. (This message will be followed with Timing Clocks).
    /// </remarks>
    [ElementName(nameof(Start))]
    [DisplayName("Start")]
    public class Start
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public Start(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static Start Read(BinaryReaderExtended reader, EventStatus status)
            => new Start(status);
    }
}
