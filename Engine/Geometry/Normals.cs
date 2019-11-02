using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Normals
    {
        /// <summary>
        /// Quadratics the bezier normal. https://github.com/LibreCAD/LibreCAD_3/blob/master/lckernel/cad/geometry/geobezier.cpp
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <returns></returns>
        public static (double X, double Y) QuadraticBezierNormal(double t, double ax, double ay, double bx, double by, double cx, double cy)
        {
            var Bx = bx - ax;
            var By = by - ay;
            var Ax = ax - (bx * 2d) + cx;
            var Ay = ay - (by * 2d) + cy;

            var tanx = Ay * t + By;
            var tany = -(Ax * t + Bx);

            var lNorm = Math.Sqrt(tanx * tanx + tany * tany);

            if (lNorm > 0)
            {
                tanx /= lNorm;
                tany /= lNorm;
            }

            return (tanx, tany);
        }
    }
}
