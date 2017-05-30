// <copyright file="MeasurementsTests.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// A set of unit test cases designed to test the measurements of various shapes.
    /// </summary>
    [TestClass()]
    public class MeasurementsTests
    {
        #region Constants

        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;

        ///// <summary>
        ///// 
        ///// </summary>
        //private static List<(string description, List<Point2D> polygon)> polygons = new List<(string, List<Point2D>)>();

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
            //polygons.Add(("Square", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(100, 100),
            //    new Point2D(25, 100) }));
            //polygons.Add(("Top Left Triangle", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(25, 100) }));
            //polygons.Add(("Bottom Right Triangle", new List<Point2D> {
            //    new Point2D(100, 100),
            //    new Point2D(100, 25),
            //    new Point2D(25, 100) }));
            //polygons.Add(("Right Reversed Bow-tie", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 100),
            //    new Point2D(100, 25),
            //    new Point2D(25, 100) }));
            //polygons.Add(("Left Reversed Bow-tie", new List<Point2D> {
            //    new Point2D(100, 25),
            //    new Point2D(100, 100),
            //    new Point2D(25, 25),
            //    new Point2D(25, 100) }));
            //polygons.Add(("C Shape", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(100, 50),
            //    new Point2D(50, 50),
            //    new Point2D(50, 75),
            //    new Point2D(100, 75),
            //    new Point2D(100, 100),
            //    new Point2D(25, 100) }));
            //polygons.Add(("n Shape", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(100, 100),
            //    new Point2D(75, 100),
            //    new Point2D(75, 50),
            //    new Point2D(50, 50),
            //    new Point2D(50, 100),
            //    new Point2D(25, 100) }));
            //polygons.Add(("C Bow-tie hole Shape", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(100, 50),
            //    new Point2D(50, 75),
            //    new Point2D(50, 50),
            //    new Point2D(100, 75),
            //    new Point2D(100, 100),
            //    new Point2D(25, 100) }));
            //polygons.Add(("n Bow-tie hole Shape", new List<Point2D> {
            //    new Point2D(25, 25),
            //    new Point2D(100, 25),
            //    new Point2D(100, 100),
            //    new Point2D(75, 100),
            //    new Point2D(50, 50),
            //    new Point2D(75, 50),
            //    new Point2D(50, 100),
            //    new Point2D(25, 100) }));
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

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
        public void ArcAreaTest()
        {
            //var results = new Dictionary<(double radius, double sweepAngle), double>
            //{
            //    { (0, -15), 0 }, { (1, -15), 0.00149017134831433d },
            //    { (0, -14), 0 }, { (1, -14), 0.00121209983976921d },
            //    { (0, -13), 0 }, { (1, -13), 0.000970874207698924d },
            //    { (0, -12), 0 }, { (1, -12), 0.000763909710780109d },
            //    { (0, -11), 0 }, { (1, -11), 0.000588611171415718d },
            //    { (0, -10), 0 }, { (1, -10), 0.000442373766251308d },
            //    { (0, -9), 0 }, { (1, -9), 0.000322583819629393d },
            //    { (0, -8), 0 }, { (1, -8), 0.000226619599740463d },
            //    { (0, -7), 0 }, { (1, -7), 0.000151852117227798d },
            //    { (0, -6), 0 }, { (1, -6), 9.56459260031545E-05d },
            //    { (0, -5), 0 }, { (1, -5), 5.5359926029154E-05d },
            //    { (0, -4), 0 }, { (1, -4), 2.83481678239397E-05d },
            //    { (0, -3), 0 }, { (1, -3), 1.19606584430276E-05d },
            //    { (0, -2), 0 }, { (1, -2), 3.54416869281088E-06d },
            //    { (0, -1), 0 }, { (1, -1), 4.43041329891911E-07d },
            //    { (0, 0), 0 }, { (1, 0), 0 },
            //    { (0, 1), 0 }, { (1, 1), 4.43041329891911E-07d },
            //    { (0, 2), 0 }, { (1, 2), 3.54416869281088E-06d },
            //    { (0, 3), 0 }, { (1, 3), 1.19606584430276E-05d },
            //    { (0, 4), 0 }, { (1, 4), 2.83481678239397E-05d },
            //    { (0, 5), 0 }, { (1, 5), 5.5359926029154E-05d },
            //    { (0, 6), 0 }, { (1, 6), 9.56459260031545E-05d },
            //    { (0, 7), 0 }, { (1, 7), 0.000151852117227798d },
            //    { (0, 8), 0 }, { (1, 8), 0.000226619599740463d },
            //    { (0, 9), 0 }, { (1, 9), 0.000322583819629393d },
            //    { (0, 10), 0 }, { (1, 10), 0.000442373766251308d },
            //    { (0, 11), 0 }, { (1, 11), 0.000588611171415718d },
            //    { (0, 12), 0 }, { (1, 12), 0.000763909710780109d },
            //    { (0, 13), 0 }, { (1, 13), 0.000970874207698924d },
            //    { (0, 14), 0 }, { (1, 14), 0.00121209983976921d },
            //    { (0, 15), 0 }, { (1, 15), 0.00149017134831433d },
            //};

            //foreach (var arc in results.Keys)
            //{
            //    var area = Measurements.CircularArcSectorArea(arc.radius, arc.sweepAngle.ToRadians());
            //    Assert.AreEqual(results[(arc)], area, TestEpsilon);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
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
                Assert.AreEqual(results[r], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
        public void EllipseAreaTest()
        {
            //// A listing of expected results for specific integer values.
            //var results = new Dictionary<(double r1, double r2), double>
            //{
            //    { (0d, 0d), 0d }, { (0d, 1d), 0d },
            //    { (1d, 0d), 0d }, { (1d, 1d), 3.14159265358979d },
            //    { (2d, 0d), 0d }, { (2d, 1d), 6.28318530717959d },
            //    { (3d, 0d), 0d }, { (3d, 1d), 9.42477796076938d },
            //};

            //// Run tests for each radius.
            //foreach (var radii in results.Keys)
            //{
            //    // Retrieve the area of the circle, provided it's radius. 
            //    var area = Measurements.EllipseArea(radii.r1, radii.r2);

            //    // Check for a correct result.
            //    Assert.AreEqual(results[(radii)], area, TestEpsilon);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
        public void RectangleAreaTest()
        {
            //// A listing of expected results for specific integer values.
            //var results = new Dictionary<(double w, double h), double>
            //{
            //    { (0d, 0d), 0d }, { (0d, 1d), 0d },
            //    { (1d, 0d), 0d }, { (1d, 1d), 1d },
            //    { (2d, 0d), 0d }, { (2d, 1d), 2d },
            //    { (3d, 0d), 0d }, { (3d, 1d), 3d },
            //};

            //// Run tests for each height and width.
            //foreach (var size in results.Keys)
            //{
            //    // Retrieve the area of the circle, provided it's radius. 
            //    var area = Measurements.RectangleArea(size.w, size.h);

            //    // Check for a correct result.
            //    Assert.AreEqual(results[size], area, TestEpsilon);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
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
                Assert.AreEqual(results[w], area, TestEpsilon);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
        public void PolygonAreaTest()
        {
            //// A listing of expected results for specific polygons.
            //var results = new List<double>
            //{
            //    5625,
            //    2812.5,
            //    2812.5,
            //    // ToDo: Find fix!
            //    0, // What!?
            //    0, // What!?
            //    4375,
            //    4375,
            //    5625,
            //    5625,
            //};

            //// Run tests for each polygon.
            //for (var i = 0; i < results.Count; i++)
            //{
            //    // Retrieve the area of the polygon, provided a chain of points. 
            //    var area = Measurements.SignedPolygonArea(polygons[i].polygon);

            //    // Check for a correct result.
            //    Assert.AreEqual(results[i], area, TestEpsilon, polygons[i].description);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MeasurementsTests")]
        [Ignore]
        public void SignedPolygonAreaTest()
        {
            //// A listing of expected results for specific polygons.
            //var results = new List<double>
            //{
            //    -5625,
            //    -2812.5,
            //    2812.5,
            //    // ToDo: Find fix!
            //    0, // What!?
            //    0, // What!?
            //    -4375,
            //    -4375,
            //    -5625,
            //    -5625,
            //};

            //// Run tests for each polygon.
            //for (var i = 0; i < results.Count; i++)
            //{
            //    // Retrieve the area of the polygon, provided a chain of points. 
            //    var area = Measurements.SignedPolygonArea(polygons[i].polygon);

            //    // Check for a correct result.
            //    Assert.AreEqual(results[i], area, TestEpsilon, polygons[i].description);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CircularArcTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void EllipseTest1()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void EllipticalArcTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RotatedRectangleTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void BoundsTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ArcLengthTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CircleCircumferenceTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CubicBezierArcLengthTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void EllipsePerimeterTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void QuadraticBezierArcLengthByIntegralTest()
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PolygonPerimeterTest()
            => throw new NotImplementedException();
    }
}