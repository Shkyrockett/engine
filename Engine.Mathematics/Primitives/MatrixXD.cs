// <copyright file="Matrix.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public struct MatrixXD
        : IMatrix<MatrixXD, VectorXD>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixXD" /> struct.
        /// </summary>
        /// <param name="values">The values.</param>
        public MatrixXD(double[][] values)
        {
            Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixXD" /> struct.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixXD(double[,] matrix)
        {
            var rows = (matrix?.GetLength(0)).Value;
            var cols = (matrix?.GetLength(1)).Value;
            var values = new double[rows][];
            for (var i = 0; i < rows; i++)
            {
                var row = new double[cols];
                for (var j = 0; j < cols; j++)
                {
                    row[j] = matrix[i, j];
                }
                values[i] = row;
            }

            Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixXD" /> struct.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public MatrixXD(int rows, int columns)
            : this(new double[rows, columns])
        { }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="System.Double"/> with the specified index1.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double"/>.
        /// </value>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        /// <returns></returns>
        public double this[int index1, int index2]
        {
            get { return Values[index1][index2]; }
            set { Values[index1][index2] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public double[][] Values { get; set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows => Values.Length;

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns => Values[0].Length;

        /// <summary>
        /// Gets a value indicating whether this instance is square.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this instance is square; otherwise, <see langword="false" />.
        /// </value>
        public bool IsSquare => IsSquareMatrix(Values);

        /// <summary>
        /// Gets a value indicating whether this instance is identity.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this instance is identity; otherwise, <see langword="false" />.
        /// </value>
        public bool IsIdentity => IsIdentity(Values);

        /// <summary>
        /// Gets a value indicating whether this instance is a lower matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this instance is lower; otherwise, <see langword="false" />.
        /// </value>
        public bool IsLower => IsLowerMatrix(Values);

        /// <summary>
        /// Gets a value indicating whether this instance is an upper matrix.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this instance is upper; otherwise, <see langword="false" />.
        /// </value>
        public bool IsUpper => IsUpperMatrix(Values);

        /// <summary>
        /// The determinant
        /// </summary>
        public double Determinant => Operations.Determinant(Values);

        /// <summary>
        /// Gets the inverse determinant.
        /// </summary>
        /// <value>
        /// The inverse determinant.
        /// </value>
        public double InverseDeterminant => Operations.Determinant(Operations.Inverse(Values));

        /// <summary>
        /// Gets the transpose.
        /// </summary>
        /// <value>
        /// The transpose.
        /// </value>
        public MatrixXD Transpose => Operations.Transpose(Values);

        /// <summary>
        /// Gets the inverse.
        /// </summary>
        /// <value>
        /// The inverse.
        /// </value>
        public MatrixXD Inverse => Operations.Inverse(Values);

        /// <summary>
        /// Gets the adjoint.
        /// </summary>
        /// <value>
        /// The adjoint.
        /// </value>
        public MatrixXD Adjoint => Operations.Adjoint(Values);

        /// <summary>
        /// Gets the cofactor.
        /// </summary>
        /// <value>
        /// The cofactor.
        /// </value>
        public MatrixXD Cofactor => Operations.Cofactor(Values);

        /// <summary>
        /// Gets the decompose.
        /// </summary>
        /// <value>
        /// The decompose.
        /// </value>
        public (MatrixXD Lower, MatrixXD Upper) Decompose => DecomposeToLowerUpper(Values);
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator +(MatrixXD value) => Plus(value);

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator +(MatrixXD augend, MatrixXD addend) => Add(augend, addend);

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator -(MatrixXD value) => Negate(value);

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator -(MatrixXD minuend, MatrixXD subend) => Subtract(minuend, subend);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator *(MatrixXD multiplicand, double multiplier) => Scale(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator *(double multiplicand, MatrixXD multiplier) => Scale(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorXD operator *(MatrixXD multiplicand, VectorXD multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorXD operator *(VectorXD multiplicand, MatrixXD multiplier) => Multiply(multiplier, multiplicand);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD operator *(MatrixXD multiplicand, MatrixXD multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(MatrixXD left, MatrixXD right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(MatrixXD left, MatrixXD right) => !(left == right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Array" /> to <see cref="MatrixXD" />.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator MatrixXD(double[,] array) => ToMatrix(array);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Array" /> to <see cref="MatrixXD" />.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator MatrixXD(double[][] array) => ToMatrix(array);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Plus(MatrixXD value) => Operations.Plus(value.Values);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Add(MatrixXD augend, MatrixXD addend) => Operations.Add(augend.Values, addend.Values);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Negate(MatrixXD value) => Operations.Negate(value.Values);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Subtract(MatrixXD minuend, MatrixXD subend) => Operations.Subtract(minuend.Values, subend.Values);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Scale(MatrixXD multiplicand, double multiplier) => Operations.Scale(multiplicand.Values, multiplier);

        /// <summary>
        /// Scales the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Scale(double multiplicand, MatrixXD multiplier) => Operations.Scale(multiplicand, multiplier.Values);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VectorXD Multiply(MatrixXD multiplicand, VectorXD multiplier) => Operations.Multiply(multiplicand.Values, multiplier.Values);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VectorXD Multiply(VectorXD multiplicand, MatrixXD multiplier) => Operations.Multiply(multiplicand.Values, multiplier.Values);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MatrixXD Multiply(MatrixXD multiplicand, MatrixXD multiplier) => Operations.Multiply(multiplicand.Values, multiplier.Values);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is MatrixXD matrix && Equals(matrix);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(MatrixXD other) => other is MatrixXD matrix && EqualityComparer<double[][]>.Default.Equals(Values, matrix.Values);

        /// <summary>
        /// Converts to matrix.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD ToMatrix(double[,] array) => new MatrixXD(array);

        /// <summary>
        /// Converts to matrix.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MatrixXD ToMatrix(double[][] array) => new MatrixXD(array);
        #endregion

        /// <summary>
        /// Dots the product.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DotProduct(MatrixXD b) => Operations.DotProduct(Values, b.Values);

        /// <summary>
        /// Lerps the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixXD Lerp(MatrixXD b, double t) => Operations.Lerp(Values, b.Values, t);

        #region Standard Methods
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IEnumerable<double>> GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Values);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            for (var i = 0; i < Rows; i++)
            {
                sb.Append("{");
                for (var j = 0; j < Columns; j++)
                {
                    sb.Append($"{Values[i][j].ToString(format, formatProvider)},\t");
                }

                sb.Append("},");
            }

            sb.Append("}");

            return sb.ToString();
        }
        #endregion
    }
}
