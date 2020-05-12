// <copyright file="TimeSignature.cs" company="Shkyrockett">
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
    /// Time signature.
    /// </summary>
    /// <remarks>
    /// <para>FF 58 04  NN DD CC BB</para>
    /// </remarks>
    [ElementName(nameof(TimeSignature))]
    public class TimeSignature
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSignature" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <param name="clocks">The clocks.</param>
        /// <param name="beats">The beats.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TimeSignature(IEventStatus status, byte numerator, byte denominator, byte clocks, byte beats)
            : this(status, 4, numerator, denominator, clocks, beats)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSignature" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <param name="clocks">The clocks.</param>
        /// <param name="beats">The beats.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TimeSignature(IEventStatus status, int length, byte numerator, byte denominator, byte clocks, byte beats)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Numerator, Denominator, Clocks, Beats) = (length, numerator, denominator, clocks, beats);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the numerator.
        /// </summary>
        /// <value>
        /// The numerator.
        /// </value>
        public byte Numerator { get; set; }

        /// <summary>
        /// Gets or sets the denominator.
        /// </summary>
        /// <value>
        /// The denominator.
        /// </value>
        public byte Denominator { get; set; }

        /// <summary>
        /// Gets or sets the clocks.
        /// </summary>
        /// <value>
        /// The clocks.
        /// </value>
        public byte Clocks { get; set; }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        /// <value>
        /// The beats.
        /// </value>
        public byte Beats { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="TimeSignature" />.
        /// </returns>
        internal static new TimeSignature Read(BinaryReaderExtended reader, IEventStatus status) => new TimeSignature(status, reader.ReadVariableLengthInt(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Time Signature";
    }
}
