// <copyright file="EndOfExclusive.cs" company="Shkyrockett">
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
    /// End of Exclusive. Used to terminate a System Exclusive dump (see above).
    /// </summary>
    /// <remarks>
    /// <para>nF 07 
    /// Used to terminate a System Exclusive dump (see above).</para>
    /// </remarks>
    [ElementName(nameof(EndOfExclusive))]
    [DisplayName("End Of Exclusive")]
    public class EndOfExclusive
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndOfExclusive"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public EndOfExclusive(EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="EndOfExclusive"/>.</returns>
        internal static EndOfExclusive Read(BinaryReaderExtended reader, EventStatus status)
        {
            _ = reader;
            return new EndOfExclusive(status);
        }
    }
}
