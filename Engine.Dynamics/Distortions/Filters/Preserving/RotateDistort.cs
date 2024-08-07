﻿// <copyright file="RotateDistort.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The rotate distort class.
/// </summary>
/// <seealso cref="Engine.PreservingFilter" />
public class RotateDistort
    : PreservingFilter
{
    #region Fields
    /// <summary>
    /// Property cache for commonly used properties that may take time to calculate.
    /// </summary>
    [NonSerialized]
    protected Dictionary<object, object> propertyCache = [];

    /// <summary>
    /// The center.
    /// </summary>
    private Point2D center;

    /// <summary>
    /// The angle.
    /// </summary>
    private double angle;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RotateDistort" /> class.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="angle">The angle.</param>
    public RotateDistort(Point2D center, double angle)
    {
        this.center = center;
        Angle = angle;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the center.
    /// </summary>
    /// <value>
    /// The center.
    /// </value>
    public Point2D Center
    {
        get { return center; }
        set
        {
            center = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets or sets the angle.
    /// </summary>
    /// <value>
    /// The angle.
    /// </value>
    public double Angle
    {
        get { return angle; }
        set
        {
            angle = value;
            ClearCache();
        }
    }

    /// <summary>
    /// Gets the x axis.
    /// </summary>
    /// <value>
    /// The x axis.
    /// </value>
    public Vector2D XAxis => (Vector2D)CachingProperty(() => new Vector2D(Math.Cos(Angle), Math.Sin(Angle)));

    /// <summary>
    /// Gets the y axis.
    /// </summary>
    /// <value>
    /// The y axis.
    /// </value>
    public Vector2D YAxis => (Vector2D)CachingProperty(() => new Vector2D(-Math.Sin(Angle), Math.Cos(Angle)));
    #endregion Properties

    #region Methods
    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override Point2D Process(Point2D point) => Process(point, Center, XAxis, YAxis);

    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="center">The center.</param>
    /// <param name="xAxis">The x axis.</param>
    /// <param name="yAxis">The y axis.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Process(Point2D point, Point2D center, Vector2D xAxis, Vector2D yAxis) => Distortions.Rotate(point, center, xAxis, yAxis);

    /// <summary>
    /// Clear the cache.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ClearCache() => propertyCache.Clear();

    /// <summary>
    /// Private method for caching computationally and memory intensive properties of child objects
    /// so the child object's properties only get touched when necessary.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected object CachingProperty(Func<object> property, [CallerMemberName] string name = "")
    {
        if (!propertyCache.ContainsKey(name))
        {
            var value = property?.Invoke();
            propertyCache.Add(name, value);
            return value;
        }

        return propertyCache[name];
    }
    #endregion Methods
}
