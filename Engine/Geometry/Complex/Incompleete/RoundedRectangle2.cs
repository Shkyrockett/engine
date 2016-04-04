// <copyright file="RoundRectangle.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Bad Rounded Rectangle")]
    public abstract class RoundedRectangle2
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private Rectangle bounds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            GraphicsPath path = Create(new PointF(5, 5), new SizeF(20, 20));
            e.Graphics.DrawPath(Pens.Black, path);

            path = Create(new PointF(30, 5), new SizeF(40, 40), 5);
            e.Graphics.FillPath(Brushes.Blue, path);

            path = Create(new PointF(8, 50), new SizeF(50, 50), 5);
            e.Graphics.DrawPath(Pens.Black, path);

            e.Graphics.SetClip(path);
            using (Font f = new Font("Tahoma", 12, FontStyle.Bold))
                e.Graphics.DrawString("Draw Me!!", f, Brushes.Red, 0, 70);
            e.Graphics.ResetClip();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="radius"></param>
        /// <param name="corners"></param>
        /// <returns></returns>
        public static GraphicsPath Create(PointF location, SizeF size, float radius, RectangleCorners corners)
        {
            float xw = location.X + size.Width;
            float yh = location.Y + size.Height;
            float xwr = xw - radius;
            float yhr = yh - radius;
            float xr = location.X + radius;
            float yr = location.Y + radius;
            float r2 = radius * 2;
            float xwr2 = xw - r2;
            float yhr2 = yh - r2;

            GraphicsPath p = new GraphicsPath();
            p.StartFigure();

            //Top Left Corner
            if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft)
            {
                p.AddArc(location.X, location.Y, r2, r2, 180, 90);
            }
            else
            {
                p.AddLine(location.X, yr, location.X, location.Y);
                p.AddLine(location.X, location.Y, xr, location.Y);
            }

            //Top Edge
            p.AddLine(xr, location.Y, xwr, location.Y);

            //Top Right Corner
            if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight)
            {
                p.AddArc(xwr2, location.Y, r2, r2, 270, 90);
            }
            else
            {
                p.AddLine(xwr, location.Y, xw, location.Y);
                p.AddLine(xw, location.Y, xw, yr);
            }

            //Right Edge
            p.AddLine(xw, yr, xw, yhr);

            //Bottom Right Corner
            if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight)
            {
                p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
            }
            else
            {
                p.AddLine(xw, yhr, xw, yh);
                p.AddLine(xw, yh, xwr, yh);
            }

            //Bottom Edge
            p.AddLine(xwr, yh, xr, yh);

            //Bottom Left Corner
            if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft)
            {
                p.AddArc(location.X, yhr2, r2, r2, 90, 90);
            }
            else
            {
                p.AddLine(xr, yh, location.X, yh);
                p.AddLine(location.X, yh, location.X, yhr);
            }

            //Left Edge
            p.AddLine(location.X, yhr, location.X, yr);

            p.CloseFigure();
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static GraphicsPath Create(RectangleF rect, float radius, RectangleCorners c)
        { return Create(rect.Location, rect.Size, radius, c); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath Create(PointF location, SizeF size, float radius)
        { return Create(location, size, radius, RectangleCorners.All); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath Create(Rectangle rect, int radius)
        { return Create(rect.Location, rect.Size, radius); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static GraphicsPath Create(PointF location, SizeF size)
        { return Create(location, size, 5); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static GraphicsPath Create(Rectangle rect)
        { return Create(rect.Location, rect.Size); }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "BadRoundRectangle";
            return string.Format("{0}{{L={1},S={2}}}", "BadRoundRectangle", bounds.Location.ToString(), bounds.Size.ToString());
        }
    }
}
