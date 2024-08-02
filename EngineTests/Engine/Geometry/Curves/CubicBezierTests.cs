// <copyright file="CubicBezierTests.cs" company="Shkyrockett" >
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
/// The cubic Bézier tests unit test class.
/// </summary>
[TestClass]
public class CubicBezierTests
{
    /// <summary>
    /// The to string test.
    /// </summary>
    [TestMethod]
    [Priority(0)]
    [Owner("Shkyrockett")]
    [TestProperty(nameof(Engine), nameof(QuadraticBezierTests))]
    [DeploymentItem("Engine.dll")]
    public void ToStringTest()
    {
        var bezier = new CubicBezier2D(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150), new Point2D(44, 66));
        var value = bezier.ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual("CubicBezier={A=Point2D=[X:32, Y:150],B=Point2D=[X:50, Y:300],C=Point2D=[X:80, Y:150],D=Point2D=[X:44, Y:66]}", value);
    }
}