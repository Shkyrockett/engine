// <copyright file="RectangleCorners.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum RectangleCorners
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
