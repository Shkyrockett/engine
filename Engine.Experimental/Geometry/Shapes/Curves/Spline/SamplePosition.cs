// <copyright file="SamplePosition.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

using System;
using System.Diagnostics.CodeAnalysis;

namespace Engine
{
    /// <summary>
    /// Point at which to sample the spline.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    public struct SamplePosition : IEquatable<SamplePosition>
    {
        #region Fields
        /// <summary>
        /// Index of sampled curve in the spline curves array.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The "t" value from which to sample the curve.
        /// </summary>
        public readonly double Time;
        #endregion Fields

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(SamplePosition left, SamplePosition right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(SamplePosition left, SamplePosition right) => !(left == right);

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePosition" /> class.
        /// </summary>
        /// <param name="curveIndex">The curveIndex.</param>
        /// <param name="t">The t.</param>
        public SamplePosition(int curveIndex, double t)
        {
            Index = curveIndex;
            Time = t;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is SamplePosition position && Equals(position);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] SamplePosition other) => Index == other.Index && Time == other.Time;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Index, Time);
        #endregion Constructors
    }
}
