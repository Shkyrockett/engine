﻿// <copyright file="Vector3Lerper.cs" company="Shkyrockett" >
// Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> Based on: https://github.com/jacobalbano/glide </remarks>

using static System.Math;

namespace Engine.Tweening;

/// <summary>
/// The point3d lerper class.
/// </summary>
public class Point3DLerper
    : IMemberLerper
{
    /// <summary>
    /// The from.
    /// </summary>
    private Point3D from;

    /// <summary>
    /// The to.
    /// </summary>
    private Point3D to;

    /// <summary>
    /// The range.
    /// </summary>
    private Point3D range;

    /// <summary>
    /// Initialize.
    /// </summary>
    /// <param name="fromValue">The fromValue.</param>
    /// <param name="toValue">The toValue.</param>
    /// <param name="behavior">The behavior.</param>
    public void Initialize(object fromValue, object toValue, LerpBehaviors behavior)
    {
        from = (Point3D)fromValue;
        to = (Point3D)toValue;
        range = new Point3D(
            to.X - from.X,
            to.Y - from.Y,
            to.Z - from.Z);
    }

    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <param name="currentValue">The currentValue.</param>
    /// <param name="behavior">The behavior.</param>
    /// <returns>The <see cref="object"/>.</returns>
    public object Interpolate(double t, object currentValue, LerpBehaviors behavior)
    {
        var x = from.X + (range.X * t);
        var y = from.Y + (range.Y * t);
        var z = from.Z + (range.Z * t);

        if (behavior.HasFlag(LerpBehaviors.Round))
        {
            x = Round(x);
            y = Round(y);
            z = Round(z);
        }

        var current = (Point3D)currentValue;
        return new Point3D(
            (Abs(range.X) < double.Epsilon) ? current.X : x,
            (Abs(range.Y) < double.Epsilon) ? current.Y : y,
            (Abs(range.Z) < double.Epsilon) ? current.Z : z);
    }
}
