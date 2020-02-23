// <copyright file="Continue.cs" company="Shkyrockett">
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
    /// Continue. Continue at the point the sequence was Stopped.
    /// </summary>
    /// <remarks>
    /// <para>nF 0B 
    /// Continue at the point the sequence was Stopped.</para>
    /// </remarks>
    [ElementName(nameof(Continue))]
    [DisplayName(nameof(Continue))]
    public class Continue
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Continue"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public Continue(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Continue"/>.</returns>
        internal static Continue Read(BinaryReaderExtended reader, EventStatus status)
        {
            _ = reader;
            return new Continue(status);
        }
    }
}
