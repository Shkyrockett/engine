// <copyright file="ExperimentsTests.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MethodSpeedTester.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ExperimentsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect0Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);

            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection0(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection0(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection0(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection0(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection0(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect1Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);
            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection1(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection1(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection1(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection1(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection1(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect2Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);
            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection2(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection2(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection2(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection2(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection2(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect3Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);
            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection3(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection3(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection3(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection3(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection3(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect4Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);
            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection4(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection4(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection4(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection4(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection4(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Intersect5Test()
        {
            var sement1 = new(double, double, double, double)(0, 0, 2, 2);
            var sement2 = new(double, double, double, double)(2, 0, 0, 2);
            (bool, (double X, double Y) ?) value = (false, null);

            // Check diagonal crossing lines.
            for (int i = 0; i < 1000000; i++)
            {
                value = Experiments.Intersection5(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            }
            Assert.AreEqual(true, value.Item1, "Checking diagonal crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking diagonal crossing lines.");

            // Check horizontal and vertical crossing lines.
            sement1 = new(double, double, double, double)(1, 0, 1, 2);
            sement2 = new(double, double, double, double)(0, 1, 2, 1);
            value = Experiments.Intersection5(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(true, value.Item1, "Checking horizontal and vertical crossing lines.");
            Assert.AreEqual((1, 1).ToString(), value.Item2.ToString(), "Checking horizontal and vertical crossing lines.");

            // Check non-intersecting segments with extended intersection.
            sement1 = new(double, double, double, double)(1, 3, 2, 2);
            sement2 = new(double, double, double, double)(4, 2, 5, 3);
            value = Experiments.Intersection5(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking non-intersecting segments with extended intersection.");
            Assert.AreEqual((3, 1).ToString(), value.Item2.ToString(), "Checking non-intersecting segments with extended intersection.");

            // Check vertical parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 0, 2);
            sement2 = new(double, double, double, double)(2, 0, 2, 2);
            value = Experiments.Intersection5(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking vertical parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking vertical parallel lines.");

            // Check horizontal parallel lines.
            sement1 = new(double, double, double, double)(0, 0, 2, 0);
            sement2 = new(double, double, double, double)(0, 2, 2, 2);
            value = Experiments.Intersection5(sement1.Item1, sement1.Item2, sement1.Item3, sement1.Item4, sement2.Item1, sement2.Item2, sement2.Item3, sement2.Item4);
            Assert.AreEqual(false, value.Item1, "Checking horizontal parallel lines.");
            Assert.AreEqual(null, value.Item2, "Checking horizontal parallel lines.");
        }
    }
}