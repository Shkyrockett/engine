// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Sine))]
    public class Sine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Sine()
        {
            A = Point2D.Empty;
            B = Point2D.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public Sine(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D A { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B { get; set; }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double t) => new Point2D(Interpolaters.Sine(A.X, A.Y, B.X, B.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Sine);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Sine), nameof(A), A, nameof(B), B);
        }
    }
}
