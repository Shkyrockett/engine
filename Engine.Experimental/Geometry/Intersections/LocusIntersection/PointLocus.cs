// <copyright file="PointLocus.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    /// <summary>
    /// The point locus class.
    /// </summary>
    public class PointLocus
        : Locus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointLocus"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public PointLocus(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointLocus"/> class.
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
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double Y { get; set; }

        /// <param name="locus"></param>
        public static implicit operator Point2D(PointLocus locus)
            => new Point2D(locus.X, locus.Y);
    }
}
