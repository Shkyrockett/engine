﻿// <copyright file="Complex.cs" >
//     Copyright (c) 2007 hanzzoid. All rights reserved.
// </copyright>
// <license>
//     Licensed under the Code Project Open License (CPOL). See http://www.codeproject.com/info/cpol10.aspx for full license information.
// </license>
// <author id="hanzzoid">hanzzoid</author>
// <summary></summary>

using System;
using System.Globalization;
using System.Security;

namespace Engine.Geometry
{
    /// <summary>
    /// The complex class.
    /// </summary>
    public class Complex
        : IFormattable
    {
        #region Implementations
        /// <summary>
        /// Imaginary unit.
        /// </summary>
        public static readonly Complex I = new Complex(0, 1);

        /// <summary>
        /// Complex number zero.
        /// </summary>
        public static readonly Complex Zero = new Complex(0, 0);

        /// <summary>
        /// Complex number one.
        /// </summary>
        public static readonly Complex One = new Complex(1, 0);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Inits complex number as (0, 0).
        /// </summary>
        public Complex()
            : this(0, 0)
        { }

        /// <summary>
        /// Inits complex number with imaginary part = 0.
        /// </summary>
        /// <param name="realPart"></param>
        public Complex(double realPart)
            : this(realPart, 0)
        { }

        /// <summary>
        /// Inits complex number.
        /// </summary>
        /// <param name="imaginaryPart"></param>
        /// <param name="realPart"></param>
        public Complex(double realPart, double imaginaryPart)
        {
            Real = realPart;
            Imaginary = imaginaryPart;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Contains the real part of a complex number.
        /// </summary>
        public double Real { get; set; }

        /// <summary>
        /// Contains the imaginary part of a complex number.
        /// </summary>
        public double Imaginary { get; set; }

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool IsReal
            => (Imaginary == 0);

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public bool IsImaginary
            => (Real == 0);
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator +(Complex a, Complex b)
            => new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator +(Complex a, double b)
            => new Complex(a.Real + b, a.Imaginary);

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator +(double a, Complex b)
            => new Complex(a + b.Real, b.Imaginary);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator -(Complex a, Complex b)
            => new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator -(Complex a, double b)
            => new Complex(a.Real - b, a.Imaginary);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator -(double a, Complex b)
            => new Complex(a - b.Real, -b.Imaginary);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator -(Complex a)
            => new Complex(-a.Real, -a.Imaginary);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator *(Complex a, Complex b)
            => new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Imaginary * b.Real + a.Real * b.Imaginary);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator *(Complex a, double d)
            => new Complex(d * a.Real, d * a.Imaginary);

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator *(double d, Complex a)
            => new Complex(d * a.Real, d * a.Imaginary);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator /(Complex a, Complex b)
            => a * Conj(b) * (1 / (Abs(b) * Abs(b)));

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator /(Complex a, double b)
            => a * (1 / b);

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex operator /(double a, Complex b)
            => a * Conj(b) * (1 / (Abs(b) * Abs(b)));

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Complex a, Complex b)
            => (a.Real == b.Real && a.Imaginary == b.Imaginary);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(Complex a, double b)
            => a == new Complex(b);

