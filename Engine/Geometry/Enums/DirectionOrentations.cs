// <copyright file="DirectionOrentations.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public enum DirectionOrentations
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
}
