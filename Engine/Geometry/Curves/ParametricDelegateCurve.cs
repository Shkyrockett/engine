// <copyright file="ParametricDelegateCurve.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Parametric Delegate Curve.
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(ParametricDelegateCurve))]
    public class ParametricDelegateCurve
        : Shape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double h;

        /// <summary>
        /// 
        /// </summary>
        private double v;

        /// <summary>
        /// 
        /// </summary>
        private double r;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ParametricDelegateCurve()
        { }

        /// <summary>
        /// 
        /// </summary>
        public ParametricDelegateCurve(
            Func<double, double, double, double, double, double, Point2D> interpolater,
            Func<double, double, double, double, double, double, double, Inclusion> pointIntersector,
            Point2D location,
            Size2D scale,
            double rotation = 0d,
            double precision = 0.1d)
        {
            Interpolater = interpolater;
            PointIntersector = pointIntersector;
            Location = location;
            Scale = scale;
            Rotation = rotation;
            Precision = precision;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Func<double, double, double, double, double, double, Point2D> Interpolater { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Func<double, double, double, double, double, double, double, Inclusion> PointIntersector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public Double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public Double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Category("Adjustments")]
        [Description("The " + nameof(Location) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public Point2D Location
        {
            get
            {
                return new Point2D(x, y);
            }

            set
            {
                x = value.X;
                y = value.Y;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public Double Width
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
                OnPropertyChanged(nameof(h));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public Double Height
        {
            get
            {
                return v;
            }

            set
            {
                v = value;
                OnPropertyChanged(nameof(v));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Category("Adjustments")]
        [Description("The " + nameof(Scale) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Size2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public Size2D Scale
        {
            get
            {
                return new Size2D(h, v);
            }

            set
            {
                h = value.Width; v = value.Height;
                OnPropertyChanged(nameof(Scale));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [GeometryAngleRadians]
        [Category("Adjustments")]
        [Description("The " + nameof(Rotation) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public double Rotation
        {
            get
            {
                return r;
            }

            set
            {
                r = value;
                OnPropertyChanged(nameof(Rotation));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [Category("Adjustments")]
        [Description("The " + nameof(Precision) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public double Precision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                List<Point2D> points = (InterpolatePoints(100));
                if (points?.Count < 1)
                    return null;

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

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolate(Interpolater, x, y, h, v, r, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="a"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D Interpolate(Func<double, double, double, double, double, double, Point2D> function, double x, double y, double w, double h, double a, double t)
            => function.Invoke(x, y, w, h, a, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Contains(PointIntersector, x, y, h, v, r, point.X, point.Y) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="a"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        public static Inclusion Contains(Func<double, double, double, double, double, double, double, Inclusion> function, double x, double y, double w, double h, double a, double pX, double pY)
            => function.Invoke(x, y, w, h, a, pX, pY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(ParametricDelegateCurve);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(ParametricDelegateCurve)}{{{nameof(Location)}={Location},{nameof(Scale)}={Scale},{nameof(Precision)}={Precision}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
