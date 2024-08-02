// <copyright file="SwirlDistort.cs" company="Shkyrockett" >
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
/// The swirl distort class.
/// </summary>
/// <seealso cref="Engine.DestructiveFilter" />
public class SwirlDistort
    : DestructiveFilter
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="SwirlDistort" /> class.
    /// </summary>
    /// <param name="center">The center.</param>
    /// <param name="strength">The strength.</param>
    public SwirlDistort(Point2D center, double strength = 0.008)
    {
        Center = center;
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
    public override Point2D Process(Point2D point) => Process(point, Center, -Strength);

    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="center">The center.</param>
    /// <param name="strength">The strength.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Process(Point2D point, Point2D center, double strength) => Distortions.Swirl(point, center, strength);
    #endregion Methods
}
