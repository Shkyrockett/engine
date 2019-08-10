// <copyright file="Stop.cs" company="Shkyrockett">
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
    /// Stop. Stop the current sequence.
    /// </summary>
    /// <remarks>
    /// <para>nF 0C 
    /// Stop the current sequence.</para>
    /// </remarks>
    [ElementName(nameof(Stop))]
    [DisplayName(nameof(Stop))]
    public class Stop
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stop"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public Stop(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Stop"/>.</returns>
        internal static Stop Read(BinaryReaderExtended reader, EventStatus status)
        {
            _ = reader;
            return new Stop(status);
        }
    }
}
