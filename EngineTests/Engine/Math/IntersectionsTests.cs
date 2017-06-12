// <copyright file="IntersectionsTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Engine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class IntersectionsTests
    {
        #region Constants

        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Housekeeping

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            //MessageBox.Show("TestClassInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        #endregion

        #region Contains Tests

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest1()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest2()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest3()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest4()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest5()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest6()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest7()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest8()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest9()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void ContainsTest10()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        public void PointLineSegmentTest()
        {
            // A listing of expected results for specific values.
            var testCases = new Dictionary<(double AX, double AY, double BX, double BY, double X, double Y), bool>
            {
                { (1d, 1d, 2d, 2d, 1d, 1d), true },
                { (1d, 1d, 2d, 2d, 1.5d, 1.5d), true },
                { (1d, 1d, 2d, 2d, 2d, 2d), true },
                { (1d, 1d, 2d, 2d, 1d, 2d), false },
                { (1d, 1d, 2d, 2d, 2d, 1d), false },
            };

            foreach (var testCase in testCases.Keys)
            {
                var result = Intersections.PointLineSegmentIntersects(testCase.AX, testCase.AY, testCase.BX, testCase.BY, testCase.X, testCase.Y);
                Assert.AreEqual(testCases[testCase], result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        public void LineLineTest()
        {
            // A listing of expected results for specific values.
            var testCases = new Dictionary<(double A1X, double A1Y, double B1X, double B1Y, double A2X, double A2Y, double B2X, double B2Y), (bool, Point2D?)>
            {
                // Intersection at one point.
                { (1d, 1d, 2d, 2d, 1d, 1d, 2d, 1d), (true, new Point2D(1d, 1d)) },
                // Intersection at other point.
                { (1d, 1d, 2d, 2d, 2d, 2d, 2d, 1d), (true, new Point2D(2d, 2d)) },
                // Lines intersect, segments do not.
                { (1d, 1d, 2d, 2d, 3d, 2d, 2d, 3d), (false, new Point2D(2.5d, 2.5d)) },
                // One line intersects other at start/end.
                { (1d, 1d, 2d, 2d, 1.5d, 1.5d, 2d, 1d), (true, new Point2D(1.5d, 1.5d)) },
                // One line intersects other at other start/end.
                { (1.5d, 1.5d, 2d, 2d, 1d, 2d, 2d, 1d), (true, new Point2D(1.5d, 1.5d)) },
                // Both line segments share a point.
                { (1.5d, 1.5d, 2d, 2d, 1.5d, 1.5d, 2d, 1d), (true, new Point2D(1.5d, 1.5d)) },
                // Parallel lines. No intersection.
                { (1d, 1d, 2d, 2d, 2d, 1d, 3d, 2d), (false, null) },

                // The following special case intersections need some thought on how to handle. 
                // Coincidental lines. One intersection.
                { (0d, 0d, 1d, 1d, 1d, 1d, 2d, 2d), (false, null) },
                // Same lines. Infinite intersection.
                { (1d, 1d, 2d, 2d, 1d, 1d, 2d, 2d), (false, null) },
                // Same lines, opposite directions. Infinite intersection.
                { (2d, 2d, 1d, 1d, 1d, 1d, 2d, 2d), (false, null) },
                // Same lines, opposite directions. Infinite intersection.
                { (1d, 1d, 2d, 2d, 2d, 2d, 1d, 1d), (false, null) },
            };

            foreach (var testCase in testCases.Keys)
            {
                var result = Intersections.Intersection(new Line(testCase.A1X, testCase.A1Y, testCase.B1X, testCase.B1Y), new Line(testCase.A2X, testCase.A2Y, testCase.B2X, testCase.B2Y));
                Assert.AreEqual(testCases[testCase], result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [DeploymentItem("System.ValueTuple.dll")]
        public void QuadraticBezierSegmentQuadraticBezierSegmentIntersectionTest()
        {
            var testCases = new Dictionary<(QuadraticBezier a, QuadraticBezier b), Intersection>
            {
                { (new QuadraticBezier(0, 0, 10, 10, 20, 0), new QuadraticBezier(0, 10, 10, 0, 20, 10)), new Intersection() },
            };

            foreach (var test in testCases.Keys)
            {
                var result = Intersections.QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
                    test.a.A.X, test.a.A.Y, test.a.B.X, test.a.B.Y, test.a.C.X, test.a.C.Y,
                    test.b.A.X, test.b.A.Y, test.b.B.X, test.b.B.Y, test.b.C.X, test.b.C.Y);
                var expected = testCases[test];

                Assert.AreEqual(expected, result, $"Test case: {test}, Expected: {result}, Actual {result}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        public void CircleContainsPointTest()
        {
            var testCases = new Dictionary<(Circle circle, Point2D point), Inclusion>
            {
                { (new Circle(0, 0, 5), new Point2D(1, 1)), Inclusion.Inside },
                { (new Circle(0, 0, 5), new Point2D(0, 0)), Inclusion.Inside },
                { (new Circle(0, 0, 5), new Point2D(5, 5)), Inclusion.Outside },
                { (new Circle(0, 0, 5), new Point2D(5, -5)), Inclusion.Outside },
                { (new Circle(0, 0, 5), new Point2D(-5, -5)), Inclusion.Outside },
                { (new Circle(0, 0, 5), new Point2D(-5, 5)), Inclusion.Outside },
                { (new Circle(0, 0, 5), new Point2D(0, 5)), Inclusion.Boundary },
                { (new Circle(0, 0, 5), new Point2D(0, -5)), Inclusion.Boundary },
            };

            foreach (var test in testCases.Keys)
            {
                var result = Intersections.CircleContainsPoint(test.circle.X, test.circle.Y, test.circle.Radius, test.point.X, test.point.Y);
                var expected = testCases[test];

                Assert.AreEqual(expected, result, $"Test case: {test}, Expected: {result}, Actual {result}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        public void EllipsePointTest()
        {
            var testCases = new Dictionary<(Ellipse ellipse, Point2D point), Inclusion>
            {
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(1, 1)), Inclusion.Inside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(0, 0)), Inclusion.Inside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(5, 5)), Inclusion.Outside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(5, -5)), Inclusion.Outside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(-5, -5)), Inclusion.Outside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(-5, 5)), Inclusion.Outside },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(0, 5)), Inclusion.Boundary },
                { (new Ellipse(0, 0, 4, 5, 0), new Point2D(0, -5)), Inclusion.Boundary },
            };

            foreach (var test in testCases.Keys)
            {
                var result = Intersections.EllipseContainsPoint(test.ellipse.X, test.ellipse.Y, test.ellipse.RX, test.ellipse.RY, test.ellipse.Angle, test.point.X, test.point.Y);
                var expected = testCases[test];

                Assert.AreEqual(expected, result, $"Test case: {test}, Expected: {result}, Actual {result}");
            }
        }

        #region Ignored

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void CircularArcSectorPointTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void EllipticSectorPointTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void RectanglePointTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void PolygonPointTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void PolygonSetPointTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void RectangleRectangleTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "IntersectionsTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void PolygonPolygonTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        #endregion
    }
}