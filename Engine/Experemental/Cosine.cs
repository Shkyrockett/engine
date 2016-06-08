// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Globalization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Cosine Curve")]
    public class Cosine
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D a;

        /// <summary>
        /// 
        /// </summary>
        private Point2D b;

        /// <summary>
        /// 
        /// </summary>
        public Cosine()
        {
            a = Point2D.Empty;
            b = Point2D.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public Cosine(Point2D a, Point2D b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double index)
        {
            return Interpolate(a, b, index);
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public static Point2D Interpolate(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            double MU2 = (1.0 - Cos(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D CosineInterpolate(Point2D a, Point2D b, double Index)
        {
            double MU = ((1 - Cos((Index * 180))) / 2);
            return new Point2D(
                (a.X * (1 - MU)) + (b.X * MU),
                (a.Y * (1 - MU)) + (b.Y * MU)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Cosine);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Cosine), nameof(A), a, nameof(B), b);
        }
    }
}
