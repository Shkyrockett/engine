// <copyright file="Polyline.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Polyline")]
    public class Polyline
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        public Polyline()
        {
            base.Points = new List<Point2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polyline(List<Point2D> points)
        {
            base.Points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Render(Graphics g)
        {
            //g.FillPolygon(Style.BackBrush, Points.ToArray());
            //g.DrawLines(Style.ForePen, Points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Polyline);
            StringBuilder pts = new StringBuilder();
            foreach (Point2D pt in Points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}}}", nameof(Polyline), pts.ToString());
        }
    }
}
