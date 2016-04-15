// <copyright file="LineSegment.cs" >
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
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 2D Line Segment Structure
    /// </summary>
    /// <structure>Engine.Geometry.Segment2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Line Segment")]
    public class LineSegment
        : Shape
    {
        #region Static Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        /// <remarks></remarks>
        public static readonly LineSegment Empty = new LineSegment();
        #endregion

        #region Private Fields
        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        private Point2D a;

        /// <summary>
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        private Point2D b;
        #endregion

        #region Public Properties
        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        public LineSegment()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        /// <remarks></remarks>
        public LineSegment(Point2D a, Point2D b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(double x1, double y1, double X2, double Y2)
        {
            a = new Point2D(x1, y1);
            b = new Point2D(X2, Y2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        public LineSegment(Point2D Point, double RadAngle, double Radius)
        {
            a = new Point2D(Point.X, Point.Y);
            b = new Point2D(
                (Point.X + (Radius * Math.Cos(RadAngle))),
                (Point.Y + (Radius * Math.Sin(RadAngle)))
                );
        }
        #endregion

        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The first Point of a line segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public Point2D A
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The ending Point of a Line Segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(PointFConverter))]
        [XmlAttribute()]
        public Point2D B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        /// <remarks></remarks>
        public List<Point2D> Points
        {
            get
            {
                return new List<Point2D>() { a, b };
            }
            set
            {
                a = value[0];
                b = value[1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Offset"></param>
        /// <param name="Weight"></param>
        /// <returns></returns>
        public static Point2D OffsetInterpolate(Point2D Value1, Point2D Value2, double Offset, double Weight)
        {
            Vector2D UnitVectorAB = new Vector2D(Value1, Value2);
            Vector2D PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Interpolate(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }

        /// <summary>
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        public override Rectangle2D Bounds
        {
            get
            {
                return Rectangle2D.FromLTRB
                    (
                    a.X <= b.X ? a.X : b.X,
                    a.Y <= b.Y ? a.Y : b.Y,
                    a.X >= b.X ? a.X : b.X,
                    a.Y >= b.Y ? a.Y : b.Y
                    );
            }
        }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        public override void Render(Graphics g)
        {
            //g.DrawLine(Style.ForePen, a, b);
        }

        /// <summary>
        /// Interpolates two points
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Point2D Interpolate(float index)
        {
            return new Point2D(
                (a.X + (index * (b.X - a.X))),
                (a.Y + (index * (b.Y - a.Y)))
                );
        }

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public Point2D Interpolate(double index)
        {
            return new Point2D(
                (a.X * (1 - index)) + (b.X * index),
                (a.Y * (1 - index)) + (b.Y * index)
                );
        }

        /// <summary>
        /// Interpolates two points
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate(Point2D value1, Point2D value2, double alpha)
        {
            return new Point2D(
                value1.X + (alpha * (value2.X - value1.X)),
                value1.Y + (alpha * (value2.Y - value1.Y))
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Weight"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate2(Point2D Value1, Point2D Value2, double Weight)
        {
            return new Point2D(
                Value1.X + ((1 / (Value1.X - Value2.X)) * Weight),
                Value1.Y + ((1 / (Value1.Y - Value2.Y)) * Weight)
                );
        }

        /// <summary>
        /// Function For normal Line
        /// </summary>
        /// <param name="Y1"></param>
        /// <param name="Y2"></param>
        /// <param name="MU"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Linear_Interpolate(Point2D Y1, Point2D Y2, double MU)
        {
            return (Point2D)(Y1.Scale(1 - MU)).Add(Y2.Scale(MU));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of points</returns>
        /// <remarks></remarks>
        public Point2D[] ToArray()
        {
            return new Point2D[] {
                 a,
                 b};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "LineSegment";
            return string.Format("{0}{{A={1},B={2}}}", "LineSegment", a.ToString(), b.ToString());
        }
    }
}

