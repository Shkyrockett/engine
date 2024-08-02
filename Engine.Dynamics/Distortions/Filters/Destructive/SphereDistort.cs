// <copyright file="SphereDistort.cs" company="Shkyrockett" >
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
/// The sphere distort class.
/// </summary>
/// <seealso cref="Engine.DestructiveFilter" />
public class SphereDistort
    : DestructiveFilter
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="SphereDistort" /> class.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="strength">The strength.</param>
    public SphereDistort(Rectangle2D rect, double strength = 0.5d)
    {
        Center = rect.Center;
        Radius = Math.Sqrt((rect.Width * rect.Width) + (rect.Height * rect.Height)) * 0.5d;
        Strength = strength;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SphereDistort" /> class.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="strength">The strength.</param>
    public SphereDistort(Point2D center, double radius, double strength = 0.5d)
    {
        Center = center;
        Radius = radius;
        Strength = strength;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the center.
    /// </summary>
    /// <value>
    /// The center.
    /// </value>
    public Point2D Center { get; set; }

    /// <summary>
    /// Gets or sets the radius.
    /// </summary>
    /// <value>
    /// The radius.
    /// </value>
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the strength.
    /// </summary>
    /// <value>
    /// The strength.
    /// </value>
    public double Strength { get; set; }
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
    public override Point2D Process(Point2D point) => Process(point, Center, Radius, -Strength);

    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="center">The center.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="strength">The strength.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Process(Point2D point, Point2D center, double radius, double strength) => Distortions.Pinch(point, center, radius, strength);
    #endregion Methods
}
