﻿// <copyright file="QuadraticBezier.cs" company="Shkyrockett" >
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
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// QuadraticBezier2D
    /// </summary>
    /// <remarks>
    /// http://paulbourke.net/geometry/bezier/index.html
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(QuadraticBezier))]
    [XmlType(TypeName = "bezier-Quadratic")]
    public class QuadraticBezier
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
        /// Tangent x-coordinate.
        /// </summary>
        private double bx;

        /// <summary>
        /// Tangent y-coordinate.
        /// </summary>
        private double by;

        /// <summary>
        /// Position 2 x-coordinate.
        /// </summary>
        private double cx;

        /// <summary>
        /// Position 2 y-coordinate.
        /// </summary>
        private double cy;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points = new List<Point2D>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        public QuadraticBezier()
            : this(0, 0, 0, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public QuadraticBezier(Point2D a, Point2D b, Point2D c)
            : this(a.X, a.Y, b.X, b.Y, c.X, c.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezier"/> class.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public QuadraticBezier(double ax, double ay, double bx, double by, double cx, double cy)
        {
            this.ax = ax;
            this.ay = ay;
            this.bx = bx;
            this.by = by;
            this.cx = cx;
            this.cy = cy;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the starting node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlIgnore]
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
        /// Gets or sets the middle tangent control node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlIgnore]
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
        /// Gets or sets the closing node for the <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlIgnore]
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
        /// An approximation of the length of a <see cref="QuadraticBezier"/> curve.
        /// </summary>
        [XmlIgnore]
        public double Length
            => Distances.QuadraticBezierArcLengthByIntegral(ax, ay, bx, by, cx, cy);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override double Perimeter
            => Length;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.QuadraticBezier(ax, ay, bx, by, cx, cy);

        #endregion

        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => new Point2D(Interpolaters.QuadraticBezier(ax, ay, ax, by, cx, cy, t));

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CubicBezier ToCubicBezier()
            => new CubicBezier(ax, ay, bx,by, cx,cy);

        /// <summary>
        /// Creates a string representation of this <see cref="QuadraticBezier"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(QuadraticBezier);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(QuadraticBezier)}={{{nameof(A)}={A}{sep}{nameof(B)}={B}{sep}{nameof(C)}={C}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
