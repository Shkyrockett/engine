// <copyright file="TuneRequest.cs" company="Shkyrockett">
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
        /// <param name="unknown"></param>
        public TuneRequest(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static TuneRequest Read(MidiBinaryReader reader, EventStatus status)
            => new TuneRequest(status);
    }
}
