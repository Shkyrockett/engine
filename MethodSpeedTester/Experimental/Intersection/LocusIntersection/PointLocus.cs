// <copyright file="PointLocus.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    /// <remarks> This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178 </remarks>
    public class PointLocus
        : Locus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PointLocus(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PointLocus(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="locus"></param>
        public static implicit operator Point2D(PointLocus locus)
            => new Point2D(locus.X, locus.Y);
    }
}
