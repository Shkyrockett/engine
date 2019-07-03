// <copyright file="IntersectionsDrawable.cs" company="Drawable.IO" >
//     Copyright © 2014 - 2015 Stephan Smola. All rights reserved.
// </copyright>
// <author id="drawableIO">Stephan Smola</author>
// <license>
//     Licensed not listed.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.BobLyonCommon;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Measurements;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class IntersectionsDrawable
    {
        /// <summary>
        /// Intersect two arbitrarily rotated ellipses. In the general case this boils down to solving a quartic equation. This can have complex results, that are ignored.
        /// Special cases like intersecting two circles or intersecting two congruent ellipses are considered. The latter can be reduced to intersecting a line with one of the ellipses.
        /// This could numerically be improved by not carrying so many intermediate results, I guess. Also the rotation to avoid problems with ellipses that are not rotated introduces numeric error as well.
        /// </summary>
        /// <param name="ellipse1">The ellipse1.</param>
        /// <param name="ellipse2">The ellipse2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        /// <acknowledgment>
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/
        /// http://en.wikipedia.org/wiki/Quartic_function#General_formula_for_roots
        /// ... and some Wolfram alpha.
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y)[] IntersectEllipseEllipse(((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse1, ((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse2, double epsilon = Epsilon)
        {
            _ = epsilon;
            var v = new List<(double x, double y)>();

            (double cos, double sin) cosSinAngle1 = (Cos(ellipse1.angle), Sin(ellipse1.angle));
            (double cos, double sin) cosSinAngle2 = (Cos(ellipse2.angle), Sin(ellipse2.angle));

            if ((ellipse1.radiusX == ellipse1.radiusY) && (ellipse2.radiusX == ellipse2.radiusY))
            {
                // Special case: Two circles
                return IntersectCircleCircle((ellipse1.origin, ellipse1.radiusX), (ellipse2.origin, ellipse2.radiusX));
            }
            else if (((ellipse1.angle == ellipse2.angle) || Abs(ellipse1.angle - ellipse2.angle) == PI) && (ellipse1.radiusX == ellipse2.radiusX) && (ellipse1.radiusY == ellipse2.radiusY))
            {
                // Special cases congruent ellipses incl. rotation
                // There are at max two intersection points: We can construct a line that runs through these points
                var l = GetLine(cosSinAngle1, ellipse1.radiusX, ellipse1.radiusY, ellipse1.origin, ellipse2.origin);
                return IntersectLineEllipse(l, ellipse1);
            }
            else if (((Abs(ellipse1.angle - ellipse2.angle) == HalfPi) || Abs(ellipse1.angle - ellipse2.angle) == PI * 3d / 2d) && (ellipse1.radiusX == ellipse2.radiusY) && (ellipse1.radiusY == ellipse2.radiusX))
            {
                // Special cases congruent ellipses incl. rotation but one is 90 rotated and the radius sizes are swapped
                // There are at max two intersection points: We can construct a line that runs through these points
                var l = GetLine(cosSinAngle2, ellipse1.radiusX, ellipse1.radiusY, ellipse1.origin, ellipse2.origin);
                return IntersectLineEllipse(l, ellipse1);
            }
            else
            {
                // General case
                // Test for one situation:
                // One of the ellipses axis is parallel to x- or y-axis.
                // To avoid special cases testing in getY we simply rotate everything around ellipse1 origin by something and later
                // rotate the results back.
                var mPI1 = ellipse1.angle % PI;
                var mPI2 = ellipse2.angle % PI;
                var corr = 0d;

                if ((mPI1 == 0) || (mPI2 == 0))
                {
                    corr = 0.05d;
                }

                (double a, double b, double c, double d, double e, double f) e1;
                (double a, double b, double c, double d, double e, double f) e2;

                if (corr != 0d)
                {
                    e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, ellipse1.angle + corr);
                    e2 = GetQuadratic(RotatePoint2D(ellipse2.origin.x, ellipse2.origin.y, corr, ellipse1.origin.x, ellipse1.origin.y), ellipse2.radiusX, ellipse2.radiusY, ellipse2.angle + corr);
                }
                else
                {
                    e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, cosSinAngle1);
                    e2 = GetQuadratic(ellipse2.origin, ellipse2.radiusX, ellipse2.radiusY, cosSinAngle2);
                }

                var (a, b, c, d, e) = GetQuartic(e1, e2);
                var y = Operations.QuarticRoots(a, b, c, d, e).ToArray();

                v.AddRange(CalculatePoints(y, e1, e2));

                if (corr != 0d)
                {
                    for (var i = 0; i < v.Count; i++)
                    {
                        v[i] = RotatePoint2D(v[i].x, v[i].y, -corr, ellipse1.origin.x, ellipse1.origin.y);
                    }
                }
            }

            return v.ToArray();
        }

        /// <summary>
        /// The intersect LE.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double x, double y)[] IntersectLineEllipse(((double x, double y) origin, double angle) l, ((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse, double epsilon = Epsilon)
            => IntersectLineEllipse((l.origin, (Cos(l.angle), Sin(l.angle))), (ellipse.origin, ellipse.radiusX, ellipse.radiusY, (Cos(ellipse.angle), Sin(ellipse.angle))), epsilon);

        /// <summary>
        /// The intersect LE.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="ellipse">The ellipse1.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double x, double y)[] IntersectLineEllipse(((double x, double y) origin, (double cos, double sin) angle) l, ((double x, double y) origin, double radiusX, double radiusY, (double cos, double sin) angle) ellipse, double epsilon = Epsilon)
        {
            _ = epsilon;
            var cx = ellipse.origin.x;
            var cy = ellipse.origin.y;
            var rx = ellipse.radiusX;
            var ry = ellipse.radiusY;
            var cosA = ellipse.angle.cos;
            var sinA = ellipse.angle.sin;
            var lx = l.origin.x;
            var ly = l.origin.y;
            var li = l.angle.cos;
            var lj = l.angle.sin;

            // Initialize the resulting intersection structure.
            var result = new List<(double x, double y)>();

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((lx == li) && (ly == lj)))
            {
                return result.ToArray();
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = lx - cx;
            var v1 = ly - cy;
            var u2 = lx + li - cx;
            var v2 = ly + lj - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);
            var u2A = (u2 * cosA) - (v2 * -sinA);
            var v2A = (u2 * -sinA) + (v2 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                //result.State |= IntersectionState.Outside;
                return result.ToArray();
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = -b / (2d * a);

                // Add the point.
                result.Add((u1 + ((u2 - u1) * t) + cx, v1 + ((v2 - v1) * t) + cy));
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                var t2 = (-b - Sqrt(discriminant)) / (2d * a);

                // Add the points.
                result.Add((u1 + ((u2 - u1) * t1) + cx, v1 + ((v2 - v1) * t1) + cy));
                result.Add((u1 + ((u2 - u1) * t2) + cx, v1 + ((v2 - v1) * t2) + cy));
            }

            // ToDo: Return IntersectionState.Inside if both points are inside the Ellipse.

            //// Return the intersections.
            //if (result.Count > 0)
            //{
            //    result.State |= IntersectionState.Intersection;
            //}

            return result.ToArray();
        }

        /// <summary>
        /// The intersect CC.
        /// </summary>
        /// <param name="c1">The p1.</param>
        /// <param name="c2">The p2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double x, double y)[] IntersectCircleCircle(((double x, double y) origin, double radius) c1, ((double x, double y) origin, double radius) c2, double epsilon = Epsilon)
        {
            _ = epsilon;
            var result = new List<(double x, double y)>();

            var r_max = c1.radius + c2.radius;
            var r_min = Abs(c1.radius - c2.radius);
            var c_dist = Distance(c1.origin.x, c1.origin.y, c2.origin.x, c2.origin.y);

            if (c_dist > r_max)
            {
                //result = new Intersection(IntersectionState.Outside);
            }
            else if (c_dist < r_min)
            {
                //result = new Intersection(IntersectionState.Inside);
            }
            else
            {
                //result = new Intersection(IntersectionState.Intersection);
                var a = ((c1.radius * c1.radius) - (c2.radius * c2.radius) + (c_dist * c_dist)) / (2d * c_dist);
                var h = Sqrt((c1.radius * c1.radius) - (a * a));
                var (x, y) = Lerp(c1.origin.x, c1.origin.y, c2.origin.x, c2.origin.y, a / c_dist);
                var b = h / c_dist;
                result.Add((x - (b * (c2.origin.y - c1.origin.y)), y + (b * (c2.origin.x - c1.origin.x))));
                result.Add((x + (b * (c2.origin.y - c1.origin.y)), y - (b * (c2.origin.x - c1.origin.x))));
            }

            return result.ToArray();
        }

        /// <summary>
        /// This basically calculates the rational roots of the quartic.
        /// </summary>
        /// <param name="quartics">The quartics.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:double[]"/>.</returns>
        /// <acknowledgment>
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] QuarticRoots((double a, double b, double c, double d, double e) quartics, double epsilon = Epsilon)
        {
            (var a, var b, var c, var d, var e) = quartics;
            //var delta = (256d * a * a * a * e * e * e) - (192d * a * a * b * d * e * e) - (128d * a * a * c * c * e * e) + (144d * a * a * c * d * d * e) - (27d * a * a * d * d * d * d) + (144d * a * b * b * c * e * e) - (6d * a * b * b * d * d * e) - (80d * a * b * c * c * d * e) + (18d * a * b * c * d * d * d) + (16d * a * c * c * c * c * e) - (4d * a * c * c * c * d * d) - (27d * b * b * b * b * e * e) + (18d * b * b * b * c * d * e) - (4d * b * b * b * d * d * d) - (4d * b * b * c * c * c * e) + (b * b * c * c * d * d);
            //var P = (8d * a * c) - (3d * b * b);
            //var D = (64d * a * a * a * e) - (16d * a * a * c * c) + (16d * a * b * b * c) - (16d * a * a * b * d) - (3d * b * b * b * b);
            var d0 = (c * c) - (3d * b * d) + (12d * a * e);
            var d1 = (2d * c * c * c) - (9d * b * c * d) + (27d * b * b * e) + (27d * a * d * d) - (72d * a * c * e);
            var p = ((8 * a * c) - (3d * b * b)) / (8d * a * a);
            var q = ((b * b * b) - (4d * a * b * c) + (8 * a * a * d)) / (8d * a * a * a);
            var phi = Acos(d1 / (2d * Sqrt(d0 * d0 * d0)));

            double S;
            if (double.IsNaN(phi) && (d1 == 0d))
            {
                // if (delta < 0) I guess the new test is okay because we're only interested in real roots
                var Q = d1 + Sqrt((d1 * d1) - (4d * d0 * d0 * d0));
                Q /= 2d;
                Q = Pow(Q, 1d / 3d);
                S = 0.5d * Sqrt((-2d / 3d * p) + (1d / (3d * a) * (Q + (d0 / Q))));
            }
            else
            {
                S = 0.5d * Sqrt((-2d / 3d * p) + (2d / (3d * a) * Sqrt(d0) * Cos(phi / 3d)));
            }

            var y = new List<double>();
            if (S != 0d)
            {
                var R = (-4d * S * S) - (2d * p) + (q / S);

                if (Abs(R) < epsilon)
                {
                    R = 0d;
                }

                if (R > 0d)
                {
                    R = 0.5d * Sqrt(R);
                    y.Add((-b / (4 * a)) - S + R);
                    y.Add((-b / (4 * a)) - S - R);
                }
                else if (Abs(R) < epsilon)
                {
                    y.Add((-b / (4d * a)) - S);
                }

                R = (-4d * S * S) - (2d * p) - (q / S);

                if (Abs(R) < epsilon)
                {
                    R = 0d;
                }

                if (R > 0d)
                {
                    R = 0.5d * Sqrt(R);
                    y.Add((-b / (4d * a)) + S + R);
                    y.Add((-b / (4d * a)) + S - R);
                }
                else if (R == 0d)
                {
                    y.Add((-b / (4d * a)) + S);
                }
            }

            return y.ToArray();
        }

        /// <summary>
        /// Calculate the x coordinates for the given y coordinates.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="el1">The el1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        /// <acknowledgment>
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y)[] CalculatePoints(double[] y, (double a, double b, double c, double d, double e, double f) el1, (double a, double b, double c, double d, double e, double f) e2, double epsilon = Epsilon)
        {
            _ = epsilon;
            var r = new List<(double x, double y)>();
            for (var i = 0; i < y.Length; i++)
            {
                var x = -((el1.a * e2.f) + (el1.a * e2.c * y[i] * y[i]) - (e2.a * el1.c * y[i] * y[i]) + (el1.a * e2.e * y[i]) - (e2.a * el1.e * y[i]) - (e2.a * el1.f)) / ((el1.a * e2.b * y[i]) + (el1.a * e2.d) - (e2.a * el1.b * y[i]) - (e2.a * el1.d));
                r.Add((x, y[i]));
            }
            return r.ToArray();
        }

        /// <summary>
        /// Calculates the line that runs through the intersection points of two congruent ellipses with the same rotation.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:((double x, double y) v, double z)"/>.</returns>
        /// <acknowledgment>
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ((double x, double y) v, double angle) GetLine(double rotation, double rx, double ry, (double x, double y) o1, (double x, double y) o2, double epsilon = Epsilon)
            => GetLine((Cos(rotation), Sin(rotation)), rx, ry, o1, o2, epsilon);

        /// <summary>
        /// Calculates the line that runs through the intersection points of two congruent ellipses with the same rotation.
        /// </summary>
        /// <param name="rotation">The rotation.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:((double x, double y) v, double z)"/>.</returns>
        /// <acknowledgment>
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ((double x, double y) p, double angle) GetLine((double cos, double sin) rotation, double rx, double ry, (double x, double y) o1, (double x, double y) o2, double epsilon = Epsilon)
        {
            _ = epsilon;
            // a squared.
            //var a = rx;
            var a2 = rx * rx;

            // b squared.
            //var b = ry;
            var b2 = ry * ry;

            var (cosT, sinT) = (rotation.cos, rotation.sin);

            // The polynomial coefficients for the initial rotation.
            var aa = (cosT * cosT / a2) + (sinT * sinT / b2);
            var bb = (2d * cosT * sinT / b2) - (2d * cosT * sinT / a2);
            var cc = (cosT * cosT / b2) + (sinT * sinT / a2);

            // Working with Ellipse 1.
            var x1 = (bb * o1.y) - (2d * aa * o1.x);
            var y1 = (bb * o1.x) + (2d * cc * o1.y);
            var v = (aa * o1.x * o1.x) + (bb * o1.x * o1.y) + (cc * o1.y * o1.y);

            // Working with Ellipse 2.
            var x2 = (bb * o2.y) - (2d * aa * o2.x);
            var y2 = (bb * o2.x) + (2d * cc * o2.y);
            var z = (aa * o2.x * o2.x) + (bb * o2.x * o2.y) + (cc * o2.y * o2.y);

            var angle = z - v; // Is z-v an angle? Can I get a direction vector instead?

            return ((x1 - x2, y2 - y1), angle.ToRadians()); // Why don't the y values make any sense? They seem to always be near 0.
        }
    }
}
