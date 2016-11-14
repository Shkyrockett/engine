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
using System.Diagnostics.Contracts;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 2D Line Segment Structure
    /// </summary>
    /// <structure>Engine.Geometry.Segment2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(LineSegment))]
    [XmlType(TypeName = "line")]
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
        /// 
        /// </summary>
        private double ax;

        /// <summary>
        /// 
        /// </summary>
        private double ay;

        /// <summary>
        /// 
        /// </summary>
        private double bx;

        /// <summary>
        /// 
        /// </summary>
        private double by;

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
        public LineSegment((double x1, double y1, double x2, double y2) tuple)
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
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        public LineSegment(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D((Point.X + (Radius * Cos(RadAngle))), (Point.Y + (Radius * Sin(RadAngle)))))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        /// <remarks></remarks>
        public LineSegment(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public LineSegment(List<Point2D> points)
        {
            Points = points;
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
            get { return (Points as List<Point2D>)[index]; }
            set
            {
                (Points as List<Point2D>)[index] = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The first Point of a line segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D A
        {
            get { return new Point2D(ax, ay); }
            set
            {
                ax = value.X;
                ay = value.Y;
                OnPropertyChanged(nameof(A));
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the first Point of a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ax")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the first Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AX
        {
            get { return ax; }
            set
            {
                ax = value;
                OnPropertyChanged(nameof(AX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the first Point of a line segment.
        /// </summary>
        [XmlAttribute("ay")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the first Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AY
        {
            get { return ay; }
            set
            {
                ay = value;
                OnPropertyChanged(nameof(AY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        [Category("Properties")]
        [Description("The ending Point of a Line Segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D B
        {
            get { return new Point2D(bx, by); }
            set
            {
                bx = value.X;
                by = value.Y;
                OnPropertyChanged(nameof(B));
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the second Point of a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("bx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the second Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BX
        {
            get { return bx; }
            set
            {
                bx = value;
                OnPropertyChanged(nameof(BX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the second Point of a line segment.
        /// </summary>
        [XmlAttribute("by")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the second Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BY
        {
            get { return by; }
            set
            {
                by = value;
                OnPropertyChanged(nameof(BY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Rectangle2D.FromLTRB
            (
            A.X <= B.X ? A.X : B.X,
            A.Y <= B.Y ? A.Y : B.Y,
            A.X >= B.X ? A.X : B.X,
            A.Y >= B.Y ? A.Y : B.Y
            );

        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public List<Point2D> Points
        {
            get { return new List<Point2D> { A, B }; }
            set
            {
                A = value[0];
                B = value[1];
                OnPropertyChanged(nameof(Points));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public double Length
            => Distances.Distance(A.X, A.Y, B.X, B.Y);

        #endregion

        #region Interpolaters

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.Linear(A, B, t);

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            Point2D temp = A;
            A = B;
            B = temp;
            update?.Invoke();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of points</returns>
        /// <remarks></remarks>
        public Point2D[] ToArray() => new Point2D[] {
                 A,
                 B};

        /// <summary>
        /// Creates a string representation of this <see cref="LineSegment"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(LineSegment);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(LineSegment)}{{{nameof(A)}={A},{nameof(B)}={B}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}

