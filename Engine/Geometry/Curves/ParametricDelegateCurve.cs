// <copyright file="ParametricDelegateCurve.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <date></date>
// <summary></summary>
// <remarks>
// </remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine.Geometry
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
        /// <summary>
        /// 
        /// </summary>
        private Point2D location;

        /// <summary>
        /// 
        /// </summary>
        private double rotation;

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

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Func<double, double, double, double, double, double, Point2D> Interpolater { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Func<double, double, double, double, double, double, double, Inclusion> PointIntersector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Adjustments")]
        [Description("The " + nameof(Location) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [XmlAttribute]
        public Point2D Location
        {
            get { return location; }
            set
            {
                location = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Adjustments")]
        [Description("The " + nameof(Scale) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Size2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [XmlAttribute]
        public Size2D Scale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [GeometryAngle]
        [Category("Adjustments")]
        [Description("The " + nameof(Rotation) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [XmlAttribute]
        public double Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Adjustments")]
        [Description("The " + nameof(Precision) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [XmlAttribute]
        public double Precision { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolate(Interpolater, location.X, location.Y, Scale.Width, Scale.Height, rotation, t);

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
            => function?.Invoke(x, y, w, h, a, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Contains(PointIntersector, location.X, location.Y, Scale.Width, Scale.Height, rotation, point.X, point.Y) != Inclusion.Outside;

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
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null)
                return nameof(ParametricDelegateCurve);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(ParametricDelegateCurve)}{{{nameof(Location)}={Location},{nameof(Scale)}={Scale},{nameof(Precision)}={Precision}}}";
            return formatable.ToString(format, provider);
        }
    }
}
