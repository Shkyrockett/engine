// <copyright file="StringFormat.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    public class TextFormat
    {
        public TextFormat(TextBoxFormatFlags formatFlags, int digitSubstitutionLanguage)
        {
            Format = formatFlags;
            Language = digitSubstitutionLanguage;
        }

        public TextBoxFormatFlags Format { get; set; }

        public int Language { get; set; }
    }

    [Flags]
    public enum TextBoxFormatFlags
    {
        DirectionRightToLeft = 1,
        DirectionVertical = 2,
        FitBlackBox = 4,
        DisplayFormatControl = 32,
        NoFontFallback = 1024,
        MeasureTrailingSpaces = 2048,
        NoWrap = 4096,
        LineLimit = 8192,
        NoClip = 16384,
    }
}