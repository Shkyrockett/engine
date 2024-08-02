// <copyright file="LineCurveSegment2D.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Polynomials;

namespace Engine;

/// <summary>
/// The line curve segment class.
/// </summary>
[DataContract, Serializable]
[DebuggerDisplay("{ToString()}")]
public class LineCurveSegment2D
     : CurveSegment2D
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="LineCurveSegment2D"/> class.
    /// </summary>
    public LineCurveSegment2D()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCurveSegment2D"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="relative">The relative.</param>
    /// <param name="args">The args.</param>
    public LineCurveSegment2D(CurveSegment2D previous, bool relative, params double[] args)
        : this(previous, args.Length == 2 ? new Point2D(args[0], args[1]) : null)
    {
        if (relative)
        {
            Tail = (Point2D)(Tail + previous?.Tail);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineCurveSegment"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="end">The end.</param>
    public LineCurveSegment2D(CurveSegment2D previous, Point2D? end)
    {
        ArgumentNullException.ThrowIfNull(previous);

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
        ax = Head.X;
        ay = Head.Y;
        bx = Tail.X;
        by = Tail.Y;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the start.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override Point2D Head
    {
        get { return Previous?.Tail; }
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
    /// Gets or sets the next to end.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override Point2D NextToEnd { get { return Head; } set { Head = value; } }

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
    public override List<Point2D> Grips => [Head, Tail];

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(Head.X, Head.Y, Tail.X, Tail.Y));

    /// <summary>
    /// Gets the <see cref="QuadraticBezier2D"/> curve's polynomial representation along the x-axis.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveX
    {
        get
        {
            var curveX = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)LinearBezierBernsteinBasis(Head.X, Tail.X));
            curveX.IsReadonly = true;
            return curveX;
        }
    }

    /// <summary>
    /// Gets the <see cref="QuadraticBezier2D"/> curve's polynomial representation along the y-axis.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveY
    {
        get
        {
            var curveY = (Polynomial<double>)CachingProperty(() => (Polynomial<double>)LinearBezierBernsteinBasis(Head.Y, Tail.Y));
            curveY.IsReadonly = true;
            return curveY;
        }
    }

    /// <summary>
    /// Gets the length.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override double Length => (double)CachingProperty(() => ToLineSegment().Length);
    #endregion Properties

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public override Point2D Interpolate(double t) => ToLineSegment().Interpolate(t);

    #region Methods
    /// <summary>
    /// The to line segment.
    /// </summary>
    /// <returns>The <see cref="LineSegment2D"/>.</returns>
    public LineSegment2D ToLineSegment() => new(Head, Tail);
    #endregion Methods
}
