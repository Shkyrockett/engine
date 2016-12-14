// <copyright file="Nearests.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Nearests
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D ToPointOnLineSegment(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->P
            var diffAP = new Point2D(pX - aX, pY - aY);

            // Vector A->B
            var diffAB = new Point2D(bX - aX, bY - aY);
            double dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            double dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            //  # The normalized "distance" from a to the closest point
            double dist = dotABAP / dotAB;

            if (dist < 0)
                return new Point2D(aX, aY);
            else if (dist > dotABAP)
                return new Point2D(bX, bY);
            else
                return new Point2D(aX + diffAB.X * dist, aY + diffAB.Y * dist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="a0"></param>
        /// <param name="a1"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/36260793/algorithm-for-shortest-distance-from-a-point-to-an-elliptic-arc?rq=1
        /// </remarks>        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) ToPointOnEllipse(
            double cX, double cY,
            double rX, double rY,
            double a0, double a1,
            double pX, double pY)
        {
            int e;
            double a, x, y;

            while (a0 >= a1)
                // just make sure a0<a1
                a0 -= Tau;

            // 25 sample points in first iteration
            double b0 = a0;
            double b1 = a1;
            double da = (b1 - b0) / 25.0;

            // No best solution yet
            double ll = -1;
            double aa = a0;

            // More recursions means more accurate result
            for (int i = 0; i < 3; i++)
            {
                // sample arc a=<b0,b1> with step da
                for (e = 1, a = b0; e != 0; a += da)
                {
                    if (a >= b1) { a = b1; e = 0; }

                    // elliptic arc sampled point
                    x = cX + rX * Cos(a);
                    y = cY - rY * Sin(a);               // mine y axis is in reverse order therefore -
                                                        // distance^2 to x_in,y_in
                    x -= pX;
                    x *= x;
                    y -= pY;
                    y *= y;
                    double l = x + y;

                    // Remember best solution
                    if ((ll < 0d) || (ll > l))
                    {
                        aa = a;
                        ll = l;
                    }
                }

                // Use just area near found solution aa
                b0 = aa - da; if (b0 < a0) b0 = a0;
                b1 = aa + da; if (b1 > a1) b1 = a1;

                // 10 points per area stop if too small area already
                da = 0.1 * (b1 - b0); if (da < 1e-6) break;
            }

            // Mine y axis is in reverse order therefore -
            return (cX + rX * Cos(aa), cY - rY * Sin(aa));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0X"></param>
        /// <param name="p0Y"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? ToPointOnCubicBezier(
            double p0X, double p0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double pX, double pY)
        { return null; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0Y"></param>
        /// <param name="p0X"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y)? ToPointOnQuadraticBezier(
            double p0Y, double p0X,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double pX, double pY)
        { return null; }
    }
}