        /// <summary>
        /// The operator ==.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator ==(double a, Complex b)
            => new Complex(a) == b;

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Complex a, Complex b)
            => !(a == b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(Complex a, double b)
            => !(a == b);

        /// <summary>
        /// The operator !=.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool operator !=(double a, Complex b)
            => !(a == b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public static implicit operator Complex(int d)
            => new Complex(d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public static implicit operator Complex(float d)
            => new Complex(d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public static implicit operator Complex(double d)
            => new Complex(d);
        #endregion Operators

        #region Static Methods
        /// <summary>
        /// Calcs the absolute value of a complex number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Abs(Complex a)
            => Math.Sqrt(a.Imaginary * a.Imaginary + a.Real * a.Real);

        /// <summary>
        /// Inverts a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Inv(Complex a)
            => new Complex(a.Real / (a.Real * a.Real + a.Imaginary * a.Imaginary),
                -a.Imaginary / (a.Real * a.Real + a.Imaginary * a.Imaginary));

        /// <summary>
        /// Tangent of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Tan(Complex a)
            => Sin(a) / Cos(a);

        /// <summary>
        /// Hyperbolic cosine of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Cosh(Complex a)
            => (Exp(a) + Exp(-a)) / 2;

        /// <summary>
        /// Hyperbolic sine of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Sinh(Complex a)
            => (Exp(a) - Exp(-a)) / 2;

        /// <summary>
        /// Hyperbolic tangent of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Tanh(Complex a)
            => (Exp(2 * a) - 1) / (Exp(2 * a) + 1);

        /// <summary>
        /// Hyperbolic cotangent of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Coth(Complex a)
            => (Exp(2 * a) + 1) / (Exp(2 * a) - 1);

        /// <summary>
        /// Hyperbolic secant of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Sech(Complex a)
            => Inv(Cosh(a));

        /// <summary>
        /// Hyperbolic cosecant of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Csch(Complex a)
            => Inv(Sinh(a));

        /// <summary>
        /// Cotangent of a.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Cot(Complex a)
            => Cos(a) / Sin(a);

        /// <summary>
        /// Computes the conjugation of a complex number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Conj(Complex a)
            => new Complex(a.Real, -a.Imaginary);

        /// <summary>
        /// Complex square root.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Complex Sqrt(double d)
            => (d >= 0) ? new Complex(Math.Sqrt(d)) : new Complex(0, Math.Sqrt(-d));

        /// <summary>
        /// Complex square root.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Sqrt(Complex a)
            => Pow(a, 0.5d);

        /// <summary>
        /// Complex exponential function.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Exp(Complex a)
            => new Complex(Math.Exp(a.Real) * Math.Cos(a.Imaginary), Math.Exp(a.Real) * Math.Sin(a.Imaginary));

        /// <summary>
        /// Main value of the complex logarithm.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// Log[|w|]+I*(Arg[w]+2*Pi*k)
        /// </remarks>
        public static Complex Log(Complex a)
            => new Complex(Math.Log(Abs(a)), Arg(a));

        /// <summary>
        /// Argument of the complex number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double Arg(Complex a)
        {
            if (a.Real < 0)
            {
                if (a.Imaginary < 0)
                    return Math.Atan(a.Imaginary / a.Real) - Math.PI;
                else
                    return Math.PI - Math.Atan(-a.Imaginary / a.Real);
            }
            else
                return Math.Atan(a.Imaginary / a.Real);

        }

        /// <summary>
        /// Complex cosine.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Cos(Complex a)
            => 0.5d * (Exp(I * a) + Exp(-I * a));

        /// <summary>
        /// Complex sine.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Complex Sin(Complex a)
            => (Exp(I * a) - Exp(-I * a)) / (2d * I);

        /// <summary>
        /// The pow.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex Pow(Complex a, Complex b)
            => Exp(b * Log(a));

        /// <summary>
        /// The pow.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex Pow(double a, Complex b)
            => Exp(b * Math.Log(a));

        /// <summary>
        /// The pow.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public static Complex Pow(Complex a, double b)
            => Exp(b * Log(a));
        #endregion Static Methods

        #region Overrides
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
            => obj.ToString() == ToString();

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [SecuritySafeCritical]
        public override int GetHashCode()
            => Real.GetHashCode() ^ Imaginary.GetHashCode();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => ConvertToString(String.Empty, CultureInfo.In­variantCulture);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format)
            => ConvertToString(format, CultureInfo.In­variantCulture);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(String format, IFormatProvider formatProvider)
            => ConvertToString(format, formatProvider);

        /// <summary>
        /// Convert the to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ConvertToString(string format, IFormatProvider formatProvider)
        {
            if (this == Zero) return "0";
            else if (double.IsInfinity(Real) || double.IsInfinity(Imaginary)) return "oo";
            else if (double.IsNaN(Real) || double.IsNaN(Imaginary)) return "?";

            string re, im, sign;

            if (Imaginary < 0)
            {
                sign = Real == 0 ? "-" : " - ";
            }
            else sign = Imaginary > 0 && Real != 0 ? " + " : "";

            re = Real == 0 ? "" : Real.ToString(format, formatProvider);

            if (Imaginary == 0) im = "";
            else im = Imaginary == -1 || Imaginary == 1 ? "i" : Math.Abs(Imaginary).ToString(format, formatProvider) + "i";

            return re + sign + im;
        }
        #endregion Overrides
    }
}
