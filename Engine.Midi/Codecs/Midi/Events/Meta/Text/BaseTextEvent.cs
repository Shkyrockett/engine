﻿// <copyright file="Chunk.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// FF 01 len text
    /// </remarks>
    public abstract class BaseTextEvent
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status"></param>
        public BaseTextEvent(string text, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
    }
}