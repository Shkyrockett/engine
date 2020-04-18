// <copyright file="CurveFitBase.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> Adapted from:https://github.com/burningmime/curves</remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Engine.Mathematics;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// This is the base class containing implementations common to <see cref="CurveFit"/>. Most of this
    /// is ported from http://tog.acm.org/resources/GraphicsGems/gems/FitCurves.c
    /// </summary>
    public abstract class CurveFitBase
    {
        #region Constants
        /// <summary>
        /// maximum number of iterations of newton's method to run before giving up and splitting curve
        /// </summary>
        protected const int MaxItterations = 4;

        /// <summary>
        /// maximum number of points to base end tangent on
        /// </summary>
        protected const int EndTangentNPoints = 8;

        /// <summary>
        /// maximum number of points on each side to base mid tangent on
        /// </summary>
        protected const int MidTangentNPoints = 4;
        #endregion Constants

        #region Fields
        /// <summary>
        /// Points in the whole line being used for fitting.
        /// </summary>
        protected readonly List<Point2D> points = new List<Point2D>(256);

        /// <summary>
        /// length of curve before each point (so, arclen[0] = 0, arclen[1] = distance(pts[0], pts[1]),
        /// arclen[2] = arclen[1] + distance(pts[1], pts[2]) ... arclen[n -1] = length of the entire curve, etc).
        /// </summary>
        protected readonly List<double> arclen = new List<double>(256);

        /// <summary>
        /// current parametrization of the curve. When fitting, u[i] is the parametrization for the point in pts[first + i]. This is
        /// an optimization for CurveBuilder, since it might not need to allocate as big of a _u as is necessary to hold the whole
        /// curve.
        /// </summary>
        protected readonly List<double> u = new List<double>(256);

        /// <summary>
        /// maximum squared error before we split the curve
        /// </summary>
        protected double squaredError;
        #endregion Fields

        /// <summary>
        /// Gets the tangent for the start of the curve.
        /// </summary>
        protected Vector2D GetLeftTangent(int last, double epsilon = double.Epsilon)
        {
            var totalLen = arclen[^1];
            var p0 = points[0];
            var tanL = GeometryOperations.Normalize(points[1] - p0);
            var total = tanL;
            var weightTotal = 1d;
            last = Min(EndTangentNPoints, last - 1);

            for (var i = 2; i <= last; i++)
            {
                var ti = 1d - (arclen[i] / totalLen);
                var weight = ti * ti * ti;
                var v = GeometryOperations.Normalize(points[i] - p0);
                total += v * weight;
                weightTotal += weight;
            }

            // if the vectors add up to zero (IE going opposite directions), there's no way to normalize them
            if (total.Length > epsilon)
            {
                tanL = GeometryOperations.Normalize(total / weightTotal);
            }

            return tanL;
        }

        /// <summary>
        /// Gets the tangent at a given point in the curve.
        /// </summary>
        protected Vector2D GetCenterTangent(int first, int last, int split, double epsilon = double.Epsilon)
        {
            // Because we want to maintain C1 continuity on the spline, the tangents on either side must be inverses of one another.
            Debug.Assert(first < split && split < last);
            var splitLen = arclen[split];
            var pSplit = points[split];

            // Left side
            var firstLen = arclen[first];
            var partLen = splitLen - firstLen;
            var total = default(Vector2D);
            var weightTotal = 0d;

            for (var i = Max(first, split - MidTangentNPoints); i < split; i++)
            {
                var t = (arclen[i] - firstLen) / partLen;
                var weight = t * t * t;
                var v = (points[i] - pSplit).Normalize();
                total += v * weight;
                weightTotal += weight;
            }

            var tanL = total.Length > epsilon && weightTotal > epsilon ?
                (total / weightTotal).Normalize() :
                (points[split - 1] - pSplit).Normalize();

            // Right side
            partLen = arclen[last] - splitLen;
            var rMax = Min(last, split + MidTangentNPoints);
            total = default;
            weightTotal = 0;

            for (var i = split + 1; i <= rMax; i++)
            {
                var ti = 1d - ((arclen[i] - splitLen) / partLen);
                var weight = ti * ti * ti;
                var v = (pSplit - points[i]).Normalize();
                total += v * weight;
                weightTotal += weight;
            }

            var tanR = total.Length > epsilon && weightTotal > epsilon ?
                (total / weightTotal).Normalize() :
                (pSplit - points[split + 1]).Normalize();

            // The reason we separate this into two halves is because we want the right and left tangents to be weighted
            // equally no matter the weights of the individual parts of them, so that one of the curves doesn't get screwed
            // for the pleasure of the other half
            total = tanL + tanR;

            // Since the points are never coincident, the vector between any two of them will be normalizable, however this can happen in some really
            // odd cases when the points are going directly opposite directions (therefore the tangent is undefined)
            if (total.LengthSquared < epsilon)
            {
                // try one last time using only the three points at the center, otherwise just use one of the sides
                tanL = (points[split - 1] - pSplit).Normalize();
                tanR = (pSplit - points[split + 1]).Normalize();
                total = tanL + tanR;
                return total.LengthSquared < epsilon ? tanL : (total / 2d).Normalize();
            }
            else
            {
                return (total / 2d).Normalize();
            }
        }

        /// <summary>
        /// Gets the tangent for the end of the curve.
        /// </summary>
        protected Vector2D GetRightTangent(int first, double epsilon = double.Epsilon)
        {
            var totalLen = arclen[^1];
            var p3 = points[^1];
            var tanR = (points[^2] - p3).Normalize();
            var total = tanR;
            var weightTotal = 1d;
            first = Max(points.Count - (EndTangentNPoints + 1), first + 1);

            for (var i = points.Count - 3; i >= first; i--)
            {
                var t = arclen[i] / totalLen;
                var weight = t * t * t;
                var v = (points[i] - p3).Normalize();
                total += v * weight;
                weightTotal += weight;
            }

            if (total.Length > epsilon)
            {
                tanR = (total / weightTotal).Normalize();
            }

            return tanR;
        }

        /// <summary>
        /// Tries to fit single Bézier curve to the points in [first ... last]. Destroys anything in <see cref="u"/> in the process.
        /// Assumes there are at least two points to fit.
        /// </summary>
        /// <param name="first">Index of first point to consider.</param>
        /// <param name="last">Index of last point to consider (inclusive).</param>
        /// <param name="tanL">Tangent at the start of the curve ("left").</param>
        /// <param name="tanR">Tangent on the end of the curve ("right").</param>
        /// <param name="curve">The fitted curve.</param>
        /// <param name="split">Point at which to split if this method returns false.</param>
        /// <returns>true if the fit was within error tolerance, false if the curve should be split. Even if this returns false, curve will contain
        /// a curve that somewhat fits the points; it's just outside error tolerance.</returns>
        protected bool FitCurve(int first, int last, Vector2D tanL, Vector2D tanR, out CubicBezier2D curve, out int split)
        {
            var nPts = last - first + 1;
            if (nPts < 2)
            {
                throw new InvalidOperationException("INTERNAL ERROR: Should always have at least 2 points here");
            }
            else if (nPts == 2)
            {
                // If we only have 2 points left, estimate the curve using Wu/Barsky.
                var p0 = points[first];
                var p3 = points[last];
                var alpha = Measurements.Distance(p0, p3) / 3d;
                var p1 = (tanL * alpha) + p0;
                var p2 = (tanR * alpha) + p3;
                curve = new CubicBezier2D(p0, p1, p2, p3);
                split = 0;
                return true;
            }
            else
            {
                split = 0;
                ArcLengthParamaterize(first, last); // Initially start u with a simple chord-length parameterization.
                curve = default;
                for (var i = 0; i < MaxItterations + 1; i++)
                {
                    // Use newton's method to find better parameters (except on first run, since we don't have a curve yet).
                    if (i != 0)
                    {
                        Reparameterize(first, last, curve);
                    }

                    // Generate the curve itself
                    curve = GenerateCubicBezier(first, last, tanL, tanR);

                    // Calculate error and get split point (point of max error)
                    var error = FindMaxSquaredError(first, last, curve, out split);

                    // If we're within error tolerance, awesome!
                    if (error < squaredError)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Builds the arc length array using the points array. Assumes _pts has points and _arclen is empty.
        /// </summary>
        protected void InitializeArcLengths()
        {
            var count = points.Count;
            Debug.Assert(arclen.Count == 0);
            arclen.Add(0);
            var clen = 0d;
            var pp = points[0];
            for (var i = 1; i < count; i++)
            {
                var np = points[i];
                clen += Measurements.Distance(pp, np);
                arclen.Add(clen);
                pp = np;
            }
        }

        /// <summary>
        /// Initializes the first (last - first) elements of u with scaled arc lengths.
        /// </summary>
        protected void ArcLengthParamaterize(int first, int last)
        {
            u.Clear();
            var diff = arclen[last] - arclen[first];
            var start = arclen[first];
            var nPts = last - first;
            u.Add(0);
            for (var i = 1; i < nPts; i++)
            {
                u.Add((arclen[first + i] - start) / diff);
            }

            u.Add(1);
        }

        /// <summary>
        /// Generates a Bézier curve for the segment using a least-squares approximation. for the derivation of this and why it works,
        /// see http://read.pudn.com/downloads141/ebook/610086/Graphics_Gems_I.pdf page 626 and beyond. tl;dr: math.
        /// </summary>
        protected CubicBezier2D GenerateCubicBezier(int first, int last, Vector2D tanL, Vector2D tanR, double epsilon = double.Epsilon)
        {
            var nPts = last - first + 1;
            var p0 = points[first];
            var p3 = points[last]; // First and last points of curve are actual points on data.
            double c00 = 0d, c01 = 0d, c11 = 0d, x0 = 0d, x1 = 0d; // Matrix members -- both C[0,1] and C[1,0] are the same, stored in c01.
            for (var i = 1; i < nPts; i++)
            {
                // Calculate cubic Bézier multipliers.
                var t = u[i];
                var ti = 1d - t;
                var t0 = ti * ti * ti;
                var t1 = 3d * ti * ti * t;
                var t2 = 3d * ti * t * t;
                var t3 = t * t * t;

                // For X matrix; moving this up here since profiling shows it's better up here (maybe a0/a1 not in registers vs only v not in regs).
                var s = (p0 * t0) + (p0 * t1) + (p3 * t2) + (p3 * t3); // NOTE: this would be Q(t) if p1=p0 and p2=p3.
                var v = points[first + i] - s;

                // C matrix.
                var a0 = tanL * t1;
                var a1 = tanR * t2;
                c00 += Operations.DotProduct(a0, a0);
                c01 += Operations.DotProduct(a0, a1);
                c11 += Operations.DotProduct(a1, a1);

                // X matrix.
                x0 += Operations.DotProduct((Point2D)a0, v);
                x1 += Operations.DotProduct((Point2D)a1, v);
            }

            // Determinants of X and C matrices.
            var det_C0_C1 = (c00 * c11) - (c01 * c01);
            var det_C0_X = (c00 * x1) - (c01 * x0);
            var det_X_C1 = (x0 * c11) - (x1 * c01);
            var alphaL = det_X_C1 / det_C0_C1;
            var alphaR = det_C0_X / det_C0_C1;

            // If alpha is negative, zero, or very small (or we can't trust it since C matrix is small), fall back to Wu/Barsky heuristic.
            var linDist = Measurements.Distance(p0, p3);
            var epsilon2 = epsilon * linDist;
            if (Abs(det_C0_C1) < epsilon || alphaL < epsilon2 || alphaR < epsilon2)
            {
                var alpha = linDist / 3d;
                var p1 = (tanL * alpha) + p0;
                var p2 = (tanR * alpha) + p3;
                return new CubicBezier2D(p0, p1, p2, p3);
            }
            else
            {
                var p1 = (tanL * alphaL) + p0;
                var p2 = (tanR * alphaR) + p3;
                return new CubicBezier2D(p0, p1, p2, p3);
            }
        }

        /// <summary>
        /// Attempts to find a slightly better parameterization for u on the given curve.
        /// </summary>
        protected void Reparameterize(int first, int last, CubicBezier2D curve, double epsilon = double.Epsilon)
        {
            if (curve is null) return;
            var nPts = last - first;

            for (var i = 1; i < nPts; i++)
            {
                var p = points[first + i];
                var t = u[i];
                var ti = 1d - t;

                // Control vertices for Q'.
                var qp0 = (curve.B - curve.A) * 3d;
                var qp1 = (curve.C - curve.B) * 3d;
                var qp2 = (curve.D - curve.C) * 3d;

                // Control vertices for Q''.
                var qpp0 = (qp1 - qp0) * 2d;
                var qpp1 = (qp2 - qp1) * 2d;

                // Evaluate Q(t), Q'(t), and Q''(t).
                var p0 = curve.Interpolate(t);
                var p1 = (ti * ti * qp0) + (2d * ti * t * qp1) + (t * t * qp2);
                var p2 = (ti * qpp0) + (t * qpp1);

                // These are the actual fitting calculations using http://en.wikipedia.org/wiki/Newton%27s_method.
                var num = ((p0.X - p.X) * p1.I) + ((p0.Y - p.Y) * p1.J);
                var den = (p1.I * p1.I) + (p1.J * p1.J) + ((p0.X - p.X) * p2.I) + ((p0.Y - p.Y) * p2.J);
                var newU = t - (num / den);

                if (Abs(den) > epsilon && newU >= 0 && newU <= 1d)
                {
                    u[i] = newU;
                }
            }
        }

        /// <summary>
        /// Computes the maximum squared distance from a point to the curve using the current parameterization.
        /// </summary>
        protected double FindMaxSquaredError(int first, int last, CubicBezier2D curve, out int split)
        {
            if (curve is null)
            {
                split = 0;
                return 0d;
            }

            var s = (last - first + 1) / 2;
            var nPts = last - first + 1;
            double max = 0;
            for (var i = 1; i < nPts; i++)
            {
                var v0 = points[first + i];
                var v1 = curve.Interpolate(u[i]);
                var d = Measurements.SquareDistance(v0, v1);
                if (d > max)
                {
                    max = d;
                    s = i;
                }
            }

            // Split at point of maximum error.
            split = s + first;
            if (split <= first)
            {
                split = first + 1;
            }

            if (split >= last)
            {
                split = last - 1;
            }

            return max;
        }
    }
}
