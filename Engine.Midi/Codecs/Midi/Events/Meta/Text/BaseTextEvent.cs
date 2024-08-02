// <copyright file="Chunk.cs" company="Shkyrockett">
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

using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// Base for all Text events.
/// </summary>
/// <remarks>
/// <para>FF 01 len text</para>
/// </remarks>
public class BaseTextEvent
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseTextEvent"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="text">The text.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public BaseTextEvent(IEventStatus status, string text)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>The <see cref="BaseTextEvent"/>.</returns>
    internal static new BaseTextEvent Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadASCIIString());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"{base.ToString()}:{t}" : base.ToString();
}
