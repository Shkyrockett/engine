// <copyright file="CubicBezierSegment2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using static Engine.Polynomials;

namespace Engine
{
    /// <summary>
    /// The cubic bezier segment class.
    /// </summary>
    [DataContract, Serializable]
    public class CubicBezierSegment2D
         : CurveSegment2D
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezierSegment"/> class.
        /// </summary>
        public CubicBezierSegment2D()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezierSegment"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="args">The args.</param>
        public CubicBezierSegment2D(CurveSegment2D item, bool relative, double[] args)
            : this(item, relative, args.Length == 6 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]), new Point2D(args[4], args[5]) }
                : args.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) } : null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezierSegment"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="args">The args.</param>
        public CubicBezierSegment2D(CurveSegment2D item, bool relative, Point2D[] args)
            : this(item, args.Length == 3 ? (Point2D?)args[0] : null, args.Length == 3 ? args[1] : args[0], args.Length == 3 ? args[1] : args[2])
        {
            if (relative)
            {
                Handle1 = (Point2D)(Handle1 + item.Tail);
                Handle2 = (Point2D)(Handle2 + item.Tail);
                Tail = (Point2D)(Tail + item.Tail);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubicBezierSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="handle1">The handle1.</param>
        /// <param name="handle2">The handle2.</param>
        /// <param name="end">The end.</param>
        public CubicBezierSegment2D(CurveSegment2D previous, Point2D? handle1, Point2D handle2, Point2D end)
        {
            if (previous is null)
            {
                throw new ArgumentNullException(nameof(previous));
            }

            Previous = previous;
            previous.Next = this;
            Handle1 = handle1 ?? (Point2D)((2d * previous.Tail) - previous.NextToEnd);
            Handle2 = handle2;
            Tail = end;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="CubicBezierSegment"/> to a Tuple.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy, out double dx, out double dy)
        {
            ax = Head.Value.X;
            ay = Head.Value.Y;
            bx = Handle1.X;
            by = Handle1.Y;
            cx = Handle2.Value.X;
            cy = Handle2.Value.Y;
            dx = Tail.Value.X;
            dy = Tail.Value.Y;
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
                    Previous = new PointSegment2D(value);
                }
                else
                { Previous.Tail = value; }
            }
        }

        /// <summary>
        /// Gets or sets the handle1.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D Handle1 { get; set; }

        /// <summary>
        /// Gets or sets the handle2.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D? Handle2 { get; set; }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Handle2; } set { Handle2 = value; } }

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
            => new List<Point2D> { Head.Value, Handle1, Handle2.Value, Tail.Value };

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            //=> Measurements.CubicBezierBounds(Start.Value.X, Start.Value.Y, Handle1.X, Handle1.Y, Handle2.Value.X, Handle2.Value.Y, End.Value.X, End.Value.Y);
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// Gets the <see cref="CubicBezier2D"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)CubicBezierBernsteinBasis(Head.Value.X, Handle1.X, Handle2.Value.X, Tail.Value.X));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="CubicBezier2D"/> curve's polynomial representation along the y-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)CubicBezierBernsteinBasis(Head.Value.Y, Handle1.Y, Handle2.Value.X, Tail.Value.Y));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => Measurements.CubicBezierArcLength(Head.Value.X, Head.Value.Y, Handle1.X, Handle1.Y, Handle2.Value.X, Handle2.Value.Y, Tail.Value.X, Tail.Value.Y));
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => ToCubicBezier().Interpolate(t);

        #region Methods
        /// <summary>
        /// The to cubic bezier.
        /// </summary>
        /// <returns>The <see cref="CubicBezier2D"/>.</returns>
        public CubicBezier2D ToCubicBezier()
            => new CubicBezier2D(Head.Value, Handle1, Handle2.Value, Tail.Value);
        #endregion Methods
    }
}
