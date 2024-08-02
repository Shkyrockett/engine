// <copyright file="PitchBend.cs" company="Shkyrockett">
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
/// Pitch Wheel Bend Status.
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>nE 0lllllll 0mmmmmmm
/// 0mmmmmmm This message is sent to indicate a change in the pitch bender (wheel or lever, typically).
/// The pitch bender is measured by a fourteen bit value. Center (no pitch change) is 2000H.
/// Sensitivity is a function of the transmitter.
/// (llllll) are the least significant 7 bits.
/// (mmmmmm) are the most significant 7 bits.</para>
/// </remarks>
[ElementName(nameof(PitchBend))]
public class PitchBend
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PitchBend" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="bend">The bend.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PitchBend(IEventStatus status, short bend)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        Bend = bend;
    }

    /// <summary>
    /// Gets or sets the bend.
    /// </summary>
    /// <value>
    /// The bend.
    /// </value>
    public short Bend { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="PitchBend" />.
    /// </returns>
    internal static new PitchBend Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadNetworkInt14());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Pitch Bend";
}
