﻿// <copyright file="ClipingOperations.cs" >
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
/// An enumeration of Boolean Clipping Operations that can be performed on geometry.
/// </summary>
public enum ClippingOperation
    : byte
{
    /// <summary>
    /// No Intersection.
    /// </summary>
    None = 0,

    /// <summary>
    /// Locate the geometry that is the intersection of the two polygons.
    /// </summary>
    Intersection = 1,

    /// <summary>
    /// Locate the geometry of one polygon that does not overlap the other.
    /// </summary>
    Difference = 2,

    /// <summary>
    /// Combine geometry.
    /// </summary>
    Union = 3,

    /// <summary>
    /// Locate the geometry that does not overlap.
    /// </summary>
    Xor = 4
}
