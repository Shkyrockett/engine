// <copyright file="Oval.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class Oval
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private PointF location;

        /// <summary>
        /// 
        /// </summary>
        private SizeF size;

        /// <summary>
        /// 
        /// </summary>
        public PointF Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public void Render(Graphics g)
        {
            DrawOval(g, new SolidBrush(Color.Beige), new Pen(Brushes.Black), location.X, location.Y, size.Width, size.Height);
        }

        /// <summary>
        /// Draw the notes as elongated circles. This should be called from the Paint event. Also come up with oter variations.
        /// </summary>
        /// <param name="GraphicsObject"></param>
        /// <param name="NoteBrush"></param>
        /// <param name="NotePen"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <remarks></remarks>
        private void DrawOval(Graphics GraphicsObject, Brush NoteBrush, Pen NotePen, float X, float Y, float Width, float Height)
        {
            //  Determine the orientation.
            float radius;
            if ((Height > Width))
            {
                radius = (Width / 2);
            }
            else
            {
                radius = (Height / 2);
            }
            //  Start the Path object.
            GraphicsPath GfxPath = new GraphicsPath();
            //  prepare the curves.
            GfxPath.AddArc((X + (Width - (radius * 2))), Y, (radius * 2), (radius * 2), 270, 90);
            GfxPath.AddArc((X + (Width - (radius * 2))), (Y + (Height - (radius * 2))), (radius * 2), (radius * 2), 0, 90);
            GfxPath.AddArc(X, (Y + (Height - (radius * 2))), (radius * 2), (radius * 2), 90, 90);
            GfxPath.AddArc(X, Y, (radius * 2), (radius * 2), 180, 90);
            //  Close the path .
            GfxPath.CloseFigure();
            //  Draw the path.
            GraphicsObject.FillPath(NoteBrush, GfxPath);
            GraphicsObject.DrawPath(NotePen, GfxPath);
            //  Clean up.
            GfxPath.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Oval";
        }
    }
}
