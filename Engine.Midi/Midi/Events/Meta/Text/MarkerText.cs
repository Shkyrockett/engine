// <copyright file="MarkerText.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.Midi
{
    /// <summary
    /// >Marker Text.
    /// </summary>
    /// <remarks>
    /// FF 06 len text
    /// </remarks>
    [ElementName(nameof(MarkerText))]
    [DisplayName("Marker Text")]
    public class MarkerText
        : BaseTextEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public MarkerText(string text, EventStatus status)
            : base(text, status)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static MarkerText Read(MidiBinaryReader reader, EventStatus status)
            => new MarkerText(reader.ReadASCIIString(), status);
    }
}
