using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using static Engine.Geometry.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Intersections
    {
        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </remarks>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside Contains(this Circle circle, Point2D point)
        {
            // Check if it is within the bounding rectangle.
            if (point.X >= circle.X - circle.Radius && point.X <= circle.X + circle.Radius &&
                point.Y >= circle.Y - circle.Radius && point.Y <= circle.Y + circle.Radius)
            {
                double dx = circle.X - point.X;
                double dy = circle.Y - point.Y;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = circle.Radius * circle.Radius;
                return (radiusSquared >= distanceSquared) ? ((radiusSquared == distanceSquared) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
            }

            return InsideOutside.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside Contains(this Ellipse ellipse, Point2D point)
        {
            if (ellipse.R1 <= 0d || ellipse.R2 <= 0d) return InsideOutside.Outside;

            double cosT = Cos(-ellipse.Angle);
            double sinT = Sin(-ellipse.Angle);

            double u = point.X - ellipse.Center.X;
            double v = point.Y - ellipse.Center.Y;

            double a = (cosT * u + sinT * v) * (cosT * u + sinT * v);
            double b = (sinT * u - cosT * v) * (sinT * u - cosT * v);

            double d1Squared = 4 * ellipse.R1 * ellipse.R1;
            double d2Squared = 4 * ellipse.R2 * ellipse.R2;

            double normalizedRadius = (a / d1Squared)
                                    + (b / d2Squared);

            return (normalizedRadius <= 1d) ? ((normalizedRadius == 1d) ? InsideOutside.Boundary : InsideOutside.Inside) : InsideOutside.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        //[DebuggerStepThrough]
        public static InsideOutside Contains(this Rectangle2D rectangle, Point2D point)
        {
            if (((rectangle.X == point.X || rectangle.Bottom == point.X) && ((rectangle.Y <= point.Y) == (rectangle.Bottom >= point.Y)))
             || ((rectangle.Right == point.Y || rectangle.Left == point.Y) && ((rectangle.X <= point.X) == (rectangle.Right >= point.X))))
            {
                return InsideOutside.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? InsideOutside.Inside : InsideOutside.Outside;
        }

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rect2"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        [Pure]
        public static bool Contains(this Rectangle2D rect1, Rectangle2D rect2) => (rect1.X <= rect2.X)
    && ((rect2.X + rect2.Width) <= (rect1.X + rect1.Width))
    && (rect1.Y <= rect2.Y)
    && ((rect2.Y + rect2.Height) <= (rect1.Y + rect1.Height));

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        public static InsideOutside Contains(this Polygon polygon, Point2D point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            InsideOutside result = InsideOutside.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior. 
            if (polygon.Points.Count < 3) return InsideOutside.Outside;
            Point2D curPoint = polygon[0];
            for (int i = 1; i <= polygon.Points.Count; ++i)
            {
                Point2D nextPoint = (i == polygon.Points.Count ? polygon[0] : polygon[i]);
                if (nextPoint.Y == point.Y)
                {
                    if ((nextPoint.X == point.X) || (curPoint.Y == point.Y
                    && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return InsideOutside.Boundary;
                    }
                }

                if ((curPoint.Y < point.Y) != (nextPoint.Y < point.Y))
                {
                    if (curPoint.X >= point.X)
                    {
                        if (nextPoint.X > point.X)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                                return InsideOutside.Boundary;
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y)) result = 1 - result;
                        }
                    }
                    else
                    {
                        if (nextPoint.X > point.X)
                        {
                            double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                            if (determinant == 0)
                                return InsideOutside.Boundary;
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y)) result = 1 - result;
                        }
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="PolygonSet"/>.
        /// </summary>
        /// <param name="polygons"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>This function automatically knows that enclosed polygons are "no-go" areas.</remarks>
        [Pure]
        //[DebuggerStepThrough]
        public static bool Contains(this PolygonSet polygons, Point2D point)
        {
            bool returnValue = false;

            foreach (Polygon poly in polygons.Polygons)
                returnValue = !poly.Points.Contains(point);

            return returnValue;
        }

        /// <summary>
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="allPolys"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool Contains(this PolygonSet allPolys, Point2D start, Point2D end)
        {
            int i;
            int j;
            double sX;
            double sY;
            double eX;
            double eY;
            double rotSX;
            double rotSY;
            double rotEX;
            double rotEY;
            double crossX;

            end.X -= start.X;
            end.Y -= start.Y;
            double dist = Sqrt(end.X * end.X + end.Y * end.Y);
            double theCos = end.X / dist;
            double theSin = end.Y / dist;
            foreach (Polygon poly in allPolys.Polygons)
            {
                for (i = 0; i < poly.Points.Count; i++)
                {
                    j = i + 1;
                    if (j == poly.Points.Count) j = 0;

                    sX = poly.Points[i].X - start.X;
                    sY = poly.Points[i].Y - start.Y;
                    eX = poly.Points[j].X - start.X;
                    eY = poly.Points[j].Y - start.Y;
                    if (sX == 0.0 && sY == 0.0 && eX == end.X && eY == end.Y
                    || eX == 0.0 && eY == 0.0 && sX == end.X && sY == end.Y)
                    {
                        return true;
                    }

                    rotSX = sX * theCos + sY * theSin;
                    rotSY = sY * theCos - sX * theSin;
                    rotEX = eX * theCos + eY * theSin;
                    rotEY = eY * theCos - eX * theSin;
                    if (rotSY < 0.0 && rotEY > 0.0
                    || rotEY < 0.0 && rotSY > 0.0)
                    {
                        crossX = rotSX + (rotEX - rotSX) * (0.0 - rotSY) / (rotEY - rotSY);
                        if (crossX >= 0.0 && crossX <= dist) return false;
                    }

                    if (rotSY == 0.0 && rotEY == 0.0
                    && (rotSX >= 0.0 || rotEX >= 0.0)
                    && (rotSX <= dist || rotEX <= dist)
                    && (rotSX < 0.0 || rotEX < 0.0
                    || rotSX > dist || rotEX > dist))
                    {
                        return false;
                    }
                }
            }

            return allPolys.Contains(new Point2D(start.X + end.X / 2.0, start.Y + end.Y / 2.0));
        }

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        [Pure]
        public static bool RectangleRectangle(Rectangle2D rect1, Rectangle2D rect2) => (rect2.X < rect1.X + rect1.Width)
    && (rect1.X < (rect2.X + rect2.Width))
    && (rect2.Y < rect1.Y + rect1.Height)
    && (rect1.Y < rect2.Y + rect2.Height);

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
        private static Tuple<int, Point2D, Point2D> CircleCircle(
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
                return new Tuple<int, Point2D, Point2D>(0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return new Tuple<int, Point2D, Point2D>(0, intersection1, intersection2);
            }
            else if ((dist == 0) && (radius0 == radius1))
            {
                // No solutions, the circles coincide.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return new Tuple<int, Point2D, Point2D>(0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
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
                if (dist == radius0 + radius1)
                    return new Tuple<int, Point2D, Point2D>(1, intersection1, intersection2);

                return new Tuple<int, Point2D, Point2D>(2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/</remarks>
        [Pure]
        private static Tuple<int, Point2D, Point2D> CircleLine(
            double centerX, double centerY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            double t;

            double dx = x2 - x1;
            double dy = y2 - y1;

            double A = dx * dx + dy * dy;
            double B = 2 * (dx * (x1 - centerX) + dy * (y1 - centerY));
            double C = (x1 - centerX) * (x1 - centerX) + (y1 - centerY) * (y1 - centerY) - radius * radius;

            Point2D intersection1;
            Point2D intersection2;

            double determinant = B * B - 4 * A * C;

            if ((A <= 0.0000001) || (determinant < 0))
            {
                // No real solutions.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return new Tuple<int, Point2D, Point2D>(0, intersection1, intersection2);
            }
            else if (determinant == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new Point2D(x1 + t * dx, y1 + t * dy);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return new Tuple<int, Point2D, Point2D>(1, intersection1, intersection2);
            }
            else
            {
                // Two solutions.
                t = ((-B + Sqrt(determinant)) / (2 * A));
                intersection1 = new Point2D(x1 + t * dx, y1 + t * dy);
                t = ((-B - Sqrt(determinant)) / (2 * A));
                intersection2 = new Point2D(x1 + t * dx, y1 + t * dy);
                return new Tuple<int, Point2D, Point2D>(2, intersection1, intersection2);
            }
        }

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
        public static Tuple<bool, Point2D> LineLine(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Calculate the delta length vectors for the line segments.
            double deltaAI = (x1 - x0);
            double deltaAJ = (y1 - y0);
            double deltaBI = (x3 - x2);
            double deltaBJ = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (determinant == 0) return new Tuple<bool, Point2D>(false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * deltaAJ + (y2 - y0) * deltaAI) / -determinant;
            double t = ((x2 - x0) * deltaBJ + (y0 - y2) * deltaBI) / determinant;

            return new Tuple<bool, Point2D>(
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Point2D(x0 + t * deltaAI, y0 + t * deltaAJ));
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
        public static List<Point2D> PolygonPolygon(List<Point2D> subjectPoly, List<Point2D> clipPoly)
        {
            if (subjectPoly.Count < 3 || clipPoly.Count < 3)
                throw new ArgumentException(string.Format("The polygons passed in must have at least 3 points: subject={0}, clip={1}", subjectPoly.Count.ToString(), clipPoly.Count.ToString()));

            // clone it
            List<Point2D> outputList = subjectPoly.ToList();

            // Make sure it's clockwise
            if (!IsClockwise(subjectPoly))
                outputList.Reverse();

            // Walk around the clip polygon clockwise
            foreach (LineSegment clipEdge in IterateEdgesClockwise(clipPoly))
            {
                // clone it
                List<Point2D> inputList = outputList.ToList();
                outputList.Clear();

                // Sometimes when the polygons don't intersect, this list goes to zero.
                // Jump out to avoid an index out of range exception
                if (inputList.Count == 0) break;

                Point2D S = inputList[inputList.Count - 1];

                foreach (Point2D E in inputList)
                {
                    if (IsInside(clipEdge, E))
                    {
                        if (!IsInside(clipEdge, S))
                        {
                            Tuple<bool, Point2D> point = Intersections.LineLine(S.X, S.Y, E.X, E.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                            if (point == null)
                            {
                                // may be collinear, or may be a bug
                                throw new ApplicationException("Line segments don't intersect");
                            }
                            else
                            {
                                outputList.Add(point.Item2);
                            }
                        }

                        outputList.Add(E);
                    }
                    else if (IsInside(clipEdge, S))
                    {
                        Tuple<bool, Point2D> point = Intersections.LineLine(S.X, S.Y, E.X, E.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                        if (point == null)
                        {
                            // may be collinear, or may be a bug
                            throw new ApplicationException("Line segments don't intersect");
                        }
                        else
                        {
                            outputList.Add(point.Item2);
                        }
                    }

                    S = E;
                }
            }

            // Exit Function
            return outputList;
        }

        #region SutherlandHodgman Private Methods

        /// <summary>
        /// This iterates through the edges of the polygon, always clockwise
        /// </summary>
        private static IEnumerable<LineSegment> IterateEdgesClockwise(List<Point2D> polygon)
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
        private static bool IsInside(LineSegment edge, Point2D test)
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
        private static bool IsClockwise(List<Point2D> polygon)
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
        private static bool? IsLeftOf(LineSegment edge, Point2D test)
        {
            Vector2D tmp1 = edge.B - edge.A;
            Vector2D tmp2 = test - edge.B;

            double x = (tmp1.I * tmp2.J) - (tmp1.J * tmp2.I);
            // dot product of perpendicular?

            if (x < 0) return false;
            else if (x > 0) return true;
            // Collinear points;
            else return null;
        }

        #endregion
    }
}
