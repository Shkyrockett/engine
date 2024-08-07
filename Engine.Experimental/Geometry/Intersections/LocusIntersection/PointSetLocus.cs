﻿// <copyright file="PointSetLocus.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The point set locus class.
/// </summary>
/// <seealso cref="Locus" />
/// <remarks>
/// <para>This class is based on an idea presented by Eric Lippert http://stackoverflow.com/a/2258178</para>
/// </remarks>
public class PointSetLocus
    : Locus
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PointSetLocus" /> class.
    /// </summary>
    public PointSetLocus()
        : this(new List<Point2D>())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PointSetLocus" /> class.
    /// </summary>
    /// <param name="points">The points.</param>
    public PointSetLocus(params Point2D[] points)
        : this(new List<Point2D>(points))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PointSetLocus" /> class.
    /// </summary>
    /// <param name="points">The points.</param>
    public PointSetLocus(List<Point2D> points)
    {
        Points = points;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PointSetLocus" /> class.
    /// </summary>
    /// <param name="polys">The polys.</param>
    public PointSetLocus(List<List<Point2D>> polys)
    {
        ArgumentNullException.ThrowIfNull(polys);

        Points = [];
        foreach (var set in polys)
        {
            Points.Concat(set);
        }
    }

    /// <summary>
    /// Gets or sets the points.
    /// </summary>
    /// <value>
    /// The points.
    /// </value>
    public List<Point2D> Points { get; set; }

    /// <summary>
    /// Performs an implicit conversion from <see cref="PointSetLocus"/> to <see cref="PointSet2D"/>.
    /// </summary>
    /// <param name="locus">The locus.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator PointSet2D(PointSetLocus locus) => new(locus?.Points);

    /// <summary>
    /// Performs an explicit conversion from <see cref="PolylineLocus"/> to <see cref="PointSetLocus"/>.
    /// </summary>
    /// <param name="locus">The locus.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator PointSetLocus(PolylineLocus locus) => new(locus?.Points);

    /// <summary>
    /// Performs an explicit conversion from <see cref="PolygonLocus"/> to <see cref="PointSetLocus"/>.
    /// </summary>
    /// <param name="locus">The locus.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator PointSetLocus(PolygonLocus locus) => new(locus?.Points);

    /// <summary>
    /// Performs an explicit conversion from <see cref="PolylineSetLocus"/> to <see cref="PointSetLocus"/>.
    /// </summary>
    /// <param name="locus">The locus.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator PointSetLocus(PolylineSetLocus locus) => new(locus?.Polylines);

    /// <summary>
    /// Performs an explicit conversion from <see cref="PolygonSetLocus"/> to <see cref="PointSetLocus"/>.
    /// </summary>
    /// <param name="locus">The locus.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator PointSetLocus(PolygonSetLocus locus) => new(locus?.Polygons);

    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>
    /// The <see cref="PointSetLocus" />.
    /// </returns>
    public PointSetLocus Add(Point2D point)
    {
        Points.Add(point);
        return this;
    }

    /// <summary>
    /// Converts to pointset.
    /// </summary>
    /// <returns></returns>
    public PointSet2D ToPointSet() => new(Points);

    /// <summary>
    /// Converts to pointset2d.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public PointSet2D ToPointSet2D() => throw new System.NotImplementedException();

    /// <summary>
    /// Converts to pointsetlocus.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static PointSetLocus ToPointSetLocus(PointSetLocus left, PointSetLocus right) => throw new System.NotImplementedException();
}
