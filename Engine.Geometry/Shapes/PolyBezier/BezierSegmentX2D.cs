﻿// <copyright file="BezierSegmentX2D.cs" >
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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// Utility class about 2D Bézier curves
/// Aka a segment, or quadratic or cubic bezier curve.
/// Extensive Bezier explanation can be found at http://pomax.github.io/bezierinfo/
/// </summary>
/// <remarks> <para>https://github.com/superlloyd/Poly</para> </remarks>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(BezierSegmentX2D))]
[XmlType(TypeName = "bezier-Segment")]
[DebuggerDisplay("{ToString()}")]
public class BezierSegmentX2D
    : Shape2D
{
    #region Fields
    /// <summary>
    /// The handles.
    /// </summary>
    private Point2D[] handles;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a default instance of the <see cref="BezierSegmentX2D"/> class with no terms.
    /// </summary>
    public BezierSegmentX2D()
    {
        handles = [];
        Previous = this;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class as a single point constant term curve.
    /// </summary>
    public BezierSegmentX2D(Point2D point)
    {
        handles = [point];
        Previous = this;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class as a line segment from the previous curve.
    /// </summary>
    /// <param name="previous">The previous curve.</param>
    /// <param name="point">The next point.</param>
    public BezierSegmentX2D(BezierSegmentX2D previous, Point2D point)
        : this(previous, [point])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class as a quadratic bezier curve from the previous curve.
    /// </summary>
    /// <param name="previous">The previous curve.</param>
    /// <param name="handle">The quadratic curve handle.</param>
    /// <param name="point">The next point.</param>
    public BezierSegmentX2D(BezierSegmentX2D previous, Point2D handle, Point2D point)
        : this(previous, [handle, point])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class as a quadratic bezier curve from the previous curve.
    /// </summary>
    /// <param name="previous">The previous curve.</param>
    /// <param name="handle1">The first cubic curve handle.</param>
    /// <param name="handle2">The second cubic curve handle.</param>
    /// <param name="point">The next point.</param>
    public BezierSegmentX2D(BezierSegmentX2D previous, Point2D handle1, Point2D handle2, Point2D point)
        : this(previous, [handle1, handle2, point])
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class using a parameter array.
    /// </summary>
    /// <param name="previous"></param>
    /// <param name="points"></param>
    public BezierSegmentX2D(BezierSegmentX2D previous, params Point2D[] points)
    {
        handles = points ?? throw new ArgumentNullException(nameof(points));
        Previous = previous;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegmentX2D"/> class.
    /// </summary>
    /// <param name="previous">The previous.</param>
    /// <param name="points">The points.</param>
    public BezierSegmentX2D(BezierSegmentX2D previous, params IEnumerable<Point2D> points)
        : this(previous, points.ToArray())
    { }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// The deconstruct.
    /// </summary>
    /// <param name="points">The points.</param>
    public void Deconstruct(out Point2D[] points)
        => points = handles;
    #endregion Deconstructors

    #region Indexers
    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <param name="index">The index index.</param>
    /// <returns>One element of type Point2D.</returns>
    public Point2D this[int index]
    {
        get { return handles[index]; }
        set { handles[index] = value; }
    }
    #endregion Indexers

    #region Properties
    /// <summary>
    /// Gets or sets a reference to the previous geometric item.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public BezierSegmentX2D Previous { get; set; }

    /// <summary>
    /// Gets or sets a reference to the end point of the previous <see cref="BezierSegmentX2D"/> to use as the stating point.
    /// </summary>
    [DataMember, XmlElement, SoapElement]
    public Point2D Start
    {
        get { return Previous.End; }
        set
        {
            Previous.End = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets or sets the handles.
    /// </summary>
    [XmlArray]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof(ArrayConverter))]
    public Point2D[] Handles
    {
        get { return handles.Slice(0, handles.Length - 2).ToArray(); }
        set
        {
            var temp = new[] { handles[^1] };
            handles = [.. value, .. temp];
            ClearCache();
            OnPropertyChanged(nameof(Handles));
        }
    }

    /// <summary>
    /// Gets or sets the points.
    /// </summary>
    [XmlArray]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof(ArrayConverter))]
    public Point2D[] Points
    {
        get { return handles.Slice(0, handles.Length - 2).ToArray(); }
        set
        {
            var temp = new[] { handles[^1] };
            handles = [.. value, .. temp];
            ClearCache();
            OnPropertyChanged(nameof(Handles));
        }
    }

    /// <summary>
    /// Gets or sets the next to end.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D NextToEnd
    {
        get { return (handles.Length >= 2) ? handles[^2] : Start; }
        set
        {
            if (handles.Length >= 2)
            {
                handles[^2] = value;
            }
            else
            {
                Start = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the end.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D End
    {
        get { return handles[^1]; }
        set { handles[^1] = value; }
    }

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

    /// <summary>
    /// Gets the length.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public double Length => (double)CachingProperty(() => this.Length());

    /// <summary>
    /// Gets the curve x.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveX
    {
        get
        {
            var curveX = (Polynomial<double>)CachingProperty(() => Polynomials.Bezier(handles.Concat([Start]).Select(p => p.X).ToArray()));
            curveX.IsReadonly = true;
            return curveX;
        }
    }

    /// <summary>
    /// Gets the curve y.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Polynomial<double> CurveY
    {
        get
        {
            var curveY = (Polynomial<double>)CachingProperty(() => Polynomials.Bezier(handles.Concat([Start]).Select(p => p.Y).ToArray()));
            curveY.IsReadonly = true;
            return curveY;
        }
    }

    /// <summary>
    /// Gets the degree.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public PolynomialDegree Degree
        => (PolynomialDegree)Handles.Length;
    #endregion Properties

    #region Methods
    /// <summary>
    /// Creates a string representation of this <see cref="PolygonContour2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ConvertToString(string format, IFormatProvider provider)
    {
        if (this is null)
        {
            return nameof(BezierSegmentX2D);
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        IFormattable formatable = $"{nameof(BezierSegmentX2D)}{{{string.Join<Point2D>(sep, handles)}}}";
        return formatable.ToString(format, provider);
    }
    #endregion Methods
}
