// <copyright file="Stop.cs" company="Shkyrockett">
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
    /// Stop. Stop the current sequence.
    /// </summary>
    /// <remarks>
    /// nF 0C 
    /// Stop the current sequence.
    /// </remarks>
    [ElementName(nameof(Stop))]
    [DisplayName(nameof(Stop))]
    public class Stop
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public Stop(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static Stop Read(BinaryReaderExtended reader, EventStatus status)
            => new Stop(status);
    }
}
