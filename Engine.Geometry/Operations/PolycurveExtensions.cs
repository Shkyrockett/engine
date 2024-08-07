﻿// <copyright file="PolycurveExtensions.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The polycurve extensions class.
/// </summary>
public static class PolycurveExtensions
{
    /// <summary>
    /// Add the arc.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="pointA">The pointA.</param>
    /// <param name="pointB">The pointB.</param>
    /// <param name="pointC">The pointC.</param>
    public static void AddArc(this List<Shape2D> path, Point2D pointA, Point2D pointB, Point2D pointC) => path?.Add(new CircularArc2D(pointA, pointB, pointC));
}
