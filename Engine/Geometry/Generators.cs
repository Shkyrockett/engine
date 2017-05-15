// <copyright file="Generators.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Generators
    {
        #region Regular Polygons

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="count"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Contour RegularConvexPolygon(double x, double y, double radius, int count, double angle = -Right)
        {
            Point2D[] points = new Point2D[count];
            var theta = angle;
            var dtheta = Tau / count;
            for (var i = 0; i < count; i++)
            {
                points[i].X = x + (radius * Cos(theta));
                points[i].Y = y + (radius * Sin(theta));
                theta += dtheta;
            }

            return new Contour(points);
        }

        #endregion

        #region Heart Curves

        /// <summary>
        /// a is the constant of the original Cardioid.
        /// </summary>
        const double a = 1d;

        /// <summary>
        /// the minimum values of the phase angle "t".
        /// </summary>
        const double tmin = -PI * 0.5d;

        /// <summary>
        /// the maximum values of the phase angle "t".
        /// </summary>
        const double tmax = 3d * PI * 0.5d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <returns></returns>
        public static List<Point2D> HeartCurve(double x, double y, double radius, double alpha, double beta)
        {
            // its increment [radian] of a Cardioid before the conversion plotting interval of the phase angle "t" before conversion
            var dt = (tmax - tmin) / radius; // 10

            // Optimization
            var scale = radius * tmax / 1.855d;

            var points = new List<Point2D>();

            // Execution of calculation
            for (var t = tmin; t <= tmax; t = t + dt) // the phase angle [radian]
            {
                // The moving radius of a Cardioid after some conversion
                var r = 0d;

                // The phase angle [radian] of a Cardioid after some conversion
                var z = 0d;

                if (t > (tmax - dt) && t < (tmax + dt))
                {
                    z = tmin;
                }
                else if (t > (tmin - dt) && t < (tmin + dt))
                {
                    z = -tmin;
                }
                else
                {
                    var sinT = Sin(t); // Optimization to reduce the number of times sin() is called
                    r = a * Sqrt((5d - 3d * sinT) * (1d + sinT));
                    z = Asin(a * (1d - sinT) * Cos(t) / r);
                }

                // The phase angle [radian] of a Cardioid after the final conversion into a horned one
                var f = -alpha * z / PI + PI * 0.5d;

                points.Add(new Point2D(-(r * Cos(f)) * scale + x, -(beta * r * Sin(f)) * scale + y + radius));
            }

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static PolycurveContour HeartCurve(double x, double y, double radius)
        {
            // ToDo: Optimize algorithm to calculate minimum points for minimum curves.
            var heart = HeartCurve(x, y, radius, 0.9d, OneThird);
            var first = CurveFit.Fit(heart.Take(heart.Count / 2).ToList(), 0.5d);
            var last = CurveFit.Fit(heart.Skip(heart.Count / 2).ToList(), 0.5d);
            var heartCurve = new PolycurveContour(first);
            heartCurve.AddCubicBeziers(last);
            return heartCurve;
        }

        #endregion
    }
}
