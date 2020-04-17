// <copyright file="LineSegmentLocus.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
    /// The line segment locus class.
    /// </summary>
    /// <seealso cref="Locus" />
    /// <remarks>
    /// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
    /// </remarks>
    public class LineSegmentLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegmentLocus" /> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        public LineSegmentLocus(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegmentLocus" /> class.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        public LineSegmentLocus(double aX, double aY, double bX, double bY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY))
        { }

        /// <summary>
        /// Gets or sets the a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        public Point2D A { get; set; }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        public Point2D B { get; set; }

        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public List<Point2D> Points
            => new List<Point2D> { A, B };

        /// <summary>
        /// Performs an implicit conversion from <see cref="LineSegmentLocus"/> to <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LineSegment2D(LineSegmentLocus locus)
            => new LineSegment2D(locus.A, locus.B);

        /// <summary>
        /// Performs an implicit conversion from <see cref="LineSegmentLocus"/> to <see cref="Polyline"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Polyline2D(LineSegmentLocus locus)
            => new Polyline2D(locus.A, locus.B);

        /// <summary>
        /// Converts to linesegment.
        /// </summary>
        /// <returns></returns>
        public LineSegment2D ToLineSegment() => new LineSegment2D(A, B);

        /// <summary>
        /// Converts to polyline.
        /// </summary>
        /// <returns></returns>
        public Polyline2D ToPolyline() => new Polyline2D(A, B);
    }
}
