/*
    Copyright (c) 2013, Kevin Lindsey
    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification,
    are permitted provided that the following conditions are met:

      Redistributions of source code must retain the above copyright notice, this
      list of conditions and the following disclaimer.

      Redistributions in binary form must reproduce the above copyright notice, this
      list of conditions and the following disclaimer in the documentation and/or
      other materials provided with the distribution.

      Neither the name of the {organization} nor the names of its
      contributors may be used to endorse or promote products derived from
      this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
    ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
    ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Intersections2
    {
        /**
         *  intersectQuadraticBezierQuadraticBezier
         *
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @param {Point2D} a3
         *  @param {Point2D} b1
         *  @param {Point2D} b2
         *  @param {Point2D} b3
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectQuadraticBezierQuadraticBezier(Point2D a1, Point2D a2, Point2D a3, Point2D b1, Point2D b2, Point2D b3)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            var a = a2.Scale(-2);
            var c12 = a1.Add(a.Add(a3));

            a = a1.Scale(-2);
            var b = a2.Scale(2);
            var c11 = (Point2D)a.Add(b);

            var c10 = new Point2D(a1.X, a1.Y);

            a = b2.Scale(-2);
            var c22 = b1.Add(a.Add(b3));

            a = b1.Scale(-2);
            b = b2.Scale(2);
            var c21 = (Point2D)a.Add(b);

            var c20 = new Point2D(b1.X, b1.Y);

            var poly = new Polynomial();

            var v0 = 0d;
            var v1 = 0d;
            var v2 = 0d;
            var v3 = 0d;
            var v4 = 0d;
            var v5 = 0d;
            var v6 = 0d;
            if (c12.Y == 0)
            {
                v0 = c12.X * (c10.Y - c20.Y);
                v1 = v0 - c11.X * c11.Y;
                v2 = v0 + v1;
                v3 = c11.Y * c11.Y;

                poly = new Polynomial(
                    (c10.X - c20.X) * v3 + (c10.Y - c20.Y) * v1,
                    -c21.X * v3 - c21.Y * v0 - c21.Y * v1,
                    c12.X * c21.Y * c21.Y - c22.X * v3 - c22.Y * v0 - c22.Y * v1,
                    2 * c12.X * c21.Y * c22.Y,
                    c12.X * c22.Y * c22.Y
                );
            }
            else
            {
                v0 = c12.X * c22.Y - c12.Y * c22.X;
                v1 = c12.X * c21.Y - c21.X * c12.Y;
                v2 = c11.X * c12.Y - c11.Y * c12.X;
                v3 = c10.Y - c20.Y;
                v4 = c12.Y * (c10.X - c20.X) - c12.X * v3;
                v5 = -c11.Y * v2 + c12.Y * v4;
                v6 = v2 * v2;

                poly = new Polynomial(
                    (v3 * v6 + v4 * v5) / c12.Y,
                    (-c21.Y * v6 + c12.Y * v1 * v4 + v1 * v5) / c12.Y,
                    (-c22.Y * v6 + c12.Y * v1 * v1 + c12.Y * v0 * v4 + v0 * v5) / c12.Y,
                    2 * v0 * v1,
                    v0 * v0
                );
            }

            var roots = poly.Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];

                if (0 <= s && s <= 1)
                {
                    var xRoots = new Polynomial(
                        c10.X - c20.X - s * c21.X - s * s * c22.X,
                        c11.X,
                        c12.X
                    ).Roots();
                    var yRoots = new Polynomial(
                        c10.Y - c20.Y - s * c21.Y - s * s * c22.Y,
                        c11.Y,
                        c12.Y
                    ).Roots();

                    if (xRoots.Count > 0 && yRoots.Count > 0)
                    {
                        var TOLERANCE = 1e-4;

                        checkRoots:
                        for (var j = 0; j < xRoots.Count; j++)
                        {
                            var xRoot = xRoots[j];

                            if (0 <= xRoot && xRoot <= 1)
                            {
                                for (var k = 0; k < yRoots.Count; k++)
                                {
                                    if (Math.Abs(xRoot - yRoots[k]) < TOLERANCE)
                                    {
                                        result.Points.Add(c22.Scale(s * s).Add(c21.Scale(s).Add(c20)));
                                        goto checkRoots;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectQuadraticBezierCubicBezier
         *
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @param {Point2D} a3
         *  @param {Point2D} b1
         *  @param {Point2D} b2
         *  @param {Point2D} b3
         *  @param {Point2D} b4
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectQuadraticBezierCubicBezier(Point2D a1, Point2D a2, Point2D a3, Point2D b1, Point2D b2, Point2D b3, Point2D b4)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            var a = a2.Scale(-2);
            var c12 = a1.Add(a.Add(a3));

            a = a1.Scale(-2);
            var b = a2.Scale(2);
            var c11 = (Point2D)a.Add(b);

            var c10 = new Point2D(a1.X, a1.Y);

            a = b1.Scale(-1);
            b = b2.Scale(3);
            var c = b3.Scale(-3);
            var d = (Point2D)a.Add(b.Add(c.Add(b4)));
            var c23 = new Point2D(d.X, d.Y);

            a = b1.Scale(3);
            b = b2.Scale(-6);
            c = b3.Scale(3);
            d = a.Add(b.Add(c));
            var c22 = new Point2D(d.X, d.Y);

            a = b1.Scale(-3);
            b = b2.Scale(3);
            c = (Point2D)a.Add(b);
            var c21 = new Point2D(c.X, c.Y);

            var c20 = new Point2D(b1.X, b1.Y);

            var c10x2 = c10.X * c10.X;
            var c10y2 = c10.Y * c10.Y;
            var c11x2 = c11.X * c11.X;
            var c11y2 = c11.Y * c11.Y;
            var c12x2 = c12.X * c12.X;
            var c12y2 = c12.Y * c12.Y;
            var c20x2 = c20.X * c20.X;
            var c20y2 = c20.Y * c20.Y;
            var c21x2 = c21.X * c21.X;
            var c21y2 = c21.Y * c21.Y;
            var c22x2 = c22.X * c22.X;
            var c22y2 = c22.Y * c22.Y;
            var c23x2 = c23.X * c23.X;
            var c23y2 = c23.Y * c23.Y;

            var poly = new Polynomial(
                -2 * c10.X * c10.Y * c12.X * c12.Y - c10.X * c11.X * c11.Y * c12.Y - c10.Y * c11.X * c11.Y * c12.X + 2 * c10.X * c12.X * c20.Y * c12.Y + 2 * c10.Y * c20.X * c12.X * c12.Y + c11.X * c20.X * c11.Y * c12.Y + c11.X * c11.Y * c12.X * c20.Y - 2 * c20.X * c12.X * c20.Y * c12.Y - 2 * c10.X * c20.X * c12y2 + c10.X * c11y2 * c12.X + c10.Y * c11x2 * c12.Y - 2 * c10.Y * c12x2 * c20.Y - c20.X * c11y2 * c12.X - c11x2 * c20.Y * c12.Y + c10x2 * c12y2 + c10y2 * c12x2 + c20x2 * c12y2 + c12x2 * c20y2,
                2 * c10.X * c12.X * c12.Y * c21.Y + 2 * c10.Y * c12.X * c21.X * c12.Y + c11.X * c11.Y * c12.X * c21.Y + c11.X * c11.Y * c21.X * c12.Y - 2 * c20.X * c12.X * c12.Y * c21.Y - 2 * c12.X * c20.Y * c21.X * c12.Y - 2 * c10.X * c21.X * c12y2 - 2 * c10.Y * c12x2 * c21.Y + 2 * c20.X * c21.X * c12y2 - c11y2 * c12.X * c21.X - c11x2 * c12.Y * c21.Y + 2 * c12x2 * c20.Y * c21.Y,
                2 * c10.X * c12.X * c12.Y * c22.Y + 2 * c10.Y * c12.X * c12.Y * c22.X + c11.X * c11.Y * c12.X * c22.Y + c11.X * c11.Y * c12.Y * c22.X - 2 * c20.X * c12.X * c12.Y * c22.Y - 2 * c12.X * c20.Y * c12.Y * c22.X - 2 * c12.X * c21.X * c12.Y * c21.Y - 2 * c10.X * c12y2 * c22.X - 2 * c10.Y * c12x2 * c22.Y + 2 * c20.X * c12y2 * c22.X - c11y2 * c12.X * c22.X - c11x2 * c12.Y * c22.Y + c21x2 * c12y2 + c12x2 * (2 * c20.Y * c22.Y + c21y2),
                2 * c10.X * c12.X * c12.Y * c23.Y + 2 * c10.Y * c12.X * c12.Y * c23.X + c11.X * c11.Y * c12.X * c23.Y + c11.X * c11.Y * c12.Y * c23.X - 2 * c20.X * c12.X * c12.Y * c23.Y - 2 * c12.X * c20.Y * c12.Y * c23.X - 2 * c12.X * c21.X * c12.Y * c22.Y - 2 * c12.X * c12.Y * c21.Y * c22.X - 2 * c10.X * c12y2 * c23.X - 2 * c10.Y * c12x2 * c23.Y + 2 * c20.X * c12y2 * c23.X + 2 * c21.X * c12y2 * c22.X - c11y2 * c12.X * c23.X - c11x2 * c12.Y * c23.Y + c12x2 * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y),
                -2 * c12.X * c21.X * c12.Y * c23.Y - 2 * c12.X * c12.Y * c21.Y * c23.X - 2 * c12.X * c12.Y * c22.X * c22.Y + 2 * c21.X * c12y2 * c23.X + c12y2 * c22x2 + c12x2 * (2 * c21.Y * c23.Y + c22y2),
                -2 * c12.X * c12.Y * c22.X * c23.Y - 2 * c12.X * c12.Y * c22.Y * c23.X + 2 * c12y2 * c22.X * c23.X + 2 * c12x2 * c22.Y * c23.Y,
                -2 * c12.X * c12.Y * c23.X * c23.Y + c12x2 * c23y2 + c12y2 * c23x2
            );
            var roots = poly.RootsInInterval(0, 1);

            RemoveMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c10.X - c20.X - s * c21.X - s * s * c22.X - s * s * s * c23.X,
                    c11.X,
                    c12.X
                ).Roots();
                var yRoots = new Polynomial(
                    c10.Y - c20.Y - s * c21.Y - s * s * c22.Y - s * s * s * c23.Y,
                    c11.Y,
                    c12.Y
                ).Roots();

                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    var TOLERANCE = 1e-4;

                    checkRoots:
                    for (var j = 0; j < xRoots.Count; j++)
                    {
                        var xRoot = xRoots[j];

                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Count; k++)
                            {
                                if (Math.Abs(xRoot - yRoots[k]) < TOLERANCE)
                                {
                                    var v = (Point2D)c23.Scale(s * s * s).Add(c22.Scale(s * s).Add(c21.Scale(s).Add(c20)));
                                    result.Points.Add(new Point2D(v.X, v.Y));
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;

        }

        /**
         *  intersectQuadraticBezierEllipse
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} ec
         *  @param {Number} rx
         *  @param {Number} ry
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectQuadraticBezierEllipse(Point2D p1, Point2D p2, Point2D p3, Point2D ec, double rx, double ry)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            var a = p2.Scale(-2);
            var c2 = p1.Add(a.Add(p3));

            a = p1.Scale(-2);
            var b = p2.Scale(2);
            var c1 = (Point2D)a.Add(b);

            var c0 = new Point2D(p1.X, p1.Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;
            var roots = new Polynomial(
                ryry * (c0.X * c0.X + ec.X * ec.X) + rxrx * (c0.Y * c0.Y + ec.Y * ec.Y) - 2 * (ryry * ec.X * c0.X + rxrx * ec.Y * c0.Y) - rxrx * ryry,
                2 * (ryry * c1.X * (c0.X - ec.X) + rxrx * c1.Y * (c0.Y - ec.Y)),
                ryry * (2 * c2.X * c0.X + c1.X * c1.X) + rxrx * (2 * c2.Y * c0.Y + c1.Y * c1.Y) - 2 * (ryry * ec.X * c2.X + rxrx * ec.Y * c2.Y),
                2 * (ryry * c2.X * c1.X + rxrx * c2.Y * c1.Y),
                ryry * c2.X * c2.X + rxrx * c2.Y * c2.Y
            ).Roots();

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                    result.Points.Add(c2.Scale(t * t).Add(c1.Scale(t).Add(c0)));
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectQuadraticBezierLine
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectQuadraticBezierLineSegment(Point2D p1, Point2D p2, Point2D p3, Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new Intersection(IntersectionState.NoIntersection);

            var a = p2.Scale(-2);
            var c2 = p1.Add(a.Add(p3));

            a = p1.Scale(-2);
            var b = p2.Scale(2);
            var c1 = a.Add(b);

            var c0 = new Point2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // Transform cubic coefficients to line's coordinate system and find roots
            // of cubic
            var roots = new Polynomial(
                Primitives.DotProduct((Point2D)n, c0) + cl,
                Primitives.DotProduct((Point2D)n, c1),
                Primitives.DotProduct((Point2D)n, c2)
            ).Roots();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p4 = p1.Lerp(p2, t);
                    var p5 = p2.Lerp(p3, t);

                    var p6 = p4.Lerp(p5, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p6
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.AppendPoint(p6);
                        }
                    }
                    else if (min.X <= p6.X && p6.X <= max.X && min.Y <= p6.Y && p6.Y <= max.Y)
                    {
                        result.AppendPoint(p6);
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectQuadraticBezierLine
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectQuadraticBezierLine(Point2D p1, Point2D p2, Point2D p3, Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new Intersection(IntersectionState.NoIntersection);

            var a = p2.Scale(-2);
            var c2 = p1.Add(a.Add(p3));

            a = p1.Scale(-2);
            var b = p2.Scale(2);
            var c1 = a.Add(b);

            var c0 = new Point2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // Transform cubic coefficients to line's coordinate system and find roots
            // of cubic
            var roots = new Polynomial(
                Primitives.DotProduct((Point2D)n, c0) + cl,
                Primitives.DotProduct((Point2D)n, c1),
                Primitives.DotProduct((Point2D)n, c2)
            ).Roots();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p4 = p1.Lerp(p2, t);
                    var p5 = p2.Lerp(p3, t);

                    var p6 = p4.Lerp(p5, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p6
                    if (a1.X == a2.X)
                    {
                        //if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        //if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.AppendPoint(p6);
                        }
                    }
                    else// if (min.X <= p6.X && p6.X <= max.X && min.Y <= p6.Y && p6.Y <= max.Y)
                    {
                        result.AppendPoint(p6);
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectCubicBezierCubicBezier
         *
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @param {Point2D} a3
         *  @param {Point2D} a4
         *  @param {Point2D} b1
         *  @param {Point2D} b2
         *  @param {Point2D} b3
         *  @param {Point2D} b4
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectCubicBezierCubicBezier(Point2D a1, Point2D a2, Point2D a3, Point2D a4, Point2D b1, Point2D b2, Point2D b3, Point2D b4)
        {
            var result = new Intersection(IntersectionState.Intersection);

            // Calculate the coefficients of cubic polynomial
            var a = a1.Scale(-1);
            var b = a2.Scale(3);
            var c = a3.Scale(-3);
            var d = (Point2D)a.Add(b.Add(c.Add(a4)));
            var c13 = (Point2D)new Vector2D(d.X, d.Y);

            a = a1.Scale(3);
            b = a2.Scale(-6);
            c = a3.Scale(3);
            d = a.Add(b.Add(c));
            var c12 = (Point2D)new Vector2D(d.X, d.Y);

            a = a1.Scale(-3);
            b = a2.Scale(3);
            c = (Point2D)a.Add(b);
            var c11 = (Point2D)new Vector2D(c.X, c.Y);

            var c10 = (Point2D)new Vector2D(a1.X, a1.Y);

            a = b1.Scale(-1);
            b = b2.Scale(3);
            c = b3.Scale(-3);
            d = (Point2D)a.Add(b.Add(c.Add(b4)));
            var c23 = (Point2D)new Vector2D(d.X, d.Y);

            a = b1.Scale(3);
            b = b2.Scale(-6);
            c = b3.Scale(3);
            d = a.Add(b.Add(c));
            var c22 = (Point2D)new Vector2D(d.X, d.Y);

            a = b1.Scale(-3);
            b = b2.Scale(3);
            c = (Point2D)a.Add(b);
            var c21 = (Point2D)new Vector2D(c.X, c.Y);

            var c20 = (Point2D)new Vector2D(b1.X, b1.Y);

            var c10x2 = c10.X * c10.X;
            var c10x3 = c10.X * c10.X * c10.X;
            var c10y2 = c10.Y * c10.Y;
            var c10y3 = c10.Y * c10.Y * c10.Y;
            var c11x2 = c11.X * c11.X;
            var c11x3 = c11.X * c11.X * c11.X;
            var c11y2 = c11.Y * c11.Y;
            var c11y3 = c11.Y * c11.Y * c11.Y;
            var c12x2 = c12.X * c12.X;
            var c12x3 = c12.X * c12.X * c12.X;
            var c12y2 = c12.Y * c12.Y;
            var c12y3 = c12.Y * c12.Y * c12.Y;
            var c13x2 = c13.X * c13.X;
            var c13x3 = c13.X * c13.X * c13.X;
            var c13y2 = c13.Y * c13.Y;
            var c13y3 = c13.Y * c13.Y * c13.Y;
            var c20x2 = c20.X * c20.X;
            var c20x3 = c20.X * c20.X * c20.X;
            var c20y2 = c20.Y * c20.Y;
            var c20y3 = c20.Y * c20.Y * c20.Y;
            var c21x2 = c21.X * c21.X;
            var c21x3 = c21.X * c21.X * c21.X;
            var c21y2 = c21.Y * c21.Y;
            var c22x2 = c22.X * c22.X;
            var c22x3 = c22.X * c22.X * c22.X;
            var c22y2 = c22.Y * c22.Y;
            var c23x2 = c23.X * c23.X;
            var c23x3 = c23.X * c23.X * c23.X;
            var c23y2 = c23.Y * c23.Y;
            var c23y3 = c23.Y * c23.Y * c23.Y;
            var poly = new Polynomial(
                c10.X * c10.Y * c11.X * c12.Y * c13.X * c13.Y - c10.X * c10.Y * c11.Y * c12.X * c13.X * c13.Y + c10.X * c11.X * c11.Y * c12.X * c12.Y * c13.Y - c10.Y * c11.X * c11.Y * c12.X * c12.Y * c13.X - c10.X * c11.X * c20.Y * c12.Y * c13.X * c13.Y + 6 * c10.X * c20.X * c11.Y * c12.Y * c13.X * c13.Y + c10.X * c11.Y * c12.X * c20.Y * c13.X * c13.Y - c10.Y * c11.X * c20.X * c12.Y * c13.X * c13.Y - 6 * c10.Y * c11.X * c12.X * c20.Y * c13.X * c13.Y + c10.Y * c20.X * c11.Y * c12.X * c13.X * c13.Y - c11.X * c20.X * c11.Y * c12.X * c12.Y * c13.Y + c11.X * c11.Y * c12.X * c20.Y * c12.Y * c13.X + c11.X * c20.X * c20.Y * c12.Y * c13.X * c13.Y - c20.X * c11.Y * c12.X * c20.Y * c13.X * c13.Y - 2 * c10.X * c20.X * c12y3 * c13.X + 2 * c10.Y * c12x3 * c20.Y * c13.Y - 3 * c10.X * c10.Y * c11.X * c12.X * c13y2 - 6 * c10.X * c10.Y * c20.X * c13.X * c13y2 + 3 * c10.X * c10.Y * c11.Y * c12.Y * c13x2 - 2 * c10.X * c10.Y * c12.X * c12y2 * c13.X - 2 * c10.X * c11.X * c20.X * c12.Y * c13y2 - c10.X * c11.X * c11.Y * c12y2 * c13.X + 3 * c10.X * c11.X * c12.X * c20.Y * c13y2 - 4 * c10.X * c20.X * c11.Y * c12.X * c13y2 + 3 * c10.Y * c11.X * c20.X * c12.X * c13y2 + 6 * c10.X * c10.Y * c20.Y * c13x2 * c13.Y + 2 * c10.X * c10.Y * c12x2 * c12.Y * c13.Y + 2 * c10.X * c11.X * c11y2 * c13.X * c13.Y + 2 * c10.X * c20.X * c12.X * c12y2 * c13.Y + 6 * c10.X * c20.X * c20.Y * c13.X * c13y2 - 3 * c10.X * c11.Y * c20.Y * c12.Y * c13x2 + 2 * c10.X * c12.X * c20.Y * c12y2 * c13.X + c10.X * c11y2 * c12.X * c12.Y * c13.X + c10.Y * c11.X * c11.Y * c12x2 * c13.Y + 4 * c10.Y * c11.X * c20.Y * c12.Y * c13x2 - 3 * c10.Y * c20.X * c11.Y * c12.Y * c13x2 + 2 * c10.Y * c20.X * c12.X * c12y2 * c13.X + 2 * c10.Y * c11.Y * c12.X * c20.Y * c13x2 + c11.X * c20.X * c11.Y * c12y2 * c13.X - 3 * c11.X * c20.X * c12.X * c20.Y * c13y2 - 2 * c10.X * c12x2 * c20.Y * c12.Y * c13.Y - 6 * c10.Y * c20.X * c20.Y * c13x2 * c13.Y - 2 * c10.Y * c20.X * c12x2 * c12.Y * c13.Y - 2 * c10.Y * c11x2 * c11.Y * c13.X * c13.Y - c10.Y * c11x2 * c12.X * c12.Y * c13.Y - 2 * c10.Y * c12x2 * c20.Y * c12.Y * c13.X - 2 * c11.X * c20.X * c11y2 * c13.X * c13.Y - c11.X * c11.Y * c12x2 * c20.Y * c13.Y + 3 * c20.X * c11.Y * c20.Y * c12.Y * c13x2 - 2 * c20.X * c12.X * c20.Y * c12y2 * c13.X - c20.X * c11y2 * c12.X * c12.Y * c13.X + 3 * c10y2 * c11.X * c12.X * c13.X * c13.Y + 3 * c11.X * c12.X * c20y2 * c13.X * c13.Y + 2 * c20.X * c12x2 * c20.Y * c12.Y * c13.Y - 3 * c10x2 * c11.Y * c12.Y * c13.X * c13.Y + 2 * c11x2 * c11.Y * c20.Y * c13.X * c13.Y + c11x2 * c12.X * c20.Y * c12.Y * c13.Y - 3 * c20x2 * c11.Y * c12.Y * c13.X * c13.Y - c10x3 * c13y3 + c10y3 * c13x3 + c20x3 * c13y3 - c20y3 * c13x3 - 3 * c10.X * c20x2 * c13y3 - c10.X * c11y3 * c13x2 + 3 * c10x2 * c20.X * c13y3 + c10.Y * c11x3 * c13y2 + 3 * c10.Y * c20y2 * c13x3 + c20.X * c11y3 * c13x2 + c10x2 * c12y3 * c13.X - 3 * c10y2 * c20.Y * c13x3 - c10y2 * c12x3 * c13.Y + c20x2 * c12y3 * c13.X - c11x3 * c20.Y * c13y2 - c12x3 * c20y2 * c13.Y - c10.X * c11x2 * c11.Y * c13y2 + c10.Y * c11.X * c11y2 * c13x2 - 3 * c10.X * c10y2 * c13x2 * c13.Y - c10.X * c11y2 * c12x2 * c13.Y + c10.Y * c11x2 * c12y2 * c13.X - c11.X * c11y2 * c20.Y * c13x2 + 3 * c10x2 * c10.Y * c13.X * c13y2 + c10x2 * c11.X * c12.Y * c13y2 + 2 * c10x2 * c11.Y * c12.X * c13y2 - 2 * c10y2 * c11.X * c12.Y * c13x2 - c10y2 * c11.Y * c12.X * c13x2 + c11x2 * c20.X * c11.Y * c13y2 - 3 * c10.X * c20y2 * c13x2 * c13.Y + 3 * c10.Y * c20x2 * c13.X * c13y2 + c11.X * c20x2 * c12.Y * c13y2 - 2 * c11.X * c20y2 * c12.Y * c13x2 + c20.X * c11y2 * c12x2 * c13.Y - c11.Y * c12.X * c20y2 * c13x2 - c10x2 * c12.X * c12y2 * c13.Y - 3 * c10x2 * c20.Y * c13.X * c13y2 + 3 * c10y2 * c20.X * c13x2 * c13.Y + c10y2 * c12x2 * c12.Y * c13.X - c11x2 * c20.Y * c12y2 * c13.X + 2 * c20x2 * c11.Y * c12.X * c13y2 + 3 * c20.X * c20y2 * c13x2 * c13.Y - c20x2 * c12.X * c12y2 * c13.Y - 3 * c20x2 * c20.Y * c13.X * c13y2 + c12x2 * c20y2 * c12.Y * c13.X,
                -c10.X * c11.X * c12.Y * c13.X * c21.Y * c13.Y + c10.X * c11.Y * c12.X * c13.X * c21.Y * c13.Y + 6 * c10.X * c11.Y * c21.X * c12.Y * c13.X * c13.Y - 6 * c10.Y * c11.X * c12.X * c13.X * c21.Y * c13.Y - c10.Y * c11.X * c21.X * c12.Y * c13.X * c13.Y + c10.Y * c11.Y * c12.X * c21.X * c13.X * c13.Y - c11.X * c11.Y * c12.X * c21.X * c12.Y * c13.Y + c11.X * c11.Y * c12.X * c12.Y * c13.X * c21.Y + c11.X * c20.X * c12.Y * c13.X * c21.Y * c13.Y + 6 * c11.X * c12.X * c20.Y * c13.X * c21.Y * c13.Y + c11.X * c20.Y * c21.X * c12.Y * c13.X * c13.Y - c20.X * c11.Y * c12.X * c13.X * c21.Y * c13.Y - 6 * c20.X * c11.Y * c21.X * c12.Y * c13.X * c13.Y - c11.Y * c12.X * c20.Y * c21.X * c13.X * c13.Y - 6 * c10.X * c20.X * c21.X * c13y3 - 2 * c10.X * c21.X * c12y3 * c13.X + 6 * c10.Y * c20.Y * c13x3 * c21.Y + 2 * c20.X * c21.X * c12y3 * c13.X + 2 * c10.Y * c12x3 * c21.Y * c13.Y - 2 * c12x3 * c20.Y * c21.Y * c13.Y - 6 * c10.X * c10.Y * c21.X * c13.X * c13y2 + 3 * c10.X * c11.X * c12.X * c21.Y * c13y2 - 2 * c10.X * c11.X * c21.X * c12.Y * c13y2 - 4 * c10.X * c11.Y * c12.X * c21.X * c13y2 + 3 * c10.Y * c11.X * c12.X * c21.X * c13y2 + 6 * c10.X * c10.Y * c13x2 * c21.Y * c13.Y + 6 * c10.X * c20.X * c13.X * c21.Y * c13y2 - 3 * c10.X * c11.Y * c12.Y * c13x2 * c21.Y + 2 * c10.X * c12.X * c21.X * c12y2 * c13.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c21.Y + 6 * c10.X * c20.Y * c21.X * c13.X * c13y2 + 4 * c10.Y * c11.X * c12.Y * c13x2 * c21.Y + 6 * c10.Y * c20.X * c21.X * c13.X * c13y2 + 2 * c10.Y * c11.Y * c12.X * c13x2 * c21.Y - 3 * c10.Y * c11.Y * c21.X * c12.Y * c13x2 + 2 * c10.Y * c12.X * c21.X * c12y2 * c13.X - 3 * c11.X * c20.X * c12.X * c21.Y * c13y2 + 2 * c11.X * c20.X * c21.X * c12.Y * c13y2 + c11.X * c11.Y * c21.X * c12y2 * c13.X - 3 * c11.X * c12.X * c20.Y * c21.X * c13y2 + 4 * c20.X * c11.Y * c12.X * c21.X * c13y2 - 6 * c10.X * c20.Y * c13x2 * c21.Y * c13.Y - 2 * c10.X * c12x2 * c12.Y * c21.Y * c13.Y - 6 * c10.Y * c20.X * c13x2 * c21.Y * c13.Y - 6 * c10.Y * c20.Y * c21.X * c13x2 * c13.Y - 2 * c10.Y * c12x2 * c21.X * c12.Y * c13.Y - 2 * c10.Y * c12x2 * c12.Y * c13.X * c21.Y - c11.X * c11.Y * c12x2 * c21.Y * c13.Y - 4 * c11.X * c20.Y * c12.Y * c13x2 * c21.Y - 2 * c11.X * c11y2 * c21.X * c13.X * c13.Y + 3 * c20.X * c11.Y * c12.Y * c13x2 * c21.Y - 2 * c20.X * c12.X * c21.X * c12y2 * c13.Y - 2 * c20.X * c12.X * c12y2 * c13.X * c21.Y - 6 * c20.X * c20.Y * c21.X * c13.X * c13y2 - 2 * c11.Y * c12.X * c20.Y * c13x2 * c21.Y + 3 * c11.Y * c20.Y * c21.X * c12.Y * c13x2 - 2 * c12.X * c20.Y * c21.X * c12y2 * c13.X - c11y2 * c12.X * c21.X * c12.Y * c13.X + 6 * c20.X * c20.Y * c13x2 * c21.Y * c13.Y + 2 * c20.X * c12x2 * c12.Y * c21.Y * c13.Y + 2 * c11x2 * c11.Y * c13.X * c21.Y * c13.Y + c11x2 * c12.X * c12.Y * c21.Y * c13.Y + 2 * c12x2 * c20.Y * c21.X * c12.Y * c13.Y + 2 * c12x2 * c20.Y * c12.Y * c13.X * c21.Y + 3 * c10x2 * c21.X * c13y3 - 3 * c10y2 * c13x3 * c21.Y + 3 * c20x2 * c21.X * c13y3 + c11y3 * c21.X * c13x2 - c11x3 * c21.Y * c13y2 - 3 * c20y2 * c13x3 * c21.Y - c11.X * c11y2 * c13x2 * c21.Y + c11x2 * c11.Y * c21.X * c13y2 - 3 * c10x2 * c13.X * c21.Y * c13y2 + 3 * c10y2 * c21.X * c13x2 * c13.Y - c11x2 * c12y2 * c13.X * c21.Y + c11y2 * c12x2 * c21.X * c13.Y - 3 * c20x2 * c13.X * c21.Y * c13y2 + 3 * c20y2 * c21.X * c13x2 * c13.Y,
                -c10.X * c11.X * c12.Y * c13.X * c13.Y * c22.Y + c10.X * c11.Y * c12.X * c13.X * c13.Y * c22.Y + 6 * c10.X * c11.Y * c12.Y * c13.X * c22.X * c13.Y - 6 * c10.Y * c11.X * c12.X * c13.X * c13.Y * c22.Y - c10.Y * c11.X * c12.Y * c13.X * c22.X * c13.Y + c10.Y * c11.Y * c12.X * c13.X * c22.X * c13.Y + c11.X * c11.Y * c12.X * c12.Y * c13.X * c22.Y - c11.X * c11.Y * c12.X * c12.Y * c22.X * c13.Y + c11.X * c20.X * c12.Y * c13.X * c13.Y * c22.Y + c11.X * c20.Y * c12.Y * c13.X * c22.X * c13.Y + c11.X * c21.X * c12.Y * c13.X * c21.Y * c13.Y - c20.X * c11.Y * c12.X * c13.X * c13.Y * c22.Y - 6 * c20.X * c11.Y * c12.Y * c13.X * c22.X * c13.Y - c11.Y * c12.X * c20.Y * c13.X * c22.X * c13.Y - c11.Y * c12.X * c21.X * c13.X * c21.Y * c13.Y - 6 * c10.X * c20.X * c22.X * c13y3 - 2 * c10.X * c12y3 * c13.X * c22.X + 2 * c20.X * c12y3 * c13.X * c22.X + 2 * c10.Y * c12x3 * c13.Y * c22.Y - 6 * c10.X * c10.Y * c13.X * c22.X * c13y2 + 3 * c10.X * c11.X * c12.X * c13y2 * c22.Y - 2 * c10.X * c11.X * c12.Y * c22.X * c13y2 - 4 * c10.X * c11.Y * c12.X * c22.X * c13y2 + 3 * c10.Y * c11.X * c12.X * c22.X * c13y2 + 6 * c10.X * c10.Y * c13x2 * c13.Y * c22.Y + 6 * c10.X * c20.X * c13.X * c13y2 * c22.Y - 3 * c10.X * c11.Y * c12.Y * c13x2 * c22.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c22.Y + 2 * c10.X * c12.X * c12y2 * c22.X * c13.Y + 6 * c10.X * c20.Y * c13.X * c22.X * c13y2 + 6 * c10.X * c21.X * c13.X * c21.Y * c13y2 + 4 * c10.Y * c11.X * c12.Y * c13x2 * c22.Y + 6 * c10.Y * c20.X * c13.X * c22.X * c13y2 + 2 * c10.Y * c11.Y * c12.X * c13x2 * c22.Y - 3 * c10.Y * c11.Y * c12.Y * c13x2 * c22.X + 2 * c10.Y * c12.X * c12y2 * c13.X * c22.X - 3 * c11.X * c20.X * c12.X * c13y2 * c22.Y + 2 * c11.X * c20.X * c12.Y * c22.X * c13y2 + c11.X * c11.Y * c12y2 * c13.X * c22.X - 3 * c11.X * c12.X * c20.Y * c22.X * c13y2 - 3 * c11.X * c12.X * c21.X * c21.Y * c13y2 + 4 * c20.X * c11.Y * c12.X * c22.X * c13y2 - 2 * c10.X * c12x2 * c12.Y * c13.Y * c22.Y - 6 * c10.Y * c20.X * c13x2 * c13.Y * c22.Y - 6 * c10.Y * c20.Y * c13x2 * c22.X * c13.Y - 6 * c10.Y * c21.X * c13x2 * c21.Y * c13.Y - 2 * c10.Y * c12x2 * c12.Y * c13.X * c22.Y - 2 * c10.Y * c12x2 * c12.Y * c22.X * c13.Y - c11.X * c11.Y * c12x2 * c13.Y * c22.Y - 2 * c11.X * c11y2 * c13.X * c22.X * c13.Y + 3 * c20.X * c11.Y * c12.Y * c13x2 * c22.Y - 2 * c20.X * c12.X * c12y2 * c13.X * c22.Y - 2 * c20.X * c12.X * c12y2 * c22.X * c13.Y - 6 * c20.X * c20.Y * c13.X * c22.X * c13y2 - 6 * c20.X * c21.X * c13.X * c21.Y * c13y2 + 3 * c11.Y * c20.Y * c12.Y * c13x2 * c22.X + 3 * c11.Y * c21.X * c12.Y * c13x2 * c21.Y - 2 * c12.X * c20.Y * c12y2 * c13.X * c22.X - 2 * c12.X * c21.X * c12y2 * c13.X * c21.Y - c11y2 * c12.X * c12.Y * c13.X * c22.X + 2 * c20.X * c12x2 * c12.Y * c13.Y * c22.Y - 3 * c11.Y * c21x2 * c12.Y * c13.X * c13.Y + 6 * c20.Y * c21.X * c13x2 * c21.Y * c13.Y + 2 * c11x2 * c11.Y * c13.X * c13.Y * c22.Y + c11x2 * c12.X * c12.Y * c13.Y * c22.Y + 2 * c12x2 * c20.Y * c12.Y * c22.X * c13.Y + 2 * c12x2 * c21.X * c12.Y * c21.Y * c13.Y - 3 * c10.X * c21x2 * c13y3 + 3 * c20.X * c21x2 * c13y3 + 3 * c10x2 * c22.X * c13y3 - 3 * c10y2 * c13x3 * c22.Y + 3 * c20x2 * c22.X * c13y3 + c21x2 * c12y3 * c13.X + c11y3 * c13x2 * c22.X - c11x3 * c13y2 * c22.Y + 3 * c10.Y * c21x2 * c13.X * c13y2 - c11.X * c11y2 * c13x2 * c22.Y + c11.X * c21x2 * c12.Y * c13y2 + 2 * c11.Y * c12.X * c21x2 * c13y2 + c11x2 * c11.Y * c22.X * c13y2 - c12.X * c21x2 * c12y2 * c13.Y - 3 * c20.Y * c21x2 * c13.X * c13y2 - 3 * c10x2 * c13.X * c13y2 * c22.Y + 3 * c10y2 * c13x2 * c22.X * c13.Y - c11x2 * c12y2 * c13.X * c22.Y + c11y2 * c12x2 * c22.X * c13.Y - 3 * c20x2 * c13.X * c13y2 * c22.Y + 3 * c20y2 * c13x2 * c22.X * c13.Y + c12x2 * c12.Y * c13.X * (2 * c20.Y * c22.Y + c21y2) + c11.X * c12.X * c13.X * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c12x3 * c13.Y * (-2 * c20.Y * c22.Y - c21y2) + c10.Y * c13x3 * (6 * c20.Y * c22.Y + 3 * c21y2) + c11.Y * c12.X * c13x2 * (-2 * c20.Y * c22.Y - c21y2) + c11.X * c12.Y * c13x2 * (-4 * c20.Y * c22.Y - 2 * c21y2) + c10.X * c13x2 * c13.Y * (-6 * c20.Y * c22.Y - 3 * c21y2) + c20.X * c13x2 * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c13x3 * (-2 * c20.Y * c21y2 - c20y2 * c22.Y - c20.Y * (2 * c20.Y * c22.Y + c21y2)),
                -c10.X * c11.X * c12.Y * c13.X * c13.Y * c23.Y + c10.X * c11.Y * c12.X * c13.X * c13.Y * c23.Y + 6 * c10.X * c11.Y * c12.Y * c13.X * c13.Y * c23.X - 6 * c10.Y * c11.X * c12.X * c13.X * c13.Y * c23.Y - c10.Y * c11.X * c12.Y * c13.X * c13.Y * c23.X + c10.Y * c11.Y * c12.X * c13.X * c13.Y * c23.X + c11.X * c11.Y * c12.X * c12.Y * c13.X * c23.Y - c11.X * c11.Y * c12.X * c12.Y * c13.Y * c23.X + c11.X * c20.X * c12.Y * c13.X * c13.Y * c23.Y + c11.X * c20.Y * c12.Y * c13.X * c13.Y * c23.X + c11.X * c21.X * c12.Y * c13.X * c13.Y * c22.Y + c11.X * c12.Y * c13.X * c21.Y * c22.X * c13.Y - c20.X * c11.Y * c12.X * c13.X * c13.Y * c23.Y - 6 * c20.X * c11.Y * c12.Y * c13.X * c13.Y * c23.X - c11.Y * c12.X * c20.Y * c13.X * c13.Y * c23.X - c11.Y * c12.X * c21.X * c13.X * c13.Y * c22.Y - c11.Y * c12.X * c13.X * c21.Y * c22.X * c13.Y - 6 * c11.Y * c21.X * c12.Y * c13.X * c22.X * c13.Y - 6 * c10.X * c20.X * c13y3 * c23.X - 6 * c10.X * c21.X * c22.X * c13y3 - 2 * c10.X * c12y3 * c13.X * c23.X + 6 * c20.X * c21.X * c22.X * c13y3 + 2 * c20.X * c12y3 * c13.X * c23.X + 2 * c21.X * c12y3 * c13.X * c22.X + 2 * c10.Y * c12x3 * c13.Y * c23.Y - 6 * c10.X * c10.Y * c13.X * c13y2 * c23.X + 3 * c10.X * c11.X * c12.X * c13y2 * c23.Y - 2 * c10.X * c11.X * c12.Y * c13y2 * c23.X - 4 * c10.X * c11.Y * c12.X * c13y2 * c23.X + 3 * c10.Y * c11.X * c12.X * c13y2 * c23.X + 6 * c10.X * c10.Y * c13x2 * c13.Y * c23.Y + 6 * c10.X * c20.X * c13.X * c13y2 * c23.Y - 3 * c10.X * c11.Y * c12.Y * c13x2 * c23.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c23.Y + 2 * c10.X * c12.X * c12y2 * c13.Y * c23.X + 6 * c10.X * c20.Y * c13.X * c13y2 * c23.X + 6 * c10.X * c21.X * c13.X * c13y2 * c22.Y + 6 * c10.X * c13.X * c21.Y * c22.X * c13y2 + 4 * c10.Y * c11.X * c12.Y * c13x2 * c23.Y + 6 * c10.Y * c20.X * c13.X * c13y2 * c23.X + 2 * c10.Y * c11.Y * c12.X * c13x2 * c23.Y - 3 * c10.Y * c11.Y * c12.Y * c13x2 * c23.X + 2 * c10.Y * c12.X * c12y2 * c13.X * c23.X + 6 * c10.Y * c21.X * c13.X * c22.X * c13y2 - 3 * c11.X * c20.X * c12.X * c13y2 * c23.Y + 2 * c11.X * c20.X * c12.Y * c13y2 * c23.X + c11.X * c11.Y * c12y2 * c13.X * c23.X - 3 * c11.X * c12.X * c20.Y * c13y2 * c23.X - 3 * c11.X * c12.X * c21.X * c13y2 * c22.Y - 3 * c11.X * c12.X * c21.Y * c22.X * c13y2 + 2 * c11.X * c21.X * c12.Y * c22.X * c13y2 + 4 * c20.X * c11.Y * c12.X * c13y2 * c23.X + 4 * c11.Y * c12.X * c21.X * c22.X * c13y2 - 2 * c10.X * c12x2 * c12.Y * c13.Y * c23.Y - 6 * c10.Y * c20.X * c13x2 * c13.Y * c23.Y - 6 * c10.Y * c20.Y * c13x2 * c13.Y * c23.X - 6 * c10.Y * c21.X * c13x2 * c13.Y * c22.Y - 2 * c10.Y * c12x2 * c12.Y * c13.X * c23.Y - 2 * c10.Y * c12x2 * c12.Y * c13.Y * c23.X - 6 * c10.Y * c13x2 * c21.Y * c22.X * c13.Y - c11.X * c11.Y * c12x2 * c13.Y * c23.Y - 2 * c11.X * c11y2 * c13.X * c13.Y * c23.X + 3 * c20.X * c11.Y * c12.Y * c13x2 * c23.Y - 2 * c20.X * c12.X * c12y2 * c13.X * c23.Y - 2 * c20.X * c12.X * c12y2 * c13.Y * c23.X - 6 * c20.X * c20.Y * c13.X * c13y2 * c23.X - 6 * c20.X * c21.X * c13.X * c13y2 * c22.Y - 6 * c20.X * c13.X * c21.Y * c22.X * c13y2 + 3 * c11.Y * c20.Y * c12.Y * c13x2 * c23.X + 3 * c11.Y * c21.X * c12.Y * c13x2 * c22.Y + 3 * c11.Y * c12.Y * c13x2 * c21.Y * c22.X - 2 * c12.X * c20.Y * c12y2 * c13.X * c23.X - 2 * c12.X * c21.X * c12y2 * c13.X * c22.Y - 2 * c12.X * c21.X * c12y2 * c22.X * c13.Y - 2 * c12.X * c12y2 * c13.X * c21.Y * c22.X - 6 * c20.Y * c21.X * c13.X * c22.X * c13y2 - c11y2 * c12.X * c12.Y * c13.X * c23.X + 2 * c20.X * c12x2 * c12.Y * c13.Y * c23.Y + 6 * c20.Y * c13x2 * c21.Y * c22.X * c13.Y + 2 * c11x2 * c11.Y * c13.X * c13.Y * c23.Y + c11x2 * c12.X * c12.Y * c13.Y * c23.Y + 2 * c12x2 * c20.Y * c12.Y * c13.Y * c23.X + 2 * c12x2 * c21.X * c12.Y * c13.Y * c22.Y + 2 * c12x2 * c12.Y * c21.Y * c22.X * c13.Y + c21x3 * c13y3 + 3 * c10x2 * c13y3 * c23.X - 3 * c10y2 * c13x3 * c23.Y + 3 * c20x2 * c13y3 * c23.X + c11y3 * c13x2 * c23.X - c11x3 * c13y2 * c23.Y - c11.X * c11y2 * c13x2 * c23.Y + c11x2 * c11.Y * c13y2 * c23.X - 3 * c10x2 * c13.X * c13y2 * c23.Y + 3 * c10y2 * c13x2 * c13.Y * c23.X - c11x2 * c12y2 * c13.X * c23.Y + c11y2 * c12x2 * c13.Y * c23.X - 3 * c21x2 * c13.X * c21.Y * c13y2 - 3 * c20x2 * c13.X * c13y2 * c23.Y + 3 * c20y2 * c13x2 * c13.Y * c23.X + c11.X * c12.X * c13.X * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c12x3 * c13.Y * (-2 * c20.Y * c23.Y - 2 * c21.Y * c22.Y) + c10.Y * c13x3 * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c11.Y * c12.X * c13x2 * (-2 * c20.Y * c23.Y - 2 * c21.Y * c22.Y) + c12x2 * c12.Y * c13.X * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y) + c11.X * c12.Y * c13x2 * (-4 * c20.Y * c23.Y - 4 * c21.Y * c22.Y) + c10.X * c13x2 * c13.Y * (-6 * c20.Y * c23.Y - 6 * c21.Y * c22.Y) + c20.X * c13x2 * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c21.X * c13x2 * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c13x3 * (-2 * c20.Y * c21.Y * c22.Y - c20y2 * c23.Y - c21.Y * (2 * c20.Y * c22.Y + c21y2) - c20.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                c11.X * c21.X * c12.Y * c13.X * c13.Y * c23.Y + c11.X * c12.Y * c13.X * c21.Y * c13.Y * c23.X + c11.X * c12.Y * c13.X * c22.X * c13.Y * c22.Y - c11.Y * c12.X * c21.X * c13.X * c13.Y * c23.Y - c11.Y * c12.X * c13.X * c21.Y * c13.Y * c23.X - c11.Y * c12.X * c13.X * c22.X * c13.Y * c22.Y - 6 * c11.Y * c21.X * c12.Y * c13.X * c13.Y * c23.X - 6 * c10.X * c21.X * c13y3 * c23.X + 6 * c20.X * c21.X * c13y3 * c23.X + 2 * c21.X * c12y3 * c13.X * c23.X + 6 * c10.X * c21.X * c13.X * c13y2 * c23.Y + 6 * c10.X * c13.X * c21.Y * c13y2 * c23.X + 6 * c10.X * c13.X * c22.X * c13y2 * c22.Y + 6 * c10.Y * c21.X * c13.X * c13y2 * c23.X - 3 * c11.X * c12.X * c21.X * c13y2 * c23.Y - 3 * c11.X * c12.X * c21.Y * c13y2 * c23.X - 3 * c11.X * c12.X * c22.X * c13y2 * c22.Y + 2 * c11.X * c21.X * c12.Y * c13y2 * c23.X + 4 * c11.Y * c12.X * c21.X * c13y2 * c23.X - 6 * c10.Y * c21.X * c13x2 * c13.Y * c23.Y - 6 * c10.Y * c13x2 * c21.Y * c13.Y * c23.X - 6 * c10.Y * c13x2 * c22.X * c13.Y * c22.Y - 6 * c20.X * c21.X * c13.X * c13y2 * c23.Y - 6 * c20.X * c13.X * c21.Y * c13y2 * c23.X - 6 * c20.X * c13.X * c22.X * c13y2 * c22.Y + 3 * c11.Y * c21.X * c12.Y * c13x2 * c23.Y - 3 * c11.Y * c12.Y * c13.X * c22x2 * c13.Y + 3 * c11.Y * c12.Y * c13x2 * c21.Y * c23.X + 3 * c11.Y * c12.Y * c13x2 * c22.X * c22.Y - 2 * c12.X * c21.X * c12y2 * c13.X * c23.Y - 2 * c12.X * c21.X * c12y2 * c13.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c21.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c22.X * c22.Y - 6 * c20.Y * c21.X * c13.X * c13y2 * c23.X - 6 * c21.X * c13.X * c21.Y * c22.X * c13y2 + 6 * c20.Y * c13x2 * c21.Y * c13.Y * c23.X + 2 * c12x2 * c21.X * c12.Y * c13.Y * c23.Y + 2 * c12x2 * c12.Y * c21.Y * c13.Y * c23.X + 2 * c12x2 * c12.Y * c22.X * c13.Y * c22.Y - 3 * c10.X * c22x2 * c13y3 + 3 * c20.X * c22x2 * c13y3 + 3 * c21x2 * c22.X * c13y3 + c12y3 * c13.X * c22x2 + 3 * c10.Y * c13.X * c22x2 * c13y2 + c11.X * c12.Y * c22x2 * c13y2 + 2 * c11.Y * c12.X * c22x2 * c13y2 - c12.X * c12y2 * c22x2 * c13.Y - 3 * c20.Y * c13.X * c22x2 * c13y2 - 3 * c21x2 * c13.X * c13y2 * c22.Y + c12x2 * c12.Y * c13.X * (2 * c21.Y * c23.Y + c22y2) + c11.X * c12.X * c13.X * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) + c21.X * c13x2 * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c12x3 * c13.Y * (-2 * c21.Y * c23.Y - c22y2) + c10.Y * c13x3 * (6 * c21.Y * c23.Y + 3 * c22y2) + c11.Y * c12.X * c13x2 * (-2 * c21.Y * c23.Y - c22y2) + c11.X * c12.Y * c13x2 * (-4 * c21.Y * c23.Y - 2 * c22y2) + c10.X * c13x2 * c13.Y * (-6 * c21.Y * c23.Y - 3 * c22y2) + c13x2 * c22.X * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c20.X * c13x2 * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-2 * c20.Y * c21.Y * c23.Y - c22.Y * (2 * c20.Y * c22.Y + c21y2) - c20.Y * (2 * c21.Y * c23.Y + c22y2) - c21.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                6 * c11.X * c12.X * c13.X * c13.Y * c22.Y * c23.Y + c11.X * c12.Y * c13.X * c22.X * c13.Y * c23.Y + c11.X * c12.Y * c13.X * c13.Y * c22.Y * c23.X - c11.Y * c12.X * c13.X * c22.X * c13.Y * c23.Y - c11.Y * c12.X * c13.X * c13.Y * c22.Y * c23.X - 6 * c11.Y * c12.Y * c13.X * c22.X * c13.Y * c23.X - 6 * c10.X * c22.X * c13y3 * c23.X + 6 * c20.X * c22.X * c13y3 * c23.X + 6 * c10.Y * c13x3 * c22.Y * c23.Y + 2 * c12y3 * c13.X * c22.X * c23.X - 2 * c12x3 * c13.Y * c22.Y * c23.Y + 6 * c10.X * c13.X * c22.X * c13y2 * c23.Y + 6 * c10.X * c13.X * c13y2 * c22.Y * c23.X + 6 * c10.Y * c13.X * c22.X * c13y2 * c23.X - 3 * c11.X * c12.X * c22.X * c13y2 * c23.Y - 3 * c11.X * c12.X * c13y2 * c22.Y * c23.X + 2 * c11.X * c12.Y * c22.X * c13y2 * c23.X + 4 * c11.Y * c12.X * c22.X * c13y2 * c23.X - 6 * c10.X * c13x2 * c13.Y * c22.Y * c23.Y - 6 * c10.Y * c13x2 * c22.X * c13.Y * c23.Y - 6 * c10.Y * c13x2 * c13.Y * c22.Y * c23.X - 4 * c11.X * c12.Y * c13x2 * c22.Y * c23.Y - 6 * c20.X * c13.X * c22.X * c13y2 * c23.Y - 6 * c20.X * c13.X * c13y2 * c22.Y * c23.X - 2 * c11.Y * c12.X * c13x2 * c22.Y * c23.Y + 3 * c11.Y * c12.Y * c13x2 * c22.X * c23.Y + 3 * c11.Y * c12.Y * c13x2 * c22.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c22.X * c23.Y - 2 * c12.X * c12y2 * c13.X * c22.Y * c23.X - 2 * c12.X * c12y2 * c22.X * c13.Y * c23.X - 6 * c20.Y * c13.X * c22.X * c13y2 * c23.X - 6 * c21.X * c13.X * c21.Y * c13y2 * c23.X - 6 * c21.X * c13.X * c22.X * c13y2 * c22.Y + 6 * c20.X * c13x2 * c13.Y * c22.Y * c23.Y + 2 * c12x2 * c12.Y * c13.X * c22.Y * c23.Y + 2 * c12x2 * c12.Y * c22.X * c13.Y * c23.Y + 2 * c12x2 * c12.Y * c13.Y * c22.Y * c23.X + 3 * c21.X * c22x2 * c13y3 + 3 * c21x2 * c13y3 * c23.X - 3 * c13.X * c21.Y * c22x2 * c13y2 - 3 * c21x2 * c13.X * c13y2 * c23.Y + c13x2 * c22.X * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c13x2 * c13.Y * c23.X * (6 * c20.Y * c22.Y + 3 * c21y2) + c21.X * c13x2 * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-2 * c20.Y * c22.Y * c23.Y - c23.Y * (2 * c20.Y * c22.Y + c21y2) - c21.Y * (2 * c21.Y * c23.Y + c22y2) - c22.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                c11.X * c12.Y * c13.X * c13.Y * c23.X * c23.Y - c11.Y * c12.X * c13.X * c13.Y * c23.X * c23.Y + 6 * c21.X * c22.X * c13y3 * c23.X + 3 * c11.X * c12.X * c13.X * c13.Y * c23y2 + 6 * c10.X * c13.X * c13y2 * c23.X * c23.Y - 3 * c11.X * c12.X * c13y2 * c23.X * c23.Y - 3 * c11.Y * c12.Y * c13.X * c13.Y * c23x2 - 6 * c10.Y * c13x2 * c13.Y * c23.X * c23.Y - 6 * c20.X * c13.X * c13y2 * c23.X * c23.Y + 3 * c11.Y * c12.Y * c13x2 * c23.X * c23.Y - 2 * c12.X * c12y2 * c13.X * c23.X * c23.Y - 6 * c21.X * c13.X * c22.X * c13y2 * c23.Y - 6 * c21.X * c13.X * c13y2 * c22.Y * c23.X - 6 * c13.X * c21.Y * c22.X * c13y2 * c23.X + 6 * c21.X * c13x2 * c13.Y * c22.Y * c23.Y + 2 * c12x2 * c12.Y * c13.Y * c23.X * c23.Y + c22x3 * c13y3 - 3 * c10.X * c13y3 * c23x2 + 3 * c10.Y * c13x3 * c23y2 + 3 * c20.X * c13y3 * c23x2 + c12y3 * c13.X * c23x2 - c12x3 * c13.Y * c23y2 - 3 * c10.X * c13x2 * c13.Y * c23y2 + 3 * c10.Y * c13.X * c13y2 * c23x2 - 2 * c11.X * c12.Y * c13x2 * c23y2 + c11.X * c12.Y * c13y2 * c23x2 - c11.Y * c12.X * c13x2 * c23y2 + 2 * c11.Y * c12.X * c13y2 * c23x2 + 3 * c20.X * c13x2 * c13.Y * c23y2 - c12.X * c12y2 * c13.Y * c23x2 - 3 * c20.Y * c13.X * c13y2 * c23x2 + c12x2 * c12.Y * c13.X * c23y2 - 3 * c13.X * c22x2 * c13y2 * c22.Y + c13x2 * c13.Y * c23.X * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c13x2 * c22.X * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-2 * c21.Y * c22.Y * c23.Y - c20.Y * c23y2 - c22.Y * (2 * c21.Y * c23.Y + c22y2) - c23.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                -6 * c21.X * c13.X * c13y2 * c23.X * c23.Y - 6 * c13.X * c22.X * c13y2 * c22.Y * c23.X + 6 * c13x2 * c22.X * c13.Y * c22.Y * c23.Y + 3 * c21.X * c13y3 * c23x2 + 3 * c22x2 * c13y3 * c23.X + 3 * c21.X * c13x2 * c13.Y * c23y2 - 3 * c13.X * c21.Y * c13y2 * c23x2 - 3 * c13.X * c22x2 * c13y2 * c23.Y + c13x2 * c13.Y * c23.X * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-c21.Y * c23y2 - 2 * c22y2 * c23.Y - c23.Y * (2 * c21.Y * c23.Y + c22y2)),
                -6 * c13.X * c22.X * c13y2 * c23.X * c23.Y + 6 * c13x2 * c13.Y * c22.Y * c23.X * c23.Y + 3 * c22.X * c13y3 * c23x2 - 3 * c13x3 * c22.Y * c23y2 - 3 * c13.X * c13y2 * c22.Y * c23x2 + 3 * c13x2 * c22.X * c13.Y * c23y2,
                -c13x3 * c23y3 + c13y3 * c23x3 - 3 * c13.X * c13y2 * c23x2 * c23.Y + 3 * c13x2 * c13.Y * c23.X * c23y2
            );
            var roots = poly.RootsInInterval(0, 1);
            RemoveMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c10.X - c20.X - s * c21.X - s * s * c22.X - s * s * s * c23.X,
                    c11.X,
                    c12.X,
                    c13.X
                ).Roots();
                var yRoots = new Polynomial(
                    c10.Y - c20.Y - s * c21.Y - s * s * c22.Y - s * s * s * c23.Y,
                    c11.Y,
                    c12.Y,
                    c13.Y
                ).Roots();

                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    var TOLERANCE = 1e-4;

                    checkRoots:
                    for (var j = 0; j < xRoots.Count; j++)
                    {
                        var xRoot = xRoots[j];

                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Count; k++)
                            {
                                if (Math.Abs(xRoot - yRoots[k]) < TOLERANCE)
                                {
                                    var v = (Point2D)c23.Scale(s * s * s).Add(c22.Scale(s * s).Add(c21.Scale(s).Add(c20)));
                                    result.Points.Add(new Point2D(v.X, v.Y));
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectCubicBezierEllipse
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} p4
         *  @param {Point2D} ec
         *  @param {Number} rx
         *  @param {Number} ry
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectCubicBezierEllipse(Point2D p1, Point2D p2, Point2D p3, Point2D p4, Point2D ec, double rx, double ry)
        {
            var result = new Intersection(IntersectionState.Intersection);

            // Calculate the coefficients of cubic polynomial
            var a = p1.Scale(-1);
            var b = p2.Scale(3);
            var c = p3.Scale(-3);
            var d = (Point2D)a.Add(b.Add(c.Add(p4)));
            var c3 = (Point2D)new Vector2D(d.X, d.Y);

            a = p1.Scale(3);
            b = p2.Scale(-6);
            c = p3.Scale(3);
            d = a.Add(b.Add(c));
            var c2 = (Point2D)new Vector2D(d.X, d.Y);

            a = p1.Scale(-3);
            b = p2.Scale(3);
            c = (Point2D)a.Add(b);
            var c1 = (Point2D)new Vector2D(c.X, c.Y);

            var c0 = (Point2D)new Vector2D(p1.X, p1.Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;
            var poly = new Polynomial(
                c0.X * c0.X * ryry - 2 * c0.Y * ec.Y * rxrx - 2 * c0.X * ec.X * ryry + c0.Y * c0.Y * rxrx + ec.X * ec.X * ryry + ec.Y * ec.Y * rxrx - rxrx * ryry,
                2 * c1.X * ryry * (c0.X - ec.X) + 2 * c1.Y * rxrx * (c0.Y - ec.Y),
                2 * c2.X * ryry * (c0.X - ec.X) + 2 * c2.Y * rxrx * (c0.Y - ec.Y) + c1.X * c1.X * ryry + c1.Y * c1.Y * rxrx,
                2 * c3.X * ryry * (c0.X - ec.X) + 2 * c3.Y * rxrx * (c0.Y - ec.Y) + 2 * (c2.X * c1.X * ryry + c2.Y * c1.Y * rxrx),
                2 * (c3.X * c1.X * ryry + c3.Y * c1.Y * rxrx) + c2.X * c2.X * ryry + c2.Y * c2.Y * rxrx,
                2 * (c3.X * c2.X * ryry + c3.Y * c2.Y * rxrx),
                c3.X * c3.X * ryry + c3.Y * c3.Y * rxrx
            );
            var roots = poly.RootsInInterval(0, 1);
            RemoveMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                var v = (Point2D)c3.Scale(t * t * t).Add(c2.Scale(t * t).Add(c1.Scale(t).Add(c0)));
                result.Points.Add(new Point2D(v.X, v.Y));
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectCubicBezierLine
         *
         *  Many thanks to Dan Sunday at SoftSurfer.com.  He gave me a very thorough
         *  sketch of the algorithm used here.  Without his help, I'm not sure when I
         *  would have figured out this intersection problem.
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} p4
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectCubicBezierLineSegment(Point2D p1, Point2D p2, Point2D p3, Point2D p4, Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new Intersection(IntersectionState.NoIntersection);

            // Start with Bezier using Bernstein polynomials for weighting functions:
            //     (1-t^3)P1 + 3t(1-t)^2P2 + 3t^2(1-t)P3 + t^3P4
            //
            // Expand and collect terms to form linear combinations of original Bezier
            // controls.  This ends up with a vector cubic in t:
            //     (-P1+3P2-3P3+P4)t^3 + (3P1-6P2+3P3)t^2 + (-3P1+3P2)t + P1
            //             /\                  /\                /\       /\
            //             ||                  ||                ||       ||
            //             c3                  c2                c1       c0

            // Calculate the coefficients
            var a = p1.Scale(-1);
            var b = p2.Scale(3);
            var c = p3.Scale(-3);
            var d = (Point2D)a.Add(b.Add(c.Add(p4)));
            var c3 = new Vector2D(d.X, d.Y);

            a = p1.Scale(3);
            b = p2.Scale(-6);
            c = p3.Scale(3);
            d = a.Add(b.Add(c));
            var c2 = new Vector2D(d.X, d.Y);

            a = p1.Scale(-3);
            b = p2.Scale(3);
            c = (Point2D)a.Add(b);
            var c1 = new Vector2D(c.X, c.Y);

            var c0 = new Vector2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // ?Rotate each cubic coefficient using line for new coordinate system?
            // Find roots of rotated cubic
            var roots = new Polynomial(
                Primitives.DotProduct(n, c0) + cl,
                Primitives.DotProduct(n, c1),
                Primitives.DotProduct(n, c2),
                Primitives.DotProduct(n, c3)
            ).Roots();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                if (t >= 0 && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p5 = p1.Lerp(p2, t);
                    var p6 = p2.Lerp(p3, t);
                    var p7 = p3.Lerp(p4, t);

                    var p8 = p5.Lerp(p6, t);
                    var p9 = p6.Lerp(p7, t);

                    var p10 = p8.Lerp(p9, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p10
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.AppendPoint(p10);
                        }
                    }
                    else if (min.X <= p10.X && p10.X <= max.X && min.Y <= p10.Y && p10.Y <= max.Y)
                    {
                        result.AppendPoint(p10);
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /**
         *  intersectCubicBezierLine
         *
         *  Many thanks to Dan Sunday at SoftSurfer.com.  He gave me a very thorough
         *  sketch of the algorithm used here.  Without his help, I'm not sure when I
         *  would have figured out this intersection problem.
         *
         *  @param {Point2D} p1
         *  @param {Point2D} p2
         *  @param {Point2D} p3
         *  @param {Point2D} p4
         *  @param {Point2D} a1
         *  @param {Point2D} a2
         *  @returns {Intersection}
         */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection IntersectCubicBezierLine(Point2D p1, Point2D p2, Point2D p3, Point2D p4, Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new Intersection(IntersectionState.NoIntersection);

            // Start with Bezier using Bernstein polynomials for weighting functions:
            //     (1-t^3)P1 + 3t(1-t)^2P2 + 3t^2(1-t)P3 + t^3P4
            //
            // Expand and collect terms to form linear combinations of original Bezier
            // controls.  This ends up with a vector cubic in t:
            //     (-P1+3P2-3P3+P4)t^3 + (3P1-6P2+3P3)t^2 + (-3P1+3P2)t + P1
            //             /\                  /\                /\       /\
            //             ||                  ||                ||       ||
            //             c3                  c2                c1       c0

            // Calculate the coefficients
            var a = p1.Scale(-1);
            var b = p2.Scale(3);
            var c = p3.Scale(-3);
            var d = (Point2D)a.Add(b.Add(c.Add(p4)));
            var c3 = new Vector2D(d.X, d.Y);

            a = p1.Scale(3);
            b = p2.Scale(-6);
            c = p3.Scale(3);
            d = a.Add(b.Add(c));
            var c2 = new Vector2D(d.X, d.Y);

            a = p1.Scale(-3);
            b = p2.Scale(3);
            c = (Point2D)a.Add(b);
            var c1 = new Vector2D(c.X, c.Y);

            var c0 = new Vector2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // ?Rotate each cubic coefficient using line for new coordinate system?
            // Find roots of rotated cubic
            var roots = new Polynomial(
                Primitives.DotProduct(n, c0) + cl,
                Primitives.DotProduct(n, c1),
                Primitives.DotProduct(n, c2),
                Primitives.DotProduct(n, c3)
            ).Roots();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                if (t >= 0 && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p5 = p1.Lerp(p2, t);
                    var p6 = p2.Lerp(p3, t);
                    var p7 = p3.Lerp(p4, t);

                    var p8 = p5.Lerp(p6, t);
                    var p9 = p6.Lerp(p7, t);

                    var p10 = p8.Lerp(p9, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p10
                    if (a1.X == a2.X)
                    {
                        //if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        //if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.AppendPoint(p10);
                        }
                    }
                    else// if (min.X <= p10.X && p10.X <= max.X && min.Y <= p10.Y && p10.Y <= max.Y)
                    {
                        result.AppendPoint(p10);
                    }
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        /// <remarks>http://www.kevlindev.com/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Bezout(double[] e1, double[] e2)
        {
            var AB = e1[0] * e2[1] - e2[0] * e1[1];
            var AC = e1[0] * e2[2] - e2[0] * e1[2];
            var AD = e1[0] * e2[3] - e2[0] * e1[3];
            var AE = e1[0] * e2[4] - e2[0] * e1[4];
            var AF = e1[0] * e2[5] - e2[0] * e1[5];
            var BC = e1[1] * e2[2] - e2[1] * e1[2];
            var BE = e1[1] * e2[4] - e2[1] * e1[4];
            var BF = e1[1] * e2[5] - e2[1] * e1[5];
            var CD = e1[2] * e2[3] - e2[2] * e1[3];
            var DE = e1[3] * e2[4] - e2[3] * e1[4];
            var DF = e1[3] * e2[5] - e2[3] * e1[5];
            var BFpDE = BF + DE;
            var BEmCD = BE - CD;
            return new Polynomial(
                AD * DF - AF * AF,
                AB * DF + AD * BFpDE - 2 * AE * AF,
                AB * BFpDE + AD * BEmCD - AE * AE - 2 * AC * AF,
                AB * BEmCD + AD * BC - 2 * AC * AE,
                AB * BC - AC * AC);
        }

        /// <summary>
        /// intended for removal of multiple "identical" root copies, when roots are in interval [0,1]
        /// </summary>
        /// <param name="roots">will be modified, almost identical root copies will be removed</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveMultipleRootsIn01(List<double> roots)
        {
            var ZEROepsilon = 1e-15;
            roots.Sort((a, b) => a.CompareTo(b));
            for (var i = 1; i < roots.Count;)
            {
                if (Math.Abs(roots[i] - roots[i - 1]) < ZEROepsilon)
                {
                    roots.Splice(i, 1);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
