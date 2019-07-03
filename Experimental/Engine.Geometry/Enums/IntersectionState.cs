// <copyright file="IntersectionState.cs" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// An enumeration of intersections between two shapes.
    /// </summary>
    [Flags]
    public enum IntersectionState
        : sbyte
    {
        /// <summary>
        /// One shape is outside of the other.
        /// </summary>
        Outside = -4,

        /// <summary>
        /// One shape is inside the other.
        /// </summary>
        Inside = -2,

        /// <summary>
        /// The Lines, Rays, or Line segments are parallel.
        /// </summary>
        Parallel = -1,

        /// <summary>
        /// There is no intersection of the two shapes.
        /// </summary>
        NoIntersection = 0,

        /// <summary>
        /// The shapes intersect at one or more locations.
        /// </summary>
        Intersection = 1,

        /// <summary>
        /// The shapes, or a portion of the shapes, are coincidental to each other.
        /// Intersecting along the path of the shapes. 
        /// </summary>
        Coincident = 2,

        /// <summary>
        /// A line, ray, or line segment is tangent to a curve.
        /// </summary>
        Tangent = 4,
    }
}
