﻿// <copyright file="CubicBSpline.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author>Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Cubic B Spline")]
    public class CubicBSpline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public CubicBSpline()
        {
            NodePoints = new List<Point2D> { Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public CubicBSpline(List<Point2D> points)
        {
            NodePoints = points;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> NodePoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double index) => Interpolaters.CubicBSpline(NodePoints, index);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(CubicBSpline);
            return string.Format("{0}{{{1}}}", nameof(CubicBSpline), NodePoints.ToString());
        }
    }
}
