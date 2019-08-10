// <copyright file="TextStyle.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
    /// The text style enum.
    /// </summary>
    [Flags]
    public enum TextStyles
    {
        /// <summary>
        /// The Regular = 0.
        /// </summary>
        Regular = 0,

        /// <summary>
        /// The Bold = 1.
        /// </summary>
        Bold = 1,

        /// <summary>
        /// The Italic = 2.
        /// </summary>
        Italic = 2,

        /// <summary>
        /// The Underline = 4.
        /// </summary>
        Underline = 4,

        /// <summary>
        /// The Strikeout = 8.
        /// </summary>
        Strikeout = 8,
    }
}
