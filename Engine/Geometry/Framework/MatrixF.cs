using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// http://referencesource.microsoft.com
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(MatrixFConverter))]
    [DisplayName("MatrixF")]
    public partial struct MatrixF
        : IFormattable
    {
        internal MatrixTypes _type;

        internal float _m11;
        internal float _m12;
        internal float _m21;
        internal float _m22;
        internal float _offsetX;
        internal float _offsetY;

        // This field is only used by unmanaged code which isn't detected by the compiler.
        // Matrix in blt'd to unmanaged code, so this is padding 
        // to align structure.
        //
        // ToDo: [....], Validate that this blt will work on 64-bit
        //
        internal Int32 _padding;

        // the transform is identity by default
        // Actually fill in the fields - some (internal) code uses the fields directly for perf.
        private static MatrixF s_identity = CreateIdentity();

        // The hash code for a matrix is the xor of its element's hashes.
        // Since the identity matrix has 2 1's and 4 0's its hash is 0.
        private const int c_identityHashCode = 0;

        /// <summary>
        /// Creates a matrix of the form
        ///             / m11, m12, 0 \
        ///             | m21, m22, 0 |
        ///             \ offsetX, offsetY, 1 /
        /// </summary>
        public MatrixF(float m11, float m12, float m21, float m22, float offsetX, float offsetY)
        {
            _m11 = m11;
            _m12 = m12;
            _m21 = m21;
            _m22 = m22;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _type = MatrixTypes.UNKNOWN;
            _padding = 0;

            // We will detect EXACT identity, scale, translation or
            // scale+translation and use special case algorithms.
            DeriveMatrixType();
        }

        /// <summary>
        /// M11
        /// </summary>
        public float M11
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 1.0f;
                }
                else
                {
                    return _m11;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(value, 0,
                              0, 1,
                              0, 0,
                              MatrixTypes.SCALING);
                }
                else
                {
                    _m11 = value;
                    if (_type != MatrixTypes.UNKNOWN)
                    {
                        _type |= MatrixTypes.SCALING;
                    }
                }
            }
        }

        /// <summary>
        /// M12
        /// </summary>
        public float M12
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 0;
                }
                else
                {
                    return _m12;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, value,
                              0, 1,
                              0, 0,
                              MatrixTypes.UNKNOWN);
                }
                else
                {
                    _m12 = value;
                    _type = MatrixTypes.UNKNOWN;
                }
            }
        }

        /// <summary>
        /// M22
        /// </summary>
        public float M21
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 0;
                }
                else
                {
                    return _m21;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              value, 1,
                              0, 0,
                              MatrixTypes.UNKNOWN);
                }
                else
                {
                    _m21 = value;
                    _type = MatrixTypes.UNKNOWN;
                }
            }
        }

        /// <summary>
        /// M22
        /// </summary>
        public float M22
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 1.0f;
                }
                else
                {
                    return _m22;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, value,
                              0, 0,
                              MatrixTypes.SCALING);
                }
                else
                {
                    _m22 = value;
                    if (_type != MatrixTypes.UNKNOWN)
                    {
                        _type |= MatrixTypes.SCALING;
                    }
                }
            }
        }

        /// <summary>
        /// OffsetX
        /// </summary>
        public float OffsetX
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 0;
                }
                else
                {
                    return _offsetX;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              value, 0,
                              MatrixTypes.TRANSLATION);
                }
                else
                {
                    _offsetX = value;
                    if (_type != MatrixTypes.UNKNOWN)
                    {
                        _type |= MatrixTypes.TRANSLATION;
                    }
                }
            }
        }

        /// <summary>
        /// OffsetY
        /// </summary>
        public float OffsetY
        {
            get
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    return 0;
                }
                else
                {
                    return _offsetY;
                }
            }
            set
            {
                if (_type == MatrixTypes.IDENTITY)
                {
                    SetMatrix(1, 0,
                              0, 1,
                              0, value,
                              MatrixTypes.TRANSLATION);
                }
                else
                {
                    _offsetY = value;
                    if (_type != MatrixTypes.UNKNOWN)
                    {
                        _type |= MatrixTypes.TRANSLATION;
                    }
                }
            }
        }

        /// <summary>
        /// Efficient but conservative test for identity.  Returns
        /// true if the matrix is identity.  If it returns false
        /// the matrix may still be identity.
        /// </summary>
        private bool IsDistinguishedIdentity
        {
            get
            {
                return _type == MatrixTypes.IDENTITY;
            }
        }

        /// <summary>
        /// Identity
        /// </summary>
        public static MatrixF Identity
        {
            get { return s_identity; }
        }

        /// <summary>
        /// Sets the transformation to the identity.
        /// </summary>
        private static MatrixF CreateIdentity()
        {
            MatrixF matrix = new MatrixF();
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
            _type = MatrixTypes.IDENTITY;
        }

        /// <summary>
        /// Tests whether or not a given transform is an identity transform
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return (_type == MatrixTypes.IDENTITY ||
                        (_m11 == 1 && _m12 == 0 && _m21 == 0 && _m22 == 1 && _offsetX == 0 && _offsetY == 0));
            }
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
        public static bool operator ==(MatrixF matrix1, MatrixF matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
            {
                return matrix1.IsIdentity == matrix2.IsIdentity;
            }
            else
            {
                return matrix1.M11 == matrix2.M11 &&
                       matrix1.M12 == matrix2.M12 &&
                       matrix1.M21 == matrix2.M21 &&
                       matrix1.M22 == matrix2.M22 &&
                       matrix1.OffsetX == matrix2.OffsetX &&
                       matrix1.OffsetY == matrix2.OffsetY;
            }
        }

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
        public static bool operator !=(MatrixF matrix1, MatrixF matrix2)
        {
            return !(matrix1 == matrix2);
        }

        /// <summary>
        /// Operator Point * Matrix
        /// </summary>
        public static PointF operator *(PointF point, MatrixF matrix)
        {
            return matrix.Transform(point);
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
        public static bool Equals(MatrixF matrix1, MatrixF matrix2)
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
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is MatrixF))
            {
                return false;
            }

            MatrixF value = (MatrixF)o;
            return MatrixF.Equals(this, value);
        }

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
        public bool Equals(MatrixF value)
        {
            return MatrixF.Equals(this, value);
        }

        /// <summary>
        /// Multiplies two transformations.
        /// </summary>
        public static MatrixF operator *(MatrixF trans1, MatrixF trans2)
        {
            MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Multiply
        /// </summary>
        public static MatrixF Multiply(MatrixF trans1, MatrixF trans2)
        {
            MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// TransformRect - Internal helper for perf
        /// </summary>
        /// <param name="rect"> The Rectangle to transform. </param>
        /// <param name="matrix"> The Matrix with which to transform the Rectangle. </param>
        internal static void TransformRect(ref RectangleF rect, ref MatrixF matrix)
        {
            if (rect.IsEmpty)
            {
                return;
            }

            MatrixTypes matrixType = matrix._type;

            // If the matrix is identity, don't worry.
            if (matrixType == MatrixTypes.IDENTITY)
            {
                return;
            }

            // Scaling
            if (0 != (matrixType & MatrixTypes.SCALING))
            {
                rect.X *= matrix._m11;
                rect.Y *= matrix._m22;
                rect.Width *= matrix._m11;
                rect.Height *= matrix._m22;

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
                rect.X += matrix._offsetX;

                // Y
                rect.Y += matrix._offsetY;
            }

            if (matrixType == MatrixTypes.UNKNOWN)
            {
                // Al Bunny implementation.
                PointF point0 = matrix.Transform(rect.TopLeft());
                PointF point1 = matrix.Transform(rect.TopRight());
                PointF point2 = matrix.Transform(rect.BottomRight());
                PointF point3 = matrix.Transform(rect.BottomLeft());

                // Width and height is always positive here.
                rect.X = Math.Min(Math.Min(point0.X, point1.X), Math.Min(point2.X, point3.X));
                rect.Y = Math.Min(Math.Min(point0.Y, point1.Y), Math.Min(point2.Y, point3.Y));

                rect.Width = Math.Max(Math.Max(point0.X, point1.X), Math.Max(point2.X, point3.X)) - rect.X;
                rect.Height = Math.Max(Math.Max(point0.Y, point1.Y), Math.Max(point2.Y, point3.Y)) - rect.Y;
            }
        }

        /// <summary>
        /// Multiplies two transformations, where the behavior is matrix1 *= matrix2.
        /// This code exists so that we can efficient combine matrices without copying
        /// the data around, since each matrix is 52 bytes.
        /// To reduce duplication and to ensure consistent behavior, this is the
        /// method which is used to implement Matrix * Matrix as well.
        /// </summary>
        internal static void MultiplyMatrix(ref MatrixF matrix1, ref MatrixF matrix2)
        {
            MatrixTypes type1 = matrix1._type;
            MatrixTypes type2 = matrix2._type;

            // Check for identities

            // If the second is identities, we can just return
            if (type2 == MatrixTypes.IDENTITY)
            {
                return;
            }

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
                matrix1._offsetX += matrix2._offsetX;
                matrix1._offsetY += matrix2._offsetY;

                // If matrix 1 wasn't unknown we added a translation
                if (type1 != MatrixTypes.UNKNOWN)
                {
                    matrix1._type |= MatrixTypes.TRANSLATION;
                }

                return;
            }

            // Check for the first value being a translate
            if (type1 == MatrixTypes.TRANSLATION)
            {
                // Save off the old offsets
                double offsetX = matrix1._offsetX;
                double offsetY = matrix1._offsetY;

                // Copy the matrix
                matrix1 = matrix2;

                matrix1._offsetX = (float)(offsetX * matrix2._m11 + offsetY * matrix2._m21 + matrix2._offsetX);
                matrix1._offsetY = (float)(offsetX * matrix2._m12 + offsetY * matrix2._m22 + matrix2._offsetY);

                if (type2 == MatrixTypes.UNKNOWN)
                {
                    matrix1._type = MatrixTypes.UNKNOWN;
                }
                else
                {
                    matrix1._type = MatrixTypes.SCALING | MatrixTypes.TRANSLATION;
                }
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
                    matrix1._m11 *= matrix2._m11;
                    matrix1._m22 *= matrix2._m22;
                    return;

                case 35:  // S * S|T
                    matrix1._m11 *= matrix2._m11;
                    matrix1._m22 *= matrix2._m22;
                    matrix1._offsetX = matrix2._offsetX;
                    matrix1._offsetY = matrix2._offsetY;

                    // Transform set to Translate and Scale
                    matrix1._type = MatrixTypes.TRANSLATION | MatrixTypes.SCALING;
                    return;

                case 50: // S|T * S
                    matrix1._m11 *= matrix2._m11;
                    matrix1._m22 *= matrix2._m22;
                    matrix1._offsetX *= matrix2._m11;
                    matrix1._offsetY *= matrix2._m22;
                    return;

                case 51: // S|T * S|T
                    matrix1._m11 *= matrix2._m11;
                    matrix1._m22 *= matrix2._m22;
                    matrix1._offsetX = matrix2._m11 * matrix1._offsetX + matrix2._offsetX;
                    matrix1._offsetY = matrix2._m22 * matrix1._offsetY + matrix2._offsetY;
                    return;
                case 36: // S * U
                case 52: // S|T * U
                case 66: // U * S
                case 67: // U * S|T
                case 68: // U * U
                    matrix1 = new MatrixF(
                        matrix1._m11 * matrix2._m11 + matrix1._m12 * matrix2._m21,
                        matrix1._m11 * matrix2._m12 + matrix1._m12 * matrix2._m22,

                        matrix1._m21 * matrix2._m11 + matrix1._m22 * matrix2._m21,
                        matrix1._m21 * matrix2._m12 + matrix1._m22 * matrix2._m22,

                        matrix1._offsetX * matrix2._m11 + matrix1._offsetY * matrix2._m21 + matrix2._offsetX,
                        matrix1._offsetX * matrix2._m12 + matrix1._offsetY * matrix2._m22 + matrix2._offsetY);
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
        internal static void PrependOffset(ref MatrixF matrix, float offsetX, float offsetY)
        {
            if (matrix._type == MatrixTypes.IDENTITY)
            {
                matrix = new MatrixF(1, 0, 0, 1, offsetX, offsetY);
                matrix._type = MatrixTypes.TRANSLATION;
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

                matrix._offsetX += matrix._m11 * offsetX + matrix._m21 * offsetY;
                matrix._offsetY += matrix._m12 * offsetX + matrix._m22 * offsetY;

                // It just gained a translate if was a scale transform. Identity transform is handled above.
                Debug.Assert(matrix._type != MatrixTypes.IDENTITY);
                if (matrix._type != MatrixTypes.UNKNOWN)
                {
                    matrix._type |= MatrixTypes.TRANSLATION;
                }
            }
        }

        /// <summary>
        /// Append - "this" becomes this * matrix, the same as this *= matrix.
        /// </summary>
        /// <param name="matrix"> The Matrix to append to this Matrix </param>
        public void Append(MatrixF matrix)
        {
            this *= matrix;
        }

        /// <summary>
        /// Prepend - "this" becomes matrix * this, the same as this = matrix * this.
        /// </summary>
        /// <param name="matrix"> The Matrix to prepend to this Matrix </param>
        public void Prepend(MatrixF matrix)
        {
            this = matrix * this;
        }

        /// <summary>
        /// Rotates this matrix about the origin
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void Rotate(float angle)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this *= CreateRotationRadians((float)(angle * (Math.PI / 180.0)));
        }

        /// <summary>
        /// Prepends a rotation about the origin to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        public void RotatePrepend(float angle)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this = CreateRotationRadians((float)(angle * (Math.PI / 180.0))) * this;
        }

        /// <summary>
        /// Rotates this matrix about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAt(float angle, float centerX, float centerY)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this *= CreateRotationRadians((float)(angle * (Math.PI / 180.0)), centerX, centerY);
        }

        /// <summary>
        /// Prepends a rotation about the given point to "this"
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in degrees</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        public void RotateAtPrepend(float angle, float centerX, float centerY)
        {
            angle %= 360.0f; // Doing the modulo before converting to radians reduces total error
            this = CreateRotationRadians((float)(angle * (Math.PI / 180.0)), centerX, centerY) * this;
        }

        /// <summary>
        /// Scales this matrix around the origin
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void Scale(float scaleX, float scaleY)
        {
            this *= CreateScaling(scaleX, scaleY);
        }

        /// <summary>
        /// Prepends a scale around the origin to "this"
        /// </summary>
        /// <param name='scaleX'>The scale factor in the x dimension</param>
        /// <param name='scaleY'>The scale factor in the y dimension</param>
        public void ScalePrepend(float scaleX, float scaleY)
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
        public void ScaleAt(float scaleX, float scaleY, float centerX, float centerY)
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
        public void ScaleAtPrepend(float scaleX, float scaleY, float centerX, float centerY)
        {
            this = CreateScaling(scaleX, scaleY, centerX, centerY) * this;
        }

        /// <summary>
        /// Skews this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public void Skew(float skewX, float skewY)
        {
            skewX %= 360;
            skewY %= 360;
            this *= CreateSkewRadians((float)(skewX * (Math.PI / 180.0)),
                                      (float)(skewY * (Math.PI / 180.0)));
        }

        /// <summary>
        /// Prepends a skew to this matrix
        /// </summary>
        /// <param name='skewX'>The skew angle in the x dimension in degrees</param>
        /// <param name='skewY'>The skew angle in the y dimension in degrees</param>
        public void SkewPrepend(float skewX, float skewY)
        {
            skewX %= 360;
            skewY %= 360;
            this = CreateSkewRadians((float)(skewX * (Math.PI / 180.0)),
                                     (float)(skewY * (Math.PI / 180.0))) * this;
        }

        /// <summary>
        /// Translates this matrix
        /// </summary>
        /// <param name='offsetX'>The offset in the x dimension</param>
        /// <param name='offsetY'>The offset in the y dimension</param>
        public void Translate(float offsetX, float offsetY)
        {
            //
            // / a b 0 \   / 1 0 0 \    / a      b       0 \
            // | c d 0 | * | 0 1 0 | = |  c      d       0 |
            // \ e f 1 /   \ x y 1 /    \ e+x    f+y     1 /
            //
            // (where e = _offsetX and f == _offsetY)
            //

            if (_type == MatrixTypes.IDENTITY)
            {
                // Values would be incorrect if matrix was created using default constructor.
                // or if SetIdentity was called on a matrix which had values.
                //
                SetMatrix(1, 0,
                          0, 1,
                          offsetX, offsetY,
                          MatrixTypes.TRANSLATION);
            }
            else if (_type == MatrixTypes.UNKNOWN)
            {
                _offsetX += offsetX;
                _offsetY += offsetY;
            }
            else
            {
                _offsetX += offsetX;
                _offsetY += offsetY;

                // If matrix wasn't unknown we added a translation
                _type |= MatrixTypes.TRANSLATION;
            }
        }

        /// <summary>
        /// Prepends a translation to this matrix
        /// </summary>
        /// <param name='offsetX'>The offset in the x dimension</param>
        /// <param name='offsetY'>The offset in the y dimension</param>
        public void TranslatePrepend(float offsetX, float offsetY)
        {
            this = CreateTranslation(offsetX, offsetY) * this;
        }

        /// <summary>
        /// Transform - returns the result of transforming the point by this matrix
        /// </summary>
        /// <returns>
        /// The transformed point
        /// </returns>
        /// <param name="point"> The Point to transform </param>
        public PointF Transform(PointF point)
        {
            PointF newPoint = point;
            MultiplyPoint(ref newPoint);
            return newPoint;
        }

        /// <summary>
        /// Transform - Transforms each point in the array by this matrix
        /// </summary>
        /// <param name="points"> The Point array to transform </param>
        public void Transform(PointF[] points)
        {
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    MultiplyPoint(ref points[i]);
                }
            }
        }

        /// <summary>
        /// Transform - returns the result of transforming the Vector by this matrix.
        /// </summary>
        /// <returns>
        /// The transformed vector
        /// </returns>
        /// <param name="vector"> The Vector to transform </param>
        public VectorF Transform(VectorF vector)
        {
            VectorF newVector = vector;
            MultiplyVector(ref newVector);
            return newVector;
        }

        /// <summary>
        /// Transform - Transforms each Vector in the array by this matrix.
        /// </summary>
        /// <param name="vectors"> The Vector array to transform </param>
        public void Transform(VectorF[] vectors)
        {
            if (vectors != null)
            {
                for (int i = 0; i < vectors.Length; i++)
                {
                    MultiplyVector(ref vectors[i]);
                }
            }
        }

        /// <summary>
        /// The determinant of this matrix
        /// </summary>
        public float Determinant
        {
            get
            {
                switch (_type)
                {
                    case MatrixTypes.IDENTITY:
                    case MatrixTypes.TRANSLATION:
                        return 1.0f;
                    case MatrixTypes.SCALING:
                    case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                        return (_m11 * _m22);
                    default:
                        return (_m11 * _m22) - (_m12 * _m21);
                }
            }
        }

        /// <summary>
        /// HasInverse Property - returns true if this matrix is invert-able, false otherwise.
        /// </summary>
        public bool HasInverse
        {
            get { return !Determinant.IsZero(); }
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
            float determinant = Determinant;

            if (determinant.IsZero())
            {
                //throw new System.InvalidOperationException(SR.Get(SRID.Transform_NotInvertible));
                throw new InvalidOperationException();
            }

            // Inversion does not change the type of a matrix.
            switch (_type)
            {
                case MatrixTypes.IDENTITY:
                    break;
                case MatrixTypes.SCALING:
                    {
                        _m11 = 1.0f / _m11;
                        _m22 = 1.0f / _m22;
                    }
                    break;
                case MatrixTypes.TRANSLATION:
                    _offsetX = -_offsetX;
                    _offsetY = -_offsetY;
                    break;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    {
                        _m11 = 1.0f / _m11;
                        _m22 = 1.0f / _m22;
                        _offsetX = -_offsetX * _m11;
                        _offsetY = -_offsetY * _m22;
                    }
                    break;
                default:
                    {
                        float invdet = 1.0f / determinant;
                        SetMatrix(_m22 * invdet,
                                  -_m12 * invdet,
                                  -_m21 * invdet,
                                  _m11 * invdet,
                                  (_m21 * _offsetY - _offsetX * _m22) * invdet,
                                  (_offsetX * _m12 - _m11 * _offsetY) * invdet,
                                  MatrixTypes.UNKNOWN);
                    }
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        internal void MultiplyVector(ref float x, ref float y)
        {
            switch (_type)
            {
                case MatrixTypes.IDENTITY:
                case MatrixTypes.TRANSLATION:
                    return;
                case MatrixTypes.SCALING:
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    x *= _m11;
                    y *= _m22;
                    break;
                default:
                    float xadd = y * _m21;
                    float yadd = x * _m12;
                    x *= _m11;
                    x += xadd;
                    y *= _m22;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyVector
        /// </summary>
        internal void MultiplyVector(ref VectorF vector)
        {
            switch (_type)
            {
                case MatrixTypes.IDENTITY:
                case MatrixTypes.TRANSLATION:
                    return;
                case MatrixTypes.SCALING:
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    vector.X *= _m11;
                    vector.Y *= _m22;
                    break;
                default:
                    float xadd = vector.Y * _m21;
                    float yadd = vector.X * _m12;
                    vector.X *= _m11;
                    vector.X += xadd;
                    vector.Y *= _m22;
                    vector.Y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        internal void MultiplyPoint(ref float x, ref float y)
        {
            switch (_type)
            {
                case MatrixTypes.IDENTITY:
                    return;
                case MatrixTypes.TRANSLATION:
                    x += _offsetX;
                    y += _offsetY;
                    return;
                case MatrixTypes.SCALING:
                    x *= _m11;
                    y *= _m22;
                    return;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    x *= _m11;
                    x += _offsetX;
                    y *= _m22;
                    y += _offsetY;
                    break;
                default:
                    float xadd = y * _m21 + _offsetX;
                    float yadd = x * _m12 + _offsetY;
                    x *= _m11;
                    x += xadd;
                    y *= _m22;
                    y += yadd;
                    break;
            }
        }

        /// <summary>
        /// MultiplyPoint
        /// </summary>
        internal void MultiplyPoint(ref PointF point)
        {
            switch (_type)
            {
                case MatrixTypes.IDENTITY:
                    return;
                case MatrixTypes.TRANSLATION:
                    point.X += _offsetX;
                    point.Y += _offsetY;
                    return;
                case MatrixTypes.SCALING:
                    point.X *= _m11;
                    point.Y *= _m22;
                    return;
                case MatrixTypes.SCALING | MatrixTypes.TRANSLATION:
                    point.X *= _m11;
                    point.X += _offsetX;
                    point.Y *= _m22;
                    point.Y += _offsetY;
                    break;
                default:
                    float xadd = point.Y * _m21 + _offsetX;
                    float yadd = point.X * _m12 + _offsetY;
                    point.X *= _m11;
                    point.X += xadd;
                    point.Y *= _m22;
                    point.Y += yadd;
                    break;
            }
        }

        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        internal static MatrixF CreateRotationRadians(float angle)
        {
            return CreateRotationRadians(angle, /* centerX = */ 0, /* centerY = */ 0);
        }

        /// <summary>
        /// Creates a rotation transformation about the given point
        /// </summary>
        /// <param name='angle'>The angle to rotate specified in radians</param>
        /// <param name='centerX'>The centerX of rotation</param>
        /// <param name='centerY'>The centerY of rotation</param>
        internal static MatrixF CreateRotationRadians(float angle, float centerX, float centerY)
        {
            MatrixF matrix = new MatrixF();
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);
            float dx = (float)((centerX * (1.0 - cos)) + (centerY * sin));
            float dy = (float)((centerY * (1.0 - cos)) - (centerX * sin));

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
        internal static MatrixF CreateScaling(float scaleX, float scaleY, float centerX, float centerY)
        {
            MatrixF matrix = new MatrixF();

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
        internal static MatrixF CreateScaling(float scaleX, float scaleY)
        {
            MatrixF matrix = new MatrixF();
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
        internal static MatrixF CreateSkewRadians(float skewX, float skewY)
        {
            MatrixF matrix = new MatrixF();

            matrix.SetMatrix(1.0f, (float)Math.Tan(skewY),
                             (float)Math.Tan(skewX), 1.0f,
                             0.0f, 0.0f,
                             MatrixTypes.UNKNOWN);

            return matrix;
        }

        /// <summary>
        /// Sets the transformation to the given translation specified by the offset vector.
        /// </summary>
        /// <param name='offsetX'>The offset in X</param>
        /// <param name='offsetY'>The offset in Y</param>
        internal static MatrixF CreateTranslation(float offsetX, float offsetY)
        {
            MatrixF matrix = new MatrixF();

            matrix.SetMatrix(1, 0,
                             0, 1,
                             offsetX, offsetY,
                             MatrixTypes.TRANSLATION);

            return matrix;
        }

        ///<summary>
        /// Sets the transform to
        ///             / m11, m12, 0 \
        ///             | m21, m22, 0 |
        ///             \ offsetX, offsetY, 1 /
        /// where offsetX, offsetY is the translation.
        ///</summary>
        private void SetMatrix(float m11, float m12,
                               float m21, float m22,
                               float offsetX, float offsetY,
                               MatrixTypes type)
        {
            _m11 = m11;
            _m12 = m12;
            _m21 = m21;
            _m22 = m22;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _type = type;
        }

        /// <summary>
        /// Set the type of the matrix based on its current contents
        /// </summary>
        private void DeriveMatrixType()
        {
            _type = 0;

            // Now classify our matrix.
            if (!(_m21 == 0 && _m12 == 0))
            {
                _type = MatrixTypes.UNKNOWN;
                return;
            }

            if (!(_m11 == 1 && _m22 == 1))
            {
                _type = MatrixTypes.SCALING;
            }

            if (!(_offsetX == 0 && _offsetY == 0))
            {
                _type |= MatrixTypes.TRANSLATION;
            }

            if (0 == (_type & (MatrixTypes.TRANSLATION | MatrixTypes.SCALING)))
            {
                // We have an identity matrix.
                _type = MatrixTypes.IDENTITY;
            }
            return;
        }

        // Helper to get the numeric list separator for a given IFormatProvider.
        // Separator is a comma [,] if the decimal separator is not a comma, or a semicolon [;] otherwise.
        static internal char GetNumericListSeparator(IFormatProvider provider)
        {
            char numericSeparator = ',';

            // Get the NumberFormatInfo out of the provider, if possible
            // If the IFormatProvider doesn't not contain a NumberFormatInfo, then
            // this method returns the current culture's NumberFormatInfo.
            NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(provider);

            Debug.Assert(null != numberFormat);

            // Is the decimal separator is the same as the list separator?
            // If so, we use the ";".
            if ((numberFormat.NumberDecimalSeparator.Length > 0) && (numericSeparator == numberFormat.NumberDecimalSeparator[0]))
            {
                numericSeparator = ';';
            }

            return numericSeparator;
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
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Matrix data </param>
        /// </summary>
        public static MatrixF Parse(string source)
        {
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            TokenizerHelper th = new TokenizerHelper(source, formatProvider);

            MatrixF value;

            string firstToken = th.NextTokenRequired();

            // The token will already have had whitespace trimmed so we can do a
            // simple string compare.
            if (firstToken == "Identity")
            {
                value = Identity;
            }
            else
            {
                value = new MatrixF(
                    MathExtensions.ToFloat(firstToken, formatProvider),
                    MathExtensions.ToFloat(th.NextTokenRequired(), formatProvider),
                    MathExtensions.ToFloat(th.NextTokenRequired(), formatProvider),
                    MathExtensions.ToFloat(th.NextTokenRequired(), formatProvider),
                    MathExtensions.ToFloat(th.NextTokenRequired(), formatProvider),
                    MathExtensions.ToFloat(th.NextTokenRequired(), formatProvider));
            }

            // There should be no more tokens in this string.
            th.LastTokenRequired();

            return value;
        }

        /// <summary>
        /// Creates a string representation of this object based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, null /* format provider */);
        }

        /// <summary>
        /// Creates a string representation of this object based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null /* format string */, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        string IFormattable.ToString(string format, IFormatProvider provider)
        {

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, provider);
        }

        /// <summary>
        /// Creates a string representation of this object based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (IsIdentity)
            {
                return "Identity";
            }

            // Helper to get the numeric list separator for a given culture.
            char separator = GetNumericListSeparator(provider);
            return string.Format(provider,
                                 "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}{0}{4:" + format + "}{0}{5:" + format + "}{0}{6:" + format + "}",
                                 separator,
                                 _m11,
                                 _m12,
                                 _m21,
                                 _m22,
                                 _offsetX,
                                 _offsetY);
        }
    }
}
