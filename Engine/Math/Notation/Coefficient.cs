// <copyright file="Coefficient.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Coefficient
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Term"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The value of the <see cref="Coefficient"/>.
        /// </summary>
        public decimal Value { get; set; }
    }
}
