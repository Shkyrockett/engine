// <copyright file="PolylineSet.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// Set of a open Polyline structures
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PolylineSet))]
    public class PolylineSet
        : Shape
    {
        #region Private Fields

        /// <summary>
        /// An array of Polygons representing a set.
        /// </summary>
        /// <remarks></remarks>
        private List<Polyline> polylines;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a default instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        public PolylineSet()
        {
            polylines = new List<Polyline>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineSet"/> class.
        /// </summary>
        public PolylineSet(List<Polyline> polylines)
        {
            this.polylines = polylines;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public List<Polyline> Polylines
        {
            get { return polylines; }
            set
            {
                polylines = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore]
        public int Count => polylines.Count;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [XmlIgnore]
        public double Perimeters => polylines.Sum(p => p.Perimeter);

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        [XmlIgnore]
        public override Rectangle2D Bounds
        {
            get
            {
                Rectangle2D bounds = polylines[0].Bounds;

                foreach (Polyline polyline in polylines)
                    bounds.Union(polyline.Bounds);

                return bounds;
            }
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public void Add(Polyline polyline)
        {
            polylines.Add(polyline);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return nameof(PolylineSet);
            var pointsString = new StringBuilder();
            foreach (Polyline polyline in Polylines)
            {
                pointsString.Append(polyline.ToString());
                pointsString.Append(",");
            }
            if (pointsString.Length > 0) pointsString.Remove(pointsString.Length - 1, 1);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}}}", nameof(PolylineSet), pointsString.ToString());
        }

        #endregion
    }
}
