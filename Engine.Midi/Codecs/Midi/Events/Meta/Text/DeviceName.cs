// <copyright file="DeviceName.cs" company="Shkyrockett">
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public DeviceName(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static DeviceName Read(BinaryReaderExtended reader, EventStatus status)
            => new DeviceName(reader.ReadASCIIString(), status);
    }
}
