// <copyright file="Vector.cs" company="Shkyrockett" >
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public struct Vector
        : IEquatable<Vector>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector(double[] values)
        {
            Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public Vector(int rows)
        {
            Values = new double[rows];
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="System.Double"/> with the specified index1.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public double this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public double[] Values { get; internal set; }
        #endregion

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Vector left, Vector right) => EqualityComparer<Vector>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Vector left, Vector right) => !(left == right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Array" /> to <see cref="Vector" />.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Vector(double[] array) => ToVector(array);
        #endregion

        #region Operator Backing MEthods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj) => obj is Vector vector && Equals(vector);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Vector other) => other != null && EqualityComparer<double[]>.Default.Equals(Values, other.Values);

        /// <summary>
        /// Converts to vector.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        private static Vector ToVector(double[] array) => new Vector(array);
        #endregion

        #region Standard Methods
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Values);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
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
            for (var i = 0; i < Values.Length; i++)
            {
                sb.Append($"{Values[i].ToString(format, formatProvider)},\t");
            }

            sb.Append("}");

            return sb.ToString();
        }
        #endregion
    }
}
