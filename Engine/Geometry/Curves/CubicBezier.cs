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
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace Engine
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
    [XmlType(TypeName = "bezier-Cubic")]
    public class CubicBezier
        : Shape, IOpenShape
    {
        #region Private Fields

        /// <summary>
        /// Position 1 x-coordinate.
        /// </summary>
        private double ax;

        /// <summary>
        /// Position 1 y-coordinate.
        /// </summary>
        private double ay;

        /// <summary>
        /// Tangent 1 x-coordinate.
        /// </summary>
        private double bx;

        /// <summary>
        /// Tangent 1 y-coordinate.
        /// </summary>
        private double by;

        /// <summary>
        /// Tangent 2 x-coordinate.
        /// </summary>
        private double cx;

        /// <summary>
        /// Tangent 2 y-coordinate.
        /// </summary>
        private double cy;

        /// <summary>
        /// Position 2 x-coordinate.
        /// </summary>
        private double dx;

        /// <summary>
        /// Position 2 y-coordinate.
        /// </summary>
        private double dy;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        public CubicBezier()
            : this(0, 0, 0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public CubicBezier(Point2D a, Point2D b, Point2D c)
        {
            var nodes = Interpolaters.QuadraticBezierToCubicBezier(a, b, c);
            ax = nodes[0].X;
            ay = nodes[0].Y;
            bx = nodes[1].X;
            by = nodes[1].Y;
            cx = nodes[2].X;
            cy = nodes[2].Y;
            dx = nodes[3].X;
            dy = nodes[3].Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public CubicBezier(double ax, double ay, double bx, double by, double cx, double cy)
        {
            var nodes = Interpolaters.QuadraticBezierToCubicBezier(ax, ay, bx, by, cx, cy);
            ax = nodes[0].X;
            ay = nodes[0].Y;
            bx = nodes[1].X;
            by = nodes[1].Y;
            cx = nodes[2].X;
            cy = nodes[2].Y;
            dx = nodes[3].X;
            dy = nodes[3].Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="a">Position1</param>
        /// <param name="b">Tangent1</param>
        /// <param name="c">Position2</param>
        /// <param name="d">Tangent2</param>
        public CubicBezier(Point2D a, Point2D b, Point2D c, Point2D d)
            : this(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezier"/> class.
        /// </summary>
        /// <param name="ax">Position1</param>
        /// <param name="ay">Position1</param>
        /// <param name="bx">Tangent1</param>
        /// <param name="by">Tangent1</param>
        /// <param name="cx">Position2</param>
        /// <param name="cy">Position2</param>
        /// <param name="dx">Tangent2</param>
        /// <param name="dy">Tangent2</param>
        public CubicBezier(double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy)
        {
            this.ax = ax;
            this.ay = ay;
            this.bx = bx;
            this.by = by;
            this.cx = cx;
            this.cy = cy;
            this.dx = dx;
            this.dy = dy;
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
        [XmlIgnore]
        [Category("Elements")]
        [Description("The first Point of a Cubic Bezier.")]
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
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the first Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ax")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the first Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the first Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("ay")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the first Point of a Cubic Bezier.")]
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
        /// 
        /// </summary>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The second Point of a Cubic Bezier.")]
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
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("bx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the second Point of a Cubic Bezier.")]
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
        /// Gets or sets the Y coordinate of the second Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("by")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the second Point of a Cubic Bezier.")]
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
        /// 
        /// </summary>
        [XmlIgnore]
        [Category("Elements")]
        [Description("The third Point of a Cubic Bezier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D C
        {
            get { return new Point2D(cx, cy); }
            set
            {
                cx = value.X;
                cy = value.Y;
                OnPropertyChanged(nameof(C));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the third Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("cx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the third Point of a Cubic Bezier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double CX
        {
            get { return cx; }
            set
            {
                bx = value;
                OnPropertyChanged(nameof(CX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the third Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("cy")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the third Point of a Cubic Bezier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double CY
        {
            get { return cy; }
            set
            {
                by = value;
                OnPropertyChanged(nameof(CY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D D
        {
            get { return new Point2D(dx, dy); }
            set
            {
                dx = value.X;
                dy = value.Y;
                OnPropertyChanged(nameof(D));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the fourth Point of a Cubic Bezier.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("dx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the fourth Point of a Cubic Bezier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double DX
        {
            get { return dx; }
            set
            {
                bx = value;
                OnPropertyChanged(nameof(DX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the fourth Point of a Cubic Bezier.
        /// </summary>
        [XmlAttribute("dy")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the fourth Point of a Cubic Bezier.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double DY
        {
            get { return dy; }
            set
            {
                by = value;
                OnPropertyChanged(nameof(DY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public double Length
            => Distances.CubicBezierArcLength(ax, ay, bx, by, cx, cy, dx, dy);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [XmlIgnore]
        public override Rectangle2D Bounds
            => Boundings.CubicBezier(ax, ay, bx, by, cx, cy, dx, dy);

        #endregion

        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
        {
            yield return new Point2D(Interpolaters.CubicBezier(A.X, A.Y, B.X, B.Y, C.X, C.Y, D.X, D.Y, Length));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="CubicBezier"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(CubicBezier);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CubicBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}{sep}{nameof(D)}={D}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
