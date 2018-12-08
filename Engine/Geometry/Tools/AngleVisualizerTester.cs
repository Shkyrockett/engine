// <copyright file="AngleVisualizerTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// The angle visualizer tester class.
    /// </summary>
    [DataContract, Serializable]
    public class AngleVisualizerTester
        : Shape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AngleVisualizerTester"/> class.
        /// </summary>
        public AngleVisualizerTester()
            : this(Point2D.Empty, 0, new List<double> { 0 }, 0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleVisualizerTester"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="testAngles">The testAngles.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public AngleVisualizerTester(Point2D location, double radius, List<double> testAngles, double startAngle, double sweepAngle)
            : this(location.X, location.Y, radius, testAngles, startAngle, sweepAngle)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleVisualizerTester"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="testAngles">The testAngles.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public AngleVisualizerTester(double x, double y, double radius, List<double> testAngles, double startAngle, double sweepAngle)
        {
            X = x;
            Y = y;
            Radius = radius;
            TestAngles = testAngles;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="testAngles">The testAngles.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        public void Deconstruct(out double x, out double y, out double radius, out List<double> testAngles, out double startAngle, out double sweepAngle)
        {
            x = X;
            y = Y;
            radius = Radius;
            testAngles = TestAngles;
            startAngle = StartAngle;
            sweepAngle = SweepAngle;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The radius of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets the test angles.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The test angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<double> TestAngles { get; set; }

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle { get; set; }

        /// <summary>
        /// Gets or sets the sweep angle.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle { get; set; }

        /// <summary>
        /// Gets or sets the end angle.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The end angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return StartAngle + SweepAngle; }
            set { SweepAngle = value - StartAngle; }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The center location of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D Location
        {
            get { return new Point2D(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The rectangular boundaries of the circle containing the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(X - Radius, Y - Radius, Radius * 2, Radius * 2); }
            set
            {
                Location = value.Center();
                Radius = value.Height <= value.Width ? value.Width : value.Height;
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the test points.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The point on the arc at the test angle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> TestPoints
        {
            get
            {
                var points = new List<Point2D>();
                foreach (var angle in TestAngles)
                {
                    points.Add(Interpolators.Circle(X, Y, Radius, angle));
                }

                return points;
            }
            set
            {
                var angles = new List<double>();
                foreach (Point2D point in value)
                {
                    angles.Add(Maths.Angle(X, Y, point.X, point.Y));
                }

                TestAngles = angles;
                update?.Invoke();
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Test the point.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D TestPoint(double angle)
            => Interpolators.Circle(X, Y, Radius, angle);

        /// <summary>
        /// The in sweep.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool InSweep(double angle)
            => Intersections.Within(angle, StartAngle, SweepAngle);
        #endregion Methods
    }
}
