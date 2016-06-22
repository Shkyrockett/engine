using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Math;
using static Engine.Maths;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://referencesource.microsoft.com</remarks>
    [Serializable]
    [ComVisible(true)]
    //[DisplayName(nameof(Matrix2D))]
    [TypeConverter(typeof(Matrix2DConverter))]
    public partial struct Matrix2D
        : IFormattable
    {
        #region Static Implementations

        /// <summary>
        /// An Empty <see cref="Matrix2D"/>.
        /// </summary>
        public static readonly Matrix2D Empty = new Matrix2D();

        /// <summary>
        /// An Identity <see cref="Matrix2D"/>.
        /// </summary>
        public static readonly Matrix2D Identity = CreateIdentity();

        #endregion

        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private MatrixTypes type;

        /// <summary>
        /// 
        /// </summary>
        private double m1x1;

        /// <summary>
        /// 
        /// </summary>
        private double m1x2;

        /// <summary>
        /// 
        /// </summary>
        private double m2x1;

        /// <summary>
        /// 
        /// </summary>
        private double m2x2;

        /// <summary>
        /// 
        /// </summary>
        private double offsetX;

        /// <summary>
        /// 
        /// </summary>
        private double offsetY;

#pragma warning disable CS0414
        /// <summary>
        /// This field is only used by unmanaged code which isn't detected by the compiler.
        /// Matrix in blt'd to unmanaged code, so this is padding 
        /// to align structure.
        /// </summary>
        private int padding;
#pragma warning restore CS0414

        #endregion

        #region Constants

        /// <summary>
        /// The hash code for a matrix is the xor of its element's hashes.
        /// Since the identity matrix has 2 1's and 4 0's its hash is 0.
        /// </summary>
        private const int c_identityHashCode = 0;

        #endregion

        #region Constructors

        ///// <summary>
        ///// 
        ///// </summary>
        //public Matrix2D()
        //    : this(0,0,0,0,0,0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2D"/> class of the form:<br/>
        /// / m11, m12, 0 \<br/>
        /// | m21, m22, 0 |<br/>
        /// \ offsetX, offsetY, 1 /<br/>
        /// </summary>
        public Matrix2D(double m1x1, double m1x2, double m2x1, double m2x2, double offsetX, double offsetY)
        {
            this.m1x1 = m1x1;
            this.m1x2 = m1x2;
            this.m2x1 = m2x1;
            this.m2x2 = m2x2;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            type = MatrixTypes.UNKNOWN;
            padding = 0;

            // We will detect EXACT identity, scale, translation or
            // scale+translation and use special case algorithms.
            DeriveMatrixType();
        }

        #endregion

        #region Properties

        /// <summary>
        /// M11
        /// </summary>
        public double M11
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 1.0f;
                else
                    return m1x1;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(value, 0,
                              0, 1,
                              0, 0,
                              MatrixTypes.SCALING);
                }
                else
                {
                    m1x1 = value;
                    if (type != MatrixTypes.UNKNOWN)
                        type |= MatrixTypes.SCALING;
                }
            }
        }

        /// <summary>
        /// M12
        /// </summary>
        public double M12
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 0;
                return m1x2;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, value,
                              0, 1,
                              0, 0,
                              MatrixTypes.UNKNOWN);
                }
                else
                {
                    m1x2 = value;
                    type = MatrixTypes.UNKNOWN;
                }
            }
        }

        /// <summary>
        /// M22
        /// </summary>
        public double M21
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 0;
                else
                    return m2x1;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              value, 1,
                              0, 0,
                              MatrixTypes.UNKNOWN);
                }
                else
                {
                    m2x1 = value;
                    type = MatrixTypes.UNKNOWN;
                }
            }
        }

        /// <summary>
        /// M22
        /// </summary>
        public double M22
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 1.0f;
                else
                    return m2x2;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, value,
                              0, 0,
                              MatrixTypes.SCALING);
                }
                else
                {
                    m2x2 = value;
                    if (type != MatrixTypes.UNKNOWN)
                        type |= MatrixTypes.SCALING;
                }
            }
        }

        /// <summary>
        /// OffsetX
        /// </summary>
        public double OffsetX
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 0;
                else
                    return offsetX;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              value, 0,
                              MatrixTypes.TRANSLATION);
                }
                else
                {
                    offsetX = value;
                    if (type != MatrixTypes.UNKNOWN)
                        type |= MatrixTypes.TRANSLATION;
                }
            }
        }

        /// <summary>
        /// OffsetY
        /// </summary>
        public double OffsetY
        {
            get
            {
                if (type == MatrixTypes.IDENTITY)
                    return 0;
                else
                    return offsetY;
            }
            set
            {
                if (type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              0, value,
                              MatrixTypes.TRANSLATION);
                }
                else
                {
                    offsetY = value;
                    if (type != MatrixTypes.UNKNOWN)
                        type |= MatrixTypes.TRANSLATION;
                }
            }
        }

        /// <summary>
        /// Efficient but conservative test for identity.  Returns
        /// true if the matrix is identity.  If it returns false
        /// the matrix may still be identity.
        /// </summary>
        private bool IsDistinguishedIdentity => type == MatrixTypes.IDENTITY;

        /// <summary>
        /// Sets the transformation to the identity.
        /// </summary>
        private static Matrix2D CreateIdentity()
        {
            var matrix = new Matrix2D();
            matrix.SetMatrix(1, 0,
                             0, 1,
                             0, 0,
                             MatrixTypes.IDENTITY);
            return matrix;
        }

        /// <summary>
        /// Sets the matrix to identity.
        /// </summary>
        public void SetIdentity()
        {
            type = MatrixTypes.IDENTITY;
        }

        /// <summary>
        /// Tests whether or not a given transform is an identity transform
        /// </summary>
        public bool IsIdentity
            => (type == MatrixTypes.IDENTITY ||
        (
            Abs(m1x1 - 1) < DoubleEpsilon
            && Abs(m1x2) < DoubleEpsilon
            && Abs(m2x1) < DoubleEpsilon
            && Abs(m2x2 - 1) < DoubleEpsilon
            && Abs(offsetX) < DoubleEpsilon
            && Abs(offsetY) < DoubleEpsilon));

        /// <summary>
        /// HasInverse Property - returns true if this matrix is invert-able, false otherwise.
        /// </summary>
        public bool HasInverse => !Determinant.IsZero();

        #endregion

        #region Operators

        /// <summary>
        /// Operator Point * Matrix
        /// </summary>
        public static Point2D operator *(Point2D point, Matrix2D matrix) => matrix.Transform(point);

        /// <summary>
        /// Multiplies two transformations.
        /// </summary>
        public static Matrix2D operator *(Matrix2D trans1, Matrix2D trans2)
        {
            MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Multiply
        /// </summary>
        public static Matrix2D Multiply(Matrix2D trans1, Matrix2D trans2)
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
        public static bool operator ==(Matrix2D matrix1, Matrix2D matrix2) => Equals(matrix1, matrix2);

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
        public static bool operator !=(Matrix2D matrix1, Matrix2D matrix2) => !Equals(matrix1, matrix2);

        /// <summary>
        /// Compares two Matrix2D
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Matrix2D a, Matrix2D b) => Equals(a, b);

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
        public static bool Equals(Matrix2D matrix1, Matrix2D matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
            {
                return matrix1.IsIdentity == matrix2.IsIdentity;
            }
            else
            {
                return matrix1.M11.Equals(matrix2.M11) &&
                       matrix1.M12.Equals(matrix2.M12) &&
                       matrix1.M21.Equals(matrix2.M21) &&
                       matrix1.M22.Equals(matrix2.M22) &&
                       matrix1.OffsetX.Equals(matrix2.OffsetX) &&
                       matrix1.OffsetY.Equals(matrix2.OffsetY);
            }
        }

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of Matrix and if it's equal to "this".
        /// </returns>
        /// <param name='obj'>The object to compare to "this"</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Matrix2D && Equals(this, (Matrix2D)obj);

        /// <summary>
        /// Equals - compares this Matrix with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The Matrix to compare to "this"</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix2D value) => Equals(this, value);

        #endregion

        #region Factories

        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        internal static Matrix2D CreateRotationRadians(double angle) => CreateRotationRadians(angle, /* centerX = */ 0, /* centerY = */ 0);

        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        internal static Matrix2D CreateRotationRadians(double angle, double centerX, double centerY)
        {
            var matrix = new Matrix2D();
            double sin = Sin(angle);
            double cos = Cos(angle);
            double dx = ((centerX * (1.0 - cos)) + (centerY * sin));
            double dy = ((centerY * (1.0 - cos)) - (centerX * sin));

            matrix.SetMatrix(cos, sin,
                              -sin, cos,
                              dx, dy,
                              MatrixTypes.UNKNOWN);
            return matrix;
        }

        /// <summary>
        /// Creates a scaling transform around the given point
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name='centerX'>The centerX of scaling</param>
        /// <param name='centerY'>The centerY of scaling</param>
        internal static Matrix2D CreateScaling(double scaleX, double scaleY, double centerX, double centerY)
        {
            var matrix = new Matrix2D();

            matrix.SetMatrix(scaleX, 0,
                             0, scaleY,
                             centerX - scaleX * centerX, centerY - scaleY * centerY,
                             MatrixTypes.SCALING | MatrixTypes.TRANSLATION);

            return matrix;
        }

        /// <summary>
        /// Creates a scaling transform around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        internal static Matrix2D CreateScaling(double scaleX, double scaleY)
        {
            var matrix = new Matrix2D();
            matrix.SetMatrix(scaleX, 0,
                             0, scaleY,
                             0, 0,
                             MatrixTypes.SCALING);
            return matrix;
        }

        /// <summary>
        /// Creates a skew transform
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        internal static Matrix2D CreateSkewRadians(double skewX, double skewY)
        {
            var matrix = new Matrix2D();

            matrix.SetMatrix(1.0f, Tan(skewY),
                             Tan(skewX), 1.0f,
                             0.0f, 0.0f,
                             MatrixTypes.UNKNOWN);

            return matrix;
        }

        /// <summary>
        /// Sets the transformation to the given translation specified by the offset vector.
        /// </summary>
        /// <param name='offsetX'>The offset in X</param>
        /// <param name='offsetY'>The offset in Y</param>
        internal static Matrix2D CreateTranslation(double offsetX, double offsetY)
        {
            var matrix = new Matrix2D();

            matrix.SetMatrix(1, 0,
                             0, 1,
                             offsetX, offsetY,
                             MatrixTypes.TRANSLATION);

            return matrix;
        }

        /// <summary>
        /// Parse a string for a <see cref="Matrix2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Matrix2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Matrix2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Matrix2D Parse(string source)
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;
            var tokenizer = new Tokenizer(source, formatProvider);
            Matrix2D value;
            string firstToken = tokenizer.NextTokenRequired();
            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Identity")
            {
                value = Identity;
            }
            else
            {
                value = new Matrix2D(
                    firstToken.ParseFloat(formatProvider),
                    tokenizer.NextTokenRequired().ParseFloat(formatProvider),
                    tokenizer.NextTokenRequired().ParseFloat(formatProvider),
                    tokenizer.NextTokenRequired().ParseFloat(formatProvider),
                    tokenizer.NextTokenRequired().ParseFloat(formatProvider),
                    tokenizer.NextTokenRequired().ParseFloat(formatProvider));
            }
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Mutators

        /// <summary>
        /// TransformRect - Internal helper for perf
        /// </summary>
        /// <param name="rect"> The Rectangle to transform. </param>
        /// <param name="matrix"> The Matrix with which to transform the Rectangle. </param>
        internal static void TransformRect(ref Rectangle2D rect, ref Matrix2D matrix)
        {
            if (rect.IsEmpty)
                return;

            MatrixTypes matrixType = matrix.type;

            // If the matrix is identity, don't worry.
            if (matrixType == MatrixTypes.IDENTITY)
                return;

            // Scaling
            if (0 != (matrixType & MatrixTypes.SCALING))
            {
                rect.X *= matrix.m1x1;
                rect.Y *= matrix.m2x2;
                rect.Width *= matrix.m1x1;
                rect.Height *= matrix.m2x2;

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
            if (0 != (matrixType & MatrixTypes.TRANSLATION))
            {
                // X
                rect.X += matrix.offsetX;

                // Y
                rect.Y += matrix.offsetY;
            }

            if (matrixType == MatrixTypes.UNKNOWN)
            {
                // Al Bunny implementation.
                Point2D point0 = matrix.Transform(rect.TopLeft);
                Point2D point1 = matrix.Transform(rect.TopRight);
                Point2D point2 = matrix.Transform(rect.BottomRight);
                Point2D point3 = matrix.Transform(rect.BottomLeft);

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
        internal static void MultiplyMatrix(ref Matrix2D matrix1, ref Matrix2D matrix2)
        {
            MatrixTypes type1 = matrix1.type;
            MatrixTypes type2 = matrix2.type;

            // Check for identities

            // If the second is identities, we can just return
            if (type2 == MatrixTypes.IDENTITY)
                return;

            // If the first is identities, we can just copy the memory across.
            if (type1 == MatrixTypes.IDENTITY)
            {
                matrix1 = matrix2;
                return;
            }

            // Optimize for translate case, where the second is a translate
            if (type2 == MatrixTypes.TRANSLATION)
            {
                // 2 additions
                matrix1.offsetX += matrix2.offsetX;
                matrix1.offsetY += matrix2.offsetY;

                // If matrix 1 wasn't unknown we added a translation
                if (type1 != MatrixTypes.UNKNOWN)
                    matrix1.type |= MatrixTypes.TRANSLATION;

                return;
            }

            // Check for the first value being a translate
            if (type1 == MatrixTypes.TRANSLATION)
            {
                // Save off the old offsets
                double offsetX = matrix1.offsetX;
                double offsetY = matrix1.offsetY;

                // Copy the matrix
                matrix1 = matrix2;

                matrix1.offsetX = (float)(offsetX * matrix2.m1x1 + offsetY * matrix2.m2x1 + matrix2.offsetX);
                matrix1.offsetY = (float)(offsetX * matrix2.m1x2 + offsetY * matrix2.m2x2 + matrix2.offsetY);

                if (type2 == MatrixTypes.UNKNOWN)
                    matrix1.type = MatrixTypes.UNKNOWN;
                else
                    matrix1.type = MatrixTypes.SCALING | MatrixTypes.TRANSLATION;
                return;
            }

            // The following code combines the type of the transformations so that the high nibble
            // is "this"'s type, and the low nibble is mat's type.  This allows for a switch rather
            // than nested switches.

            // trans1._type |  trans2._type
            //  7  6  5  4   |  3  2  1  0
            int combinedType = ((int)type1 << 4) | (int)type2;

            switch (combinedType)
            {
                case 34:  // S * S
                    // 2 multiplications
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.m2x2 *= matrix2.m2x2;
                    return;

                case 35:  // S * S|T
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.m2x2 *= matrix2.m2x2;
                    matrix1.offsetX = matrix2.offsetX;
                    matrix1.offsetY = matrix2.offsetY;

                    // Transform set to Translate and Scale
                    matrix1.type = MatrixTypes.TRANSLATION | MatrixTypes.SCALING;
                    return;

                case 50: // S|T * S
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.m2x2 *= matrix2.m2x2;
                    matrix1.offsetX *= matrix2.m1x1;
                    matrix1.offsetY *= matrix2.m2x2;
                    return;

                case 51: // S|T * S|T
                    matrix1.m1x1 *= matrix2.m1x1;
                    matrix1.m2x2 *= matrix2.m2x2;
                    matrix1.offsetX = matrix2.m1x1 * matrix1.offsetX + matrix2.offsetX;
                    matrix1.offsetY = matrix2.m2x2 * matrix1.offsetY + matrix2.offsetY;
                    return;
                case 36: // S * U
                case 52: // S|T * U
                case 66: // U * S
                case 67: // U * S|T
                case 68: // U * U
                    matrix1 = new Matrix2D(
                        matrix1.m1x1 * matrix2.m1x1 + matrix1.m1x2 * matrix2.m2x1,
                        matrix1.m1x1 * matrix2.m1x2 + matrix1.m1x2 * matrix2.m2x2,

                        matrix1.m2x1 * matrix2.m1x1 + matrix1.m2x2 * matrix2.m2x1,
                        matrix1.m2x1 * matrix2.m1x2 + matrix1.m2x2 * matrix2.m2x2,

                        matrix1.offsetX * matrix2.m1x1 + matrix1.offsetY * matrix2.m2x1 + matrix2.offsetX,
                        matrix1.offsetX * matrix2.m1x2 + matrix1.offsetY * matrix2.m2x2 + matrix2.offsetY);
                    return;
#if DEBUG
                default:
                    Debug.Fail("Matrix multiply hit an invalid case: " + combinedType);
                    break;
#endif
            }
        }

        /// <summary>
        /// Applies an offset to the specified matrix in place.
        /// </summary>
        internal static void PrependOffset(ref Matrix2D matrix, double offsetX, double offsetY)
        {
            if (matrix.type == MatrixTypes.IDENTITY)
            {
                matrix = new Matrix2D(1, 0, 0, 1, offsetX, offsetY);
                matrix.type = MatrixTypes.TRANSLATION;
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

                matrix.offsetX += matrix.m1x1 * offsetX + matrix.m2x1 * offsetY;
                matrix.offsetY += matrix.m1x2 * offsetX + matrix.m2x2 * offsetY;

                // It just gained a translate if was a scale transform. Identity transform is handled above.
                Debug.Assert(matrix.type != MatrixTypes.IDENTITY);
                if (matrix.type != MatrixTypes.UNKNOWN)
                    matrix.type |= MatrixTypes.TRANSLATION;
            }
        }

        /// <summary>
        /// Append - "this" becomes this * matrix, the same as this *= matrix.
        /// </summary>
        /// <param name="matrix"> The Matrix to append to this Matrix </param>
        public void Append(Matrix2D matrix)
        {
            this *= matrix;
        }

        /// <summary>
        /// Prepend - "this" becomes matrix * this, the same as this = matrix * this.
        /// </summary>
        /// <param name="matrix"> The Matrix to prepend to this Matrix </param>
        public void Prepend(Matrix2D matrix)
        {
            this = matrix * this;
        }

        /// <summary>
        /// Rotates this matrix about the origin
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void Rotate(double angle)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this *= CreateRotationRadians((angle * (PI / 180.0)));
        }

        /// <summary>
        /// Prepends a rotation about the origin to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void RotatePrepend(double angle)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this = CreateRotationRadians((angle * (PI / 180.0))) * this;
        }

        /// <summary>
        /// Rotates this matrix about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAt(double angle, double centerX, double centerY)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this *= CreateRotationRadians((angle * (PI / 180.0)), centerX, centerY);
        }

        /// <summary>
        /// Prepends a rotation about the given point to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAtPrepend(double angle, double centerX, double centerY)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this = CreateRotationRadians((angle * (PI / 180.0)), centerX, centerY) * this;
        }

        /// <summary>
        /// Scales this matrix around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void Scale(double scaleX, double scaleY)
        {
            this *= CreateScaling(scaleX, scaleY);
        }

        /// <summary>
        /// Prepends a scale around the origin to "this"
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void ScalePrepend(double scaleX, double scaleY)
        {
            this = CreateScaling(scaleX, scaleY) * this;
        }

        /// <summary>
        /// Scales this matrix around the center provided
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name="centerX">The centerX about which to scale</param>
        /// <param name="centerY">The centerY about which to scale</param>
        public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY)
        {
            this *= CreateScaling(scaleX, scaleY, centerX, centerY);
        }

        /// <summary>
        /// Prepends a scale around the center provided to "this"
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        /// <param name="centerX">The centerX about which to scale</param>
        /// <param name="centerY">The centerY about which to scale</param>
        public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY)
        {
            this = CreateScaling(scaleX, scaleY, centerX, centerY) * this;
        }

        /// <summary>
        /// Skews this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public void Skew(double skewX, double skewY)
        {
            skewX %= 360;
            skewY %= 360;
            this *= CreateSkewRadians((skewX * (PI / 180.0)),
                                      (skewY * (PI / 180.0)));
        }

        /// <summary>
        /// Prepends a skew to this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public void SkewPrepend(double skewX, double skewY)
        {
            skewX %= 360;
            skewY %= 360;
            this = CreateSkewRadians((skewX * (PI / 180.0)),
                                     (skewY * (PI / 180.0))) * this;
        }

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

            if (type == MatrixTypes.IDENTITY)
            {
                // Values would be incorrect if matrix was created using default constructor.
                // or if SetIdentity was called on a matrix which had values.
                //
                SetMatrix(1, 0,
                          0, 1,
                          offsetX, offsetY,
                          MatrixTypes.TRANSLATION);
            }
            else if (type == MatrixTypes.UNKNOWN)
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;
            }
            else
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;

                // If matrix wasn't unknown we added a translation
                type |= MatrixTypes.TRANSLATION;
            }
        }

        /// <summary>
        /// Prepends a translation to this matrix
        /// </summary>
        /// <param name='offsetX'>The offset in the x dimension</param>
        /// <param name='offsetY'>The offset in the y dimension</param>
        public void TranslatePrepend(double offsetX, double offsetY)
        {
            this = CreateTranslation(offsetX, offsetY) * this;
        }

        /// <summary>
        /// Transform - Transforms each Vector in the array by this matrix.
        /// </summary>
        /// <param name="vectors"> The Vector array to transform </param>
        public void Transform(Vector2D[] vectors)
        {
            if (vectors != null)
            {
                for (int i = 0; i < vectors.Length; i++)
                    MultiplyVector(ref vectors[i]);
            }
        }

        /// <summary>
        /// Transform - Transforms each point in the array by this matrix
        /// </summary>
        /// <param name="points"> The Point array to transform </param>
        public void Transform(Point2D[] points)
        {
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                    MultiplyPoint(ref points[i]);
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
            double determinant = Determinant;

            if (determinant.IsZero())
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.Transform_NotInvertible));
                throw new InvalidOperationException();
            }

            // Inversion does not change the type of a matrix.
            switch (type)
            {
                case MatrixTypes.IDENTITY:
                    break;
                case MatrixTypes.SCALING:
                    {
                        m1x1 = 1.0f / m1x1;
                        m2x2 = 1.0f / m2x2;
                    }
                    break;
                case MatrixTypes.TRANSLATION:
                    offsetX = -offsetX;
                    offsetY = -offsetY;
                    break;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    {
                        m1x1 = 1.0f / m1x1;
                        m2x2 = 1.0f / m2x2;
                        offsetX = -offsetX * m1x1;
                        offsetY = -offsetY * m2x2;
                    }
                    break;
                default:
                    {
                        double invdet = 1.0f / determinant;
                        SetMatrix(m2x2 * invdet,
                                  -m1x2 * invdet,
                                  -m2x1 * invdet,
                                  m1x1 * invdet,
                                  (m2x1 * offsetY - offsetX * m2x2) * invdet,
                                  (offsetX * m1x2 - m1x1 * offsetY) * invdet,
                                  MatrixTypes.UNKNOWN);
                    }
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        internal void MultiplyVector(ref double x, ref double y)
        {
            switch (type)
            {
                case MatrixTypes.IDENTITY:
                case MatrixTypes.TRANSLATION:
                    return;
                case MatrixTypes.SCALING:
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    x *= m1x1;
                    y *= m2x2;
                    break;
                default:
                    double xadd = y * m2x1;
                    double yadd = x * m1x2;
                    x *= m1x1;
                    x += xadd;
                    y *= m2x2;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        internal void MultiplyVector(ref Vector2D vector)
        {
            switch (type)
            {
                case MatrixTypes.IDENTITY:
                case MatrixTypes.TRANSLATION:
                    return;
                case MatrixTypes.SCALING:
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    vector.I *= m1x1;
                    vector.J *= m2x2;
                    break;
                default:
                    double xadd = vector.J * m2x1;
                    double yadd = vector.I * m1x2;
                    vector.I *= m1x1;
                    vector.I += xadd;
                    vector.J *= m2x2;
                    vector.J += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        internal void MultiplyPoint(ref double x, ref double y)
        {
            switch (type)
            {
                case MatrixTypes.IDENTITY:
                    return;
                case MatrixTypes.TRANSLATION:
                    x += offsetX;
                    y += offsetY;
                    return;
                case MatrixTypes.SCALING:
                    x *= m1x1;
                    y *= m2x2;
                    return;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    x *= m1x1;
                    x += offsetX;
                    y *= m2x2;
                    y += offsetY;
                    break;
                default:
                    double xadd = y * m2x1 + offsetX;
                    double yadd = x * m1x2 + offsetY;
                    x *= m1x1;
                    x += xadd;
                    y *= m2x2;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        internal void MultiplyPoint(ref Point2D point)
        {
            switch (type)
            {
                case MatrixTypes.IDENTITY:
                    return;
                case MatrixTypes.TRANSLATION:
                    point.X += offsetX;
                    point.Y += offsetY;
                    return;
                case MatrixTypes.SCALING:
                    point.X *= m1x1;
                    point.Y *= m2x2;
                    return;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    point.X *= m1x1;
                    point.X += offsetX;
                    point.Y *= m2x2;
                    point.Y += offsetY;
                    break;
                default:
                    double xadd = point.Y * m2x1 + offsetX;
                    double yadd = point.X * m1x2 + offsetY;
                    point.X *= m1x1;
                    point.X += xadd;
                    point.Y *= m2x2;
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
            m1x1 = m11;
            m1x2 = m12;
            m2x1 = m21;
            m2x2 = m22;
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
            if (!(Abs(m2x1) < DoubleEpsilon && Abs(m1x2) < DoubleEpsilon))
            {
                type = MatrixTypes.UNKNOWN;
                return;
            }

            if (!(Abs(m1x1 - 1) < DoubleEpsilon && Abs(m2x2 - 1) < DoubleEpsilon))
                type = MatrixTypes.SCALING;

            if (!(Abs(offsetX) < DoubleEpsilon && Abs(offsetY) < DoubleEpsilon))
                type |= MatrixTypes.TRANSLATION;

            if (0 == (type & (MatrixTypes.TRANSLATION | MatrixTypes.SCALING)))
            {
                // We have an identity matrix.
                type = MatrixTypes.IDENTITY;
            }
            return;
        }

        #endregion

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
            Point2D newPoint = point;
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
            Vector2D newVector = vector;
            MultiplyVector(ref newVector);
            return newVector;
        }

        /// <summary>
        /// The determinant of this matrix
        /// </summary>
        public double Determinant
        {
            get
            {
                switch (type)
                {
                    case MatrixTypes.IDENTITY:
                    case MatrixTypes.TRANSLATION:
                        return 1.0d;
                    case MatrixTypes.SCALING:
                    case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                        return (m1x1 * m2x2);
                    default:
                        return ((m1x1 * m2x2) - (m1x2 * m2x1));
                }
            }
        }

        /// <summary>
        /// Returns the HashCode for this Matrix
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Matrix
        /// </returns>
        public override int GetHashCode()
        {
            if (IsDistinguishedIdentity)
            {
                return c_identityHashCode;
            }
            else
            {
                // Perform field-by-field XOR of HashCodes
                return M11.GetHashCode() ^
                       M12.GetHashCode() ^
                       M21.GetHashCode() ^
                       M22.GetHashCode() ^
                       OffsetX.GetHashCode() ^
                       OffsetY.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2D"/> struct based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Matrix2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
#pragma warning disable RECS0065 // Expression is always 'true' or always 'false'
            if (this == null) return nameof(Matrix2D);
#pragma warning restore RECS0065 // Expression is always 'true' or always 'false'
            if (IsIdentity) return "Identity";
            // Helper to get the numeric list separator for a given culture.
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Matrix2D)}{{{nameof(M11)}={m1x1}{sep}{nameof(M12)}={m1x2}{sep}{nameof(M21)}={m2x1}{sep}{nameof(M22)}={m2x2}{sep}{nameof(OffsetX)}={offsetX}{sep}{nameof(OffsetY)}={offsetY}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
