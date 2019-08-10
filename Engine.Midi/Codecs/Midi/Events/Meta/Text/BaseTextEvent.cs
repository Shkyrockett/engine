// <copyright file="Chunk.cs" company="Shkyrockett">
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
    /// Base for all Text events.
    /// </summary>
    /// <remarks>
    /// <para>FF 01 len text</para>
    /// </remarks>
    public abstract class BaseTextEvent
        : EventStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTextEvent"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        public BaseTextEvent(string text, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
    }
}
