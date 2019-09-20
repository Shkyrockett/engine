// <copyright file="MeasurementsTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Engine;
using System.Globalization;

namespace EngineTests
{
    /// <summary>
    /// A set of unit test cases designed to test the measurements of various shapes.
    /// </summary>
    [TestClass()]
    public class MeasurementsTests
    {
        #region Constants
        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double testEpsilon = 0.0000000000001d;

        /// <summary>
        /// A Listing of primitive polygons that can exhibit odd behavior.
        /// </summary>
        private static readonly List<(string description, List<Point2D> polygon)> polygons = new List<(string, List<Point2D>)>
        {
            ("Square", Commons.Polygons.SquareClockwise),
            ("Top Left Triangle", Commons.Polygons.RightTriangleTopLeftClockwise),
            ("Bottom Right Triangle", Commons.Polygons.RightTriangleBottomRightCounterClockwise),
            ("Right Reversed Bow-tie", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100) }),
            ("Left Reversed Bow-tie", new List<Point2D> {
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 25),
                new Point2D(25, 100) }),
            ("C Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 50),
                new Point2D(50, 75),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100) }),
            ("n Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(75, 50),
                new Point2D(50, 50),
                new Point2D(50, 100),
                new Point2D(25, 100) }),
            ("C Bow-tie hole Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 75),
                new Point2D(50, 50),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100) }),
            ("n Bow-tie hole Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(50, 50),
                new Point2D(75, 50),
                new Point2D(50, 100),
                new Point2D(25, 100) }),
        };
        #endregion Constants

        #region Properties
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        #endregion Properties

        #region Housekeeping
        /// <summary>
        /// ClassInitialize runs code before running the first test in the class.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context) => _ = context;

        /// <summary>
        /// TestInitialize runs code before running each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// TestCleanup runs code after each test has run.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// ClassCleanup runs code after all tests in a class have run.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }
        #endregion Housekeeping

        #region Distance
        /// <summary>
        /// The distance2d test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void Distance2DTest()
        {
            var cases = new Dictionary<(Point2D a, Point2D b), double>
            {
                { ((0d, 0d), (0d, 0d)), 0d },
                { ((0d, 0d), (1d, 0d)), 1d },
                { ((0d, 0d), (1d, 1d)), 1.4142135623731d },
                { ((1d, 1d), (0d, 0d)), 1.4142135623731d },
                { ((1d, 1d), (3d, 3d)), 2.82842712474619d },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.Distance(k.a.X, k.a.Y, k.b.X, k.b.Y);
                Assert.AreEqual(cases[k], distance, testEpsilon, $"Point A: {k.a.ToString(CultureInfo.InvariantCulture)}, Point B: {k.b.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The distance line segment point test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void DistanceLineSegmentPointTest()
        {
            var cases = new Dictionary<(LineSegment s, Point2D p), double>
            {
                { ((0d, 0d, 0d, 0d), (0d, 0d)), 0d },
                { ((0d, 0d, 2d, 2d), (1d, 1d)), 0d },
                { ((0d, 0d, 2d, 0d), (1d, 1d)), 1d },
                { ((0d, 0d, 2d, 2d), (1d, 0d)), 0.707106781186547d },
                { ((1d, 1d, 2d, 2d), (0d, 0d)), 1.4142135623731d },
                { ((1d, 1d, 2d, 2d), (3d, 3d)), 1.4142135623731d },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.DistanceLineSegmentPoint(k.s.AX, k.s.AY, k.s.BX, k.s.BY, k.p.X, k.p.Y);
                Assert.AreEqual(cases[k], distance, testEpsilon, $"Segment: {k.s.ToString(CultureInfo.InvariantCulture)}, Point: {k.p.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The constrained distance line segment point test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void ConstrainedDistanceLineSegmentPointTest()
        {
            var cases = new Dictionary<(LineSegment s, Point2D p), double?>
            {
                { ((0d, 0d, 0d, 0d), (0d, 0d)), null },
                { ((0d, 0d, 2d, 2d), (1d, 1d)), 0d },
                { ((0d, 0d, 2d, 0d), (1d, 1d)), 1d },
                { ((0d, 0d, 2d, 2d), (1d, 0d)), 0.707106781186547d },
                { ((1d, 1d, 2d, 2d), (0d, 0d)), null },
                { ((1d, 1d, 2d, 2d), (3d, 3d)), null },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.ConstrainedDistanceLineSegmentPoint(k.s.AX, k.s.AY, k.s.BX, k.s.BY, k.p.X, k.p.Y);
                Assert.AreEqual(cases[k] ?? double.PositiveInfinity, distance ?? double.PositiveInfinity, testEpsilon, $"Segment: {k.s.ToString(CultureInfo.InvariantCulture)}, Point: {k.p.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The square distance line segment point test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void SquareDistanceLineSegmentPointTest()
        {
            var cases = new Dictionary<(LineSegment s, Point2D p), double>
            {
                { ((0d, 0d, 0d, 0d), (0d, 0d)), 0d },
                { ((0d, 0d, 2d, 2d), (1d, 1d)), 0d },
                { ((0d, 0d, 2d, 0d), (1d, 1d)), 1d },
                { ((0d, 0d, 2d, 2d), (1d, 0d)), 0.5d },
                { ((1d, 1d, 2d, 2d), (0d, 0d)), 2d },
                { ((1d, 1d, 2d, 2d), (3d, 3d)), 2d },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.SquareDistanceLineSegmentPoint(k.s.AX, k.s.AY, k.s.BX, k.s.BY, k.p.X, k.p.Y);
                Assert.AreEqual(cases[k], distance, testEpsilon, $"Segment: {k.s.ToString(CultureInfo.InvariantCulture)}, Point: {k.p.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }
        #endregion Distance

        #region Length
        /// <summary>
        /// The vector magnitude test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void VectorMagnitudeTest()
        {
            var cases = new Dictionary<Vector2D, double>
            {
                { (0d, 0d), 0d },
                { (1d, 0d), 1d },
                { (1d, 1d), 1.4142135623731d },
                { (0d, 1d), 1d },
                { (3d, 3d), 4.24264068711928d },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.VectorMagnitude(k.I, k.J);
                Assert.AreEqual(cases[k], distance, testEpsilon, $"Vector: {k.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The vector magnitude squared test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void VectorMagnitudeSquaredTest()
        {
            var cases = new Dictionary<Vector2D, double>
            {
                { (0d, 0d), 0d },
                { (1d, 0d), 1d },
                { (1d, 1d), 2d },
                { (0d, 1d), 1d },
                { (3d, 3d), 18d },
            };

            foreach (var k in cases.Keys)
            {
                var distance = Measurements.VectorMagnitudeSquared(k.I, k.J);
                Assert.AreEqual(cases[k], distance, testEpsilon, $"Vector: {k.ToString(CultureInfo.InvariantCulture)}, Expected: {cases[k]}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The arc length test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void ArcLengthTest()
        {
            var cases = new Dictionary<(double r, double sweepAngle), double>
            {
                { (0d, 90d.DegreesToRadians()), 0d },
                { (1d, 90d.DegreesToRadians()), -9.869604401089358d },
                { (2d, 90d.DegreesToRadians()), -19.739208802178716d },
                { (3d, 90d.DegreesToRadians()), -29.608813203268074d },
                { (4d, 90d.DegreesToRadians()), -39.478417604357432d },
            };

            foreach (var test in cases)
            {
                var distance = Measurements.ArcLength(test.Key.r, test.Key.sweepAngle);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString()}, Expected: {test.Value}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The circle circumference test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void CircleCircumferenceTest()
        {
            var cases = new Dictionary<double, double>
            {
                { 0d, 0d },
                { 1d, 6.2831853071795862d },
                { 2d, 12.566370614359173d },
                { 3d, 18.849555921538759d },
                { 4d, 25.132741228718345d },
            };

            foreach (var test in cases)
            {
                var distance = Measurements.CircleCircumference(test.Key);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString(CultureInfo.InvariantCulture)}, Expected: {test.Value}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The ellipse perimeter test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void EllipsePerimeterTest()
        {
            var cases = new Dictionary<(double a, double b), double>
            {
                { (0d, 1d), 4d },
                { (1d, 1d), 6.2831853071795862d },
                { (2d, 1d), 9.710913742906115d },
                { (3d, 1d), 13.424777960769379d },
                { (4d, 1d), 17.253096491487337d },
            };

            foreach (var test in cases)
            {
                var distance = Measurements.EllipsePerimeter(test.Key.a, test.Key.b);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString()}, Expected: {test.Value}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The quadratic bezier arc length by integral test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void QuadraticBezierArcLengthByIntegralTest()
        {
            var cases = new Dictionary<(double ax, double ay, double bx, double by, double cx, double cy), double>
            {
                //{ (0d, 0d, 0d, 0d, 0d, 0d), 0d },
                { (0d, 0d, 10d, 10d, 20d, 0d), 22.955871493926381d },
                { (0d, 10d, 10d, 0d, 20d, 10d), 22.955871493926381d },
                { (5d, 0d, 10d, 10d, 20d, 0d), 18.773182061585533d },
                { (83d, 214d, 335d, 173d, 91d, 137d), 266.33762321669229d }, // KLD Quadratic test
                { (92d, 233d, 152d, 30d, 198d, 227d), 235.60570888481504d }, // KLD Quadratic test
                { (123d, 47d, 146d, 255d, 188d, 47d), 223.69024423969862d }, // KLD Quadratic test
            };

            foreach (var test in cases)
            {
                var distance = Measurements.QuadraticBezierArcLengthByIntegral(test.Key.ax, test.Key.ay, test.Key.bx, test.Key.by, test.Key.cx, test.Key.cy);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString()}, Expected: {test.Value}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The cubic bezier arc length test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void CubicBezierArcLengthTest()
        {
            var cases = new Dictionary<(double ax, double ay, double bx, double by, double cx, double cy, double dx, double dy), double>
            {
                { (0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d), 0d },
                { (0d, 10d, 6.66666666666667d, 3.33333333333333d, 13.3333333333333d, 3.33333333333333d, 20d, 10d), 28.772285755369104d },
                { (0d, 0d, 6.66666666666667d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d), 28.853157091613134d },
                { (5d, 0d, 8.33333333333333d, 6.66666666666667d, 13.3333333333333d, 6.66666666666667d, 20d, 0d), 22.972804939583188d },
                { (83d, 214d, 251d, 186.666666666667d, 253.666666666667d, 161d, 91d, 137d), 202.43303990653033d }, // KLD Quadratic test
                { (92d, 233d, 132d, 97.6666666666667d, 167.333333333333d, 95.6666666666667d, 198d, 227d), 52.117557445746179d }, // KLD Quadratic test
                { (123d, 47d, 138.333333333333d, 185.666666666667d, 160d, 185.666666666667d, 188d, 47d), 109.34854245841467d }, // KLD Quadratic test
                { (203d, 140d, 206d, 359d, 245d, 6d, 248d, 212d), 389.0464480832751d }, // KLD Cubic test
                { (177d, 204d, 441d, 204d, 8d, 149d, 265d, 154d), 560.24650162127568d }, // KLD Cubic test
                { (171d, 143d, 22d, 132d, 330d, 64d, 107d, 65d), 331.17868818300917d }, // KLD Cubic test
            };

            foreach (var test in cases)
            {
                var distance = Measurements.CubicBezierArcLength(test.Key.ax, test.Key.ay, test.Key.bx, test.Key.by, test.Key.cx, test.Key.cy, test.Key.dx, test.Key.dy);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString()}, Expected: {test.Value}, Actual: {distance}");
            }
        }

        /// <summary>
        /// The polygon perimeter test.
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Engine.dll")]
        public void PolygonPerimeterTest()
        {
            var cases = new Dictionary<List<Point2D>, double>
            {
                {Commons.Polygons.SquareClockwise, 300d },
                {Commons.Polygons.SquareCounterClockwise, 300d },
                {Commons.Polygons.RightTriangleTopLeftClockwise, 256.06601717798213d },
                {Commons.Polygons.RightTriangleTopLeftCounterClockwise, 256.06601717798213d },
                {Commons.Polygons.RightTriangleBottomRightClockwise, 256.06601717798213d },
                {Commons.Polygons.RightTriangleBottomRightCounterClockwise, 256.06601717798213d },
                {Commons.Polygons.BowTieRightReversed, 362.13203435596427d },
                {Commons.Polygons.BowTieLeftReversed, 362.13203435596427d },
                {Commons.Polygons.CShape, 400d },
                {Commons.Polygons.NShape, 400d },
                {Commons.Polygons.CBowTieHoleShape, 411.80339887498951d },
                {Commons.Polygons.NBowTieHoleShape, 411.80339887498951d },
            };

            foreach (var test in cases)
            {
                var distance = Measurements.PolygonContourPerimeter(test.Key);
                Assert.AreEqual(test.Value, distance, testEpsilon, $"Value: {test.Key.ToString()}, Expected: {test.Value}, Actual: {distance}");
            }
        }
        #endregion Length

        #region Area
        /// <summary>
        /// The arc area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void ArcAreaTest()
        {
            var results = new Dictionary<(double radius, double sweepAngle), double>
            {
                { (0, -15), 0 }, { (1, -15), 0.00149017134831433d },
                { (0, -14), 0 }, { (1, -14), 0.00121209983976921d },
                { (0, -13), 0 }, { (1, -13), 0.000970874207698924d },
                { (0, -12), 0 }, { (1, -12), 0.000763909710780109d },
                { (0, -11), 0 }, { (1, -11), 0.000588611171415718d },
                { (0, -10), 0 }, { (1, -10), 0.000442373766251308d },
                { (0, -9), 0 }, { (1, -9), 0.000322583819629393d },
                { (0, -8), 0 }, { (1, -8), 0.000226619599740463d },
                { (0, -7), 0 }, { (1, -7), 0.000151852117227798d },
                { (0, -6), 0 }, { (1, -6), 9.56459260031545E-05d },
                { (0, -5), 0 }, { (1, -5), 5.5359926029154E-05d },
                { (0, -4), 0 }, { (1, -4), 2.83481678239397E-05d },
                { (0, -3), 0 }, { (1, -3), 1.19606584430276E-05d },
                { (0, -2), 0 }, { (1, -2), 3.54416869281088E-06d },
                { (0, -1), 0 }, { (1, -1), 4.43041329891911E-07d },
                { (0, 0), 0 }, { (1, 0), 0 },
                { (0, 1), 0 }, { (1, 1), 4.43041329891911E-07d },
                { (0, 2), 0 }, { (1, 2), 3.54416869281088E-06d },
                { (0, 3), 0 }, { (1, 3), 1.19606584430276E-05d },
                { (0, 4), 0 }, { (1, 4), 2.83481678239397E-05d },
                { (0, 5), 0 }, { (1, 5), 5.5359926029154E-05d },
                { (0, 6), 0 }, { (1, 6), 9.56459260031545E-05d },
                { (0, 7), 0 }, { (1, 7), 0.000151852117227798d },
                { (0, 8), 0 }, { (1, 8), 0.000226619599740463d },
                { (0, 9), 0 }, { (1, 9), 0.000322583819629393d },
                { (0, 10), 0 }, { (1, 10), 0.000442373766251308d },
                { (0, 11), 0 }, { (1, 11), 0.000588611171415718d },
                { (0, 12), 0 }, { (1, 12), 0.000763909710780109d },
                { (0, 13), 0 }, { (1, 13), 0.000970874207698924d },
                { (0, 14), 0 }, { (1, 14), 0.00121209983976921d },
                { (0, 15), 0 }, { (1, 15), 0.00149017134831433d },
            };

            foreach (var arc in results.Keys)
            {
                var area = Measurements.CircularArcSectorArea(arc.radius, arc.sweepAngle.DegreesToRadians());
                Assert.AreEqual(results[arc], area, testEpsilon);
            }
        }

        /// <summary>
        /// The circle area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void CircleAreaTest()
        {
            // A listing of expected results for specific integer values.
            var results = new Dictionary<double, double>
            {
                { 0d, 0d },
                { 1d, 3.14159265358979d },
                { 2d, 12.5663706143592d },
                { 3d, 28.2743338823081d },
            };

            // Run tests for each radius.

            foreach (var r in results.Keys)
            {
                // Retrieve the area of the circle, provided it's radius. 
                var area = Measurements.CircleArea(r);

                // Check for a correct result.
                Assert.AreEqual(results[r], area, testEpsilon);
            }
        }

        /// <summary>
        /// The ellipse area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void EllipseAreaTest()
        {
            // A listing of expected results for specific integer values.
            var results = new Dictionary<(double r1, double r2), double>
            {
                { (0d, 0d), 0d }, { (0d, 1d), 0d },
                { (1d, 0d), 0d }, { (1d, 1d), 3.14159265358979d },
                { (2d, 0d), 0d }, { (2d, 1d), 6.28318530717959d },
                { (3d, 0d), 0d }, { (3d, 1d), 9.42477796076938d },
            };

            // Run tests for each radius.
            foreach (var radii in results.Keys)
            {
                // Retrieve the area of the circle, provided it's radius. 
                var area = Measurements.EllipseArea(radii.r1, radii.r2);

                // Check for a correct result.
                Assert.AreEqual(results[radii], area, testEpsilon);
            }
        }

        /// <summary>
        /// The rectangle area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void RectangleAreaTest()
        {
            // A listing of expected results for specific integer values.
            var results = new Dictionary<(double w, double h), double>
            {
                { (0d, 0d), 0d }, { (0d, 1d), 0d },
                { (1d, 0d), 0d }, { (1d, 1d), 1d },
                { (2d, 0d), 0d }, { (2d, 1d), 2d },
                { (3d, 0d), 0d }, { (3d, 1d), 3d },
            };

            // Run tests for each height and width.
            foreach (var size in results.Keys)
            {
                // Retrieve the area of the circle, provided it's radius. 
                var area = Measurements.RectangleArea(size.w, size.h);

                // Check for a correct result.
                Assert.AreEqual(results[size], area, testEpsilon);
            }
        }

        /// <summary>
        /// The square area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void SquareAreaTest()
        {
            // A listing of expected results for specific values.
            var results = new Dictionary<double, double>
            {
                { 0d, 0d },
                { 1d, 1d },
                { 2d, 4d },
                { 3d, 9d },
                { 4d, 16d },
            };

            // Run tests for each radius.
            foreach (var w in results.Keys)
            {
                // Retrieve the area of the circle, provided it's radius. 
                var area = Measurements.SquareArea(w);

                // Check for a correct result.
                Assert.AreEqual(results[w], area, testEpsilon);
            }
        }

        /// <summary>
        /// The polygon area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void PolygonAreaTest()
        {
            // A listing of expected results for specific polygons.
            var results = new List<double>
            {
                5625,
                2812.5,
                2812.5,
                // ToDo: Find fix!
                0, // What!?
                0, // What!?
                4375,
                4375,
                5625,
                5625,
            };

            // Run tests for each polygon.
            for (var i = 0; i < results.Count; i++)
            {
                // Retrieve the area of the polygon, provided a chain of points. 
                var area = Measurements.PolygonArea(polygons[i].polygon);

                // Check for a correct result.
                Assert.AreEqual(results[i], area, testEpsilon, polygons[i].description);
            }
        }

        /// <summary>
        /// The signed polygon area test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void SignedPolygonAreaTest()
        {
            // A listing of expected results for specific polygons.
            var results = new List<double>
            {
                5625,
                2812.5,
                -2812.5,
                // ToDo: Find fix!
                0, // What!?
                0, // What!?
                4375,
                4375,
                5625,
                5625,
            };

            // Run tests for each polygon.
            for (var i = 0; i < results.Count; i++)
            {
                // Retrieve the area of the polygon, provided a chain of points. 
                var area = Measurements.SignedPolygonArea(polygons[i].polygon);

                // Check for a correct result.
                Assert.AreEqual(results[i], area, testEpsilon, polygons[i].description);
            }
        }
        #endregion Area

        #region Bounds
        /// <summary>
        /// The line segment bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void LineSegmentBoundsTest()
        {
            var cases = new Dictionary<LineSegment, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d), (0, 0, 0, 0) },
                { (0d, 0d, 2d, 2d), (0, 0, 2, 2) },
                { (0d, 0d, 2d, 0d), (0, 0, 2, 0) },
                { (1d, 1d, 2d, 2d), (1, 1, 1, 1) },
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.LineSegmentBounds(k.AX, k.AY, k.BX, k.BY);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Segment: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Segment: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Segment: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Segment: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The circle bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void CircleBoundsTest()
        {
            var cases = new Dictionary<Circle, Rectangle2D>
            {
                { (0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 2d), (-2d, -2d, 4d, 4d) },
                { (1d, 1d, 2d), (-1d, -1d, 4d, 4d) },
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.CircleBounds(k.X, k.Y, k.Radius);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Circle: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Circle: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Circle: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Circle: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The circular arc bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void CircularArcBoundsTest()
        {
            var cases = new Dictionary<CircularArc, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 2d, 0d, 0d), (-2d, -2d, 4d, 4d) },
                { (0d, 0d, 2d, 0d, 180d.DegreesToRadians()), (-2d, 0d, 4d, 2d) },
                { (0d, 0d, 2d, 0d, -180d.DegreesToRadians()), (-2d, -2d, 4d, 2d) },
                { (1d, 1d, 2d, 0d, 0d), (-1d, -1d, 4d, 4d) },
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.CircularArcBounds(k.X, k.Y, k.Radius, 0, k.StartAngle, k.SweepAngle);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The ellipse bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void EllipseBoundsTest()
        {
            var cases = new Dictionary<Ellipse, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 2d, 2d, 0d), (-2d, -2d, 4d, 4d) },
                { (1d, 1d, 2d, 2d, 0d), (-1d, -1d, 4d, 4d) },
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.EllipseBounds(k.X, k.Y, k.RX, k.RY);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Ellipse: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Ellipse: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Ellipse: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Ellipse: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The elliptical arc bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void EllipticalArcBoundsTest()
        {
            var cases = new Dictionary<EllipticalArc, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 2d, 2d, 0d, 0d, 270d.DegreesToRadians()), (-2d, -2d, 4d, 4d) },
                { (1d, 1d, 2d, 2d, 0d, 0d, 270d.DegreesToRadians()), (-1d, -1d, 4d, 4d) },
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.EllipticalArcBounds(k.X, k.Y, k.RX, k.RY, k.Angle, k.StartAngle, k.SweepAngle);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Arc: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The rotated rectangle bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void RotatedRectangleBoundsTest()
        {
            var cases = new Dictionary<(double X, double Y, double Width, double Height, double Angle), Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 2d, 2d, 0d), (-1d, -1d, 2d, 2d) },
                { (1d, 1d, 2d, 2d, 0d), (0d, 0d, 2d, 2d) },
                // ToDo: Add more relevant test cases.
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.RotatedRectangleBounds(k.Width, k.Height, k.X, k.Y, k.Angle);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Rectangle: {k.ToString()}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Rectangle: {k.ToString()}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Rectangle: {k.ToString()}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Rectangle: {k.ToString()}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The polygon bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void PolygonBoundsTest()
        {
            var cases = new Dictionary<List<Point2D>, Rectangle2D>
            {
                { new List<Point2D> { new Point2D(0d, 0d), new Point2D(0d, 0d), new Point2D(0d, 0d) }, (0d, 0d, 0d, 0d) },
                { new List<Point2D> { new Point2D(0d, 0d), new Point2D(2d, 2d), new Point2D(3d, 0d) }, (0d, 0d, 3d, 2d) },
                { new List<Point2D> { new Point2D(1d, 1d), new Point2D(2d, 0d), new Point2D(0d, 2d) }, (0d, 0d, 2d, 2d) },
                // ToDo: Add more relevant test cases.
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.PolygonBounds(k);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Polygon: {k.ToString()}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Polygon: {k.ToString()}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Polygon: {k.ToString()}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Polygon: {k.ToString()}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The polyline bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void PolylineBoundsTest()
        {
            var cases = new Dictionary<List<Point2D>, Rectangle2D>
            {
                { new List<Point2D> { new Point2D(0d, 0d), new Point2D(0d, 0d), new Point2D(0d, 0d) }, (0d, 0d, 0d, 0d) },
                { new List<Point2D> { new Point2D(0d, 0d), new Point2D(2d, 2d), new Point2D(3d, 0d) }, (0d, 0d, 3d, 2d) },
                { new List<Point2D> { new Point2D(1d, 1d), new Point2D(2d, 0d), new Point2D(0d, 2d) }, (0d, 0d, 2d, 2d) },
                // ToDo: Add more relevant test cases.
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.PolylineBounds(k);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Polyline: {k.ToString()}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Polyline: {k.ToString()}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Polyline: {k.ToString()}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Polyline: {k.ToString()}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The quadratic bezier bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void QuadraticBezierBoundsTest()
        {
            var cases = new Dictionary<QuadraticBezier, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { (0d, 0d, 10d, 10d, 20d, 0d), (0d, 0d, 20d, 5d) },
                { (0d, 10d, 10d, 0d, 20d, 10d), (0d, 5d, 20d, 5d) },
                { (5d, 0d, 10d, 10d, 20d, 0d), (5d, 0d, 15d, 5d) },
                { (83d, 214d, 335d, 173d, 91d, 137d), (83d, 137d, 128.0322580645161d, 77d) }, // KLD Quadratic test
                { (92d, 233d, 152d, 30d, 198d, 227d), (92d, 129.9775d, 106d, 103.0225d) }, // KLD Quadratic test
                { (123d, 47d, 146d, 255d, 188d, 47d), (123d, 47d, 65d, 104d) }, // KLD Quadratic test
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.BezierBounds(k.CurveX, k.CurveY);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Quadratic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Quadratic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Quadratic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Quadratic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        /// <summary>
        /// The cubic bezier bounds test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(MeasurementsTests))]
        [DeploymentItem("Engine.dll")]
        public void CubicBezierBoundsTest()
        {
            var cases = new Dictionary<CubicBezier, Rectangle2D>
            {
                { (0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d), (0d, 0d, 0d, 0d) },
                { new QuadraticBezier(0d, 10d, 10d, 0d, 20d, 10d).ToCubicBezier(), (0d, 5d, 20d, 5d) },
                { new QuadraticBezier(0d, 0d, 10d, 10d, 20d, 0d).ToCubicBezier(), (0d, 0d, 20d, 5d) },
                { new QuadraticBezier(5d, 0d, 10d, 10d, 20d, 0d).ToCubicBezier(), (5d, 0d, 15d, 5d) },
                { new QuadraticBezier(83d, 214d, 335d, 173d, 91d, 137d).ToCubicBezier(), (83d, 137d, 128.0322580645161d, 77d) }, // KLD Quadratic test
                { new QuadraticBezier(92d, 233d, 152d, 30d, 198d, 227d).ToCubicBezier(), (92d, 129.9775d, 106d, 103.0225d) }, // KLD Quadratic test
                { new QuadraticBezier(123d, 47d, 146d, 255d, 188d, 47d).ToCubicBezier(), (123d, 47d, 65d, 104d) }, // KLD Quadratic test
                { (203d, 140d, 206d, 359d, 245d, 6d, 248d, 212d), (203d, 140d, 45d, 74.7074310714899d) }, // KLD Cubic test
                { (177d, 204d, 441d, 204d, 8d, 149d, 265d, 154d), (177d, 153.67863894139887d, 88.9946642212202d, 50.321361058601127d) }, // KLD Cubic test
                { (171d, 143d, 22d, 132d, 330d, 64d, 107d, 65d), (107d, 64.9890820086819d, 84.1459246542469d, 78.0109179913181d) }, // KLD Cubic test
            };

            foreach (var k in cases.Keys)
            {
                var bounds = Measurements.BezierBounds(k.CurveX, k.CurveY);
                Assert.AreEqual(cases[k].Left, bounds.Left, testEpsilon, $"Cubic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Left)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Top, bounds.Top, testEpsilon, $"Cubic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Top)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Width, bounds.Width, testEpsilon, $"Cubic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Width)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
                Assert.AreEqual(cases[k].Height, bounds.Height, testEpsilon, $"Cubic: {k.ToString(CultureInfo.InvariantCulture)}, Expected {nameof(Rectangle2D.Height)}: {cases[k].ToString(CultureInfo.InvariantCulture)}, Actual: {bounds.ToString(CultureInfo.InvariantCulture)}");
            }
        }
        #endregion Bounds
    }
}