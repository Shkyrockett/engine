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
    /// <summary>
    /// 
    /// </summary>
    public class RenderFont
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="textStyle"></param>
        public RenderFont(string name, double size, TextStyle textStyle)
        {
            Name = name;
            Size = size;
            Style = textStyle;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TextStyle Style { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum TextStyle
    {
        /// <summary>
        /// 
        /// </summary>
        Regular = 0,

        /// <summary>
        /// 
        /// </summary>
        Bold = 1,

        /// <summary>
        /// 
        /// </summary>
        Italic = 2,

        /// <summary>
        /// 
        /// </summary>
        Underline = 4,

        /// <summary>
        /// 
        /// </summary>
        Strikeout = 8,
    }
}
