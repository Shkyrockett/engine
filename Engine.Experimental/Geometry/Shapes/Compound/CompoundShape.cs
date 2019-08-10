using System;
using System.Collections.Generic;

namespace Engine.Experimental
{
    /// <summary>
    /// The compound shape struct.
    /// </summary>
    public struct CompoundShape<T>
        : IEquatable<CompoundShape<T>> where T: IPrimitive
    {
        /// <summary>
        /// Gets or sets the primitives.
        /// </summary>
        public T[] primitives { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(CompoundShape<T> left, CompoundShape<T> right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(CompoundShape<T> left, CompoundShape<T> right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is CompoundShape<T> shape && Equals(shape);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(CompoundShape<T> other) => EqualityComparer<T[]>.Default.Equals(primitives, other.primitives);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => 1865477943 + EqualityComparer<T[]>.Default.GetHashCode(primitives);
    }
}
