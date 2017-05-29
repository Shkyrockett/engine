// <copyright file="AngleVisualizerTester.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class AngleVisualizerTester
        : Shape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public AngleVisualizerTester()
            : this(Point2D.Empty, 0, new List<double> { 0 }, 0, 0)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        /// <param name="testAngles"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public AngleVisualizerTester(Point2D location, double radius, List<double> testAngles, double startAngle, double sweepAngle)
            : this(location.X, location.Y, radius, testAngles, startAngle, sweepAngle)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="testAngles"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public AngleVisualizerTester(double x, double y, double radius, List<double> testAngles, double startAngle, double sweepAngle)
            : base()
        {
            X = x;
            Y = y;
            Radius = radius;
            TestAngles = testAngles;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="testAngles"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public void Deconstruct(out double x, out double y, out double radius, out List<double> testAngles, out double startAngle, out double sweepAngle)
        {
            x = this.X;
            y = this.Y;
            radius = this.Radius;
            testAngles = this.TestAngles;
            startAngle = this.StartAngle;
            sweepAngle = this.SweepAngle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [Category("Properties")]
        [Description("The radius of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The test angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public List<double> TestAngles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        [GeometryAngleRadians]
        [Category("Properties")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle { get; set; }

        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The point on the arc at the test angle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public List<Point2D> TestPoints
        {
            get
            {
                var points = new List<Point2D>();
                foreach (var angle in TestAngles)
                    points.Add(Interpolators.Circle(X, Y, Radius, angle));
                return points;
            }
            set
            {
                var angles = new List<double>();
                foreach (Point2D point in value)
                    angles.Add(Maths.Angle(X, Y, point.X, point.Y));
                TestAngles = angles;
                update?.Invoke();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public Point2D TestPoint(double angle)
            => Interpolators.Circle(X, Y, Radius, angle);

        /// <summary>
        /// 
        /// </summary>
        public bool InSweep(double angle)
            => Intersections.Within(angle, StartAngle, SweepAngle);

        #endregion
    }
}
