// <copyright file="JenkinsHash.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> Adapted from:https://github.com/burningmime/curves</remarks>

using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// Simple implementation of Jenkin's hashing algorithm.
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Jenkins_hash_function
    /// </remarks>
    public struct JenkinsHash
    {
        /// <summary>
        /// The current.
        /// </summary>
        private int current;

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
        /// <returns>The <see cref="int"/>.</returns>
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
    }
}
