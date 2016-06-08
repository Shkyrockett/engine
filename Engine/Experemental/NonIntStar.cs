// <copyright file="Star.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-non-intersecting-star-in-c/</summary>

using System;
using System.ComponentModel;
using System.Drawing;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Star")]
    public class NonIntStar
        : Polygon
    {
        // Return PointFs to define a non-intersecting star.
        private PointF[] NonIntersectingStarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            PointF[] pts = new PointF[2 * num_points];

            double rx1 = bounds.Width / 2;
            double ry1 = bounds.Height / 2;
            double rx2 = rx1 * 0.5;
            double ry2 = ry1 * 0.5;
            double cx = bounds.X + rx1;
            double cy = bounds.Y + ry1;

            // Start at the top.
            double theta = -PI / 2;
            double dtheta = PI / num_points;
            for (int i = 0; i < 2 * num_points; i += 2)
            {
                pts[i] = new PointF(
                    (float)(cx + rx1 * Cos(theta)),
                    (float)(cy + ry1 * Sin(theta)));
                theta += dtheta;

                pts[i + 1] = new PointF(
                    (float)(cx + rx2 * Cos(theta)),
                    (float)(cy + ry2 * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "ElipticStar";
            return "Star";
        }
    }
}
