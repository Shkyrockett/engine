// <copyright file="PathItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// An abstract class representing a piece of a geometric figure.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(PathArc))]
    [XmlInclude(typeof(PathCardinal))]
    [XmlInclude(typeof(PathCubicBezier))]
    [XmlInclude(typeof(PathLineSegment))]
    [XmlInclude(typeof(PathPoint))]
    [XmlInclude(typeof(PathQuadraticBezier))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathItem()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the flag indicating whether the item'spossition should be 
        /// calculated relitive to the last item, or from Origin. 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public bool Relitive { get; set; }

        /// <summary>
        /// Gets or sets a reference to the previous gometric item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PathItem Previous { get; set; }

        /// <summary>
        /// Gets or sets a reference to the next geometric item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PathItem Next { get; set; }

        /// <summary>
        /// Gets or sets the starting coordinates for the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? Start { get; set; }

        /// <summary>
        /// Gets or sets the next to last point of the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? NextToEnd { get; set; }

        /// <summary>
        /// Gets or sets the ending coordinates for the item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract Point2D? End { get; set; }

        /// <summary>
        /// Gets the bounding rectangle of this segment of the shape.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public abstract Rectangle2D Bounds { get; }

        /// <summary>
        /// Gets the length of the Path segment.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public abstract double Length { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Point2D Interpolate(double t);

        #endregion
    }
}
