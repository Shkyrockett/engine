// <copyright file="Shape2D.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// Base <see cref="Shape2D"/> class for using as a template for various shapes.
    /// </summary>
    /// <seealso cref="Engine.GraphicsObject" />
    /// <seealso cref="Engine.IShape" />
    public abstract class Shape2D
        : GraphicsObject, IShape, IBoundable
    { }
}
