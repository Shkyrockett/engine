// <copyright file="PolygonSetLocus.cs" company="Shkyrockett" >
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
    /// <summary>
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PolygonSetLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        public PolygonSetLocus()
            : this(new List<List<Point2D>>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public PolygonSetLocus(IEnumerable<List<Point2D>> polygons)
        {
            Polygons = polygons as List<List<Point2D>>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public PolygonSetLocus(params IEnumerable<Point2D>[] polygons)
            : this(new List<List<Point2D>>(polygons as List<Point2D>[]))
        { }

        /// <summary>
        /// 
        /// </summary>
        public List<List<Point2D>> Polygons { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator Polygon(PolygonSetLocus locus)
            => new Polygon(locus.Polygons);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PolygonSetLocus Add(List<Point2D> point)
        {
            Polygons.Add(point);
            return this;
        }
    }
}
