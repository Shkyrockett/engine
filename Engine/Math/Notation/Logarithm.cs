// <copyright file="Logarithm.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
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
    public class Logarithm
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Logarithm"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The base of the <see cref="Logarithm"/>.
        /// </summary>
        public Expression Base { get; set; }

        /// <summary>
        /// The expression the group should contain.
        /// </summary>
        public Expression Expression { get; set; }
    }
}
