// <copyright file="RectangleCorners.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
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
