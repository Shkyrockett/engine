// <copyright file="PolygonExtensions.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Math;

namespace Engine.Geometry.Polygons
{
    /// <summary>
    /// 
    /// </summary>
    public static class PolygonExtensions
    {
        /// <summary>
        /// Finds the shortest path from sX,sY to eX,eY that stays within the polygon set.
        /// Note:  To be safe, the solutionX and solutionY arrays should be large enough
        ///  to accommodate all the corners of your polygon set (although it is
        /// unlikely that anywhere near that many elements will ever be needed).
        /// Returns YES if the optimal solution was found, or NO if there is no solution.
        /// If a solution was found, solutionX and solutionY will contain the coordinates
        /// of the intermediate nodes of the path, in order.  (The startpoint and endpoint
        /// are assumed, and will not be included in the solution.)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static Polyline ShortestPath(this PolygonSet polygons, Point2D start, Point2D end)
        {
            // (larger than total solution dist could ever be)
            double maxLength = double.MaxValue;

            List<TestPoint2D> pointList = new List<TestPoint2D>();
            List<Point2D> solution = new List<Point2D>();

            int pointCount;
            int solutionNodes;

            int treeCount;
            int bestI = 0;
            int bestJ;
            double bestDist;
            double newDist;

            //  Fail if either the startpoint or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            //  If there is a straight-line solution, return with it immediately.
            if (polygons.Contains(start, end))
            {
                return new Polyline(new List<Point2D>() { start, end });
            }

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the startpoint and endpoint.
            pointList.Add(start);
            pointCount = 1;
            foreach (var poly in polygons.Polygons)
            {
                foreach (var point in poly.Points)
                {
                    pointList.Add(point);
                    pointCount++;
                }
            }

            pointList.Add(end);
            pointCount++;

            //  Initialize the shortest-path tree to include just the startpoint.
            treeCount = 1;
            pointList[0].TotalDistance = 0.0;

            //  Iteratively grow the shortest-path tree until it reaches the endpoint
            //  -- or until it becomes unable to grow, in which case exit with failure.
            bestJ = 0;
            while (bestJ < pointCount - 1)
            {
                bestDist = maxLength;
                for (int ti = 0; ti < treeCount; ti++)
                {
                    for (int tj = treeCount; tj < pointCount; tj++)
                    {
                        if (polygons.Contains((Point2D)pointList[ti], (Point2D)pointList[tj]))
                        {
                            newDist = pointList[ti].TotalDistance + Primitives.Distance((Point2D)pointList[ti], (Point2D)pointList[tj]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist; bestI = ti; bestJ = tj;
                            }
                        }
                    }
                }

                if (bestDist == maxLength) return null;   //  (no solution)
                pointList[bestJ].Previous = bestI;
                pointList[bestJ].TotalDistance = bestDist;

                // Swap
                TestPoint2D temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            //  Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            int i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }
            int j = solutionNodes - 1;
            i = treeCount - 1;
            while (j >= 0)
            {
                i = pointList[i].Previous;
                solution.Insert(1, (Point2D)pointList[i]);
                j--;
            }
            solution.Add(end);

            //  Success.
            return new Polyline(solution);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        public static double EllipsePerimeter(double a, double b)
        {
            return 4 * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        public static double CubicBezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            Point2D k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            Point2D k2 = 3 * (p1 + p3) - 6 * p2;
            Point2D k3 = (Point2D)(3 * (p2 - p1));
            Point2D k4 = p1;

            double q1 = 9.0 * (Sqrt(Abs(k1.X)) + Sqrt((Abs(k1.Y))));
            double q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            double q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            double q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            double q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson. 
            double a = 0;
            double b = 1;
            int n_limit = 1024;
            double TOLERANCE = 0.001;

            int n = 1;

            double multiplier = (b - a) / 6.0;
            double endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            double interval = (b - a) / 2.0;
            double asum = 0;
            double bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            double est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            double est0 = 2 * est1;

            while (n < n_limit && (Abs(est1) > 0 && Abs((est1 - est0) / est1) > TOLERANCE))
            {
                n *= 2;
                multiplier /= 2;
                interval /= 2;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                double interval_div_2n = interval / (2.0 * n);

                for (int i = 1; i < 2 * n; i += 2)
                {
                    double t = a + i * interval_div_2n;
                    bsum += CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, t);
                }

                est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            }

            return est1 * 10;
        }

        /// <summary>
        /// Bezier Arc Length Function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        private static double CubicBezierArcLengthHelper(ref double q1, ref double q2, ref double q3, ref double q4, ref double q5, double t)
        {
            double result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrum"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static List<Point2D> RotatedRectangle(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            List<Point2D> points = new List<Point2D>();

            Point2D xaxis = new Point2D(Cos(angle), Sin(angle));
            Point2D yaxis = new Point2D(-Sin(angle), Cos(angle));

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrum.X + ((-width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrum.Y + ((-width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrum.Y + ((width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrum.Y + ((width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrum.X + ((-width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrum.Y + ((-width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));

            return points;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrum"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Rectangle2D RotatedRectangleBounds(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            double cosAngle = Abs(Cos(angle));
            double sinAngle = Abs(Sin(angle));

            Size2D size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            Point2D loc = new Point2D(
                fulcrum.X + ((-width / 2) * cosAngle + (-height / 2) * sinAngle),
                fulcrum.Y + ((-width / 2) * sinAngle + (-height / 2) * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }

        /// <summary>
        /// Find the polygon's centroid.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static Point2D FindCentroid(this Polygon polygon)
        {
            // Add the first point at the end of the array.
            int num_points = polygon.Points.Count;
            Point2D[] pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y -
                    pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            double polygon_area = PolygonArea(polygon);
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }

            return new Point2D(X, Y);
        }

        /// <summary>
        /// Return true if the point is in the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static bool PointInPolygon(this Polygon polygon, Point2D point)
        {
            return PointInPolygon(polygon, point.X, point.Y);
        }

        /// <summary>
        /// Return true if the point is in the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static bool PointInPolygon(this Polygon polygon, double X, double Y)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon.Points.Count - 1;
            double total_angle = GetAngle(
                polygon.Points[max_point].X, polygon.Points[max_point].Y,
                X, Y,
                polygon.Points[0].X, polygon.Points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon.Points[i].X, polygon.Points[i].Y,
                    X, Y,
                   polygon.Points[i + 1].X, polygon.Points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Abs(total_angle) > 0.000001);
        }

        /// <summary>
        /// Return true if the polygon is oriented clockwise.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static bool PolygonIsOrientedClockwise(this Polygon polygon)
        {
            return (SignedPolygonArea(polygon) < 0);
        }

        /// <summary>
        /// If the polygon is oriented counterclockwise, reverse the order of its points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void OrientPolygonClockwise(this Polygon polygon)
        {
            if (!PolygonIsOrientedClockwise(polygon))
                polygon.Points.Reverse();
        }

        /// <summary>
        /// Return the polygon's area in "square units."
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static double PolygonArea(this Polygon polygon)
        {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polygon is
            // oriented clockwise.
            return Abs(SignedPolygonArea(polygon));
        }

        /// <summary>
        /// Return the polygon's area in "square units."
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        ///
        /// The value will be negative if the polygon is
        /// oriented clockwise.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static double SignedPolygonArea(this Polygon polygon)
        {
            // Add the first point to the end.
            int num_points = polygon.Points.Count;
            Point2D[] pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Get the areas.
            double area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        /// <summary>
        /// Return true if the polygon is convex.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static bool PolygonIsConvex(this Polygon polygon)
        {
            // For each set of three adjacent points A, B, C,
            // find the dot product AB · BC. If the sign of
            // all the dot products is the same, the angles
            // are all positive or negative (depending on the
            // order in which we visit them) so the polygon
            // is convex.
            bool got_negative = false;
            bool got_positive = false;
            int num_points = polygon.Points.Count;
            int B, C;
            for (int A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                double cross_product =
                    Maths.CrossProductVector(
                        polygon.Points[A].X, polygon.Points[A].Y,
                        polygon.Points[B].X, polygon.Points[B].Y,
                        polygon.Points[C].X, polygon.Points[C].Y);
                if (cross_product < 0)
                {
                    got_negative = true;
                }
                else if (cross_product > 0)
                {
                    got_positive = true;
                }
                if (got_negative && got_positive) return false;
            }

            // If we got this far, the polygon is convex.
            return true;
        }

        /// <summary>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static double GetAngle(double Ax, double Ay, double Bx, double By, double Cx, double Cy)
        {
            // Get the dot product.
            double dot_product = Maths.DotProductVector(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            double cross_product = Maths.CrossProductVector(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return Atan2(cross_product, dot_product);
        }

        /// <summary>
        /// Find the indexes of three points that form an "ear."
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void FindEar(this Polygon polygon, ref int A, ref int B, ref int C)
        {
            int num_points = polygon.Points.Count;

            for (A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                if (FormsEar(polygon.Points.ToArray(), A, B, C)) return;
            }

            // We should never get here because there should
            // always be at least two ears.
            Debug.Assert(false);
        }

        /// <summary>
        /// Return true if the three points form an ear.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static bool FormsEar(Point2D[] points, int A, int B, int C)
        {
            // See if the angle ABC is concave.
            if (GetAngle(
                points[A].X, points[A].Y,
                points[B].X, points[B].Y,
                points[C].X, points[C].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            Triangle triangle = new Triangle(
                points[A], points[B], points[C]);

            // Check the other points to see 
            // if they lie in triangle A, B, C.
            for (int i = 0; i < points.Length; i++)
            {
                if ((i != A) && (i != B) && (i != C))
                {
                    if (triangle.PointInPolygon(points[i].X, points[i].Y))
                    {
                        // This point is in the triangle 
                        // do this is not an ear.
                        return false;
                    }
                }
            }

            // This is an ear.
            return true;
        }

        /// <summary>
        /// Remove an ear from the polygon and
        /// add it to the triangles array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="triangles"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void RemoveEar(this Polygon polygon, List<Triangle> triangles)
        {
            // Find an ear.
            int A = 0, B = 0, C = 0;
            FindEar(polygon, ref A, ref B, ref C);

            // Create a new triangle for the ear.
            triangles.Add(new Triangle(polygon.Points[A], polygon.Points[B], polygon.Points[C]));

            // Remove the ear from the polygon.
            RemovePoint2DromArray(polygon, B);
        }

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void RemovePoint2DromArray(this Polygon polygon, int target)
        {
            polygon.Points.RemoveAt(target);
            //List<Point2D> pts = new List<Point2D>(polygon.Points.Count);
            //Array.Copy(polygon.Points.ToArray(), 0, pts, 0, target);
            //Array.Copy(polygon.Points.ToArray(), target + 1, pts, target, polygon.Points.Count - target - 1);
            //polygon.Points = pts;
        }

        /// <summary>
        /// Triangulate the polygon.
        ///
        /// For a nice, detailed explanation of this method,
        /// see Ian Garton's Web page:
        /// http://www-cgrl.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/cutting_ears.html
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static List<Triangle> Triangulate(this Polygon polygon)
        {
            // Copy the points into a scratch array.
            Point2D[] pts = new Point2D[polygon.Points.Count];
            Array.Copy(polygon.Points.ToArray(), pts, polygon.Points.Count);

            // Make a scratch polygon.
            Polygon pgon = new Polygon(new List<Point2D>(pts));

            // Orient the polygon clockwise.
            pgon.OrientPolygonClockwise();

            // Make room for the triangles.
            List<Triangle> triangles = new List<Triangle>();

            // While the copy of the polygon has more than
            // three points, remove an ear.
            while (pgon.Points.Count > 3)
            {
                // Remove an ear from the polygon.
                pgon.RemoveEar(triangles);
            }

            // Copy the last three points into their own triangle.
            triangles.Add(new Triangle(pgon.Points[0], pgon.Points[1], pgon.Points[2]));

            return triangles;
        }

        /// <summary>
        /// Get ready to start.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void ResetBoundingRect(this Polygon polygon, BoundingRect boundingRect)
        {
            boundingRect.NumPoints = polygon.Points.Count;

            // Find the initial control points.
            FindInitialControlPoints(polygon, boundingRect);

            // So far we have not checked any edges.
            boundingRect.EdgeChecked = new bool[boundingRect.NumPoints];

            // Start with this bounding rectangle.
            boundingRect.CurrentControlPoint = 1;
            boundingRect.BestArea = float.MaxValue;

            // Find the initial bounding rectangle.
            FindBoundingRectangle(polygon, boundingRect);

            // Remember that we have checked this edge.
            boundingRect.EdgeChecked[boundingRect.ControlPoints[boundingRect.CurrentControlPoint]] = true;
        }

        /// <summary>
        /// Find the initial control points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void FindInitialControlPoints(this Polygon polygon, BoundingRect boundingRect)
        {
            for (int i = 0; i < boundingRect.NumPoints; i++)
            {
                if (CheckInitialControlPoints(polygon, boundingRect, i)) return;
            }
            Debug.Assert(false, "Could not find initial control points.");
        }

        /// <summary>
        /// See if we can use segment i --> i + 1 as the base for the initial control points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static bool CheckInitialControlPoints(this Polygon polygon, BoundingRect boundingRect, int i)
        {
            // Get the i -> i + 1 unit vector.
            int i1 = (i + 1) % boundingRect.NumPoints;
            double vix = polygon.Points[i1].X - polygon.Points[i].X;
            double viy = polygon.Points[i1].Y - polygon.Points[i].Y;

            // The candidate control point indexes.
            for (int num = 0; num < 4; num++)
            {
                boundingRect.ControlPoints[num] = i;
            }

            // Check backward from i until we find a vector
            // j -> j+1 that points opposite to i -> i+1.
            for (int num = 1; num < boundingRect.NumPoints; num++)
            {
                // Get the new edge vector.
                int j = (i - num + boundingRect.NumPoints) % boundingRect.NumPoints;
                int j1 = (j + 1) % boundingRect.NumPoints;
                double vjx = polygon.Points[j1].X - polygon.Points[j].X;
                double vjy = polygon.Points[j1].Y - polygon.Points[j].Y;

                // Project vj along vi. The length is vj dot vi.
                double dot_product = vix * vjx + viy * vjy;

                // If the dot product < 0, then j1 is
                // the index of the candidate control point.
                if (dot_product < 0)
                {
                    boundingRect.ControlPoints[0] = j1;
                    break;
                }
            }

            // If j == i, then i is not a suitable control point.
            if (boundingRect.ControlPoints[0] == i) return false;

            // Check forward from i until we find a vector
            // j -> j+1 that points opposite to i -> i+1.
            for (int num = 1; num < boundingRect.NumPoints; num++)
            {
                // Get the new edge vector.
                int j = (i + num) % boundingRect.NumPoints;
                int j1 = (j + 1) % boundingRect.NumPoints;
                double vjx = polygon.Points[j1].X - polygon.Points[j].X;
                double vjy = polygon.Points[j1].Y - polygon.Points[j].Y;

                // Project vj along vi. The length is vj dot vi.
                double dot_product = vix * vjx + viy * vjy;

                // If the dot product <= 0, then j is
                // the index of the candidate control point.
                if (dot_product <= 0)
                {
                    boundingRect.ControlPoints[2] = j;
                    break;
                }
            }

            // If j == i, then i is not a suitable control point.
            if (boundingRect.ControlPoints[2] == i) return false;

            // Check forward from m_ControlPoints[2] until
            // we find a vector j -> j+1 that points opposite to
            // m_ControlPoints[2] -> m_ControlPoints[2]+1.

            i = boundingRect.ControlPoints[2] - 1;//@
            double temp = vix;
            vix = viy;
            viy = -temp;

            for (int num = 1; num < boundingRect.NumPoints; num++)
            {
                // Get the new edge vector.
                int j = (i + num) % boundingRect.NumPoints;
                int j1 = (j + 1) % boundingRect.NumPoints;
                double vjx = polygon.Points[j1].X - polygon.Points[j].X;
                double vjy = polygon.Points[j1].Y - polygon.Points[j].Y;

                // Project vj along vi. The length is vj dot vi.
                double dot_product = vix * vjx + viy * vjy;

                // If the dot product <=, then j is
                // the index of the candidate control point.
                if (dot_product <= 0)
                {
                    boundingRect.ControlPoints[3] = j;
                    break;
                }
            }

            // If j == i, then i is not a suitable control point.
            if (boundingRect.ControlPoints[0] == i) return false;

            // These control points work.
            return true;
        }

        /// <summary>
        /// Find the next bounding rectangle and check it.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void CheckNextRectangle(this Polygon polygon, BoundingRect boundingRect)
        {
            // Increment the current control point.
            // This means we are done with using this edge.
            if (boundingRect.CurrentControlPoint >= 0)
            {
                boundingRect.ControlPoints[boundingRect.CurrentControlPoint] =
                    (boundingRect.ControlPoints[boundingRect.CurrentControlPoint] + 1) % boundingRect.NumPoints;
            }

            // Find the next point on an edge to use.
            double dx0, dy0, dx1, dy1, dx2, dy2, dx3, dy3;
            FindDxDy(polygon, boundingRect, out dx0, out dy0, boundingRect.ControlPoints[0]);
            FindDxDy(polygon, boundingRect, out dx1, out dy1, boundingRect.ControlPoints[1]);
            FindDxDy(polygon, boundingRect, out dx2, out dy2, boundingRect.ControlPoints[2]);
            FindDxDy(polygon, boundingRect, out dx3, out dy3, boundingRect.ControlPoints[3]);

            // Switch so we can look for the smallest opposite/adjacent ratio.
            double opp0 = dx0;
            double adj0 = dy0;
            double opp1 = -dy1;
            double adj1 = dx1;
            double opp2 = -dx2;
            double adj2 = -dy2;
            double opp3 = dy3;
            double adj3 = -dx3;

            // Assume the first control point is the best point to use next.
            double bestopp = opp0;
            double bestadj = adj0;
            int best_control_point = 0;

            // See if the other control points are better.
            if (opp1 * bestadj < bestopp * adj1)
            {
                bestopp = opp1;
                bestadj = adj1;
                best_control_point = 1;
            }
            if (opp2 * bestadj < bestopp * adj2)
            {
                bestopp = opp2;
                bestadj = adj2;
                best_control_point = 2;
            }
            if (opp3 * bestadj < bestopp * adj3)
            {
                bestopp = opp3;
                bestadj = adj3;
                best_control_point = 3;
            }

            // Use the new best control point.
            boundingRect.CurrentControlPoint = best_control_point;

            // Remember that we have checked this edge.
            boundingRect.EdgeChecked[boundingRect.ControlPoints[boundingRect.CurrentControlPoint]] = true;

            // Find the current bounding rectangle
            // and see if it is an improvement.
            FindBoundingRectangle(polygon, boundingRect);
        }

        /// <summary>
        /// Find the current bounding rectangle and
        /// see if it is better than the previous best.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void FindBoundingRectangle(this Polygon polygon, BoundingRect boundingRect)
        {
            // See which point has the current edge.
            int i1 = boundingRect.ControlPoints[boundingRect.CurrentControlPoint];
            int i2 = (i1 + 1) % boundingRect.NumPoints;
            double dx = polygon.Points[i2].X - polygon.Points[i1].X;
            double dy = polygon.Points[i2].Y - polygon.Points[i1].Y;

            // Make dx and dy work for the first line.
            switch (boundingRect.CurrentControlPoint)
            {
                case 0: // Nothing to do.
                    break;
                case 1: // dx = -dy, dy = dx
                    double temp1 = dx;
                    dx = -dy;
                    dy = temp1;
                    break;
                case 2: // dx = -dx, dy = -dy
                    dx = -dx;
                    dy = -dy;
                    break;
                case 3: // dx = dy, dy = -dx
                    double temp2 = dx;
                    dx = dy;
                    dy = -temp2;
                    break;
            }

            double px0 = polygon.Points[boundingRect.ControlPoints[0]].X;
            double py0 = polygon.Points[boundingRect.ControlPoints[0]].Y;
            double dx0 = dx;
            double dy0 = dy;
            double px1 = polygon.Points[boundingRect.ControlPoints[1]].X;
            double py1 = polygon.Points[boundingRect.ControlPoints[1]].Y;
            double dx1 = dy;
            double dy1 = -dx;
            double px2 = polygon.Points[boundingRect.ControlPoints[2]].X;
            double py2 = polygon.Points[boundingRect.ControlPoints[2]].Y;
            double dx2 = -dx;
            double dy2 = -dy;
            double px3 = polygon.Points[boundingRect.ControlPoints[3]].X;
            double py3 = polygon.Points[boundingRect.ControlPoints[3]].Y;
            double dx3 = -dy;
            double dy3 = dx;

            // Find the points of intersection.
            boundingRect.CurrentRectangle = new Point2D[4];
            FindIntersection(px0, py0, px0 + dx0, py0 + dy0, px1, py1, px1 + dx1, py1 + dy1, ref boundingRect.CurrentRectangle[0]);
            FindIntersection(px1, py1, px1 + dx1, py1 + dy1, px2, py2, px2 + dx2, py2 + dy2, ref boundingRect.CurrentRectangle[1]);
            FindIntersection(px2, py2, px2 + dx2, py2 + dy2, px3, py3, px3 + dx3, py3 + dy3, ref boundingRect.CurrentRectangle[2]);
            FindIntersection(px3, py3, px3 + dx3, py3 + dy3, px0, py0, px0 + dx0, py0 + dy0, ref boundingRect.CurrentRectangle[3]);

            // See if this is the best bounding rectangle so far.
            // Get the area of the bounding rectangle.
            double vx0 = boundingRect.CurrentRectangle[0].X - boundingRect.CurrentRectangle[1].X;
            double vy0 = boundingRect.CurrentRectangle[0].Y - boundingRect.CurrentRectangle[1].Y;
            double len0 = Sqrt(vx0 * vx0 + vy0 * vy0);

            double vx1 = boundingRect.CurrentRectangle[1].X - boundingRect.CurrentRectangle[2].X;
            double vy1 = boundingRect.CurrentRectangle[1].Y - boundingRect.CurrentRectangle[2].Y;
            double len1 = Sqrt(vx1 * vx1 + vy1 * vy1);

            // See if this is an improvement.
            boundingRect.CurrentArea = len0 * len1;
            if (boundingRect.CurrentArea < boundingRect.BestArea)
            {
                boundingRect.BestArea = boundingRect.CurrentArea;
                boundingRect.BestRectangle = boundingRect.CurrentRectangle;
            }
        }

        /// <summary>
        /// Find the slope of the edge from point i to point i + 1.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="i"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void FindDxDy(this Polygon polygon, BoundingRect boundingRect, out double dx, out double dy, int i)
        {
            int i2 = (i + 1) % boundingRect.NumPoints;
            dx = polygon.Points[i2].X - polygon.Points[i].X;
            dy = polygon.Points[i2].Y - polygon.Points[i].Y;
        }

        /// <summary>
        /// Find the point of intersection between two lines.
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="intersect"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static bool FindIntersection(double X1, double Y1, double X2, double Y2, double A1, double B1, double A2, double B2, ref Point2D intersect)
        {
            double dx = X2 - X1;
            double dy = Y2 - Y1;
            double da = A2 - A1;
            double db = B2 - B1;
            double s, t;

            // If the segments are parallel, return False.
            if (Abs(da * dy - db * dx) < 0.001) return false;

            // Find the point of intersection.
            s = (dx * (B1 - Y1) + dy * (X1 - A1)) / (da * dy - db * dx);
            t = (da * (Y1 - B1) + db * (A1 - X1)) / (db * dx - da * dy);
            intersect = new Point2D(X1 + t * dx, Y1 + t * dy);
            return true;
        }

        /// <summary>
        /// Find a smallest bounding rectangle.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static Point2D[] FindSmallestBoundingRectangle(this Polygon polygon, BoundingRect boundingRect)
        {
            // This algorithm assumes the polygon
            // is oriented counter-clockwise.
            Debug.Assert(!PolygonIsOrientedClockwise(polygon));

            // Get ready;
            ResetBoundingRect(polygon, boundingRect);

            // Check all possible bounding rectangles.
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                CheckNextRectangle(polygon, boundingRect);
            }

            // Return the best result.
            return boundingRect.BestRectangle;
        }
    }
}
