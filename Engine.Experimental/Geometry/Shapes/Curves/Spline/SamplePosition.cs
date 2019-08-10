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

namespace Engine
{
    /// <summary>
    /// Point at which to sample the spline.
    /// </summary>
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

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SamplePosition"/> class.
        /// </summary>
        /// <param name="curveIndex">The curveIndex.</param>
        /// <param name="t">The t.</param>
        public SamplePosition(int curveIndex, double t)
        {
            Index = curveIndex;
            Time = t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(SamplePosition left, SamplePosition right) => left.Equals(right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(SamplePosition left, SamplePosition right) => !(left == right);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is SamplePosition position && Equals(position);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SamplePosition other) => Index == other.Index && Time == other.Time;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 1621472519;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            return hashCode;
        }
        #endregion Constructors
    }
}
