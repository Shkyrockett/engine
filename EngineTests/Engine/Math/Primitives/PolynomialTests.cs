// <copyright file="PolynomialTests.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace EngineTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PolynomialTests
    {
        #region Constants

        /// <summary>
        /// A value indicating the amount of difference a test may have in the return value.
        /// </summary>
        private const double TestEpsilon = 0.0000000000001d;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Class housekeeping

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

        #endregion

        /// <summary>
        /// Test the <see cref="Polynomial.Trim"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void TrimTest()
        {
            var cases = new Dictionary<Polynomial, Polynomial>
            {
                {new Polynomial(0d), new Polynomial(0d)},
                {new Polynomial(2d, 0d, 25d, 0d, 4d, 0d), new Polynomial(2d, 0d, 25d, 0d, 4d, 0d)},
                {new Polynomial(0d, 5d, 25d, 0d, 4d, 0d), new Polynomial(5d, 25d, 0d, 4d, 0d)},
                {new Polynomial(0d, 0d, 25d, 0d, 4d, 0d), new Polynomial(25d, 0d, 4d, 0d)},
                {new Polynomial(0d, 0d, 0d, 8d, 4d, 0d), new Polynomial(8d, 4d, 0d)},
                {new Polynomial(0d, 0d, 0d, 0d, 4d, 0d), new Polynomial(4d, 0d)},
                {new Polynomial(0d, 0d, 0d, 0d, 0d, 1d), new Polynomial(1d)},
                {new Polynomial(1d, 0d, 0d, 0d, 0d, 0d), new Polynomial(1d, 0d, 0d, 0d, 0d, 0d)},
            };

            foreach (var test in cases)
            {
                Assert.AreEqual(test.Value.Count, (test.Key.Trim()).Count);
                Assert.AreEqual(test.Value, test.Key.Trim());
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Derivate"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void DerivateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = new Polynomial(0, 20, 75, 0, 4);
            var result = value.Derivate();
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Normalize(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void NormalizeTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = new Polynomial(1, 5, 0, 0.8, 0);
            var result = value.Normalize();
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Integrate(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void IntegrateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = new Polynomial(0, 1, 6.25, 0, 2, 0, 0);
            var result = value.Integrate();
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Power(int)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void PowerTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = new Polynomial(0, 0, 25, 250, 625, 40, 200, 0, 16, 0, 0);
            var result = value.Power(2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Evaluate(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void EvaluateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = 288d;
            var result = value.Evaluate(2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Differentiate(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void DifferentiateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            var expected = 464d;
            var result = value.Differentiate(2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.MinMax(double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void MinMaxTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5);
            var expected = (5, 15);
            var result = value.MinMax();
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.GetStandardBase(int)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void GetStandardBaseTest()
        {
            var expected = new Polynomial[] { new Polynomial(1), new Polynomial(1, 0) };
            var result = Polynomial.GetStandardBase(2);
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Term(PolynomialDegree, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void TermTest()
        {
            var expected = new Polynomial(1, 0, 0, 0);
            var result = Polynomial.Term(PolynomialDegree.Cubic);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Interpolate(double[])"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void InterpolateTest()
        {
            var value = new double[] { 1, 2 };
            var expected = new Polynomial(1, 1);
            var result = Polynomial.Interpolate(value);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Monomial(PolynomialDegree)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void MonomialTest()
        {
            var expected = new Polynomial(1, 0, 0, 0);
            var result = Polynomial.Monomial(PolynomialDegree.Cubic);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Bezier(double[])"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void BezierTest()
        {
            var values = new double[] { 1, 2, 3, 4 };
            var expected = new Polynomial(0, 0, 3, 1);
            var result = Polynomial.Bezier(values);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Linear(double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void LinearTest()
        {
            var expected = new Polynomial(1, 1);
            var result = Polynomial.Linear(1, 2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Quadratic(double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void QuadraticTest()
        {
            var expected = new Polynomial(0, 2, 1);
            var result = Polynomial.Quadratic(1, 2, 3);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Cubic(double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void CubicTest()
        {
            var expected = new Polynomial(0, 0, 3, 1);
            var result = Polynomial.Cubic(1, 2, 3, 4);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Quartic(double, double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void QuarticTest()
        {
            var expected = new Polynomial(0, 0, 0, 4, 1);
            var result = Polynomial.Quartic(1, 2, 3, 4, 5);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Quintic(double, double, double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void QuinticTest()
        {
            var expected = new Polynomial(0, 0, 0, 0, 5, 1);
            var result = Polynomial.Quintic(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Sextic(double, double, double, double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void SexticTest()
        {
            var expected = new Polynomial(0, 0, 0, 0, 0, 6, 1);
            var result = Polynomial.Sextic(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Septic(double, double, double, double, double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void SepticTest()
        {
            var expected = new Polynomial(0, 0, 0, 0, 0, 0, 7, 1);
            var result = Polynomial.Septic(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Octic(double, double, double, double, double, double, double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void OcticTest()
        {
            var expected = new Polynomial(0, 0, 0, 0, 0, 0, 0, 8, 1);
            var result = Polynomial.Octic(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Bisection(double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void BisectionTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6, 7, 8, 9);
            double? expected = null;
            var result = value.Bisection(2, 4);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.RealOrComplexRoots(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void RealOrComplexRootsTest()
        {
            var value = new Polynomial(12, 9, 6, 3, 0);
            var expected = new double[] { -8.3266726846886741E-17, -0.605829586188268 };
            var result = value.RealOrComplexRoots().ToArray();

            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], TestEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.ComplexRoots(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void ComplexRootsTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            var expected = new Complex[] { new Complex(-313.995047760172, -277.124450638795), new Complex(-1.2106854781489, 1.79296866987468), new Complex(18.9284340792415, -5.02664569182785), new Complex(double.NaN, double.NaN), new Complex(6.63750764045928, 16.1927372642297), };
            var result = value.ComplexRoots();

            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                if (double.IsNaN(expected[i].Real))
                {
                    Assert.AreEqual(double.IsNaN(expected[i].Real), double.IsNaN(result[i].Real));
                    Assert.AreEqual(double.IsNaN(expected[i].Imaginary), double.IsNaN(result[i].Imaginary));
                }
                else
                {
                    Assert.AreEqual(expected[i].Real, result[i].Real, 100 * TestEpsilon);
                    Assert.AreEqual(expected[i].Imaginary, result[i].Imaginary, 100 * TestEpsilon);
                }
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.RootsInInterval(double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void RootsInIntervalTest()
        {
            var value = new Polynomial(12, 9, 6, 3, 0);
            var expected = new double[] { -0.605829820992767, -5.4831934903631918E-07, };
            var result = value.RootsInInterval(-1, 1);

            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], TestEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Roots(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void RootsTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            var expected = new double[] { -0.6723782435877943, -3.234022892850585, -0.046799431780810474, -0.046799431780810474, 0, 0 };
            var result = value.Roots();

            Assert.AreEqual(expected.Length, result.Length, TestEpsilon);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], TestEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.RealOrder"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "Polynomial")]
        [DeploymentItem("Engine.dll")]
        public void RealOrderTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            var expected = PolynomialDegree.Quintic;
            var result = value.RealOrder();
            Assert.AreEqual(expected, result);
        }
    }
}
