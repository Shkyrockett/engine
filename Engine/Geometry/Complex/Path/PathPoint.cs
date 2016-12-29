// <copyright file="PathPoint.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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
    public class PathPoint
        : PathItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PathPoint()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public PathPoint(PathItem previous, bool relitive, Double[] args)
            : this(args.Length == 2 ? (Point2D?)new Point2D(args[0], args[1]) : null)
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
        public PathPoint(PathItem previous, Boolean relitive, Point2D startPoint)
            : this(startPoint)
        {
            if (relitive)
                Start = (Point2D)(Start + previous.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        public PathPoint(Point2D? start)
        {
            Start = start;
            Previous = this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public override Point2D? Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get => Start; set => Start = value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? End { get => Start; set => Start = value; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.LineSegment(Start.Value, End.Value);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D ToPoint2D
            => Start.Value;

        #endregion
    }
}
