// <copyright file="Fraction.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.MathNotation
{
    /// <summary>
    /// The fraction class.
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
