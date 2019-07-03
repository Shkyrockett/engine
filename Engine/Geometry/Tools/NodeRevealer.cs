// <copyright file="NodeRevealer.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The node revealer class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class NodeRevealer
        : Shape
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRevealer"/> class.
        /// </summary>
        public NodeRevealer()
            : this(new List<Point2D>(), 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRevealer"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="radius">The radius.</param>
        public NodeRevealer(Point2D point, double radius)
            : this(new List<Point2D> { point }, radius)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRevealer"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="radius">The radius.</param>
        public NodeRevealer(Point2D[] points, double radius)
            : this(new List<Point2D>(points), radius)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeRevealer"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="radius">The radius.</param>
        public NodeRevealer(IList<Point2D> points, double radius)
        {
            Points = points.ToList();
            Radius = radius;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="radius">The radius.</param>
        public void Deconstruct(out List<Point2D> points, out double radius)
        {
            points = Points;
            radius = Radius;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public bool ConnectPoints { get; set; } = true;

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                if (Points is null)
                {
                    return null;
                }

                var boundings = Measurements.PolygonBounds(Points);
                boundings?.Inflate(Radius, Radius);
                return boundings;
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Contains(Point2D point)
        {
            foreach (var pt in Points)
            {
                if (Intersections.CircleContainsPoint(pt.X, pt.Y, Radius, point.X, point.Y) != Inclusion.Outside)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion Methods
    }
}
