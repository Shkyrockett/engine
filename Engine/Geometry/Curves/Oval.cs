// <copyright file="Oval.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Oval")]
    public class Oval
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private Point2D location;

        /// <summary>
        /// 
        /// </summary>
        private Size2D size;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="size"></param>
        public Oval(Point2D location, Size2D size)
        {
            this.location = location;
            this.size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size2D Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(location, size); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(Oval);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Oval), nameof(Location), location, nameof(Size), size);
        }
    }
}
