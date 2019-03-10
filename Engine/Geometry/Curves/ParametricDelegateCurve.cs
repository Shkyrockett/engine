// <copyright file="ParametricDelegateCurve.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Parametric Delegate Curve.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(ParametricDelegateCurve))]
    public class ParametricDelegateCurve
        : Shape
    {
        #region Fields
        /// <summary>
        /// The x.
        /// </summary>
        private double x;

        /// <summary>
        /// The y.
        /// </summary>
        private double y;

        /// <summary>
        /// The h.
        /// </summary>
        private double h;

        /// <summary>
        /// The v.
        /// </summary>
        private double v;

        /// <summary>
        /// The r.
        /// </summary>
        private double r;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricDelegateCurve"/> class.
        /// </summary>
        public ParametricDelegateCurve()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametricDelegateCurve"/> class.
        /// </summary>
        /// <param name="interpolator">The interpolator.</param>
        /// <param name="pointIntersector">The pointIntersector.</param>
        /// <param name="location">The location.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="precision">The precision.</param>
        public ParametricDelegateCurve(
            Func<double, double, double, double, double, double, Point2D> interpolator,
            Func<double, double, double, double, double, double, double, Inclusion> pointIntersector,
            Point2D location,
            Size2D scale,
            double rotation = 0d,
            double precision = 0.1d)
        {
            Interpolator = interpolator;
            PointIntersector = pointIntersector;
            Location = location;
            Scale = scale;
            Rotation = rotation;
            Precision = precision;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="ParametricDelegateCurve"/> to a Tuple.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="h">The h.</param>
        /// <param name="v">The v.</param>
        /// <param name="r">The r.</param>
        public void Deconstruct(out double x, out double y, out double h, out double v, out double r)
        {
            x = this.x;
            y = this.y;
            h = this.h;
            v = this.v;
            r = this.r;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the interpolator.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Func<double, double, double, double, double, double, Point2D> Interpolator { get; set; }

        /// <summary>
        /// Gets or sets the point intersector.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        public Func<double, double, double, double, double, double, double, Inclusion> PointIntersector { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Adjustments")]
        [Description("The " + nameof(Location) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public Point2D Location
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public double Width
        {
            get { return h; }
            set
            {
                h = value;
                OnPropertyChanged(nameof(h));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public double Height
        {
            get { return v; }
            set
            {
                v = value;
                OnPropertyChanged(nameof(v));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Adjustments")]
        [Description("The " + nameof(Scale) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        //[TypeConverter(typeof(Size2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public Size2D Scale
        {
            get { return new Size2D(h, v); }
            set
            {
                h = value.Width; v = value.Height;
                OnPropertyChanged(nameof(Scale));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
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
            get { return r; }
            set
            {
                r = value;
                OnPropertyChanged(nameof(Rotation));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Adjustments")]
        [Description("The " + nameof(Precision) + " of the " + nameof(ParametricDelegateCurve) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public double Precision { get; set; }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                var points = InterpolatePoints(100);
                if (points?.Count < 1)
                {
                    return null;
                }

                var left = points[0].X;
                var top = points[0].Y;
                var right = points[0].X;
                var bottom = points[0].Y;

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
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing]
        //private void OnSerializing(StreamingContext context)
        //{
        //    //Debug.WriteLine($"{nameof(ParametricDelegateCurve)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized]
        //private void OnSerialized(StreamingContext context)
        //{
        //    //Debug.WriteLine($"{nameof(ParametricDelegateCurve)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    //Debug.WriteLine($"{nameof(ParametricDelegateCurve)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    //Debug.WriteLine($"{nameof(ParametricDelegateCurve)} has been deserialized.");
        //}

        //#endregion

        #region Methods
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => Interpolate(Interpolator, x, y, h, v, r, t);

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="a">The a.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D Interpolate(Func<double, double, double, double, double, double, Point2D> function, double x, double y, double w, double h, double a, double t)
            => function.Invoke(x, y, w, h, a, t);

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Contains(Point2D point)
            => Contains(PointIntersector, x, y, h, v, r, point.X, point.Y) != Inclusion.Outside;

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="a">The a.</param>
        /// <param name="pX">The pX.</param>
        /// <param name="pY">The pY.</param>
        /// <returns>The <see cref="Inclusion"/>.</returns>
        public static Inclusion Contains(Func<double, double, double, double, double, double, double, Inclusion> function, double x, double y, double w, double h, double a, double pX, double pY)
            => (function != null) ? function.Invoke(x, y, w, h, a, pX, pY) : Inclusion.Outside;

        /// <summary>
        /// Convert the to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(ParametricDelegateCurve);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(ParametricDelegateCurve)}{{{nameof(Location)}={Location}{sep}{nameof(Scale)}={Scale}{sep}{nameof(Precision)}={Precision}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
