﻿// <copyright file="Start.cs" company="Shkyrockett">
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
    /// Start. Start the current sequence playing. (This message will be followed with Timing Clocks).
    /// </summary>
    /// <remarks>
    /// <para>nF 0A 
    /// Start the current sequence playing. (This message will be followed with Timing Clocks).</para>
    /// </remarks>
    [ElementName(nameof(Start))]
    [DisplayName(nameof(Start))]
    public class Start
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Start"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public Start(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Start"/>.</returns>
        internal static Start Read(BinaryReaderExtended reader, EventStatus status)
        {
            _ = reader;
            return new Start(status);
        }
    }
}
