// <copyright file="PathExtensions.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        public static void AddArc(this List<Shape> path, Point2D pointA, Point2D pointB, Point2D pointC)
        {
            path.Add(new CircularArc(pointA, pointB, pointC));
        }
    }
}
