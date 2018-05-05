﻿// <copyright file="CubicBSpline.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName("Cubic B Spline")]
    public class CubicBSpline
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public CubicBSpline()
            : this(new List<Point2D> { Point2D.Empty })
        { }

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
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Interpolators.CubicBSpline(NodePoints, t);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(CubicBSpline);
            return $"{nameof(CubicBSpline)}{{{NodePoints.ToString()}}}";
        }
    }
}