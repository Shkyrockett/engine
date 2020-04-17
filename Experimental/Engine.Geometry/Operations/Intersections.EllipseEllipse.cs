// <copyright file="Intersections.EllipseEllipse.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        /// <summary>
        /// Find the intersection between a cubic Bézier and an rotated ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="cos">The cos a.</param>
        /// <param name="sin">The sin a.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Intersection CubicBezierSegmentObliqueEllipseIntersectionPlayground(
            Polynomial xCurve, Polynomial yCurve,
            double h, double k, double a, double b, double cos, double sin,
            double epsilon = double.Epsilon)
        {
            // This is broken.
            if (sin == 1d || sin == -1d) cos = 0d;

            _ = epsilon;

            // The Cubic Bezier 
            (var ax, var bx, var cx, var dx) = (xCurve[0], xCurve[1], xCurve[2], xCurve[3]);
            (var ay, var by, var cy, var dy) = (yCurve[0], yCurve[1], yCurve[2], yCurve[3]);

            // ToDo: Need to figure out why the rotation transform is not working as expected.
            // The equation of a no-rotated transformed ellipse is: $\frac{(x-h)^2}{a^2}+\frac{(y-k)^2}{b^2}=1$
            // and the equation of a rotated ellipse is: $\frac{((x-h)\cos{t}+(y-k)\sin{t})^2}{a^2}+\frac{((x-h)\sin{t}-(y-k)\cos{t})^2}{b^2}=1$
            // So, I thought that switching out (x-h) and (y-k) for the rotated versions should work. But it only almost works.
            // The results seem to indicate that the rotation is possibly occurring about the d point of the curve?
            // What am I missing?

            // Apply the rotation transform to the ellipse.
            var dxh = (/*cos * cos **/ (dx - h) /*- sin * sin * (dy - k)*/);
            var dyk = (/*cos * cos **/ (dy - k)  /*- sin *sin * (dx - h)*/);
            var aa = a * a;
            var bb = b * b;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial((
                a: (ax * ax * bb) + (ay * ay * aa),
                b: (2d * (ax * bx * bb)) + (2d * (ay * by * aa)),
                c: (((2d * (ax * cx)) + (bx * bx)) * bb) + (((2d * (ay * cy)) + (by * by)) * aa),
                d: (2d * ((ax * dxh) + (bx * cx)) * bb) + (2d * ((ay * dyk) + (by * cy)) * aa),
                e: (((2d * (bx * dxh)) + (cx * cx)) * bb) + (((2d * (by * dyk)) + (cy * cy)) * aa),
                f: (2d * cx * bb * dxh) + (2d * cy * aa * dyk),
                g: (bb * (dxh * dxh)) + (aa * (dyk * dyk)) - (aa * bb)
                )).Trim().RootsInInterval();

            Array.Sort(roots);

            //var roots = new Polynomial((
            //    a: 2 * (ax - 3 * bx + 3 * cx - dx) * b * b * cos * cos * h + 2 * a * a * (ay - 3 * by + 3 * cy - dy) * cos * cos * k - 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * (ay - 3 * by + 3 * cy - dy) * cos * sin + 2 * (ax - 3 * bx + 3 * cx - dx) * (ay - 3 * by + 3 * cy - dy) * b * b * cos * sin - 2 * a * a * (ay - 3 * by + 3 * cy - dy) * cos * h * sin + 2 * (ay - 3 * by + 3 * cy - dy) * b * b * cos * h * sin - 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * cos * k * sin + 2 * (ax - 3 * bx + 3 * cx - dx) * b * b * cos * k * sin + 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * h * sin * sin + 2 * (ay - 3 * by + 3 * cy - dy) * b * b * k * sin * sin,
            //    b: -6 * (ax - 2 * bx + cx) * b * b * cos * cos * h - 6 * a * a * (ay - 2 * by + cy) * cos * cos * k - 18 * a * a * (ax - 2 * bx + cx) * (ay - 2 * by + cy) * cos * sin + 18 * (ax - 2 * bx + cx) * (ay - 2 * by + cy) * b * b * cos * sin + 6 * a * a * (ay - 2 * by + cy) * cos * h * sin - 6 * (ay - 2 * by + cy) * b * b * cos * h * sin + 6 * a * a * (ax - 2 * bx + cx) * cos * k * sin - 6 * (ax - 2 * bx + cx) * b * b * cos * k * sin - 6 * a * a * (ax - 2 * bx + cx) * h * sin * sin - 6 * (ay - 2 * by + cy) * b * b * k * sin * sin,
            //    c: 6 * (ax - bx) * b * b * cos * cos * h + 6 * a * a * (ay - by) * cos * cos * k - 18 * a * a * (ax - bx) * (ay - by) * cos * sin + 18 * (ax - bx) * (ay - by) * b * b * cos * sin - 6 * a * a * (ay - by) * cos * h * sin + 6 * (ay - by) * b * b * cos * h * sin - 6 * a * a * (ax - bx) * cos * k * sin + 6 * (ax - bx) * b * b * cos * k * sin + 6 * a * a * (ax - bx) * h * sin * sin + 6 * (ay - by) * b * b * k * sin * sin,
            //    d: a * a * ay * ay * cos * cos + ax * ax * b * b * cos * cos - 6 * ax * b * b * bx * cos * cos + 9 * b * b * bx * bx * cos * cos - 6 * a * a * ay * by * cos * cos + 9 * a * a * by * by * cos * cos + 6 * ax * b * b * cx * cos * cos - 18 * b * b * bx * cx * cos * cos + 9 * b * b * cx * cx * cos * cos + 6 * a * a * ay * cy * cos * cos - 18 * a * a * by * cy * cos * cos + 9 * a * a * cy * cy * cos * cos - 2 * ax * b * b * cos * cos * dx + 6 * b * b * bx * cos * cos * dx - 6 * b * b * cx * cos * cos * dx + b * b * cos * cos * dx * dx - 2 * a * a * ay * cos * cos * dy + 6 * a * a * by * cos * cos * dy - 6 * a * a * cy * cos * cos * dy + a * a * cos * cos * dy * dy + ay * ay * b * b * sin * sin - 6 * ay * b * b * by * sin * sin + 9 * b * b * by * by * sin * sin + 6 * ay * b * b * cy * sin * sin - 18 * b * b * by * cy * sin * sin + 9 * b * b * cy * cy * sin * sin - 2 * ay * b * b * dy * sin * sin + 6 * b * b * by * dy * sin * sin - 6 * b * b * cy * dy * sin * sin + b * b * dy * dy * sin * sin + a * a * ax * ax * sin - 6 * a * a * ax * bx * sin + 9 * a * a * bx * bx * sin + 6 * a * a * ax * cx * sin - 18 * a * a * bx * cx * sin + 9 * a * a * cx * cx * sin - 2 * a * a * ax * dx * sin + 6 * a * a * bx * dx * sin - 6 * a * a * cx * dx * sin + a * a * dx * dx * sin,
            //    e: -6 * a * a * ay * ay * cos * cos - 6 * ax * ax * b * b * cos * cos + 30 * ax * b * b * bx * cos * cos - 36 * b * b * bx * bx * cos * cos + 30 * a * a * ay * by * cos * cos - 36 * a * a * by * by * cos * cos - 24 * ax * b * b * cx * cos * cos + 54 * b * b * bx * cx * cos * cos - 18 * b * b * cx * cx * cos * cos - 24 * a * a * ay * cy * cos * cos + 54 * a * a * by * cy * cos * cos - 18 * a * a * cy * cy * cos * cos + 6 * ax * b * b * cos * cos * dx - 12 * b * b * bx * cos * cos * dx + 6 * b * b * cx * cos * cos * dx + 6 * a * a * ay * cos * cos * dy - 12 * a * a * by * cos * cos * dy + 6 * a * a * cy * cos * cos * dy - 6 * ay * ay * b * b * sin * sin + 30 * ay * b * b * by * sin * sin - 36 * b * b * by * by * sin * sin - 24 * ay * b * b * cy * sin * sin + 54 * b * b * by * cy * sin * sin - 18 * b * b * cy * cy * sin * sin + 6 * ay * b * b * dy * sin * sin - 12 * b * b * by * dy * sin * sin + 6 * b * b * cy * dy * sin * sin - 6 * a * a * ax * ax * sin + 30 * a * a * ax * bx * sin - 36 * a * a * bx * bx * sin - 24 * a * a * ax * cx * sin + 54 * a * a * bx * cx * sin - 18 * a * a * cx * cx * sin + 6 * a * a * ax * dx * sin - 12 * a * a * bx * dx * sin + 6 * a * a * cx * dx * sin,
            //    f: 15 * a * a * ay * ay * cos * cos + 15 * ax * ax * b * b * cos * cos - 60 * ax * b * b * bx * cos * cos + 54 * b * b * bx * bx * cos * cos - 60 * a * a * ay * by * cos * cos + 54 * a * a * by * by * cos * cos + 36 * ax * b * b * cx * cos * cos - 54 * b * b * bx * cx * cos * cos + 9 * b * b * cx * cx * cos * cos + 36 * a * a * ay * cy * cos * cos - 54 * a * a * by * cy * cos * cos + 9 * a * a * cy * cy * cos * cos - 6 * ax * b * b * cos * cos * dx + 6 * b * b * bx * cos * cos * dx - 6 * a * a * ay * cos * cos * dy + 6 * a * a * by * cos * cos * dy + 15 * ay * ay * b * b * sin * sin - 60 * ay * b * b * by * sin * sin + 54 * b * b * by * by * sin * sin + 36 * ay * b * b * cy * sin * sin - 54 * b * b * by * cy * sin * sin + 9 * b * b * cy * cy * sin * sin - 6 * ay * b * b * dy * sin * sin + 6 * b * b * by * dy * sin * sin + 15 * a * a * ax * ax * sin - 60 * a * a * ax * bx * sin + 54 * a * a * bx * bx * sin + 36 * a * a * ax * cx * sin - 54 * a * a * bx * cx * sin + 9 * a * a * cx * cx * sin - 6 * a * a * ax * dx * sin + 6 * a * a * bx * dx * sin,
            //    g: -20 * a * a * ay * ay * cos * cos - 20 * ax * ax * b * b * cos * cos + 60 * ax * b * b * bx * cos * cos - 36 * b * b * bx * bx * cos * cos + 60 * a * a * ay * by * cos * cos - 36 * a * a * by * by * cos * cos - 24 * ax * b * b * cx * cos * cos + 18 * b * b * bx * cx * cos * cos - 24 * a * a * ay * cy * cos * cos + 18 * a * a * by * cy * cos * cos + 2 * ax * b * b * cos * cos * dx + 2 * a * a * ay * cos * cos * dy - 20 * ay * ay * b * b * sin * sin + 60 * ay * b * b * by * sin * sin - 36 * b * b * by * by * sin * sin - 24 * ay * b * b * cy * sin * sin + 18 * b * b * by * cy * sin * sin + 2 * ay * b * b * dy * sin * sin - 20 * a * a * ax * ax * sin + 60 * a * a * ax * bx * sin - 36 * a * a * bx * bx * sin - 24 * a * a * ax * cx * sin + 18 * a * a * bx * cx * sin + 2 * a * a * ax * dx * sin,
            //    h: 15 * a * a * ay * ay * cos * cos + 15 * ax * ax * b * b * cos * cos - 30 * ax * b * b * bx * cos * cos + 9 * b * b * bx * bx * cos * cos - 30 * a * a * ay * by * cos * cos + 9 * a * a * by * by * cos * cos + 6 * ax * b * b * cx * cos * cos + 6 * a * a * ay * cy * cos * cos + 15 * ay * ay * b * b * sin * sin - 30 * ay * b * b * by * sin * sin + 9 * b * b * by * by * sin * sin + 6 * ay * b * b * cy * sin * sin + 15 * a * a * ax * ax * sin - 30 * a * a * ax * bx * sin + 9 * a * a * bx * bx * sin + 6 * a * a * ax * cx * sin,
            //    i: -6 * a * a * ay * ay * cos * cos - 6 * ax * ax * b * b * cos * cos + 6 * ax * b * b * bx * cos * cos + 6 * a * a * ay * by * cos * cos - 6 * ay * ay * b * b * sin * sin + 6 * ay * b * b * by * sin * sin - 6 * a * a * ax * ax * sin + 6 * a * a * ax * bx * sin,
            //    j: a * a * ay * ay * cos * cos + ax * ax * b * b * cos * cos - 2 * ax * b * b * cos * cos * h + b * b * cos * cos * h * h - 2 * a * a * ay * cos * cos * k + a * a * cos * cos * k * k - 2 * a * a * ax * ay * cos * sin + 2 * ax * ay * b * b * cos * sin + 2 * a * a * ay * cos * h * sin - 2 * ay * b * b * cos * h * sin + 2 * a * a * ax * cos * k * sin - 2 * ax * b * b * cos * k * sin - 2 * a * a * cos * h * k * sin + 2 * b * b * cos * h * k * sin + ay * ay * b * b * sin * sin - 2 * a * a * ax * h * sin * sin + a * a * h * h * sin * sin - 2 * ay * b * b * k * sin * sin + b * b * k * k * sin * sin + a * a * ax * ax * sin - a * a * b * b
            //    )).Trim().RootsInInterval();

            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);
            foreach (var s in roots)
            {
                var point = (
                    X: (ax * s * s * s) + (bx * s * s) + (cx * s) + dx,
                    Y: (ay * s * s * s) + (by * s * s) + (cy * s) + dy);

                result.Points.Add(point);
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Finds the intersection between two <see cref="Ellipse2D"/>s.
        /// </summary>
        /// <param name="cx0">The <paramref name="cx0"/>.</param>
        /// <param name="cy0">The <paramref name="cy0"/>.</param>
        /// <param name="rx0">The <paramref name="rx0"/>.</param>
        /// <param name="ry0">The <paramref name="ry0"/>.</param>
        /// <param name="angle0">The <paramref name="angle0"/>.</param>
        /// <param name="cx1">The <paramref name="cx1"/>.</param>
        /// <param name="cy1">The <paramref name="cy1"/>.</param>
        /// <param name="rx1">The <paramref name="rx1"/>.</param>
        /// <param name="ry1">The <paramref name="ry1"/>.</param>
        /// <param name="angle1">The <paramref name="angle1"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Intersection EllipseEllipseIntersection(double cx0, double cy0, double rx0, double ry0, double angle0, double cx1, double cy1, double rx1, double ry1, double angle1, double epsilon = Epsilon) => EllipseEllipseIntersection(cx0, cy0, rx0, ry0, Cos(angle0), Sin(angle0), cx1, cy1, rx1, ry1, Cos(angle1), Sin(angle1), epsilon);

        /// <summary>
        /// Finds the intersection between two <see cref="Ellipse2D"/>s.
        /// </summary>
        /// <param name="h0">The <paramref name="h0"/>.</param>
        /// <param name="k0">The <paramref name="k0"/>.</param>
        /// <param name="a0">The <paramref name="a0"/>.</param>
        /// <param name="b0">The <paramref name="b0"/>.</param>
        /// <param name="cosA0">The <paramref name="cosA0"/>.</param>
        /// <param name="sinA0">The <paramref name="sinA0"/>.</param>
        /// <param name="h1">The <paramref name="h1"/>.</param>
        /// <param name="k1">The <paramref name="k1"/>.</param>
        /// <param name="a1">The <paramref name="a1"/>.</param>
        /// <param name="b1">The <paramref name="b1"/>.</param>
        /// <param name="cosA1">The <paramref name="cosA1"/>.</param>
        /// <param name="sinA1">The <paramref name="sinA1"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Intersection EllipseEllipseIntersection(
            double h0, double k0, double a0, double b0, double cosA0, double sinA0,
            double h1, double k1, double a1, double b1, double cosA1, double sinA1,
            double epsilon = Epsilon)
        {
            // Equations of rotated ellipses.

            // ((x-h)cos(t)+(y-k)sin(t))^2/a^2+((x-h)sin(t)-(y-k)cos(t))^2/b^2=1

            // A_1* x^2+2*B_1* x*y+C_1* y^2+D_1* x+E_1* y+F_1=0
            // A_2* x^2+2*B_2* x*y+C_2* y^2+D_2* x+E_2* y+F_2=0

            // (((x*cos(t_1)+y*sin(t_1)-H_1)^2)/(a_1^2))+(((x*sin(t_1)-y*cos(t_1)-K_1)^2)/(b_1^2))=1
            // (((x*cos(t_2)+y*sin(t_2)-H_2)^2)/(a_2^2))+(((x*sin(t_2)-y*cos(t_2)-K_2)^2)/(b_2^2))=1

            // (a*x^2+b*y^2)cos(t)^2+(b*x^2+a*y^2)sin(t)^2+(a-b)x*y*sin(2*t)=1

            // x Solutions of rotated ellipses.
            // x=(-2B_1* y-D_1)±Sqrt((2*B_1* y+D_1)^2-4A_1* (C_1* y^2+E_1* y+1))/(2*A_1)
            // x=(-2B_2* y-D_2)±Sqrt((2*B_2* y+D_2)^2-4A_2* (C_1* y^2+E_2* y+1))/(2*A_2)

            // Potential References:
            // https://www.geometrictools.com/Documentation/IntersectionOfEllipses.pdf
            // https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/
            // https://web.archive.org/web/20130608140915/http://www.math.niu.edu/~rusin/known-math/99/2ellipses
            // https://web.archive.org/web/20110725185255/http://www.math.niu.edu/~rusin/known-math/95/ellipse.int
            // https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
            // https://books.google.fi/books?id=8CGj9_ZlFKoC&lpg=PA72&dq=hill%20conic%20sections%20graphics%20gems&pg=PA72#v=onepage&q=hill%20conic%20sections%20graphics%20gems&f=false
            // https://www.physicsforums.com/threads/intersection-between-rotated-translated-ellipse-and-line.769193/
            // https://web.archive.org/web/20160305073734/http://www.e-lc.org/docs/2007_01_17_00_46_52/
            // https://gist.github.com/drawable/92792f59b6ff8869d8b1
            // http://www.iis.sinica.edu.tw/papers/liu/637-F.pdf
            // http://yehar.com/blog/?p=2926
            // https://stackoverflow.com/q/2945337
            // https://stackoverflow.com/q/17213922
            // https://stackoverflow.com/q/38904322
            // https://stackoverflow.com/q/46639224
            // https://math.stackexchange.com/q/425366
            // https://math.stackexchange.com/q/426150
            // https://math.stackexchange.com/q/1114879
            // http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/
            // http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-2/
            // http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-3/
            // http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/
            // http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
            // http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
            // http://csharphelper.com/blog/2017/08/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/

            var result = new Intersection(IntersectionStates.NoIntersection);

            // Dot product of the angles of the two ellipses rotations. If the angles are perpendicular the dot product is 0, if parallel it is 1 or -1.
            var dot = DotProduct(cosA0, sinA0, cosA1, sinA1);

            if ((a0 == b0) && (a1 == b1))
            {
                // Special case, both ellipses are circles.
                return CircleCircleIntersection(h0, k0, a0, h1, k1, a1, epsilon);
            }
            else if (sinA0 == 0d && sinA1 == 0d && cosA0 == 1d && cosA1 == 1d)
            {
                // Special case, neither ellipse is rotated.
                return OrthogonalEllipseOrthogonalEllipseIntersection(h0, k0, a0, b0, h1, k1, a1, b1, epsilon);
            }
            else if (sinA0 == 0d && Abs(sinA1) == 1d && cosA0 == 1d && Abs(cosA1) <= CosineZeroEpsilon)
            {
                // Special case, first ellipse is not rotated, but the second is rotated 90 degrees left or right.
                return OrthogonalEllipseOrthogonalEllipseIntersection(h0, k0, a0, b0, h1, k1, b1, a1, epsilon);
            }
            else if (dot == 0d /*perpendicular*/ && (a0 == b1) && (b0 == a1) /*Equal sizes*/)
            {

            }
            else if (Abs(dot) == 1d /*parallel*/ && (a0 == a1) && (b0 == b1) /*Same size*/)
            {

            }

            return result;
        }

        /// <summary>
        /// The ellipse and ellipse intersects method.
        /// </summary>
        /// <param name="h0">The cx0.</param>
        /// <param name="k0">The cy0.</param>
        /// <param name="a0">The rx0.</param>
        /// <param name="b0">The ry0.</param>
        /// <param name="angle0">The angle0.</param>
        /// <param name="h1">The cx1.</param>
        /// <param name="k1">The cy1.</param>
        /// <param name="a1">The rx1.</param>
        /// <param name="b1">The ry1.</param>
        /// <param name="angle1">The angle1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool EllipseEllipseIntersects(double h0, double k0, double a0, double b0, double angle0, double h1, double k1, double a1, double b1, double angle1, double epsilon = Epsilon) => EllipseEllipseIntersects(h0, k0, a0, b0, Cos(angle0), Sin(angle0), h1, k1, a1, b1, Cos(angle1), Sin(angle1), epsilon);

        /// <summary>
        /// The ellipse and ellipse intersects method.
        /// </summary>
        /// <param name="h0">The cx0.</param>
        /// <param name="k0">The cy0.</param>
        /// <param name="a0">The rx0.</param>
        /// <param name="b0">The ry0.</param>
        /// <param name="cosA0">The cosA0.</param>
        /// <param name="sinA0">The sinA0.</param>
        /// <param name="h1">The cx1.</param>
        /// <param name="k1">The cy1.</param>
        /// <param name="a1">The rx1.</param>
        /// <param name="b1">The ry1.</param>
        /// <param name="cosA1">The cosA1.</param>
        /// <param name="sinA1">The sinA1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool EllipseEllipseIntersects(
            double h0, double k0, double a0, double b0, double cosA0, double sinA0,
            double h1, double k1, double a1, double b1, double cosA1, double sinA1,
            double epsilon = Epsilon)
        {
            _ = h0;
            _ = k0;
            _ = a0;
            _ = b0;
            _ = cosA0;
            _ = sinA0;
            _ = h1;
            _ = k1;
            _ = a1;
            _ = b1;
            _ = cosA1;
            _ = sinA1;
            _ = epsilon;
            return false;
        }
    }
}
