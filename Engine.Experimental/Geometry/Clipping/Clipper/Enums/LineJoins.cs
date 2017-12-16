// <copyright file="LineJoins.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// The line joins enum.
    /// </summary>
    public enum LineJoins
        : byte
    {
        /// <summary>
        /// Use a Miter joint at the corners.
        /// </summary>
        Miter = 0,

        /// <summary>
        /// Use a Bevel joint at the corners.
        /// </summary>
        Bevel = 1,

        /// <summary>
        /// Use a Round joint at the corners.
        /// </summary>
        Round = 2,

        /// <summary>
        /// Use a Square joint at the corners.
        /// </summary>
        Square = 3
    }
}
