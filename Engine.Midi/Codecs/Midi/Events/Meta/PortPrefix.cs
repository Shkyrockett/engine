// <copyright file="MIDIPort.cs" company="Shkyrockett">
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
/// MIDI Port (not official?).
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>FF 21 01  pp</para>
/// </remarks>
[ElementName(nameof(PortPrefix))]
public class PortPrefix
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PortPrefix" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="port">The port.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PortPrefix(IEventStatus status, byte port)
        : this(status, 1, port)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PortPrefix" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="length">The length.</param>
    /// <param name="port">The port.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal PortPrefix(IEventStatus status, int length, byte port)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        (Length, Port) = (length, port);
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <value>
    /// The length.
    /// </value>
    public int Length { get; }

    /// <summary>
    /// Gets or sets the port.
    /// </summary>
    /// <value>
    /// The port.
    /// </value>
    public byte Port { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="PortPrefix" />.
    /// </returns>
    internal static new PortPrefix Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadVariableLengthInt(), reader.ReadByte());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"Port Prefix: {Port}";
}
