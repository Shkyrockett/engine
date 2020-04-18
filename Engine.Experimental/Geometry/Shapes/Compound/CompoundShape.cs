using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Experimental
{
    /// <summary>
    /// The compound shape struct.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IEquatable{T}" />
    public struct CompoundShape<T>
        : IEquatable<CompoundShape<T>> where T : IPrimitive
    {
        /// <summary>
        /// Gets or sets the primitives.
        /// </summary>
        /// <value>
        /// The primitives.
        /// </value>
        public T[] Primitives { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(CompoundShape<T> left, CompoundShape<T> right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(CompoundShape<T> left, CompoundShape<T> right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is CompoundShape<T> shape && Equals(shape);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] CompoundShape<T> other) => EqualityComparer<T[]>.Default.Equals(Primitives, other.Primitives);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Primitives);
    }
}
