// <copyright file="AngleVisualizerTester.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class AngleVisualizerTester
        : Shape
    {
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
        /// <param name="testAngle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public AngleVisualizerTester(Point2D location, double radius, List<double> testAngle, double startAngle, double sweepAngle)
            : this(location.X, location.Y, radius, testAngle, startAngle, sweepAngle)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="testAngle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        public AngleVisualizerTester(double x, double y, double radius, List<double> testAngle, double startAngle, double sweepAngle)
        {
            X = x;
            Y = y;
            Radius = radius;
            TestAngles = testAngle;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The center x coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The center y coordinate location of the arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties")]
        [Description("The radius of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [GeometryAngle]
        [Category("Properties")]
        [Description("The test angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public List<double> TestAngles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [GeometryAngle]
        [Category("Properties")]
        [Description("The start angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double StartAngle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [GeometryAngle]
        [Category("Properties")]
        [Description("The sweep angle of the Arc.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public double SweepAngle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [GeometryAngle]
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
        [Category("Properties")]
        [Description("The point on the arc at the test angle.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public List<Point2D> TestPoints
        {
            get
            {
                var points = new List<Point2D>();
                foreach (double angle in TestAngles)
                    points.Add(Interpolaters.Circle(X, Y, Radius, angle));
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

        /// <summary>
        /// 
        /// </summary>
        public Point2D TestPoint(double angle)
            => Interpolaters.Circle(X, Y, Radius, angle);

        /// <summary>
        /// 
        /// </summary>
        public bool InSweep(double angle)
            => Intersections.Contains(angle, StartAngle, SweepAngle);
    }
}
