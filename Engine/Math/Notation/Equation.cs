﻿using Engine.Geometry;
using Engine.Objects;
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
