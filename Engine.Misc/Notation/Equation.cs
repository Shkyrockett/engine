// <copyright file="Equation.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Equation
        : GraphicsObject
    {
        /// <summary>
        /// The location the <see cref="Term"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The list of comparison operators to apply to the expressions.
        /// </summary>
        public List<EquationOperators> Operations { get; set; }

        /// <summary>
        /// The expression the <see cref="Equation"/> should contain.
        /// </summary>
        public List<Expression> Expressions { get; set; }
    }
}
