// <copyright file="Triangle.cs">
//     Copyright (c) 2013 - 2016 Shkyrockett. All rights reserved.
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
    [DisplayName("Triangle")]
    public class Triangle
         : Polygon
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        public Triangle()
            : this(PointF.Empty, PointF.Empty, PointF.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Triangle(List<PointF> points)
            : base(points)
        {
            if (Points.Count > 3) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(PointF a, PointF b, PointF c)
            : base(new List<PointF>() { a, b, c })
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF A
        {
            get { return points[0]; }
            set { points[0] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF B
        {
            get { return points[1]; }
            set { points[1] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF C
        {
            get { return points[2]; }
            set { points[2] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public new List<PointF> Points
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
            if (this == null) return "Triangle";
            StringBuilder pts = new StringBuilder();
            foreach (PointF pt in points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", "Triangle", pts.ToString());
        }
    }
}
