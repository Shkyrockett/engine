// <copyright file="Operations.Vectors.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
        #region Is Empty Vector
        /// <summary>
        /// Determines whether [is empty vector] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>
        ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyVector(double i, double j) => Math.Abs(i) < double.Epsilon && Math.Abs(j) < double.Epsilon;

        /// <summary>
        /// Determines whether [is empty vector] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>
        ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyVector(double i, double j, double k) => Math.Abs(i) < double.Epsilon && Math.Abs(j) < double.Epsilon && Math.Abs(k) < double.Epsilon;

        /// <summary>
        /// Determines whether [is empty vector] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <returns>
        ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyVector(double i, double j, double k, double l) => Math.Abs(i) < double.Epsilon && Math.Abs(j) < double.Epsilon && Math.Abs(k) < double.Epsilon && Math.Abs(l) < double.Epsilon;

        /// <summary>
        /// Determines whether [is empty vector] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <returns>
        ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyVector(double i, double j, double k, double l, double m) => Math.Abs(i) < double.Epsilon && Math.Abs(j) < double.Epsilon && Math.Abs(k) < double.Epsilon && Math.Abs(l) < double.Epsilon && Math.Abs(m) < double.Epsilon;

        /// <summary>
        /// Determines whether [is empty vector] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyVector(double i, double j, double k, double l, double m, double n) => Math.Abs(i) < double.Epsilon && Math.Abs(j) < double.Epsilon && Math.Abs(k) < double.Epsilon && Math.Abs(l) < double.Epsilon && Math.Abs(m) < double.Epsilon && Math.Abs(n) < double.Epsilon;
        #endregion

        #region Is Unit Vector
        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i">The i1.</param>
        /// <param name="j">The j1.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i, double j) => Math.Abs(Magnitude(i, j) - 1d) < double.Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i">The i1.</param>
        /// <param name="j">The j1.</param>
        /// <param name="k">The k1.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i, double j, double k) => Math.Abs(Magnitude(i, j, k) - 1d) < double.Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i">The i1.</param>
        /// <param name="j">The j1.</param>
        /// <param name="k">The k1.</param>
        /// <param name="l">The l.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i, double j, double k, double l) => Math.Abs(Magnitude(i, j, k, l) - 1d) < double.Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i">The i1.</param>
        /// <param name="j">The j1.</param>
        /// <param name="k">The k1.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i, double j, double k, double l, double m) => Math.Abs(Magnitude(i, j, k, l, m) - 1d) < double.Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i">The i1.</param>
        /// <param name="j">The j1.</param>
        /// <param name="k">The k1.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i, double j, double k, double l, double m, double n) => Math.Abs(Magnitude(i, j, k, l, m, n) - 1d) < double.Epsilon;
        #endregion

        #region Add Value To Vector
        /// <summary>
        /// Adds the vector uniformly.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) AddVectorUniform(double augendA, double augendB, double addend) => (augendA + addend, augendB + addend);

        /// <summary>
        /// Adds the vector uniformly.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) AddVectorUniform(double augendA, double augendB, double augendC, double addend) => (augendA + addend, augendB + addend, augendC + addend);

        /// <summary>
        /// Adds the vector uniformly.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) AddVectorUniform(double augendA, double augendB, double augendC, double augendD, double addend) => (augendA + addend, augendB + addend, augendC + addend, augendD + addend);

        /// <summary>
        /// Adds the vector uniformly.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="augendE">The augend e.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) AddVectorUniform(double augendA, double augendB, double augendC, double augendD, double augendE, double addend) => (augendA + addend, augendB + addend, augendC + addend, augendD + addend, augendE + addend);

        /// <summary>
        /// Adds the vector uniformly.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="augendE">The augend e.</param>
        /// <param name="augendF">The augend f.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) AddVectorUniform(double augendA, double augendB, double augendC, double augendD, double augendE, double augendF, double addend) => (augendA + addend, augendB + addend, augendC + addend, augendD + addend, augendE + addend, augendF + addend);
        #endregion Add Value To Vector

        #region Add Two Vectors
        /// <summary>
        /// Adds the vectors.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="addendA">The addend a.</param>
        /// <param name="addendB">The addend b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) AddVectors(double augendA, double augendB, double addendA, double addendB) => (augendA + addendA, augendB + addendB);

        /// <summary>
        /// Adds the vectors.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="addendA">The addend a.</param>
        /// <param name="addendB">The addend b.</param>
        /// <param name="addendC">The addend c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) AddVectors(double augendA, double augendB, double augendC, double addendA, double addendB, double addendC) => (augendA + addendA, augendB + addendB, augendC + addendC);

        /// <summary>
        /// Adds the vectors.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="addendA">The addend a.</param>
        /// <param name="addendB">The addend b.</param>
        /// <param name="addendC">The addend c.</param>
        /// <param name="addendD">The addend d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) AddVectors(double augendA, double augendB, double augendC, double augendD, double addendA, double addendB, double addendC, double addendD) => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD);

        /// <summary>
        /// Adds the vectors.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="augendE">The augend e.</param>
        /// <param name="addendA">The addend a.</param>
        /// <param name="addendB">The addend b.</param>
        /// <param name="addendC">The addend c.</param>
        /// <param name="addendD">The addend d.</param>
        /// <param name="addendE">The addend e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) AddVectors(double augendA, double augendB, double augendC, double augendD, double augendE, double addendA, double addendB, double addendC, double addendD, double addendE) => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD, augendE + addendE);

        /// <summary>
        /// Adds the vectors.
        /// </summary>
        /// <param name="augendA">The augend a.</param>
        /// <param name="augendB">The augend b.</param>
        /// <param name="augendC">The augend c.</param>
        /// <param name="augendD">The augend d.</param>
        /// <param name="augendE">The augend e.</param>
        /// <param name="augendF">The augend f.</param>
        /// <param name="addendA">The addend a.</param>
        /// <param name="addendB">The addend b.</param>
        /// <param name="addendC">The addend c.</param>
        /// <param name="addendD">The addend d.</param>
        /// <param name="addendE">The addend e.</param>
        /// <param name="addendF">The addend f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) AddVectors(double augendA, double augendB, double augendC, double augendD, double augendE, double augendF, double addendA, double addendB, double addendC, double addendD, double addendE, double addendF) => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD, augendE + addendE, augendF + addendF);
        #endregion Add Two Vectors

        #region Subtract Value From Vector
        /// <summary>
        /// Subtracts the vector uniformly.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) SubtractVectorUniform(double minuendA, double minuendB, double subend) => (minuendA - subend, minuendB - subend);

        /// <summary>
        /// Subtracts the vector uniformly.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) SubtractVectorUniform(double minuendA, double minuendB, double minuendC, double subend) => (minuendA - subend, minuendB - subend, minuendC - subend);

        /// <summary>
        /// Subtracts the vector uniformly.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) SubtractVectorUniform(double minuendA, double minuendB, double minuendC, double minuendD, double subend) => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend);

        /// <summary>
        /// Subtracts the vector uniformly.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) SubtractVectorUniform(double minuendA, double minuendB, double minuendC, double minuendD, double minuendE, double subend) => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend, minuendE - subend);

        /// <summary>
        /// Subtracts the vector uniformly.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <param name="minuendF">The minuend f.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) SubtractVectorUniform(double minuendA, double minuendB, double minuendC, double minuendD, double minuendE, double minuendF, double subend) => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend, minuendE - subend, minuendF - subend);
        #endregion Subtract Value From Vector

        #region Subtract Vector From Value
        /// <summary>
        /// Subtracts from minuend2 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) SubtractFromMinuend(double minuend, double subendA, double subendB) => (minuend - subendA, minuend - subendB);

        /// <summary>
        /// Subtracts from minuend3 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) SubtractFromMinuend(double minuend, double subendA, double subendB, double subendC) => (minuend - subendA, minuend - subendB, minuend - subendC);

        /// <summary>
        /// Subtracts from minuend4 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) SubtractFromMinuend(double minuend, double subendA, double subendB, double subendC, double subendD) => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD);

        /// <summary>
        /// Subtracts from minuend5 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) SubtractFromMinuend(double minuend, double subendA, double subendB, double subendC, double subendD, double subendE) => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD, minuend - subendE);

        /// <summary>
        /// Subtracts from minuend6 d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <param name="subendF">The subend f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) SubtractFromMinuend(double minuend, double subendA, double subendB, double subendC, double subendD, double subendE, double subendF) => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD, minuend - subendE, minuend - subendF);
        #endregion Subtract Vector From Value

        #region Subtract Two Vectors
        /// <summary>
        /// Subtracts the vector.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) SubtractVector(double minuendA, double minuendB, double subendA, double subendB) => (minuendA - subendA, minuendB - subendB);

        /// <summary>
        /// Subtracts the vector.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) SubtractVector(double minuendA, double minuendB, double minuendC, double subendA, double subendB, double subendC) => (minuendA - subendA, minuendB - subendB, minuendC - subendC);

        /// <summary>
        /// Subtracts the vector.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) SubtractVector(double minuendA, double minuendB, double minuendC, double minuendD, double subendA, double subendB, double subendC, double subendD) => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD);

        /// <summary>
        /// Subtracts the vector.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) SubtractVector(double minuendA, double minuendB, double minuendC, double minuendD, double minuendE, double subendA, double subendB, double subendC, double subendD, double subendE) => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD, minuendE - subendE);

        /// <summary>
        /// Subtracts the vector.
        /// </summary>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <param name="minuendF">The minuend f.</param>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <param name="subendF">The subend f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) SubtractVector(double minuendA, double minuendB, double minuendC, double minuendD, double minuendE, double minuendF, double subendA, double subendB, double subendC, double subendD, double subendE, double subendF) => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD, minuendE - subendE, minuendF - subendF);
        #endregion Subtract Two Vectors

        #region Difference Between Two Vectors
        /// <summary>
        /// Finds the Delta of two 2D Vectors.
        /// </summary>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DeltaVector(double subendA, double subendB, double minuendA, double minuendB) => SubtractVector(minuendA, minuendB, subendA, subendB);

        /// <summary>
        /// Finds the Delta of two 3D Vectors.
        /// </summary>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DeltaVector(double subendA, double subendB, double subendC, double minuendA, double minuendB, double minuendC) => SubtractVector(minuendA, minuendB, minuendC, subendA, subendB, subendC);

        /// <summary>
        /// Finds the Delta of two 4D Vectors.
        /// </summary>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DeltaVector(double subendA, double subendB, double subendC, double subendD, double minuendA, double minuendB, double minuendC, double minuendD) => SubtractVector(minuendA, minuendB, minuendC, minuendD, subendA, subendB, subendC, subendD);

        /// <summary>
        /// Finds the Delta of two 5D Vectors.
        /// </summary>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) DeltaVector(double subendA, double subendB, double subendC, double subendD, double subendE, double minuendA, double minuendB, double minuendC, double minuendD, double minuendE) => SubtractVector(minuendA, minuendB, minuendC, minuendD, minuendE, subendA, subendB, subendC, subendD, subendE);

        /// <summary>
        /// Finds the Delta of two 6D Vectors.
        /// </summary>
        /// <param name="subendA">The subend a.</param>
        /// <param name="subendB">The subend b.</param>
        /// <param name="subendC">The subend c.</param>
        /// <param name="subendD">The subend d.</param>
        /// <param name="subendE">The subend e.</param>
        /// <param name="subendF">The subend f.</param>
        /// <param name="minuendA">The minuend a.</param>
        /// <param name="minuendB">The minuend b.</param>
        /// <param name="minuendC">The minuend c.</param>
        /// <param name="minuendD">The minuend d.</param>
        /// <param name="minuendE">The minuend e.</param>
        /// <param name="minuendF">The minuend f.</param>
        /// <returns>
        /// Returns the Difference Between PointA and PointB
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) DeltaVector(double subendA, double subendB, double subendC, double subendD, double subendE, double subendF, double minuendA, double minuendB, double minuendC, double minuendD, double minuendE, double minuendF) => SubtractVector(minuendA, minuendB, minuendC, minuendD, minuendE, minuendF, subendA, subendB, subendC, subendD, subendE, subendF);
        #endregion Difference Between Two Vectors

        #region Multiply A Vector by a Scalar Value
        /// <summary>
        /// Scales the vector.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) ScaleVector(double multiplicandA, double multiplicandB, double scalar) => (multiplicandA * scalar, multiplicandB * scalar);

        /// <summary>
        /// Scales the vector.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) ScaleVector(double multiplicandA, double multiplicandB, double multiplicandC, double scalar) => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar);

        /// <summary>
        /// Scales the vector.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) ScaleVector(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double scalar) => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar);

        /// <summary>
        /// Scales the vector.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="multiplicandE">The multiplicand e.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) ScaleVector(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double multiplicandE, double scalar) => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar, multiplicandE * scalar);

        /// <summary>
        /// Scales the vector.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="multiplicandE">The multiplicand e.</param>
        /// <param name="multiplicandF">The multiplicand f.</param>
        /// <param name="scalar">The scalar.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) ScaleVector(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double multiplicandE, double multiplicandF, double scalar) => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar, multiplicandE * scalar, multiplicandF * scalar);
        #endregion Multiply A Vector by a Value

        #region Multiply Each Vector Component By The One in Another Vector
        /// <summary>
        /// Scales the vector parametrically.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplierA">The multiplier a.</param>
        /// <param name="multiplierB">The multiplier b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) ScaleVectorParametric(double multiplicandA, double multiplicandB, double multiplierA, double multiplierB) => (multiplicandA * multiplierA, multiplicandB * multiplierB);

        /// <summary>
        /// Scales the vector parametrically.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplierA">The multiplier a.</param>
        /// <param name="multiplierB">The multiplier b.</param>
        /// <param name="multiplierC">The multiplier c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) ScaleVectorParametric(double multiplicandA, double multiplicandB, double multiplicandC, double multiplierA, double multiplierB, double multiplierC) => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC);

        /// <summary>
        /// Scales the vector parametrically.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="multiplierA">The multiplier a.</param>
        /// <param name="multiplierB">The multiplier b.</param>
        /// <param name="multiplierC">The multiplier c.</param>
        /// <param name="multiplierD">The multiplier d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) ScaleVectorParametric(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double multiplierA, double multiplierB, double multiplierC, double multiplierD) => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD);

        /// <summary>
        /// Scales the vector parametrically.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="multiplicandE">The multiplicand e.</param>
        /// <param name="multiplierA">The multiplier a.</param>
        /// <param name="multiplierB">The multiplier b.</param>
        /// <param name="multiplierC">The multiplier c.</param>
        /// <param name="multiplierD">The multiplier d.</param>
        /// <param name="multiplierE">The multiplier e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) ScaleVectorParametric(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double multiplicandE, double multiplierA, double multiplierB, double multiplierC, double multiplierD, double multiplierE) => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD, multiplicandE * multiplierE);

        /// <summary>
        /// Scales the vector parametrically.
        /// </summary>
        /// <param name="multiplicandA">The multiplicand a.</param>
        /// <param name="multiplicandB">The multiplicand b.</param>
        /// <param name="multiplicandC">The multiplicand c.</param>
        /// <param name="multiplicandD">The multiplicand d.</param>
        /// <param name="multiplicandE">The multiplicand e.</param>
        /// <param name="multiplicandF">The multiplicand f.</param>
        /// <param name="multiplierA">The multiplier a.</param>
        /// <param name="multiplierB">The multiplier b.</param>
        /// <param name="multiplierC">The multiplier c.</param>
        /// <param name="multiplierD">The multiplier d.</param>
        /// <param name="multiplierE">The multiplier e.</param>
        /// <param name="multiplierF">The multiplier f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) ScaleVectorParametric(double multiplicandA, double multiplicandB, double multiplicandC, double multiplicandD, double multiplicandE, double multiplicandF, double multiplierA, double multiplierB, double multiplierC, double multiplierD, double multiplierE, double multiplierF) => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD, multiplicandE * multiplierE, multiplicandF * multiplierF);
        #endregion Multiply Each Vector Component By The One in Another Vector

        #region Divide Vector By Value
        /// <summary>
        /// Divides the vector uniform.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DivideVectorUniform(double divisorA, double divisorB, double dividend) => (divisorA / dividend, divisorB / dividend);

        /// <summary>
        /// Divides the vector uniform.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DivideVectorUniform(double divisorA, double divisorB, double divisorC, double dividend) => (divisorA / dividend, divisorB / dividend, divisorC / dividend);

        /// <summary>
        /// Divides the vector uniform.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DivideVectorUniform(double divisorA, double divisorB, double divisorC, double divisorD, double dividend) => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend);

        /// <summary>
        /// Divides the vector uniform.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="divisorE">The divisor e.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) DivideVectorUniform(double divisorA, double divisorB, double divisorC, double divisorD, double divisorE, double dividend) => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend, divisorE / dividend);

        /// <summary>
        /// Divides the vector uniform.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="divisorE">The divisor e.</param>
        /// <param name="divisorF">The divisor f.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) DivideVectorUniform(double divisorA, double divisorB, double divisorC, double divisorD, double divisorE, double divisorF, double dividend) => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend, divisorE / dividend, divisorF / dividend);
        #endregion Divide Vector By Value

        #region Divide Value into Vector Components
        /// <summary>
        /// Divides the by vector uniformly.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DivideByVectorUniform(double divisor, double dividendA, double dividendB) => (divisor / dividendA, divisor / dividendB);

        /// <summary>
        /// Divides the by vector uniformly.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DivideByVectorUniform(double divisor, double dividendA, double dividendB, double dividendC) => (divisor / dividendA, divisor / dividendB, divisor / dividendC);

        /// <summary>
        /// Divides the by vector uniformly.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DivideByVectorUniform(double divisor, double dividendA, double dividendB, double dividendC, double dividendD) => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD);

        /// <summary>
        /// Divides the by vector uniformly.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <param name="dividendE">The dividend e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) DivideByVectorUniform(double divisor, double dividendA, double dividendB, double dividendC, double dividendD, double dividendE) => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD, divisor / dividendE);

        /// <summary>
        /// Divides the by vector uniformly.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <param name="dividendE">The dividend e.</param>
        /// <param name="dividendF">The dividend f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) DivideByVectorUniform(double divisor, double dividendA, double dividendB, double dividendC, double dividendD, double dividendE, double dividendF) => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD, divisor / dividendE, divisor / dividendF);
        #endregion Divide Value into Vector Components

        #region Divide Each Of The Components Of A Vector By The Same Components Of Another Vector
        /// <summary>
        /// Divides the vector parametrically.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) DivideVectorParametric(double divisorA, double divisorB, double dividendA, double dividendB) => (divisorA / dividendA, divisorB / dividendB);

        /// <summary>
        /// Divides the vector parametrically.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) DivideVectorParametric(double divisorA, double divisorB, double divisorC, double dividendA, double dividendB, double dividendC) => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC);

        /// <summary>
        /// Divides the vector parametrically.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) DivideVectorParametric(double divisorA, double divisorB, double divisorC, double divisorD, double dividendA, double dividendB, double dividendC, double dividendD) => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD);

        /// <summary>
        /// Divides the vector parametrically.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="divisorE">The divisor e.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <param name="dividendE">The dividend e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) DivideVectorParametric(double divisorA, double divisorB, double divisorC, double divisorD, double divisorE, double dividendA, double dividendB, double dividendC, double dividendD, double dividendE) => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD, divisorE / dividendE);

        /// <summary>
        /// Divides the vector parametrically.
        /// </summary>
        /// <param name="divisorA">The divisor a.</param>
        /// <param name="divisorB">The divisor b.</param>
        /// <param name="divisorC">The divisor c.</param>
        /// <param name="divisorD">The divisor d.</param>
        /// <param name="divisorE">The divisor e.</param>
        /// <param name="divisorF">The divisor f.</param>
        /// <param name="dividendA">The dividend a.</param>
        /// <param name="dividendB">The dividend b.</param>
        /// <param name="dividendC">The dividend c.</param>
        /// <param name="dividendD">The dividend d.</param>
        /// <param name="dividendE">The dividend e.</param>
        /// <param name="dividendF">The dividend f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) DivideVectorParametric(double divisorA, double divisorB, double divisorC, double divisorD, double divisorE, double divisorF, double dividendA, double dividendB, double dividendC, double dividendD, double dividendE, double dividendF) => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD, divisorE / dividendE, divisorF / dividendF);
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

        /// <summary>
        /// The Magnitude of a four dimensional Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k, double l) => Sqrt((i * i) + (j * j) + (k * k) + (l * l));

        /// <summary>
        /// The Magnitude of a five dimensional Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k, double l, double m) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m));

        /// <summary>
        /// The Magnitude of a six dimensional Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k, double l, double m, double n) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n));
        #endregion Modulus Magnitude

        #region Invert
        /// <summary>
        /// Inverts the vector parametrically.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InvertVectorParametric(double x, double y) => (1d / x, 1d / y);

        /// <summary>
        /// Inverts the vector parametrically.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) InvertVectorParametric(double x, double y, double z) => (1d / x, 1d / y, 1d / z);

        /// <summary>
        /// Inverts the vector parametrically.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) InvertVectorParametric(double x, double y, double z, double w) => (1d / x, 1d / y, 1d / z, 1d / w);

        /// <summary>
        /// Inverts the vector parametrically.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V) InvertVectorParametric(double x, double y, double z, double w, double v) => (1d / x, 1d / y, 1d / z, 1d / w, 1d / v);

        /// <summary>
        /// Inverts the vector parametrically.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V, double U) InvertVectorParametric(double x, double y, double z, double w, double v, double u) => (1d / x, 1d / y, 1d / z, 1d / w, 1d / v, 1d / u);
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
        public static (double X, double Y) Lerp(double x0, double y0, double x1, double y1, double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t));

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
        public static (double X, double Y, double Z) Lerp(double x0, double y0, double z0, double x1, double y1, double z1, double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t));

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
        public static (double X, double Y, double Z, double W) Lerp(double x0, double y0, double z0, double w0, double x1, double y1, double z1, double w1, double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t));

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="w0">The w0.</param>
        /// <param name="v0">The v0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V) Lerp(double x0, double y0, double z0, double w0, double v0, double x1, double y1, double z1, double w1, double v1, double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t), v0 + ((v1 - v0) * t));

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="w0">The w0.</param>
        /// <param name="v0">The v0.</param>
        /// <param name="u0">The u0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="u1">The u1.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V, double U) Lerp(double x0, double y0, double z0, double w0, double v0, double u0, double x1, double y1, double z1, double w1, double v1, double u1, double t) => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t), v0 + ((v1 - v0) * t), u0 + ((u1 - u0) * t));
        #endregion Linear Interpolate

        #region Magnitude
        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j) => Sqrt((i * i) + (j * j));

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k) => Sqrt((i * i) + (j * j) + (k * k));

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <param name="l">The l parameter of the vector.</param>
        /// <returns>Returns the magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k, double l) => Sqrt((i * i) + (j * j) + (k * k) + (l * l));

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <param name="l">The l parameter of the vector.</param>
        /// <param name="m">The m.</param>
        /// <returns>
        /// Returns the magnitude of the vector.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k, double l, double m) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m));

        /// <summary>
        /// Calculates the magnitude or length of a vector.
        /// </summary>
        /// <param name="i">The i parameter of the vector.</param>
        /// <param name="j">The j parameter of the vector.</param>
        /// <param name="k">The k parameter of the vector.</param>
        /// <param name="l">The l parameter of the vector.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// Returns the magnitude of the vector.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitude(double i, double j, double k, double l, double m, double n) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n));
        #endregion

        #region Magnitude Squared
        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j) => (i * i) + (j * j);

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k) => (i * i) + (j * j) + (k * k);

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <param name="l">The l parameter.</param>
        /// <returns>Returns the squared magnitude of the vector.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k, double l) => (i * i) + (j * j) + (k * k) + (l * l);

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <param name="l">The l parameter.</param>
        /// <param name="m">The m.</param>
        /// <returns>
        /// Returns the squared magnitude of the vector.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k, double l, double m) => (i * i) + (j * j) + (k * k) + (l * l) + (m * m);

        /// <summary>
        /// Calculates the Magnitude or squared length of a vector.
        /// </summary>
        /// <param name="i">The i parameter.</param>
        /// <param name="j">The j parameter.</param>
        /// <param name="k">The k parameter.</param>
        /// <param name="l">The l parameter.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// Returns the squared magnitude of the vector.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double VectorMagnitudeSquared(double i, double j, double k, double l, double m, double n) => (i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n);
        #endregion

        #region Dot Product
        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y) tuple, double x2, double y2) => DotProduct(tuple.X, tuple.Y, x2, y2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y) tuple1, (double X, double Y) tuple2) => DotProduct(tuple1.X, tuple1.Y, tuple2.X, tuple2.Y);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z) tuple, double x2, double y2, double z2) => DotProduct(tuple.X, tuple.Y, tuple.Z, x2, y2, z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z) tuple1, (double X, double Y, double Z) tuple2) => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple2.X, tuple2.Y, tuple2.Z);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <param name="w2">The w2.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z, double W) tuple, double x2, double y2, double z2, double w2) => DotProduct(tuple.X, tuple.Y, tuple.Z, tuple.W, x2, y2, z2, w2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z, double W) tuple1, (double X, double Y, double Z, double W) tuple2) => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple1.W, tuple2.X, tuple2.Y, tuple2.Z, tuple2.W);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z, double W, double V) tuple, double x2, double y2, double z2, double w2, double v2) => DotProduct(tuple.X, tuple.Y, tuple.Z, tuple.W, tuple.V, x2, y2, z2, w2, v2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct((double X, double Y, double Z, double W, double V) tuple1, (double X, double Y, double Z, double W, double V) tuple2) => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple1.W, tuple1.V, tuple2.X, tuple2.Y, tuple2.Z, tuple2.W, tuple2.V);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        /// <remarks>
        /// <para>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double x1, double y1, double x2, double y2) => (x1 * x2) + (y1 * y2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double x1, double y1, double z1, double x2, double y2, double z2) => (x1 * x2) + (y1 * y2) + (z1 * z2);

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
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double x1, double y1, double z1, double w1, double x2, double y2, double z2, double w2) => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2);

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
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double x1, double y1, double z1, double w1, double v1, double x2, double y2, double z2, double w2, double v2) => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2) + (v1 * v2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="w1">First Point W component.</param>
        /// <param name="v1">First Point V component.</param>
        /// <param name="u1">The u1.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <param name="w2">Second Point W component.</param>
        /// <param name="v2">Second Point V component.</param>
        /// <param name="u2">The u2.</param>
        /// <returns>
        /// The Dot Product.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double x1, double y1, double z1, double w1, double v1, double u1, double x2, double y2, double z2, double w2, double v2, double u2) => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2) + (v1 * v2) + (u1 * u2);
        #endregion Dot Product

        #region Dot Product Triple Vector
        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductTriple(double x1, double y1, double x2, double y2, double x3, double y3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2));

        /// <summary>
        /// The Dot Product of the vector of three points
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
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductTriple(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2));

        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="w3">The w3.</param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductTriple(double x1, double y1, double z1, double w1, double x2, double y2, double z2, double w2, double x3, double y3, double z3, double w3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2));

        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="w3">The w3.</param>
        /// <param name="v3">The v3.</param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductTriple(double x1, double y1, double z1, double w1, double v1, double x2, double y2, double z2, double w2, double v2, double x3, double y3, double z3, double w3, double v3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2)) + ((v1 - v2) * (v3 - v2));

        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="u1">The u1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="u2">The u2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="w3">The w3.</param>
        /// <param name="v3">The v3.</param>
        /// <param name="u3">The u3.</param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductTriple(double x1, double y1, double z1, double w1, double v1, double u1, double x2, double y2, double z2, double w2, double v2, double u2, double x3, double y3, double z3, double w3, double v3, double u3) => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2)) + ((v1 - v2) * (v3 - v2)) + ((u1 - u2) * (u3 - u2));
        #endregion Dot Product Triple Vector

        #region Cross Product
        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns>
        /// the cross product AB · BC.
        /// </returns>
        /// <remarks>
        /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct((double x, double y) t1, (double x, double y) t2) => CrossProduct(t1.x, t1.y, t2.x, t2.y);

        /// <summary>
        /// The cross product.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct((double x, double y, double z) t1, (double x, double y, double z) t2) => CrossProduct(t1.x, t1.y, t1.z, t2.x, t2.y, t2.z);

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
        public static double CrossProduct(double x1, double y1, double x2, double y2) => (x1 * y2) - (y1 * x2);

        /// <summary>
        /// The cross product.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct(double x1, double y1, double z1, double x2, double y2, double z2) => (X: (y1 * z2) - (z1 * y2), Y: (z1 * x2) - (x1 * z2), Z: (x1 * y2) - (y1 * x2));
        #endregion Cross Product

        #region Cross Product Triple
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
        public static double CrossProductTriple(double x1, double y1, double x2, double y2, double x3, double y3) => ((x1 - x2) * (y3 - y2)) - ((y1 - y2) * (x3 - x2));

        /// <summary>
        /// Cross4 computes the four-dimensional cross product of the three vectors U, V and W, in that order. It returns the resulting four-vector.
        /// https://web.archive.org/web/20040213224251/http://research.microsoft.com/~hollasch/thesis/chapter2.html
        /// </summary>
        /// <param name="uI">The u i.</param>
        /// <param name="uJ">The u j.</param>
        /// <param name="uK">The u k.</param>
        /// <param name="uL">The u l.</param>
        /// <param name="vI">The v i.</param>
        /// <param name="vJ">The v j.</param>
        /// <param name="vK">The v k.</param>
        /// <param name="vL">The v l.</param>
        /// <param name="wI">The w i.</param>
        /// <param name="wJ">The w j.</param>
        /// <param name="wK">The w k.</param>
        /// <param name="wL">The w l.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W)
            CrossProductTriple(
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
        #endregion Cross Product Triple

        #region Complex Product
        /// <summary>
        /// The complex product.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
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
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MixedProduct(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3) => DotProduct(CrossProduct(x1, y1, z1, x2, y2, z2), x3, y3, z3);
        #endregion Mixed Product

        #region Normalize Vector
        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize((double i, double j) tuple) => Normalize(tuple.i, tuple.j);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize((double i, double j, double k) tuple) => Normalize(tuple.i, tuple.j, tuple.k);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize((double i, double j, double k, double l) tuple) => Normalize(tuple.i, tuple.j, tuple.k, tuple.l);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize(double i) => i / Sqrt(i * i);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize(double i, double j) => (i / Sqrt((i * i) + (j * j)), j / Sqrt((i * i) + (j * j)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize(double i, double j, double k) => (i / Sqrt((i * i) + (j * j) + (k * k)), j / Sqrt((i * i) + (j * j) + (k * k)), k / Sqrt((i * i) + (j * j) + (k * k)));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize(double i, double j, double k, double l) => (i / Sqrt((i * i) + (j * j) + (k * k) + (l * l)), j / Sqrt((i * i) + (j * j) + (k * k) + (l * l)), k / Sqrt((i * i) + (j * j) + (k * k) + (l * l)), l / Sqrt((i * i) + (j * j) + (k * k) + (l * l)));
        #endregion Normalize Vector

        #region Normalize Vectors
        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <returns>
        /// The Normal of two Points
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) NormalizeVectors(double i1, double j1, double i2, double j2) => (i1 / Sqrt((i1 * i2) + (j1 * j2)), j1 / Sqrt((i1 * i2) + (j1 * j2)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the second Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <returns>
        /// The Normal of two Points
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) NormalizeVectors(double i1, double j1, double k1, double i2, double j2, double k2) => (i1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)), j1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)), k1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the first Point.</param>
        /// <param name="l1">The l1.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <param name="l2">The l2.</param>
        /// <returns>
        /// The Normal of two Points
        /// </returns>
        /// <acknowledgment>
        /// http://www.fundza.com/vectors/normalize/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) NormalizeVectors(double i1, double j1, double k1, double l1, double i2, double j2, double k2, double l2) => (i1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), j1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), k1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), l1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)));
        #endregion Normalize Vectors

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
        /// <para>To get the perpendicular vector in two dimensions use I = -J, J = I</para>
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
        /// <para>To get the perpendicular vector in two dimensions use I = -J, J = I</para>
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
        public static (double X, double Y, double Z) PitchRotateX(double x1, double y1, double z1, double yOff, double zOff, double rad) => PitchRotateX(x1, y1, z1, yOff, zOff, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) PitchRotateX(double x1, double y1, double z1, double rad) => PitchRotateX(x1, y1, z1, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) PitchRotateX(double x1, double y1, double z1, double sin, double cos) => (x1, (y1 * cos) - (z1 * sin), (y1 * sin) + (z1 * cos));

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
        public static (double X, double Y, double Z) PitchRotateX(double x1, double y1, double z1, double yOff, double zOff, double sin, double cos) => (x1, (y1 * cos) - (z1 * sin) + ((yOff * (1d - cos)) + (zOff * sin)), (y1 * sin) + (z1 * cos) + ((zOff * (1d - cos)) - (yOff * sin)));
        #endregion Pitch Rotate X

        #region Yaw Rotate Y
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
        public static (double X, double Y, double Z) YawRotateY(double x1, double y1, double z1, double rad) => YawRotateY(x1, y1, z1, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) YawRotateY(double x1, double y1, double z1, double xOff, double zOff, double rad) => YawRotateY(x1, y1, z1, xOff, zOff, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) YawRotateY(double x1, double y1, double z1, double sin, double cos) => ((z1 * sin) + (x1 * cos), y1, (z1 * cos) - (x1 * sin));

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
        public static (double X, double Y, double Z) YawRotateY(double x1, double y1, double z1, double xOff, double zOff, double sin, double cos) => ((z1 * sin) + (x1 * cos) + ((xOff * (1d - cos)) - (zOff * sin)), y1, (z1 * cos) - (x1 * sin) + ((zOff * (1d - cos)) + (xOff * sin)));
        #endregion Yaw Rotate Y

        #region Roll Rotate Z
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
        public static (double X, double Y, double Z) RollRotateZ(double x1, double y1, double z1, double rad) => RollRotateZ(x1, y1, z1, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) RollRotateZ(double x1, double y1, double z1, double xOff, double yOff, double rad) => RollRotateZ(x1, y1, z1, xOff, yOff, Sin(rad), Cos(rad));

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
        public static (double X, double Y, double Z) RollRotateZ(double x1, double y1, double z1, double sin, double cos) => ((x1 * cos) - (y1 * sin), (x1 * sin) + (y1 * cos), z1);

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
        public static (double X, double Y, double Z) RollRotateZ(double x1, double y1, double z1, double xOff, double yOff, double sin, double cos) => ((x1 * cos) - (y1 * sin) + ((xOff * (1d - cos)) + (yOff * sin)), (x1 * sin) + (y1 * cos) + ((yOff * (1d - cos)) - (xOff * sin)), z1);
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
        public static (double X, double Y, double Z) Projection(double x1, double y1, double z1, double x2, double y2, double z2) => (x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2), y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2), z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2));
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
        public static (double X, double Y, double Z) Rejection(double x1, double y1, double z1, double x2, double y2, double z2) => (x1 - (x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)), z1 - (y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)), z1 - (z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)));
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
    }
}
