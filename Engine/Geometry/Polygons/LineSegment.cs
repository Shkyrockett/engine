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
        #region Private Fields
        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        private PointF a;

        /// <summary>
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        private PointF b;
        #endregion

        #region Public Properties
        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        public LineSegment()
            : this(PointF.Empty, PointF.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        /// <remarks></remarks>
        public LineSegment(PointF a, PointF b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="xA">Horizontal component of starting point</param>
        /// <param name="yA">Vertical component of starting point</param>
        /// <param name="xB">Horizontal component of ending point</param>
        /// <param name="yB">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(int xA, int yA, int xB, int yB)
        {
            a = new PointF(xA, yA);
            b = new PointF(xB, yB);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(float x1, float y1, float X2, float Y2)
        {
            a = new PointF(x1, y1);
            b = new PointF(X2, Y2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        public LineSegment(PointF Point, double RadAngle, double Radius)
        {
            a = new PointF(Point.X, Point.Y);
            b = new PointF(
                (float)(Point.X + (Radius * Math.Cos(RadAngle))),
                (float)(Point.Y + (Radius * Math.Sin(RadAngle)))
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
        public PointF A
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
        public PointF B
        {
            get { return b; }
            set { b = value; }
        }

        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        /// <remarks></remarks>
        public PointF[] Points
        {
            get
            {
                return new PointF[] { a, b };
            }
            set
            {
                a = value[0];
                b = value[1];
            }
        }

        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Empty
        {
            get { return new LineSegment(0, 0, 0, 0); }
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
        public static PointF OffsetInterpolate(PointF Value1, PointF Value2, float Offset, float Weight)
        {
            VectorF UnitVectorAB = new VectorF(Value1, Value2);
            VectorF PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Interpolate(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }

        /// <summary>
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        public override RectangleF Bounds
        {
            get
            {
                return RectangleF.FromLTRB
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
            g.DrawLine(Style.ForePen, a, b);
        }

        /// <summary>
        /// Interpolates two points
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public PointF Interpolate(float index)
        {
            return new PointF(
                (a.X + (index * (b.X - a.X))),
                (a.Y + (index * (b.Y - a.Y)))
                );
        }

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public PointF Interpolate(double index)
        {
            return new PointF(
                (float)((a.X * (1 - index)) + (b.X * index)),
                (float)((a.Y * (1 - index)) + (b.Y * index))
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
        public static PointF Interpolate(PointF value1, PointF value2, double alpha)
        {
            return new PointF(
                (float)(value1.X + (alpha * (value2.X - value1.X))),
                (float)(value1.Y + (alpha * (value2.Y - value1.Y)))
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
        public static PointF Interpolate2(PointF Value1, PointF Value2, double Weight)
        {
            return new PointF(
                (float)(Value1.X + ((1 / (Value1.X - Value2.X)) * Weight)),
                (float)(Value1.Y + ((1 / (Value1.Y - Value2.Y)) * Weight))
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
        public static PointF Linear_Interpolate(PointF Y1, PointF Y2, float MU)
        {
            return ((PointF)(Y1.Scale(1 - MU)).Add(Y2.Scale(MU)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of points</returns>
        /// <remarks></remarks>
        public PointF[] ToArrayF()
        {
            return new PointF[] { a, b };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of points</returns>
        /// <remarks></remarks>
        public Point[] ToArray()
        {
            return new Point[] {
                 Point.Round(a),
                 Point.Round(b)};
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

