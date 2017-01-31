// <copyright file="PolylineLocus.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PolylineLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        public PolylineLocus()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        public PolylineLocus(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PolylineLocus(List<Point2D> points)
            => Points = points;

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator Polyline(PolylineLocus locus)
            => new Polyline(locus.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PolylineLocus Add(Point2D point)
        {
            Points.Add(point);
            return this;
        }
    }
}
