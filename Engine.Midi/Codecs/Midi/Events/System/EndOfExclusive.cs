// <copyright file="EndOfExclusive.cs" company="Shkyrockett">
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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.File;

/// <summary>
/// End of Exclusive. Used to terminate a System Exclusive dump (see above).
/// </summary>
/// <remarks>
/// <para>nF 07 
/// Used to terminate a System Exclusive dump (see above).</para>
/// </remarks>
[ElementName(nameof(EndOfExclusive))]
public class EndOfExclusive
    : EventStatus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndOfExclusive" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    /// <param name="exclusive">The exclusive.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public EndOfExclusive(IEventStatus status, byte[] exclusive)
        : base((status?.DeltaTime).Value, status.Message, status.Channel)
    {
        Exclusive = exclusive;
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <value>
    /// The length.
    /// </value>
    public int Length => Exclusive.Length;

    /// <summary>
    /// Gets or sets the exclusive.
    /// </summary>
    /// <value>
    /// The exclusive.
    /// </value>
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public byte[] Exclusive { get; set; }

    /// <summary>
    /// Read.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="status">The status.</param>
    /// <returns>
    /// The <see cref="EndOfExclusive" />.
    /// </returns>
    internal static new EndOfExclusive Read(BinaryReaderExtended reader, IEventStatus status)
    {
        var buffer = reader.ReadVariableLengthBytes();
        return new EndOfExclusive(status, buffer);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => "End Of Exclusive";
}
