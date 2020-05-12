// <copyright file="JenkinsHash.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> Adapted from:https://github.com/burningmime/curves</remarks>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// Simple implementation of Jenkin's hashing algorithm.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    /// <remarks>
    /// <para>http://en.wikipedia.org/wiki/Jenkins_hash_function</para>
    /// </remarks>
    public struct JenkinsHash
        : IEquatable<JenkinsHash>
    {
        /// <summary>
        /// The current.
        /// </summary>
        private int current;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(JenkinsHash left, JenkinsHash right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(JenkinsHash left, JenkinsHash right) => !(left == right);

        /// <summary>
        /// The mixin.
        /// </summary>
        /// <param name="hash">The hash.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Mixin(int hash)
        {
            unchecked
            {
                var num = current;
                if (num == 0)
                {
                    num = 0x7e53a269;
                }
                else
                {
                    num *= -0x5aaaaad7;
                }

                num += hash;
                num += num << 10;
                num ^= num >> 6;
                current = num;
            }
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetValue()
        {
            unchecked
            {
                var num = current;
                num += num << 3;
                num ^= num >> 11;
                num += num << 15;
                return num;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj) => obj is JenkinsHash hash && Equals(hash);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] JenkinsHash other) => current == other.current;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(current);
    }
}
