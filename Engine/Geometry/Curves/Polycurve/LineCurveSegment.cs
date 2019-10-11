// <copyright file="LineCurveSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;

namespace Engine
{
    /// <summary>
    /// The line curve segment class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class LineCurveSegment
         : CurveSegment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LineCurveSegment"/> class.
        /// </summary>
        public LineCurveSegment()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineCurveSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relitive">The relitive.</param>
        /// <param name="args">The args.</param>
        public LineCurveSegment(CurveSegment previous, bool relitive, params double[] args)
            : this(previous, args.Length == 2 ? (Point2D?)new Point2D(args[0], args[1]) : null)
        {
            if (relitive)
            {
                Tail = (Point2D)(Tail + previous.Tail);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineCurveSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="end">The end.</param>
        public LineCurveSegment(CurveSegment previous, Point2D? end)
        {
            if (previous is null)
            {
                throw new ArgumentNullException(nameof(previous));
            }

            Previous = previous;
            previous.Next = this;
            Tail = end;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by)
        {
            ax = Head.Value.X;
            ay = Head.Value.Y;
            bx = Tail.Value.X;
            by = Tail.Value.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? Head
        {
            get { return Previous?.Tail; }
            set
            {
                if (Previous is null)
                {
                    Previous = new PointSegment(value);
                }
                else
                {
                    Previous.Tail = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Head; } set { Head = value; } }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? Tail { get; set; }

        /// <summary>
        /// Gets the grips.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips
            => new List<Point2D> { Head.Value, Tail.Value };

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(Head.Value.X, Head.Value.Y, Tail.Value.X, Tail.Value.Y));

        /// <summary>
        /// Gets the <see cref="QuadraticBezier"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)LinearBezierBernsteinPolynomial(Head.Value.X, Tail.Value.X));
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
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)LinearBezierBernsteinPolynomial(Head.Value.Y, Tail.Value.Y));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => ToLineSegment().Length);
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => ToLineSegment().Interpolate(t);

        #region Methods
        /// <summary>
        /// The to line segment.
        /// </summary>
        /// <returns>The <see cref="LineSegment"/>.</returns>
        public LineSegment ToLineSegment()
            => new LineSegment(Head.Value, Tail.Value);
        #endregion Methods
    }
}
