// <copyright file="Operations.Vectors.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Some standard shared operations to perform on Points, Vectors, and Sizes.
    /// </summary>
    public static partial class Operations
    {
        #region Is Unit Vector
        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1)
            => Math.Abs(Magnitude(i1, j1) - 1) < double.Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1, double k1)
            => Math.Abs(Magnitude(i1, j1, k1) - 1) < double.Epsilon;
        #endregion

        #region Unary Add
        /// <summary>
        /// Unaries the add2 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) UnaryAdd2D(double a, double b) => (+a, +b);

        /// <summary>
        /// Unaries the add3 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) UnaryAdd3D(double a, double b, double c) => (+a, +b, +c);

        /// <summary>
        /// Unaries the add4 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) UnaryAdd4D(double a, double b, double c, double d) => (+a, +b, +c, +d);
        #endregion Unary Add

        #region Unary Negate
        /// <summary>
        /// Unaries the negate2 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) UnaryNegate2D(double a, double b) => (-a, -b);

        /// <summary>
        /// Unaries the negate3 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) UnaryNegate3D(double a, double b, double c) => (-a, -b, -c);

        /// <summary>
        /// Unaries the negate4 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) UnaryNegate4D(double a, double b, double c, double d) => (-a, -b, -c, -d);
        #endregion Unary Negate

        #region Add Value To Vector
        /// <summary>
        /// Add2s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Add2D(double a1, double b1, double addend) => (a1 + addend, b1 + addend);

        /// <summary>
        /// Add3s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Add3D(double a1, double b1, double c1, double addend) => (a1 + addend, b1 + addend, c1 + addend);

        /// <summary>
        /// Add4s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Add4D(double a1, double b1, double c1, double d1, double addend) => (a1 + addend, b1 + addend, c1 + addend, d1 + addend);
        #endregion Add Value To Vector

        #region Add Two Vectors
        /// <summary>
        /// Add2s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Add2D(double a1, double b1, double a2, double b2) => (a1 + a2, b1 + b2);

        /// <summary>
        /// Add3s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Add3D(double a1, double b1, double c1, double a2, double b2, double c2) => (a1 + a2, b1 + b2, c1 + c2);

        /// <summary>
        /// Add4s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Add4D(double a1, double b1, double c1, double d1, double a2, double b2, double c2, double d2) => (a1 + a2, b1 + b2, c1 + c2, d1 + d2);
        #endregion Add Two Vectors

        #region Subtract Value From Vector
        /// <summary>
        /// Subtracts the subtrahend2 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) SubtractSubtrahend2D(double a1, double b1, double subend) => (a1 - subend, b1 - subend);

        /// <summary>
        /// Subtracts the subtrahend3 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) SubtractSubtrahend3D(double a1, double b1, double c1, double subend) => (a1 - subend, b1 - subend, c1 - subend);

        /// <summary>
        /// Subtracts the subtrahend4 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) SubtractSubtrahend4D(double a1, double b1, double c1, double d1, double subend) => (a1 - subend, b1 - subend, c1 - subend, d1 - subend);
        #endregion Subtract Value From Vector

        #region Subtract Vector From Value
        /// <summary>
        /// Subtracts from minuend2 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) SubtractFromMinuend2D(double minuend, double a1, double b1) => (minuend - a1, minuend - b1);

        /// <summary>
        /// Subtracts from minuend3 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) SubtractFromMinuend3D(double minuend, double a1, double b1, double c1) => (minuend - a1, minuend - b1, minuend - c1);

        /// <summary>
        /// Subtracts from minuend4 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) SubtractFromMinuend4D(double minuend, double a1, double b1, double c1, double d1) => (minuend - a1, minuend - b1, minuend - c1, minuend - d1);
        #endregion Subtract Vector From Value

        #region Subtract Two Vectors
        /// <summary>
        /// Subtract2s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Subtract2D(double a1, double b1, double a2, double b2) => (a1 - a2, b1 - b2);

        /// <summary>
        /// Subtract3s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Subtract3D(double a1, double b1, double c1, double a2, double b2, double c2) => (a1 - a2, b1 - b2, c1 - c2);

        /// <summary>
        /// Subtract4s the d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Subtract4D(double a1, double b1, double c1, double d1, double a2, double b2, double c2, double d2) => (a1 - a2, b1 - b2, c1 - c2, d1 - d2);
        #endregion Subtract Two Vectors

        #region Difference Between Two Vectors
        /// <summary>
        /// Finds the Delta of two 2D Vectors.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Delta(double i1, double j1, double i2, double j2) => Subtract2D(i2, j2, i1, j1);

        /// <summary>
        /// Finds the Delta of two 3D Vectors.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="k2">The k2.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Delta(double i1, double j1, double k1, double i2, double j2, double k2) => Subtract3D(i2, j2, k2, i1, j1, k1);

        /// <summary>
        /// Finds the Delta of two 3D Vectors.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <param name="l1">The l1.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="k2">The k2.</param>
        /// <param name="l2">The l2.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Delta(double i1, double j1, double k1, double l1, double i2, double j2, double k2, double l2) => Subtract4D(i2, j2, k2, l2, i1, j1, k1, l1);
        #endregion Difference Between Two Vectors

        #region Multiply A Vector by a Value
        /// <summary>
        /// Scale2s the d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Scale2D(double a, double b, double scalar) => (a * scalar, b * scalar);

        /// <summary>
        /// Scale3s the d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Scale3D(double a, double b, double c, double scalar) => (a * scalar, b * scalar, c * scalar);

        /// <summary>
        /// Scale4s the d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Scale4D(double a, double b, double c, double d, double scalar) => (a * scalar, b * scalar, c * scalar, d * scalar);
        #endregion Multiply A Vector by a Value

        #region Multiply Each Vector Component By The One in Another Vector
        /// <summary>
        /// Parametrics the scale2 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) ParametricScale2D(double a1, double b1, double a2, double b2) => (a1 * a2, b1 * b2);

        /// <summary>
        /// Parametrics the scale3 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) ParametricScale3D(double a1, double b1, double c1, double a2, double b2, double c2) => (a1 * a2, b1 * b2, c1 * c2);

        /// <summary>
        /// Parametrics the scale4 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) ParametricScale4D(double a1, double b1, double c1, double d1, double a2, double b2, double c2, double d2) => (a1 * a2, b1 * b2, c1 * c2, d1 * d2);
        #endregion Multiply Each Vector Component By The One in Another Vector

        #region Divide Vector By Value
        /// <summary>
        /// Divides the by dividend2 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DivideByDividend2D(double a, double b, double dividend) => (a / dividend, b / dividend);

        /// <summary>
        /// Divides the by dividend3 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DivideByDividend3D(double a, double b, double c, double dividend) => (a / dividend, b / dividend, c / dividend);

        /// <summary>
        /// Divides the by dividend4 d.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DivideByDividend4D(double a, double b, double c, double d, double dividend) => (a / dividend, b / dividend, c / dividend, d / dividend);
        #endregion Divide Vector By Value

        #region Divide Value into Vector Components
        /// <summary>
        /// Divides the divisor2 d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DivideDivisor2D(double divisor, double a, double b) => (divisor / a, divisor / b);

        /// <summary>
        /// Divides the divisor3 d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DivideDivisor3D(double divisor, double a, double b, double c) => (divisor / a, divisor / b, divisor / c);

        /// <summary>
        /// Divides the divisor4 d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DivideDivisor4D(double divisor, double a, double b, double c, double d) => (divisor / a, divisor / b, divisor / c, divisor / d);
        #endregion Divide Value into Vector Components

        #region Divide Each Of The Components Of A Vector By The Same Components Of Another Vector
        /// <summary>
        /// Parametrics the divide2 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) ParametricDivide2D(double a1, double b1, double a2, double b2) => (a1 / a2, b1 / b2);

        /// <summary>
        /// Parametrics the divide3 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) ParametricDivide3D(double a1, double b1, double c1, double a2, double b2, double c2) => (a1 / a2, b1 / b2, c1 / c2);

        /// <summary>
        /// Parametrics the divide4 d.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="b1">The b1.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="d1">The d1.</param>
        /// <param name="a2">The a2.</param>
        /// <param name="b2">The b2.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="d2">The d2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) ParametricDivide4D(double a1, double b1, double c1, double d1, double a2, double b2, double c2, double d2) => (a1 / a2, b1 / b2, c1 / c2, d1 / d2);
        #endregion Divide Each Of The Components Of A Vector By The Same Components Of Another Vector

        #region Modulus Magnitude
        /// <summary>
        /// The Magnitude of a two dimensional Vector.
        /// </summary>
        /// <param name="i">The i component of the vector.</param>
        /// <param name="j">The j component of the vector.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j) => Sqrt((i * i) + (j * j));

        /// <summary>
        /// The Magnitude of a three dimensional Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k) => Sqrt((i * i) + (j * j) + (k * k));
        #endregion Modulus Magnitude

        #region Invert
        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InvertVector(double x, double y) => (1d / x, 1d / y);

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) InvertVector(double x, double y, double z) => (1d / x, 1d / y, 1d / z);

        /// <summary>
        /// Inverts a Vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) InvertVector(double x, double y, double z, double w) => (1d / x, 1d / y, 1d / z, 1d / w);
        #endregion Invert

        #region Linear Interpolate
        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="u0">The u0.</param>
        /// <param name="u1">The u1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(double u0, double u1, double t) => u0 + ((u1 - u0) * t);

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Lerp(
            double x0, double y0,
            double x1, double y1,
            double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t));

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Lerp(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t));

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="w0">The w0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Lerp(
            double x0, double y0, double z0, double w0,
            double x1, double y1, double z1, double w1,
            double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t));
        #endregion Linear Interpolate

        #region Dot Product
        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks><para>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1,
            double x2, double y2) => (x1 * x2) + (y1 * y2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2) => (x1 * x2) + (y1 * y2) + (z1 * z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple,
            double x2, double y2, double z2) => DotProduct(tuple.X, tuple.Y, tuple.Z, x2, y2, z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple1,
            (double X, double Y, double Z) tuple2)
            => DotProduct(
                tuple1.X, tuple1.Y, tuple1.Z,
                tuple2.X, tuple2.Y, tuple2.Z
                );

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="w1">First Point W component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <param name="w2">Second Point W component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1, double z1, double w1,
            double x2, double y2, double z2, double w2) => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="w1">First Point W component.</param>
        /// <param name="v1">First Point V component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <param name="w2">Second Point W component.</param>
        /// <param name="v2">Second Point V component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1, double z1, double w1, double v1,
            double x2, double y2, double z2, double w2, double v2) => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2) + (v1 * v2);
        #endregion Dot Product

        #region Dot Product Vector
        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>Return the dot product AB · BC.</returns>
        /// <remarks><para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2));
        #endregion Dot Product Vector

        #region Cross Product
        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks><para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(
            double x1, double y1,
            double x2, double y2) => (x1 * y2) - (y1 * x2);

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks><para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(
            (double x, double y) t1,
            (double x, double y) t2) => (t1.x * t2.y) - (t1.y * t2.x);

        /// <summary>
        /// The cross product.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                (y1 * z2) - (z1 * y2), // X
                (z1 * x2) - (x1 * z2), // Y
                (x1 * y2) - (y1 * x2)  // Z
                );

        /// <summary>
        /// The cross product.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct(
            (double x, double y, double z) t1,
            (double x, double y, double z) t2)
            => (
                (t1.y * t2.z) - (t1.z * t2.y), // X
                (t1.z * t2.x) - (t1.x * t2.z), // Y
                (t1.x * t2.y) - (t1.y * t2.x)  // Z
                );

        /// <summary>
        /// Cross4 computes the four-dimensional cross product of the three vectors U, V and W, in that order.  It returns the resulting four-vector.
        /// https://web.archive.org/web/20040213224251/http://research.microsoft.com/~hollasch/thesis/chapter2.html
        /// </summary>
        /// <param name="uI"></param>
        /// <param name="uJ"></param>
        /// <param name="uK"></param>
        /// <param name="uL"></param>
        /// <param name="vI"></param>
        /// <param name="vJ"></param>
        /// <param name="vK"></param>
        /// <param name="vL"></param>
        /// <param name="wI"></param>
        /// <param name="wJ"></param>
        /// <param name="wK"></param>
        /// <param name="wL"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W)
            CrossProductVector(
            double uI, double uJ, double uK, double uL,
            double vI, double vJ, double vK, double vL,
            double wI, double wJ, double wK, double wL)
        {
            // Calculate intermediate values.
            var a = (vI * wJ) - (vJ * wI);
            var b = (vI * wK) - (vK * wI);
            var c = (vI * wL) - (vL * wI);
            var d = (vJ * wK) - (vK * wJ);
            var e = (vJ * wL) - (vL * wJ);
            var f = (vK * wL) - (vL * wK);

            // Calculate the result-vector components.
            return (
                (uJ * f) - (uK * e) + (uL * d),
                -(uI * f) + (uK * c) - (uL * b),
                (uI * e) - (uJ * c) + (uL * a),
                -(uI * d) + (uJ * b) - (uK * a)
             );
        }
        #endregion Cross Product

        #region Complex Product
        /// <summary>
        /// The complex product.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) ComplexProduct(
            double x0, double y0,
            double x1, double y1)
            => (
                (x0 * x1) - (y0 * y1),
                (x0 * y1) + (y0 * x1)
                );
        #endregion Complex Product

        #region Mixed Product
        /// <summary>
        /// The mixed product.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MixedProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3) => DotProduct(CrossProduct(x1, y1, z1, x2, y2, z2), x3, y3, z3);
        #endregion Mixed Product

        #region Vector Cross Product
        /// <summary>
        /// The cross product vector.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>Return the cross product AB x BC. The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3) => ((x1 - x2) * (y3 - y2)) - ((y1 - y2) * (x3 - x2));
        #endregion Vector Cross Product

        #region Unit Normalize
        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize1D(double i) => i / Sqrt(i * i);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize2D(
            double i, double j)
            => (i / Sqrt((i * i) + (j * j)),
                j / Sqrt((i * i) + (j * j)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize2D(
            (double i, double j) tuple)
            => (tuple.i / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j)),
                tuple.j / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j)));

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize2D(
            double i1, double j1,
            double i2, double j2)
            => (
                i1 / Sqrt((i1 * i2) + (j1 * j2)),
                j1 / Sqrt((i1 * i2) + (j1 * j2))
                );

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize3D(
            (double i, double j, double k) tuple)
            => (tuple.i / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k)),
                tuple.j / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k)),
                tuple.k / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize3D(
            double i, double j, double k)
            => (i / Sqrt((i * i) + (j * j) + (k * k)),
                j / Sqrt((i * i) + (j * j) + (k * k)),
                k / Sqrt((i * i) + (j * j) + (k * k)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the second Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize3D(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
            => (
                i1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)),
                j1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)),
                k1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize4D(
            (double i, double j, double k, double l) tuple)
            => (tuple.i / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k) + (tuple.l * tuple.l)),
                tuple.j / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k) + (tuple.l * tuple.l)),
                tuple.k / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k) + (tuple.l * tuple.l)),
                tuple.l / Sqrt((tuple.i * tuple.i) + (tuple.j * tuple.j) + (tuple.k * tuple.k) + (tuple.l * tuple.l)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize4D(
            double i, double j, double k, double l)
            => (i / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                j / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                k / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                l / Sqrt((i * i) + (j * j) + (k * k) + (l * l)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the first Point.</param>
        /// <param name="l1"></param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <param name="l2"></param>
        /// <returns>The Normal of two Points</returns>
        /// <acknowledgment>
        /// http://www.fundza.com/vectors/normalize/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize4D(
            double i1, double j1, double k1, double l1,
            double i2, double j2, double k2, double l2)
            => (
                i1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                j1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                k1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                l1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)));
        #endregion Unit Normalize

        #region Unit Normal
        /// <summary>
        /// Get the unit normal.
        /// </summary>
        /// <param name="pt1X">The pt1X.</param>
        /// <param name="pt1Y">The pt1Y.</param>
        /// <param name="pt2X">The pt2X.</param>
        /// <param name="pt2Y">The pt2Y.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        public static (double X, double Y) UnitNormal(double pt1X, double pt1Y, double pt2X, double pt2Y)
        {
            var dx = pt2X - pt1X;
            var dy = pt2Y - pt1Y;
            if ((dx == 0d) && (dy == 0d))
            {
                return (0d, 0d);
            }

            var f = 1d / Sqrt((dx * dx) + (dy * dy));
            dx *= f;
            dy *= f;

            return (dy, -dx);
        }
        #endregion Unit Normal

        #region Perpendicular Clockwise
        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        /// <remarks>
        /// To get the perpendicular vector in two dimensions use I = -J, J = I
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularClockwise(double i, double j) => (-j, i);
        #endregion Perpendicular Clockwise

        #region Perpendicular Counter Clockwise
        /// <summary>
        /// Find the Counter Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        /// <remarks>
        /// To get the perpendicular vector in two dimensions use I = -J, J = I
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularCounterClockwise(double i, double j) => (j, -i);
        #endregion Perpendicular Counter Clockwise

        #region Pitch Rotate X
        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double yOff, double zOff, double rad) => RotateX(x1, y1, z1, yOff, zOff, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="sin">The Sine of the angle.</param>
        /// <param name="cos">The Cosine of the angle.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double yOff, double zOff, double sin, double cos)
            => (
                x1,
                (y1 * cos) - (z1 * sin) + ((yOff * (1d - cos)) + (zOff * sin)),
                (y1 * sin) + (z1 * cos) + ((zOff * (1d - cos)) - (yOff * sin))
                );

        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double rad) => RotateX(x1, y1, z1, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="cos">The cos.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double sin, double cos)
            => (
                x1,
                (y1 * cos) - (z1 * sin),
                (y1 * sin) + (z1 * cos)
                );

        /// <summary>
        /// The pitch.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Pitch(double x1, double y1, double z1, double rad) => RotateX(x1, y1, z1, Sin(rad), Cos(rad));
        #endregion Pitch Rotate X

        #region Yaw Rotate Y
        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double xOff, double zOff, double rad) => RotateY(x1, y1, z1, xOff, zOff, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="sin">The Sine of the angle.</param>
        /// <param name="cos">The Cosine of the angle.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double xOff, double zOff, double sin, double cos)
            => (
                (z1 * sin) + (x1 * cos) + ((xOff * (1d - cos)) - (zOff * sin)),
                y1,
                (z1 * cos) - (x1 * sin) + ((zOff * (1d - cos)) + (xOff * sin))
                );

        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double rad) => RotateY(x1, y1, z1, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="sin">The Sine of the angle.</param>
        /// <param name="cos">The Cosine of the angle.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double sin, double cos)
            => (
                (z1 * sin) + (x1 * cos),
                y1,
                (z1 * cos) - (x1 * sin)
                );

        /// <summary>
        /// The yaw.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Yaw(double x1, double y1, double z1, double rad) => RotateY(x1, y1, z1, Sin(rad), Cos(rad));
        #endregion Yaw Rotate Y

        #region Roll Rotate Z
        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double xOff, double yOff, double rad) => RotateZ(x1, y1, z1, xOff, yOff, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="sin">The Sine of the angle.</param>
        /// <param name="cos">The Cosine of the angle.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double xOff, double yOff, double sin, double cos)
            => (
                (x1 * cos) - (y1 * sin) + ((xOff * (1d - cos)) + (yOff * sin)),
                (x1 * sin) + (y1 * cos) + ((yOff * (1d - cos)) - (xOff * sin)),
                z1
                );

        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double rad) => RotateZ(x1, y1, z1, Sin(rad), Cos(rad));

        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="cos">The cos.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double sin, double cos)
            => (
                (x1 * cos) - (y1 * sin),
                (x1 * sin) + (y1 * cos),
                z1
                );

        /// <summary>
        /// The roll.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Roll(double x1, double y1, double z1, double rad) => RotateZ(x1, y1, z1, Sin(rad), Cos(rad));
        #endregion Roll Rotate Z

        #region Projection
        /// <summary>
        /// The projection.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Projection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)
                );
        #endregion Projection

        #region Rejection
        /// <summary>
        /// The rejection.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Rejection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x1 - (x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)),
                z1 - (y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)),
                z1 - (z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2))
                );
        #endregion Rejection

        #region Reflection
        /// <summary>
        /// The reflection.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="k2">The k2.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Reflection(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
        {
            // if v2 has a right angle to vector, return -vector and stop
            if (Math.Abs(Math.Abs(Angle(i1, j1, k1, i2, j2, k2)) - (PI / 2d)) < double.Epsilon)
            {
                return (-i1, -j1, -k1);
            }

            (var x, var y, var z) = Projection(i1, j1, k1, i2, j2, k2);
            return (
                ((2d * x) - i1) * Magnitude(i1, j1, k1),
                ((2d * y) - j1) * Magnitude(i1, j1, k1),
                ((2d * z) - k1) * Magnitude(i1, j1, k1)
                );
        }
        #endregion Reflection

        #region Min Point
        /// <summary>
        /// The min point.
        /// </summary>
        /// <param name="point1X">The point1X.</param>
        /// <param name="point1Y">The point1Y.</param>
        /// <param name="point2X">The point2X.</param>
        /// <param name="point2Y">The point2Y.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MinPoint(double point1X, double point1Y, double point2X, double point2Y) => (Math.Min(point1X, point2X), Math.Min(point1Y, point2Y));
        #endregion Min Point

        #region Max Point
        /// <summary>
        /// The max point.
        /// </summary>
        /// <param name="point1X">The point1X.</param>
        /// <param name="point1Y">The point1Y.</param>
        /// <param name="point2X">The point2X.</param>
        /// <param name="point2Y">The point2Y.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MaxPoint(double point1X, double point1Y, double point2X, double point2Y) => (Math.Max(point1X, point2X), Math.Max(point1Y, point2Y));
        #endregion Max Point

        ///// <summary>
        ///// Ares the close.
        ///// </summary>
        ///// <param name="aX">a x.</param>
        ///// <param name="aY">a y.</param>
        ///// <param name="bX">The b x.</param>
        ///// <param name="bY">The b y.</param>
        ///// <param name="epsilonSqrd">The epsilon SQRD.</param>
        ///// <returns></returns>
        ////[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool AreClose(double aX, double aY, double bX, double bY, double epsilonSqrd = double.Epsilon * double.Epsilon)
        //    => (Distances.SquareDistance(aX, aY, bX, bY) <= epsilonSqrd);
    }
}
