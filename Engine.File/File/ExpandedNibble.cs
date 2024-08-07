﻿// <copyright file="ExpandedNibble.cs" company="Shkyrockett">
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp
// </references>

using System.Globalization;

namespace Engine.File;

/// <summary>
/// The expanded nibble struct.
/// </summary>
/// <!-- This class is based on several solutions found at: http://stackoverflow.com/questions/11607848/working-with-nibbles-in-c-sharp -->
public struct ExpandedNibble
    : IEquatable<ExpandedNibble>
{
    /// <summary>
    /// Creates an instance of the <see cref="ExpandedNibble"/> class.
    /// </summary>
    /// <param name="highNibble">Initial value for the y component.</param>
    /// <param name="lowNibble">Initial value for the x component.</param>
    public ExpandedNibble(byte highNibble, byte lowNibble)
    {
        (HighNibble, LowNibble) = (highNibble, lowNibble);
    }

    /// <summary>
    /// Gets or sets the high nibble.
    /// </summary>
    public byte HighNibble { get; set; }

    /// <summary>
    /// Gets or sets the low nibble.
    /// </summary>
    public byte LowNibble { get; set; }

    /// <summary>
    /// Gets or sets the packed value.
    /// </summary>
    private byte PackedValue
    {
        readonly get { return (byte)((HighNibble & (0xF0 << 4)) | (LowNibble & 0x0F)); }
        set
        {
            (HighNibble, LowNibble) = ((byte)(value >> 4), (byte)(value & 0x0F));
        }
    }

    /// <summary>
    /// The operator ==.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool operator ==(ExpandedNibble a, ExpandedNibble b) => a.PackedValue == b.PackedValue;

    /// <summary>
    /// The operator !=.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public static bool operator !=(ExpandedNibble a, ExpandedNibble b) => a.PackedValue != b.PackedValue;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public override bool Equals(object obj) => obj is ExpandedNibble nibble && Equals(nibble);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Equals(ExpandedNibble other) => PackedValue == other.PackedValue;

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override readonly string ToString() => $"HighNibble: {HighNibble.ToString(CultureInfo.InvariantCulture)} LowNibble: {LowNibble.ToString(CultureInfo.InvariantCulture)}";

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    public override int GetHashCode() => PackedValue;
}
