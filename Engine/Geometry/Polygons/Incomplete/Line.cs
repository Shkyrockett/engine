// <copyright file="Line.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 2D Line Structure
    /// </summary>
    /// <structure>Engine.Geometry.Line2D</structure>
    /// <remarks></remarks>
    [Serializable()]
    public class Line
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute()]
        private Point location;

        /// <summary>
        /// 
        /// </summary>
        private Vector vector;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public Line()
        {
            location = Point.Empty;
            vector = Vector.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="vector"></param>
        /// <remarks></remarks>
        public Line(Point location, Vector vector)
        {
            this.location = location;
            this.vector = vector;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute()]
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute()]
        public Vector Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Line";
        }
    }
}
