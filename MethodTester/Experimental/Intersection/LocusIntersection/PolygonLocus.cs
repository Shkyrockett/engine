// <copyright file="PolygonLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PolygonLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        public PolygonLocus()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        public PolygonLocus(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PolygonLocus(List<Point2D> points)
        {
            Points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator Contour(PolygonLocus locus)
            => new Contour(locus.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PolygonLocus Add(Point2D point)
        {
            Points.Add(point);
            return this;
        }
    }
}
