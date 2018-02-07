// <copyright file="EllipseTests.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    public class EllipseTests
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
        #endregion Housekeeping

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(EllipseTests))]
        [DeploymentItem("Engine.dll")]
        public void PerimeterTest()
        {
            // Test a perfect circle.
            var ellipse = new Ellipse(new Point2D(), 100, 100, 0);
            var value = ellipse.Perimeter;
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
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(EllipseTests))]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InterpolateTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(EllipseTests))]
        [DeploymentItem("Engine.dll")]
        [Ignore]
        public void InterpolatePointsTest()
        {
            Assert.Inconclusive("ToDo: Implement code to verify target.");
            throw new NotImplementedException();
        }
    }
}