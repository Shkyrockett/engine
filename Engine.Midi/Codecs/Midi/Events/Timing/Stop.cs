// <copyright file="Stop.cs" company="Shkyrockett">
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
    /// Stop. Stop the current sequence.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>nF 0C
    /// Stop the current sequence.</para>
    /// </remarks>
    [ElementName(nameof(Stop))]
    public class Stop
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stop" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Stop(IEventStatus status)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="Stop" />.
        /// </returns>
        internal static new Stop Read(BinaryReaderExtended reader, IEventStatus status)
        {
            _ = reader;
            return new Stop(status);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => "Stop";
    }
}
