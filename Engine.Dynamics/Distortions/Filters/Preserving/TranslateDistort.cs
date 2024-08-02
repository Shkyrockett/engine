// <copyright file="TranslateDistort.cs" company="Shkyrockett" >
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
/// The translate distort class.
/// </summary>
public class TranslateDistort
    : PreservingFilter
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateDistort" /> class.
    /// </summary>
    /// <param name="offset">The offset.</param>
    public TranslateDistort(Vector2D offset)
    {
        Offset = offset;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the offset.
    /// </summary>
    /// <value>
    /// The offset.
    /// </value>
    public Vector2D Offset { get; set; }
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
    public override Point2D Process(Point2D point) => Process(point, Offset);

    /// <summary>
    /// Process.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="offset">The offset.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Process(Point2D point, Vector2D offset) => Distortions.Translate(point, offset);
    #endregion Methods
}
