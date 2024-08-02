// <copyright file="ChannelPressure.cs" company="Shkyrockett">
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
/// Channel After-touch Pressure Status.
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>nD 0vvvvvvv
/// This message is most often sent by pressing down on the key after it "bottoms out".
/// This message is different from polyphonic after-touch. Use this message to send the
/// single greatest pressure value (of all the current depressed keys). (vvvvvvv) is the pressure value.</para>
/// </remarks>
[ElementName(nameof(ChannelPressure))]
public class ChannelPressure
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChannelPressure" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="pressure">The pressure.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ChannelPressure(IEventStatus status, byte pressure)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        Pressure = pressure;
    }

    /// <summary>
    /// Gets or sets the pressure.
    /// </summary>
    /// <value>
    /// The pressure.
    /// </value>
    public byte Pressure { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="ChannelPressure" />.
    /// </returns>
    internal static new ChannelPressure Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadByte());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Channel Pressure";
}
