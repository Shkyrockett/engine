// <copyright file="MidiStart.cs" company="Shkyrockett">
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
/// MidiStart. Start the current sequence playing. (This message will be followed with Timing Clocks).
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>nF 0A
/// Start the current sequence playing. (This message will be followed with Timing Clocks).</para>
/// </remarks>
[ElementName(nameof(MidiStart))]
public class MidiStart
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MidiStart" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public MidiStart(IEventStatus status)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    { }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="MidiStart" />.
    /// </returns>
    internal static new MidiStart Read(BinaryReaderExtended reader, IEventStatus status)
    {
        _ = reader;
        return new MidiStart(status);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "MidiStart";
}
