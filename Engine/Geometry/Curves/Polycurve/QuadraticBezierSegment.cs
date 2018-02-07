// <copyright file="QuadraticBezierSegment.cs" company="Shkyrockett" >
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
    public class QuadraticBezierSegment
         : CurveSegment
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public QuadraticBezierSegment()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        public QuadraticBezierSegment(CurveSegment previous, bool relitive, Double[] args)
            : this(previous, relitive, args.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) }
                : args.Length == 2 ? new Point2D[] { new Point2D(args[0], args[1]) } : null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="relitive"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuadraticBezierSegment(CurveSegment previous, bool relitive, Point2D[] args)
            : this(previous, args.Length == 2 ? (Point2D?)args[0] : null, args.Length == 2 ? args[0] : args[1])
        {
            if (relitive)
            {
                Handle = (Point2D)(Handle + previous.End);
                End = (Point2D)(End + previous.End);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuadraticBezierSegment(CurveSegment previous, Point2D? handle, Point2D end)
        {
            Previous = previous;
            previous.Next = this;
            Handle = handle ?? (Point2D)(2 * previous.End - previous.NextToEnd);
            End = end;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="QuadraticBezier"/> to a Tuple.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy)
        {
            ax = Start.Value.X;
            ay = Start.Value.Y;
            bx = Handle.Value.X;
            by = Handle.Value.Y;
            cx = End.Value.X;
            cy = End.Value.Y;
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
        [DataMember, XmlElement, SoapElement]
        public Point2D? Handle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Handle; } set { Handle = value; } }

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
            => new List<Point2D> { Start.Value, Handle.Value, End.Value };

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            //=> Measurements.QuadraticBezierBounds(Start.Value.X, Start.Value.Y, Handle.Value.X, Handle.Value.Y, End.Value.X, End.Value.Y);
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// Gets the <see cref="QuadraticBezier"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)Maths.QuadraticBezierCoefficients(Start.Value.X, Handle.Value.X, End.Value.X));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="QuadraticBezier"/> curve's polynomial representation along the y-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)Maths.QuadraticBezierCoefficients(Start.Value.Y, Handle.Value.Y, End.Value.Y));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => Measurements.QuadraticBezierArcLengthByIntegral(Start.Value.X, Start.Value.Y, Handle.Value.X, Handle.Value.Y, End.Value.X, End.Value.Y));
        #endregion Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
            => ToQuadtraticBezier().Interpolate(t);

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public QuadraticBezier ToQuadtraticBezier()
            => new QuadraticBezier(Start.Value, Handle.Value, End.Value);
        #endregion Methods
    }
}
