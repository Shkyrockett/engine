// <copyright file="Shape.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright> 
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Objects;
using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// Base <see cref="Shape"/> class for using as a template for various shapes.
    /// </summary>
    public abstract class Shape
        : GraphicsObject, IShape
    {
    }
}
