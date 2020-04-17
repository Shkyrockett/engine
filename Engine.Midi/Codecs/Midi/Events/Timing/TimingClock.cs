// <copyright file="TimingClock.cs" company="Shkyrockett">
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

namespace Engine.File
{
    /// <summary>
    /// Timing Clock. Sent 24 times per quarter note when synchronization is required (see text).
    /// </summary>
    /// <remarks>
    /// <para>nF 08 
    /// Sent 24 times per quarter note when synchronization is required (see text).</para>
    /// </remarks>
    [ElementName(nameof(TimingClock))]
    public class TimingClock
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimingClock"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public TimingClock(EventStatus status)
            : base((status?.DeltaTime).Value, status.Status, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="TimingClock"/>.</returns>
        internal static TimingClock Read(BinaryReaderExtended reader, EventStatus status)
        {
            _ = reader;
            return new TimingClock(status);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Timing Clock";
    }
}
