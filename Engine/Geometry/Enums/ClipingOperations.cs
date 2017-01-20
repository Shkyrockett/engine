// <copyright file="ClipingOperations.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
{
    /// <summary>
    /// An enumeration of Boolean Clipping Operations that can be performed on geometry.
    /// </summary>
    public enum ClipingOperations
        : byte
    {
        /// <summary>
        /// Locate the geometry that is the intersection of the two polygons.
        /// </summary>
        Intersection = 0,

        /// <summary>
        /// Locate the geometry of one polygon that does not overlap the other.
        /// </summary>
        Difference = 1,

        /// <summary>
        /// Combine geometry.
        /// </summary>
        Union = 2,

        /// <summary>
        /// Locate the geomertry that does not overlap.
        /// </summary>
        Xor = 3
    }
}
