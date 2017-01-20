﻿// <copyright file="IRenderable.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Drawing;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Gets or sets the array of handles for the shape.
        /// </summary>
        List<PointF> Handles { get; set; }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        void Render(Graphics g);

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        /// <param name="pen">The <see cref="Pen"/> to use to draw the shape.</param>
        /// <param name="brush">The <see cref="Brush"/> to use to fill the shape.</param>
        void Render(Graphics g, Pen pen, Brush brush);

        /// <summary>
        /// Render the grip handles to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw the grip handles on.</param>
        /// <param name="pen">The <see cref="Pen"/> to use to draw the shape grip handles.</param>
        /// <param name="brush">The <see cref="Brush"/> to use to fill the shape grip handles.</param>
        void RenderHandles(Graphics g, Pen pen, Brush brush);
    }
}
