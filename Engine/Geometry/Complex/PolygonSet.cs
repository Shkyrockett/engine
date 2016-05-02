﻿// <copyright file="Multipolygon.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Set of a Closed Polygon structures
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>
    /// <remarks></remarks>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(PolygonSet))]
    public class PolygonSet
        : Shape
    {
        /// <summary>
        /// An array of Polygons representing a set.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        private List<Polygon> polygons;

        /// <summary>
        /// Initializes a default instance of the <see cref="PolygonSet"/> class.
        /// </summary>
        public PolygonSet()
        {
            polygons = new List<Polygon>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonSet"/> class.
        /// </summary>
        public PolygonSet(List<Polygon> polygons)
        {
            this.polygons = polygons;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Polygon> Polygons
        {
            get { return polygons; }
            set { polygons = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return polygons.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = polygons[0].Bounds;

                foreach (Polygon polygon in polygons)
                {
                    bounds.Union(polygon.Bounds);
                }

                return bounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ShapeStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public void Add(Polygon polygon)
        {
            polygons.Add(polygon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(PolygonSet);
            StringBuilder pts = new StringBuilder();
            foreach (Polygon pn in Polygons)
            {
                pts.Append(pn.ToString());
                pts.Append(",");
            }
            if (pts.Length > 0) pts.Remove(pts.Length - 1, 1);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}}}", nameof(PolygonSet), pts.ToString());
        }
    }
}
