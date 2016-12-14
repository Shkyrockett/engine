// <copyright file="MathExtensionsTests.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Tests
{
    /// <summary>
    /// This test class for the <see cref="Maths"/> class, is intended to 
    /// contain all of the Unit tests for the <see cref="Maths"/> class.
    /// </summary>
    [TestClass]
    public class MathExtensionsTests
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
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        /// <summary>
        /// A Test for converting Radians to Degrees.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ToRadiansTest()
        {
            double value = 0;
            value = Maths.ToRadians(0);
            Assert.AreEqual(0, value);
            value = Maths.ToRadians(30);
            Assert.AreEqual(30 * (Math.PI / 180f), value);
            value = Maths.ToRadians(45);
            Assert.AreEqual(45 * (Math.PI / 180f), value);
            value = Maths.ToRadians(60);
            Assert.AreEqual(60 * (Math.PI / 180f), value);
            value = Maths.ToRadians(90);
            Assert.AreEqual(90 * (Math.PI / 180f), value);
            value = Maths.ToRadians(120);
            Assert.AreEqual(120 * (Math.PI / 180f), value);
            value = Maths.ToRadians(135);
            Assert.AreEqual(135 * (Math.PI / 180f), value);
            value = Maths.ToRadians(150);
            Assert.AreEqual(150 * (Math.PI / 180f), value);
            value = Maths.ToRadians(180);
            Assert.AreEqual(180 * (Math.PI / 180f), value);
            value = Maths.ToRadians(210);
            Assert.AreEqual(210 * (Math.PI / 180f), value);
            value = Maths.ToRadians(225);
            Assert.AreEqual(225 * (Math.PI / 180f), value);
            value = Maths.ToRadians(240);
            Assert.AreEqual(240 * (Math.PI / 180f), value);
            value = Maths.ToRadians(270);
            Assert.AreEqual(270 * (Math.PI / 180f), value);
            value = Maths.ToRadians(300);
            Assert.AreEqual(300 * (Math.PI / 180f), value);
            value = Maths.ToRadians(315);
            Assert.AreEqual(315 * (Math.PI / 180f), value);
            value = Maths.ToRadians(330);
            Assert.AreEqual(330 * (Math.PI / 180f), value);
            value = Maths.ToRadians(360);
            Assert.AreEqual(360 * (Math.PI / 180f), value);
        }

        /// <summary>
        /// A Test for converting Degrees to Radians.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ToDegreesTest()
        {
            double value = 0;
            value = Maths.ToDegrees(0);
            Assert.AreEqual(0, value);
            value = Maths.ToDegrees(30);
            Assert.AreEqual(30 * (180f / Math.PI), value);
            value = Maths.ToDegrees(45);
            Assert.AreEqual(45 * (180f / Math.PI), value);
            value = Maths.ToDegrees(60);
            Assert.AreEqual(60 * (180f / Math.PI), value);
            value = Maths.ToDegrees(90);
            Assert.AreEqual(90 * (180f / Math.PI), value);
            value = Maths.ToDegrees(120);
            Assert.AreEqual(120 * (180f / Math.PI), value);
            value = Maths.ToDegrees(135);
            Assert.AreEqual(135 * (180f / Math.PI), value);
            value = Maths.ToDegrees(150);
            Assert.AreEqual(150 * (180f / Math.PI), value);
            value = Maths.ToDegrees(180);
            Assert.AreEqual(180 * (180f / Math.PI), value);
            value = Maths.ToDegrees(210);
            Assert.AreEqual(210 * (180f / Math.PI), value);
            value = Maths.ToDegrees(225);
            Assert.AreEqual(225 * (180f / Math.PI), value);
            value = Maths.ToDegrees(240);
            Assert.AreEqual(240 * (180f / Math.PI), value);
            value = Maths.ToDegrees(270);
            Assert.AreEqual(270 * (180f / Math.PI), value);
            value = Maths.ToDegrees(300);
            Assert.AreEqual(300 * (180f / Math.PI), value);
            value = Maths.ToDegrees(315);
            Assert.AreEqual(315 * (180f / Math.PI), value);
            value = Maths.ToDegrees(330);
            Assert.AreEqual(330 * (180f / Math.PI), value);
            value = Maths.ToDegrees(360);
            Assert.AreEqual(360 * (180f / Math.PI), value);
        }

        /// <summary>
        /// A Test for rounding a number to an arbitrary value.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void RoundToMultipleTest()
        {
            double value = 0;
            value = Maths.RoundToMultiple(3.5, 1.5);
            Assert.AreEqual(3, value);
        }

        /// <summary>
        /// A Test for retrieving the modulo of an arbitrary value, the same way Excel does.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ModuloTest()
        {
            double value = 0;
            value = Maths.Modulo(3.5, 1.5);
            Assert.AreEqual(0.5d, value);
        }

        /// <summary>
        /// A Test for finding the average value from a list of numbers.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void AverageTest()
        {
            double value = 0;
            var array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = Maths.Average(array);
            Assert.AreEqual(4.5, value);
        }

        /// <summary>
        /// A Test for finding the sum value from a list of numbers.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void SumTest()
        {
            double value = 0;
            var array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = Maths.Sum(array);
            Assert.AreEqual(45, value);
        }

        [TestMethod]
        [Ignore]
        public void RandomTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void Atan2Test()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void Atan2Test()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void SecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void CosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void CotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicTangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HyperbolicCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicTangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void LogarithmTobaseNTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void ToFloatTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void LessThanTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void GreaterThanTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void LessThanOrCloseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void GreaterThanOrCloseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void IsOneTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void IsZeroTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void IsBetweenZeroAndOneTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void FloatToIntTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void DoubleToIntTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void HIWORDTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        [TestMethod]
        [Ignore]
        public void LOWORDTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }
    }
}