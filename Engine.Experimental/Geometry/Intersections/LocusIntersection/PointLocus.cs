// <copyright file="PointLocus.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The point locus class.
    /// </summary>
    /// <seealso cref="Locus" />
    /// <remarks>
    /// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
    /// </remarks>
    public class PointLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointLocus" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public PointLocus(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointLocus" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public PointLocus(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="PointLocus"/> to <see cref="Point2D"/>.
        /// </summary>
        /// <param name="locus">The locus.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Point2D(PointLocus locus) => new Point2D((locus?.X).Value, locus.Y);

        /// <summary>
        /// Converts to point2d.
        /// </summary>
        /// <returns></returns>
        public Point2D ToPoint2D() => new Point2D(X, Y);
    }
}
