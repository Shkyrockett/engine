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
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Triangle()
            : this(PointF.Empty, PointF.Empty, PointF.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public Triangle(PointF a, PointF b, PointF c)
            : base(new List<PointF>() { a, b, c })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Triangle(List<PointF> points)
            : base(points)
        {
            if (points.Count > 3) throw new IndexOutOfRangeException();
            if (points.Count < 3) throw new IndexOutOfRangeException();
            base.Points = points;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public PointF A
        {
            get { return base.Points[0]; }
            set { base.Points[0] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF B
        {
            get { return base.Points[1]; }
            set { base.Points[1] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PointF C
        {
            get { return base.Points[2]; }
            set { base.Points[2] = value; }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public override List<PointF> Points
        //{
        //    get { return base.Points; }
        //    set { base.Points = value; }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public override RectangleF Bounds
        //{
        //    get
        //    {
        //        float left = base.Points[0].X;
        //        float top = base.Points[0].Y;
        //        float right = base.Points[0].X;
        //        float bottom = base.Points[0].Y;

        //        foreach (PointF point in base.Points)
        //        {
        //            // ToDo: Measure performance impact of overwriting each time.
        //            left = point.X <= left ? point.X : left;
        //            top = point.Y <= top ? point.Y : top;
        //            right = point.X >= right ? point.X : right;
        //            bottom = point.Y >= bottom ? point.Y : bottom;
        //        }

        //        return RectangleF.FromLTRB(left, top, right, bottom);
        //    }
        //}
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Triangle";
            StringBuilder pts = new StringBuilder();
            foreach (PointF pt in base.Points)
            {
                pts.Append(pt.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format("{0}{{{1}}}", "Triangle", pts.ToString());
        }
        #endregion
    }
}
