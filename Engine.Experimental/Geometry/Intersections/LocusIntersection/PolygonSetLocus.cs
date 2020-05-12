// <copyright file="PolygonSetLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
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
    /// The polygon set locus class.
    /// </summary>
    /// <seealso cref="Locus" />
    /// <remarks>
    /// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
    /// </remarks>
    public class PolygonSetLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSetLocus" /> class.
        /// </summary>
        public PolygonSetLocus()
            : this(new List<List<Point2D>>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSetLocus" /> class.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        public PolygonSetLocus(IEnumerable<List<Point2D>> polygons)
        {
            Polygons = polygons as List<List<Point2D>>;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSetLocus" /> class.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        public PolygonSetLocus(params IEnumerable<Point2D>[] polygons)
            : this(new List<List<Point2D>>(polygons as List<Point2D>[]))
        { }

        /// <summary>
        /// Gets or sets the polygons.
        /// </summary>
        /// <value>
        /// The polygons.
        /// </value>
        public List<List<Point2D>> Polygons { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PolygonSetLocus"/> to <see cref="Polygon2D"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Polygon2D(PolygonSetLocus locus) => new Polygon2D(locus?.Polygons);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="PolygonSetLocus" />.
        /// </returns>
        public PolygonSetLocus Add(List<Point2D> point)
        {
            Polygons.Add(point);
            return this;
        }

        /// <summary>
        /// Converts to polygon.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Polygon2D ToPolygon2D() => new Polygon2D(Polygons);
    }
}
