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
    /// <summary>
    /// 
    /// </summary>
    public class TextFormat
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatFlags"></param>
        /// <param name="digitSubstitutionLanguage"></param>
        public TextFormat(TextBoxFormatFlags formatFlags, int digitSubstitutionLanguage)
        {
            Format = formatFlags;
            Language = digitSubstitutionLanguage;
        }

        /// <summary>
        /// 
        /// </summary>
        public TextBoxFormatFlags Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Language { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum TextBoxFormatFlags
    {
        /// <summary>
        /// 
        /// </summary>
        DirectionRightToLeft = 1,

        /// <summary>
        /// 
        /// </summary>
        DirectionVertical = 2,

        /// <summary>
        /// 
        /// </summary>
        FitBlackBox = 4,

        /// <summary>
        /// 
        /// </summary>
        DisplayFormatControl = 32,

        /// <summary>
        /// 
        /// </summary>
        NoFontFallback = 1024,

        /// <summary>
        /// 
        /// </summary>
        MeasureTrailingSpaces = 2048,

        /// <summary>
        /// 
        /// </summary>
        NoWrap = 4096,

        /// <summary>
        /// 
        /// </summary>
        LineLimit = 8192,

        /// <summary>
        /// 
        /// </summary>
        NoClip = 16384,
    }
}