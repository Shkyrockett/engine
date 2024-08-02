// <copyright file="MathematicalConstants.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public static partial class Numbers<T>
    where T : INumber<T>
{
    /// <summary>
    /// The lower limit for H.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T HueMin = T.Zero;

    /// <summary>
    /// The upper limit for H.
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public static readonly T HueMax = T.CreateChecked(360);

    /// <summary>
    /// The lower limit for R, G, B (integer version).
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public const byte RGBMin = 0;

    /// <summary>
    /// The upper limit for R, G, B (integer version).
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public const byte RGBMax = 255;

    /// <summary>
    /// The lower limit for R, G, B (integer version).
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public const byte CMYKMin = 0;

    /// <summary>
    /// The upper limit for R, G, B (integer version).
    /// </summary>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    public const byte CMYKMax = 100;

    #region Bézier Bernstein Basis Matrices
    /// <summary>
    /// The linear Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        LinearBezierBernsteinBasisMatrix
        = (T.One, T.Zero,
          -T.One, T.One);

    /// <summary>
    /// The quadratic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        QuadraticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero,
           T.CreateSaturating(-2), T.CreateSaturating(2), T.Zero,
           T.One, T.CreateSaturating(-2), T.One);

    /// <summary>
    /// The cubic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        CubicBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-3), T.CreateSaturating(3), T.Zero, T.Zero,
           T.CreateSaturating(3), T.CreateSaturating(-6), T.CreateSaturating(3), T.Zero,
           -T.One, T.CreateSaturating(3), T.CreateSaturating(-3), T.One);

    /// <summary>
    /// The quartic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        QuarticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-4), T.CreateSaturating(4), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(6), T.CreateSaturating(-12), T.CreateSaturating(6), T.Zero, T.Zero,
           T.CreateSaturating(-4), T.CreateSaturating(12), T.CreateSaturating(-12), T.CreateSaturating(4), T.Zero,
           T.One, T.CreateSaturating(-4), T.CreateSaturating(6), T.CreateSaturating(-4), T.One);

    /// <summary>
    /// The quintic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        QuinticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-5), T.CreateSaturating(5), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(10), T.CreateSaturating(-20), T.CreateSaturating(10), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-10), T.CreateSaturating(30), T.CreateSaturating(-30), T.CreateSaturating(10), T.Zero, T.Zero,
           T.CreateSaturating(5), T.CreateSaturating(-20), T.CreateSaturating(30), T.CreateSaturating(-20), T.CreateSaturating(5), T.Zero,
           -T.One, T.CreateSaturating(5), T.CreateSaturating(-10), T.CreateSaturating(10), T.CreateSaturating(-5), T.One);

    /// <summary>
    /// The sextic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7)
        SexticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-6), T.CreateSaturating(6), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(15), T.CreateSaturating(-30), T.CreateSaturating(15), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-20), T.CreateSaturating(60), T.CreateSaturating(-60), T.CreateSaturating(20), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(15), T.CreateSaturating(-60), T.CreateSaturating(90), T.CreateSaturating(-60), T.CreateSaturating(15), T.Zero, T.Zero,
           T.CreateSaturating(-6), T.CreateSaturating(30), T.CreateSaturating(-60), T.CreateSaturating(60), T.CreateSaturating(-30), T.CreateSaturating(6), T.Zero,
           T.One, T.CreateSaturating(-6), T.CreateSaturating(15), T.CreateSaturating(-20), T.CreateSaturating(15), T.CreateSaturating(-6), T.One);

    /// <summary>
    /// The septic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8)
        SepticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-7), T.CreateSaturating(7), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(21), T.CreateSaturating(-42), T.CreateSaturating(21), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-35), T.CreateSaturating(105), T.CreateSaturating(-105), T.CreateSaturating(35), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(35), T.CreateSaturating(-140), T.CreateSaturating(210), T.CreateSaturating(-140), T.CreateSaturating(35), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-21), T.CreateSaturating(105), T.CreateSaturating(-210), T.CreateSaturating(210), T.CreateSaturating(-105), T.CreateSaturating(21), T.Zero, T.Zero,
           T.CreateSaturating(7), T.CreateSaturating(-42), T.CreateSaturating(105), T.CreateSaturating(-140), T.CreateSaturating(105), T.CreateSaturating(-42), T.CreateSaturating(7), T.Zero,
           -T.One, T.CreateSaturating(7), T.CreateSaturating(-21), T.CreateSaturating(35), T.CreateSaturating(-35), T.CreateSaturating(21), T.CreateSaturating(-7), T.One);

    /// <summary>
    /// The octic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9)
        OcticBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-8), T.CreateSaturating(8), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(28), T.CreateSaturating(-56), T.CreateSaturating(28), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-56), T.CreateSaturating(168), T.CreateSaturating(-168), T.CreateSaturating(56), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(70), T.CreateSaturating(-280), T.CreateSaturating(240), T.CreateSaturating(-280), T.CreateSaturating(70), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-56), T.CreateSaturating(280), T.CreateSaturating(-560), T.CreateSaturating(560), T.CreateSaturating(-280), T.CreateSaturating(56), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(28), T.CreateSaturating(-168), T.CreateSaturating(420), T.CreateSaturating(-560), T.CreateSaturating(420), T.CreateSaturating(-168), T.CreateSaturating(28), T.Zero, T.Zero,
           T.CreateSaturating(-8), T.CreateSaturating(56), T.CreateSaturating(-168), T.CreateSaturating(280), T.CreateSaturating(-280), T.CreateSaturating(168), T.CreateSaturating(-56), T.CreateSaturating(8), T.Zero,
           T.One, T.CreateSaturating(-8), T.CreateSaturating(28), T.CreateSaturating(-56), T.CreateSaturating(70), T.CreateSaturating(-56), T.CreateSaturating(28), T.CreateSaturating(-8), T.One);

    /// <summary>
    /// The nonic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10)
        NonicBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-9), T.CreateSaturating(9), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(36), T.CreateSaturating(-72), T.CreateSaturating(36), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-84), T.CreateSaturating(252), T.CreateSaturating(-252), T.CreateSaturating(84), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(126), T.CreateSaturating(-504), T.CreateSaturating(756), T.CreateSaturating(-504), T.CreateSaturating(126), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-126), T.CreateSaturating(630), T.CreateSaturating(-1260), T.CreateSaturating(1260), T.CreateSaturating(-630), T.CreateSaturating(126), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(84), T.CreateSaturating(-504), T.CreateSaturating(1260), T.CreateSaturating(-1680), T.CreateSaturating(1260), T.CreateSaturating(-504), T.CreateSaturating(84), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-36), T.CreateSaturating(252), T.CreateSaturating(-756), T.CreateSaturating(1260), T.CreateSaturating(-1260), T.CreateSaturating(756), T.CreateSaturating(-252), T.CreateSaturating(36), T.Zero, T.Zero,
           T.CreateSaturating(9), T.CreateSaturating(-27), T.CreateSaturating(252), T.CreateSaturating(-504), T.CreateSaturating(360), T.CreateSaturating(-504), T.CreateSaturating(252), T.CreateSaturating(-72), T.CreateSaturating(9), T.Zero,
           -T.One, T.CreateSaturating(9), T.CreateSaturating(-36), T.CreateSaturating(84), T.CreateSaturating(-126), T.CreateSaturating(126), T.CreateSaturating(-84), T.CreateSaturating(36), T.CreateSaturating(-9), T.One);

    /// <summary>
    /// The decic Bézier Bernstein basis matrix.
    /// </summary>
    public static readonly (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6, T m1x7, T m1x8, T m1x9, T m1x10, T m1x11,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6, T m2x7, T m2x8, T m2x9, T m2x10, T m2x11,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6, T m3x7, T m3x8, T m3x9, T m3x10, T m3x11,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6, T m4x7, T m4x8, T m4x9, T m4x10, T m4x11,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6, T m5x7, T m5x8, T m5x9, T m5x10, T m5x11,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6, T m6x7, T m6x8, T m6x9, T m6x10, T m6x11,
        T m7x1, T m7x2, T m7x3, T m7x4, T m7x5, T m7x6, T m7x7, T m7x8, T m7x9, T m7x10, T m7x11,
        T m8x1, T m8x2, T m8x3, T m8x4, T m8x5, T m8x6, T m8x7, T m8x8, T m8x9, T m8x10, T m8x11,
        T m9x1, T m9x2, T m9x3, T m9x4, T m9x5, T m9x6, T m9x7, T m9x8, T m9x9, T m9x10, T m9x11,
        T m10x1, T m10x2, T m10x3, T m10x4, T m10x5, T m10x6, T m10x7, T m10x8, T m10x9, T m10x10, T m10x11,
        T m11x1, T m11x2, T m11x3, T m11x4, T m11x5, T m11x6, T m11x7, T m11x8, T m11x9, T m11x10, T m11x11)
        DecicBezierBernsteinBasisMatrix
        = (T.One, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-10), T.CreateSaturating(10), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(45), T.CreateSaturating(-90), T.CreateSaturating(45), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-120), T.CreateSaturating(360), T.CreateSaturating(-360), T.CreateSaturating(120), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(210), T.CreateSaturating(-840), T.CreateSaturating(1260), T.CreateSaturating(-840), T.CreateSaturating(210), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-252), T.CreateSaturating(1260), T.CreateSaturating(-2520), T.CreateSaturating(2520), T.CreateSaturating(-1260), T.CreateSaturating(252), T.Zero, T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(210), T.CreateSaturating(-1260), T.CreateSaturating(3150), T.CreateSaturating(-4200), T.CreateSaturating(3150), T.CreateSaturating(-1260), T.CreateSaturating(210), T.Zero, T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(-120), T.CreateSaturating(840), T.CreateSaturating(-2520), T.CreateSaturating(4200), T.CreateSaturating(-4200), T.CreateSaturating(2520), T.CreateSaturating(-840), T.CreateSaturating(120), T.Zero, T.Zero, T.Zero,
           T.CreateSaturating(45), T.CreateSaturating(-360), T.CreateSaturating(1260), T.CreateSaturating(-2520), T.CreateSaturating(3150), T.CreateSaturating(-2520), T.CreateSaturating(1260), T.CreateSaturating(-T.CreateSaturating(360)), T.CreateSaturating(45), T.Zero, T.Zero,
           T.CreateSaturating(-10), T.CreateSaturating(90), T.CreateSaturating(-360), T.CreateSaturating(840), T.CreateSaturating(-1260), T.CreateSaturating(1260), T.CreateSaturating(-840), T.CreateSaturating(360), T.CreateSaturating(-90), T.CreateSaturating(10), T.Zero,
           T.One, T.CreateSaturating(-10), T.CreateSaturating(45), T.CreateSaturating(-120), T.CreateSaturating(210), T.CreateSaturating(-252), T.CreateSaturating(210), T.CreateSaturating(-120), T.CreateSaturating(45), T.CreateSaturating(-10), T.One);
    #endregion
}
