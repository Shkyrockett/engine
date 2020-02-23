// <copyright file="Tangents.cs" company="Shkyrockett" >
//     Copyright © 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using static System.Math;

namespace Engine
{
    /// <summary>
    /// The tangents class.
    /// </summary>
    public static class Tangents
    {
        /// <summary>
        /// Quadratics the bezier tangent. https://github.com/LibreCAD/LibreCAD_3/blob/master/lckernel/cad/geometry/geobezier.cpp
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <returns></returns>
        public static (double X, double Y) QuadraticBezierTangent(double t, double ax, double ay, double bx, double by, double cx, double cy)
        {
            var Bx = bx - ax;
            var By = by - ay;
            var Ax = ax - (bx * 2d) + cx;
            var Ay = ay - (by * 2d) + cy;
            var tanx = Ax * t + Bx;
            var tany = Ay * t + By;
            return (tanx, tany);
        }

        /// <summary>
        /// Cubics the bezier tangent. https://github.com/dbrizov/Unity-BezierCurves/blob/master/Assets/Bezier%20Curves/Scripts/BezierCurve3D.cs
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <returns></returns>
        public static (double X, double Y) CubicBezierTangent(double t, double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
        {
            var u = 1f - t;
            var u2 = u * u;
            var t2 = t * t;

            var (tanX, tanY) = (
                (-u2) * ax +
                u * (u - 2d * t) * bx -
                t * (t - 2d * u) * cx +
                t2 * dx,
                (-u2) * ay +
                u * (u - 2d * t) * by -
                t * (t - 2d * u) * cy +
                t2 * dy);
            var distance = Sqrt(tanX * tanX + tanY * tanY);
            return (tanX / distance, tanY / distance);
        }
    }
}
