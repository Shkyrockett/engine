﻿// <copyright file="CircularSegment.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The circular segment class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(CircularSegment))]
    public class CircularSegment
        : Shape
    {
        #region Fields
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

        /// <summary>
        /// The start angle.
        /// </summary>
        private double startAngle;

        /// <summary>
        /// The end angle.
        /// </summary>
        private double endAngle;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new default instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        public CircularSegment()
            : this(Point2D.Empty, 0, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        /// <param name="triangle"></param>
        public CircularSegment(Triangle triangle)
            : this(triangle.A, triangle.B, triangle.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        public CircularSegment(Circle circle, double startAngle, double endAngle)
            : this(circle.Center, circle.Radius, startAngle, endAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        /// <param name="x">The center x coordinate point of the circle.</param>
        /// <param name="y">The center y coordinate point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public CircularSegment(double x, double y, double radius, double startAngle, double endAngle)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public CircularSegment(Point2D center, double radius, double startAngle, double endAngle)
        {
            x = center.X;
            y = center.Y;
            this.radius = radius;
            this.startAngle = startAngle;
            this.endAngle = endAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularSegment"/> class.
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        public CircularSegment(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            // ToDo: calculate the angles of the start and end points from the center to fill them in.
            // Calculate the slopes of the lines.
            var slopeA = PointA.Slope(PointB);
            var slopeB = PointC.Slope(PointB);
            var f = new Vector2D((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)),
                (((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));

            // Find the center.
            x = f.I - (slopeB * ((f.I - f.J) / (slopeB - slopeA)));
            y = (f.I - f.J) / (slopeB - slopeA);

            // Get the radius.
            radius = Center.Distance(PointA);
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="CircularSegment"/> to a Tuple.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle"></param>
        public void Deconstruct(out double x, out double y, out double radius, out double startAngle, out double endAngle)
        {
            x = this.x;
            y = this.y;
            radius = this.radius;
            startAngle = this.startAngle;
            endAngle = this.endAngle;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location of the center point of the circular segment.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The location of the center point of the circular segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return new Point2D(x, y); }
            set
            {
                x = value.X;
                y = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(Location));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the center of the Chord.
        /// </summary>
        [Category("Elements")]
        [Description("The center location of the Chord.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets the point on the circular arc circumference coincident to the starting angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the starting angle.")]
        public Point2D StartPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.CircularArc(x, y, radius, startAngle, SweepAngle, 0));

        /// <summary>
        /// Gets the point on the circular arc circumference coincident to the ending angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [Description("The point on the circular arc circumference coincident to the ending angle.")]
        public Point2D EndPoint
            => (Point2D)CachingProperty(() => (Point2D)Interpolators.CircularArc(x, y, radius, startAngle, SweepAngle, 1));

        /// <summary>
        /// Gets or sets the X coordinate location of the center of the circle.
        /// </summary>
        [Category("Elements")]
        [Description("The center x coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember, XmlAttribute, SoapAttribute]
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
        [Category("Elements")]
        [Description("The center y coordinate location of the circle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        [DataMember, XmlAttribute, SoapAttribute]
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
        /// Gets or sets the radius of the Chord.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Category("Elements")]
        [Description("The radius of the Chord.")]
        [DataMember, XmlAttribute, SoapAttribute]
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
        /// Gets or sets the start angle of the Chord.
        /// </summary>
        [Category("Elements")]
        [Description("The start angle of the Chord.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle
        {
            get { return startAngle; }
            set
            {
                startAngle = value;
                ClearCache();
                OnPropertyChanged(nameof(StartAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the start angle of the Arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Start")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The start angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngleDegrees
        {
            get { return startAngle.ToDegrees(); }
            set
            {
                startAngle = value.ToRadians();
                ClearCache();
                OnPropertyChanged(nameof(StartAngleDegrees));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the ellipse.
        /// </summary>
        [Category("Elements")]
        [Description("The sweep angle of the Chord.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle
        {
            get { return startAngle - endAngle; }
            set
            {
                endAngle = value + startAngle;
                ClearCache();
                OnPropertyChanged(nameof(SweepAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the sweep angle of the Arc in Degrees.
        /// </summary>
        [XmlAttribute("angle-Sweep")]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The sweep angle of the Arc in Degrees.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngleDegrees
        {
            get { return SweepAngle.ToDegrees(); }
            set
            {
                SweepAngle = value.ToRadians();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Chord.
        /// </summary>
        [Category("Elements")]
        [Description("The end angle of the Chord.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return endAngle; }
            set
            {
                endAngle = value;
                ClearCache();
                OnPropertyChanged(nameof(EndAngle));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the end angle of the Arc.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [GeometryAngleDegrees]
        [Category("Clipping")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngleDegrees
        {
            get { return endAngle.ToDegrees(); }
            set
            {
                endAngle = value.ToRadians();
                ClearCache();
                OnPropertyChanged(nameof(EndAngleDegrees));
                update?.Invoke();
            }
        }

        /// <returns></returns>
        /// <summary>
        /// Gets the chord length.
        /// </summary>
        [Category("Properties")]
        [Description("The distance around the Chord.")]
        public double ChordLength
            => (double)CachingProperty(() => Abs(SweepAngle) * radius);

        /// <returns></returns>
        /// <summary>
        /// Gets the perimiter.
        /// </summary>
        [Category("Properties")]
        [Description("The distance around the arc.")]
        public double Perimiter
            => (double)CachingProperty(() => (2 * PI * radius * -SweepAngle) + (Abs(SweepAngle) * radius));

        /// <remarks>https://en.wikipedia.org/wiki/Circular_segment</remarks>
        /// <summary>
        /// Gets the area.
        /// </summary>
        [Category("Properties")]
        [Description("The area of the Chord.")]
        public override double Area
            => (double)CachingProperty(() => radius * radius * 0.5d * (SweepAngle - Sin(SweepAngle)));

        //return radius * (1 - Cos(SweepAngle * 0.5d));
        /// <summary>
        /// Gets the sagitta.
        /// </summary>
        /// <summary>
        /// Gets the sagitta.
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Circular_segment</remarks>
        [Category("Properties")]
        [Description("The sagitta of the Chord.")]
        public double Sagitta
            => (double)CachingProperty(() => radius - Sqrt(radius * radius - (SweepAngle * SweepAngle / 4)));

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [Category("Properties")]
        [Description("The tight rectangular boundaries of the Chord.")]
        public override Rectangle2D Bounds
        {
            get
            {
                return (Rectangle2D)CachingProperty(() => boundings());

                Rectangle2D boundings()
                {
                    var bounds = new Rectangle2D(StartPoint, EndPoint);
                    var angleEnd = endAngle;
                    // check that angle2 > angle1
                    if (angleEnd < startAngle) angleEnd += 2 * PI;
                    if ((angleEnd >= 0) && (startAngle <= 0)) bounds.Right = x + radius;
                    if ((angleEnd >= Maths.HalfPi) && (startAngle <= Maths.HalfPi)) bounds.Top = y - radius;
                    if ((angleEnd >= PI) && (startAngle <= PI)) bounds.Left = x - radius;
                    if ((angleEnd >= Maths.Pau) && (startAngle <= Maths.Pau)) bounds.Bottom = y + radius;
                    if ((angleEnd >= Maths.Tau) && (startAngle <= Maths.Tau)) bounds.Right = x + radius;
                    return bounds;
                }
            }
        }

        /// <summary>
        /// Gets the drawing bounds.
        /// </summary>
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle containing the Chord.")]
        public Rectangle2D DrawingBounds
            => (Rectangle2D)CachingProperty(() => Rectangle2D.FromLTRB(x - radius, y - radius, x + radius, y + radius));
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularSegment)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularSegment)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularSegment)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(CircularSegment)} has been deserialized.");
        //}

        //#endregion

        #region Interpolators
        /// <summary>
        /// Interpolates the circle.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
        {
            // ToDo: Add the 
            var theta = startAngle + SweepAngle * t;
            return new Point2D(
                x + (Sin(theta) * radius),
                y + (Cos(theta) * radius));
        }

        /// <returns></returns>
        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolatePoints()
        {
            //double delta_phi = 2 * PI / ArcLength;
            var points = new List<Point2D>();
            //for (double i = 0.0f; i <= 2.0 * PI; i += delta_phi)
            //{
            //    points.Add(Interpolate(i));
            //}

            return points;
        }
        #endregion Interpolators

        #region Methods
        /// <summary>
        /// Creates a string representation of this <see cref="CircularSegment"/> struct based on the format string
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
            if (this is null) return nameof(CircularSegment);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(CircularSegment)}{{{nameof(Center)}={Center},{nameof(Radius)}={radius},{nameof(StartAngle)}={startAngle},{nameof(EndAngle)}={endAngle}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
