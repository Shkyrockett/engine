﻿// <copyright file="Equation.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.MathNotation;

/// <summary>
/// The equation class.
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
    public List<EquationOperator> Operations { get; set; }

    /// <summary>
    /// The expression the <see cref="Equation"/> should contain.
    /// </summary>
    public List<Expression> Expressions { get; set; }
}
