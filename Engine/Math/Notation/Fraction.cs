// <copyright file="Fraction.cs" company="Shkyrockett" >
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
    public class Fraction
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Logarithm"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The top number of the <see cref="Fraction"/>.
        /// </summary>
        public Expression Numerator { get; set; }

        /// <summary>
        /// The bottom number of the <see cref="Fraction"/>.
        /// </summary>
        public Expression Denominator { get; set; }
    }
}
