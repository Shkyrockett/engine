// <copyright file="Tempo.cs" company="Shkyrockett">
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
    /// Set tempo.
    /// </summary>
    /// <remarks>
    /// FF 51 03  TT TT TT
    /// </remarks>
    [ElementName(nameof(Tempo))]
    [DisplayName("Tempo")]
    public class Tempo
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unknown"></param>
        public Tempo(int value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Tempo Read(MidiBinaryReader reader, EventStatus status)
            => new Tempo(reader.ReadNetworkInt24(), status);
    }
}
