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
using System.ComponentModel;
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
        //: Shape
        : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public Polyline()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polyline(ICollection<Point2D> points)
        {
            Points = (List<Point2D>)points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public Polyline(ICollection<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (Polyline polyline in polylines)
            {
                points.AddRange(polyline.Points);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public new Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public new List<Point2D> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                double left = points[0].X;
                double top = points[0].Y;
                double right = points[0].X;
                double bottom = points[0].Y;

                foreach (Point2D point in points)
                {
                    // ToDo: Measure performance impact of overwriting each time.
                    left = point.X <= left ? point.X : left;
                    top = point.Y <= top ? point.Y : top;
                    right = point.X >= right ? point.X : right;
                    bottom = point.Y >= bottom ? point.Y : bottom;
                }

                return Rectangle2D.FromLTRB(left, top, right, bottom);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public new void Add(Point2D point)
        {
            Points.Add(point);
        }

        /// <summary>
        /// 
        /// </summary>
        public new void Reverse()
        {
            Points.Reverse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new Polyline Clone()
        {
            return new Polyline(Points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public new Polyline Offset(double offset)
        {
            Polyline polyline = new Polyline();

            LineSegment offsetLine = Experimental.OffsetSegment(Points[0], Points[1], offset);
            polyline.Add(offsetLine.A);

            for (int i = 2; i < Points.Count; i++)
            {
                LineSegment newOffsetLine = Experimental.OffsetSegment(Points[i - 1], Points[i], offset);
                polyline.Add(Experimental.Intersect2(offsetLine.A, offsetLine.B, newOffsetLine.A, newOffsetLine.B));
                offsetLine = newOffsetLine;
            }

            polyline.Add(offsetLine.B);

            return polyline;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
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
