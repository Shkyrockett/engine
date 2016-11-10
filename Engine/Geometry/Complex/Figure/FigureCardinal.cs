// <copyright file="FigureQuadratic.cs" >
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
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FigureCardinal
         : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        public FigureCardinal()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="points"></param>
        public FigureCardinal(FigureItem previous, List<Point2D> points)
        {
            Previous = previous;
            previous.Next = this;
            CenteralPoints = points;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        public List<Point2D> CenteralPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public List<Point2D> Nodes
        {
            get
            {
                var nodes = new List<Point2D>() { Start };
                nodes.AddRange(CenteralPoints);
                return nodes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D NextToEnd { get { return Nodes[Nodes.Count-1]; } set { Nodes[Nodes.Count-1] = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D End { get { return CenteralPoints[CenteralPoints.Count - 1]; } set { CenteralPoints[CenteralPoints.Count - 1] = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.Polygon(Nodes);
    }
}
