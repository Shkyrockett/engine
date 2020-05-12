using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicOperations
    {
        /// <summary>
        /// Finds the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of the Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(this Quaternion4D quaternion) => QuaternionMagnitude(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Finds the square distance of the length of a Quaternion.
        /// </summary>
        /// <param name="quaternion">The Quaternion.</param>
        /// <returns>Returns the length of a Quaternion.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LengthSquared(this Quaternion4D quaternion) => QuaternionNormal(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// The negate.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Negate(Quaternion4D quaternion)
            => new Quaternion4D(
                -quaternion.X,
                -quaternion.Y,
                -quaternion.Z,
                -quaternion.W);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(this Quaternion4D minuend, double subtrahend) => SubtractVectorUniform(minuend.X, minuend.Y, minuend.Z, minuend.W, subtrahend);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Subtract(this Quaternion4D minuend, Quaternion4D subtrahend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, subtrahend.X, subtrahend.Y, subtrahend.Z, subtrahend.W);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="Transform2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform2D Subtract(this Transform2D minuend, Transform2D subtrahend) => new Transform2D(minuend.X - subtrahend.X, minuend.Y - subtrahend.Y, NormalizeRadian(minuend.SkewX - subtrahend.SkewX), NormalizeRadian(minuend.SkewY - subtrahend.SkewY), minuend.ScaleX / subtrahend.ScaleX, minuend.ScaleY / subtrahend.ScaleY);

        /// <summary>
        /// The scale.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        public static Quaternion4D Scale(this Quaternion4D quaternion1, double scalar)
            => new Quaternion4D(
                quaternion1.X * scalar,
                quaternion1.Y * scalar,
                quaternion1.Z * scalar,
                quaternion1.W * scalar);

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Multiply(this Quaternion4D quaternion1, Quaternion4D quaternion2)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = (y * num2) - (z * num3);
            var num11 = (z * num4) - (x * num2);
            var num10 = (x * num3) - (y * num4);
            var num9 = (x * num4) + (y * num3) + (z * num2);
            return new Quaternion4D(
                (x * num) + (num4 * w) + num12,
                (y * num) + (num3 * w) + num11,
                (z * num) + (num2 * w) + num10,
                (w * num) - num9);
        }

        /// <summary>
        /// Multiply: Point * Matrix
        /// </summary>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Multiply(this Point2D point, Matrix3x2D matrix)
            => matrix.Transform(point);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Divide(this Quaternion4D quaternion1, Quaternion4D quaternion2)
        {
            var num14 = (quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y) + (quaternion2.Z * quaternion2.Z) + (quaternion2.W * quaternion2.W);
            var num5 = 1f / num14;
            var num4 = -quaternion2.X * num5;
            var num3 = -quaternion2.Y * num5;
            var num2 = -quaternion2.Z * num5;
            var num = quaternion2.W * num5;
            var num13 = (quaternion1.Y * num2) - (quaternion1.Z * num3);
            var num12 = (quaternion1.Z * num4) - (quaternion1.X * num2);
            var num11 = (quaternion1.X * num3) - (quaternion1.Y * num4);
            var num10 = (quaternion1.X * num4) + (quaternion1.Y * num3) + (quaternion1.Z * num2);
            return new Quaternion4D(
                (quaternion1.X * num) + (num4 * quaternion1.W) + num13,
                (quaternion1.Y * num) + (num3 * quaternion1.W) + num12,
                (quaternion1.Z * num) + (num2 * quaternion1.W) + num11,
                (quaternion1.W * num) - num10);
        }

        /// <summary>
        /// The dot product.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(this Quaternion4D quaternion1, Quaternion4D quaternion2)
            => (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);

        #region Rotate Around Point
        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.
        /// </summary>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <param name="center">The point around which to rotate.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2D RotateAroundPoint(this Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix3x2D();

            // We need to go counter-clockwise.
            result.RotateAt((float)-angle.RadiansToDegrees(), center.X, center.Y);

            return result;
        }
        #endregion Rotate Around Point

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Invert(Quaternion4D quaternion)
        {
            var normal = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            if (normal == 0d)
            {
                return Quaternion4D.Empty;
            }

            var inverseNormal = 1f / normal;
            return new Quaternion4D(
                -quaternion.X * inverseNormal,
                -quaternion.Y * inverseNormal,
                -quaternion.Z * inverseNormal,
                quaternion.W * inverseNormal);
        }

        /// <summary>
        /// The normalize.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Normalize(this Quaternion4D quaternion)
        {
            var num2 = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            var num = 1f / Sqrt(num2);
            return new Quaternion4D(
                quaternion.X * num,
                quaternion.Y * num,
                quaternion.Z * num,
                quaternion.W * num);
        }

        #region Exponent
        /// <summary>
        /// Calculates the Exponent of a Quaternion.
        /// </summary>
        /// <param name="source">ToDo: describe source parameter on Exponent</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Exponent(Quaternion4D source)
        {
            // If q = A*(X*i+Y*j+Z*k) Where (X,Y,Z) is unit length, then
            // eXp(q) = cos(A)+sin(A)*(X*i+Y*j+Z*k).  If sin(A) is near Zero,
            // use eXp(q) = cos(A)+A*(X*i+Y*j+Z*k) since A/sin(A) has limit 1.

            var angle = Sqrt((source.X * source.X) + (source.Y * source.Y) + (source.Z * source.Z));
            var sin = Sin(angle);

            // start off With a Zero Quaternion
            var returnvalue = Quaternion4D.Empty;

            returnvalue.W = Cos(angle);

            if (Abs(sin) >= double.Epsilon)
            {
                var coeff = sin / angle;

                returnvalue.X = coeff * source.X;
                returnvalue.Y = coeff * source.Y;
                returnvalue.Z = coeff * source.Z;
            }
            else
            {
                returnvalue.X = source.X;
                returnvalue.Y = source.Y;
                returnvalue.Z = source.Z;
            }

            return returnvalue;
        }
        #endregion Exponent

        #region Concatenate
        /// <summary>
        /// The concatenate.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Concatenate(this Quaternion4D a, Quaternion4D b)
            => new Quaternion4D(
                (b.X * a.W) + (a.X * b.W) + ((b.Y * a.Z) - (b.Z * a.Y)),
                (b.Y * a.W) + (a.Y * b.W) + ((b.Z * a.X) - (b.X * a.Z)),
                (b.Z * a.W) + (a.Z * b.W) + ((b.X * a.Y) - (b.Y * a.X)),
                (b.W * a.W) - ((b.X * a.X) + (b.Y * a.Y) + (b.Z * a.Z)));
        #endregion Concatenate

        #region Conjugate
        /// <summary>
        /// The conjugate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Conjugate(this Quaternion4D value)
            => new Quaternion4D(-value.X, -value.Y, -value.Z, value.W);
        #endregion Conjugate

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Lerp(this Quaternion4D quaternion1, Quaternion4D quaternion2, double amount)
        {
            var num = amount;
            var num2 = 1f - num;
            var quaternion = new Quaternion4D();
            var num5 = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);
            if (num5 >= 0f)
            {
                quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
            }
            else
            {
                quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
                quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
                quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
                quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
            }
            var num4 = (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W);
            var num3 = 1f / Sqrt(num4);
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }

        #region Slerp
        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="quaternion1">The quaternion1.</param>
        /// <param name="quaternion2">The quaternion2.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The <see cref="Quaternion4D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Slerp(this Quaternion4D quaternion1, Quaternion4D quaternion2, double amount)
        {
            double num2;
            double num3;
            var num = amount;
            var num4 = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y) + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);
            var flag = false;
            if (num4 < 0d)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999d)
            {
                num3 = 1d - num;
                num2 = flag ? -num : num;
            }
            else
            {
                var num5 = Acos(num4);
                var num6 = 1d / Sin(num5);
                num3 = Sin((1d - num) * num5) * num6;
                num2 = flag ? -Sin(num * num5) * num6 : Sin(num * num5) * num6;
            }
            return new Quaternion4D(
                (num3 * quaternion1.X) + (num2 * quaternion2.X),
                (num3 * quaternion1.Y) + (num2 * quaternion2.Y),
                (num3 * quaternion1.Z) + (num2 * quaternion2.Z),
                (num3 * quaternion1.W) + (num2 * quaternion2.W));
        }
        #endregion Slerp

        #region Log
        /// <summary>
        /// Calculates the logarithm of a Quaternion.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion4D Log(this Quaternion4D source)
        {
            // BLACKBOX: Learn this
            // If q = cos(A)+sin(A)*(X*i+Y*j+Z*k) Where (X,Y,Z) is unit length, then
            // log(q) = A*(X*i+Y*j+Z*k).  If sin(A) is near Zero, use log(q) =
            // sin(A)*(X*i+Y*j+Z*k) since sin(A)/A has limit 1.

            // start off With a Zero Quaternion
            var returnvalue = Quaternion4D.Empty;

            if (Abs(source.W) < 1d)
            {
                var angle = Acos(source.W);
                var sin = Sin(angle);

                if (Abs(sin) >= double.Epsilon)
                {
                    var coeff = angle / sin;
                    returnvalue.X = coeff * source.X;
                    returnvalue.Y = coeff * source.Y;
                    returnvalue.Z = coeff * source.Z;
                }
                else
                {
                    returnvalue.X = source.X;
                    returnvalue.Y = source.Y;
                    returnvalue.Z = source.Z;
                }
            }

            return returnvalue;
        }
        #endregion Log

        /// <summary>
        /// Gets a 3x3 rotation matrix from this Quaternion.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x3D ToRotationMatrix(this Quaternion4D quaternion) => QuaternionToRotationMatrix(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// Gets a 4x4 matrix from this Quaternion.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>source -&gt; http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Quaternions_to_represent_rotation#Quaternion_to_Matrix</para>
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4D ToMatrix(this Quaternion4D quaternion) => QuaternionToMatrix(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// The to axis.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3D XAxis, Vector3D YAxis, Vector3D ZAxis) ToAxis(this Quaternion4D quaternion)
        {
            var (XAxisX, XAxisY, XAxisZ, YAxisX, YAxisY, YAxisZ, ZAxisX, ZAxisY, ZAxisZ) = QuaternionToAxis(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
            return ((XAxisX, XAxisY, XAxisZ), (YAxisX, YAxisY, YAxisZ), (ZAxisX, ZAxisY, ZAxisZ));
        }

        /// <summary>
        /// The quaternion representing the rotation is
        /// q = cos(A/2)+sin(A/2)*(X*i+Y*j+Z*k)
        /// </summary>
        /// <param name="quaternion"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Angle, Vector3D Axis) ToAngleAxis(this Quaternion4D quaternion)
        {
            (var Angle, var X, var Y, var Z) = QuaternionToAngleAxis(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
            return (Angle, (X, Y, Z));
        }

        /// <summary>
        /// The to Euler angles.
        /// </summary>
        /// <param name="quaternion">The quaternion.</param>
        /// <returns>The <see cref="Orientation3D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Orientation3D ToEulerAngles(this Quaternion4D quaternion) => QuaternionToEulerAngles(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }
}
