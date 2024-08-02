// <copyright file="RotationDirections.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The rotation directions enum.
/// </summary>
public enum RotationDirection
    : sbyte
{
    /// <summary>
    /// The object is rotating over the top to the left.
    /// </summary>
    CounterClockwise = -1,

    /// <summary>
    /// The object is rotating over the top to the right.
    /// </summary>
    Clockwise = 1,
}
