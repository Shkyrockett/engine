// <copyright file="Experiments.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
{
    /// <summary>
    /// Enumeration of the inclusion of a point within a shape.
    /// </summary>
    public enum Inclusion
    {
        /// <summary>
        /// Point lies outside the shape.
        /// </summary>
        Outside = 0,

        /// <summary>
        /// Point is contained inside the shape.
        /// </summary>
        Inside = 1,

        /// <summary>
        /// Touches the boundary of the shape.
        /// </summary>
        Boundary = -1,
    }
}
