// <copyright file="PolyphonicPressure.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    /// Polyphonic Pressure (After-touch) Status.
    /// </summary>
    /// <remarks>
    /// <para>nA 0kkkkkkk 0vvvvvvv
    /// This message is most often sent by pressing down on the key after it "bottoms out". 
    /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the pressure value.</para>
    /// </remarks>
    [ElementName(nameof(PolyphonicPressure))]
    [DisplayName("Polyphonic Pressure")]
    public class PolyphonicPressure
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyphonicPressure"/> class.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="pressure">The pressure.</param>
        /// <param name="status">The status.</param>
        public PolyphonicPressure(byte note, byte pressure, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Note = note;
            Pressure = pressure;
        }

        /// <summary>
        /// Gets or sets the MIDI note (0x0 to 0x7F).
        /// </summary>
        public byte Note { get; set; }

        /// <summary>
        /// Gets or sets the pressure of the note (0x0 to 0x7F).
        /// </summary>
        public byte Pressure { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="PolyphonicPressure"/>.</returns>
        internal static PolyphonicPressure Read(BinaryReaderExtended reader, EventStatus status)
            => new PolyphonicPressure(reader.ReadByte(), reader.ReadByte(), status);
    }
}
