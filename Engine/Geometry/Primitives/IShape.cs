// <copyright file="IShape.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright> 
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// 
        /// </summary>
        RectangleF Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool HitTest(Point point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool HitTest(PointF point);
    }
}
