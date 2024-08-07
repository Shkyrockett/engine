﻿// <copyright file="CurveBuilder.cs" >
// Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
// Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>https://github.com/burningmime/curves</remarks>

using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine;

/// <summary>
/// This is a version of <see cref="CurveFit"/> that works on partial curves so that a spline can be built in "real-time"
/// as the user is drawing it. The quality of the generated spline may be lower, and it might use more Bézier curves
/// than is necessary. Only the most recent two curves will be modified, once another curve is being built on top of it, curves
/// lower in the "stack" are permanent. This reduces visual jumpiness as the user draws since the entire spline doesn't move
/// around as points are added. It only uses linearizion-based preprocessing; it doesn't support the RDP method.
/// 
/// Add points using the <see cref="AddPoint"/> method.To get the results, either enumerate (foreach) the CurveBuilder itself
/// or use the <see cref="Curves"/> property. The results might be updated every time a point is added.
/// </summary>
public sealed class CurveBuilder
    : CurveFitBase, IEnumerable<CubicBezier2D>
{
    #region Fields
    /// <summary>
    /// result curves (updated whenever a new point is added)
    /// </summary>
    private readonly List<CubicBezier2D> result;

    /// <summary>
    /// ReadOnlyCollection view of result
    /// </summary>
    private readonly ReadOnlyCollection<CubicBezier2D> resultView;

    /// <summary>
    /// distance between points
    /// </summary>
    private readonly double linDist;

    /// <summary>
    /// most recent point added
    /// </summary>
    private Vector2D prev;

    /// <summary>
    /// left tangent of current curve (can't change this except on first curve or we'll lose C1 continuity)
    /// </summary>
    private Vector2D tanL;

    /// <summary>
    /// Total length of the curve so far (for updating arclen)
    /// </summary>
    private double totalLength;

    /// <summary>
    /// Index of first point in current curve
    /// </summary>
    private int first;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="CurveBuilder"/> class.
    /// </summary>
    /// <param name="linDist">The linDist.</param>
    /// <param name="error">The error.</param>
    public CurveBuilder(double linDist, double error)
    {
        squaredError = error * error;
        result = new List<CubicBezier2D>(16);
        resultView = new ReadOnlyCollection<CubicBezier2D>(result);
        this.linDist = linDist;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// The current curves in the builder.
    /// </summary>
    public ReadOnlyCollection<CubicBezier2D> Curves
        => resultView;
    #endregion Properties

    #region Mutators
    /// <summary>
    /// Adds a data point to the curve builder. This doesn't always result in the generated curve changing immediately.
    /// </summary>
    /// <param name="p">The data point to add.</param>
    /// <returns><see cref="AddPointResult"/> for info about this.</returns>
    public AddPointResult AddPoint(Point2D p)
    {
        var count = points.Count;
        if (count != 0)
        {
            var td = Measurements.Distance((Point2D)prev, p);
            var md = linDist;
            if (td > md)
            {
                var first = int.MaxValue;
                var add = false;
                var rd = td - md;
                // OPTIMIZE if we're adding many points at once, we could do them in a batch
                var dir = ((Vector2D)(p - prev)).Normalize();
                do
                {
                    var np = prev + (dir * md);
                    var res = AddInternal((Point2D)np);
                    first = Math.Min(first, res.FirstChangedIndex);
                    add |= res.WasAdded;
                    prev = np;
                    rd -= md;
                } while (rd > md);
                return new AddPointResult(first, add);
            }
            return AddPointResult.NoChange;
        }
        else
        {
            prev = (Vector2D)p;
            points.Add(p);
            arclen.Add(0);
            return AddPointResult.NoChange; // no curves were actually added yet
        }
    }

    /// <summary>
    /// Add the internal.
    /// </summary>
    /// <param name="np">The np.</param>
    /// <returns>The <see cref="AddPointResult"/>.</returns>
    private AddPointResult AddInternal(Point2D np)
    {
        var last = points.Count;
        Debug.Assert(last != 0); // should always have one point at least
        points.Add(np);
        arclen.Add(totalLength += linDist);
        if (last == 1)
        {
            // This is the second point
            Debug.Assert(result.Count == 0);
            var p0 = points[0];
            var tanL = (np - p0).Normalize();
            var tanR = -tanL;
            this.tanL = tanL;
            var alpha = linDist / 3;
            var p1 = (tanL * alpha) + p0;
            var p2 = (tanR * alpha) + np;
            result.Add(new CubicBezier2D(p0, p1, p2, np));
            return new AddPointResult(0, true);
        }
        else
        {
            var lastCurve = result.Count - 1;
            var first = this.first;

            // If we're on the first curve, we're free to improve the left tangent
            var tanL = lastCurve == 0 ? GetLeftTangent(last) : this.tanL;

            // We can always do the end tangent
            var tanR = GetRightTangent(first);

            // Try fitting with the new point
            if (FitCurve(first, last, tanL, tanR, out var curve, out var split))
            {
                result[lastCurve] = curve;
                return new AddPointResult(lastCurve, false);
            }
            else
            {
                // Need to split
                // first, get mid tangent
                var tanM1 = GetCenterTangent(first, last, split);
                var tanM2 = -tanM1;

                // PERHAPS do a full fitRecursive here since its our last chance?

                // our left tangent might be based on points outside the new curve (this is possible for mid tangents too
                // but since we need to maintain C1 continuity, it's too late to do anything about it)
                if (first == 0 && split < EndTangentNPoints)
                {
                    tanL = GetLeftTangent(split);
                }

                // do a final pass on the first half of the curve
                FitCurve(first, split, tanL, tanM1, out curve, out var _);
                result[lastCurve] = curve;

                // prepare to fit the second half
                FitCurve(split, last, tanM2, tanR, out curve, out _);
                result.Add(curve);
                this.first = split;
                this.tanL = tanM2;

                return new AddPointResult(lastCurve, true);
            }
        }
    }

    /// <summary>
    /// Clears the curve builder.
    /// </summary>
    public void Clear()
    {
        result.Clear();
        points.Clear();
        arclen.Clear();
        u.Clear();
        totalLength = 0;
        first = 0;
        tanL = default;
        prev = default;
    }
    #endregion Mutators

    // We provide these for both convenience and performance, since a call to List<T>.GetEnumerator() doesn't actually allocate if
    // the type is never boxed

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="List{CubicBezier}.Enumerator"/>.</returns>
    public List<CubicBezier2D>.Enumerator GetEnumerator()
        => result.GetEnumerator();

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
    IEnumerator<CubicBezier2D> IEnumerable<CubicBezier2D>.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    /// Get the enumerator.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
