﻿using Engine.Geometry;
using Engine.Objects;
using System.Collections.Generic;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Term
        : GraphicsObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficient"></param>
        /// <param name="variables"></param>
        public Term(Coefficient coefficient, List<Variable> variables)
            : this(Point2D.Empty, coefficient, variables)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="coefficient"></param>
        /// <param name="variables"></param>
        public Term(Point2D location, Coefficient coefficient, List<Variable> variables)
        {
            Location = location;
            Coefficient = coefficient;
            Variables = variables;
        }

        /// <summary>
        /// The location the <see cref="Term"/> should be printed.
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// The constant number or <see cref="Coefficient"/> of the term.
        /// </summary>
        public Coefficient Coefficient { get; set; }

        /// <summary>
        /// The list of variables of the <see cref="Term"/>.
        /// </summary>
        public List<Variable> Variables { get; set; }
    }
}
