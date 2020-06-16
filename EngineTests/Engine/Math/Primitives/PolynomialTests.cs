// <copyright file="PolynomialTests.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

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
        private const double testEpsilon = 0.0000000000001d;
        #endregion Constants

        #region Properties
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        #endregion Properties

        #region Class Housekeeping
        /// <summary>
        /// ClassInitialize runs code before running the first test in the class.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context) => _ = context;//MessageBox.Show("ClassInit " + context.TestName);

        /// <summary>
        /// TestInitialize runs code before running each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// TestCleanup runs code after each test has run.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// ClassCleanup runs code after all tests in a class have run.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }
        #endregion Class Housekeeping

        /// <summary>
        /// Test the <see cref="Polynomial.Trim"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
                Assert.AreEqual(test.Value.Count, test.Key.Trim().Count);
                Assert.AreEqual(test.Value, test.Key.Trim());
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Derivate"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void EvaluateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            const double expected = 288d;
            var result = value.Evaluate(2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Differentiate(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void DifferentiateTest()
        {
            var value = new Polynomial(0, 5, 25, 0, 4, 0);
            const double expected = 464d;
            var result = value.Differentiate(2);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.MinMax(double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void BezierTest()
        {
            var values = new double[] { 1, 2, 3, 4 };
            var expected = new Polynomial(0, 0, 3, 1);
            var result = Polynomials.Bezier(values);
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Bisection(double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
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
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void RealOrComplexRootsTest()
        {
            var value = new Polynomial(12, 9, 6, 3, 0);
            var expected = new double[] { -8.3266726846886741E-17, -0.605829586188268 };
            var result = value.RealOrComplexRoots().ToArray();

            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], testEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.ComplexRoots(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void ComplexRootsTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            var expected = new Complex[] { new Complex(-0.805786469389031, -1.22290471337441), new Complex(0.551685463458982, -1.25334886027721), new Complex(0.551685463458982, 1.25334886027721), new Complex(-0.805786469389031, 1.22290471337441), new Complex(-1.4917979881399, -1.42626051663959E-26) };
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
                    Assert.AreEqual(expected[i].Real, result[i].Real, 100 * testEpsilon);
                    Assert.AreEqual(expected[i].Imaginary, result[i].Imaginary, 100 * testEpsilon);
                }
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.RootsInInterval(double, double, double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void RootsInIntervalTest()
        {
            var value = new Polynomial(12, 9, 6, 3, 0);
            var expected = new double[] { -0.605829820992767, -5.4831934903631918E-07, };
            var result = value.RootsInInterval(-1, 1);

            Assert.AreEqual(expected.Length, result.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], testEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.Roots(double)"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void RootsTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            var expected = new double[] { -0.6723782435877943, -3.234022892850585, -0.046799431780810474, -0.046799431780810474, 0, 0 };
            var result = value.Roots();

            Assert.AreEqual(expected.Length, result.Length, testEpsilon);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], testEpsilon);
            }
        }

        /// <summary>
        /// Test the <see cref="Polynomial.RealOrder"/> method.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty(nameof(Engine), nameof(Polynomial))]
        [DeploymentItem("Engine.dll")]
        public void RealOrderTest()
        {
            var value = new Polynomial(1, 2, 3, 4, 5, 6);
            const PolynomialDegree expected = PolynomialDegree.Quintic;
            var result = value.RealOrder();
            Assert.AreEqual(expected, result);
        }
    }
}
