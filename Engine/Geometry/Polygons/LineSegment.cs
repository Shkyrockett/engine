// <copyright file="LineSegment.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

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
        /// Creates a new Instance of LineSeg
        /// </summary>
        /// <param name="X1">Horizontal component of starting point</param>
        /// <param name="Y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(int X1, int Y1, int X2, int Y2)
        {
            a = new PointF(X1, Y1);
            b = new PointF(X2, Y2);
        }

        /// <summary>
        /// Creates a new Instance of LineSeg
        /// </summary>
        /// <param name="X1">Horizontal component of starting point</param>
        /// <param name="Y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(float X1, float Y1, float X2, float Y2)
        {
            a = new PointF(X1, Y1);
            b = new PointF(X2, Y2);
        }

        /// <summary>
        /// Creates a new Instance of Segment
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        /// <history>
        /// Shkyrockett[Alma Jenks] 16/May/2005      Created
        /// Shkyrockett[Alma Jenks] 15/December/2005 Updated to handle array method
        /// </history>
        public LineSegment(PointF Point, double RadAngle, double Radius)
        {
            a = new PointF(Point.X, Point.Y);
            b = new PointF(
                (float)(Point.X + (Radius * Math.Cos(RadAngle))),
                (float)(Point.Y + (Radius * Math.Sin(RadAngle))));
        }

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
        /// <history>
        /// Shkyrockett[Alma Jenks] 16/May/2005      Created
        /// Shkyrockett[Alma Jenks] 15/December/2005 Changed to property to use array
        /// </history>
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
        /// <history>
        /// Shkyrockett[Alma Jenks] 15/December/2005 Created
        /// </history>
        public new PointF[] Points
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
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Offset"></param>
        /// <param name="Weight"></param>
        /// <returns></returns>
        public static PointF OffsetInterpolate(PointF Value1, PointF Value2, float Offset, float Weight)
        {
            VectorF UnitVectorAB = new VectorF(Value1, Value2);
            VectorF PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Interpolate(Value1, Value2, Weight).Scale(PerpendicularAB);
        }

        /// <summary>
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        public RectangleF Bounds()
        {
            return RectangleF.FromLTRB(a.X, a.Y, b.X, b.Y);
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

