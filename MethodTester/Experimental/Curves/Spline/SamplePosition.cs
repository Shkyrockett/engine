// <copyright file="SamplePosition.cs" >
//     Copyright (c) 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

namespace Engine
{
    /// <summary>
    /// Point at which to sample the spline.
    /// </summary>
    public struct SamplePosition
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

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curveIndex"></param>
        /// <param name="t"></param>
        public SamplePosition(int curveIndex, double t)
        {
            Index = curveIndex;
            Time = t;
        }

        #endregion
    }
}
