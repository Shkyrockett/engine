// <copyright file="ControllerChange.cs" company="Shkyrockett">
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
/// Controller/Mode Change Status.
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>nB 0ccccccc 0vvvvvvv
/// This message is sent when a controller value changes.
/// Controllers include devices such as pedals and levers.
/// Controller numbers 120-127 are reserved as "Channel Mode Messages"
/// (below). (ccccccc) is the controller number (0-119).
/// (vvvvvvv) is the controller value (0-127).</para>
/// </remarks>
[ElementName(nameof(ControllerChange))]
public class ControllerChange
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ControllerChange" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="controller">The controller.</param>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ControllerChange(IEventStatus status, MidiControlChange controller, byte value)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        (Controller, Value) = (controller, value);
    }

    /// <summary>
    /// Gets or sets the controller.
    /// </summary>
    /// <value>
    /// The controller.
    /// </value>
    public MidiControlChange Controller { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public byte Value { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="ControllerChange" />.
    /// </returns>
    internal static new ControllerChange Read(BinaryReaderExtended reader, IEventStatus status) => new(status, (MidiControlChange)reader.ReadByte(), reader.ReadByte());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"Controller Change: {Controller} = {Value}";
}
