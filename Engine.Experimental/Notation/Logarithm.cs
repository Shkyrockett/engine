﻿// <copyright file="Logarithm.cs" company="Shkyrockett" >
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
/// The logarithm class.
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
