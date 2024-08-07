﻿// <copyright file="LerpBehavior.cs" company="Shkyrockett" >
// Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://github.com/jacobalbano/glide </remarks>

namespace Engine;

/// <summary>
/// Lerp behavior 
/// </summary>
[Flags]
public enum LerpBehaviors
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
