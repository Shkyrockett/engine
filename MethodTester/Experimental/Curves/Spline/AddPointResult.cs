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
    /// Changes made to the CurveBuilder.curves list after a call to <see cref="CurveBuilder.AddPoint"/>.
    /// This seems like a prime candidate for an F#-style discriminated union/algebraic data type.
    /// </summary>
    public struct AddPointResult
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
        /// 
        /// </summary>
        /// <param name="firstChangedIndex"></param>
        /// <param name="curveAdded"></param>
        public AddPointResult(int firstChangedIndex, bool curveAdded)
        {
            if (firstChangedIndex < 0 || firstChangedIndex == int.MaxValue)
                throw new InvalidOperationException("firstChangedIndex must be greater than zero");
            data = (firstChangedIndex + 1) * (curveAdded ? -1 : 1);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Index into curves array of first curve that was changed, or -1 if no curves were changed.
        /// All curves after this are assumed to have changed/been added as well. If a curve was added
        /// this is a considered a "change" so <see cref="WasAdded"/> will always be true.
        /// </summary>
        public int FirstChangedIndex
            => Math.Abs(data) - 1;

        /// <summary>
        /// Were any curves added?
        /// </summary>
        public bool WasAdded
            => data < 0;

        /// <summary>
        /// Were any curves changed or added?
        /// </summary>
        public bool WasChanged
            => data != 0;
        #endregion Properties
    }
}
