using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class EllipseTests
    {
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
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PerimeterTest()
        {
            // Test a perfect circle.
            var ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            double value = ellipse.Perimeter;
            Assert.AreEqual((2 * Math.PI * ellipse.MajorRadius).ToString(), value.ToString());

            // Test a flat line.
            ellipse = new Ellipse(new Point2D(), 100, 0, 0);
            value = ellipse.Perimeter;
            Assert.AreEqual(400.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InterpolateTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InterpolatePointsTest()
        {
            throw new NotImplementedException();
        }
    }
}