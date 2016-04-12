// <copyright file="Shape.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
// </copyright> 
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Drawing;

namespace Engine.Geometry
{
    /// <summary>
    /// Base <see cref="Shape"/> class for using as a template for various shapes.
    /// </summary>
    public abstract class Shape
        :IShape
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual RectangleF Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool HitTest(Point point)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool HitTest(PointF point)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render the shape to the canvas.
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> object to draw on.</param>
        /// <param name="style">The <see cref="IStyle"/> to use to draw the shape.</param>
        public virtual void Render(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
