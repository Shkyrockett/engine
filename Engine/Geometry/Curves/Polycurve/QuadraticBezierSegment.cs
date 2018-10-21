﻿// <copyright file="QuadraticBezierSegment.cs" company="Shkyrockett" >
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
    /// The quadratic bezier segment class.
    /// </summary>
    [DataContract, Serializable]
    public class QuadraticBezierSegment
         : CurveSegment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment"/> class.
        /// </summary>
        public QuadraticBezierSegment()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relitive">The relitive.</param>
        /// <param name="args">The args.</param>
        public QuadraticBezierSegment(CurveSegment previous, bool relitive, double[] args)
            : this(previous, relitive, args.Length == 4 ? new Point2D[] { new Point2D(args[0], args[1]), new Point2D(args[2], args[3]) }
                : args.Length == 2 ? new Point2D[] { new Point2D(args[0], args[1]) } : null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="relitive">The relitive.</param>
        /// <param name="args">The args.</param>
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
        /// Initializes a new instance of the <see cref="QuadraticBezierSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="end">The end.</param>
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
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
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
        /// Gets or sets the start.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? Start { get { return Previous.End; } set { Previous.End = value; } }

        /// <summary>
        /// Gets or sets the handle.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D? Handle { get; set; }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Handle; } set { Handle = value; } }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? End { get; set; }

        /// <summary>
        /// Gets the grips.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips
            => new List<Point2D> { Start.Value, Handle.Value, End.Value };

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
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override double Length
            => (double)CachingProperty(() => Measurements.QuadraticBezierArcLengthByIntegral(Start.Value.X, Start.Value.Y, Handle.Value.X, Handle.Value.Y, End.Value.X, End.Value.Y));
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => ToQuadtraticBezier().Interpolate(t);

        #region Methods
        /// <summary>
        /// The to quadtratic bezier.
        /// </summary>
        /// <returns>The <see cref="QuadraticBezier"/>.</returns>
        public QuadraticBezier ToQuadtraticBezier()
            => new QuadraticBezier(Start.Value, Handle.Value, End.Value);
        #endregion Methods
    }
}
