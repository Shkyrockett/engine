// <copyright file="LerpBehavior.cs" >
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
    [Flags]
    public enum LerpBehavior
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
}
