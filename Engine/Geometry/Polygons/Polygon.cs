// <copyright file="Polygon.cs" >
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
    [DisplayName(nameof(Polygon))]
    public class Polygon
        : Shape, IClosedShape
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        public Polygon()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public Polygon(Polygon polygon)
            : this(polygon.points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polygon(ICollection<Point2D> points)
        {
            this.points = (List<Point2D>)points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public Polygon(ICollection<Polyline> polylines)
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
        public Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
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
        /// <param name="point"></param>
        public void Add(Point2D point)
        {
            Points.Add(point);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            Points.Reverse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Polygon Clone()
        {
            return new Polygon(points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public virtual Polygon Offset(double offset)
        {
            Polygon polyline = new Polygon();

            LineSegment offsetLine = PrimitivesExtensions.OffsetSegment(Points[Points.Count - 1], Points[0], offset);
            LineSegment startLine = offsetLine;

            for (int i = 1; i < Points.Count; i++)
            {
                LineSegment newOffsetLine = PrimitivesExtensions.OffsetSegment(Points[i - 1], Points[i], offset);
                polyline.Add(Experimental.Intersection2(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y).Item2);
                offsetLine = newOffsetLine;
            }

            polyline.Add(Experimental.Intersection2(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, startLine.A.X, startLine.A.Y, startLine.B.X, startLine.B.Y).Item2);

            return polyline;
        }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Render(Graphics g)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Polygon);
            StringBuilder pts = new StringBuilder();
            foreach (Point2D pt in points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}}}", nameof(Polygon), pts.ToString());
        }
    }
}
