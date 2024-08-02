// <copyright file="Generators.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine;

/// <summary>
/// The generators class.
/// </summary>
public static class Generators
{
    #region Regular Polygons
    /// <summary>
    /// The regular convex polygon.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="count">The count.</param>
    /// <param name="angle">The angle.</param>
    /// <returns>
    /// The <see cref="PolygonContour2D" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PolygonContour2D RegularConvexPolygon(double x, double y, double radius, int count, double? angle = null)
    {
        angle = -Floats<double>.Hau;
        var points = new Point2D[count];
        var theta = angle.Value;
        var difference = Tau / count;
        for (var i = 0; i < count; i++)
        {
            points[i] = new Point2D(x + (radius * Cos(theta)), y + (radius * Sin(theta)));
            theta += difference;
        }

        return new PolygonContour2D(points);
    }
    #endregion Regular Polygons

    #region Heart Curves
    /// <summary>
    /// The heart curve.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="alpha">The alpha.</param>
    /// <param name="beta">The beta.</param>
    /// <returns>
    /// The <see cref="List{T}" />.
    /// </returns>
    public static List<Point2D> HeartCurve(double x, double y, double radius, double alpha, double beta)
    {
        // The maximum values of the phase angle "t".
        double tmax = Floats<double>.Pau;
        // The minimum values of the phase angle "t".
        double tmin = -Floats<double>.Hau;
        // The constant of the original Cardioid.
        const double a = 1d;

        // its increment [radian] of a Cardioid before the conversion plotting interval of the phase angle "t" before conversion
        var dt = (tmax - tmin) / radius; // 10

        // Optimization
        var scale = radius * tmax / 1.855d;

        var points = new List<Point2D>();

        // Execution of calculation
        for (var t = tmin; t <= tmax; t += dt) // the phase angle [radian]
        {
            // The moving radius of a Cardioid after some conversion
            var r = 0d;

            // The phase angle [radian] of a Cardioid after some conversion

            double z;
            if (t > (tmax - dt) && t < (tmax + dt))
            {
                z = tmin;
            }
            else if (t > (tmin - dt) && t < (tmin + dt))
            {
                z = -tmin;
            }
            else
            {
                var sinT = Sin(t); // Optimization to reduce the number of times sin() is called
                r = a * Sqrt((5d - (3d * sinT)) * (1d + sinT));
                z = Asin(a * (1d - sinT) * Cos(t) / r);
            }

            // The phase angle [radian] of a Cardioid after the final conversion into a horned one
            var f = (-alpha * z / PI) + (PI * Floats<double>.OneHalf);

            points.Add(new Point2D((-(r * Cos(f)) * scale) + x, (-(beta * r * Sin(f)) * scale) + y + radius));
        }

        return points;
    }

    /// <summary>
    /// The heart curve.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="radius">The radius.</param>
    /// <returns>
    /// The <see cref="PolycurveContour" />.
    /// </returns>
    public static PolycurveContour2D HeartCurve(double x, double y, double radius)
    {
        // ToDo: Optimize algorithm to calculate minimum points for minimum curves.
        var heart = HeartCurve(x, y, radius, 0.9d, Floats<double>.OneThird);
        var first = CurveFit.Fit(heart.Take(heart.Count / 2).ToList(), Floats<double>.OneHalf);
        var last = CurveFit.Fit(heart.Skip(heart.Count / 2).ToList(), Floats<double>.OneHalf);
        var heartCurve = new PolycurveContour2D(first);
        heartCurve.AddCubicBeziers(last);
        return heartCurve;
    }
    #endregion Heart Curves
}
