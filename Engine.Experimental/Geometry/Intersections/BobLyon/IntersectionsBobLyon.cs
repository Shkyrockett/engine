// <copyright file="IntersectionsBobLyon.cs" company="BobLyon" >
//     Copyright © 2016 - 2018 Bob Lyon. All rights reserved.
// </copyright>
// <author id="BobLyon">Bob Lyon</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     I am honored any time anybody uses my code for any purpose.
//     Copy freely! All my programs are at
//     https://www.khanacademy.org/profile/BobLyon/programs
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;
using static Engine.BobLyonCommon;

namespace Engine
{
    /// <summary>
    /// This program demonstrates the collisions between pairs of seven different geometric shapes. It also provides a Javascript library of easy to use, stateless functions that can be copied and pasted to other programs.
    /// The opening scene provides a 7¡¿7 button matrix where you choose the desired demonstration of a collision between two shapes.  Clicking a button will take you to a new scene where the two desired shapes are displayed.
    /// The shapes have little circles are called "knots" and they are draggable by the mouse. There are two ways to drag a knot. By clicking inside its circle the knot may be dragged anywhere, subject to the constrains imposed by the maintaining the knot's associated shape.  By clicking outside the knot's circle but within its "halo" the knot can only be dragged in a circular orbit around its associated "origin" knot.
    /// Dragging a shape's central or origin knot will drag the entire shape with it. This is how you move the a shape to collide with the other shape.  Collisions cause both shapes to change their appearences.
    /// If you wish to copy and paste the Javascript code that detects collisions between the two shapes, then click on the "Code" button and code will be provided in the Processing.js console panel via println().
    /// This program also demonstrates interior detection, that is, whether or not a point is inside the shape.  Interior detection is often associated with the mouse "hovering" over the shape.  In this program, if a shape is not involved in a collision, then hovering the mouse over the shape will cause a change in its appearence.
    /// If you wish to see a shape's interior detection Javascript code, then choose the collision scene between that shape and itself.  For example, to see the function that detects if a point is in a circle, then choose the scene for a circle colliding with a circle.  Selecting the "Code" button will produce Javascript code for both collisions and interior detection.
    /// Rotation and rectangles:
    /// The program and the collision detection library functions support rotated rectangles and ellipses.  Ellipses are assumed to be specified and drawn using the Processing.js default ellipseMode() of CENTER.  The library functions support all three modes for specifying rectangles - CORNER (the default), CENTER and CORNERS.
    /// If a scene involves a rectangle, then three additional buttons are supplied whereby you can select your desired rectangleMode().  If you are contemplating collision detection using rotated rectangles, then I recommend using rectangleMode() and rectMode() of CENTER.  However, the other two modes also work.
    /// Thanks to Phil Fulks @phfraf and Diogo Ribeiro @SlySherZ for identifying a critical bugs.
    /// I am honored any time anybody uses my code for any purpose.
    /// Copy freely! All my programs are at
    /// https://www.khanacademy.org/profile/BobLyon/programs
    /// </summary>
    public static partial class IntersectionsBobLyon
    {
        /// <summary>
        /// The ellipse, ellipse intersects.
        /// </summary>
        /// <param name="cx0">The cx0.</param>
        /// <param name="cy0">The cy0.</param>
        /// <param name="rx0">The rx0.</param>
        /// <param name="ry0">The ry0.</param>
        /// <param name="angle0">The angle0.</param>
        /// <param name="cx1">The cx1.</param>
        /// <param name="cy1">The cy1.</param>
        /// <param name="rx1">The rx1.</param>
        /// <param name="ry1">The ry1.</param>
        /// <param name="angle1">The angle1.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseEllipseIntersects(
            double cx0, double cy0, double rx0, double ry0, double angle0,
            double cx1, double cy1, double rx1, double ry1, double angle1)
            => EllipseEllipseIntersects(cx0, cy0, rx0, ry0, Cos(angle0), Sin(angle0), cx1, cy1, rx1, ry1, Cos(angle1), Sin(angle1));

