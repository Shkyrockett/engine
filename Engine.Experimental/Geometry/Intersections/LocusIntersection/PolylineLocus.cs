// <copyright file="PolylineLocus.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
    /// The polyline locus class.
    /// </summary>
    /// <seealso cref="Locus" />
    /// <remarks>
    /// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
    /// </remarks>
    public class PolylineLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineLocus" /> class.
        /// </summary>
        public PolylineLocus()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineLocus" /> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolylineLocus(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineLocus" /> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolylineLocus(List<Point2D> points)
        {
            Points = points;
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PolylineLocus"/> to <see cref="Polyline"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Polyline(PolylineLocus locus)
            => new Polyline(locus.Points);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="PolylineLocus" />.
        /// </returns>
        public PolylineLocus Add(Point2D point)
        {
            Points.Add(point);
            return this;
        }

        /// <summary>
        /// Converts to polyline.
        /// </summary>
        /// <returns></returns>
        public Polyline ToPolyline() => new Polyline(Points);
    }
}
