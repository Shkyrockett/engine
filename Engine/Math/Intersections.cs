// <copyright file="Intersections.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// A collection of methods for checking the interactions of objects.
    /// </summary>
    public static class Intersections
    {
        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Inclusion Intersects(LineSegment s, Point2D p)
            => LineSegmentPoint(s.A.X, s.A.Y, s.B.X, s.B.Y, p.X, p.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion LineSegmentPoint(
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY,
            double pointX,
            double pointY)
            => ((pointX == segmentAX) && (pointY == segmentAY)) ||
                ((pointX == segmentBX) && (pointY == segmentBY)) ||
                (((pointX > segmentAX) == (pointX < segmentBX)) &&
                ((pointY > segmentAY) == (pointY < segmentBY)) &&
                ((pointX - segmentAX) * (segmentBY - segmentAY) ==
                (segmentBX - segmentAX) * (pointY - segmentAY))) ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static (bool Intersecting, Point2D Points) Intersects(LineSegment s1, LineSegment s2)
            => LineLine(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y);

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool Intersecting, Point2D Points) LineLine(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Translate lines to origin.
            double deltaAI = (x1 - x0);
            double deltaAJ = (y1 - y0);
            double deltaBI = (x3 - x2);
            double deltaBJ = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (Abs(determinant) < Epsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * deltaAJ + (y2 - y0) * deltaAI) / -determinant;
            double t = ((x2 - x0) * deltaBJ + (y0 - y2) * deltaBI) / determinant;

            return (
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Point2D(x0 + t * deltaAI, y0 + t * deltaAJ));
        }

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangle(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="height1"></param>
        /// <param name="width1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="height2"></param>
        /// <param name="width2"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangle(
            double x1, double y1,
            double height1, double width1,
            double x2, double y2,
            double height2, double width2)
            => (x2 < x1 + width1)
            && (x1 < (x2 + width2))
            && (y2 < y1 + height1)
            && (y1 < y2 + width2);

        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int Count, Point2D Intersecting1, Point2D Intersecting2) CircleCircle(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            Point2D intersection1;
            Point2D intersection2;

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if ((Abs(dist) < Epsilon) && (Abs(radius0 - radius1) < Epsilon))
            {
                // No solutions, the circles coincide.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0
                    - radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = new Point2D(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist));
                intersection2 = new Point2D(
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist));

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < Epsilon)
                    return (1, intersection1, intersection2);

                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static (bool, (double, double)?, bool, (double, double)?) Intersects(this Circle c, LineSegment s)
            => CircleLineSegment(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="radius"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) CircleLineSegment(
            double cX, double cY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double a = dx * dx + dy * dy;
            double b = 2 * (dx * (x1 - cX) + dy * (y1 - cY));
            double c = (x1 - cX) * (x1 - cX) + (y1 - cY) * (y1 - cY) - radius * radius;

            double determinant = b * b - 4 * a * c;

            if ((a <= 0.0000001) || (determinant < 0))
            {
                // No real solutions.
                return (false, null, false, null);
            }
            else if (determinant == 0)
            {
                // One solution.
                double t = -b / (2 * a);
                return ((t >= 0d) && (t <= 1d), (x1 + t * dx, y1 + t * dy), false, null);
            }
            else
            {
                // Two solutions.
                double t1 = ((-b + Sqrt(determinant)) / (2 * a));
                double t2 = ((-b - Sqrt(determinant)) / (2 * a));
                return ((t1 >= 0d) && (t1 <= 1d), (x1 + t1 * dx, y1 + t1 * dy),
                        (t2 >= 0d) && (t2 <= 1d), (x1 + t2 * dx, y1 + t2 * dy));
            }
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) Intersects(this Ellipse e, LineSegment s)
            => EllipseLineSegment(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) EllipseLineSegment(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return (false, null, false, null);

            // Translate so the ellipse is centered at the origin.
            double p1X = x0 - cx;
            double p1Y = y0 - cy;
            double p2X = x1 - cx;
            double p2Y = y1 - cy;

            // Calculate the quadratic parameters.
            double a = (p2X - p1X) * (p2X - p1X) / rx / rx + (p2Y - p1Y) * (p2Y - p1Y) / ry / ry;
            double b = 2d * p1X * (p2X - p1X) / rx / rx + 2 * p1Y * (p2Y - p1Y) / ry / ry;
            double c = p1X * p1X / rx / rx + p1Y * p1Y / ry / ry - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                double t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return ((t >= 0d) && (t <= 1d), (p1X + (p2X - p1X) * t + cx, p1Y + (p2Y - p1Y) * t + cy), false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Return the points. If the points are on the segment set the bool to true.
                return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // No real solutions.
            return (false, null, false, null);
        }

        /// <summary>
        /// Sutherland Hodgman Intersection. This clips the subject polygon against the clip polygon (gets the intersection of the two polygons)
        /// </summary>
        /// <param name="subjectPoly">Can be concave or convex</param>
        /// <param name="clipPoly">Must be convex</param>
        /// <returns>The intersection of the two polygons (or null)</returns>
        /// <remarks>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Based on the psuedocode from:
        /// http://en.wikipedia.org/wiki/Sutherland%E2%80%93Hodgman
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> PolygonPolygon(List<Point2D> subjectPoly, List<Point2D> clipPoly)
        {
            if (subjectPoly.Count < 3 || clipPoly.Count < 3)
                throw new ArgumentException($"The polygons passed in must have at least 3 points: subject={subjectPoly.Count}, clip={clipPoly.Count}");

            // clone it
            List<Point2D> outputList = subjectPoly.ToList();

            // Make sure it's clockwise
            if (!PolygonExtensions.IsClockwise(subjectPoly))
                outputList.Reverse();

            // Walk around the clip polygon clockwise
            foreach (LineSegment clipEdge in PolygonExtensions.IterateEdgesClockwise(clipPoly))
            {
                // clone it
                List<Point2D> inputList = outputList.ToList();
                outputList.Clear();

                // Sometimes when the polygons don't intersect, this list goes to zero.
                // Jump out to avoid an index out of range exception
                if (inputList.Count == 0)
                    break;

                Point2D S = inputList[inputList.Count - 1];

                foreach (Point2D e in inputList)
                {
                    if (PolygonExtensions.IsInside(clipEdge, e))
                    {
                        if (!PolygonExtensions.IsInside(clipEdge, S))
                        {
                            (bool Intersects, Point2D Point) point = LineLine(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                            if (point.Intersects == false)
                            {
                                // may be collinear, or may be a bug
                                throw new ApplicationException("Line segments don't intersect");
                            }
                            else
                            {
                                outputList.Add(point.Item2);
                            }
                        }

                        outputList.Add(e);
                    }
                    else if (PolygonExtensions.IsInside(clipEdge, S))
                    {
                        (bool Intersects, Point2D Point) point = LineLine(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                        if (point.Intersects == false)
                        {
                            // may be collinear, or may be a bug
                            throw new ApplicationException("Line segments don't intersect");
                        }
                        else
                        {
                            outputList.Add(point.Item2);
                        }
                    }

                    S = e;
                }
            }

            // Exit Function
            return outputList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="l0x"></param>
        /// <param name="l0y"></param>
        /// <param name="l1x"></param>
        /// <param name="l1y"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://www.particleincell.com/2013/cubic-line-intersection/
        /// </remarks>
        public static List<Point2D> CubicBezierLineSegment(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new List<Point2D>();

            var A = l1y - l0y;      //A=y2-y1
            var B = l0x - l1x;      //B=x1-x2
            var C = l0x * (l0y - l1y) + l0y * (l1x - l0x);  //C=x1*(y1-y2)+y1*(x2-x1)

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            var r = cubicRoots(
                A * bx.A + B * by.A,    /*t^3*/
                A * bx.B + B * by.B,    /*t^2*/
                A * bx.C + B * by.C,    /*t*/
                A * bx.D + B * by.D + C /*1*/
                );

            /*verify the roots are in bounds of the linear segment*/
            for (var i = 0; i < 3; i++)
            {
                double t = r[i];

                double x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                double y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

                /*above is intersection point assuming infinitely long line segment,
                  make sure we are also in bounds of the line*/
                double m;
                if ((l1x - l0x) != 0)           /*if not vertical line*/
                    m = (x - l0x) / (l1x - l0x);
                else
                    m = (y - l0y) / (l1y - l0y);

                /*in bounds?*/
                if (t < 0 || t > 1d || m < 0 || m > 1d)
                {
                    x = 0;// -100;  /*move off screen*/
                    y = 0;// -100;
                }
                else
                    /*intersection point*/
                    I.Add(new Point2D(x, y));
            }
            return I;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </remarks>
        private static double[] cubicRoots(double a, double b, double c, double d)
        {
            // The horizontal line issue seems to be somewhere in here.
            var A = b / a;
            var B = c / a;
            var C = d / a;

            double S, T, Im;

            double Q = (3 * B - Pow(A, 2)) / 9;
            double R = (9 * A * B - 27 * C - 2 * Pow(A, 3)) / 54;
            double D = Pow(Q, 3) + Pow(R, 2);    // polynomial discriminant

            var t = new double[3];

            if (D >= 0)                                 // complex or duplicate roots
            {
                S = Sign(R + Sqrt(D)) * Pow(Abs(R + Sqrt(D)), (1 / 3));
                T = Sign(R - Sqrt(D)) * Pow(Abs(R - Sqrt(D)), (1 / 3));

                t[0] = -A / 3 + (S + T);                    // real root
                t[1] = -A / 3 - (S + T) / 2;                  // real part of complex root
                t[2] = -A / 3 - (S + T) / 2;                  // real part of complex root
                Im = Abs(Sqrt(3) * (S - T) / 2);    // complex part of root pair   

                /*discard complex roots*/
                if (Im != 0)
                {
                    t[1] = -1;
                    t[2] = -1;
                }

            }
            else                                          // distinct real roots
            {
                var th = Acos(R / Sqrt(-Pow(Q, 3)));

                t[0] = 2 * Sqrt(-Q) * Cos(th / 3) - A / 3;
                t[1] = 2 * Sqrt(-Q) * Cos((th + Tau) / 3) - A / 3;
                t[2] = 2 * Sqrt(-Q) * Cos((th + 4 * PI) / 3) - A / 3;
                Im = 0.0;
            }

            /*discard out of spec roots*/
            for (var i = 0; i < 3; i++)
                if (t[i] < 0 || t[i] > 1.0) t[i] = -1;

            /*sort but place -1 at the end*/
            t = sortSpecial(t);

            //Console.log(t[0] + " " + t[1] + " " + t[2]);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static double[] sortSpecial(double[] a)
        {
            bool flip;
            double temp;

            do
            {
                flip = false;
                for (var i = 0; i < a.Length - 1; i++)
                {
                    if ((a[i + 1] >= 0 && a[i] > a[i + 1]) ||
                        (a[i] < 0 && a[i + 1] >= 0))
                    {
                        flip = true;
                        temp = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = temp;

                    }
                }
            } while (flip);
            return a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/27664298/calculating-intersection-point-of-quadratic-bezier-curve
        /// </remarks>
        private static List<Point2D> QuadraticBezierLineSegment(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            List<Point2D> intersections = new List<Point2D>();

            // inverse line normal
            var normal = new Point2D(a1Y - a2Y, a2X - a1X);

            // Q-coefficients
            var c2 = new Point2D(p1X + p2X * -2 + p3X, p1Y + p2Y * -2 + p3Y);
            var c1 = new Point2D(p1X * -2 + p2X * 2, p1Y * -2 + p2Y * 2);
            var c0 = new Point2D(p1X, p1Y);

            // Transform to line 
            var coefficient = a1X * a2Y - a2X * a1Y;
            var a = normal.X * c2.X + normal.Y * c2.Y;
            var b = (normal.X * c1.X + normal.Y * c1.Y) / a;
            var c = (normal.X * c0.X + normal.Y * c0.Y + coefficient) / a;

            // solve the roots
            List<double> roots = new List<double>();
            var d = b * b - 4 * c;
            if (d > 0)
            {
                var e = Sqrt(d);
                roots.Add((-b + Sqrt(d)) / 2);
                roots.Add((-b - Sqrt(d)) / 2);
            }
            else if (d == 0)
            {
                roots.Add(-b / 2);
            }

            // calc the solution points
            for (var i = 0; i < roots.Count; i++)
            {
                var minX = Min(a1X, a2X);
                var minY = Min(a1Y, a2Y);
                var maxX = Max(a1X, a2X);
                var maxY = Max(a1Y, a2Y);
                var t = roots[i];
                if (t >= 0 && t <= 1)
                {
                    // possible point -- pending bounds check
                    var point = new Point2D(
                        Interpolaters.Linear(Interpolaters.Linear(p1X, p2X, t), Interpolaters.Linear(p2X, p3X, t), t),
                        Interpolaters.Linear(Interpolaters.Linear(p1Y, p2Y, t), Interpolaters.Linear(p2Y, p3Y, t), t));
                    var x = point.X;
                    var y = point.Y;
                    // bounds checks
                    if (a1X == a2X && y >= minY && y <= maxY)
                    {
                        // vertical line
                        intersections.Add(point);
                    }
                    else if (a1Y == a2Y && x >= minX && x <= maxX)
                    {
                        // horizontal line
                        intersections.Add(point);
                    }
                    else if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                    {
                        // line passed bounds check
                        intersections.Add(point);
                    }
                }
            }
            return intersections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1x"></param>
        /// <param name="a1y"></param>
        /// <param name="a2x"></param>
        /// <param name="a2y"></param>
        /// <param name="a3x"></param>
        /// <param name="a3y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <returns></returns>
        public static List<Point2D> QuadraticBezierQuadraticBezier(
            double a1x, double a1y,
            double a2x, double a2y,
            double a3x, double a3y,
            double b1x, double b1y,
            double b2x, double b2y,
            double b3x, double b3y) => intersectBezier2Bezier2(
                new Point2D(a1x, a1y), new Point2D(a2x, a2y), new Point2D(a3x, a3y),
                new Point2D(b1x, b1y), new Point2D(b2x, b2y), new Point2D(b3x, b3y));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier2Bezier2(
            Point2D a1, Point2D a2, Point2D a3,
            Point2D b1, Point2D b2, Point2D b3)
        {
            var result = new List<Point2D>(); //= new Intersection();
            Polynomial poly;

            var a = a2 * -2;
            var c12 = a1 + a + a3;

            a = a1 * -2;
            var b = a2 * 2;
            var c11 = a + b;

            var c10 = new Point2D(a1.X, a1.Y);

            a = b2 * -2;
            var c22 = b1 + a + b3;

            a = b1 * -2;
            b = b2 * 2;
            var c21 = a + b;

            var c20 = new Point2D(b1.X, b1.Y);

            if (c12.Y == 0)
            {
                var v0 = c12.X * (c10.Y - c20.Y);
                var v1 = v0 - c11.I * c11.J;
                var v2 = v0 + v1;
                var v3 = c11.J * c11.J;

                poly = new Polynomial(
                    c12.X * c22.Y * c22.Y,
                    2 * c12.X * c21.J * c22.Y,
                    c12.X * c21.J * c21.J - c22.X * v3 - c22.Y * v0 - c22.Y * v1,
                    -c21.I * v3 - c21.J * v0 - c21.J * v1,
                    (c10.X - c20.X) * v3 + (c10.Y - c20.Y) * v1
                );
            }
            else
            {
                var v0 = c12.X * c22.Y - c12.Y * c22.X;
                var v1 = c12.X * c21.J - c21.I * c12.Y;
                var v2 = c11.I * c12.Y - c11.J * c12.X;
                var v3 = c10.Y - c20.Y;
                var v4 = c12.Y * (c10.X - c20.X) - c12.X * v3;
                var v5 = -c11.J * v2 + c12.Y * v4;
                var v6 = v2 * v2;

                poly = new Polynomial(
                    v0 * v0,
                    2 * v0 * v1,
                    (-c22.Y * v6 + c12.Y * v1 * v1 + c12.Y * v0 * v4 + v0 * v5) / c12.Y,
                    (-c21.J * v6 + c12.Y * v1 * v4 + v1 * v5) / c12.Y,
                    (v3 * v6 + v4 * v5) / c12.Y
                );
            }

            var roots = poly.SolveRealRoots().ToArray();
            for (var i = 0; i < roots.Count(); i++)
            {
                var s = roots[i];

                if (0 <= s && s <= 1)
                {
                    var xRoots = new Polynomial(
                        c12.X,
                        c11.I,
                        c10.X - c20.X - s * c21.I - s * s * c22.X
                    ).SolveRealRoots().ToArray();
                    var yRoots = new Polynomial(
                        c12.Y,
                        c11.J,
                        c10.Y - c20.Y - s * c21.J - s * s * c22.Y
                    ).SolveRealRoots().ToArray();

                    if (xRoots.Length > 0 && yRoots.Length > 0)
                    {
                        var TOLERANCE = 1e-4;

                        for (var j = 0; j < xRoots.Length; j++)
                        {
                            var xRoot = xRoots[j];

                            if (0 <= xRoot && xRoot <= 1)
                            {
                                for (var k = 0; k < yRoots.Length; k++)
                                {
                                    if (Abs(xRoot - yRoots[k]) < TOLERANCE)
                                    {
                                        result.Add((Point2D)(c22 * (s * s) + (c21 * s + (c20))));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier2Bezier3(
            Point2D a1, Point2D a2, Point2D a3,
            Point2D b1, Point2D b2, Point2D b3, Point2D b4)
        {
            var result = new List<Point2D>();

            var a = a2 * -2;
            var c12 = a1 + a + a3;

            a = a1 * -2;
            var b = a2 * 2;
            var c11 = a + b;

            var c10 = new Point2D(a1.X, a1.Y);

            a = b1 * -1;
            b = b2 * 3;
            var c = b3 * -3;
            var d = a + b + c + b4;
            var c23 = new Vector2D(d.I, d.J);

            a = b1 * 3;
            b = b2 * -6;
            c = b3 * 3;
            d = a + b + c;
            var c22 = new Vector2D(d.I, d.J);

            a = b1 * -3;
            b = b2 * 3;
            c = (Point2D)(a + b);
            var c21 = new Vector2D(c.X, c.Y);

            var c20 = new Vector2D(b1.X, b1.Y);

            var c10x2 = c10.X * c10.X;
            var c10y2 = c10.Y * c10.Y;
            var c11x2 = c11.I * c11.I;
            var c11y2 = c11.J * c11.J;
            var c12x2 = c12.X * c12.X;
            var c12y2 = c12.Y * c12.Y;
            var c20x2 = c20.I * c20.I;
            var c20y2 = c20.J * c20.J;
            var c21x2 = c21.I * c21.I;
            var c21y2 = c21.J * c21.J;
            var c22x2 = c22.I * c22.I;
            var c22y2 = c22.J * c22.J;
            var c23x2 = c23.I * c23.I;
            var c23y2 = c23.J * c23.J;

            var poly = new Polynomial(
                -2 * c12.X * c12.Y * c23.I * c23.J + c12x2 * c23y2 + c12y2 * c23x2,
                -2 * c12.X * c12.Y * c22.I * c23.J - 2 * c12.X * c12.Y * c22.J * c23.I + 2 * c12y2 * c22.I * c23.I +
                    2 * c12x2 * c22.J * c23.J,
                -2 * c12.X * c21.I * c12.Y * c23.J - 2 * c12.X * c12.Y * c21.J * c23.I - 2 * c12.X * c12.Y * c22.I * c22.J +
                    2 * c21.I * c12y2 * c23.I + c12y2 * c22x2 + c12x2 * (2 * c21.J * c23.J + c22y2),
                2 * c10.X * c12.X * c12.Y * c23.J + 2 * c10.Y * c12.X * c12.Y * c23.I + c11.I * c11.J * c12.X * c23.J +
                    c11.I * c11.J * c12.Y * c23.I - 2 * c20.I * c12.X * c12.Y * c23.J - 2 * c12.X * c20.J * c12.Y * c23.I -
                    2 * c12.X * c21.I * c12.Y * c22.J - 2 * c12.X * c12.Y * c21.J * c22.I - 2 * c10.X * c12y2 * c23.I -
                    2 * c10.Y * c12x2 * c23.J + 2 * c20.I * c12y2 * c23.I + 2 * c21.I * c12y2 * c22.I -
                    c11y2 * c12.X * c23.I - c11x2 * c12.Y * c23.J + c12x2 * (2 * c20.J * c23.J + 2 * c21.J * c22.J),
                2 * c10.X * c12.X * c12.Y * c22.J + 2 * c10.Y * c12.X * c12.Y * c22.I + c11.I * c11.J * c12.X * c22.J +
                    c11.I * c11.J * c12.Y * c22.I - 2 * c20.I * c12.X * c12.Y * c22.J - 2 * c12.X * c20.J * c12.Y * c22.I -
                    2 * c12.X * c21.I * c12.Y * c21.J - 2 * c10.X * c12y2 * c22.I - 2 * c10.Y * c12x2 * c22.J +
                    2 * c20.I * c12y2 * c22.I - c11y2 * c12.X * c22.I - c11x2 * c12.Y * c22.J + c21x2 * c12y2 +
                    c12x2 * (2 * c20.J * c22.J + c21y2),
                2 * c10.X * c12.X * c12.Y * c21.J + 2 * c10.Y * c12.X * c21.I * c12.Y + c11.I * c11.J * c12.X * c21.J +
                    c11.I * c11.J * c21.I * c12.Y - 2 * c20.I * c12.X * c12.Y * c21.J - 2 * c12.X * c20.J * c21.I * c12.Y -
                    2 * c10.X * c21.I * c12y2 - 2 * c10.Y * c12x2 * c21.J + 2 * c20.I * c21.I * c12y2 -
                    c11y2 * c12.X * c21.I - c11x2 * c12.Y * c21.J + 2 * c12x2 * c20.J * c21.J,
                -2 * c10.X * c10.Y * c12.X * c12.Y - c10.X * c11.I * c11.J * c12.Y - c10.Y * c11.I * c11.J * c12.X +
                    2 * c10.X * c12.X * c20.J * c12.Y + 2 * c10.Y * c20.I * c12.X * c12.Y + c11.I * c20.I * c11.J * c12.Y +
                    c11.I * c11.J * c12.X * c20.J - 2 * c20.I * c12.X * c20.J * c12.Y - 2 * c10.X * c20.I * c12y2 +
                    c10.X * c11y2 * c12.X + c10.Y * c11x2 * c12.Y - 2 * c10.Y * c12x2 * c20.J -
                    c20.I * c11y2 * c12.X - c11x2 * c20.J * c12.Y + c10x2 * c12y2 + c10y2 * c12x2 +
                    c20x2 * c12y2 + c12x2 * c20y2
            );
            var roots = poly.SolveRealRoots(0, 1).ToArray();
            //Intersection.Utils.removeMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Length; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c12.X,
                    c11.I,
                    c10.X - c20.I - s * c21.I - s * s * c22.I - s * s * s * c23.I
                ).SolveRealRoots().ToArray();
                var yRoots = new Polynomial(
                    c12.Y,
                    c11.J,
                    c10.Y - c20.J - s * c21.J - s * s * c22.J - s * s * s * c23.J
                ).SolveRealRoots().ToArray();

                if (xRoots.Length > 0 && yRoots.Length > 0)
                {
                    var TOLERANCE = 1e-4;

                    //checkRoots:
                    for (var j = 0; j < xRoots.Length; j++)
                    {
                        var xRoot = xRoots[j];

                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Length; k++)
                            {
                                if (Abs(xRoot - yRoots[k]) < TOLERANCE)
                                {
                                    var v = c23 * (s * s * s) + (c22 * (s * s) + (c21 * (s) + (c20)));
                                    result.Add(new Point2D(v.I, v.J));
                                    break;// checkRoots;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier2Ellipse(
            Point2D p1, Point2D p2, Point2D p3,
            Point2D ec, double rx, double ry)
        {
            var result = new List<Point2D>();

            var a = p2 * -2;
            var c2 = p1 + a + p3;

            a = p1 * -2;
            var b = p2 * 2;
            var c1 = a + b;

            var c0 = new Point2D(p1.X, p1.Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;
            var roots = new Polynomial(
                ryry * c2.X * c2.X + rxrx * c2.Y * c2.Y,
                2 * (ryry * c2.X * c1.I + rxrx * c2.Y * c1.J),
                ryry * (2 * c2.X * c0.X + c1.I * c1.I) + rxrx * (2 * c2.Y * c0.Y + c1.J * c1.J) -
                    2 * (ryry * ec.X * c2.X + rxrx * ec.Y * c2.Y),
                2 * (ryry * c1.I * (c0.X - ec.X) + rxrx * c1.J * (c0.Y - ec.Y)),
                ryry * (c0.X * c0.X + ec.X * ec.X) + rxrx * (c0.Y * c0.Y + ec.Y * ec.Y) -
                    2 * (ryry * ec.X * c0.X + rxrx * ec.Y * c0.Y) - rxrx * ryry
            ).SolveRealRoots().ToArray();

            for (var i = 0; i < roots.Length; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                    result.Add((Point2D)(c2 * t * t + (c1 * t + c0)));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier2Line(
            Point2D p1, Point2D p2, Point2D p3,
            Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new List<Point2D>();

            var a = p2 * -2;
            var c2 = p1 + a + p3;

            a = p1 * -2;
            var b = p2 * 2;
            var c1 = a + b;

            var c0 = new Point2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // Transform cubic coefficients to line's coordinate system and find roots
            // of cubic
            var roots = new Polynomial(
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl
            ).SolveRealRoots().ToArray();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Length; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p4 = Interpolaters.Linear(p1, p2, t);
                    var p5 = Interpolaters.Linear(p2, p3, t);
                    var p6 = Interpolaters.Linear(p4, p5, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p6
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.Add(p6);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.Add(p6);
                        }
                    }
                    else if (min.X <= p6.X && p6.X <= max.X && min.Y <= p6.Y && p6.Y <= max.Y)
                    {
                        result.Add(p6);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="a4"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier3Bezier3(
            Point2D a1, Point2D a2, Point2D a3, Point2D a4,
            Point2D b1, Point2D b2, Point2D b3, Point2D b4)
        {
            var result = new List<Point2D>();

            // Calculate the coefficients of cubic polynomial
            var a = a1 * -1;
            var b = a2 * 3;
            var c = a3 * -3;
            var d = a + b + c + a4;
            var c13 = new Point2D(d.I, d.J);

            a = a1 * 3;
            b = a2 * -6;
            c = a3 * 3;
            d = a + b + c;
            var c12 = new Point2D(d.I, d.J);

            a = a1 * -3;
            b = a2 * 3;
            c = (Point2D)(a + b);
            var c11 = new Point2D(c.X, c.Y);

            var c10 = new Point2D(a1.X, a1.Y);

            a = b1 * -1;
            b = b2 * 3;
            c = b3 * -3;
            d = a + b + c + b4;
            var c23 = new Point2D(d.I, d.J);

            a = b1 * 3;
            b = b2 * -6;
            c = b3 * 3;
            d = a + b + c;
            var c22 = new Point2D(d.I, d.J);

            a = b1 * -3;
            b = b2 * 3;
            c = (Point2D)(a + b);
            var c21 = new Point2D(c.X, c.Y);

            var c20 = new Point2D(b1.X, b1.Y);

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
                -c13x3 * c23y3 + c13y3 * c23x3 - 3 * c13.X * c13y2 * c23x2 * c23.Y +
                    3 * c13x2 * c13.Y * c23.X * c23y2,
                -6 * c13.X * c22.X * c13y2 * c23.X * c23.Y + 6 * c13x2 * c13.Y * c22.Y * c23.X * c23.Y + 3 * c22.X * c13y3 * c23x2 -
                    3 * c13x3 * c22.Y * c23y2 - 3 * c13.X * c13y2 * c22.Y * c23x2 + 3 * c13x2 * c22.X * c13.Y * c23y2,
                -6 * c21.X * c13.X * c13y2 * c23.X * c23.Y - 6 * c13.X * c22.X * c13y2 * c22.Y * c23.X + 6 * c13x2 * c22.X * c13.Y * c22.Y * c23.Y +
                    3 * c21.X * c13y3 * c23x2 + 3 * c22x2 * c13y3 * c23.X + 3 * c21.X * c13x2 * c13.Y * c23y2 - 3 * c13.X * c21.Y * c13y2 * c23x2 -
                    3 * c13.X * c22x2 * c13y2 * c23.Y + c13x2 * c13.Y * c23.X * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-c21.Y * c23y2 -
                    2 * c22y2 * c23.Y - c23.Y * (2 * c21.Y * c23.Y + c22y2)),
                c11.X * c12.Y * c13.X * c13.Y * c23.X * c23.Y - c11.Y * c12.X * c13.X * c13.Y * c23.X * c23.Y + 6 * c21.X * c22.X * c13y3 * c23.X +
                    3 * c11.X * c12.X * c13.X * c13.Y * c23y2 + 6 * c10.X * c13.X * c13y2 * c23.X * c23.Y - 3 * c11.X * c12.X * c13y2 * c23.X * c23.Y -
                    3 * c11.Y * c12.Y * c13.X * c13.Y * c23x2 - 6 * c10.Y * c13x2 * c13.Y * c23.X * c23.Y - 6 * c20.X * c13.X * c13y2 * c23.X * c23.Y +
                    3 * c11.Y * c12.Y * c13x2 * c23.X * c23.Y - 2 * c12.X * c12y2 * c13.X * c23.X * c23.Y - 6 * c21.X * c13.X * c22.X * c13y2 * c23.Y -
                    6 * c21.X * c13.X * c13y2 * c22.Y * c23.X - 6 * c13.X * c21.Y * c22.X * c13y2 * c23.X + 6 * c21.X * c13x2 * c13.Y * c22.Y * c23.Y +
                    2 * c12x2 * c12.Y * c13.Y * c23.X * c23.Y + c22x3 * c13y3 - 3 * c10.X * c13y3 * c23x2 + 3 * c10.Y * c13x3 * c23y2 +
                    3 * c20.X * c13y3 * c23x2 + c12y3 * c13.X * c23x2 - c12x3 * c13.Y * c23y2 - 3 * c10.X * c13x2 * c13.Y * c23y2 +
                    3 * c10.Y * c13.X * c13y2 * c23x2 - 2 * c11.X * c12.Y * c13x2 * c23y2 + c11.X * c12.Y * c13y2 * c23x2 - c11.Y * c12.X * c13x2 * c23y2 +
                    2 * c11.Y * c12.X * c13y2 * c23x2 + 3 * c20.X * c13x2 * c13.Y * c23y2 - c12.X * c12y2 * c13.Y * c23x2 -
                    3 * c20.Y * c13.X * c13y2 * c23x2 + c12x2 * c12.Y * c13.X * c23y2 - 3 * c13.X * c22x2 * c13y2 * c22.Y +
                    c13x2 * c13.Y * c23.X * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c13x2 * c22.X * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) +
                    c13x3 * (-2 * c21.Y * c22.Y * c23.Y - c20.Y * c23y2 - c22.Y * (2 * c21.Y * c23.Y + c22y2) - c23.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                6 * c11.X * c12.X * c13.X * c13.Y * c22.Y * c23.Y + c11.X * c12.Y * c13.X * c22.X * c13.Y * c23.Y + c11.X * c12.Y * c13.X * c13.Y * c22.Y * c23.X -
                    c11.Y * c12.X * c13.X * c22.X * c13.Y * c23.Y - c11.Y * c12.X * c13.X * c13.Y * c22.Y * c23.X - 6 * c11.Y * c12.Y * c13.X * c22.X * c13.Y * c23.X -
                    6 * c10.X * c22.X * c13y3 * c23.X + 6 * c20.X * c22.X * c13y3 * c23.X + 6 * c10.Y * c13x3 * c22.Y * c23.Y + 2 * c12y3 * c13.X * c22.X * c23.X -
                    2 * c12x3 * c13.Y * c22.Y * c23.Y + 6 * c10.X * c13.X * c22.X * c13y2 * c23.Y + 6 * c10.X * c13.X * c13y2 * c22.Y * c23.X +
                    6 * c10.Y * c13.X * c22.X * c13y2 * c23.X - 3 * c11.X * c12.X * c22.X * c13y2 * c23.Y - 3 * c11.X * c12.X * c13y2 * c22.Y * c23.X +
                    2 * c11.X * c12.Y * c22.X * c13y2 * c23.X + 4 * c11.Y * c12.X * c22.X * c13y2 * c23.X - 6 * c10.X * c13x2 * c13.Y * c22.Y * c23.Y -
                    6 * c10.Y * c13x2 * c22.X * c13.Y * c23.Y - 6 * c10.Y * c13x2 * c13.Y * c22.Y * c23.X - 4 * c11.X * c12.Y * c13x2 * c22.Y * c23.Y -
                    6 * c20.X * c13.X * c22.X * c13y2 * c23.Y - 6 * c20.X * c13.X * c13y2 * c22.Y * c23.X - 2 * c11.Y * c12.X * c13x2 * c22.Y * c23.Y +
                    3 * c11.Y * c12.Y * c13x2 * c22.X * c23.Y + 3 * c11.Y * c12.Y * c13x2 * c22.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c22.X * c23.Y -
                    2 * c12.X * c12y2 * c13.X * c22.Y * c23.X - 2 * c12.X * c12y2 * c22.X * c13.Y * c23.X - 6 * c20.Y * c13.X * c22.X * c13y2 * c23.X -
                    6 * c21.X * c13.X * c21.Y * c13y2 * c23.X - 6 * c21.X * c13.X * c22.X * c13y2 * c22.Y + 6 * c20.X * c13x2 * c13.Y * c22.Y * c23.Y +
                    2 * c12x2 * c12.Y * c13.X * c22.Y * c23.Y + 2 * c12x2 * c12.Y * c22.X * c13.Y * c23.Y + 2 * c12x2 * c12.Y * c13.Y * c22.Y * c23.X +
                    3 * c21.X * c22x2 * c13y3 + 3 * c21x2 * c13y3 * c23.X - 3 * c13.X * c21.Y * c22x2 * c13y2 - 3 * c21x2 * c13.X * c13y2 * c23.Y +
                    c13x2 * c22.X * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c13x2 * c13.Y * c23.X * (6 * c20.Y * c22.Y + 3 * c21y2) +
                    c21.X * c13x2 * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) + c13x3 * (-2 * c20.Y * c22.Y * c23.Y - c23.Y * (2 * c20.Y * c22.Y + c21y2) -
                    c21.Y * (2 * c21.Y * c23.Y + c22y2) - c22.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                c11.X * c21.X * c12.Y * c13.X * c13.Y * c23.Y + c11.X * c12.Y * c13.X * c21.Y * c13.Y * c23.X + c11.X * c12.Y * c13.X * c22.X * c13.Y * c22.Y -
                    c11.Y * c12.X * c21.X * c13.X * c13.Y * c23.Y - c11.Y * c12.X * c13.X * c21.Y * c13.Y * c23.X - c11.Y * c12.X * c13.X * c22.X * c13.Y * c22.Y -
                    6 * c11.Y * c21.X * c12.Y * c13.X * c13.Y * c23.X - 6 * c10.X * c21.X * c13y3 * c23.X + 6 * c20.X * c21.X * c13y3 * c23.X +
                    2 * c21.X * c12y3 * c13.X * c23.X + 6 * c10.X * c21.X * c13.X * c13y2 * c23.Y + 6 * c10.X * c13.X * c21.Y * c13y2 * c23.X +
                    6 * c10.X * c13.X * c22.X * c13y2 * c22.Y + 6 * c10.Y * c21.X * c13.X * c13y2 * c23.X - 3 * c11.X * c12.X * c21.X * c13y2 * c23.Y -
                    3 * c11.X * c12.X * c21.Y * c13y2 * c23.X - 3 * c11.X * c12.X * c22.X * c13y2 * c22.Y + 2 * c11.X * c21.X * c12.Y * c13y2 * c23.X +
                    4 * c11.Y * c12.X * c21.X * c13y2 * c23.X - 6 * c10.Y * c21.X * c13x2 * c13.Y * c23.Y - 6 * c10.Y * c13x2 * c21.Y * c13.Y * c23.X -
                    6 * c10.Y * c13x2 * c22.X * c13.Y * c22.Y - 6 * c20.X * c21.X * c13.X * c13y2 * c23.Y - 6 * c20.X * c13.X * c21.Y * c13y2 * c23.X -
                    6 * c20.X * c13.X * c22.X * c13y2 * c22.Y + 3 * c11.Y * c21.X * c12.Y * c13x2 * c23.Y - 3 * c11.Y * c12.Y * c13.X * c22x2 * c13.Y +
                    3 * c11.Y * c12.Y * c13x2 * c21.Y * c23.X + 3 * c11.Y * c12.Y * c13x2 * c22.X * c22.Y - 2 * c12.X * c21.X * c12y2 * c13.X * c23.Y -
                    2 * c12.X * c21.X * c12y2 * c13.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c21.Y * c23.X - 2 * c12.X * c12y2 * c13.X * c22.X * c22.Y -
                    6 * c20.Y * c21.X * c13.X * c13y2 * c23.X - 6 * c21.X * c13.X * c21.Y * c22.X * c13y2 + 6 * c20.Y * c13x2 * c21.Y * c13.Y * c23.X +
                    2 * c12x2 * c21.X * c12.Y * c13.Y * c23.Y + 2 * c12x2 * c12.Y * c21.Y * c13.Y * c23.X + 2 * c12x2 * c12.Y * c22.X * c13.Y * c22.Y -
                    3 * c10.X * c22x2 * c13y3 + 3 * c20.X * c22x2 * c13y3 + 3 * c21x2 * c22.X * c13y3 + c12y3 * c13.X * c22x2 +
                    3 * c10.Y * c13.X * c22x2 * c13y2 + c11.X * c12.Y * c22x2 * c13y2 + 2 * c11.Y * c12.X * c22x2 * c13y2 -
                    c12.X * c12y2 * c22x2 * c13.Y - 3 * c20.Y * c13.X * c22x2 * c13y2 - 3 * c21x2 * c13.X * c13y2 * c22.Y +
                    c12x2 * c12.Y * c13.X * (2 * c21.Y * c23.Y + c22y2) + c11.X * c12.X * c13.X * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) +
                    c21.X * c13x2 * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c12x3 * c13.Y * (-2 * c21.Y * c23.Y - c22y2) +
                    c10.Y * c13x3 * (6 * c21.Y * c23.Y + 3 * c22y2) + c11.Y * c12.X * c13x2 * (-2 * c21.Y * c23.Y - c22y2) +
                    c11.X * c12.Y * c13x2 * (-4 * c21.Y * c23.Y - 2 * c22y2) + c10.X * c13x2 * c13.Y * (-6 * c21.Y * c23.Y - 3 * c22y2) +
                    c13x2 * c22.X * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c20.X * c13x2 * c13.Y * (6 * c21.Y * c23.Y + 3 * c22y2) +
                    c13x3 * (-2 * c20.Y * c21.Y * c23.Y - c22.Y * (2 * c20.Y * c22.Y + c21y2) - c20.Y * (2 * c21.Y * c23.Y + c22y2) -
                    c21.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                -c10.X * c11.X * c12.Y * c13.X * c13.Y * c23.Y + c10.X * c11.Y * c12.X * c13.X * c13.Y * c23.Y + 6 * c10.X * c11.Y * c12.Y * c13.X * c13.Y * c23.X -
                    6 * c10.Y * c11.X * c12.X * c13.X * c13.Y * c23.Y - c10.Y * c11.X * c12.Y * c13.X * c13.Y * c23.X + c10.Y * c11.Y * c12.X * c13.X * c13.Y * c23.X +
                    c11.X * c11.Y * c12.X * c12.Y * c13.X * c23.Y - c11.X * c11.Y * c12.X * c12.Y * c13.Y * c23.X + c11.X * c20.X * c12.Y * c13.X * c13.Y * c23.Y +
                    c11.X * c20.Y * c12.Y * c13.X * c13.Y * c23.X + c11.X * c21.X * c12.Y * c13.X * c13.Y * c22.Y + c11.X * c12.Y * c13.X * c21.Y * c22.X * c13.Y -
                    c20.X * c11.Y * c12.X * c13.X * c13.Y * c23.Y - 6 * c20.X * c11.Y * c12.Y * c13.X * c13.Y * c23.X - c11.Y * c12.X * c20.Y * c13.X * c13.Y * c23.X -
                    c11.Y * c12.X * c21.X * c13.X * c13.Y * c22.Y - c11.Y * c12.X * c13.X * c21.Y * c22.X * c13.Y - 6 * c11.Y * c21.X * c12.Y * c13.X * c22.X * c13.Y -
                    6 * c10.X * c20.X * c13y3 * c23.X - 6 * c10.X * c21.X * c22.X * c13y3 - 2 * c10.X * c12y3 * c13.X * c23.X + 6 * c20.X * c21.X * c22.X * c13y3 +
                    2 * c20.X * c12y3 * c13.X * c23.X + 2 * c21.X * c12y3 * c13.X * c22.X + 2 * c10.Y * c12x3 * c13.Y * c23.Y - 6 * c10.X * c10.Y * c13.X * c13y2 * c23.X +
                    3 * c10.X * c11.X * c12.X * c13y2 * c23.Y - 2 * c10.X * c11.X * c12.Y * c13y2 * c23.X - 4 * c10.X * c11.Y * c12.X * c13y2 * c23.X +
                    3 * c10.Y * c11.X * c12.X * c13y2 * c23.X + 6 * c10.X * c10.Y * c13x2 * c13.Y * c23.Y + 6 * c10.X * c20.X * c13.X * c13y2 * c23.Y -
                    3 * c10.X * c11.Y * c12.Y * c13x2 * c23.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c23.Y + 2 * c10.X * c12.X * c12y2 * c13.Y * c23.X +
                    6 * c10.X * c20.Y * c13.X * c13y2 * c23.X + 6 * c10.X * c21.X * c13.X * c13y2 * c22.Y + 6 * c10.X * c13.X * c21.Y * c22.X * c13y2 +
                    4 * c10.Y * c11.X * c12.Y * c13x2 * c23.Y + 6 * c10.Y * c20.X * c13.X * c13y2 * c23.X + 2 * c10.Y * c11.Y * c12.X * c13x2 * c23.Y -
                    3 * c10.Y * c11.Y * c12.Y * c13x2 * c23.X + 2 * c10.Y * c12.X * c12y2 * c13.X * c23.X + 6 * c10.Y * c21.X * c13.X * c22.X * c13y2 -
                    3 * c11.X * c20.X * c12.X * c13y2 * c23.Y + 2 * c11.X * c20.X * c12.Y * c13y2 * c23.X + c11.X * c11.Y * c12y2 * c13.X * c23.X -
                    3 * c11.X * c12.X * c20.Y * c13y2 * c23.X - 3 * c11.X * c12.X * c21.X * c13y2 * c22.Y - 3 * c11.X * c12.X * c21.Y * c22.X * c13y2 +
                    2 * c11.X * c21.X * c12.Y * c22.X * c13y2 + 4 * c20.X * c11.Y * c12.X * c13y2 * c23.X + 4 * c11.Y * c12.X * c21.X * c22.X * c13y2 -
                    2 * c10.X * c12x2 * c12.Y * c13.Y * c23.Y - 6 * c10.Y * c20.X * c13x2 * c13.Y * c23.Y - 6 * c10.Y * c20.Y * c13x2 * c13.Y * c23.X -
                    6 * c10.Y * c21.X * c13x2 * c13.Y * c22.Y - 2 * c10.Y * c12x2 * c12.Y * c13.X * c23.Y - 2 * c10.Y * c12x2 * c12.Y * c13.Y * c23.X -
                    6 * c10.Y * c13x2 * c21.Y * c22.X * c13.Y - c11.X * c11.Y * c12x2 * c13.Y * c23.Y - 2 * c11.X * c11y2 * c13.X * c13.Y * c23.X +
                    3 * c20.X * c11.Y * c12.Y * c13x2 * c23.Y - 2 * c20.X * c12.X * c12y2 * c13.X * c23.Y - 2 * c20.X * c12.X * c12y2 * c13.Y * c23.X -
                    6 * c20.X * c20.Y * c13.X * c13y2 * c23.X - 6 * c20.X * c21.X * c13.X * c13y2 * c22.Y - 6 * c20.X * c13.X * c21.Y * c22.X * c13y2 +
                    3 * c11.Y * c20.Y * c12.Y * c13x2 * c23.X + 3 * c11.Y * c21.X * c12.Y * c13x2 * c22.Y + 3 * c11.Y * c12.Y * c13x2 * c21.Y * c22.X -
                    2 * c12.X * c20.Y * c12y2 * c13.X * c23.X - 2 * c12.X * c21.X * c12y2 * c13.X * c22.Y - 2 * c12.X * c21.X * c12y2 * c22.X * c13.Y -
                    2 * c12.X * c12y2 * c13.X * c21.Y * c22.X - 6 * c20.Y * c21.X * c13.X * c22.X * c13y2 - c11y2 * c12.X * c12.Y * c13.X * c23.X +
                    2 * c20.X * c12x2 * c12.Y * c13.Y * c23.Y + 6 * c20.Y * c13x2 * c21.Y * c22.X * c13.Y + 2 * c11x2 * c11.Y * c13.X * c13.Y * c23.Y +
                    c11x2 * c12.X * c12.Y * c13.Y * c23.Y + 2 * c12x2 * c20.Y * c12.Y * c13.Y * c23.X + 2 * c12x2 * c21.X * c12.Y * c13.Y * c22.Y +
                    2 * c12x2 * c12.Y * c21.Y * c22.X * c13.Y + c21x3 * c13y3 + 3 * c10x2 * c13y3 * c23.X - 3 * c10y2 * c13x3 * c23.Y +
                    3 * c20x2 * c13y3 * c23.X + c11y3 * c13x2 * c23.X - c11x3 * c13y2 * c23.Y - c11.X * c11y2 * c13x2 * c23.Y +
                    c11x2 * c11.Y * c13y2 * c23.X - 3 * c10x2 * c13.X * c13y2 * c23.Y + 3 * c10y2 * c13x2 * c13.Y * c23.X - c11x2 * c12y2 * c13.X * c23.Y +
                    c11y2 * c12x2 * c13.Y * c23.X - 3 * c21x2 * c13.X * c21.Y * c13y2 - 3 * c20x2 * c13.X * c13y2 * c23.Y + 3 * c20y2 * c13x2 * c13.Y * c23.X +
                    c11.X * c12.X * c13.X * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c12x3 * c13.Y * (-2 * c20.Y * c23.Y - 2 * c21.Y * c22.Y) +
                    c10.Y * c13x3 * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) + c11.Y * c12.X * c13x2 * (-2 * c20.Y * c23.Y - 2 * c21.Y * c22.Y) +
                    c12x2 * c12.Y * c13.X * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y) + c11.X * c12.Y * c13x2 * (-4 * c20.Y * c23.Y - 4 * c21.Y * c22.Y) +
                    c10.X * c13x2 * c13.Y * (-6 * c20.Y * c23.Y - 6 * c21.Y * c22.Y) + c20.X * c13x2 * c13.Y * (6 * c20.Y * c23.Y + 6 * c21.Y * c22.Y) +
                    c21.X * c13x2 * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) + c13x3 * (-2 * c20.Y * c21.Y * c22.Y - c20y2 * c23.Y -
                    c21.Y * (2 * c20.Y * c22.Y + c21y2) - c20.Y * (2 * c20.Y * c23.Y + 2 * c21.Y * c22.Y)),
                -c10.X * c11.X * c12.Y * c13.X * c13.Y * c22.Y + c10.X * c11.Y * c12.X * c13.X * c13.Y * c22.Y + 6 * c10.X * c11.Y * c12.Y * c13.X * c22.X * c13.Y -
                    6 * c10.Y * c11.X * c12.X * c13.X * c13.Y * c22.Y - c10.Y * c11.X * c12.Y * c13.X * c22.X * c13.Y + c10.Y * c11.Y * c12.X * c13.X * c22.X * c13.Y +
                    c11.X * c11.Y * c12.X * c12.Y * c13.X * c22.Y - c11.X * c11.Y * c12.X * c12.Y * c22.X * c13.Y + c11.X * c20.X * c12.Y * c13.X * c13.Y * c22.Y +
                    c11.X * c20.Y * c12.Y * c13.X * c22.X * c13.Y + c11.X * c21.X * c12.Y * c13.X * c21.Y * c13.Y - c20.X * c11.Y * c12.X * c13.X * c13.Y * c22.Y -
                    6 * c20.X * c11.Y * c12.Y * c13.X * c22.X * c13.Y - c11.Y * c12.X * c20.Y * c13.X * c22.X * c13.Y - c11.Y * c12.X * c21.X * c13.X * c21.Y * c13.Y -
                    6 * c10.X * c20.X * c22.X * c13y3 - 2 * c10.X * c12y3 * c13.X * c22.X + 2 * c20.X * c12y3 * c13.X * c22.X + 2 * c10.Y * c12x3 * c13.Y * c22.Y -
                    6 * c10.X * c10.Y * c13.X * c22.X * c13y2 + 3 * c10.X * c11.X * c12.X * c13y2 * c22.Y - 2 * c10.X * c11.X * c12.Y * c22.X * c13y2 -
                    4 * c10.X * c11.Y * c12.X * c22.X * c13y2 + 3 * c10.Y * c11.X * c12.X * c22.X * c13y2 + 6 * c10.X * c10.Y * c13x2 * c13.Y * c22.Y +
                    6 * c10.X * c20.X * c13.X * c13y2 * c22.Y - 3 * c10.X * c11.Y * c12.Y * c13x2 * c22.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c22.Y +
                    2 * c10.X * c12.X * c12y2 * c22.X * c13.Y + 6 * c10.X * c20.Y * c13.X * c22.X * c13y2 + 6 * c10.X * c21.X * c13.X * c21.Y * c13y2 +
                    4 * c10.Y * c11.X * c12.Y * c13x2 * c22.Y + 6 * c10.Y * c20.X * c13.X * c22.X * c13y2 + 2 * c10.Y * c11.Y * c12.X * c13x2 * c22.Y -
                    3 * c10.Y * c11.Y * c12.Y * c13x2 * c22.X + 2 * c10.Y * c12.X * c12y2 * c13.X * c22.X - 3 * c11.X * c20.X * c12.X * c13y2 * c22.Y +
                    2 * c11.X * c20.X * c12.Y * c22.X * c13y2 + c11.X * c11.Y * c12y2 * c13.X * c22.X - 3 * c11.X * c12.X * c20.Y * c22.X * c13y2 -
                    3 * c11.X * c12.X * c21.X * c21.Y * c13y2 + 4 * c20.X * c11.Y * c12.X * c22.X * c13y2 - 2 * c10.X * c12x2 * c12.Y * c13.Y * c22.Y -
                    6 * c10.Y * c20.X * c13x2 * c13.Y * c22.Y - 6 * c10.Y * c20.Y * c13x2 * c22.X * c13.Y - 6 * c10.Y * c21.X * c13x2 * c21.Y * c13.Y -
                    2 * c10.Y * c12x2 * c12.Y * c13.X * c22.Y - 2 * c10.Y * c12x2 * c12.Y * c22.X * c13.Y - c11.X * c11.Y * c12x2 * c13.Y * c22.Y -
                    2 * c11.X * c11y2 * c13.X * c22.X * c13.Y + 3 * c20.X * c11.Y * c12.Y * c13x2 * c22.Y - 2 * c20.X * c12.X * c12y2 * c13.X * c22.Y -
                    2 * c20.X * c12.X * c12y2 * c22.X * c13.Y - 6 * c20.X * c20.Y * c13.X * c22.X * c13y2 - 6 * c20.X * c21.X * c13.X * c21.Y * c13y2 +
                    3 * c11.Y * c20.Y * c12.Y * c13x2 * c22.X + 3 * c11.Y * c21.X * c12.Y * c13x2 * c21.Y - 2 * c12.X * c20.Y * c12y2 * c13.X * c22.X -
                    2 * c12.X * c21.X * c12y2 * c13.X * c21.Y - c11y2 * c12.X * c12.Y * c13.X * c22.X + 2 * c20.X * c12x2 * c12.Y * c13.Y * c22.Y -
                    3 * c11.Y * c21x2 * c12.Y * c13.X * c13.Y + 6 * c20.Y * c21.X * c13x2 * c21.Y * c13.Y + 2 * c11x2 * c11.Y * c13.X * c13.Y * c22.Y +
                    c11x2 * c12.X * c12.Y * c13.Y * c22.Y + 2 * c12x2 * c20.Y * c12.Y * c22.X * c13.Y + 2 * c12x2 * c21.X * c12.Y * c21.Y * c13.Y -
                    3 * c10.X * c21x2 * c13y3 + 3 * c20.X * c21x2 * c13y3 + 3 * c10x2 * c22.X * c13y3 - 3 * c10y2 * c13x3 * c22.Y + 3 * c20x2 * c22.X * c13y3 +
                    c21x2 * c12y3 * c13.X + c11y3 * c13x2 * c22.X - c11x3 * c13y2 * c22.Y + 3 * c10.Y * c21x2 * c13.X * c13y2 -
                    c11.X * c11y2 * c13x2 * c22.Y + c11.X * c21x2 * c12.Y * c13y2 + 2 * c11.Y * c12.X * c21x2 * c13y2 + c11x2 * c11.Y * c22.X * c13y2 -
                    c12.X * c21x2 * c12y2 * c13.Y - 3 * c20.Y * c21x2 * c13.X * c13y2 - 3 * c10x2 * c13.X * c13y2 * c22.Y + 3 * c10y2 * c13x2 * c22.X * c13.Y -
                    c11x2 * c12y2 * c13.X * c22.Y + c11y2 * c12x2 * c22.X * c13.Y - 3 * c20x2 * c13.X * c13y2 * c22.Y + 3 * c20y2 * c13x2 * c22.X * c13.Y +
                    c12x2 * c12.Y * c13.X * (2 * c20.Y * c22.Y + c21y2) + c11.X * c12.X * c13.X * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) +
                    c12x3 * c13.Y * (-2 * c20.Y * c22.Y - c21y2) + c10.Y * c13x3 * (6 * c20.Y * c22.Y + 3 * c21y2) +
                    c11.Y * c12.X * c13x2 * (-2 * c20.Y * c22.Y - c21y2) + c11.X * c12.Y * c13x2 * (-4 * c20.Y * c22.Y - 2 * c21y2) +
                    c10.X * c13x2 * c13.Y * (-6 * c20.Y * c22.Y - 3 * c21y2) + c20.X * c13x2 * c13.Y * (6 * c20.Y * c22.Y + 3 * c21y2) +
                    c13x3 * (-2 * c20.Y * c21y2 - c20y2 * c22.Y - c20.Y * (2 * c20.Y * c22.Y + c21y2)),
                -c10.X * c11.X * c12.Y * c13.X * c21.Y * c13.Y + c10.X * c11.Y * c12.X * c13.X * c21.Y * c13.Y + 6 * c10.X * c11.Y * c21.X * c12.Y * c13.X * c13.Y -
                    6 * c10.Y * c11.X * c12.X * c13.X * c21.Y * c13.Y - c10.Y * c11.X * c21.X * c12.Y * c13.X * c13.Y + c10.Y * c11.Y * c12.X * c21.X * c13.X * c13.Y -
                    c11.X * c11.Y * c12.X * c21.X * c12.Y * c13.Y + c11.X * c11.Y * c12.X * c12.Y * c13.X * c21.Y + c11.X * c20.X * c12.Y * c13.X * c21.Y * c13.Y +
                    6 * c11.X * c12.X * c20.Y * c13.X * c21.Y * c13.Y + c11.X * c20.Y * c21.X * c12.Y * c13.X * c13.Y - c20.X * c11.Y * c12.X * c13.X * c21.Y * c13.Y -
                    6 * c20.X * c11.Y * c21.X * c12.Y * c13.X * c13.Y - c11.Y * c12.X * c20.Y * c21.X * c13.X * c13.Y - 6 * c10.X * c20.X * c21.X * c13y3 -
                    2 * c10.X * c21.X * c12y3 * c13.X + 6 * c10.Y * c20.Y * c13x3 * c21.Y + 2 * c20.X * c21.X * c12y3 * c13.X + 2 * c10.Y * c12x3 * c21.Y * c13.Y -
                    2 * c12x3 * c20.Y * c21.Y * c13.Y - 6 * c10.X * c10.Y * c21.X * c13.X * c13y2 + 3 * c10.X * c11.X * c12.X * c21.Y * c13y2 -
                    2 * c10.X * c11.X * c21.X * c12.Y * c13y2 - 4 * c10.X * c11.Y * c12.X * c21.X * c13y2 + 3 * c10.Y * c11.X * c12.X * c21.X * c13y2 +
                    6 * c10.X * c10.Y * c13x2 * c21.Y * c13.Y + 6 * c10.X * c20.X * c13.X * c21.Y * c13y2 - 3 * c10.X * c11.Y * c12.Y * c13x2 * c21.Y +
                    2 * c10.X * c12.X * c21.X * c12y2 * c13.Y + 2 * c10.X * c12.X * c12y2 * c13.X * c21.Y + 6 * c10.X * c20.Y * c21.X * c13.X * c13y2 +
                    4 * c10.Y * c11.X * c12.Y * c13x2 * c21.Y + 6 * c10.Y * c20.X * c21.X * c13.X * c13y2 + 2 * c10.Y * c11.Y * c12.X * c13x2 * c21.Y -
                    3 * c10.Y * c11.Y * c21.X * c12.Y * c13x2 + 2 * c10.Y * c12.X * c21.X * c12y2 * c13.X - 3 * c11.X * c20.X * c12.X * c21.Y * c13y2 +
                    2 * c11.X * c20.X * c21.X * c12.Y * c13y2 + c11.X * c11.Y * c21.X * c12y2 * c13.X - 3 * c11.X * c12.X * c20.Y * c21.X * c13y2 +
                    4 * c20.X * c11.Y * c12.X * c21.X * c13y2 - 6 * c10.X * c20.Y * c13x2 * c21.Y * c13.Y - 2 * c10.X * c12x2 * c12.Y * c21.Y * c13.Y -
                    6 * c10.Y * c20.X * c13x2 * c21.Y * c13.Y - 6 * c10.Y * c20.Y * c21.X * c13x2 * c13.Y - 2 * c10.Y * c12x2 * c21.X * c12.Y * c13.Y -
                    2 * c10.Y * c12x2 * c12.Y * c13.X * c21.Y - c11.X * c11.Y * c12x2 * c21.Y * c13.Y - 4 * c11.X * c20.Y * c12.Y * c13x2 * c21.Y -
                    2 * c11.X * c11y2 * c21.X * c13.X * c13.Y + 3 * c20.X * c11.Y * c12.Y * c13x2 * c21.Y - 2 * c20.X * c12.X * c21.X * c12y2 * c13.Y -
                    2 * c20.X * c12.X * c12y2 * c13.X * c21.Y - 6 * c20.X * c20.Y * c21.X * c13.X * c13y2 - 2 * c11.Y * c12.X * c20.Y * c13x2 * c21.Y +
                    3 * c11.Y * c20.Y * c21.X * c12.Y * c13x2 - 2 * c12.X * c20.Y * c21.X * c12y2 * c13.X - c11y2 * c12.X * c21.X * c12.Y * c13.X +
                    6 * c20.X * c20.Y * c13x2 * c21.Y * c13.Y + 2 * c20.X * c12x2 * c12.Y * c21.Y * c13.Y + 2 * c11x2 * c11.Y * c13.X * c21.Y * c13.Y +
                    c11x2 * c12.X * c12.Y * c21.Y * c13.Y + 2 * c12x2 * c20.Y * c21.X * c12.Y * c13.Y + 2 * c12x2 * c20.Y * c12.Y * c13.X * c21.Y +
                    3 * c10x2 * c21.X * c13y3 - 3 * c10y2 * c13x3 * c21.Y + 3 * c20x2 * c21.X * c13y3 + c11y3 * c21.X * c13x2 - c11x3 * c21.Y * c13y2 -
                    3 * c20y2 * c13x3 * c21.Y - c11.X * c11y2 * c13x2 * c21.Y + c11x2 * c11.Y * c21.X * c13y2 - 3 * c10x2 * c13.X * c21.Y * c13y2 +
                    3 * c10y2 * c21.X * c13x2 * c13.Y - c11x2 * c12y2 * c13.X * c21.Y + c11y2 * c12x2 * c21.X * c13.Y - 3 * c20x2 * c13.X * c21.Y * c13y2 +
                    3 * c20y2 * c21.X * c13x2 * c13.Y,
                c10.X * c10.Y * c11.X * c12.Y * c13.X * c13.Y - c10.X * c10.Y * c11.Y * c12.X * c13.X * c13.Y + c10.X * c11.X * c11.Y * c12.X * c12.Y * c13.Y -
                    c10.Y * c11.X * c11.Y * c12.X * c12.Y * c13.X - c10.X * c11.X * c20.Y * c12.Y * c13.X * c13.Y + 6 * c10.X * c20.X * c11.Y * c12.Y * c13.X * c13.Y +
                    c10.X * c11.Y * c12.X * c20.Y * c13.X * c13.Y - c10.Y * c11.X * c20.X * c12.Y * c13.X * c13.Y - 6 * c10.Y * c11.X * c12.X * c20.Y * c13.X * c13.Y +
                    c10.Y * c20.X * c11.Y * c12.X * c13.X * c13.Y - c11.X * c20.X * c11.Y * c12.X * c12.Y * c13.Y + c11.X * c11.Y * c12.X * c20.Y * c12.Y * c13.X +
                    c11.X * c20.X * c20.Y * c12.Y * c13.X * c13.Y - c20.X * c11.Y * c12.X * c20.Y * c13.X * c13.Y - 2 * c10.X * c20.X * c12y3 * c13.X +
                    2 * c10.Y * c12x3 * c20.Y * c13.Y - 3 * c10.X * c10.Y * c11.X * c12.X * c13y2 - 6 * c10.X * c10.Y * c20.X * c13.X * c13y2 +
                    3 * c10.X * c10.Y * c11.Y * c12.Y * c13x2 - 2 * c10.X * c10.Y * c12.X * c12y2 * c13.X - 2 * c10.X * c11.X * c20.X * c12.Y * c13y2 -
                    c10.X * c11.X * c11.Y * c12y2 * c13.X + 3 * c10.X * c11.X * c12.X * c20.Y * c13y2 - 4 * c10.X * c20.X * c11.Y * c12.X * c13y2 +
                    3 * c10.Y * c11.X * c20.X * c12.X * c13y2 + 6 * c10.X * c10.Y * c20.Y * c13x2 * c13.Y + 2 * c10.X * c10.Y * c12x2 * c12.Y * c13.Y +
                    2 * c10.X * c11.X * c11y2 * c13.X * c13.Y + 2 * c10.X * c20.X * c12.X * c12y2 * c13.Y + 6 * c10.X * c20.X * c20.Y * c13.X * c13y2 -
                    3 * c10.X * c11.Y * c20.Y * c12.Y * c13x2 + 2 * c10.X * c12.X * c20.Y * c12y2 * c13.X + c10.X * c11y2 * c12.X * c12.Y * c13.X +
                    c10.Y * c11.X * c11.Y * c12x2 * c13.Y + 4 * c10.Y * c11.X * c20.Y * c12.Y * c13x2 - 3 * c10.Y * c20.X * c11.Y * c12.Y * c13x2 +
                    2 * c10.Y * c20.X * c12.X * c12y2 * c13.X + 2 * c10.Y * c11.Y * c12.X * c20.Y * c13x2 + c11.X * c20.X * c11.Y * c12y2 * c13.X -
                    3 * c11.X * c20.X * c12.X * c20.Y * c13y2 - 2 * c10.X * c12x2 * c20.Y * c12.Y * c13.Y - 6 * c10.Y * c20.X * c20.Y * c13x2 * c13.Y -
                    2 * c10.Y * c20.X * c12x2 * c12.Y * c13.Y - 2 * c10.Y * c11x2 * c11.Y * c13.X * c13.Y - c10.Y * c11x2 * c12.X * c12.Y * c13.Y -
                    2 * c10.Y * c12x2 * c20.Y * c12.Y * c13.X - 2 * c11.X * c20.X * c11y2 * c13.X * c13.Y - c11.X * c11.Y * c12x2 * c20.Y * c13.Y +
                    3 * c20.X * c11.Y * c20.Y * c12.Y * c13x2 - 2 * c20.X * c12.X * c20.Y * c12y2 * c13.X - c20.X * c11y2 * c12.X * c12.Y * c13.X +
                    3 * c10y2 * c11.X * c12.X * c13.X * c13.Y + 3 * c11.X * c12.X * c20y2 * c13.X * c13.Y + 2 * c20.X * c12x2 * c20.Y * c12.Y * c13.Y -
                    3 * c10x2 * c11.Y * c12.Y * c13.X * c13.Y + 2 * c11x2 * c11.Y * c20.Y * c13.X * c13.Y + c11x2 * c12.X * c20.Y * c12.Y * c13.Y -
                    3 * c20x2 * c11.Y * c12.Y * c13.X * c13.Y - c10x3 * c13y3 + c10y3 * c13x3 + c20x3 * c13y3 - c20y3 * c13x3 -
                    3 * c10.X * c20x2 * c13y3 - c10.X * c11y3 * c13x2 + 3 * c10x2 * c20.X * c13y3 + c10.Y * c11x3 * c13y2 +
                    3 * c10.Y * c20y2 * c13x3 + c20.X * c11y3 * c13x2 + c10x2 * c12y3 * c13.X - 3 * c10y2 * c20.Y * c13x3 - c10y2 * c12x3 * c13.Y +
                    c20x2 * c12y3 * c13.X - c11x3 * c20.Y * c13y2 - c12x3 * c20y2 * c13.Y - c10.X * c11x2 * c11.Y * c13y2 +
                    c10.Y * c11.X * c11y2 * c13x2 - 3 * c10.X * c10y2 * c13x2 * c13.Y - c10.X * c11y2 * c12x2 * c13.Y + c10.Y * c11x2 * c12y2 * c13.X -
                    c11.X * c11y2 * c20.Y * c13x2 + 3 * c10x2 * c10.Y * c13.X * c13y2 + c10x2 * c11.X * c12.Y * c13y2 +
                    2 * c10x2 * c11.Y * c12.X * c13y2 - 2 * c10y2 * c11.X * c12.Y * c13x2 - c10y2 * c11.Y * c12.X * c13x2 + c11x2 * c20.X * c11.Y * c13y2 -
                    3 * c10.X * c20y2 * c13x2 * c13.Y + 3 * c10.Y * c20x2 * c13.X * c13y2 + c11.X * c20x2 * c12.Y * c13y2 - 2 * c11.X * c20y2 * c12.Y * c13x2 +
                    c20.X * c11y2 * c12x2 * c13.Y - c11.Y * c12.X * c20y2 * c13x2 - c10x2 * c12.X * c12y2 * c13.Y - 3 * c10x2 * c20.Y * c13.X * c13y2 +
                    3 * c10y2 * c20.X * c13x2 * c13.Y + c10y2 * c12x2 * c12.Y * c13.X - c11x2 * c20.Y * c12y2 * c13.X + 2 * c20x2 * c11.Y * c12.X * c13y2 +
                    3 * c20.X * c20y2 * c13x2 * c13.Y - c20x2 * c12.X * c12y2 * c13.Y - 3 * c20x2 * c20.Y * c13.X * c13y2 + c12x2 * c20y2 * c12.Y * c13.X
            );
            var roots = poly.RootsInInterval(0, 1);
            poly.RemoveMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c13.X,
                    c12.X,
                    c11.X,
                    c10.X - c20.X - s * c21.X - s * s * c22.X - s * s * s * c23.X
                ).SolveRealRoots().ToArray();
                var yRoots = new Polynomial(
                    c13.Y,
                    c12.Y,
                    c11.Y,
                    c10.Y - c20.Y - s * c21.Y - s * s * c22.Y - s * s * s * c23.Y
                ).SolveRealRoots().ToArray();

                if (xRoots.Length > 0 && yRoots.Length > 0)
                {
                    var TOLERANCE = 1e-4;

                    //checkRoots:
                    for (var j = 0; j < xRoots.Length; j++)
                    {
                        var xRoot = xRoots[j];

                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Length; k++)
                            {
                                if (Abs(xRoot - yRoots[k]) < TOLERANCE)
                                {
                                    var v = c23 * (s * s * s) + (c22 * (s * s) + (c21 * (s) + (c20)));
                                    result.Add(new Point2D(v.I, v.J));
                                    break;// checkRoots;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier3Ellipse(
            Point2D p1, Point2D p2, Point2D p3, Point2D p4,
            Point2D ec, double rx, double ry)
        {
            var result = new List<Point2D>();

            // Calculate the coefficients of cubic polynomial
            var a = p1 * -1;
            var b = p2 * 3;
            var c = p3 * -3;
            var d = a + b + c + p4;
            var c3 = new Point2D(d.I, d.J);

            a = p1 * 3;
            b = p2 * -6;
            c = p3 * 3;
            d = a + b + c;
            var c2 = new Point2D(d.I, d.J);

            a = p1 * -3;
            b = p2 * 3;
            c = (Point2D)(a + b);
            var c1 = new Point2D(c.X, c.Y);

            var c0 = new Point2D(p1.X, p1.Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;
            var poly = new Polynomial(
                c3.X * c3.X * ryry + c3.Y * c3.Y * rxrx,
                2 * (c3.X * c2.X * ryry + c3.Y * c2.Y * rxrx),
                2 * (c3.X * c1.X * ryry + c3.Y * c1.Y * rxrx) + c2.X * c2.X * ryry + c2.Y * c2.Y * rxrx,
                2 * c3.X * ryry * (c0.X - ec.X) + 2 * c3.Y * rxrx * (c0.Y - ec.Y) +
                    2 * (c2.X * c1.X * ryry + c2.Y * c1.Y * rxrx),
                2 * c2.X * ryry * (c0.X - ec.X) + 2 * c2.Y * rxrx * (c0.Y - ec.Y) +
                    c1.X * c1.X * ryry + c1.Y * c1.Y * rxrx,
                2 * c1.X * ryry * (c0.X - ec.X) + 2 * c1.Y * rxrx * (c0.Y - ec.Y),
                c0.X * c0.X * ryry - 2 * c0.Y * ec.Y * rxrx - 2 * c0.X * ec.X * ryry +
                    c0.Y * c0.Y * rxrx + ec.X * ec.X * ryry + ec.Y * ec.Y * rxrx - rxrx * ryry
            );
            var roots = poly.RootsInInterval(0, 1);
            poly.RemoveMultipleRootsIn01(roots);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                var v = c3 * (t * t * t) + (c2 * (t * t) + (c1 * (t) + (c0)));
                result.Add(new Point2D(v.I, v.J));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        private static List<Point2D> intersectBezier3Line(
            Point2D p1, Point2D p2, Point2D p3, Point2D p4,
            Point2D a1, Point2D a2)
        {
            var min = a1.Min(a2); // used to determine if point is on line segment
            var max = a1.Max(a2); // used to determine if point is on line segment
            var result = new List<Point2D>();

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
            var a = p1 * -1;
            var b = p2 * 3;
            var c = p3 * -3;
            var d = a + b + c + p4;
            var c3 = new Point2D(d.I, d.J);

            a = p1 * 3;
            b = p2 * -6;
            c = p3 * 3;
            d = a + b + c;
            var c2 = new Point2D(d.I, d.J);

            a = p1 * -3;
            b = p2 * 3;
            c = (Point2D)(a + b);
            var c1 = new Point2D(c.X, c.Y);

            var c0 = new Point2D(p1.X, p1.Y);

            // Convert line to normal form: ax + by + c = 0
            // Find normal to line: negative inverse of original line's slope
            var n = new Point2D(a1.Y - a2.Y, a2.X - a1.X);

            // Determine new c coefficient
            var cl = a1.X * a2.Y - a2.X * a1.Y;

            // ?Rotate each cubic coefficient using line for new coordinate system?
            // Find roots of rotated cubic
            var roots = new Polynomial(
                n.DotProduct(c3),
                n.DotProduct(c2),
                n.DotProduct(c1),
                n.DotProduct(c0) + cl
            ).SolveRealRoots().ToArray();

            // Any roots in closed interval [0,1] are intersections on Bezier, but
            // might not be on the line segment.
            // Find intersections and calculate point coordinates
            for (var i = 0; i < roots.Length; i++)
            {
                var t = roots[i];

                if (0 <= t && t <= 1)
                {
                    // We're within the Bezier curve
                    // Find point on Bezier
                    var p5 = Interpolaters.Linear(p1, p2, t);
                    var p6 = Interpolaters.Linear(p2, p3, t);
                    var p7 = Interpolaters.Linear(p3, p4, t);

                    var p8 = Interpolaters.Linear(p5, p6, t);
                    var p9 = Interpolaters.Linear(p6, p7, t);

                    var p10 = Interpolaters.Linear(p8, p9, t);

                    // See if point is on line segment
                    // Had to make special cases for vertical and horizontal lines due
                    // to slight errors in calculation of p10
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.Add(p10);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.Add(p10);
                        }
                    }
                    else if (min.X <= p10.X && p10.X <= max.X && min.Y <= p10.Y && p10.Y <= max.Y)
                    {
                        result.Add(p10);
                    }
                }
            }

            return result;
        }

        //private static List<Point2D> intersectEllipseEllipse(
        //    Point2D c1, double rx1, double ry1,
        //    Point2D c2, double rx2, double ry2)
        //{
        //    var a = new double[] {
        //        ry1 * ry1, 0, rx1 * rx1, -2 * ry1 * ry1 * c1.X, -2 * rx1 * rx1 * c1.Y,
        //        ry1 * ry1 * c1.X * c1.X + rx1 * rx1 * c1.Y * c1.Y - rx1 * rx1 * ry1 * ry1
        //    };
        //    var b = new double[]{
        //        ry2* ry2, 0, rx2* rx2, -2 * ry2 * ry2 * c2.X, -2 * rx2 * rx2 * c2.Y,
        //        ry2* ry2 *c2.X * c2.X + rx2 * rx2 * c2.Y * c2.Y - rx2 * rx2 * ry2 * ry2
        //    };

        //    var yPoly = Polynomial.Bezout(a, b);
        //    var yRoots = yPoly.SolveRealRoots().ToList();
        //    var epsilon = 1e-3;
        //    var norm0 = (a[0] * a[0] + 2 * a[1] * a[1] + a[2] * a[2]) * epsilon;
        //    var norm1 = (b[0] * b[0] + 2 * b[1] * b[1] + b[2] * b[2]) * epsilon;
        //    var result = new List<Point2D>();

        //    int i;
        //    //Handling root calculation error causing not detecting intersection
        //    for (i = 0; i < yRoots.Count; i++)
        //    {
        //        yRoots[i] = ((val, min, max)
        //            =>
        //        { return Math.Max(min, Math.Min(max, val)); }
        //        , yRoots[i], c1.Y - ry1, c1.Y + ry1);
        //        yRoots[i] = ((val, min, max)
        //            =>
        //        { return Math.Max(min, Math.Min(max, val)); }
        //        , yRoots[i], c2.Y - ry2, c2.Y + ry2);
        //    }

        //    //For detection of multiplicated intersection points
        //    yRoots.Sort();// function(a, b) { return a - b; });
        //    var rootPointsN = new List<double>();

        //    for (var y = 0; y < yRoots.Count; y++)
        //    {
        //        var xPoly = new Polynomial(
        //            a[0],
        //            a[3] + yRoots[y] * a[1],
        //            a[5] + yRoots[y] * (a[4] + yRoots[y] * a[2])
        //        );
        //        var ERRF = 1e-15;
        //        if (Abs(xPoly.Coefficients[0]) < 10 * ERRF * Abs(xPoly.Coefficients[2]))
        //            xPoly.Coefficients[0] = 0;
        //        var xRoots = xPoly.SolveRealRoots().ToList();

        //        rootPointsN.Add(0);
        //        for (var x = 0; x < xRoots.Count; x++)
        //        {
        //            var test =
        //                (a[0] * xRoots[x] + a[1] * yRoots[y] + a[3]) * xRoots[x] +
        //                (a[2] * yRoots[y] + a[4]) * yRoots[y] + a[5];
        //            if (Math.Abs(test) < norm0)
        //            {
        //                test =
        //                    (b[0] * xRoots[x] + b[1] * yRoots[y] + b[3]) * xRoots[x] +
        //                    (b[2] * yRoots[y] + b[4]) * yRoots[y] + b[5];
        //                if (Math.Abs(test) < norm1)
        //                {
        //                    result.Add(new Point2D(xRoots[x], yRoots[y]));
        //                    rootPointsN[y] += 1;
        //                }
        //            }
        //        }
        //    }

        //    if (result.Count <= 0)
        //        return result;

        //    //Removal of multiplicated intersection points
        //    var pts = result;
        //    if (pts.Count == 8)
        //    {
        //        pts.Insert(0, 6);
        //        pts.Insert(2, 2);
        //    }
        //    else if (pts.Count == 7)
        //    {
        //        pts.Insert(0, 6);
        //        pts.Insert(2, 2);
        //        pts.Insert(rootPointsN.IndexOf(1), 1);
        //    }
        //    else if (pts.Count == 6)
        //    {
        //        pts.Insert(2, 2);
        //        //console.log('ElEl 6pts: N: ' + rootPointsN.toString());
        //        if (rootPointsN.IndexOf(0) > -1)
        //        {
        //            if (pts[0].distanceFrom(pts[1]) < pts[2].distanceFrom(pts[3]))
        //            {
        //                pts.Insert(0, 1);
        //            }
        //            else
        //            {
        //                pts.Insert(2, 1);
        //            }
        //        }
        //        else if (rootPointsN[0] == rootPointsN[3])
        //        {
        //            pts.Insert(1, 2);
        //        }
        //    }
        //    else if (pts.length == 4)
        //    {
        //        if (
        //            (yRoots.length == 2)
        //        || (yRoots.length == 4 && (rootPointsN[0] == 2 && rootPointsN[1] == 2 || rootPointsN[2] == 2 && rootPointsN[3] == 2))
        //        )
        //        {
        //            pts.Insert(2, 2);
        //        }
        //    }
        //    else if (pts.length == 3 || pts.length == 5)
        //    {
        //        i = rootPointsN.indexOf(2);
        //        if (i > -1)
        //        {
        //            if (pts.length == 3)
        //                i = i % 2;
        //            var ii = i + (i % 2 ? -1 : 2);
        //            var d1, d2, d3;
        //            d1 = pts[i].distanceFrom(pts[i + 1]);
        //            d2 = pts[i].distanceFrom(pts[ii]);
        //            d3 = pts[i + 1].distanceFrom(pts[ii]);
        //            if (d1 < d2 && d1 < d3)
        //            {
        //                pts.Insert(i, 1);
        //            }
        //            else
        //            {
        //                pts.Insert(ii, 1);
        //            }
        //        }
        //    }

        //    var poly = yPoly;
        //    var ZEROepsilon = yPoly.zeroErrorEstimate();
        //    ZEROepsilon *= 100 * Math.SQRT2;
        //    for (i = 0; i < pts.length - 1;)
        //    {
        //        if (pts[i].distanceFrom(pts[i + 1]) < ZEROepsilon)
        //        {
        //            pts.Insert(i + 1, 1);
        //            continue;
        //        }
        //        i++;
        //    }

        //    result.points = pts;
        //    return result;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        public static List<Point2D> intersectEllipseLine(
            Point2D ec, double rx, double ry,
            Point2D a1, Point2D a2)
        {
            var result = new List<Point2D>();
            var origin = new Vector2D(a1.X, a1.Y);
            var dir = new Vector2D(a1, a2);
            var center = new Vector2D(ec.X, ec.Y);
            var diff = origin - center;
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.I / (rx * rx), diff.J / (ry * ry));

            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1.0;
            var d = b * b - a * c;

            var ERRF = 1e-15;
            var ZEROepsilon = 10 * Max(Abs(a), Abs(b), Abs(c)) * ERRF;
            if (Abs(d) < ZEROepsilon)
            {
                d = 0;
            }

            if (d < 0)
            {
                result = null;// new Intersection("Outside");
            }
            else if (d > 0)
            {
                var root = Sqrt(d);
                var t_a = (-b - root) / a;
                var t_b = (-b + root) / a;

                t_b = (t_b > 1) ? t_b - ERRF : (t_b < 0) ? t_b + ERRF : t_b;
                t_a = (t_a > 1) ? t_a - ERRF : (t_a < 0) ? t_a + ERRF : t_a;

                if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
                {
                    if ((t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1))
                        result = null;//new Intersection("Outside");
                    else
                        result = null;//new Intersection("Inside");
                }
                else
                {
                    result = new List<Point2D>();
                    if (0 <= t_a && t_a <= 1)
                        result.Add(Interpolaters.Linear(a1, a2, t_a));
                    if (0 <= t_b && t_b <= 1)
                        result.Add(Interpolaters.Linear(a1, a2, t_b));
                }
            }
            else
            {
                var t = -b / a;
                if (0 <= t && t <= 1)
                {
                    result = new List<Point2D>();
                    result.Add(Interpolaters.Linear(a1, a2, t));
                }
                else
                {
                    result = null;// new Intersection("Outside");
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static List<Point2D> intersectLineLine(
            Point2D a1, Point2D a2,
            Point2D b1, Point2D b2)
        {
            List<Point2D> result;

            var ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            var u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);

            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;

                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result = new List<Point2D>();
                    result.Add(
                        new Point2D(
                            a1.X + ua * (a2.X - a1.X),
                            a1.Y + ua * (a2.Y - a1.Y)
                        )
                    );
                }
                else
                {
                    result = new List<Point2D>();
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = null;// new Intersection("Coincident");
                }
                else
                {
                    result = null;// new Intersection("Parallel");
                }
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static List<Point2D> intersectRayRay(
            Point2D a1, Point2D a2,
            Point2D b1, Point2D b2)
        {
            List<Point2D> result;

            var ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            var u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);

            if (u_b != 0)
            {
                var ua = ua_t / u_b;

                result = new List<Point2D>();
                result.Add(
                    new Point2D(
                        a1.X + ua * (a2.X - a1.X),
                        a1.Y + ua * (a2.Y - a1.Y)
                    )
                );
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = null;// new Intersection("Coincident");
                }
                else
                {
                    result = null;// new Intersection("Parallel");
                }
            }

            return result;
        }

        //public void removeClosePoints(List<Point2D> points1, List<Point2D> points2)
        //{
        //    if (points1.Count == 0 || points2.Count == 0)
        //        return;
        //    var maxf = ;
        //    var max = points1.Reduce((p, v)=> { if (p < v.X) p = v.X; if (p < v.Y) p = v.Y; return p; }, 0);
        //    max = points2.Reduce((p, v)=> { if (p < v.X) p = v.X; if (p < v.Y) p = v.Y; return p; }, max);
        //    var ERRF = 1e-15;
        //    var ZEROepsilon = 100 * max * ERRF * Maths.Sqrt2;
        //    var j;
        //    for (var i = 0; i < points1.Count;)
        //    {
        //        for (j = 0; j < points2.Count; j++)
        //        {
        //            if (points1[i].distanceFrom(points2[j]) <= ZEROepsilon)
        //            {
        //                points1.Insert(i, 1);
        //                break;
        //            }
        //        }
        //        if (j == points2.Count)
        //            i++;
        //    }
        //};
    }
}

