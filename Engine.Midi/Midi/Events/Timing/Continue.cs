// <copyright file="Continue.cs" company="Shkyrockett">
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
    /// Continue. Continue at the point the sequence was Stopped.
    /// </summary>
    /// <remarks>
    /// nF 0B 
    /// Continue at the point the sequence was Stopped.
    /// </remarks>
    [ElementName(nameof(Continue))]
    [DisplayName("Continue")]
    public class Continue
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public Continue(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static Continue Read(MidiBinaryReader reader, EventStatus status)
            => new Continue(status);
    }
}
