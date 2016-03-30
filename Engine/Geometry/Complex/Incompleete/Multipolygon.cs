// <copyright file="Multipolygon.cs" company="Shkyrockett">
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
    /// Structure of a Closed Polygon
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Multi-polygon")]
    public class Multipolygon
        : Shape
    {
        /// <summary>
        /// An array of points representing a polygon.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public new PointF[] Points;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Multipolygon";
        }
    }
}
