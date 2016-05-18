// <copyright file="Circle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Sine Curve")]
    public class Sine
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
        public Sine()
        {
            a = Point2D.Empty;
            b = Point2D.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public Sine(Point2D a, Point2D b)
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
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public Point2D Interpolate(double index)
        {
            return Interpolate(a, b, index);
        }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public static Point2D Interpolate(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Math.Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            double MU2 = (1.0 - Math.Sin(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Sine);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Sine), nameof(A), a, nameof(B), b);
        }
    }
}
