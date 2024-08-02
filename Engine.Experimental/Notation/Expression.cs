// <copyright file="Expression.cs" company="Shkyrockett" >
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
/// The expression class.
/// </summary>
public class Expression
    : GraphicsObject
{
    /// <summary>
    /// The location the <see cref="Expression"/> should be printed.
    /// </summary>
    public Point2D Location { get; set; }

    /// <summary>
    /// The collection of <see cref="Terms"/> that make up the <see cref="Expression"/>.
    /// </summary>
    public List<Term> Terms { get; set; }
}
