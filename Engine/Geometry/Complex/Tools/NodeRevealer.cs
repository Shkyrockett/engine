// <copyright file="NodeRevealer.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
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
        /// <summary>
        /// 
        /// </summary>
        public NodeRevealer()
        {
            Points = new List<Point2D>();
            Radius = 0;
        }

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

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public double Radius { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                if (Points == null) return null;
                var boundings = Boundings.Polygon(Points);
                boundings?.Inflate(Radius, Radius);
                return boundings;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool Contains(Point2D point)
        {
            foreach (var pt in Points)
            {
                if (Containings.CirclePoint(pt.X, pt.Y, Radius, point.X, point.Y) != Inclusion.Outside) return true;
            }

            return false;
        }
    }
}
