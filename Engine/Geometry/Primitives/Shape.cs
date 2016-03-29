// <copyright file="Shape.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright> 
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// Base <see cref="Shape"/> class for using as a template for various shapes.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        /// <param name="pen">The <see cref="Pen"/> to use to draw the shape.</param>
        /// <param name="brush">The <see cref="Brush"/> to use to fill the shape.</param>
        public void Render(Graphics g, Pen pen, Brush brush)
        {
            throw new NotImplementedException();
        }
    }
}
