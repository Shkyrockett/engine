// <copyright file="LineSegment.cs" >
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
using System.Xml.Serialization;
using static System.Math;

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
        : Shape, IOpenShape
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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        public LineSegment()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public LineSegment(Tuple<double, double, double, double> tuple)
            : this(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(double x1, double y1, double X2, double Y2)
            : this(new Point2D(x1, y1), new Point2D(X2, Y2))
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
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        public LineSegment(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D((Point.X + (Radius * Cos(RadAngle))), (Point.Y + (Radius * Sin(RadAngle)))))
        { }

        #endregion

        #region Properties

        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The first Point of a line segment")]
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
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        [Category("Properties")]
        [Description("The ending Point of a Line Segment")]
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
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
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
        public double Length
        {
            get { return Maths.Distance(a.X, a.Y, b.X, b.Y); }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            Point2D temp = A;
            A = B;
            B = temp;
        }

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double index)
        {
            return Maths.LinearInterpolate(a, b, index);
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
            if (this == null) return nameof(LineSegment);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(LineSegment), nameof(A), a, nameof(B), b);
        }
    }
}

