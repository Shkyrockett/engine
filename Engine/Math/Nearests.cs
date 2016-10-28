// <copyright file="Nearests.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using static System.Math;
using static Engine.Maths;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Nearests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x_in"></param>
        /// <param name="y_in"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/36260793/algorithm-for-shortest-distance-from-a-point-to-an-elliptic-arc?rq=1
        /// </remarks>
        public static (double X, double Y) PointToPointOnEllipse(double x_in, double y_in, double x0, double y0, double rx, double ry, double a0, double a1)
        {
            int e;
            double aa, a, da, x, y, b0, b1;
            while (a0 >= a1) a0 -= Tau;                 // just make sure a0<a1
            b0 = a0; b1 = a1; da = (b1 - b0) / 25.0;          // 25 sample points in first iteration
            double ll = -1; aa = a0;                           // no best solution yet
            for (int i = 0; i < 3; i++)                       // recursions more means more accurate result
            {
                // sample arc a=<b0,b1> with step da
                for (e = 1, a = b0; e != 0; a += da)
                {
                    if (a >= b1) { a = b1; e = 0; }
                    // elliptic arc sampled point
                    x = x0 + rx * Cos(a);
                    y = y0 - ry * Sin(a);                 // mine y axis is in reverse order therefore -
                                                          // distance^2 to x_in,y_in
                    x -= x_in;
                    x *= x;
                    y -= y_in;
                    y *= y;
                    double l = x + y;
                    // remember best solution
                    if ((ll < 0d) || (ll > l)) { aa = a; ll = l; }
                }
                // use just area near found solution aa
                b0 = aa - da; if (b0 < a0) b0 = a0;
                b1 = aa + da; if (b1 > a1) b1 = a1;
                // 10 points per area stop if too small area already
                da = 0.1 * (b1 - b0); if (da < 1e-6) break;
            }
            // mine y axis is in reverse order therefore -
            return (x0 + rx * Cos(aa), y0 - ry * Sin(aa));
        }
    }
}
