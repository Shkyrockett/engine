// <copyright file="PathPoint.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public override Point2D? NextToEnd { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override Point2D? End { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value };

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Measurements.LineSegmentBounds(Start.Value, End.Value);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => Start.Value;

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D ToPoint2D
            => Start.Value;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public override double Length => 0;

        #endregion
    }
}
