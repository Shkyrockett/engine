// <copyright file="PathCardinal.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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
    public class PathCardinal
         : PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathCardinal()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="points"></param>
        public PathCardinal(PathItem previous, List<Point2D> points)
        {
            Previous = previous;
            previous.Next = this;
            CenteralPoints = points;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? Start { get => Previous.End; set => Previous.End = value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        public List<Point2D> CenteralPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Nodes
        {
            get
            {
                var nodes = new List<Point2D>() { Start.Value };
                nodes.AddRange(CenteralPoints);
                return nodes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get => Nodes[Nodes.Count - 1]; set => Nodes[Nodes.Count - 1] = value.Value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D? End { get => CenteralPoints[CenteralPoints.Count - 1]; set => CenteralPoints[CenteralPoints.Count - 1] = value.Value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.Polygon(Nodes);

        /// <summary>
        /// ToDo: Add length calculation for Cardinal curves.
        /// </summary>
        public override double Length => 0;

        #endregion
    }
}
