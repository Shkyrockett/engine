// <copyright file="Tempo.cs" company="Shkyrockett">
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
    /// Set tempo.
    /// </summary>
    /// <remarks>
    /// FF 51 03  TT TT TT
    /// </remarks>
    [ElementName(nameof(Tempo))]
    [DisplayName(nameof(Tempo))]
    public class Tempo
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="status"></param>
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
        /// <param name="status"></param>
        /// <returns></returns>
        internal static Tempo Read(BinaryReaderExtended reader, EventStatus status)
            => new Tempo(reader.ReadNetworkInt24(), status);
    }
}
