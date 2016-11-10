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

namespace Engine
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
        public static bool Intersects(LineSegment s, Point2D p)
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
        public static bool LineSegmentPoint(
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
                (segmentBX - segmentAX) * (pointY - segmentAY)));

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
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the line are parallel.
            if (Abs(determinant) < Epsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            return (
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Point2D(x0 + t * u1, y0 + t * v1));
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
        public static List<Point2D> Intersects(this Ellipse e, LineSegment s)
            => UnrotatedEllipseLineSegment(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, s.B.X, s.B.Y);

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
        public static List<Point2D> UnrotatedEllipseLineSegment(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return null;

            // Translate so the ellipse is centered at the origin.
            double u1 = x0 - cx;
            double v1 = y0 - cy;
            double u2 = x1 - cx;
            double v2 = y1 - cy;

            // Calculate the quadratic parameters.
            double a = ((u1 - u2) * (u1 - u2)) / (rx * rx) + ((v1 - v2) * (v1 - v2)) / (ry * ry);
            //double a = (u2 - u1) * (u2 - u1) / rx / rx + (v2 - v1) * (v2 - v1) / ry / ry;
            double b = 2d * u1 * (u2 - u1) / rx / rx + 2d * v1 * (v2 - v1) / ry / ry;
            double c = (u1 * u1) / rx / rx + (v1 * v1) / ry / ry - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                double t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return new List<Point2D>() { new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy) };
                //return ((t >= 0d) && (t <= 1d), (p1X + (p2X - p1X) * t + cx, p1Y + (p2Y - p1Y) * t + cy), false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Return the points. If the points are on the segment set the bool to true.
                return new List<Point2D>() { new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy),
                        new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy) };
                //return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                //        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // No real solutions.
            return null;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="A"></param>
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
        public static List<Point2D> EllipseLineSegment(
            double h, double k,
            double rx, double ry,
            double A,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return null;

            // Translate so the ellipse is centered at the origin.
            double u1 = x0 - h;
            double v1 = y0 - k;
            double u2 = x1 - h;
            double v2 = y1 - k;

            // Get the ellipse rotation transform.
            double cosA = Cos(A);
            double sinA = Sin(A);

            // Calculate the quadratic parameters.
            //double a = (((u1 - u2) * (u1 - u2)) / (rx * rx) + ((v1 - v2) * (v1 - v2)) / (ry * ry));
            double a = ((u1 - u2) * cosA + (v1 - v2) * sinA) * ((u1 - u2) * cosA + (v1 - v2) * sinA) + ((u1 - u2) * sinA - (v1 - v2) * cosA) * ((u1 - u2) * sinA - (v1 - v2) * cosA);

            double b = 2d * (u1 * (u2 - u1) / (rx * rx) + v1 * (v2 - v1) / (ry * ry));
            double c = ((u1 * u1) / (rx * rx) + (v1 * v1) / (ry * ry) - 1d);

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                double t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return new List<Point2D>() { new Point2D(u1 + (u2 - u1) * t + h, v1 + (v2 - v1) * t + k) };
                //return ((t >= 0d) && (t <= 1d), (p1X + (p2X - p1X) * t + cx, p1Y + (p2Y - p1Y) * t + cy), false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Return the points. If the points are on the segment set the bool to true.
                return new List<Point2D>() { new Point2D(u1 + (u2 - u1) * t1 + h, v1 + (v2 - v1) * t1 + k),
                        new Point2D(u1 + (u2 - u1) * t2 + h, v1 + (v2 - v1) * t2 + k) };
                //return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                //        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // No real solutions.
            return null;
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
        public static List<Point2D> QuadraticBezierLineSegment(
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
    }
}

