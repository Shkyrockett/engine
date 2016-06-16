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
using System.Drawing.Design;
using System.Xml.Serialization;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// Parametric Delegate Curve.
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(ParametricDelegateCurve))]
    public class ParametricDelegateCurve
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public ParametricDelegateCurve(
            Func<double, Point2D> function,
            Point2D location,
            Size2D scale,
            double rotation = 0,
            double precision = 0.1d)
        {
            Function = function;
            Location = location;
            Scale = scale;
            Rotation = rotation;
            Precision = precision;
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        public Func<double, Point2D> Function { get; set; }

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
        public Point2D Location { get; set; }

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
        [Category("Adjustments")]
        [Description("The " + nameof(Rotation) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [GeometryAngle]
        //[Editor(typeof(AngleEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(AngleConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [XmlAttribute]
        public double Rotation { get; set; }

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
                if (points?.Count < 1) return null;

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
        /// <param name="precision"></param>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints(double precision)
        {
            var points = new List<Point2D>();
            for (double Index = (PI * -1); (Index < PI); Index = (Index + precision))
                points.Add(Interpolate(Index));

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolate(Function, t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D Interpolate(Func<double, Point2D> function, double t)
            => function?.Invoke(t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(ParametricDelegateCurve);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(ParametricDelegateCurve)}{{{nameof(Location)}={Location},{nameof(Scale)}={Scale},{nameof(Precision)}={Precision}}}";
            return formatable.ToString(format, provider);
        }
    }
}
