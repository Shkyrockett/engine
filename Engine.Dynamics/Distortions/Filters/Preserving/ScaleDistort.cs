// <copyright file="ScaleDistort.cs" company="Shkyrockett" >
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
/// The scale distort class.
/// </summary>
public class ScaleDistort
    : PreservingFilter
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ScaleDistort"/> class.
    /// </summary>
    /// <param name="factors">The factors.</param>
    public ScaleDistort(Size2D factors)
    {
        Factors = factors;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the factors.
    /// </summary>
    public Size2D Factors { get; set; }
    #endregion Properties

    #region Methods
    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override Point2D Process(Point2D point)
        => Process(point, Factors);

    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="factors"></param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Process(Point2D point, Size2D factors)
        => Distortions.Scale(point, factors);
    #endregion Methods
}
