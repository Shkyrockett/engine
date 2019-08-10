// <copyright file="Tempo.cs" company="Shkyrockett">
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
    /// Set tempo.
    /// </summary>
    /// <remarks>
    /// <para>FF 51 03  TT TT TT</para>
    /// </remarks>
    [ElementName(nameof(Tempo))]
    [DisplayName(nameof(Tempo))]
    public class Tempo
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tempo"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="status">The status.</param>
        public Tempo(int value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="Tempo"/>.</returns>
        internal static Tempo Read(BinaryReaderExtended reader, EventStatus status)
            => new Tempo(reader.ReadNetworkInt24(), status);
    }
}
