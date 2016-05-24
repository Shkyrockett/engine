// <copyright file="Oval.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Oval")]
    public class Oval
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D location;

        /// <summary>
        /// 
        /// </summary>
        private SizeF size;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="g"></param>
        //public override void Render(Graphics g)
        //{
        //    DrawOval(g, new SolidBrush(Color.Beige), new Pen(Brushes.Black), location.X, location.Y, size.Width, size.Height);
        //}

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
        private void DrawOval(Graphics GraphicsObject, Brush NoteBrush, Pen NotePen, double X, double Y, double Width, double Height)
        {
            //  Determine the orientation.
            double radius;
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
            GfxPath.AddArc((float)(X + (Width - (radius * 2))), (float)Y, (float)(radius * 2), (float)(radius * 2), 270, 90);
            GfxPath.AddArc((float)(X + (Width - (radius * 2))), (float)(Y + (Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 0, 90);
            GfxPath.AddArc((float)X, (float)(Y + (Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 90, 90);
            GfxPath.AddArc((float)X, (float)Y, (float)(radius * 2), (float)(radius * 2), 180, 90);
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
            if (this == null) return nameof(Oval);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Oval), nameof(Location), location, nameof(Size), size);
        }
    }
}
