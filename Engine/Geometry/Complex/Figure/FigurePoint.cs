// <copyright file="FigurePoint.cs" >
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
    public class FigurePoint
        : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        public FigurePoint(Point2D start)
        {
            Start = start;
            Previous = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public override Point2D Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D End { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.LineSegment(Start, End);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Point2D ToPoint2D
            => Start;
    }
}
