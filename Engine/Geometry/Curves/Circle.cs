// <copyright file="Circle.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Circle))]
    [XmlType(TypeName = "circle", Namespace = "shape")]
    public class Circle
        : Shape
    {
        #region Static creation methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Circle FromCenterAndRadius(Point2D point, double radius)
            => new Circle(point, radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns></returns>
        public static Circle FromThreePoints(Point2D pointA, Point2D pointB, Point2D pointC)
            => new Circle(pointA, pointB, pointC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        public static Circle FromTriangle(Triangle triangle)
            => new Circle(triangle.A, triangle.B, triangle.C);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Circle FromRectangle(Rectangle2D rectangle)
            => new Circle(rectangle);

        #endregion

        #region Private Fields

        /// <summary>
        /// The center x coordinate point of the circle.
        /// </summary>
        private double x;

        /// <summary>
        /// The center y coordinate point of the circle.
        /// </summary>
        private double y;

        /// <summary>
        /// The radius of the circle.
        /// </summary>
        private double radius;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
            : this(0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(double x, double y, double radius)
            : base()
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(Point2D center, double radius)
            : base()
        {
            x = center.X;
            y = center.Y;
            this.radius = radius;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="bounds">The bounding box of the circle.</param>
        public Circle(Rectangle2D bounds)
            : base()
        {
            x = bounds.Center().X;
            y = bounds.Center().Y;
            radius = bounds.Height <= bounds.Width ? bounds.Height * 0.25d : bounds.Width * 0.25d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        public Circle(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public Circle(Point2D PointA, Point2D PointB, Point2D PointC)
            : base()
        {
            //  Calculate the slopes of the lines.
            var slopeA = (PointA.Slope(PointB));
            var slopeB = (PointC.Slope(PointB));
            var f = new Vector2D(((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X))),
                ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X))));

            // Find the center.
            x = f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA)));
            y = (f.I - f.J) / (slopeB - slopeA);

            // Get the radius.
            radius = (Center.Distance(PointA));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        //[DisplayName(nameof(Radius))]
        [Category("Elements")]
        [Description("The radius of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                ClearCache();
                OnPropertyChanged(nameof(Radius));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the center point of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        //[DisplayName(nameof(Center))]
        [Category("Elements")]
        [Description("The center location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Center
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Center));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate location of the center of the circle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The center x coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                ClearCache();
                OnPropertyChanged(nameof(X));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate location of the center of the circle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Browsable(false)]
        [Category("Elements")]
        [Description("The center y coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                ClearCache();
                OnPropertyChanged(nameof(Y));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The distance around the circle.")]
        public double Circumference
            => (double)CachingProperty(() => Measurements.CircleCircumference(radius));

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The distance around the circle.")]
        public override double Perimeter
            => Circumference;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The area of the circle.")]
        public override double Area
        {
            get { return Measurements.CircleArea(radius); }
            set
            {
                radius = Sqrt(value / PI);
                ClearCache();
                OnPropertyChanged(nameof(Area));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the angles of the extreme points of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The angles of the extreme points of the " + nameof(Circle) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<double> ExtremeAngles
            => (List<double>)CachingProperty(() => Measurements.CircleExtremeAngles());

        /// <summary>
        /// Get the points of the Cartesian extremes of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The locations of the extreme points of the " + nameof(Circle) + ".")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> ExtremePoints
            => (List<Point2D>)CachingProperty(() => Measurements.CircleExtremePoints(x, y, radius));

        /// <summary>
        /// Gets or sets the rectangular boundaries of the circle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [ReadOnly(true)]
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get { return Measurements.CircleBounds(x, y, radius); }
            set
            {
                Center = value.Center();
                radius = value.Width <= value.Height ? value.Width : value.Height;
                ClearCache();
                OnPropertyChanged(nameof(Bounds));
                update?.Invoke();
            }
        }

        #endregion

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Circle)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Circle)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Circle)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Circle)} has been deserialized.");
        //}

        //#endregion

        #region Interpolators

        /// <summary>
        /// Interpolates the circle.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
            => Interpolators.UnitCircle(x, y, radius, t);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Creates a string representation of this <see cref="Circle"/> struct based on the format string
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
            if (this == null) return nameof(Circle);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Circle)}{{{nameof(X)}={x.ToString(format, provider)}{sep}{nameof(Y)}={y.ToString(format, provider)}{sep}{nameof(Radius)}={radius.ToString(format, provider)}}}";
        }

        #endregion
    }
}
