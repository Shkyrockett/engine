// <copyright file="Start.cs" company="Shkyrockett">
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
    /// Start. Start the current sequence playing. (This message will be followed with Timing Clocks).
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nF 0A
    /// Start the current sequence playing. (This message will be followed with Timing Clocks).</para>
    /// </remarks>
    [ElementName(nameof(Start))]
    public class Start
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Start" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Start(IEventStatus status)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="Start" />.
        /// </returns>
        internal static new Start Read(BinaryReaderExtended reader, IEventStatus status)
        {
            _ = reader;
            return new Start(status);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Start";
    }
}
