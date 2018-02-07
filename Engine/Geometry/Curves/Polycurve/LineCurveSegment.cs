// <copyright file="LineCurveSegment.cs" company="Shkyrockett" >
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
    public class LineCurveSegment
         : CurveSegment
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public LineCurveSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LineCurveSegment(CurveSegment previous, bool relitive, params Double[] args)
            : this(previous, args.Length == 2 ? (Point2D?)new Point2D(args[0], args[1]) : null)
        {
            if (relitive)
                End = (Point2D)(End + previous.End);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="end"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LineCurveSegment(CurveSegment previous, Point2D? end)
        {
            Previous = previous;
            previous.Next = this;
            End = end;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by)
        {
            ax = Start.Value.X;
            ay = Start.Value.Y;
            bx = End.Value.X;
            by = End.Value.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Start; } set { Start = value; } }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value, End.Value };

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(Start.Value.X, Start.Value.Y, End.Value.X, End.Value.Y));

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => ToLineSegment().Length);
        #endregion Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => ToLineSegment().Interpolate(t);

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public LineSegment ToLineSegment()
            => new LineSegment(Start.Value, End.Value);
        #endregion Methods
    }
}
