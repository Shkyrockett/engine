// <copyright file="MatrixTypes.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// MatrixTypes
/// </summary>
[Flags]
public enum MatrixTypes
    : byte
{
    /// <summary>
    /// The Unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The Identity.
    /// </summary>
    Identity = 1,

    /// <summary>
    /// The Translation.
    /// </summary>
    Translation = 2,

    /// <summary>
    /// The Scaling.
    /// </summary>
    Scaling = 4
}
