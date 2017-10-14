// <copyright file="RenderFont.cs" company="Shkyrockett" >
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
    public class RenderFont
    {
        public RenderFont(string name, double size, TextStyle textStyle)
        {
            Name = name;
            Size = size;
            Style = textStyle;
        }

        public string Name { get; set; }

        public double Size { get; set; }

        public TextStyle Style { get; set; }
    }

    [Flags]
    public enum TextStyle
    {
        Regular = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Strikeout = 8,
    }
}
