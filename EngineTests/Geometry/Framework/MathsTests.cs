using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Engine.Geometry.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class MathsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void OffsetSegmentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AngleTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AngleVectorTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AbsoluteAngleTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AngleBetweenTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CrossProductTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CrossProduct3PointTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void DotProductTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void DotProduct3PointTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void MixedProductTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DistanceTest()
        {
            double value = Maths.Distance(1, 1, 2, 1);
            Assert.AreEqual(1, value, Maths.DoubleEpsilon, "Distance between two points.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SquareDistanceTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SquareDistanceToLineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SlopeTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AbsTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void MagnitudeTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ModulusTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void UnitizeTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void NormalizeTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void PerpendicularClockwiseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void PerpendicularCounterClockwiseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
        [Ignore]
        public void ProjectionTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RejectionTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ReflectionTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RotateXTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void PitchTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RotateYTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void YawTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RotateZTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RollTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IsBackFaceTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IsUnitVectorTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InterpolateTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AverageTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SumTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RootTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CrtTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Atan2Test()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void SecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicTangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void HyperbolicCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicSineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCosineTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicTangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicSecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCosecantTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void InverseHyperbolicCotangentTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void LogarithmTobaseNTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ModuloTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ToRadiansTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ToDegreesTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RoundToIntTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void RoundToMultipleTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ToFloatTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ToFloatTest1()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void ToDoubleTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void AreCloseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void LessThanTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GreaterThanTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void LessThanOrCloseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void GreaterThanOrCloseTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void NearZeroTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IsZeroTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IsOneTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IsBetweenZeroAndOneTest()
        {
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
            throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
        }
    }
}