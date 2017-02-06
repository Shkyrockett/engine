// <copyright file="NodeRevealer.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class NodeRevealer
        : Shape
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NodeRevealer()
            : this(new List<Point2D>(), 0)
        { }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="locus"></param>
        ///// <param name="radius"></param>
        //public NodeRevealer(Locus locus, double radius)
        //{
        //    Radius = radius;
        //    ConnectPoints = true;
        //    switch (locus)
        //    {
        //        case PointLocus p:
        //            Points = new List<Point2D> { p };
        //            ConnectPoints = false;
        //            break;
        //        case LineSegmentLocus l:
        //            Points = l.Points;
        //            break;
        //        case PointSetLocus p:
        //            Points = p.Points;
        //            ConnectPoints = false;
        //            break;
        //        case PolylineLocus p:
        //            Points = p.Points;
        //            break;
        //        case PolygonLocus p:
        //            Points = p.Points;
        //            break;
        //        case PolylineSetLocus p:
        //            Points = ((PointSetLocus)p).Points;
        //            break;
        //        case PolygonSetLocus p:
        //            Points = ((PointSetLocus)p).Points;
        //            break;
        //        case ParallelLocus p:
        //        case OutsideLocus o:
        //        case EmptyLocus e:
        //        default:
        //            Points = new List<Point2D>();
        //            break;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        public NodeRevealer(Point2D point, double radius)
            : this(new List<Point2D> { point }, radius)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="radius"></param>
        public NodeRevealer(Point2D[] points, double radius)
            : this(new List<Point2D>(points), radius)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="radius"></param>
        public NodeRevealer(List<Point2D> points, double radius)
        {
            Points = points;
            Radius = radius;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute, SoapAttribute]
        public bool ConnectPoints { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                if (Points == null) return null;
                var boundings = Measurements.PolygonBounds(Points);
                boundings?.Inflate(Radius, Radius);
                return boundings;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
        {
            foreach (var pt in Points)
            {
                if (Intersections.CircleContainsPoint(pt.X, pt.Y, Radius, point.X, point.Y) != Inclusion.Outside) return true;
            }

            return false;
        }

        #endregion
    }
}
