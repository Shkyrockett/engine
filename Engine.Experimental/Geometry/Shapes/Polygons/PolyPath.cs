// <copyright file="Path.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
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
/// The poly path class.
/// </summary>
[DataContract, Serializable]
//[GraphicsObject]
public class PolyPath
        : Shape2D
{
    /// <summary>
    /// Gets or sets the shapes.
    /// </summary>
    public List<Shape2D> Shapes { get; set; }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>The <see cref="string"/>.</returns>
    public override string ToString() => "Path";
}
