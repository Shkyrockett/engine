// <copyright file="IntersectionsTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EngineTests
{
    /// <summary>
    /// The intersections tests unit test class.
    /// </summary>
    [TestClass()]
    public class IntersectionsTests
    {
        #region Constants
        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double testEpsilon = 0.000000000001d;
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
        public static void ClassInit(TestContext context) => _ = context;//MessageBox.Show("TestClassInit");

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

        #region Contains Tests
        /// <summary>
        /// The circle contains point test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        public void CircleContainsPointTest()
        {
            var testCases = new Dictionary<(Circle2D circle, Point2D point), Inclusions>
            {
                { (new Circle2D(0, 0, 5), new Point2D(1, 1)), Inclusions.Inside },
                { (new Circle2D(0, 0, 5), new Point2D(0, 0)), Inclusions.Inside },
                { (new Circle2D(0, 0, 5), new Point2D(5, 5)), Inclusions.Outside },
                { (new Circle2D(0, 0, 5), new Point2D(5, -5)), Inclusions.Outside },
                { (new Circle2D(0, 0, 5), new Point2D(-5, -5)), Inclusions.Outside },
                { (new Circle2D(0, 0, 5), new Point2D(-5, 5)), Inclusions.Outside },
                { (new Circle2D(0, 0, 5), new Point2D(0, 5)), Inclusions.Boundary },
                { (new Circle2D(0, 0, 5), new Point2D(0, -5)), Inclusions.Boundary },
            };

            foreach (var test in testCases.Keys)
            {
                var result = Intersections.CircleContainsPoint(test.circle.X, test.circle.Y, test.circle.Radius, test.point.X, test.point.Y);
                var expected = testCases[test];

                Assert.AreEqual(expected, result, $"Test case: {test}, Expected: {result}, Actual: {result}");
            }
        }

        /// <summary>
        /// The ellipse contains point test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        public void EllipseContainsPointTest()
        {
            var testCases = new Dictionary<(Ellipse2D ellipse, Point2D point), Inclusions>
            {
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(1, 1)), Inclusions.Inside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(0, 0)), Inclusions.Inside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(5, 5)), Inclusions.Outside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(5, -5)), Inclusions.Outside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(-5, -5)), Inclusions.Outside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(-5, 5)), Inclusions.Outside },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(0, 5)), Inclusions.Boundary },
                { (new Ellipse2D(0, 0, 4, 5, 0), new Point2D(0, -5)), Inclusions.Boundary },
            };

            foreach (var test in testCases.Keys)
            {
                var result = Intersections.EllipseContainsPoint(test.ellipse.X, test.ellipse.Y, test.ellipse.RadiusA, test.ellipse.RadiusB, test.ellipse.Angle, test.point.X, test.point.Y);
                var expected = testCases[test];

                Assert.AreEqual(expected, result, $"Test case: {test}, Expected: {result}, Actual: {result}");
            }
        }
        #endregion Contains Tests

        #region Intersection Tests
        /// <summary>
        /// The point line segment test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        public void PointLineSegmentTest()
        {
            // A listing of expected results for specific values.
            var testCases = new Dictionary<((double X, double Y) point, (double AX, double AY, double BX, double BY) line), bool>
            {
                { ((1d, 1d),(1d, 1d, 2d, 2d)), true },
                { ((1.5d, 1.5d),(1d, 1d, 2d, 2d)), true },
                { ((2d, 2d),(1d, 1d, 2d, 2d)), true },
                { ((1d, 2d),(1d, 1d, 2d, 2d)), false },
                { ((2d, 1d),(1d, 1d, 2d, 2d)), false },
            };

            foreach (var test in testCases.Keys)
            {
                var expected = testCases[test];
                var result = Intersections.PointLineSegmentIntersects(test.point.X, test.point.Y, test.line.AX, test.line.AY, test.line.BX, test.line.BY);
                Assert.AreEqual(testCases[test], result, $"Test case: {test}, Expected: {expected}, Actual: {result}");
            }
        }

        /// <summary>
        /// The line line test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        public void LineLineTest()
        {
            // A listing of expected results for specific values.
            var testCases = new Dictionary<((double A1X, double A1Y, double B1X, double B1Y) A, (double A2X, double A2Y, double B2X, double B2Y) B), Intersection>
            {
                // Intersection at one point.
                { ((1d, 1d, 2d, 2d), (1d, 1d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1d, 1d)) },
                // Intersection at other point.
                { ((1d, 1d, 2d, 2d), (2d, 2d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(2d, 2d)) },
                // Lines intersect, segments do not.
                { ((1d, 1d, 2d, 2d), (3d, 2d, 2d, 3d)), new Intersection(IntersectionStates.Intersection, new Point2D(5d, 5d)) },
                // One line intersects other at start/end.
                { ((1d, 1d, 2d, 2d), (1.5d, 1.5d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1.5d, 1.5d)) },
                // One line intersects other at other start/end.
                { ((1.5d, 1.5d, 2d, 2d), (1d, 2d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(3d, 3d)) },
                // Both line segments share a point.
                { ((1.5d, 1.5d, 2d, 2d), (1.5d, 1.5d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1.5d, 1.5d)) },
                // Parallel lines. No intersection.
                { ((1d, 1d, 2d, 2d), (2d, 1d, 3d, 2d)), new Intersection(IntersectionStates.Intersection, new Point2D(-1,-1)) },

                // The following special case intersections need some thought on how to handle. 
                // Coincidental lines. One intersection.
                { ((0d, 0d, 1d, 1d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.NoIntersection) },
                // Same lines. Infinite intersection.
                { ((1d, 1d, 2d, 2d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.NoIntersection) },
                // Same lines, opposite directions. Infinite intersection.
                { ((2d, 2d, 1d, 1d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.NoIntersection) },
                // Same lines, opposite directions. Infinite intersection.
                { ((1d, 1d, 2d, 2d), (2d, 2d, 1d, 1d)), new Intersection(IntersectionStates.NoIntersection) },
            };

            foreach (var test in testCases.Keys)
            {
                var expected = testCases[test];
                var result = Intersections.Intersection(new Line2D(test.A.A1X, test.A.A1Y, test.A.B1X, test.A.B1Y), new Line2D(test.B.A2X, test.B.A2Y, test.B.B2X, test.B.B2Y));
                Assert.AreEqual(testCases[test].State, result.State, $"Test case: {test}, Expected: {expected}, Actual: {result}");

                for (var i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(expected.Points[i].X, result.Points[i].X, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} x coordinate differs.");
                    Assert.AreEqual(expected.Points[i].Y, result.Points[i].Y, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} y coordinate differs.");
                }
            }
        }

        /// <summary>
        /// The line segment line segment test.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        public void LineSegmentLineSegmentTest()
        {
            // A listing of expected results for specific values.
            var testCases = new Dictionary<((double A1X, double A1Y, double B1X, double B1Y) A, (double A2X, double A2Y, double B2X, double B2Y) B), Intersection>
            {
                // Intersection at one point.
                { ((1d, 1d, 2d, 2d), (1d, 1d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1d, 1d)) },
                // Intersection at other point.
                { ((1d, 1d, 2d, 2d), (2d, 2d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(2d, 2d)) },
                // Lines intersect, segments do not.
                { ((1d, 1d, 2d, 2d), (3d, 2d, 2d, 3d)), new Intersection(IntersectionStates.NoIntersection) },
                // One line intersects other at start/end.
                { ((1d, 1d, 2d, 2d), (1.5d, 1.5d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1.5d, 1.5d)) },
                // One line intersects other at other start/end.
                { ((1.5d, 1.5d, 2d, 2d), (1d, 2d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1.5d, 1.5d)) },
                // Both line segments share a point.
                { ((1.5d, 1.5d, 2d, 2d), (1.5d, 1.5d, 2d, 1d)), new Intersection(IntersectionStates.Intersection, new Point2D(1.5d, 1.5d)) },
                // Parallel lines. No intersection.
                { ((1d, 1d, 2d, 2d), (2d, 1d, 3d, 2d)), new Intersection(IntersectionStates.Parallel) },

                // The following special case intersections need some thought on how to handle. 
                // Coincidental lines. One intersection.
                { ((0d, 0d, 1d, 1d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.Coincident) },
                // Same lines. Infinite intersection.
                { ((1d, 1d, 2d, 2d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.Coincident) },
                // Same lines, opposite directions. Infinite intersection.
                { ((2d, 2d, 1d, 1d), (1d, 1d, 2d, 2d)), new Intersection(IntersectionStates.Coincident) },
                // Same lines, opposite directions. Infinite intersection.
                { ((1d, 1d, 2d, 2d), (2d, 2d, 1d, 1d)), new Intersection(IntersectionStates.Coincident) },
            };

            foreach (var test in testCases.Keys)
            {
                var expected = testCases[test];
                var result = Intersections.Intersection(new LineSegment2D(test.A.A1X, test.A.A1Y, test.A.B1X, test.A.B1Y), new LineSegment2D(test.B.A2X, test.B.A2Y, test.B.B2X, test.B.B2Y));
                Assert.AreEqual(testCases[test].State, result.State, $"Test case: {test}, Expected: {expected}, Actual: {result}");

                for (var i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(expected.Points[i].X, result.Points[i].X, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} x coordinate differs.");
                    Assert.AreEqual(expected.Points[i].Y, result.Points[i].Y, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} y coordinate differs.");
                }
            }
        }

        /// <summary>
        /// Test for correct intersections between two Quadratic Bézier curves.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        //[DeploymentItem("System.ValueTuple.dll")]
        public void QuadraticBezierSegmentQuadraticBezierSegmentIntersectionTest()
        {
            // List of test-cases for intersections between two Quadratic Bézier curves.
            var testCases = new Dictionary<((double AX, double AY, double BX, double BY, double CX, double CY) a, (double AX, double AY, double BX, double BY, double CX, double CY) b), Intersection>
            {
                // Parallel vertically mirrored Quadratic Bézier curves.
                { ((0, 0, 10, 10, 20, 0), (0, 5, 10, -5, 20, 5)),
                    new Intersection(IntersectionStates.Intersection, new Point2D(17.0710678118655,2.5), new Point2D(2.92893218813452,2.5)) },
                // Reduce Quintic to Quadratic Parallel Mirrored Quadratic Bézier curves with one leg shifted to the right.
                { ((5, 0, 10, 10, 20, 0), (0, 5, 10, -5, 20, 5)),
                    new Intersection(IntersectionStates.Intersection, new Point2D(17.1265312836548, 2.53937240684556), new Point2D(5.53889706744833, 0.995071968741055)) },
                // KLD four point result Quadratic Bezier intersection test case.
                { ((83, 214, 335, 173, 91, 137), (92, 233, 152, 30, 198, 227)),
                    new Intersection(IntersectionStates.Intersection,
                    new Point2D( 188.275750370236,190.334599086058), new Point2D( 173.837703431114, 152.940611499889),
                    new Point2D( 129.541879876986, 143.272853943596), new Point2D( 98.7272053850424, 211.36259052014)) },
            };

            // Run through the test cases and compare the results to those that are expected.
            foreach (var test in testCases.Keys)
            {
                var result = Intersections.QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
                    test.a.AX, test.a.AY, test.a.BX, test.a.BY, test.a.CX, test.a.CY,
                    test.b.AX, test.b.AY, test.b.BX, test.b.BY, test.b.CX, test.b.CY);
                var expected = testCases[test];

                Assert.AreEqual(expected.State, result.State, $"Test case: {test}, Expected: {expected}, Actual {result}; Intersection state differs.");
                Assert.AreEqual(expected.Points.Count, result.Count, $"Test case: {test}, Expected: {expected}, Actual {result}; Intersection point count differs.");

                for (var i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(expected.Points[i].X, result.Points[i].X, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual {result}; Intersection {i} x coordinate differs.");
                    Assert.AreEqual(expected.Points[i].Y, result.Points[i].Y, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual {result}; Intersection {i} y coordinate differs.");
                }
            }
        }

        /// <summary>
        /// Test for correct self intersections of Cubic Bézier curves.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        //[DeploymentItem("System.ValueTuple.dll")]
        public void CubicBezierSegmentSelfIntersectionTest()
        {
            var testCases = new Dictionary<(double AX, double AY, double BX, double BY, double CX, double CY, double DX, double DY), Intersection>
            {
                { (0, 0, 0, 0, 0, 0, 0, 0), new Intersection(IntersectionStates.NoIntersection, System.Array.Empty<Point2D>()) },
                { (150, 300, 300, 100, 100, 100, 250, 300), new Intersection(IntersectionStates.Intersection, new Point2D[]{ (199.893126357613, 214.564969294897) }) }
            };

            foreach (var test in testCases)
            {
                var result = Intersections.CubicBezierSegmentSelfIntersection(test.Key.AX, test.Key.AY, test.Key.BX, test.Key.BY, test.Key.CX, test.Key.CY, test.Key.DX, test.Key.DY, Maths.Epsilon);

                Assert.AreEqual(test.Value.State, result.State, $"Test case: {test}, Expected: {test.Value}, Actual {result}; Intersection state differs.");
                Assert.AreEqual(test.Value.Points.Count, result.Count, $"Test case: {test}, Expected: {test.Value}, Actual {result}; Intersection point count differs.");

                for (var i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(test.Value.Points[i].X, result.Points[i].X, testEpsilon, $"Test case: {test}, Expected: {test.Value}, Actual: {result}; Intersection {i} x coordinate differs.");
                    Assert.AreEqual(test.Value.Points[i].Y, result.Points[i].Y, testEpsilon, $"Test case: {test}, Expected: {test.Value}, Actual: {result}; Intersection {i} y coordinate differs.");
                }
            }
        }

        /// <summary>
        /// Test for correct intersections between two Cubic Bézier curves.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(IntersectionsTests))]
        [DeploymentItem("Engine.dll")]
        //[DeploymentItem("System.ValueTuple.dll")]
        public void CubicBezierSegmentCubicBezierSegmentIntersectionTest()
        {
            // List of test-cases for intersections between two Quadratic Bézier curves.
            var testCases = new Dictionary<((double AX, double AY, double BX, double BY, double CX, double CY, double DX, double DY) a, (double AX, double AY, double BX, double BY, double CX, double CY, double DX, double DY) b), Intersection>
            {
                // Parallel vertically mirrored Cubic Bézier curves.
                { ((100, 100, 166.66666666666663, 166.66666666666663, 233.33333333333337, 166.66666666666663, 300, 100), (100, 150, 166.66666666666663, 83.333333333333343, 233.33333333333337, 83.333333333333343, 300, 150)),
                    new Intersection(IntersectionStates.Intersection, new Point2D(129.289436340332, 124.99991906534), new Point2D(270.710563659668, 124.99991906534)) },
                // Parallel Mirrored Quadratic Bézier curves with one leg shifted to the right.
                { ((150, 100, 183.33333333333331, 166.66666666666663, 233.33333333333337, 166.66666666666663, 300, 100), (100, 150, 166.66666666666663, 83.333333333333343, 233.33333333333337, 83.333333333333343, 300, 150)),
                    new Intersection(IntersectionStates.Intersection, new Point2D(155.389060528796, 109.950679602517), new Point2D(271.265415062589, 125.393796920216)) },
                // KLD four point result Cubic Bezier intersection test case.
                { ((203, 140, 206, 359, 245, 6,248,212), (177, 204, 441, 204, 8, 149,265,154)),
                    new Intersection(IntersectionStates.Intersection,
                    new Point2D(206.530449213553, 203.720589365125),
                    new Point2D(218.265424922221, 203.397715460424),
                    new Point2D(247.804170297071, 201.44841621768),
                    new Point2D(247.269775330451, 184.697791042228),
                    new Point2D(226.370396487621, 177.962130033979),
                    new Point2D(203.804565424689, 171.00806141425),
                    new Point2D(203.267149868024, 154.381868925147),
                    new Point2D(234.285716881316, 153.679095558988),
                    new Point2D(244.55676560731, 153.715137938049)) },
            };

            // Run through the test cases and compare the results to those that are expected.
            foreach (var test in testCases.Keys)
            {
                var result = Intersections.CubicBezierSegmentCubicBezierSegmentIntersection(
                    test.a.AX, test.a.AY, test.a.BX, test.a.BY, test.a.CX, test.a.CY, test.a.DX, test.a.DY,
                    test.b.AX, test.b.AY, test.b.BX, test.b.BY, test.b.CX, test.b.CY, test.b.DX, test.b.DY);
                var expected = testCases[test];

                Assert.AreEqual(expected.State, result.State, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection state differs.");
                Assert.AreEqual(expected.Points.Count, result.Count, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection point count differs.");

                for (var i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(expected.Points[i].X, result.Points[i].X, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} x coordinate differs.");
                    Assert.AreEqual(expected.Points[i].Y, result.Points[i].Y, testEpsilon, $"Test case: {test}, Expected: {expected}, Actual: {result}; Intersection {i} y coordinate differs.");
                }

            }
        }
        #endregion Intersection Tests
    }
}