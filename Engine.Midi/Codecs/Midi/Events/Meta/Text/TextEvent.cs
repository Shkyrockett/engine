﻿// <copyright file="TextEvent.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File;

/// <summary>
/// Text event.
/// </summary>
/// <remarks>
/// <para>FF 01 len text</para>
/// </remarks>
[ElementName(nameof(TextEvent))]
public class TextEvent
    : BaseTextEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextEvent"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="text">The text.</param>
    public TextEvent(IEventStatus status, string text)
        : base(status, text)
    { }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>The <see cref="TextEvent"/>.</returns>
    internal static new TextEvent Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadASCIIString());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Text Event: {t}" : "Text Event";
}
