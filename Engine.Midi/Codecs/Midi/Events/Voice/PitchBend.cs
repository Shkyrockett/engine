// <copyright file="PitchBend.cs" company="Shkyrockett">
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
    /// Pitch Wheel Bend Status.
    /// </summary>
    /// <remarks>
    /// nE 0lllllll 0mmmmmmm
    /// 0mmmmmmm This message is sent to indicate a change in the pitch bender (wheel or lever, typically).
    /// The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H.
    /// Sensitivity is a function of the transmitter.
    /// (llllll) are the least significant 7 bits.
    /// (mmmmmm) are the most significant 7 bits.
    /// </remarks>
    [ElementName(nameof(PitchBend))]
    [DisplayName("Pitch Bend")]
    public class PitchBend
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PitchBend"/> class.
        /// </summary>
        /// <param name="bend">The bend.</param>
        /// <param name="status">The status.</param>
        public PitchBend(short bend, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Bend = bend;
        }

        /// <summary>
        /// Gets or sets the bend.
        /// </summary>
        public short Bend { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="PitchBend"/>.</returns>
        internal static PitchBend Read(BinaryReaderExtended reader, EventStatus status)
            => new PitchBend(reader.ReadNetworkInt14(), status);
    }
}
