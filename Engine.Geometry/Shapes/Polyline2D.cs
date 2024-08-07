﻿// <copyright file="Polyline2D.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine;

/// <summary>
/// The polyline class.
/// </summary>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Polyline2D))]
[XmlType(TypeName = "polyline", Namespace = "http://www.w3.org/2000/svg")]
[DebuggerDisplay("{ToString()}")]
public class Polyline2D
    : Shape2D, IEnumerable<Point2D>
{
    #region Fields
    /// <summary>
    /// The points.
    /// </summary>
    private List<Point2D> points;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    public Polyline2D()
        : this(new List<Point2D>())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    /// <param name="polygon">The polygon.</param>
    public Polyline2D(PolygonContour2D polygon)
        : this(polygon?.Points)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    /// <param name="polyline">The polyline.</param>
    public Polyline2D(Polyline2D polyline)
        : this(polyline?.points)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    /// <param name="points">The points.</param>
    public Polyline2D(params Point2D[] points)
        : this(new List<Point2D>(points))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    /// <param name="points">The points.</param>
    public Polyline2D(params IEnumerable<Point2D> points)
    {
        this.points = points as List<Point2D> ?? [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline2D"/> class.
    /// </summary>
    /// <param name="polylines">The polylines.</param>
    public Polyline2D(params IEnumerable<Polyline2D> polylines)
    {
        ArgumentNullException.ThrowIfNull(polylines);

        points = [];
        foreach (var polyline in polylines)
        {
            points.AddRange(polyline.Points);
        }
    }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// The deconstruct.
    /// </summary>
    /// <param name="points">The points.</param>
    public void Deconstruct(out List<Point2D> points)
        => points = this.points;
    #endregion Deconstructors

    #region Indexers
    /// <summary>
    /// The Indexer.
    /// </summary>
    /// <param name="index">The index index.</param>
    /// <returns>One element of type Point2D.</returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Point2DConverter))]
    public Point2D this[int index]
    {
        get { return points[index]; }
        set
        {
            points[index] = value;
            update?.Invoke();
        }
    }
    #endregion Indexers

    #region Properties
    /// <summary>
    /// Gets or sets the points.
    /// </summary>
    [XmlArray]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof(ExpandableCollectionConverter))]
    public List<Point2D> Points
    {
        get { return points; }
        set
        {
            points = value;
            OnPropertyChanged(nameof(Points));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the perimeter.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override double Perimeter
        => (double)CachingProperty(() => points.Zip(points.Skip(1), Measurements.Distance).Sum());

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public override Rectangle2D Bounds
        => (Rectangle2D)CachingProperty(() => Measurements.PolylineBounds(points));

    /// <summary>
    /// Gets the count.
    /// </summary>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [TypeConverter(typeof(Rectangle2DConverter))]
    public int Count
        => points.Count;
    #endregion Properties

    #region Mutators
    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public Polyline2D Add(Point2D point)
    {
        Points.Add(point);
        OnPropertyChanged(nameof(Add));
        update?.Invoke();
        return this;
    }

    /// <summary>
    /// The reverse.
    /// </summary>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public Polyline2D Reverse()
    {
        Points.Reverse();
        OnPropertyChanged(nameof(Reverse));
        update?.Invoke();
        return this;
    }
    #endregion Mutators

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public override Point2D Interpolate(double t)
    {
        switch (t)
        {
            case 0:
                return points[0];
            case 1:
                return points[^1];
            default:
                break;
        }

        var weights = new (double length, double accumulated)[points.Count];
        weights[0] = (0, 0);
        var cursor = points[0];
        double accumulatedLength = 0;

        // Build up the weights map.
        for (var i = 1; i < points.Count; i++)
        {
            var curentLength = Measurements.Distance(cursor, points[i]);
            accumulatedLength += curentLength;
            weights[i] = (curentLength, accumulatedLength);
            cursor = points[i];
        }

        var accumulatedLengthT = accumulatedLength * t;

        // Find the segment.
        for (var i = points.Count - 1; i >= 0; i--)
        {
            if (weights[i].accumulated <= accumulatedLengthT)
            {
                // Interpolate the position.
                var th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                cursor = Interpolators.Linear(th, points[i], points[i + 1]);
                break;
            }
        }

        return cursor;
    }

    /// <summary>
    /// Translate.
    /// </summary>
    /// <param name="delta">The delta.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public Polyline2D Translate(Point2D delta) => Translate(this, delta);

    /// <summary>
    /// Translate.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="delta">The delta.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    public static Polyline2D Translate(Polyline2D path, Point2D delta)
    {
        var outPath = new List<Point2D>((path?.points).Count);
        for (var i = 0; i < path.points.Count; i++)
        {
            outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
        }

        return new Polyline2D(outPath);
    }

    #region Methods
    /// <summary>
    /// Clone.
    /// </summary>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polyline2D Clone()
        => new(Points.ToArray());

    /// <summary>
    /// The offset.
    /// </summary>
    /// <param name="offset">The offset.</param>
    /// <returns>The <see cref="Polyline2D"/>.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polyline2D Offset(double offset)
        => Offsets.Offset(this, offset);

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
    public IEnumerator<Point2D> GetEnumerator()
        => points.GetEnumerator();

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// Creates a string representation of this <see cref="Polyline2D"/> struct based on the format string
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
            return nameof(Polyline2D);
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        IFormattable formatable = $"{nameof(Polyline2D)}{{{string.Join(sep.ToString(provider), Points)}}}";
        return formatable.ToString(format, provider);
    }
    #endregion Methods
}