        /// <summary>
        /// THIS IS (already) OBSOLETE.  Check https://www.khanacademy.org/computer-programming/c/5567955982876672 for a BETTER detector.
        /// Do two ellipses intersect?
        /// The first ellipse is described in its "traditional" manner by the first four parameters, along with a rotation parameter, rot1.
        /// Similarly, the last five parameters describe the second ellipse.
        /// </summary>
        /// <param name="cx0">The cx0.</param>
        /// <param name="cy0">The cy0.</param>
        /// <param name="rx0">The rx0.</param>
        /// <param name="ry0">The ry0.</param>
        /// <param name="cosA0">The cosA0.</param>
        /// <param name="sinA0">The sinA0.</param>
        /// <param name="cx1">The cx1.</param>
        /// <param name="cy1">The cy1.</param>
        /// <param name="rx1">The rx1.</param>
        /// <param name="ry1">The ry1.</param>
        /// <param name="cosA1">The cosA1.</param>
        /// <param name="sinA1">The sinA1.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseEllipseIntersects(
            double cx0, double cy0, double rx0, double ry0, double cosA0, double sinA0,
            double cx1, double cy1, double rx1, double ry1, double cosA1, double sinA1,
            double epsilon = Epsilon)
        {
            _ = epsilon;

            // Translate to origin.
            var u1 = cx1 - cx0;
            var v1 = cy1 - cy0;
            var u0 = 0d;
            var v0 = 0d;

            // Simple distance test...
            var maxR = (((rx0 > ry0) ? rx0 : ry0) + ((rx1 > ry1) ? rx1 : ry1)) / 2d;
            if ((u1 * u1) + (v1 * v1) > maxR * maxR)
            {
                // The two ellipses are too far apart to care about.
                return false;
            }

            // Is the center of one inside the other? 
            if (Intersections.EllipseContainsPoint(u0, v0, rx0, ry0, sinA0, cosA0, u1, v1) != Inclusions.Inside
             || Intersections.EllipseContainsPoint(u1, v1, rx1, ry1, sinA1, cosA1, u0, v0) != Inclusions.Inside)
            {
                return true;
            }

            // Okay. Do the hard work.
            var elps1 = BivariateForm(u0, v0, rx0, ry0, cosA0, sinA0);
            var elps2 = BivariateForm(u1, v1, rx1, ry1, cosA1, sinA1);
            var quartic = GetQuartic(elps1, elps2);
            return HasAzero(quartic);
        }

        /// <summary>
        /// Return true iff the point (x, y) is in the polygon
        /// described by the array "poly" whose elements are 
        /// vertex objects with "x" and "y" properties. Derived
        /// from Pamela's function in
        /// https://www.khanacademy.org/cs/p/5211412870725632
        /// which implements the even-odd rule of ray tracing.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="poly">The poly.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PolygonContainsPoint(
            double x, double y,
            (double x, double y)[] poly)
        {
            var isIn = false;
            for (int i = 0, j = poly.Length - 1; i < poly.Length; j = i++)
            {
                var xi = poly[i].x;
                var yi = poly[i].y;
                var xj = poly[j].x;
                var yj = poly[j].y;
                var intersect = ((yi > y) != (yj > y)) && (x < ((xj - xi) * (y - yi) / (yj - yi)) + xi);
                if (intersect)
                {
                    isIn = !isIn;
                }
            }
            return isIn;
        }

        /// <summary>
        /// Return true iff point (x, y) in the triangle
        /// described by rx, ry, w and h in conjunction
        /// with rectangleMode.mode.  OPTIONAL parameter
        /// theta describes the angle of rotation of the
        /// rectangle around its origin.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleContainsPoint(
            double x, double y,
            double rx, double ry, double w, double h, double theta, Mode rectangleMode)
        {
            if (theta != 0)
            {
                /* rotate the point wrt (rx, ry) */
                var (px, py) = RotatePoint2D(x - rx, y - ry, theta);
                x = px + rx;
                y = py + ry;
            }
            if (rectangleMode == Mode.Corners)
            {
                w -= rx;
                h -= ry;
            }
            else if (rectangleMode == Mode.Center)
            {
                rx -= w / 2;
                ry -= h / 2;
            }
            return Intersections.Between(x, rx, rx + w) && Intersections.Between(y, ry, ry + h);
        }

        /// <summary>
        /// Return true iff point (x, y) is in the circle 
        /// centered at (cx, cy) with diameter diam.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="diam">The diam.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleContainsPoint(
            double x, double y,
            double cx, double cy, double diam)
        {
            var dx = x - cx;
            var dy = y - cy;
            return (dx * dx) + (dy * dy) <= diam * diam / 4;
        }

        /// <summary>
        /// Return true iff point (x, y) is in the ellipse 
        /// centered at (ex, ey) with axes lengths w and h.
        /// Optional parameter theta is the angle of rotation
        /// of the ellipse, OR with optional parameter sine 
        /// it is the cosine of the angle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseContainsPoint(
            double x, double y,
            double ex, double ey, double w, double h)
            => EllipseContainsPoint(x, y, ex, ey, w, h, Cos(0), Sin(0));

        /// <summary>
        /// Return true iff point (x, y) is in the ellipse 
        /// centered at (ex, ey) with axes lengths w and h.
        /// Optional parameter theta is the angle of rotation
        /// of the ellipse, OR with optional parameter sine 
        /// it is the cosine of the angle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseContainsPoint(
            double x, double y,
            double ex, double ey, double w, double h, double theta)
            => EllipseContainsPoint(x, y, ex, ey, w, h, Cos(theta), Sin(theta));

        /// <summary>
        /// Return true iff point (x, y) is in the ellipse 
        /// centered at (ex, ey) with axes lengths w and h.
        /// Optional parameter theta is the angle of rotation
        /// of the ellipse, OR with optional parameter sine 
        /// it is the cosine of the angle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="cosine">The cosine.</param>
        /// <param name="sine">The sine.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseContainsPoint(
            double x, double y,
            double ex, double ey, double w, double h, double cosine, double sine)
        {
            /* Normalize wrt the ellipse center */
            x -= ex;
            y -= ey;
            if (cosine != 1 | sine != 0)
            {
                (x, y) = RotatePoint2D(x, y, cosine, sine);
            }
            var termX = 2 * x / w;  /* sqrt of first term */
            var termY = 2 * y / h;  /* sqrt of second term */
            return ((termX * termX) + (termY * termY)) <= 1;
        }

        /// <summary>
        /// Return true iff the point (x, y) is in the 
        /// pizza  slice defined by "arc" using the
        /// last six parameters.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="start">The start.</param>
        /// <param name="stop">The stop.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ArcContainsPoint(
            double x, double y,
            double ex, double ey, double w, double h, double start, double stop)
        {
            var heading = Atan2(y - ey, x - ex);
            heading += SubtendedToParametric(w / 2, h / 2, heading);
            return Intersections.AngleBetween(heading, start, stop) && EllipseContainsPoint(x, y, ex, ey, w, h);
        }

        /// <summary>
        /// Return true iff the two line segments
        /// "a" and "b" intersect.  From
        /// http://wikipedia.org/wiki/Line%E2%80%93line_intersection
        /// </summary>
        /// <param name="ax1">The ax1.</param>
        /// <param name="ay1">The ay1.</param>
        /// <param name="ax2">The ax2.</param>
        /// <param name="ay2">The ay2.</param>
        /// <param name="bx1">The bx1.</param>
        /// <param name="by1">The by1.</param>
        /// <param name="bx2">The bx2.</param>
        /// <param name="by2">The by2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLineCollide(
            double ax1, double ay1, double ax2, double ay2,
            double bx1, double by1, double bx2, double by2)
        {
            var denom = ((ax1 - ax2) * (by1 - by2)) - ((ay1 - ay2) * (bx1 - bx2));
            if (denom == 0)
            {
                /* Yuck, parallel lines! */
                if (ax1 == ax2)
                {
                    /* Vertical parallel lines! */
                    return (ax1 == bx1) && Overlap(ay1, ay2, by1, by2);
                }
                else
                {
                    var m = (ay1 - ay2) / (ax1 - ax2);  /* common slope */
                    var ka = ay1 - (m * ax1);  /* line A intercept */
                    var kb = by1 - (m * bx1);  /* line B intercept */
                    return (ka == kb) && Overlap(ax1, ax2, bx1, bx2);
                }
            }

            var roundBase = 1d / 1024d / 1024d / 1024d;  /* one part in a billion */
            double ROUND(double n)
            {
                return Round(n / roundBase) * roundBase;
            };

            var na = (ax1 * ay2) - (ay1 * ax2);
            var nb = (bx1 * by2) - (by1 * bx2);
            var x = ROUND(((na * (bx1 - bx2)) - ((ax1 - ax2) * nb)) / denom);
            var y = ROUND(((na * (by1 - by2)) - ((ay1 - ay2) * nb)) / denom);
            return Intersections.Between(x, ROUND(ax1), ROUND(ax2)) && Intersections.Between(x, ROUND(bx1), ROUND(bx2)) &&
                Intersections.Between(y, ROUND(ay1), ROUND(ay2)) && Intersections.Between(y, ROUND(by1), ROUND(by2));
        }

        /// <summary>
        /// Return true iff the line segment described by the first
        /// four parameters intersects the polygon described by
        /// "poly", an array of vertex objects with x and y properties.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="poly">The poly.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LinePolygonCollide(
            double x1, double y1, double x2, double y2,
            (double x, double y)[] poly)
        {
            var collide = PolygonContainsPoint(x1, y1, poly);
            for (int j = poly.Length - 1, i = 0; (!collide) && (i < poly.Length); j = i, i++)
            {
                collide = LineLineCollide(x1, y1, x2, y2, poly[j].x, poly[j].y, poly[i].x, poly[i].y);
            }
            return collide;
        }

        /// <summary>
        /// Return true iff the line segment described by the first
        /// four parameters intersects the triangle described by
        /// the last six parameters.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="tx1">The tx1.</param>
        /// <param name="ty1">The ty1.</param>
        /// <param name="tx2">The tx2.</param>
        /// <param name="ty2">The ty2.</param>
        /// <param name="tx3">The tx3.</param>
        /// <param name="ty3">The ty3.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineTriangleCollide(
            double x1, double y1, double x2, double y2,
            double tx1, double ty1, double tx2, double ty2, double tx3, double ty3)
        {
            var tri = Coords2Points(tx1, ty1, tx2, ty2, tx3, ty3);
            return LinePolygonCollide(x1, y1, x2, y2, tri);
        }

        /// <summary>
        /// Return true iff the line segment described by the first
        /// four parameters intersects the rectangle described by
        /// the next four parameters, rotated by OPTIONAL parameter,
        /// theta.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineRectCollide(
            double x1, double y1, double x2, double y2,
            double x, double y, double w, double h, double theta, Mode rectangleMode)
        {
            var rect = Rect2Points(x, y, w, h, theta, rectangleMode);
            return LinePolygonCollide(x1, y1, x2, y2, rect);
        }

        /// <summary>
        /// Return true iff the line segment described by the first
        /// four parameters intersects the quadrilateral described
        /// by the last eight parameters.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="qx1">The qx1.</param>
        /// <param name="qy1">The qy1.</param>
        /// <param name="qx2">The qx2.</param>
        /// <param name="qy2">The qy2.</param>
        /// <param name="qx3">The qx3.</param>
        /// <param name="qy3">The qy3.</param>
        /// <param name="qx4">The qx4.</param>
        /// <param name="qy4">The qy4.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineQuadCollide(
            double x1, double y1, double x2, double y2,
            double qx1, double qy1, double qx2, double qy2, double qx3, double qy3, double qx4, double qy4)
        {
            var quad = Coords2Points(qx1, qy1, qx2, qy2, qx3, qy3, qx4, qy4);
            return LinePolygonCollide(x1, y1, x2, y2, quad);
        }

        /// <summary>
        /// Return true iff the line segment described by
        /// the first four parameters intersects the circle
        /// described by the last three parameters.
        /// (Simplified code from lineEllipseCollide.)
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="diam">The diam.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineCircleCollide(
            double x1, double y1, double x2, double y2,
            double cx, double cy, double diam)
        {
            var m = (y2 - y1) / (x2 - x1);  /* slope */
            if (Abs(m) > 1024)
            {
                /* Convert vertical to horizontal and try again. */
                return LineCircleCollide(y1, x1, y2, x2, cy, cx, diam);
            }
            if (CircleContainsPoint(x2, y2, cx, cy, diam))
            {
                /* The segment may be entirely inside the circle */
                return true;
            }
            x1 -= cx;
            x2 -= cx;
            y1 -= cy;
            //y2 -= cy;
            var r = diam * diam / 4;  /* radius squared */
            var k = y1 - (m * x1);  /* So, y = m*x + k */
            var a = (1 + (m * m)) / r;
            var b = 2 * m * k / r;
            var c = (k * k / r) - 1;
            var discrim = (b * b) - (4 * a * c);
            if (discrim < 0)
            {
                return false;  /* No intersection */
            }
            discrim = Sqrt(discrim);
            a *= 2;
            return Intersections.Between((-b - discrim) / a, x1, x2) ||
                Intersections.Between((-b + discrim) / a, x1, x2);
        }

        /// <summary>
        /// Return true iff the line segment described by Processing.js
        /// line(x1, y1, x2, y2) intersects the ellipse(ex, ey, w, h)
        /// rotated by angle theta.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineEllipseCollide(
            double x1, double y1, double x2, double y2,
            double ex, double ey, double w, double h, double theta = 0)
        {
            var cosine = 1d;
            var sine = 0d;
            if (theta != 0)
            {
                cosine = Cos(theta);  /* compute once... */
                sine = Sin(theta);  /* ...use often */
            }
            /*
             * Normalize s.t. the ellipse is centered at
             * the origin and is not rotated.
             */
            var (r1x, r1y) = RotatePoint2D(x1 - ex, y1 - ey, cosine, sine);
            var (r2x, r2y) = RotatePoint2D(x2 - ex, y2 - ey, cosine, sine);
            var m = (r2y - r1y) / (r2x - r1x);  /* slope */
            if (Abs(m) > 1024)
            {
                /* Vertical line, so swap X & Y, and try again. */
                return LineEllipseCollide(r1y, r1x, r2y, r2x, 0, 0, h, w);
            }
            if (EllipseContainsPoint(r2x, r2y, 0, 0, w, h))
            {
                /* The segment may be entirely inside the ellipse */
                return true;
            }
            /* Intersect:
             * x©÷/s + y©÷/t = 1 with y = mx + k
             * x©÷/s + (mx + k)©÷/t = 1
             * x©÷/s + (m©÷x©÷ + 2mkx + k©÷)/t - 1 = 0
             * (1/s + m©÷/t)x©÷ + (2mk/t)x + (k©÷/t - 1) = 0
             * is the standard form of a quadratic that
             * can be solved by the quadratic formula:
             * x = (-b +|- sqrt(b©÷ - 4ac)) / 2a where
             * a = 1/s + m©÷/t, b = 2mk/t and c = k©÷/t - 1
             */
            var s = w * w / 4;
            var t = h * h / 4;  /* So, x©÷/s + y©÷/t = 1 */
            var k = r1y - (m * r1x);  /* So, y = m*x + k */
            var a = (1 / s) + (m * m / t);
            var b = 2 * m * k / t;
            var c = (k * k / t) - 1;
            var discrim = (b * b) - (4 * a * c);
            if (discrim < 0)
            {
                return false;  /* No intersection */
            }
            discrim = Sqrt(discrim);
            a *= 2;
            return Intersections.Between((-b - discrim) / a, r1x, r2x) ||
                Intersections.Between((-b + discrim) / a, r1x, r2x);
        }

        /// <summary>
        /// Return true iff the circle described by the first
        /// three parameters collides with the circle described
        /// by the last three parameters.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="diam1">The diam1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="diam2">The diam2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircleCollide(
            double x1, double y1, double diam1,
            double x2, double y2, double diam2)
        {
            var dx = x1 - x2;
            var dy = y1 - y2;
            var dist2 = (dx * dx) + (dy * dy);
            var sum = (diam1 + diam2) / 2;  /* radius1 + radius2 */
            return dist2 <= sum * sum;
        }

        /// <summary>
        /// Return true iff poly and the ellipse centered at (ex, ey) 
        /// with width w and height h, rotated by theta collide.  "poly"
        /// is an array of vertex objects with x and y properties.
        /// Algorithm is described by ShreevatsaR at
        /// http://stackoverflow.com/questions/401847/
        /// </summary>
        /// <param name="poly">The poly.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PolygonEllipseCollide(
            (double x, double y)[] poly,
            double ex, double ey, double w, double h, double theta)
        {
            var collide = PolygonContainsPoint(ex, ey, poly);
            for (int i = 0, j = poly.Length - 1; (!collide) && (i < poly.Length); j = i, i++)
            {
                collide = LineEllipseCollide(poly[j].x, poly[j].y, poly[i].x, poly[i].y, ex, ey, w, h, theta);
            }
            return collide;
        }

        /// <summary>
        /// Return true iff the rectangle described by the first four parameters
        /// rotated by rTheta collides with the rotated ellipse described by the
        /// last five parameters.
        /// </summary>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="rw">The rw.</param>
        /// <param name="rh">The rh.</param>
        /// <param name="rTheta">The rTheta.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="ey">The ey.</param>
        /// <param name="ew">The ew.</param>
        /// <param name="eh">The eh.</param>
        /// <param name="eTheta">The eTheta.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectEllipseCollide(
            double rx, double ry, double rw, double rh, double rTheta,
            double ex, double ey, double ew, double eh, double eTheta,
            Mode rectangleMode)
        {
            var rect = Rect2Points(rx, ry, rw, rh, rTheta, rectangleMode);
            return PolygonEllipseCollide(rect, ex, ey, ew, eh, eTheta);
        }

        /// <summary>
        /// Return true iff the polygon described by poly
        /// intersects the circle described by the last
        /// three parameters. "poly" is an array of vertex
        /// objects with x and y properties.
        /// </summary>
        /// <param name="poly">The poly.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="diam">The diam.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PolygonCircleCollide(
            (double x, double y)[] poly,
            double cx, double cy, double diam)
        {
            var collide = PolygonContainsPoint(cx, cy, poly);
            for (int i = 0, j = poly.Length - 1; (!collide) && (i < poly.Length); j = i, i++)
            {
                collide = LineCircleCollide(poly[j].x, poly[j].y, poly[i].x, poly[i].y, cx, cy, diam);
            }
            return collide;
        }

        /// <summary>
        /// Return true iff the rectangle described by the first
        /// four parameters rotated by theta collides with the circle
        /// described by the last three parameters.
        /// </summary>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="diam">The diam.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectCircleCollide(
            double rx, double ry, double w, double h, double theta,
            double cx, double cy, double diam, Mode rectangleMode)
        {
            if (theta != 0)
            {
                /* rotate the circle center wrt rect point (rx, ry). */
                var (X, Y) = RotatePoint2D(cx - rx, cy - ry, theta);
                cx = X + rx;
                cy = Y + ry;
            }
            if (rectangleMode == Mode.Corners)
            {
                w -= rx;
                h -= ry;
            }
            else if (rectangleMode == Mode.Center)
            {
                rx -= w / 2;
                ry -= h / 2;
            }

            /* See Cygon at http://stackoverflow.com/questions/401847/ */
            var closestX = Constrain(cx, rx, rx + w);
            var closestY = Constrain(cy, ry, ry + h);
            return CircleContainsPoint(closestX, closestY, cx, cy, diam);
        }

        /// <summary>
        /// Return true iff CONVEX polygons 1 and 2 collide.
        /// Use the Separation Axis Theorem as explained by
        /// http://www.dyn4j.org/2010/01/sat/
        /// </summary>
        /// <param name="poly1">The poly1.</param>
        /// <param name="poly2">The poly2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PolygonPolygonCollide(
            (double x, double y)[] poly1,
            (double x, double y)[] poly2)
        {
            var polys = poly1.Concat(poly2).ToArray();

            /*
             * Project polygon onto axis.  Simply
             * compute dot products between the
             * polygon's vertices and the axis, and
             * keep track of the min and max values.
             */
            static (double min, double max) project((double x, double y)[] poly, (double x, double y) axis)
            {
                var mn = double.PositiveInfinity;
                var mx = double.NegativeInfinity;
                for (var i = 0; i < poly.Length; i++)
                {
                    var dot = (poly[i].x * axis.x) + (poly[i].y * axis.y);
                    mx = Max(mx, dot);
                    mn = Min(mn, dot);
                }
                return (min: mn, max: mx);
            };

            /* Compute all projections axes of polygon. */
            static (double y, double x)[] getAxes(params (double x, double y)[] poly)
            {
                var axes = new (double y, double x)[poly.Length];
                for (var i = 0; i < poly.Length; i++)
                {
                    var n = (i + 1) % poly.Length;
                    /*
                     * The edge is simply the delta between i and n.
                     * The axis is the edge's normal. And a normal 
                     * of (x, y) is either of (y, -x) or (-y, x).
                     */
                    axes[i] = (
                        y: poly[i].x - poly[n].x,
                        x: -(poly[i].y - poly[n].y)
                    );
                }
                return axes;
            };

            for (var p = 0; p < polys.Length; p++)
            {
                var axes = getAxes(polys[p]);
                for (var i = 0; i < axes.Length; i++)
                {
                    var axis = axes[i];
                    /* Project both polygons onto this axis */
                    var (p1min, p1max) = project(poly1, axis);
                    var (p2min, p2max) = project(poly2, axis);
                    if (!Overlap(p1min, p1max, p2min, p2max))
                    {
                        /* The two polygons cannot overlap */
                        return false;
                    }
                }
            }
            return true;  /* they do overlap */
        }

        /// <summary>
        /// Return true iff two rectangles collide. Each is described by
        /// traditional Processing.js arguments, rotated by angle theta.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="h1">The h1.</param>
        /// <param name="theta1">The theta1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="h2">The h2.</param>
        /// <param name="theta2">The theta2.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectRectCollide(
            double x1, double y1, double w1, double h1, double theta1,
            double x2, double y2, double w2, double h2, double theta2,
            Mode rectangleMode)
        {
            if (theta1 == 0 && theta2 == 0)
            {
                if (rectangleMode == Mode.Corners)
                {
                    w1 -= x1;
                    h1 -= y1;
                    w2 -= x2;
                    h2 -= y2;
                }
                else if (rectangleMode == Mode.Center)
                {
                    x1 -= w1 / 2d;
                    y1 -= h1 / 2d;
                    x2 -= w2 / 2d;
                    y2 -= h2 / 2d;
                }
                return Overlap(x1, x1 + w1, x2, x2 + w2) && Overlap(y1, y1 + h1, y2, y2 + h2);
            }
            else
            {
                return PolygonPolygonCollide(
                    Rect2Points(x1, y1, w1, h1, theta1, rectangleMode),
                    Rect2Points(x2, y2, w2, h2, theta2, rectangleMode));
            }
        }

        /// <summary>
        /// Return true iff two triangles collide. Each is described by
        /// traditional Processing.js arguments.
        /// </summary>
        /// <param name="ax1">The ax1.</param>
        /// <param name="ay1">The ay1.</param>
        /// <param name="ax2">The ax2.</param>
        /// <param name="ay2">The ay2.</param>
        /// <param name="ax3">The ax3.</param>
        /// <param name="ay3">The ay3.</param>
        /// <param name="bx1">The bx1.</param>
        /// <param name="by1">The by1.</param>
        /// <param name="bx2">The bx2.</param>
        /// <param name="by2">The by2.</param>
        /// <param name="bx3">The bx3.</param>
        /// <param name="by3">The by3.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TriangleTriangleCollide(
            double ax1, double ay1, double ax2, double ay2, double ax3, double ay3,
            double bx1, double by1, double bx2, double by2, double bx3, double by3)
        {
            var triA = Coords2Points(ax1, ay1, ax2, ay2, ax3, ay3);
            var triB = Coords2Points(bx1, by1, bx2, by2, bx3, by3);
            return PolygonPolygonCollide(triA, triB);
        }

        /// <summary>
        /// Return true iff two quadrilateral collide. Each is described
        /// by traditional Processing.js arguments.
        /// </summary>
        /// <param name="ax1">The ax1.</param>
        /// <param name="ay1">The ay1.</param>
        /// <param name="ax2">The ax2.</param>
        /// <param name="ay2">The ay2.</param>
        /// <param name="ax3">The ax3.</param>
        /// <param name="ay3">The ay3.</param>
        /// <param name="ax4">The ax4.</param>
        /// <param name="ay4">The ay4.</param>
        /// <param name="bx1">The bx1.</param>
        /// <param name="by1">The by1.</param>
        /// <param name="bx2">The bx2.</param>
        /// <param name="by2">The by2.</param>
        /// <param name="bx3">The bx3.</param>
        /// <param name="by3">The by3.</param>
        /// <param name="bx4">The bx4.</param>
        /// <param name="by4">The by4.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool QuadQuadCollide(
            double ax1, double ay1, double ax2, double ay2, double ax3, double ay3, double ax4, double ay4,
            double bx1, double by1, double bx2, double by2, double bx3, double by3, double bx4, double by4)
        {
            var quadA = Coords2Points(ax1, ay1, ax2, ay2, ax3, ay3, ax4, ay4);
            var quadB = Coords2Points(bx1, by1, bx2, by2, bx3, by3, bx4, by4);
            return PolygonPolygonCollide(quadA, quadB);
        }

        /// <summary>
        /// Do two ellipses collide?  The first ellipse is described
        /// in its "traditional" manner by the first four parameters,
        /// along with a rotation parameter, theta1.  Similarly, the last
        /// five parameters describe the second ellipse.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="h1">The h1.</param>
        /// <param name="theta1">The theta1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="w2">The w2.</param>
        /// <param name="h2">The h2.</param>
        /// <param name="theta2">The theta2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EllipseEllipseCollide(
            double x1, double y1, double w1, double h1, double theta1,
            double x2, double y2, double w2, double h2, double theta2)
        {
            if (!CircleCircleCollide(x1, y1, (w1 > h1) ? w1 : h1, x2, y2, (w2 > h2) ? w2 : h2))
            {
                // If they were circles, they're too far apart! 
                return false;
            }

            var cosine1 = Cos(theta1);
            var sine1 = Sin(theta1);
            var cosine2 = Cos(theta2);
            var sine2 = Sin(theta2);

            // Is the center of one inside the other? 
            if (EllipseContainsPoint(x2, y2, x1, y1, w1, h1, cosine1, sine1) ||
                EllipseContainsPoint(x1, y1, x2, y2, w2, h2, cosine2, sine2))
            {
                return true;
            }

            // Okay, do the hard work
            var elps1 = BivariateForm(x1, y1, w1, h1, cosine1, sine1);
            var elps2 = BivariateForm(x2, y2, w2, h2, cosine2, sine2);

            // Now, ask your good friend with a PhD in Mathematics how he
            // would do it; then translate his R code.  See
            // https://docs.google.com/file/d/0B7wsEy6bpVePSEt2Ql9hY0hFdjA/
            return DoConicsIntersect(elps1, elps2);
        }
    }
}
