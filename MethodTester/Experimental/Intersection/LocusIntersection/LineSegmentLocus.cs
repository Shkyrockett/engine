// <copyright file="LineSegmentLocus.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
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
    public class LineSegmentLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public LineSegmentLocus(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        public LineSegmentLocus(double aX, double aY, double bX, double bY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY))
        { }

        /// <summary>
        /// 
        /// </summary>
        public Point2D A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
            => new List<Point2D> { A, B };

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator LineSegment(LineSegmentLocus locus)
            => new LineSegment(locus.A, locus.B);

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator Polyline(LineSegmentLocus locus)
            => new Polyline(locus.A, locus.B);
    }
}
