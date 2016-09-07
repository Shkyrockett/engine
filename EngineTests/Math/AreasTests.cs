using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Engine.Geometry.Tests
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
        [TestMethod()]
        public void ArcTest()
        {
            double radiusLimit = 1;
            double angleLowerLimit = -15;// -360;
            double angleUpperLimit = 15;// 360;

            var results = new Dictionary<(double, double), double>
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

            for (double radius = 0; radius <= radiusLimit; radius += 1d)
            {
                for (double sweepAngle = angleLowerLimit; sweepAngle < angleUpperLimit; sweepAngle++)
                {
                    var area = Areas.Arc(radius, sweepAngle.ToRadians());
                    Assert.AreEqual(results[(radius, sweepAngle)], area, TestEpsilon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void CircleTest()
        {
            // The maximum radius to test.
            double radiusLimit = 3;

            // A listing of expected results for specific integer values.
            var results = new Dictionary<double, double>
            {
                { 0d, 0d },
                { 1d, 3.14159265358979d },
                { 2d, 12.5663706143592d },
                { 3d, 28.2743338823081d },
            };

            // Run tests for each radius.
            for (double r = 0; r <= radiusLimit; r++)
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
        public void EllipseTest()
        {
            // The maximum height to test.
            double r1Limit = 3;

            // The maximum width to test.
            double r2Limit = 1;

            // A listing of expected results for specific integer values.
            var results = new Dictionary<(double, double), double>
            {
                { (0d, 0d), 0d }, { (0d, 1d), 0d },
                { (1d, 0d), 0d }, { (1d, 1d), 3.14159265358979d },
                { (2d, 0d), 0d }, { (2d, 1d), 6.28318530717959d },
                { (3d, 0d), 0d }, { (3d, 1d), 9.42477796076938d },
            };

            // Run tests for each radius.
            for (double r1 = 0; r1 <= r1Limit; r1++)
            {
                for (double r2 = 0; r2 <= r2Limit; r2++)
                {
                    // Retrieve the area of the circle, provided it's radius. 
                    var area = Areas.Ellipse(r1, r2);

                    // Check for a correct result.
                    Assert.AreEqual(results[(r1, r2)], area, TestEpsilon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void RectangleTest()
        {
            // The maximum width to test.
            double widthLimit = 1;

            // The maximum height to test.
            double heightLimit = 1;

            // A listing of expected results for specific integer values.
            var results = new Dictionary<(double, double), double>
            {
                { (0d, 0d), 0d }, { (0d, 1d), 0d },
                { (1d, 0d), 0d }, { (1d, 1d), 1d },
                { (2d, 0d), 0d }, { (2d, 1d), 2d },
                { (3d, 0d), 0d }, { (3d, 1d), 3d },
            };

            // Run tests for each radius.
            for (double w = 0; w <= widthLimit; w++)
            {
                for (double h = 0; h <= heightLimit; h++)
                {
                    // Retrieve the area of the circle, provided it's radius. 
                    var area = Areas.Rectangle(w, h);

                    // Check for a correct result.
                    Assert.AreEqual(results[(w, h)], area, TestEpsilon);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void SquareTest()
        {
            // The maximum width to test.
            double widthLimit = 3;

            // A listing of expected results for specific integer values.
            var results = new Dictionary<double, double>
            {
                { 0d, 0d },
                { 1d, 1d },
                { 2d, 4d },
                { 3d, 9d },
            };

            // Run tests for each radius.
            for (double w = 0; w <= widthLimit; w++)
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
        [Ignore]
        public void PolygonTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SignedPolygonTest()
        {
            throw new NotImplementedException();
        }
    }
}