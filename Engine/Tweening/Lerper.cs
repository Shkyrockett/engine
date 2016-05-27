// <copyright file="Lerper.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

using System;

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Lerper
    {
        /// <summary>
        /// 
        /// </summary>
		[Flags]
        public enum Behavior
        {
            /// <summary>
            /// 
            /// </summary>
			None = 0,

            /// <summary>
            /// 
            /// </summary>
			Reflect = 1,

            /// <summary>
            /// 
            /// </summary>
			Rotation = 2,

            /// <summary>
            /// 
            /// </summary>
			RotationRadians = 4,

            /// <summary>
            /// 
            /// </summary>
			RotationDegrees = 8,

            /// <summary>
            /// 
            /// </summary>
			Round = 16
        }

        /// <summary>
        /// 
        /// </summary>
        protected const double DEG = 180d / Math.PI;

        /// <summary>
        /// 
        /// </summary>
		protected const double RAD = Math.PI / 180d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public abstract void Initialize(object fromValue, object toValue, Behavior behavior);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="currentValue"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
		public abstract object Interpolate(double t, object currentValue, Behavior behavior);
    }
}
