// <copyright file="Relations.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The relations enum.
/// </summary>
public enum Relation
    : sbyte
{
    /// <summary>
    /// The LessThan = -1.
    /// </summary>
    LessThan = -1,

    /// <summary>
    /// The EqualTo = 0.
    /// </summary>
    EqualTo = 0,

    /// <summary>
    /// The GreaterThan = 1.
    /// </summary>
    GreaterThan = 1,
}
