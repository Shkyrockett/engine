using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// This test class for the <see cref="IntersectionExtention"/> class, is intended to 
    /// contain all of the Unit tests for the <see cref="IntersectionExtention"/> class.
    /// </summary>
    [TestClass()]
    public class IntersectionExtentionTests
    {
        /// <summary>
        /// 
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionExtentionTests"/> class.
        /// </summary>
        public IntersectionExtentionTests()
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
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        [TestMethod()]
        [Ignore]
        public void AreCloseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void AreCloseTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void AreCloseTest2()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void AreCloseTest3()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void AreCloseTest4()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void RectHasNaNTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonSaeedAmiriTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonKeithTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonMeowNETTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonAlienRyderFlexTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonNathanMercerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonLaschaLagidseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonGilKrTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonMKatzTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInPolygonRodStephensTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInArcTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PointInCircleTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void CentroidTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PolygonAreaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void SignedPolygonAreaTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void IsConvexTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void FindEarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void RemoveEarTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void RemovePointTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void TriangulateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void PolygonIsOrientedClockwiseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void OrientPolygonClockwiseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void SegmentsIntersectTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void GetAngleTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void GetAngleTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void CrossProductLengthTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void CrossProductLengthTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void DotProductTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void DotProductTest1()
        {
            throw new NotImplementedException();
        }
    }
}