﻿// <copyright file="FigurePoint.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FigurePoint
        : FigureItem
    {
        /// <summary>
        /// 
        /// </summary>
        public FigurePoint()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public FigurePoint(FigureItem previous, bool relitive, Double[] args)
            : this(args.Length == 2 ? new Point2D(args[0], args[1]) : null)
        {
            if (relitive)
                Start = (Point2D)(Start + previous.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="startPoint"></param>
        public FigurePoint(FigureItem previous, Boolean relitive, Point2D startPoint)
            : this(startPoint)
        {
            if (relitive)
                Start = (Point2D)(Start + previous.End);
        }

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
        [XmlElement]
        public override Point2D Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Point2D NextToEnd { get { return Start; } set { Start = value; } }

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
