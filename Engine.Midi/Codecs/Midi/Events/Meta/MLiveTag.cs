// <copyright file="MLiveTag.cs" company="Shkyrockett">
//     Copyright © 2020 Shkyrockett. All rights reserved.
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
    /// M-Live Tag.
    /// </summary>
    /// <seealso cref="Engine.File.EventStatus" />
    /// <remarks>
    /// <para>FF 4B len tt text</para>
    /// <para>https://www.mixagesoftware.com/en/midikit/help/HTML/meta_events.html</para>
    /// </remarks>
    [ElementName(nameof(MLiveTag))]
    public class MLiveTag
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MLiveTag"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="text">The text.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MLiveTag(IEventStatus status, MusicLiveTag tag, string text)
            : this(status, (text?.Length ?? 0) + 1, tag, text)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLiveTag"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="length">The length.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="text">The text.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MLiveTag(IEventStatus status, int length, MusicLiveTag tag, string text)
            : base((status?.DeltaTime).Value, status.Message, status.Channel)
        {
            (Length, Tag, Text) = (length, tag, text);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public MusicLiveTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="TimeSignature" />.
        /// </returns>
        internal static new MLiveTag Read(BinaryReaderExtended reader, IEventStatus status)
        {
            var length = reader.ReadVariableLengthInt();
            return new MLiveTag(status, length, (MusicLiveTag)reader.ReadByte(), reader.ReadUTF8String(length - 1));
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => "M-Live Tag";
    }
}
