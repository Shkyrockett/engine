// <copyright file="DeviceName.cs" company="Shkyrockett">
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
/// Device (port) name.
/// </summary>
/// <remarks>
/// <para>FF 09 len text</para>
/// </remarks>
[ElementName(nameof(DeviceName))]
public class DeviceName
    : BaseTextEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceName"/> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="text">The text.</param>
    public DeviceName(IEventStatus status, string text)
        : base(status, text)
    { }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>The <see cref="DeviceName"/>.</returns>
    internal static new DeviceName Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadASCIIString());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => (Text is string t) && !string.IsNullOrWhiteSpace(t) ? $"Device Name: {t}" : "Device Name";
}
