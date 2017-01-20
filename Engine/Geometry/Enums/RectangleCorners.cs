﻿// <copyright file="RectangleCorners.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum RectangleCorners
        : byte
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        TopLeft = 1,

        /// <summary>
        /// 
        /// </summary>
        TopRight = 2,

        /// <summary>
        /// 
        /// </summary>
        BottomLeft = 4,

        /// <summary>
        /// 
        /// </summary>
        BottomRight = 8,

        /// <summary>
        /// 
        /// </summary>
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }
}
