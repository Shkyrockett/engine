﻿// <copyright file="Heart.cs" company="Shkyrockett" >
// Copyright © 2015 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.Serialization;

namespace Engine;

/// <summary>
/// The heart class.
/// </summary>
/// <summary>
/// http://csharphelper.com/blog/2016/02/draw-parametric-heart-shaped-curve-c/
/// </summary>
[DataContract, Serializable]
//[GraphicsObject]
public class Heart
    : Shape2D
{
    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString() => nameof(Heart);
}
