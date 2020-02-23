// <copyright file="AddPointResult.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

using System;

namespace Engine
{
    /// <summary>
    /// Changes made to the CurveBuilder.curves list after a call to <see cref="CurveBuilder.AddPoint" />.
    /// This seems like a prime candidate for an F#-style discriminated union/algebraic data type.
    /// </summary>
    /// <seealso cref="IEquatable{T}" />
    public struct AddPointResult : IEquatable<AddPointResult>
    {
        #region Fields
        /// <summary>
        /// packed value... need this so that default(AddPointResult) which is always 0 to represent no change
        /// </summary>
        private readonly int data;

        /// <summary>
        /// No changes were made.
        /// </summary>
        public static readonly AddPointResult NO_CHANGE = default;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPointResult" /> class.
        /// </summary>
        /// <param name="firstChangedIndex">The firstChangedIndex.</param>
        /// <param name="curveAdded">The curveAdded.</param>
        /// <exception cref="InvalidOperationException">firstChangedIndex must be greater than zero</exception>
        public AddPointResult(int firstChangedIndex, bool curveAdded)
        {
            if (firstChangedIndex < 0 || firstChangedIndex == int.MaxValue)
            {
                throw new InvalidOperationException("firstChangedIndex must be greater than zero");
            }

            data = (firstChangedIndex + 1) * (curveAdded ? -1 : 1);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Index into curves array of first curve that was changed, or -1 if no curves were changed.
        /// All curves after this are assumed to have changed/been added as well. If a curve was added
        /// this is a considered a "change" so <see cref="WasAdded" /> will always be true.
        /// </summary>
        /// <value>
        /// The first index of the changed.
        /// </value>
        public int FirstChangedIndex
            => Math.Abs(data) - 1;

        /// <summary>
        /// Were any curves added?
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if [was added]; otherwise, <see langword="false"/>.
        /// </value>
        public bool WasAdded
            => data < 0;

        /// <summary>
        /// Were any curves changed or added?
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if [was changed]; otherwise, <see langword="false"/>.
        /// </value>
        public bool WasChanged
            => data != 0;
        #endregion Properties

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(AddPointResult left, AddPointResult right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(AddPointResult left, AddPointResult right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is AddPointResult result && Equals(result);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(AddPointResult other) => data == other.data;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => 1768953197 + data.GetHashCode();
    }
}
