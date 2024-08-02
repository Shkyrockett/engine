// <copyright file="Tempo.cs" company="Shkyrockett">
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
/// Set tempo.
/// </summary>
/// <seealso cref="Engine.File.EventStatus" />
/// <remarks>
/// <para>FF 51 03  TT TT TT</para>
/// </remarks>
[ElementName(nameof(Tempo))]
public class Tempo
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tempo" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Tempo(IEventStatus status, int value)
        : this(status, 3, value)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tempo" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="length">The length.</param>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Tempo(IEventStatus status, int length, int value)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        (Value, Length) = (value, length);
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <value>
    /// The length.
    /// </value>
    public int Length { get; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public int Value { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="Tempo" />.
    /// </returns>
    internal static new Tempo Read(BinaryReaderExtended reader, IEventStatus status) => new(status, reader.ReadVariableLengthInt(), reader.ReadNetworkInt24());

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "Tempo";
}
