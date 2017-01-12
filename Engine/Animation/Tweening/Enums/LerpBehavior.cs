// <copyright file="LerpBehavior.cs" company="Shkyrockett" >
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
    /// Lerp behavior 
    /// </summary>
    [Flags]
    public enum LerpBehavior
    {
        /// <summary>
        /// No special behaviors.
        /// </summary>
        None = 0,

        /// <summary>
        /// Reflect the interpolation back at the end.
        /// </summary>
        Reflect = 1,

        /// <summary>
        /// Rotation interpolation.
        /// </summary>
        Rotation = 2,

        /// <summary>
        /// Use Radians.
        /// </summary>
        RotationRadians = 4,

        /// <summary>
        /// Use Degrees.
        /// </summary>
        RotationDegrees = 8,

        /// <summary>
        /// Round result to integers.
        /// </summary>
        Round = 16
    }
}
