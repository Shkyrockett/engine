// <copyright file="Polyline.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Polyline")]
    public class Polyline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public Polyline()
        {
            points = new List<PointF>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polyline(List<PointF> points)
        {
            this.points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<PointF> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Polyline";
            StringBuilder pts = new StringBuilder();
            foreach (PointF pt in points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", "Polyline", pts.ToString());
        }
    }
}
