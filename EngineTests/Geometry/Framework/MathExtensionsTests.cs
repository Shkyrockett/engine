using Engine.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// This test class for the <see cref="MathExtensions"/> class, is intended to 
    /// contain all of the Unit tests for the <see cref="MathExtensions"/> class.
    /// </summary>
    [TestClass()]
    public class MathExtensionsTests
    {
        /// <summary>
        /// 
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathExtensionsTests"/> class.
        /// </summary>
        public MathExtensionsTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            //MessageBox.Show("ClassInit " + context.TestName);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            //MessageBox.Show("TestMethodInit");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            //MessageBox.Show("TestMethodCleanup");
        }

        /// <summary>
        /// 
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //MessageBox.Show("ClassCleanup");
        }

        /// <summary>
        /// A Test for converting Radians to Degrees.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ToRadiansTest()
        {
            double value = 0;
            value = MathExtensions.ToRadians(0);
            Assert.AreEqual(0, value);
            value = MathExtensions.ToRadians(30);
            Assert.AreEqual(30 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(45);
            Assert.AreEqual(45 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(60);
            Assert.AreEqual(60 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(90);
            Assert.AreEqual(90 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(120);
            Assert.AreEqual(120 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(135);
            Assert.AreEqual(135 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(150);
            Assert.AreEqual(150 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(180);
            Assert.AreEqual(180 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(210);
            Assert.AreEqual(210 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(225);
            Assert.AreEqual(225 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(240);
            Assert.AreEqual(240 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(270);
            Assert.AreEqual(270 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(300);
            Assert.AreEqual(300 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(315);
            Assert.AreEqual(315 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(330);
            Assert.AreEqual(330 * (Math.PI / 180f), value);
            value = MathExtensions.ToRadians(360);
            Assert.AreEqual(360 * (Math.PI / 180f), value);
        }

        /// <summary>
        /// A Test for converting Degrees to Radians.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ToDegreesTest()
        {
            double value = 0;
            value = MathExtensions.ToDegrees(0);
            Assert.AreEqual(0, value);
            value = MathExtensions.ToDegrees(30);
            Assert.AreEqual(30 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(45);
            Assert.AreEqual(45 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(60);
            Assert.AreEqual(60 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(90);
            Assert.AreEqual(90 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(120);
            Assert.AreEqual(120 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(135);
            Assert.AreEqual(135 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(150);
            Assert.AreEqual(150 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(180);
            Assert.AreEqual(180 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(210);
            Assert.AreEqual(210 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(225);
            Assert.AreEqual(225 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(240);
            Assert.AreEqual(240 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(270);
            Assert.AreEqual(270 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(300);
            Assert.AreEqual(300 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(315);
            Assert.AreEqual(315 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(330);
            Assert.AreEqual(330 * (180f / Math.PI), value);
            value = MathExtensions.ToDegrees(360);
            Assert.AreEqual(360 * (180f / Math.PI), value);
        }

        /// <summary>
        /// A Test for rounding a number to an arbitrary value.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void RoundToMultipleTest()
        {
            double value = 0;
            value = MathExtensions.RoundToMultiple(3.5, 1.5);
            Assert.AreEqual(3, value);
        }

        /// <summary>
        /// A Test for retrieving the modulo of an arbitrary value, the same way Excel does.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void ModuloTest()
        {
            double value = 0;
            value = MathExtensions.Modulo(3.5, 1.5);
            Assert.AreEqual(0.5d, value);
        }

        /// <summary>
        /// A Test for finding the average value from a list of numbers.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void AverageTest()
        {
            double value = 0;
            double[] array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = MathExtensions.Average(array);
            Assert.AreEqual(4.5, value);
        }

        /// <summary>
        /// A Test for finding the sum value from a list of numbers.
        /// </summary>
        [TestMethod()]
        [Priority(0)]
        [Owner("Shkyrockett")]
        [TestProperty("Engine", "MathExtensions")]
        public void SumTest()
        {
            double value = 0;
            double[] array = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            value = MathExtensions.Sum(array);
            Assert.AreEqual(45, value);
        }

        [TestMethod()]
        [Ignore]
        public void RandomTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void Atan2Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void _Atan2Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void SecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void CosecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void CotangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseSineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseCosineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseSecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseCosecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseCotangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicSineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicCosineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicTangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicSecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicCosecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HyperbolicCotangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicSineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCosineTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicTangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicSecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCosecantTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCotangentTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void LogarithmTobaseNTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void ToFloatTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void LessThanTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void GreaterThanTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void LessThanOrCloseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void GreaterThanOrCloseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void IsOneTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void IsZeroTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void IsBetweenZeroAndOneTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void FloatToIntTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void DoubleToIntTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void HIWORDTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [Ignore]
        public void LOWORDTest()
        {
            throw new NotImplementedException();
        }
    }
}