// <copyright file="Title.cs" company="Shkyrockett">
// Copyright © 2020 - 2024 Shkyrockett. All rights reserved.
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
/// Sequence title.
/// </summary>
/// <remarks>
/// <para>FF 10 len text</para>
/// <para>https://github.com/musescore/MuseScore/blob/master/miditools/midievent.h</para>
/// </remarks>
[ElementName(nameof(Title))]
public class Title
    : BaseTextEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Title"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="text">The text.</param>
    public Title(IEventStatus status, string text)
        : base(status, text)
    { }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>The <see cref="Title"/>.</returns>
    internal static new Title Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadASCIIString());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Title: {t}" : "Title";
}
