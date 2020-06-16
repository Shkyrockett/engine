// <copyright file="Rank3Tensor.cs" company="Shkyrockett" >
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    public struct Rank3Tensor
        : IEquatable<Rank3Tensor>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Rank3Tensor" /> struct.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="slices">The slices.</param>
        public Rank3Tensor(int rows, int columns, int slices)
        {
            Values = new double[rows, columns, slices];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rank3Tensor"/> struct.
        /// </summary>
        /// <param name="values">The values.</param>
        public Rank3Tensor(double[,,] values)
        {
            Values = values;
        }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="System.Double" /> with the specified index1.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double" />.
        /// </value>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        /// <param name="index3">The index3.</param>
        /// <returns></returns>
        public double this[int index1, int index2, int index3]
        {
            get { return Values[index1, index2, index3]; }
            set { Values[index1, index2, index3] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public double[,,] Values { get; set; }
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
        public static bool operator ==(Rank3Tensor left, Rank3Tensor right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Rank3Tensor left, Rank3Tensor right) => !(left == right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Array" /> to <see cref="Rank3Tensor" />.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Rank3Tensor(double[,,] array) => ToRank3Tensor(array);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj) => obj is Rank3Tensor tensor && Equals(tensor);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Rank3Tensor other) => EqualityComparer<double[,,]>.Default.Equals(Values, other.Values);

        /// <summary>
        /// Converts to rank3tensor.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static Rank3Tensor ToRank3Tensor(double[,,] array) => new Rank3Tensor(array);
        #endregion

        #region Standard Methods
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Values);
        #endregion
    }
}
