// <copyright file="Star.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-star-in-c/</summary>

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
    [DisplayName("ElipticStar")]
    public class ElipticStar
        : Polygon, IClosedShape
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num_points"></param>
        /// <param name="bounds"></param>
        /// <returns>Return PointFs to define a star.</returns>
        private PointF[] StarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            PointF[] pts = new PointF[num_points];

            double rx = bounds.Width / 2;
            double ry = bounds.Height / 2;
            double cx = bounds.X + rx;
            double cy = bounds.Y + ry;

            // Start at the top.
            double theta = -PI / 2;
            double dtheta = 4 * PI / num_points;
            for (int i = 0; i < num_points; i++)
            {
                pts[i] = new PointF(
                    (float)(cx + rx * Cos(theta)),
                    (float)(cy + ry * Sin(theta)));
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
            return "ElipticStar";
        }
    }
}
