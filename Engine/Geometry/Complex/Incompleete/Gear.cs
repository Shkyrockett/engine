// <copyright file="Gear.cs" >
//     Copyright (c) 2015 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Engine.Geometry
{
    /// <summary>
    /// http://csharphelper.com/blog/2015/08/animate-gears-with-unequal-sizes-in-c/
    /// http://csharphelper.com/blog/2015/08/draw-gears-in-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Gear))]
    public class Gear
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        // Draw the gear.
        private void picGears_Paint(object sender, PaintEventArgs e, Rectangle bounds)
        {
            // Draw smoothly.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            const double radius = 50;
            const double tooth_length = 10;
            double x = bounds.Width / 2 - radius - tooth_length - 1;
            double y = bounds.Height / 3;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightBlue, Pens.Blue, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);

            x += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightGreen, Pens.Green, new Point2D(x, y),
                radius, tooth_length, 10, 5, false);

            y += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.Pink, Pens.Red, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);
        }

        // Draw a gear.
        private void DrawGear(Graphics gr, Brush axle_brush, Brush gear_brush, Pen gear_pen, Point2D center, double radius, double tooth_length, int num_teeth, double axle_radius, bool start_with_tooth)
        {
            double dtheta = Math.PI / num_teeth;
            double dtheta_degrees = dtheta * 180 / Math.PI;     // dtheta in degrees.

            const double chamfer = 2;
            double tooth_width = radius * dtheta - chamfer;
            double alpha = tooth_width / (radius + tooth_length);
            double alpha_degrees = alpha * 180 / Math.PI;
            double phi = (dtheta - alpha) / 2;

            // Set theta for the beginning of the first tooth.
            double theta;
            if (start_with_tooth) theta = dtheta / 2;
            else theta = -dtheta / 2;

            // Make rectangles to represent the gear's inner and outer arcs.
            Rectangle2D inner_rect = new Rectangle2D(
                center.X - radius, center.Y - radius,
                2 * radius, 2 * radius);
            Rectangle2D outer_rect = new Rectangle2D(
                center.X - radius - tooth_length, center.Y - radius - tooth_length,
                2 * (radius + tooth_length), 2 * (radius + tooth_length));

            // Make a path representing the gear.
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < num_teeth; i++)
            {
                // Move across the gap between teeth.
                double degrees = theta * 180 / Math.PI;
                path.AddArc(inner_rect.ToRectangleF(), (float)degrees,(float) dtheta_degrees);
                theta += dtheta;

                // Move across the tooth's outer edge.
                degrees = (theta + phi) * 180 / Math.PI;
                path.AddArc(outer_rect.ToRectangleF(), (float)degrees, (float)alpha_degrees);
                theta += dtheta;
            }

            path.CloseFigure();

            // Draw the gear.
            gr.FillPath(gear_brush, path);
            gr.DrawPath(gear_pen, path);
            gr.FillEllipse(axle_brush,
                 (float)(center.X - axle_radius), (float)(center.Y - axle_radius),
                (float)(2 * axle_radius), (float)(2 * axle_radius));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Gear";
            return string.Format("{0}", "Gear");
        }
    }
}
