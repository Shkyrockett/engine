// <copyright file="RectangleCorners.cs" company="Shkyrockett" >
//     Copyright © 2015 - 2020 Shkyrockett. All rights reserved.
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
    /// The rectangle corners enum.
    /// </summary>
    [Flags]
    public enum RectangleCorners
        : byte
    {
        /// <summary>
        /// The None = 0.
        /// </summary>
        None = 0,

        /// <summary>
        /// The TopLeft = 1.
        /// </summary>
        TopLeft = 1,

        /// <summary>
        /// The TopRight = 2.
        /// </summary>
        TopRight = 2,

        /// <summary>
        /// The BottomLeft = 4.
        /// </summary>
        BottomLeft = 4,

        /// <summary>
        /// The BottomRight = 8.
        /// </summary>
        BottomRight = 8,

        /// <summary>
        /// The All = TopLeft | TopRight | BottomLeft | BottomRight.
        /// </summary>
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }
}
