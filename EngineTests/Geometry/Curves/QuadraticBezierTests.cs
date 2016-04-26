using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class QuadraticBezierTests
    {
        /// <summary>
        /// 
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierTests"/> class.
        /// </summary>
        public QuadraticBezierTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        { }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        { }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        { }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        { }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        public void QuadraticBezierLengthTest()
        {
            QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            double value = bezier.Length;
            Assert.AreEqual(161.735239810224d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        public void QuadraticBezierArcLengthByIntegralTest()
        {
            QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            double value = bezier.QuadraticBezierArcLengthByIntegral();
            Assert.AreEqual(161.735239810224d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        public void QuadraticBezierArcLengthBySegmentsTest()
        {
            QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            double value = bezier.QuadraticBezierArcLengthBySegments();
            Assert.AreEqual(160.211718695931d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        public void QuadraticBezierApproxArcLengthTest()
        {
            QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            double value = bezier.QuadraticBezierApproxArcLength();
            Assert.AreEqual(159.821919863669d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        public void QuadraticBezierInterpolateTest()
        {
            QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            Point2D value = bezier.Interpolate(0.5);
            Assert.AreEqual(new Point2D(53, 225), value);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [Ignore]
        public void QuadraticBezierInterpolatePointsTest()
        {
            throw new NotImplementedException();
        }
    }
}