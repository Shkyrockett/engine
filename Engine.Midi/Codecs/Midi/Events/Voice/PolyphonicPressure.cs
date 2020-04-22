// <copyright file="PolyphonicPressure.cs" company="Shkyrockett">
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

using System.Runtime.CompilerServices;

namespace Engine.File
{
    /// <summary>
    /// Polyphonic Pressure (After-touch) Status.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nA 0kkkkkkk 0vvvvvvv
    /// This message is most often sent by pressing down on the key after it "bottoms out".
    /// (kkkkkkk) is the key (note) number. (vvvvvvv) is the pressure value.</para>
    /// </remarks>
    [ElementName(nameof(PolyphonicPressure))]
    public class PolyphonicPressure
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolyphonicPressure" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="note">The note.</param>
        /// <param name="pressure">The pressure.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolyphonicPressure(IEventStatus status, byte note, byte pressure)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Note, Pressure) = (note, pressure);
        }

        /// <summary>
        /// Gets or sets the MIDI note (0x0 to 0x7F).
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public byte Note { get; set; }

        /// <summary>
        /// Gets or sets the pressure of the note (0x0 to 0x7F).
        /// </summary>
        /// <value>
        /// The pressure.
        /// </value>
        public byte Pressure { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="PolyphonicPressure" />.
        /// </returns>
        internal static new PolyphonicPressure Read(BinaryReaderExtended reader, IEventStatus status) => new PolyphonicPressure(status, reader.ReadByte(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Polyphonic Pressure";
    }
}
