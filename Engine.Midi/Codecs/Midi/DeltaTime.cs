// <copyright file="DeltaTime.cs" company="Shkyrockett">
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
    /// The delta time class.
    /// </summary>
    public class DeltaTime
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public short Value { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => Value.ToString();

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The <see cref="DeltaTime"/>.</returns>
        internal static DeltaTime Read(BinaryReaderExtended reader)
            => new DeltaTime
            {
                Value = reader.ReadNetworkInt16()
            };
    }
}
