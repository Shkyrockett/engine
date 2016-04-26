// <copyright file="Star.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary>http://csharphelper.com/blog/2015/05/draw-stars-inside-polygons-in-c/</summary>

using Engine.Imaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("ElipticStar")]
    public class NGonStar
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        // Draw the stars. 
        private void picCanvas_Paint(object sender, PaintEventArgs e, int NumPoints, Rectangle bounds, bool chkHalfOnly, bool chkRelPrimeOnly)
        {
            if (NumPoints < 3) return;

            // Get the radii.
            int r1, r2, r3;
            r3 = Math.Min(bounds.Width, bounds.Height) / 2;
            r1 = r3 / 2;
            r2 = r3 / 4;
            r3 = r1 + r2;

            // Position variables.
            int cx = bounds.Width / 2;
            int cy = bounds.Height / 2;

            // Position the original points.
            PointF[] pts1 = new PointF[NumPoints];
            PointF[] pts2 = new PointF[NumPoints];
            double theta = -Math.PI / 2;
            double dtheta = 2 * Math.PI / NumPoints;
            for (int i = 0; i < NumPoints; i++)
            {
                pts1[i].X = (float)(r1 * Math.Cos(theta));
                pts1[i].Y = (float)(r1 * Math.Sin(theta));
                pts2[i].X = (float)(r2 * Math.Cos(theta));
                pts2[i].Y = (float)(r2 * Math.Sin(theta));
                theta += dtheta;
            }

            // Draw stars.
            int max = NumPoints - 1;
            if (chkHalfOnly) max = NumPoints / 2;
            for (int skip = 1; skip <= max; skip++)
            {
                // See if they are relatively prime.
                bool draw_all = !chkRelPrimeOnly;
                if (draw_all || GCD(skip, NumPoints) == 1)
                {
                    // Draw the big version of the star.
                    DrawStar(e.Graphics, cx, cy, pts1, skip, NumPoints);

                    // Draw the smaller version.
                    theta = -Math.PI / 2 + skip * 2 * Math.PI / NumPoints;
                    int x = (int)(cx + r3 * Math.Cos(theta));
                    int y = (int)(cy + r3 * Math.Sin(theta));

                    DrawStar(e.Graphics, x, y, pts2, skip, NumPoints);
                }
            }
        }

        // Return the greatest common divisor (GCD) of a and b.
        private long GCD(long a, long b)
        {
            long remainder;

            // Pull out remainders.
            for (;;)
            {
                remainder = a % b;
                if (remainder == 0) break;
                a = b;
                b = remainder;
            }

            return b;
        }

        // Draw a star centered at (x, y) using this skip value.
        private void DrawStar(Graphics gr, int x, int y, PointF[] orig_pts, int skip, int NumPoints)
        {
            // Make a PointF array with the points in the proper order.
            PointF[] pts = new PointF[NumPoints];
            for (int i = 0; i < NumPoints; i++)
            {
                pts[i] = orig_pts[(i * skip) % NumPoints];
            }

            // Draw the star.
            gr.TranslateTransform(x, y);
            gr.DrawPolygon(Pens.Blue, pts);
            gr.ResetTransform();
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
