// <copyright file="Polyline.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polyline))]
    public class Polyline
        : Shape, IOpenShape
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

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

        #endregion

        #region Indexers

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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
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
        [XmlIgnore]
        public override double Perimeter
        {
            get
            {
                return points.Zip(points.Skip(1), Maths.Distance).Sum();
            }
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

        #endregion

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
        public Polyline Clone()
        {
            return new Polyline(Points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Polyline Offset(double offset)
        {
            Polyline polyline = new Polyline();

            LineSegment offsetLine = PrimitivesExtensions.OffsetSegment(Points[0], Points[1], offset);
            polyline.Add(offsetLine.A);

            for (int i = 2; i < Points.Count; i++)
            {
                LineSegment newOffsetLine = PrimitivesExtensions.OffsetSegment(Points[i - 1], Points[i], offset);
                polyline.Add(Experimental.Intersection2(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y)?.Item2);
                offsetLine = newOffsetLine;
            }

            polyline.Add(offsetLine.B);

            return polyline;
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
