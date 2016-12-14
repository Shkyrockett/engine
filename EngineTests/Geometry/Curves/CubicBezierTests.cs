﻿// <copyright file="CubicBezierTests.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Engine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CubicBezierTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var bezier = new CubicBezier(new Point2D(32, 150), new Point2D(50, 300), new Point2D(80, 150), new Point2D(44, 66));
            string value = bezier.ToString();
            Assert.AreEqual("CubicBezier={A=Point2D{X=32,Y=150},B=Point2D{X=50,Y=300},C=Point2D{X=80,Y=150},D=Point2D{X=44,Y=66}}", value);
        }
    }
}