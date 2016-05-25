// <copyright file="CubicBezier.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// CubicBezier
    /// </summary>
    /// <structure>Engine.Geometry.CubicBezier2D</structure>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CubicBezier))]
    public class CubicBezier
        : Shape
    {
        #region Private Fields

        /// <summary>
        /// Position 1.
        /// </summary>
        [XmlAttribute()]
        private Point2D a;

        /// <summary>
        /// Tangent 1.
        /// </summary>
        [XmlAttribute()]
        private Point2D b;

        /// <summary>
        /// Position 2.
        /// </summary>
        [XmlAttribute()]
        private Point2D c;

        /// <summary>
        /// Tangent 2.
        /// </summary>
        [XmlAttribute()]
        private Point2D d;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        public CubicBezier()
            : this(Point2D.Empty, Point2D.Empty, Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        public CubicBezier(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        #endregion

        #region Indexers

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public Point2D this[double index]
        //{
        //    get { return Experimental.InterpolateCubicBezier(this, index); }
        //} 

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D C
        {
            get { return c; }
            set { c = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [XmlAttribute()]
        public Point2D D
        {
            get { return d; }
            set { d = value; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public List<Point2D> Points
        //{
        //    get { return points; }
        //    set { points = value; }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public double Length
        {
            get { return Experimental.CubicBezierArcLength(this); }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter => Length;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [XmlIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                int sortOfCloseLength = (int)Length;
                points = new List<Point2D>(InterpolatePoints(sortOfCloseLength));

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

        #region Interpolation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double index)
        {
            return Experimental.InterpolateCubicBezier(this, index);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return Experimental.InterpolateCubicBezier(this, Length);
        }

        #region Rendering

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pen"></param>
        /// <param name="points"></param>
        /// <param name="precision"></param>
        public void DrawCubicBezierCurve(PaintEventArgs e, Pen pen, Point2D[] points, double precision)
        {
            ////Point2D NewPoint;
            ////Point2D LastPoint = NewPoint;
            //e.Graphics.DrawLines(pen, CubicBeizerPoints(points, precision));
            ////for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            ////{
            ////    LastPoint = NewPoint;
            ////    NewPoint = CubicBeizerPoint(Points, Index);
            ////    e.Graphics.DrawLine(DPen, NewPoint, LastPoint);
            ////}
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(CubicBezier);
            return string.Format(CultureInfo.CurrentCulture, "{0}={{{1}={2},{3}={4},{5}={6},{7}={8}}}", nameof(CubicBezier), nameof(A), a, nameof(B), b, nameof(C), c, nameof(D), d);
        }
    }
}
