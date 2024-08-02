// <copyright file="StringFormat.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The text box format flags enum.
/// </summary>
[Flags]
public enum TextBoxFormatFlags
{
    /// <summary>
    /// The DirectionRightToLeft = 1.
    /// </summary>
    DirectionRightToLeft = 1,

    /// <summary>
    /// The DirectionVertical = 2.
    /// </summary>
    DirectionVertical = 2,

    /// <summary>
    /// The FitBlackBox = 4.
    /// </summary>
    FitBlackBox = 4,

    /// <summary>
    /// The DisplayFormatControl = 32.
    /// </summary>
    DisplayFormatControl = 32,

    /// <summary>
    /// The NoFontFallback = 1024.
    /// </summary>
    NoFontFallback = 1024,

    /// <summary>
    /// The MeasureTrailingSpaces = 2048.
    /// </summary>
    MeasureTrailingSpaces = 2048,

    /// <summary>
    /// The NoWrap = 4096.
    /// </summary>
    NoWrap = 4096,

    /// <summary>
    /// The LineLimit = 8192.
    /// </summary>
    LineLimit = 8192,

    /// <summary>
    /// The NoClip = 16384.
    /// </summary>
    NoClip = 16384,
}