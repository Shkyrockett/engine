// <copyright file="QuadraticBezierTests.cs" company="Shkyrockett" >
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

namespace Engine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class QuadraticBezierTests
    {
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
        { }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        { }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        { }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        { }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        public void QuadraticBezierLengthTest()
        {
            var bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            var value = bezier.Length;
            Assert.AreEqual(161.735239810224d.ToString(), value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierArcLengthByIntegralTest()
        {
            //QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            //double value = bezier.QuadraticBezierArcLengthByIntegral();
            //Assert.AreEqual(161.735239810224d.ToString(), value.ToString());
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierArcLengthBySegmentsTest()
        {
            //QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            //double value = bezier.QuadraticBezierArcLengthBySegments();
            //Assert.AreEqual(160.211711355793d.ToString(), value.ToString());
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierApproxArcLengthTest()
        {
            //QuadraticBezier bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            //double value = bezier.QuadraticBezierApproxArcLength();
            //Assert.AreEqual(159.821919863669d.ToString(), value.ToString());
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierInterpolateTest()
        {
            var bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            Point2D value = bezier.Interpolate(0.5);
            Assert.AreEqual(new Point2D(53, 225), value);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "QuadraticBezierTests")]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierInterpolatePointsTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }
    }
}