// <copyright file="QuadraticBezierSegment2D.cs" company="Shkyrockett" >
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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;

namespace Engine
{
    /// <summary>
    /// The quadratic bezier segment class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class QuadraticBezierSegment2D
         : CurveSegment2D
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment2D"/> class.
        /// </summary>
        public QuadraticBezierSegment2D()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment2D"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="args">The args.</param>
        public QuadraticBezierSegment2D(CurveSegment2D previous, bool relative, double[] args)
            : this(previous, relative, args?.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) }
                : args?.Length == 2 ? new Point2D[] { new Point2D(args[0], args[1]) } : null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment2D"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relative">The relative.</param>
        /// <param name="args">The args.</param>
        public QuadraticBezierSegment2D(CurveSegment2D previous, bool relative, Point2D[] args)
            : this(previous, args?.Length == 2 ? args[0] : null, args.Length == 2 ? args[0] : args[1])
        {
            if (relative)
            {
                Handle = (Point2D)(Handle + (previous?.Tail));
                Tail = (Point2D)(Tail + (previous?.Tail));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment2D"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="end">The end.</param>
        public QuadraticBezierSegment2D(CurveSegment2D previous, Point2D? handle, Point2D end)
        {
            if (previous is null)
            {
                throw new ArgumentNullException(nameof(previous));
            }

            Previous = previous;
            previous.Next = this;
            Handle = handle ?? (Point2D)((2 * previous.Tail) - previous.NextToEnd);
            Tail = end;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="QuadraticBezier2D"/> to a Tuple.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        public void Deconstruct(out double ax, out double ay, out double bx, out double by, out double cx, out double cy)
        {
            ax = Head.X;
            ay = Head.Y;
            bx = Handle.X;
            by = Handle.Y;
            cx = Tail.X;
            cy = Tail.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D Head
        {
            get { return Previous.Tail; }
            set
            {
                if (Previous is null)
                {
                    Previous = new PointSegment2D(value);
                }
                else
                {
                    Previous.Tail = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D Handle { get; set; }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D NextToEnd { get { return Handle; } set { Handle = value; } }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D Tail { get; set; }

        /// <summary>
        /// Gets the grips.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips => new List<Point2D> { Head, Handle, Tail };

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            //=> Measurements.QuadraticBezierBounds(Start.Value.X, Start.Value.Y, Handle.Value.X, Handle.Value.Y, End.Value.X, End.Value.Y);
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// Gets the <see cref="QuadraticBezier2D"/> curve's polynomial representation along the x-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => (Polynomial)QuadraticBezierBernsteinBasis(Head.X, Handle.X, Tail.X));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the <see cref="QuadraticBezier2D"/> curve's polynomial representation along the y-axis.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => (Polynomial)QuadraticBezierBernsteinBasis(Head.Y, Handle.Y, Tail.Y));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length => (double)CachingProperty(() => Measurements.QuadraticBezierArcLengthByIntegral(Head.X, Head.Y, Handle.X, Handle.Y, Tail.X, Tail.Y));
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t) => ToQuadtraticBezier().Interpolate(t);

        #region Methods
        /// <summary>
        /// The to quadratic bezier.
        /// </summary>
        /// <returns>The <see cref="QuadraticBezier2D"/>.</returns>
        public QuadraticBezier2D ToQuadtraticBezier() => new QuadraticBezier2D(Head, Handle, Tail);
        #endregion Methods
    }
}
