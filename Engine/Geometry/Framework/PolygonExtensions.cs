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
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
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
        /// of the intermediate nodes of the path, in order.  (The start point and endpoint
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

            var pointList = new List<TestPoint2D>();
            var solution = new List<Point2D>();

            int pointCount;
            int solutionNodes;

            int treeCount;
            int bestI = 0;
            int bestJ;
            double bestDist;
            double newDist;

            //  Fail if either the start point or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            //  If there is a straight-line solution, return with it immediately.
            if (polygons.Contains(start, end))
                return new Polyline(new List<Point2D> { start, end });

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the start point and endpoint.
            pointList.Add(start);
            pointCount = 1;
            foreach (Polygon poly in polygons.Polygons)
            {
                foreach (Point2D point in poly.Points)
                    pointList.Add(point);
            }

            pointList.Add(end);
            pointCount = pointList.Count;

            //  Initialize the shortest-path tree to include just the start point.
            treeCount = 1;
            pointList[0].TotalDistance = 0d;

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
                            newDist = pointList[ti].TotalDistance + ((Point2D)pointList[ti]).Distance((Point2D)pointList[tj]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist;
                                bestI = ti;
                                bestJ = tj;
                            }
                        }
                    }
                }

                if (Abs(bestDist - maxLength) < Maths.Epsilon)
                    return null;   //  (no solution)
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
        /// Find the polygon's centroid.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static Point2D FindCentroid(this Polygon polygon)
        {
            // Add the first point at the end of the array.
            int num_points = polygon.Points.Count;
            var pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y
                    - pts[i + 1].X * pts[i].Y;
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
        /// Return true if the polygon is oriented clockwise.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static bool PolygonIsOrientedClockwise(this Polygon polygon)
            => (SignedPolygonArea(polygon) < 0);

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
        /// <returns>
        /// Return the absolute value of the signed area.
        /// The signed area is negative if the polygon is
        /// oriented clockwise.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static double PolygonArea(this Polygon polygon)
            => Abs(SignedPolygonArea(polygon));

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
            var pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Get the areas.
            double area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X)
                    * (pts[i + 1].Y + pts[i].Y) / 2;
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
                    got_negative = true;
                else
                    got_positive |= cross_product > 0;
                if (got_negative && got_positive)
                    return false;
            }

            // If we got this far, the polygon is convex.
            return true;
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

                if (FormsEar(polygon.Points.ToArray(), A, B, C))
                    return;
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
            if (AngleVector(
                points[A].X, points[A].Y,
                points[B].X, points[B].Y,
                points[C].X, points[C].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            var triangle = new Triangle(
                points[A], points[B], points[C]);

            // Check the other points to see 
            // if they lie in triangle A, B, C.
            for (int i = 0; i < points.Length; i++)
            {
                if ((i != A) && (i != B) && (i != C) && triangle.Contains(points[i]))
                {
                    // This point is in the triangle 
                    // do this is not an ear.
                    return false;
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
            RemovePoint2DFromArray(polygon, B);
        }

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void RemovePoint2DFromArray(this Polygon polygon, int target)
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
            var pts = new Point2D[polygon.Points.Count];
            Array.Copy(polygon.Points.ToArray(), pts, polygon.Points.Count);

            // Make a scratch polygon.
            var pgon = new Polygon(new List<Point2D>(pts));

            // Orient the polygon clockwise.
            pgon.OrientPolygonClockwise();

            // Make room for the triangles.
            var triangles = new List<Triangle>();

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
        private static void ResetBoundingRect(this Polygon polygon, BoundingRectPolygon boundingRect)
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
        private static void FindInitialControlPoints(this Polygon polygon, BoundingRectPolygon boundingRect)
        {
            for (int i = 0; i < boundingRect.NumPoints; i++)
            {
                if (CheckInitialControlPoints(polygon, boundingRect, i))
                    return;
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
        private static bool CheckInitialControlPoints(this Polygon polygon, BoundingRectPolygon boundingRect, int i)
        {
            // Get the i -> i + 1 unit vector.
            int i1 = (i + 1) % boundingRect.NumPoints;
            double vix = polygon.Points[i1].X - polygon.Points[i].X;
            double viy = polygon.Points[i1].Y - polygon.Points[i].Y;

            // The candidate control point indexes.
            for (int num = 0; num < 4; num++)
                boundingRect.ControlPoints[num] = i;

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
            if (boundingRect.ControlPoints[0] == i)
                return false;

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
            if (boundingRect.ControlPoints[2] == i)
                return false;

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
            if (boundingRect.ControlPoints[0] == i)
                return false;

            // These control points work.
            return true;
        }

        /// <summary>
        /// Find the next bounding rectangle and check it.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static void CheckNextRectangle(this Polygon polygon, BoundingRectPolygon boundingRect)
        {
            // Increment the current control point.
            // This means we are done with using this edge.
            if (boundingRect.CurrentControlPoint >= 0)
            {
                boundingRect.ControlPoints[boundingRect.CurrentControlPoint] =
                    (boundingRect.ControlPoints[boundingRect.CurrentControlPoint] + 1) % boundingRect.NumPoints;
            }

            // Find the next point on an edge to use.
            Point2D d0 = FindDxDy(polygon, boundingRect, boundingRect.ControlPoints[0]);
            Point2D d1 = FindDxDy(polygon, boundingRect, boundingRect.ControlPoints[1]);
            Point2D d2 = FindDxDy(polygon, boundingRect, boundingRect.ControlPoints[2]);
            Point2D d3 = FindDxDy(polygon, boundingRect, boundingRect.ControlPoints[3]);

            // Switch so we can look for the smallest opposite/adjacent ratio.
            double opp0 = d0.X;
            double adj0 = d0.Y;
            double opp1 = -d1.Y;
            double adj1 = d1.X;
            double opp2 = -d2.X;
            double adj2 = -d2.Y;
            double opp3 = d3.Y;
            double adj3 = -d3.X;

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
        private static void FindBoundingRectangle(this Polygon polygon, BoundingRectPolygon boundingRect)
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
            boundingRect.CurrentRectangle[0] = Intersections.LineLine(px0, py0, px0 + dx0, py0 + dy0, px1, py1, px1 + dx1, py1 + dy1).Item2;
            boundingRect.CurrentRectangle[1] = Intersections.LineLine(px1, py1, px1 + dx1, py1 + dy1, px2, py2, px2 + dx2, py2 + dy2).Item2;
            boundingRect.CurrentRectangle[2] = Intersections.LineLine(px2, py2, px2 + dx2, py2 + dy2, px3, py3, px3 + dx3, py3 + dy3).Item2;
            boundingRect.CurrentRectangle[3] = Intersections.LineLine(px3, py3, px3 + dx3, py3 + dy3, px0, py0, px0 + dx0, py0 + dy0).Item2;

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
        /// <param name="i"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        private static Point2D FindDxDy(this Polygon polygon, BoundingRectPolygon boundingRect, int i)
        {
            int i2 = (i + 1) % boundingRect.NumPoints;
            return new Point2D(
                polygon.Points[i2].X - polygon.Points[i].X,
                polygon.Points[i2].Y - polygon.Points[i].Y);
        }

        /// <summary>
        /// Find a smallest bounding rectangle.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="boundingRect"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static Point2D[] FindSmallestBoundingRectangle(this Polygon polygon, BoundingRectPolygon boundingRect)
        {
            // This algorithm assumes the polygon
            // is oriented counter-clockwise.
            Debug.Assert(!PolygonIsOrientedClockwise(polygon));

            // Get ready;
            ResetBoundingRect(polygon, boundingRect);

            // Check all possible bounding rectangles.
            for (int i = 0; i < polygon.Points.Count; i++)
                CheckNextRectangle(polygon, boundingRect);

            // Return the best result.
            return boundingRect.BestRectangle;
        }

        #region SutherlandHodgman algorithm's Methods

        /// <summary>
        /// This iterates through the edges of the polygon, always clockwise
        /// </summary>
        public static IEnumerable<LineSegment> IterateEdgesClockwise(List<Point2D> polygon)
        {
            if (IsClockwise(polygon))
            {
                #region Already clockwise

                for (int cntr = 0; cntr < polygon.Count - 1; cntr++)
                    yield return new LineSegment(polygon[cntr], polygon[cntr + 1]);

                yield return new LineSegment(polygon[polygon.Count - 1], polygon[0]);

                #endregion
            }
            else
            {
                #region Reverse

                for (int cntr = polygon.Count - 1; cntr > 0; cntr--)
                    yield return new LineSegment(polygon[cntr], polygon[cntr - 1]);

                yield return new LineSegment(polygon[0], polygon[polygon.Count - 1]);

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInside(LineSegment edge, Point2D test)
        {
            bool? isLeft = IsLeftOf(edge, test);
            if (isLeft == null)
            {
                //	Collinear points should be considered inside
                return true;
            }

            return !isLeft.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsClockwise(List<Point2D> polygon)
        {
            for (int cntr = 2; cntr < polygon.Count; cntr++)
            {
                bool? isLeft = IsLeftOf(new LineSegment(polygon[0], polygon[1]), polygon[cntr]);
                if (isLeft != null)     //	some of the points may be collinear.  That's ok as long as the overall is a polygon
                    return !isLeft.Value;
            }

            throw new ArgumentException("All the points in the polygon are collinear");
        }

        /// <summary>
        /// Tells if the test point lies on the left side of the edge line
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool? IsLeftOf(LineSegment edge, Point2D test)
        {
            Vector2D tmp1 = edge.B - edge.A;
            Vector2D tmp2 = test - edge.B;

            double x = (tmp1.I * tmp2.J) - (tmp1.J * tmp2.I);
            // dot product of perpendicular?

            if (x < 0)
                return false;
            else if (x > 0)
                return true;
            // Collinear points;
            else
                return null;
        }

        #endregion
    }
}
