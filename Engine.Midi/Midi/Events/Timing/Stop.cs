// <copyright file="Stop.cs" company="Shkyrockett">
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

namespace Engine.Midi
{
    /// <summary>
    /// Stop. Stop the current sequence.
    /// </summary>
    /// <remarks>
    /// nF 0C 
    /// Stop the current sequence.
    /// </remarks>
    [ElementName(nameof(Stop))]
    [DisplayName("Stop")]
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
        internal static Stop Read(MidiBinaryReader reader, EventStatus status)
            => new Stop(status);
    }
}
