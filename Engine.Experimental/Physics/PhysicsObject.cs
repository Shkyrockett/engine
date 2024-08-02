// <copyright file="PhysicsObject.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine;

/// <summary>
/// The physics object class.
/// </summary>
public class PhysicsObject<T>
    where T : INumber<T>
{
    /// <summary>
    /// The p.
    /// </summary>
    public Momentum<T> p; // p = momentum.
}
