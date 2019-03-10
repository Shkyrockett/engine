// <copyright file="TimeSignature.cs" company="Shkyrockett">
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
    /// Time signature.
    /// </summary>
    /// <remarks>
    /// FF 58 04  NN DD CC BB
    /// </remarks>
    [ElementName(nameof(TimeSignature))]
    [DisplayName("Time Signature")]
    public class TimeSignature
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSignature"/> class.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <param name="clocks">The clocks.</param>
        /// <param name="beats">The beats.</param>
        /// <param name="status">The status.</param>
        public TimeSignature(byte numerator, byte denominator, byte clocks, byte beats, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Numerator = numerator;
            Denominator = denominator;
            Clocks = clocks;
            Beats = beats;
        }

        /// <summary>
        /// Gets or sets the numerator.
        /// </summary>
        public byte Numerator { get; set; }

        /// <summary>
        /// Gets or sets the denominator.
        /// </summary>
        public byte Denominator { get; set; }

        /// <summary>
        /// Gets or sets the clocks.
        /// </summary>
        public byte Clocks { get; set; }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        public byte Beats { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="TimeSignature"/>.</returns>
        internal static TimeSignature Read(BinaryReaderExtended reader, EventStatus status)
            => new TimeSignature(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), status);
    }
}
