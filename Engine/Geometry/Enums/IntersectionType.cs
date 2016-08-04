// <copyright file="IntersectionType.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Geometry
{
    /// <summary>
    /// Some intersection possibilities
    /// </summary>
    /// <remarks></remarks>
    public enum IntersectionType
    {
        /// <summary>
        /// Plane Intersection
        /// </summary>
        Plane,

        /// <summary>
        /// Cross Intersection
        /// </summary>
        Cross,

        /// <summary>
        /// Parallel Intersection
        /// </summary>
        Parallel,
    }
}
