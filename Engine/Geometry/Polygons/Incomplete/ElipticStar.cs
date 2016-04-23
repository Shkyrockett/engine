// <copyright file="Star.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary>http://csharphelper.com/blog/2014/08/draw-a-star-in-c/</summary>

using Engine.Imaging;
using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("ElipticStar")]
    public class ElipticStar
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }


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
            double theta = -Math.PI / 2;
            double dtheta = 4 * Math.PI / num_points;
            for (int i = 0; i < num_points; i++)
            {
                pts[i] = new PointF(
                    (float)(cx + rx * Math.Cos(theta)),
                    (float)(cy + ry * Math.Sin(theta)));
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
