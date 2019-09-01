// <copyright file="QuadraticBezierTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace EngineTests
{
    /// <summary>
    /// The quadratic bezier tests unit test class.
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
        { }

        /// <summary>
        /// TestCleanup runs code after each test has run.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        { }

        /// <summary>
        /// ClassCleanup runs code after all tests in a class have run.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        { }
        #endregion Housekeeping

        /// <summary>
        /// The quadratic bezier length test.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
        [DeploymentItem("Engine.dll")]
        public void QuadraticBezierLengthTest()
        {
            var bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            var value = bezier.Length;
            Assert.AreEqual(161.735239810224d.ToString(CultureInfo.InvariantCulture), value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The quadratic bezier arc length by integral test.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
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
        /// The quadratic bezier arc length by segments test.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
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
        /// The quadratic bezier approx arc length test.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
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
        /// The quadratic bezier interpolate test.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierInterpolateTest()
        {
            var bezier = new QuadraticBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150));
            var value = bezier.Interpolate(0.5);
            Assert.AreEqual(new Point2D(53, 225), value);
        }

        /// <summary>
        /// The quadratic bezier interpolate points test.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void QuadraticBezierInterpolatePointsTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }
    }
}