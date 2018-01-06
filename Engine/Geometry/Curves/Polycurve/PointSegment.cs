// <copyright file="PointSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class PointSegment
        : CurveSegment
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PointSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PointSegment(CurveSegment previous, bool relitive, Double[] args)
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
        public PointSegment(CurveSegment previous, Boolean relitive, Point2D startPoint)
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
        public PointSegment(Point2D? start)
        {
            Start = start;
            Previous = this;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out double x, out double y)
        {
            x = Start.Value.X;
            y = Start.Value.Y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? End { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value };

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(Start.Value, End.Value));

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => 0;

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
        public Point2D ToPoint2D()
            => Start.Value;

        #endregion
    }
}
