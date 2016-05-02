// <copyright file="IShape.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright> 
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>


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
        Rectangle2D Bounds { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //IStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool HitTest(Point2D point);
    }
}
