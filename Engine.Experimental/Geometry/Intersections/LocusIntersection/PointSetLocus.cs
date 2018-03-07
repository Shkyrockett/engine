// <copyright file="PointSetLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PointSetLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        public PointSetLocus()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        public PointSetLocus(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PointSetLocus(List<Point2D> points)
        {
            Points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polys"></param>
        public PointSetLocus(List<List<Point2D>> polys)
        {
            Points = new List<Point2D>();
            foreach (var set in polys)
                Points.Concat(set);
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator PointSet(PointSetLocus locus)
            => new PointSet(locus.Points);

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static explicit operator PointSetLocus(PolylineLocus locus)
            => new PointSetLocus(locus.Points);

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static explicit operator PointSetLocus(PolygonLocus locus)
            => new PointSetLocus(locus.Points);

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static explicit operator PointSetLocus(PolylineSetLocus locus)
            => new PointSetLocus(locus.Polylines);

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static explicit operator PointSetLocus(PolygonSetLocus locus)
            => new PointSetLocus(locus.Polygons);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PointSetLocus Add(Point2D point)
        {
            Points.Add(point);
            return this;
        }
    }
}
