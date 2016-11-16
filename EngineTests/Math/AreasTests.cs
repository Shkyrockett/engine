// <copyright file="AreasTests.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Engine.Tests
{
    /// <summary>
    /// A set of unit test cases designed to test the areas of various shapes.
    /// </summary>
    [TestClass()]
    public class AreasTests
    {
        /// <summary>
        /// A value indicating the amount of diference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;

        /// <summary>
        /// 
        /// </summary>
        private static List<(string description, List<Point2D> polygon)> polygons = new List<(string, List<Point2D>)>();

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            polygons.Add(("Square", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 100) }));
            polygons.Add(("Top Left Triangle", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(25, 100) }));
            polygons.Add(("Bottom Right Triangle", new List<Point2D> {
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100) }));
            polygons.Add(("Right Reversed Bow-tie", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 100),
                new Point2D(100, 25),
                new Point2D(25, 100) }));
            polygons.Add(("Left Reversed Bow-tie", new List<Point2D> {
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(25, 25),
                new Point2D(25, 100) }));
            polygons.Add(("C Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 50),
                new Point2D(50, 75),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100) }));
            polygons.Add(("n Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(75, 50),
                new Point2D(50, 50),
                new Point2D(50, 100),
                new Point2D(25, 100) }));
            polygons.Add(("C Bow-tie hole Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 50),
                new Point2D(50, 75),
                new Point2D(50, 50),
                new Point2D(100, 75),
                new Point2D(100, 100),
                new Point2D(25, 100) }));
            polygons.Add(("n Bow-tie hole Shape", new List<Point2D> {
                new Point2D(25, 25),
                new Point2D(100, 25),
                new Point2D(100, 100),
                new Point2D(75, 100),
                new Point2D(50, 50),
                new Point2D(75, 50),
                new Point2D(50, 100),
                new Point2D(25, 100) }));
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

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void ArcTest()
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
                var area = Areas.CircularArcSector(arc.radius, arc.sweepAngle.ToRadians());
                Assert.AreEqual(results[(arc)], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void CircleTest()
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
                var area = Areas.Circle(r);

                // Check for a correct result.
                Assert.AreEqual(results[r], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void EllipseTest()
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
                var area = Areas.Ellipse(radii.r1, radii.r2);

                // Check for a correct result.
                Assert.AreEqual(results[(radii)], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void RectangleTest()
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
                var area = Areas.Rectangle(size.w, size.h);

                // Check for a correct result.
                Assert.AreEqual(results[size], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void SquareTest()
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
                var area = Areas.Square(w);

                // Check for a correct result.
                Assert.AreEqual(results[w], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        //[Ignore]
        public void PolygonTest()
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
            for (int i = 0; i < results.Count; i++)
            {
                // Retrieve the area of the polygon, provided a chain of points. 
                var area = Areas.Polygon(polygons[i].polygon);

                // Check for a correct result.
                Assert.AreEqual(results[i], area, TestEpsilon, polygons[i].description);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "AreasTests")]
        public void SignedPolygonTest()
        {
            // A listing of expected results for specific polygons.
            var results = new List<double>
            {
                -5625,
                -2812.5,
                2812.5,
                // ToDo: Find fix!
                0, // What!?
                0, // What!?
                -4375,
                -4375,
                -5625,
                -5625,
            };

            // Run tests for each polygon.
            for (int i = 0; i < results.Count; i++)
            {
                // Retrieve the area of the polygon, provided a chain of points. 
                var area = Areas.SignedPolygon(polygons[i].polygon);

                // Check for a correct result.
                Assert.AreEqual(results[i], area, TestEpsilon, polygons[i].description);
            }
        }
    }
}