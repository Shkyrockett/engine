// <copyright file="ChannelPressure.cs" company="Shkyrockett">
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
    /// Channel After-touch Pressure Status.
    /// </summary>
    /// <remarks>
    /// nD 0vvvvvvv
    /// This message is most often sent by pressing down on the key after it "bottoms out".
    /// This message is different from polyphonic after-touch. Use this message to send the
    /// single greatest pressure value (of all the current depressed keys). (vvvvvvv) is the pressure value.
    /// </remarks>
    [ElementName(nameof(ChannelPressure))]
    [DisplayName("Channel Pressure")]
    public class ChannelPressure
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name="status"></param>
        public ChannelPressure(byte pressure, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Pressure = pressure;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Pressure { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static ChannelPressure Read(BinaryReaderExtended reader, EventStatus status)
            => new ChannelPressure(reader.ReadByte(), status);
    }
}
