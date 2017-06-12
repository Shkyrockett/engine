﻿// <copyright file="TuneRequest.cs" company="Shkyrockett">
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
    /// Tune Request. Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.
    /// </summary>
    /// <remarks>
    /// nF 06 
    /// Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.
    /// </remarks>
    [ElementName(nameof(TuneRequest))]
    [DisplayName("Tune Request")]
    public class TuneRequest
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public TuneRequest(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static TuneRequest Read(BinaryReaderExtended reader, EventStatus status)
            => new TuneRequest(status);
    }
}
