// <copyright file="DeviceName.cs" company="Shkyrockett">
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
    /// Device (port) name.
    /// </summary>
    /// <remarks>
    /// FF 09 len text
    /// </remarks>
    [ElementName(nameof(DeviceName))]
    [DisplayName("Device Name")]
    public class DeviceName
        : BaseTextEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceName"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public DeviceName(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>The <see cref="DeviceName"/>.</returns>
        internal static DeviceName Read(BinaryReaderExtended reader, EventStatus status)
            => new DeviceName(reader.ReadASCIIString(), status);
    }
}
