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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
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
            : base()
        {
            Points = points;
            Radius = radius;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="radius"></param>
        public void Deconstruct(out List<Point2D> points, out double radius)
        {
            points = this.Points;
            radius = this.Radius;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public bool ConnectPoints { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
