// <copyright file="Polygon.cs" company="Shkyrockett">
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
    [DisplayName("Polygon")]
    public class Polygon
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public Polygon()
        {
            points = new List<PointF>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polygon(List<PointF> points)
        {
            this.points = points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PointF this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
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
            if (this == null) return "Polygon";
            StringBuilder pts = new StringBuilder();
            foreach (PointF pt in points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", "Polygon", pts.ToString());
        }
    }
}
