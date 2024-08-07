﻿// <copyright file="SizeFExtentions.cs" company="Shkyrockett" >
// Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The size f extentions class.
/// </summary>
public static class SizeFExtentions
{
    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, int factor) => new(size.Width * factor, size.Height * factor);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, float factor) => new(size.Width * factor, size.Height * factor);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The factor to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, double factor) => new((float)(size.Width * factor), (float)(size.Height * factor));

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, Point factor) => new(size.Width * factor.X, size.Height * factor.Y);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, PointF factor) => new(size.Width * factor.X, size.Height * factor.Y);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, Size factor) => new(size.Width * factor.Width, size.Height * factor.Height);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, SizeF factor) => new(size.Width * factor.Width, size.Height * factor.Height);

    /// <summary>
    /// Inflates a <see cref="SizeF"/> by a given factor.
    /// </summary>
    /// <param name="size">The <see cref="SizeF"/> to inflate.</param>
    /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
    /// <returns>Returns a <see cref="SizeF"/> structure inflated by the factor provided.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static SizeF Inflate(this SizeF size, Vector2D factor) => new((float)(size.Width * factor.I), (float)(size.Height * factor.J));

    ///// <summary>
    ///// Unit of a Point
    ///// </summary>
    ///// <param name="value">The Point to Unitize</param>
    ///// <returns></returns>

    //public static SizeF Unit(this SizeF value)            => value.Inflate((float)(1 / Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
}
