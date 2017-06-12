// <copyright file="Variable.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public class Variable
        : GraphicsObject
    {
        /// <summary>
        /// The location the Variable should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The superscripted exponential expression.
        /// </summary>
        public Expression Exponent { get; set; }

        /// <summary>
        /// The name of the <see cref="Variable"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The subscripted sequence identifier.
        /// </summary>
        public Expression Sequence { get; set; }
    }
}
