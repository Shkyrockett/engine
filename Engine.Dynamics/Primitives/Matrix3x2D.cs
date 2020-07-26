// <copyright file="Matrix2x3D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The matrix3x2d struct.
    /// </summary>
    /// <remarks><para>http://referencesource.microsoft.com</para></remarks>
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Matrix3x2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Matrix3x2D
        : IMatrix<Matrix3x2D, Vector2D>
    {
        #region Static Fields
        /// <summary>
        /// An Empty <see cref="Matrix3x2D"/>.
        /// </summary>
        public static readonly Matrix3x2D Empty = new Matrix3x2D(0d, 0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// The na n
        /// </summary>
        public static readonly Matrix3x2D NaN = new Matrix3x2D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// An Identity <see cref="Matrix3x2D"/>.
        /// </summary>
        public static readonly Matrix3x2D Identity = new Matrix3x2D(1d, 0d, 0d, 1d, 0d, 0d) { type = MatrixTypes.Identity };
        #endregion Static Fields

        #region Constants
        /// <summary>
        /// The hash code for a matrix is the xor of its element's hashes.
        /// Since the identity matrix has 2 1's and 4 0's its hash is 0.
        /// </summary>
        private const int c_identityHashCode = 0;
        #endregion Constants

        #region Private Fields
        /// <summary>
        /// The type.
        /// </summary>
        private MatrixTypes type;

        /// <summary>
        /// The m0x0.
        /// </summary>
        private double m0x0;

        /// <summary>
        /// The m0x1.
        /// </summary>
        private double m0x1;

        /// <summary>
        /// The m1x0.
        /// </summary>
        private double m1x0;

        /// <summary>
        /// The m1x1.
        /// </summary>
        private double m1x1;

        /// <summary>
        /// The offset x.
        /// </summary>
        private double offsetX;

        /// <summary>
        /// The offset y.
        /// </summary>
        private double offsetY;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x2D"/> class of the form:<br/>
        /// / m11, m12, 0 \<br/>
        /// | m21, m22, 0 |<br/>
        /// \ offsetX, offsetY, 1 /<br/>
        /// </summary>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3x2D(double m1x1, double m1x2, double m2x1, double m2x2, double offsetX, double offsetY)
        {
            m0x0 = m1x1;
            m0x1 = m1x2;
            m1x0 = m2x1;
            this.m1x1 = m2x2;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            type = MatrixTypes.Unknown;

            // We will detect EXACT identity, scale, translation or
            // scale+translation and use special case algorithms.
            DeriveMatrixType();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the m11.
        /// </summary>
        [DataMember(Name = nameof(M11)), XmlAttribute(nameof(M11)), SoapAttribute(nameof(M11))]
        public double M11
        {
            get
            {
                return type == MatrixTypes.Identity ? 1.0f : m0x0;
            }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(value, 0,
                              0, 1,
                              0, 0,
                              MatrixTypes.Scaling);
                }
                else
                {
                    m0x0 = value;
                    if (type != MatrixTypes.Unknown)
                    {
                        type |= MatrixTypes.Scaling;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the m12.
        /// </summary>
        [DataMember(Name = nameof(M12)), XmlAttribute(nameof(M12)), SoapAttribute(nameof(M12))]
        public double M12
        {
            get
            {
                return type == MatrixTypes.Identity ? 0 : m0x1;
            }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(1, value,
                              0, 1,
                              0, 0,
                              MatrixTypes.Unknown);
                }
                else
                {
                    m0x1 = value;
                    type = MatrixTypes.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets or sets the m21.
        /// </summary>
        [DataMember(Name = nameof(M21)), XmlAttribute(nameof(M21)), SoapAttribute(nameof(M21))]
        public double M21
        {
            get
            {
                return type == MatrixTypes.Identity ? 0 : m1x0;
            }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(1, 0,
                              value, 1,
                              0, 0,
                              MatrixTypes.Unknown);
                }
                else
                {
                    m1x0 = value;
                    type = MatrixTypes.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets or sets the m22.
        /// </summary>
        [DataMember(Name = nameof(M22)), XmlAttribute(nameof(M22)), SoapAttribute(nameof(M22))]
        public double M22
        {
            get { return type == MatrixTypes.Identity ? 1.0f : m1x1; }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(1, 0,
                              0, value,
                              0, 0,
                              MatrixTypes.Scaling);
                }
                else
                {
                    m1x1 = value;
                    if (type != MatrixTypes.Unknown)
                    {
                        type |= MatrixTypes.Scaling;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the offset x.
        /// </summary>
        [DataMember(Name = nameof(OffsetX)), XmlAttribute(nameof(OffsetX)), SoapAttribute(nameof(OffsetX))]
        public double OffsetX
        {
            get
            {
                return type == MatrixTypes.Identity ? 0 : offsetX;
            }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              value, 0,
                              MatrixTypes.Translation);
                }
                else
                {
                    offsetX = value;
                    if (type != MatrixTypes.Unknown)
                    {
                        type |= MatrixTypes.Translation;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the offset y.
        /// </summary>
        [DataMember(Name = nameof(OffsetY)), XmlAttribute(nameof(OffsetY)), SoapAttribute(nameof(OffsetY))]
        public double OffsetY
        {
            get
            {
                return type == MatrixTypes.Identity ? 0 : offsetY;
            }
            set
            {
                if (type == MatrixTypes.Identity)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              0, value,
                              MatrixTypes.Translation);
                }
                else
                {
                    offsetY = value;
                    if (type != MatrixTypes.Unknown)
                    {
                        type |= MatrixTypes.Translation;
                    }
                }
            }
        }

        /// <summary>
        /// Efficient but conservative test for identity.  Returns
        /// true if the matrix is identity.  If it returns false
        /// the matrix may still be identity.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        private bool IsDistinguishedIdentity => type == MatrixTypes.Identity;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsIdentity => type == MatrixTypes.Identity || IsMatrixIdentityEpsilon(m0x0, m0x1, m1x0, m1x1, offsetX, offsetY);

        /// <summary>
        /// HasInverse Property - returns true if this matrix is invert-able, false otherwise.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool HasInverse => !Determinant.IsZero();

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Rows => 3;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Columns => 2;

        /// <summary>
        /// Gets the number of cells in the Matrix.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => Rows * Columns;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Multiply a point by a matrix.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Point2D value, Matrix3x2D matrix) => matrix.Transform(value);

        /// <summary>
        /// Multiplies two transformations.
        /// </summary>
        public static Matrix3x2D operator *(Matrix3x2D trans1, Matrix3x2D trans2)
        {
            MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Compares two Matrix instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        public static bool operator ==(Matrix3x2D matrix1, Matrix3x2D matrix2) => Equals(matrix1, matrix2);

        /// <summary>
        /// Compares two Matrix instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        public static bool operator !=(Matrix3x2D matrix1, Matrix3x2D matrix2) => !Equals(matrix1, matrix2);
        #endregion Operators

        #region Operation Backing Methods
        /// <summary>
        /// Multiply
        /// </summary>
        /// <param name="trans1">The trans1.</param>
        /// <param name="trans2">The trans2.</param>
        /// <returns></returns>
        public static Matrix3x2D Multiply(Matrix3x2D trans1, Matrix3x2D trans2)
        {
            MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Compares two Matrix instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two Matrix instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='matrix1'>The first Matrix to compare</param>
        /// <param name='matrix2'>The second Matrix to compare</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Matrix3x2D matrix1, Matrix3x2D matrix2)
        {
            return matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity
                ? matrix1.IsIdentity == matrix2.IsIdentity
                : matrix1.M11.Equals(matrix2.M11)
                       && matrix1.M12.Equals(matrix2.M12)
                       && matrix1.M21.Equals(matrix2.M21)
                       && matrix1.M22.Equals(matrix2.M22)
                       && matrix1.OffsetX.Equals(matrix2.OffsetX)
                       && matrix1.OffsetY.Equals(matrix2.OffsetY);
        }

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <param name="obj">The object to compare to "this"</param>
        /// <returns>
        /// bool - true if the object is an instance of Matrix and if it's equal to "this".
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Matrix3x2D d && Equals(this, d);

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <param name="value">The Matrix to compare to "this"</param>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix3x2D value) => Equals(this, value);
        #endregion

        #region Factories
        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        internal static Matrix3x2D CreateRotationRadians(double angle)
            => CreateRotationRadians(angle, /* centerX = */ 0, /* centerY = */ 0);

        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        internal static Matrix3x2D CreateRotationRadians(double angle, double centerX, double centerY)
        {
            var matrix = new Matrix3x2D();
            var sin = Sin(angle);
            var cos = Cos(angle);
            var dx = (centerX * (1.0 - cos)) + (centerY * sin);
            var dy = (centerY * (1.0 - cos)) - (centerX * sin);

            matrix.SetMatrix(cos, sin,
                              -sin, cos,
                              dx, dy,
                              MatrixTypes.Unknown);
            return matrix;
        }

        /// <summary>
        /// Creates a scaling transform around the given point
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name='centerX'>The centerX of scaling</param>
        /// <param name='centerY'>The centerY of scaling</param>
        internal static Matrix3x2D CreateScaling(double scaleX, double scaleY, double centerX, double centerY)
        {
            var matrix = new Matrix3x2D();

            matrix.SetMatrix(scaleX, 0,
                             0, scaleY,
                             centerX - (scaleX * centerX), centerY - (scaleY * centerY),
                             MatrixTypes.Scaling | MatrixTypes.Translation);

            return matrix;
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        internal static Matrix3x2D CreateScaling(double scaleX, double scaleY)
        {
            var matrix = new Matrix3x2D();
            matrix.SetMatrix(scaleX, 0,
                             0, scaleY,
                             0, 0,
                             MatrixTypes.Scaling);
            return matrix;
        }

        /// <summary>
        /// Creates a skew transform
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        internal static Matrix3x2D CreateSkewRadians(double skewX, double skewY)
        {
            var matrix = new Matrix3x2D();

            matrix.SetMatrix(1.0f, Tan(skewY),
                             Tan(skewX), 1.0f,
                             0.0f, 0.0f,
                             MatrixTypes.Unknown);

            return matrix;
        }

        /// <summary>
        /// Sets the transformation to the given translation specified by the offset vector.
        /// </summary>
        /// <param name='offsetX'>The offset in X</param>
        /// <param name='offsetY'>The offset in Y</param>
        internal static Matrix3x2D CreateTranslation(double offsetX, double offsetY)
        {
            var matrix = new Matrix3x2D();

            matrix.SetMatrix(1, 0,
                             0, 1,
                             offsetX, offsetY,
                             MatrixTypes.Translation);

            return matrix;
        }

        /// <summary>
        /// Parse a string for a <see cref="Matrix3x2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix3x2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix3x2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix3x2D Parse(string source)
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            var tokenizer = new Tokenizer(source, formatProvider);
            var firstToken = tokenizer.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a simple string compare.
            var value = firstToken == nameof(Identity) ? Identity : new Matrix3x2D(
                Convert.ToDouble(firstToken, formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Mutators
        /// <summary>
        /// TransformRect - Internal helper for perf
        /// </summary>
        /// <param name="rect"> The Rectangle to transform. </param>
        /// <param name="matrix"> The Matrix with which to transform the Rectangle. </param>
        public static void TransformRect(ref Rectangle2D rect, ref Matrix3x2D matrix)
        {
            if (rect.IsEmpty)
            {
                return;
            }

            var matrixType = matrix.type;

            // If the matrix is identity, don't worry.
            if (matrixType == MatrixTypes.Identity)
            {
                return;
            }

            // Scaling
            if (0 != (matrixType & MatrixTypes.Scaling))
            {
                rect.X *= matrix.m0x0;
                rect.Y *= matrix.m1x1;
                rect.Width *= matrix.m0x0;
                rect.Height *= matrix.m1x1;

                // Ensure the width is always positive.  For example, if there was a reflection about the
                // y axis followed by a translation into the visual area, the width could be negative.
                if (rect.Width < 0.0)
                {
                    rect.X += rect.Width;
                    rect.Width = -rect.Width;
                }

                // Ensure the height is always positive.  For example, if there was a reflection about the
                // x axis followed by a translation into the visual area, the height could be negative.
                if (rect.Height < 0.0)
                {
                    rect.Y += rect.Height;
                    rect.Height = -rect.Height;
                }
            }

            // Translation
            if (0 != (matrixType & MatrixTypes.Translation))
            {
                // X
                rect.X += matrix.offsetX;

                // Y
                rect.Y += matrix.offsetY;
            }

            if (matrixType == MatrixTypes.Unknown)
            {
                // Al Bunny implementation.
                var point0 = matrix.Transform(rect.TopLeft);
                var point1 = matrix.Transform(rect.TopRight);
                var point2 = matrix.Transform(rect.BottomRight);
                var point3 = matrix.Transform(rect.BottomLeft);

                // Width and height is always positive here.
                rect.X = Min(Min(point0.X, point1.X), Min(point2.X, point3.X));
                rect.Y = Min(Min(point0.Y, point1.Y), Min(point2.Y, point3.Y));

                rect.Width = Max(Max(point0.X, point1.X), Max(point2.X, point3.X)) - rect.X;
                rect.Height = Max(Max(point0.Y, point1.Y), Max(point2.Y, point3.Y)) - rect.Y;
            }
        }

        /// <summary>
        /// Multiplies two transformations, where the behavior is matrix1 *= matrix2.
        /// This code exists so that we can efficient combine matrices without copying
        /// the data around, since each matrix is 52 bytes.
        /// To reduce duplication and to ensure consistent behavior, this is the
        /// method which is used to implement Matrix * Matrix as well.
        /// </summary>
        public static void MultiplyMatrix(ref Matrix3x2D matrix1, ref Matrix3x2D matrix2)
        {
            var type1 = matrix1.type;
            var type2 = matrix2.type;

            // Check for identities

            // If the second is identities, we can just return
            if (type2 == MatrixTypes.Identity)
            {
                return;
            }

            // If the first is identities, we can just copy the memory across.
            if (type1 == MatrixTypes.Identity)
            {
                matrix1 = matrix2;
                return;
            }

            // Optimize for translate case, where the second is a translate
            if (type2 == MatrixTypes.Translation)
            {
                // 2 additions
                matrix1.offsetX += matrix2.offsetX;
                matrix1.offsetY += matrix2.offsetY;

                // If matrix 1 wasn't unknown we added a translation
                if (type1 != MatrixTypes.Unknown)
                {
                    matrix1.type |= MatrixTypes.Translation;
                }

                return;
            }

            // Check for the first value being a translate
            if (type1 == MatrixTypes.Translation)
            {
                // Save off the old offsets
                var offsetX = matrix1.offsetX;
                var offsetY = matrix1.offsetY;

                // Copy the matrix
                matrix1 = matrix2;

                matrix1.offsetX = (float)((offsetX * matrix2.m0x0) + (offsetY * matrix2.m1x0) + matrix2.offsetX);
                matrix1.offsetY = (float)((offsetX * matrix2.m0x1) + (offsetY * matrix2.m1x1) + matrix2.offsetY);

                if (type2 == MatrixTypes.Unknown)
                {
                    matrix1.type = MatrixTypes.Unknown;
                }
                else
                {
                    matrix1.type = MatrixTypes.Scaling | MatrixTypes.Translation;
                }

                return;
            }

            // The following code combines the type of the transformations so that the high nibble
            // is "this"'s type, and the low nibble is mat's type.  This allows for a switch rather
            // than nested switches.

            // trans1._type |  trans2._type
            //  7  6  5  4   |  3  2  1  0
            var combinedType = ((int)type1 << 4) | (int)type2;

            switch (combinedType)
            {
                case 34:  // S * S
                    // 2 multiplications
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    return;

                case 35:  // S * S|T
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX = matrix2.offsetX;
                    matrix1.offsetY = matrix2.offsetY;

                    // Transform set to Translate and Scale
                    matrix1.type = MatrixTypes.Translation | MatrixTypes.Scaling;
                    return;

                case 50: // S|T * S
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX *= matrix2.m0x0;
                    matrix1.offsetY *= matrix2.m1x1;
                    return;

                case 51: // S|T * S|T
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX = (matrix2.m0x0 * matrix1.offsetX) + matrix2.offsetX;
                    matrix1.offsetY = (matrix2.m1x1 * matrix1.offsetY) + matrix2.offsetY;
                    return;
                case 36: // S * U
                case 52: // S|T * U
                case 66: // U * S
                case 67: // U * S|T
                case 68: // U * U
                    matrix1 = new Matrix3x2D(
                        (matrix1.m0x0 * matrix2.m0x0) + (matrix1.m0x1 * matrix2.m1x0),
                        (matrix1.m0x0 * matrix2.m0x1) + (matrix1.m0x1 * matrix2.m1x1),

                        (matrix1.m1x0 * matrix2.m0x0) + (matrix1.m1x1 * matrix2.m1x0),
                        (matrix1.m1x0 * matrix2.m0x1) + (matrix1.m1x1 * matrix2.m1x1),

                        (matrix1.offsetX * matrix2.m0x0) + (matrix1.offsetY * matrix2.m1x0) + matrix2.offsetX,
                        (matrix1.offsetX * matrix2.m0x1) + (matrix1.offsetY * matrix2.m1x1) + matrix2.offsetY);
                    return;
#if DEBUG
                default:
                    Debug.Fail("Matrix multiply hit an invalid case: " + combinedType);
                    break;
#endif
            }
        }

        /// <summary>
        /// Multiplies two transformations, where the behavior is matrix1 *= matrix2.
        /// This code exists so that we can efficient combine matrices without copying
        /// the data around, since each matrix is 52 bytes.
        /// To reduce duplication and to ensure consistent behavior, this is the
        /// method which is used to implement Matrix * Matrix as well.
        /// </summary>
        public static Matrix3x2D MultiplyMatrix(Matrix3x2D matrix1, Matrix3x2D matrix2)
        {
            var type1 = matrix1.type;
            var type2 = matrix2.type;

            // Check for identities

            // If the second is identities, we can just return
            if (type2 == MatrixTypes.Identity)
            {
                return matrix1;
            }

            // If the first is identities, we can just copy the memory across.
            if (type1 == MatrixTypes.Identity)
            {
                return matrix2;
            }

            // Optimize for translate case, where the second is a translate
            if (type2 == MatrixTypes.Translation)
            {
                // 2 additions
                matrix1.offsetX += matrix2.offsetX;
                matrix1.offsetY += matrix2.offsetY;

                // If matrix 1 wasn't unknown we added a translation
                if (type1 != MatrixTypes.Unknown)
                {
                    matrix1.type |= MatrixTypes.Translation;
                }

                return matrix1;
            }

            // Check for the first value being a translate
            if (type1 == MatrixTypes.Translation)
            {
                // Save off the old offsets
                var offsetX = matrix1.offsetX;
                var offsetY = matrix1.offsetY;

                // Copy the matrix
                matrix1 = matrix2;

                matrix1.offsetX = (float)((offsetX * matrix2.m0x0) + (offsetY * matrix2.m1x0) + matrix2.offsetX);
                matrix1.offsetY = (float)((offsetX * matrix2.m0x1) + (offsetY * matrix2.m1x1) + matrix2.offsetY);

                if (type2 == MatrixTypes.Unknown)
                {
                    matrix1.type = MatrixTypes.Unknown;
                }
                else
                {
                    matrix1.type = MatrixTypes.Scaling | MatrixTypes.Translation;
                }

                return matrix1;
            }

            // The following code combines the type of the transformations so that the high nibble
            // is "this"'s type, and the low nibble is mat's type.  This allows for a switch rather
            // than nested switches.

            // trans1._type |  trans2._type
            //  7  6  5  4   |  3  2  1  0
            var combinedType = ((int)type1 << 4) | (int)type2;

            switch (combinedType)
            {
                case 34:  // S * S
                    // 2 multiplications
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    return matrix1;

                case 35:  // S * S|T
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX = matrix2.offsetX;
                    matrix1.offsetY = matrix2.offsetY;

                    // Transform set to Translate and Scale
                    matrix1.type = MatrixTypes.Translation | MatrixTypes.Scaling;
                    return matrix1;

                case 50: // S|T * S
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX *= matrix2.m0x0;
                    matrix1.offsetY *= matrix2.m1x1;
                    return matrix1;

                case 51: // S|T * S|T
                    matrix1.m0x0 *= matrix2.m0x0;
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.offsetX = (matrix2.m0x0 * matrix1.offsetX) + matrix2.offsetX;
                    matrix1.offsetY = (matrix2.m1x1 * matrix1.offsetY) + matrix2.offsetY;
                    return matrix1;
                case 36: // S * U
                case 52: // S|T * U
                case 66: // U * S
                case 67: // U * S|T
                case 68: // U * U
                    matrix1 = new Matrix3x2D(
                        (matrix1.m0x0 * matrix2.m0x0) + (matrix1.m0x1 * matrix2.m1x0),
                        (matrix1.m0x0 * matrix2.m0x1) + (matrix1.m0x1 * matrix2.m1x1),

                        (matrix1.m1x0 * matrix2.m0x0) + (matrix1.m1x1 * matrix2.m1x0),
                        (matrix1.m1x0 * matrix2.m0x1) + (matrix1.m1x1 * matrix2.m1x1),

                        (matrix1.offsetX * matrix2.m0x0) + (matrix1.offsetY * matrix2.m1x0) + matrix2.offsetX,
                        (matrix1.offsetX * matrix2.m0x1) + (matrix1.offsetY * matrix2.m1x1) + matrix2.offsetY);
                    return matrix1;
#if DEBUG
                default:
                    Debug.Fail("Matrix multiply hit an invalid case: " + combinedType);
                    break;
#endif
            }

            return matrix1;
        }

        /// <summary>
        /// Applies an offset to the specified matrix in place.
        /// </summary>
        public static void PrependOffset(ref Matrix3x2D matrix, double offsetX, double offsetY)
        {
            if (matrix.type == MatrixTypes.Identity)
            {
                matrix = new Matrix3x2D(1, 0, 0, 1, offsetX, offsetY)
                {
                    type = MatrixTypes.Translation
                };
            }
            else
            {
                //
                //  / 1   0   0 \       / m11   m12   0 \
                //  | 0   1   0 |   *   | m21   m22   0 |
                //  \ tx  ty  1 /       \  ox    oy   1 /
                //
                //       /   m11                  m12                     0 \
                //  =    |   m21                  m22                     0 |
                //       \   m11*tx+m21*ty+ox     m12*tx + m22*ty + oy    1 /
                //

                matrix.offsetX += (matrix.m0x0 * offsetX) + (matrix.m1x0 * offsetY);
                matrix.offsetY += (matrix.m0x1 * offsetX) + (matrix.m1x1 * offsetY);

                // It just gained a translate if was a scale transform. Identity transform is handled above.
                Debug.Assert(matrix.type != MatrixTypes.Identity);
                if (matrix.type != MatrixTypes.Unknown)
                {
                    matrix.type |= MatrixTypes.Translation;
                }
            }
        }

        /// <summary>
        /// Append - "this" becomes this * matrix, the same as this *= matrix.
        /// </summary>
        /// <param name="matrix"> The Matrix to append to this Matrix </param>
        public void Append(Matrix3x2D matrix) => this *= matrix;

        /// <summary>
        /// Prepend - "this" becomes matrix * this, the same as this = matrix * this.
        /// </summary>
        /// <param name="matrix"> The Matrix to prepend to this Matrix </param>
        public void Prepend(Matrix3x2D matrix) => this = matrix * this;

        /// <summary>
        /// Rotates this matrix about the origin
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void Rotate(double angle) => this *= CreateRotationRadians(Operations.WrapAngle(angle));

        /// <summary>
        /// Prepends a rotation about the origin to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void RotatePrepend(double angle) => this = CreateRotationRadians(Operations.WrapAngle(angle)) * this;

        /// <summary>
        /// Rotates this matrix about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAt(double angle, double centerX, double centerY) => this *= CreateRotationRadians(Operations.WrapAngle(angle), centerX, centerY);

        /// <summary>
        /// Prepends a rotation about the given point to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAtPrepend(double angle, double centerX, double centerY) => this = CreateRotationRadians(Operations.WrapAngle(angle), centerX, centerY) * this;

        /// <summary>
        /// Scales this matrix around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void Scale(double scaleX, double scaleY) => this *= CreateScaling(scaleX, scaleY);

        /// <summary>
        /// Prepends a scale around the origin to "this"
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void ScalePrepend(double scaleX, double scaleY) => this = CreateScaling(scaleX, scaleY) * this;

        /// <summary>
        /// Scales this matrix around the center provided
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name="centerX">The centerX about which to scale</param>
        /// <param name="centerY">The centerY about which to scale</param>
        public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY) => this *= CreateScaling(scaleX, scaleY, centerX, centerY);

        /// <summary>
        /// Prepends a scale around the center provided to "this"
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name="centerX">The centerX about which to scale</param>
        /// <param name="centerY">The centerY about which to scale</param>
        public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY) => this = CreateScaling(scaleX, scaleY, centerX, centerY) * this;

        /// <summary>
        /// Skews this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in radians</param>
        /// <param name='skewY'>The skew angle in the y dimension in radians</param>
        public void Skew(double skewX, double skewY) => this *= CreateSkewRadians(Operations.WrapAngle(skewX), Operations.WrapAngle(skewY));

        /// <summary>
        /// Prepends a skew to this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in radians</param>
        /// <param name='skewY'>The skew angle in the y dimension in radians</param>
        public void SkewPrepend(double skewX, double skewY) => this = CreateSkewRadians(Operations.WrapAngle(skewX), Operations.WrapAngle(skewY)) * this;

        /// <summary>
        /// Translates this matrix
        /// </summary>
        /// <param name='offsetX'>The offset in the x dimension</param>
        /// <param name='offsetY'>The offset in the y dimension</param>
        public void Translate(double offsetX, double offsetY)
        {
            //
            // / a b 0 \   / 1 0 0 \    / a      b       0 \
            // | c d 0 | * | 0 1 0 | = |  c      d       0 |
            // \ e f 1 /   \ x y 1 /    \ e+x    f+y     1 /
            //
            // (where e = _offsetX and f == _offsetY)
            //

            if (type == MatrixTypes.Identity)
            {
                // Values would be incorrect if matrix was created using default constructor.
                // or if SetIdentity was called on a matrix which had values.
                //
                SetMatrix(1, 0,
                          0, 1,
                          offsetX, offsetY,
                          MatrixTypes.Translation);
            }
            else if (type == MatrixTypes.Unknown)
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;
            }
            else
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;

                // If matrix wasn't unknown we added a translation
                type |= MatrixTypes.Translation;
            }
        }

        /// <summary>
        /// Prepends a translation to this matrix
        /// </summary>
        /// <param name='offsetX'>The offset in the x dimension</param>
        /// <param name='offsetY'>The offset in the y dimension</param>
        public void TranslatePrepend(double offsetX, double offsetY) => this = CreateTranslation(offsetX, offsetY) * this;

        /// <summary>
        /// Transform - Transforms each Vector in the array by this matrix.
        /// </summary>
        /// <param name="vectors"> The Vector array to transform </param>
        public void Transform(Vector2D[] vectors)
        {
            if (vectors is not null)
            {
                for (var i = 0; i < vectors.Length; i++)
                {
                    MultiplyVector(ref vectors[i]);
                }
            }
        }

        /// <summary>
        /// Transform - Transforms each point in the array by this matrix
        /// </summary>
        /// <param name="points"> The Point array to transform </param>
        public void Transform(Point2D[] points)
        {
            if (points is not null)
            {
                for (var i = 0; i < points.Length; i++)
                {
                    MultiplyPoint(ref points[i]);
                }
            }
        }

        /// <summary>
        /// Replaces matrix with the inverse of the transformation.  This will throw an InvalidOperationException
        /// if !HasInverse
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This will throw an InvalidOperationException if the matrix is non-invert-able
        /// </exception>
        public void Invert()
        {
            var determinant = Determinant;

            if (determinant.IsZero())
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.Transform_NotInvertible));
                throw new InvalidOperationException();
            }

            // Inversion does not change the type of a matrix.
            switch (type)
            {
                case MatrixTypes.Identity:
                    break;
                case MatrixTypes.Scaling:
                    {
                        m0x0 = 1.0f / m0x0;
                        m1x1 = 1.0f / m1x1;
                    }
                    break;
                case MatrixTypes.Translation:
                    offsetX = -offsetX;
                    offsetY = -offsetY;
                    break;
                case MatrixTypes.Scaling | MatrixTypes.Translation:
                    {
                        m0x0 = 1.0f / m0x0;
                        m1x1 = 1.0f / m1x1;
                        offsetX = -offsetX * m0x0;
                        offsetY = -offsetY * m1x1;
                    }
                    break;
                default:
                    {
                        var invdet = 1.0f / determinant;
                        SetMatrix(m1x1 * invdet,
                                  -m0x1 * invdet,
                                  -m1x0 * invdet,
                                  m0x0 * invdet,
                                  ((m1x0 * offsetY) - (offsetX * m1x1)) * invdet,
                                  ((offsetX * m0x1) - (m0x0 * offsetY)) * invdet,
                                  MatrixTypes.Unknown);
                    }
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        public void MultiplyVector(ref double x, ref double y)
        {
            switch (type)
            {
                case MatrixTypes.Identity:
                case MatrixTypes.Translation:
                    return;
                case MatrixTypes.Scaling:
                case MatrixTypes.Scaling | MatrixTypes.Translation:
                    x *= m0x0;
                    y *= m1x1;
                    break;
                default:
                    var xadd = y * m1x0;
                    var yadd = x * m0x1;
                    x *= m0x0;
                    x += xadd;
                    y *= m1x1;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        public void MultiplyVector(ref Vector2D vector)
        {
            switch (type)
            {
                case MatrixTypes.Identity:
                case MatrixTypes.Translation:
                    return;
                case MatrixTypes.Scaling:
                case MatrixTypes.Scaling | MatrixTypes.Translation:
                    vector.I *= m0x0;
                    vector.J *= m1x1;
                    break;
                default:
                    var xadd = vector.J * m1x0;
                    var yadd = vector.I * m0x1;
                    vector.I *= m0x0;
                    vector.I += xadd;
                    vector.J *= m1x1;
                    vector.J += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        public void MultiplyPoint(ref double x, ref double y)
        {
            switch (type)
            {
                case MatrixTypes.Identity:
                    return;
                case MatrixTypes.Translation:
                    x += offsetX;
                    y += offsetY;
                    return;
                case MatrixTypes.Scaling:
                    x *= m0x0;
                    y *= m1x1;
                    return;
                case MatrixTypes.Scaling | MatrixTypes.Translation:
                    x *= m0x0;
                    x += offsetX;
                    y *= m1x1;
                    y += offsetY;
                    break;
                default:
                    var xadd = (y * m1x0) + offsetX;
                    var yadd = (x * m0x1) + offsetY;
                    x *= m0x0;
                    x += xadd;
                    y *= m1x1;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        public void MultiplyPoint(ref Point2D point)
        {
            switch (type)
            {
                case MatrixTypes.Identity:
                    return;
                case MatrixTypes.Translation:
                    point.X += offsetX;
                    point.Y += offsetY;
                    return;
                case MatrixTypes.Scaling:
                    point.X *= m0x0;
                    point.Y *= m1x1;
                    return;
                case MatrixTypes.Scaling | MatrixTypes.Translation:
                    point.X *= m0x0;
                    point.X += offsetX;
                    point.Y *= m1x1;
                    point.Y += offsetY;
                    break;
                default:
                    var xadd = (point.Y * m1x0) + offsetX;
                    var yadd = (point.X * m0x1) + offsetY;
                    point.X *= m0x0;
                    point.X += xadd;
                    point.Y *= m1x1;
                    point.Y += yadd;
                    break;
            }
        }

        ///<summary>
        /// Sets the transform to
        ///             / m11, m12, 0 \
        ///             | m21, m22, 0 |
        ///             \ offsetX, offsetY, 1 /
        /// where offsetX, offsetY is the translation.
        ///</summary>
        private void SetMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY, MatrixTypes type)
        {
            m0x0 = m11;
            m0x1 = m12;
            m1x0 = m21;
            m1x1 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.type = type;
        }

        /// <summary>
        /// Set the type of the matrix based on its current contents
        /// </summary>
        private void DeriveMatrixType()
        {
            type = 0;

            // Now classify our matrix.
            if (!(Abs(m1x0) < double.Epsilon && Abs(m0x1) < double.Epsilon))
            {
                type = MatrixTypes.Unknown;
                return;
            }
            else if (0 == (type & (MatrixTypes.Translation | MatrixTypes.Scaling)))
            {
                // We have an identity matrix.
                type = MatrixTypes.Identity;
                return;
            }
            else if (!(Abs(m0x0 - 1) < double.Epsilon && Abs(m1x1 - 1) < double.Epsilon))
            {
                type = MatrixTypes.Scaling;
            }

            if (!(Abs(offsetX) < double.Epsilon && Abs(offsetY) < double.Epsilon))
            {
                type |= MatrixTypes.Translation;
            }
            return;
        }
        #endregion Mutators

        #region Methods
        /// <summary>
        /// Transform - returns the result of transforming the point by this matrix
        /// </summary>
        /// <returns>
        /// The transformed point
        /// </returns>
        /// <param name="point"> The Point to transform </param>
        public Point2D Transform(Point2D point)
        {
            var newPoint = point;
            MultiplyPoint(ref newPoint);
            return newPoint;
        }

        /// <summary>
        /// Transform - returns the result of transforming the Vector by this matrix.
        /// </summary>
        /// <returns>
        /// The transformed vector
        /// </returns>
        /// <param name="vector"> The Vector to transform </param>
        public Vector2D Transform(Vector2D vector)
        {
            var newVector = vector;
            MultiplyVector(ref newVector);
            return newVector;
        }

        /// <summary>
        /// The determinant of this matrix
        /// </summary>
        public double Determinant => type switch
        {
            MatrixTypes.Identity or MatrixTypes.Translation => 1.0d,
            MatrixTypes.Scaling or MatrixTypes.Scaling | MatrixTypes.Translation => m0x0 * m1x1,
            _ => (m0x0 * m1x1) - (m0x1 * m1x0),
        };
        #endregion

        #region Standard Methods
        /// <returns></returns>
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<IEnumerable<double>> GetEnumerator() => new List<List<double>>
            {
                new List<double> { m0x0, m0x1, offsetX },
                new List<double> { m1x0, m1x1, offsetY },
            }.GetEnumerator();

        /// <returns></returns>
        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            if (IsDistinguishedIdentity)
            {
                return c_identityHashCode;
            }
            else
            {
                // Perform field-by-field XOR of HashCodes
                return M11.GetHashCode()
                       ^ M12.GetHashCode()
                       ^ M21.GetHashCode()
                       ^ M22.GetHashCode()
                       ^ OffsetX.GetHashCode()
                       ^ OffsetY.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider) => ToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix3x2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Matrix3x2D);
            if (IsIdentity) return nameof(Identity);
            // Helper to get the numeric list separator for a given culture.
            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            IFormattable formatable = $"{nameof(Matrix3x2D)}{{{nameof(M11)}={m0x0}{sep}{nameof(M12)}={m0x1}{sep}{nameof(M21)}={m1x0}{sep}{nameof(M22)}={m1x1}{sep}{nameof(OffsetX)}={offsetX}{sep}{nameof(OffsetY)}={offsetY}}}";
            return formatable.ToString(format, formatProvider);
        }
        #endregion Methods
    }
}
