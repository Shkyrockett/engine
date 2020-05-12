// <copyright file="Incidence.cs" company="Shkyrockett" >
//     Copyright © 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// Angle of Incidence Categorization.
    /// </summary>
    public enum Incidence
        : sbyte
    {
        /// <summary>
        /// The angle of incidence is oblique.
        /// </summary>
        Oblique = -1,

        /// <summary>
        /// The angle of incidence is parallel.
        /// </summary>
        Parallel = 0,

        /// <summary>
        /// The angle of incidence is perpendicular.
        /// </summary>
        Perpendicular = 1,
    }
}
