// <copyright file="ChainMember.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public FigureItem Previous { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public FigureItem Next { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract Point2D Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract Point2D End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public abstract Rectangle2D Bounds { get; }
    }
}
