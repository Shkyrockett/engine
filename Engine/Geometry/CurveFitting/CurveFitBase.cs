// <copyright file="CurveFitBase.cs" >
//     Copyright (c) 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> Adapted from: https://github.com/burningmime/curves </remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        #endregion

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

        #endregion

        /// <summary>
        /// Gets the tangent for the start of the cure.
        /// </summary>
        protected Vector2D GetLeftTangent(int last)
        {
            double totalLen = arclen[arclen.Count - 1];
            var p0 = points[0];
            var tanL = Primitives.Normalize(points[1] - p0);
            var total = tanL;
            double weightTotal = 1;
            last = Math.Min(EndTangentNPoints, last - 1);
            for (int i = 2; i <= last; i++)
            {
                double ti = 1 - (arclen[i] / totalLen);
                double weight = ti * ti * ti;
                var v = Primitives.Normalize(points[i] - p0);
                total += v * weight;
                weightTotal += weight;
            }
            // if the vectors add up to zero (IE going opposite directions), there's no way to normalize them
            if (total.Length > Maths.Epsilon)
                tanL = Primitives.Normalize(total / weightTotal);
            return tanL;
        }

        /// <summary>
        /// Gets the tangent for the end of the curve.
        /// </summary>
        protected Vector2D GetRightTangent(int first)
        {
            double totalLen = arclen[arclen.Count - 1];
            Point2D p3 = points[points.Count - 1];
            var tanR = (points[points.Count - 2] - p3).Normalize();
            var total = tanR;
            double weightTotal = 1;
            first = Math.Max(points.Count - (EndTangentNPoints + 1), first + 1);
            for (int i = points.Count - 3; i >= first; i--)
            {
                double t = arclen[i] / totalLen;
                double weight = t * t * t;
                var v = (points[i] - p3).Normalize();
                total += v * weight;
                weightTotal += weight;
            }
            if (total.Length > Maths.Epsilon)
                tanR = (total / weightTotal).Normalize();
            return tanR;
        }

        /// <summary>
        /// Gets the tangent at a given point in the curve.
        /// </summary>
        protected Vector2D GetCenterTangent(int first, int last, int split)
        {
            // because we want to maintain C1 continuity on the spline, the tangents on either side must be inverses of one another
            Debug.Assert(first < split && split < last);
            double splitLen = arclen[split];
            Point2D pSplit = points[split];

            // left side
            double firstLen = arclen[first];
            double partLen = splitLen - firstLen;
            Vector2D total = default(Vector2D);
            double weightTotal = 0;
            for (int i = Math.Max(first, split - MidTangentNPoints); i < split; i++)
            {
                double t = (arclen[i] - firstLen) / partLen;
                double weight = t * t * t;
                var v = (points[i] - pSplit).Normalize();
                total += v * weight;
                weightTotal += weight;
            }
            Vector2D tanL = total.Length > Maths.Epsilon && weightTotal > Maths.Epsilon ?
                (total / weightTotal).Normalize() :
                (points[split - 1] - pSplit).Normalize();

            // right side
            partLen = arclen[last] - splitLen;
            int rMax = Math.Min(last, split + MidTangentNPoints);
            total = default(Vector2D);
            weightTotal = 0;
            for (int i = split + 1; i <= rMax; i++)
            {
                double ti = 1 - ((arclen[i] - splitLen) / partLen);
                double weight = ti * ti * ti;
                var v = (pSplit - points[i]).Normalize();
                total += v * weight;
                weightTotal += weight;
            }
            Vector2D tanR = total.Length > Maths.Epsilon && weightTotal > Maths.Epsilon ?
                (total / weightTotal).Normalize() :
                (pSplit - points[split + 1]).Normalize();

            // The reason we separate this into two halves is because we want the right and left tangents to be weighted
            // equally no matter the weights of the individual parts of them, so that one of the curves doesn't get screwed
            // for the pleasure of the other half
            total = tanL + tanR;

            // Since the points are never coincident, the vector between any two of them will be normalizable, however this can happen in some really
            // odd cases when the points are going directly opposite directions (therefore the tangent is undefined)
            if (total.LengthSquared < Maths.Epsilon)
            {
                // try one last time using only the three points at the center, otherwise just use one of the sides
                tanL = (points[split - 1] - pSplit).Normalize();
                tanR = (pSplit - points[split + 1]).Normalize();
                total = tanL + tanR;
                return total.LengthSquared < Maths.Epsilon ? tanL : (total / 2).Normalize();
            }
            else
            {
                return (total / 2).Normalize();
            }
        }

        /// <summary>
        /// Tries to fit single Bezier curve to the points in [first ... last]. Destroys anything in <see cref="u"/> in the process.
        /// Assumes there are at least two points to fit.
        /// </summary>
        /// <param name="first">Index of first point to consider.</param>
        /// <param name="last">Index of last point to consider (inclusive).</param>
        /// <param name="tanL">Tangent at teh start of the curve ("left").</param>
        /// <param name="tanR">Tangent on the end of the curve ("right").</param>
        /// <param name="curve">The fitted curve.</param>
        /// <param name="split">Point at which to split if this method returns false.</param>
        /// <returns>true if the fit was within error tolerence, false if the curve should be split. Even if this returns false, curve will contain
        /// a curve that somewhat fits the points; it's just outside error tolerance.</returns>
        protected bool FitCurve(int first, int last, Vector2D tanL, Vector2D tanR, out CubicBezier curve, out int split)
        {
            int nPts = last - first + 1;
            if (nPts < 2)
            {
                throw new InvalidOperationException("INTERNAL ERROR: Should always have at least 2 points here");
            }
            else if (nPts == 2)
            {
                // if we only have 2 points left, estimate the curve using Wu/Barsky
                Point2D p0 = points[first];
                Point2D p3 = points[last];
                double alpha = Measurements.Distance(p0, p3) / 3;
                var p1 = (tanL * alpha) + p0;
                var p2 = (tanR * alpha) + p3;
                curve = new CubicBezier(p0, (Point2D)p1, (Point2D)p2, p3);
                split = 0;
                return true;
            }
            else
            {
                split = 0;
                ArcLengthParamaterize(first, last); // initially start u with a simple chord-length parameterization
                curve = default(CubicBezier);
                for (int i = 0; i < MaxItterations + 1; i++)
                {
                    if (i != 0) Reparameterize(first, last, curve);                                  // use newton's method to find better parameters (except on first run, since we don't have a curve yet)
                    curve = GenerateCubicBezier(first, last, tanL, tanR);                                // generate the curve itself
                    double error = FindMaxSquaredError(first, last, curve, out split);               // calculate error and get split point (point of max error)
                    if (error < squaredError) return true;                                         // if we're within error tolerance, awesome!
                }
                return false;
            }
        }

        /// <summary>
        /// Builds the arc length array using the points array. Assumes _pts has points and _arclen is empty.
        /// </summary>
        protected void InitializeArcLengths()
        {
            int count = points.Count;
            Debug.Assert(arclen.Count == 0);
            arclen.Add(0);
            double clen = 0;
            Point2D pp = points[0];
            for (int i = 1; i < count; i++)
            {
                Point2D np = points[i];
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
            double diff = arclen[last] - arclen[first];
            double start = arclen[first];
            int nPts = last - first;
            u.Add(0);
            for (int i = 1; i < nPts; i++)
                u.Add((arclen[first + i] - start) / diff);
            u.Add(1);
        }

        /// <summary>
        /// Generates a bezier curve for the segment using a least-squares approximation. for the derivation of this and why it works,
        /// see http://read.pudn.com/downloads141/ebook/610086/Graphics_Gems_I.pdf page 626 and beyond. tl;dr: math.
        /// </summary>
        protected CubicBezier GenerateCubicBezier(int first, int last, Vector2D tanL, Vector2D tanR)
        {
            int nPts = last - first + 1;
            Point2D p0 = points[first], p3 = points[last]; // first and last points of curve are actual points on data
            double c00 = 0, c01 = 0, c11 = 0, x0 = 0, x1 = 0; // matrix members -- both C[0,1] and C[1,0] are the same, stored in c01
            for (int i = 1; i < nPts; i++)
            {
                // Calculate cubic bezier multipliers
                double t = u[i];
                double ti = 1 - t;
                double t0 = ti * ti * ti;
                double t1 = 3 * ti * ti * t;
                double t2 = 3 * ti * t * t;
                double t3 = t * t * t;

                // For X matrix; moving this up here since profiling shows it's better up here (maybe a0/a1 not in registers vs only v not in regs)
                var s = (p0 * t0) + (p0 * t1) + (p3 * t2) + (p3 * t3); // NOTE: this would be Q(t) if p1=p0 and p2=p3
                var v = points[first + i] - s;

                // C matrix
                var a0 = tanL * t1;
                var a1 = tanR * t2;
                c00 += Primitives.DotProduct(a0, a0);
                c01 += Primitives.DotProduct(a0, a1);
                c11 += Primitives.DotProduct(a1, a1);

                // X matrix
                x0 += Primitives.DotProduct((Point2D)a0, v);
                x1 += Primitives.DotProduct((Point2D)a1, v);
            }

            // determinants of X and C matrices
            double det_C0_C1 = c00 * c11 - c01 * c01;
            double det_C0_X = c00 * x1 - c01 * x0;
            double det_X_C1 = x0 * c11 - x1 * c01;
            double alphaL = det_X_C1 / det_C0_C1;
            double alphaR = det_C0_X / det_C0_C1;

            // if alpha is negative, zero, or very small (or we can't trust it since C matrix is small), fall back to Wu/Barsky heuristic
            double linDist = Measurements.Distance(p0, p3);
            double epsilon2 = Maths.Epsilon * linDist;
            if (Math.Abs(det_C0_C1) < Maths.Epsilon || alphaL < epsilon2 || alphaR < epsilon2)
            {
                double alpha = linDist / 3;
                var p1 = (tanL * alpha) + p0;
                var p2 = (tanR * alpha) + p3;
                return new CubicBezier(p0, p1, p2, p3);
            }
            else
            {
                var p1 = (tanL * alphaL) + p0;
                var p2 = (tanR * alphaR) + p3;
                return new CubicBezier(p0, p1, p2, p3);
            }
        }

        /// <summary>
        /// Attempts to find a slightly better parameterization for u on the given curve.
        /// </summary>
        protected void Reparameterize(int first, int last, CubicBezier curve)
        {
            int nPts = last - first;
            for (int i = 1; i < nPts; i++)
            {
                Point2D p = points[first + i];
                double t = u[i];
                double ti = 1 - t;

                // Control vertices for Q'
                var qp0 = (curve.B - curve.A) * 3;
                var qp1 = (curve.C - curve.B) * 3;
                var qp2 = (curve.D - curve.C) * 3;

                // Control vertices for Q''
                var qpp0 = (qp1 - qp0) * 2;
                var qpp1 = (qp2 - qp1) * 2;

                // Evaluate Q(t), Q'(t), and Q''(t)
                var p0 = curve.Sample(t);
                var p1 = ((ti * ti) * qp0) + ((2 * ti * t) * qp1) + ((t * t) * qp2);
                var p2 = (ti * qpp0) + (t * qpp1);

                // these are the actual fitting calculations using http://en.wikipedia.org/wiki/Newton%27s_method
                double num = ((p0.X - p.X) * p1.I) + ((p0.Y - p.Y) * p1.J);
                double den = (p1.I * p1.I) + (p1.J * p1.J) + ((p0.X - p.X) * p2.I) + ((p0.Y - p.Y) * p2.J);
                double newU = t - num / den;
                if (Math.Abs(den) > Maths.Epsilon && newU >= 0 && newU <= 1)
                    u[i] = newU;
            }
        }

        /// <summary>
        /// Computes the maximum squared distance from a point to the curve using the current parameterization.
        /// </summary>
        protected double FindMaxSquaredError(int first, int last, CubicBezier curve, out int split)
        {
            int s = (last - first + 1) / 2;
            int nPts = last - first + 1;
            double max = 0;
            for (int i = 1; i < nPts; i++)
            {
                Point2D v0 = points[first + i];
                Point2D v1 = curve.Sample(u[i]);
                double d = Measurements.SquareDistance(v0, v1);
                if (d > max)
                {
                    max = d;
                    s = i;
                }
            }

            // split at point of maximum error
            split = s + first;
            if (split <= first)
                split = first + 1;
            if (split >= last)
                split = last - 1;

            return max;
        }
    }
}
