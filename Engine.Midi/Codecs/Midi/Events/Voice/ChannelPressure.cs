// <copyright file="ChannelPressure.cs" company="Shkyrockett">
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
        /// Initializes a new instance of the <see cref="ChannelPressure"/> class.
        /// </summary>
        /// <param name="pressure">The pressure.</param>
        /// <param name="status">The status.</param>
        public ChannelPressure(byte pressure, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Pressure = pressure;
        }

        /// <summary>
        /// Gets or sets the pressure.
        /// </summary>
        public byte Pressure { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="ChannelPressure"/>.</returns>
        internal static ChannelPressure Read(BinaryReaderExtended reader, EventStatus status)
            => new ChannelPressure(reader.ReadByte(), status);
    }
}
