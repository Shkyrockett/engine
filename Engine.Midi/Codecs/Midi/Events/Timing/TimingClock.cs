// <copyright file="TimingClock.cs" company="Shkyrockett">
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
    /// Timing Clock. Sent 24 times per quarter note when synchronization is required (see text).
    /// </summary>
    /// <remarks>
    /// nF 08 
    /// Sent 24 times per quarter note when synchronization is required (see text).
    /// </remarks>
    [ElementName(nameof(TimingClock))]
    [DisplayName("Timing Clock")]
    public class TimingClock
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public TimingClock(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static TimingClock Read(BinaryReaderExtended reader, EventStatus status)
            => new TimingClock(status);
    }
}
