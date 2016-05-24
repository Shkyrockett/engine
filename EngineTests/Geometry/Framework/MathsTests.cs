using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class MathsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void OffsetSegmentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AngleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AngleVectorTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AbsoluteAngleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AngleBetweenTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CrossProductTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CrossProduct3PointTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void DotProductTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void DotProduct3PointTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void MixedProductTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void DistanceTest()
        {
            double value = Maths.Distance(1, 1, 2, 1);
            Assert.AreEqual(1, value, Maths.DoubleEpsilon, "Distance between two points.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SquareDistanceTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SquareDistanceToLineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SlopeTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AbsTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void MagnitudeTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ModulusTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void UnitizeTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void NormalizeTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerpendicularClockwiseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PerpendicularCounterClockwiseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void DeterminantTest()
        {
            double value = Maths.Determinant(1, 2, 2, 1);
            Assert.AreEqual(-3d, value, Maths.DoubleEpsilon, "Determinant of a 2x2 matrix.");

            value = Maths.Determinant(1, 2, 3, 2, 1, 3, 3, 2, 1);
            Assert.AreEqual(12d, value, Maths.DoubleEpsilon, "Determinant of a 3x3 matrix.");

            value = Maths.Determinant(1, 2, 3, 4, 2, 1, 3, 4, 3, 2, 1, 4, 4, 3, 2, 1);
            Assert.AreEqual(-60d, value, Maths.DoubleEpsilon, "Determinant of a 4x4 matrix.");

            value = Maths.Determinant(1, 2, 3, 4, 5, 2, 1, 3, 4, 5, 3, 2, 1, 4, 5, 4, 3, 2, 1, 5, 5, 4, 3, 2, 1);
            Assert.AreEqual(360d, value, Maths.DoubleEpsilon, "Determinant of a 5x5 matrix.");

            value = Maths.Determinant(1, 2, 3, 4, 5, 6, 2, 1, 3, 4, 5, 6, 3, 2, 1, 4, 5, 6, 4, 3, 2, 1, 5, 6, 5, 4, 3, 2, 1, 6, 6, 5, 4, 3, 2, 1);
            Assert.AreEqual(-2520d, value, Maths.DoubleEpsilon, "Determinant of a 6x6 matrix.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void InverseDeterminantTest()
        {
            double value = Maths.InverseDeterminant(1, 2, 2, 1);
            Assert.AreEqual(1d / -3d, value, Maths.DoubleEpsilon, "Determinant of a 2x2 matrix.");

            value = Maths.InverseDeterminant(1, 2, 3, 2, 1, 3, 3, 2, 1);
            Assert.AreEqual(1d / 12d, value, Maths.DoubleEpsilon, "Determinant of a 3x3 matrix.");

            value = Maths.InverseDeterminant(1, 2, 3, 4, 2, 1, 3, 4, 3, 2, 1, 4, 4, 3, 2, 1);
            Assert.AreEqual(1d / -60d, value, Maths.DoubleEpsilon, "Determinant of a 4x4 matrix.");

            value = Maths.InverseDeterminant(1, 2, 3, 4, 5, 2, 1, 3, 4, 5, 3, 2, 1, 4, 5, 4, 3, 2, 1, 5, 5, 4, 3, 2, 1);
            Assert.AreEqual(1d / 360d, value, Maths.DoubleEpsilon, "Determinant of a 5x5 matrix.");

            value = Maths.InverseDeterminant(1, 2, 3, 4, 5, 6, 2, 1, 3, 4, 5, 6, 3, 2, 1, 4, 5, 6, 4, 3, 2, 1, 5, 6, 5, 4, 3, 2, 1, 6, 6, 5, 4, 3, 2, 1);
            Assert.AreEqual(1d / -2520d, value, Maths.DoubleEpsilon, "Determinant of a 6x6 matrix.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ProjectionTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RejectionTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ReflectionTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RotateXTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void PitchTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RotateYTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void YawTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RotateZTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RollTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void IsBackFaceTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void IsUnitVectorTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InterpolateTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AverageTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SumTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RootTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CrtTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void Atan2Test()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void SecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CosecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void CotangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseSineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseCosineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseSecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseCosecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseCotangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicSineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicCosineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicTangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicSecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicCosecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void HyperbolicCotangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicSineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCosineTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicTangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicSecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCosecantTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void InverseHyperbolicCotangentTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void LogarithmTobaseNTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ModuloTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ToRadiansTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ToDegreesTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RoundToIntTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void RoundToMultipleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ToFloatTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ToFloatTest1()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void ToDoubleTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void AreCloseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void LessThanTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void GreaterThanTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void LessThanOrCloseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void GreaterThanOrCloseTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void NearZeroTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void IsZeroTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void IsOneTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        [Ignore]
        public void IsBetweenZeroAndOneTest()
        {
            throw new NotImplementedException();
        }
    }
}