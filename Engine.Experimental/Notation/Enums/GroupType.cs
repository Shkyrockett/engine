// <copyright file="GroupTypes.cs" company="Shkyrockett" >
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
/// The group types enum.
/// </summary>
/// <remarks>
/// <para>http://symbolcodes.tlt.psu.edu/bylanguage/mathchart.html
/// https://www.splashmath.com/math-vocabulary/algebra/brackets</para>
/// </remarks>
public enum GroupType
{
    /// <summary>
    /// () Round brackets, or parenthesizes.
    /// </summary>
    Round,

    /// <summary>
    /// [] Square, box brackets, or braces.
    /// </summary>
    Square,

    /// <summary>
    /// {} Curly brackets.
    /// </summary>
    Curly,

    /// <summary>
    /// ⟨⟩ Angle brackets.
    /// </summary>
    Angle
}
