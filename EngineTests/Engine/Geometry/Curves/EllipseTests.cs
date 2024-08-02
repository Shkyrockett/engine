// <copyright file="EllipseTests.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace EngineTests;

/// <summary>
/// The ellipse tests unit test class.
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
    /// The perimeter test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty(nameof(Engine), nameof(EllipseTests))]
    [DeploymentItem("Engine.dll")]
    public void PerimeterTest()
    {
        // Test a perfect circle.
        var ellipse = new Ellipse2D(new Point2D(), 100, 100, 0);
        var value = ellipse.Perimeter;
        Assert.AreEqual((2d * Math.PI * ellipse.MajorRadius).ToString(CultureInfo.InvariantCulture), value.ToString(CultureInfo.InvariantCulture));

        // Test a flat line.
        ellipse = new Ellipse2D(new Point2D(), 100, 0, 0);
        value = ellipse.Perimeter;
        Assert.AreEqual(400.ToString(CultureInfo.InvariantCulture), value.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// The interpolate test.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
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
    /// The interpolate points test.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
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