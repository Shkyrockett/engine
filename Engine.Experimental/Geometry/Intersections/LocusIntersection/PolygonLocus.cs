// <copyright file="PolygonLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
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
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    /// <summary>
    /// The polygon locus class.
    /// </summary>
    public class PolygonLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonLocus"/> class.
        /// </summary>
        public PolygonLocus()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonLocus"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolygonLocus(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonLocus"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolygonLocus(List<Point2D> points)
        {
            Points = points;
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator PolygonContour(PolygonLocus locus)
            => new PolygonContour(locus.Points);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="PolygonLocus"/>.</returns>
        public PolygonLocus Add(Point2D point)
        {
            Points.Add(point);
            return this;
        }
    }
}
