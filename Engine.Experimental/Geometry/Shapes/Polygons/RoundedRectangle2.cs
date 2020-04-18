// <copyright file="RoundRectangle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
//using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
//using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// The rounded rectangle2 class.
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    public abstract class RoundedRectangle2
        : Shape2D
    {
        ///// <summary>
        ///// The bounds.
        ///// </summary>
        //private readonly Rectangle bounds;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //protected static void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);

        //    var path = Create(new Point2D(5, 5), new Size2D(20, 20));
        //    e.Graphics.DrawPath(Pens.Black, path);

        //    path = Create(new Point2D(30, 5), new Size2D(40, 40), 5);
        //    e.Graphics.FillPath(Brushes.Blue, path);

        //    path = Create(new Point2D(8, 50), new Size2D(50, 50), 5);
        //    e.Graphics.DrawPath(Pens.Black, path);

        //    e.Graphics.SetClip(path);
        //    using (var f = new Font("Tahoma", 12, FontStyle.Bold))
        //        e.Graphics.DrawString("Draw Me", f, Brushes.Red, 0, 70);
        //    e.Graphics.ResetClip();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="location"></param>
        ///// <param name="size"></param>
        ///// <param name="radius"></param>
        ///// <param name="corners"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Point2D location, Size2D size, double radius, RectangleCorners corners)
        //{
        //    var xw = location.X + size.Width;
        //    var yh = location.Y + size.Height;
        //    var xwr = xw - radius;
        //    var yhr = yh - radius;
        //    var xr = location.X + radius;
        //    var yr = location.Y + radius;
        //    var r2 = radius * 2;
        //    var xwr2 = xw - r2;
        //    var yhr2 = yh - r2;

        //    var p = new GraphicsPath();
        //    p.StartFigure();

        //    //Top Left Corner
        //    if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft)
        //    {
        //        p.AddArc((float)location.X, (float)location.Y, (float)r2, (float)r2, 180, 90);
        //    }
        //    else
        //    {
        //        p.AddLine((float)location.X, (float)yr, (float)location.X, (float)location.Y);
        //        p.AddLine((float)location.X, (float)location.Y, (float)xr, (float)location.Y);
        //    }

        //    //Top Edge
        //    p.AddLine((float)xr, (float)location.Y, (float)xwr, (float)location.Y);

        //    //Top Right Corner
        //    if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight)
        //    {
        //        p.AddArc((float)xwr2, (float)location.Y, (float)r2, (float)r2, 270, 90);
        //    }
        //    else
        //    {
        //        p.AddLine((float)xwr, (float)location.Y, (float)xw, (float)location.Y);
        //        p.AddLine((float)xw, (float)location.Y, (float)xw, (float)yr);
        //    }

        //    //Right Edge
        //    p.AddLine((float)xw, (float)yr, (float)xw, (float)yhr);

        //    //Bottom Right Corner
        //    if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight)
        //    {
        //        p.AddArc((float)xwr2, (float)yhr2, (float)r2, (float)r2, 0, 90);
        //    }
        //    else
        //    {
        //        p.AddLine((float)xw, (float)yhr, (float)xw, (float)yh);
        //        p.AddLine((float)xw, (float)yh, (float)xwr, (float)yh);
        //    }

        //    //Bottom Edge
        //    p.AddLine((float)xwr, (float)yh, (float)xr, (float)yh);

        //    //Bottom Left Corner
        //    if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft)
        //    {
        //        p.AddArc((float)location.X, (float)yhr2, (float)r2, (float)r2, 90, 90);
        //    }
        //    else
        //    {
        //        p.AddLine((float)xr, (float)yh, (float)location.X, (float)yh);
        //        p.AddLine((float)location.X, (float)yh, (float)location.X, (float)yhr);
        //    }

        //    //Left Edge
        //    p.AddLine((float)location.X, (float)yhr, (float)location.X, (float)yr);

        //    p.CloseFigure();
        //    return p;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="rect"></param>
        ///// <param name="radius"></param>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Rectangle2D rect, double radius, RectangleCorners c)
        //    => Create(rect.Location, rect.Size, radius, c);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="location"></param>
        ///// <param name="size"></param>
        ///// <param name="radius"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Point2D location, Size2D size, double radius)
        //    => Create(location, size, radius, RectangleCorners.All);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="rect"></param>
        ///// <param name="radius"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Rectangle2D rect, int radius)
        //    => Create(rect.Location, rect.Size, radius);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="location"></param>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Point2D location, Size2D size)
        //    => Create(location, size, 5);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="rect"></param>
        ///// <returns></returns>
        //public static GraphicsPath Create(Rectangle2D rect)
        //    => Create(rect.Location, rect.Size);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public override string ToString()
        //{
        //    if (this is null) return "BadRoundRectangle";
        //    return $"{"BadRoundRectangle"}{{L={bounds.Location},S={bounds.Size}}}";
        //}
    }
}
