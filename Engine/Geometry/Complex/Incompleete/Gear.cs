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
    [DisplayName("Gear Shape")]
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

            const float radius = 50;
            const float tooth_length = 10;
            float x = bounds.Width / 2 - radius - tooth_length - 1;
            float y = bounds.Height / 3;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightBlue, Pens.Blue, new PointF(x, y),
                radius, tooth_length, 10, 5, true);

            x += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightGreen, Pens.Green, new PointF(x, y),
                radius, tooth_length, 10, 5, false);

            y += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.Pink, Pens.Red, new PointF(x, y),
                radius, tooth_length, 10, 5, true);
        }

        // Draw a gear.
        private void DrawGear(Graphics gr, Brush axle_brush, Brush gear_brush, Pen gear_pen, PointF center, float radius, float tooth_length, int num_teeth, float axle_radius, bool start_with_tooth)
        {
            float dtheta = (float)(Math.PI / num_teeth);
            float dtheta_degrees = (float)(dtheta * 180 / Math.PI);     // dtheta in degrees.

            const float chamfer = 2;
            float tooth_width = radius * dtheta - chamfer;
            float alpha = tooth_width / (radius + tooth_length);
            float alpha_degrees = (float)(alpha * 180 / Math.PI);
            float phi = (dtheta - alpha) / 2;

            // Set theta for the beginning of the first tooth.
            float theta;
            if (start_with_tooth) theta = dtheta / 2;
            else theta = -dtheta / 2;

            // Make rectangles to represent the gear's inner and outer arcs.
            RectangleF inner_rect = new RectangleF(
                center.X - radius, center.Y - radius,
                2 * radius, 2 * radius);
            RectangleF outer_rect = new RectangleF(
                center.X - radius - tooth_length, center.Y - radius - tooth_length,
                2 * (radius + tooth_length), 2 * (radius + tooth_length));

            // Make a path representing the gear.
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < num_teeth; i++)
            {
                // Move across the gap between teeth.
                float degrees = (float)(theta * 180 / Math.PI);
                path.AddArc(inner_rect, degrees, dtheta_degrees);
                theta += dtheta;

                // Move across the tooth's outer edge.
                degrees = (float)((theta + phi) * 180 / Math.PI);
                path.AddArc(outer_rect, degrees, alpha_degrees);
                theta += dtheta;
            }

            path.CloseFigure();

            // Draw the gear.
            gr.FillPath(gear_brush, path);
            gr.DrawPath(gear_pen, path);
            gr.FillEllipse(axle_brush,
                center.X - axle_radius, center.Y - axle_radius,
                2 * axle_radius, 2 * axle_radius);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Gear";
        }
    }
}
