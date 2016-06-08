using Engine.Geometry;
using Engine.Geometry.Polygons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Experimental
    {
        #region Intersection of Point and Line

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Point"></param>
        /// <param name="Line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointOnLine(Point2D Point, LineSegment Line)
        {
            double Length1 = Point.Length(Line.B);
            // Sqrt((Point.X - Line.B.X) ^ 2 + (Point.Y - Line.B.Y))
            double Length2 = Point.Length(Line.A);
            // Sqrt((Point.X - Line.A.X) ^ 2 + (Point.Y - Line.A.Y))
            return Line.Length() == Length1 + Length2;
        }

        /// <summary>
        /// Calculate the distance between the point and the segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="RetNear"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double DistanceToSegment(Point2D p, Point2D A, Point2D B, out Point2D RetNear)
        {
            RetNear = new Point2D();
            Point2D Delta = new Point2D((B.X - A.X), (B.Y - A.Y));
            if (((Delta.X == 0) && (Delta.Y == 0)))
            {
                //  It's a point not a line segment.
                Delta.X = (p.X - A.X);
                Delta.Y = (p.Y - A.Y);
                RetNear.X = A.X;
                RetNear.Y = A.Y;
                return (Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
            }
            //  Calculate the t that minimizes the distance.
            double t = ((((p.X - A.X) * Delta.X) + ((p.Y - A.Y) * Delta.Y)) / ((Delta.X * Delta.X) + (Delta.Y * Delta.Y)));
            if ((t < 0))
            {
                Delta.X = (p.X - A.X);
                Delta.Y = (p.Y - A.Y);
                RetNear.X = A.X;
                RetNear.Y = A.Y;
            }
            else if ((t > 1))
            {
                Delta.X = (p.X - B.X);
                Delta.Y = (p.Y - B.Y);
                RetNear.X = B.X;
                RetNear.Y = B.Y;
            }
            else
            {
                RetNear.X = (A.X + (t * Delta.X));
                RetNear.Y = (A.Y + (t * Delta.Y));
                Delta.X = (p.X - RetNear.X);
                Delta.Y = (p.Y - RetNear.Y);
            }
            return (Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double DistToSegment2(double px, double py, double x1, double y1, double x2, double y2)
        {
            double dx;
            double dy;
            double t;
            dx = (x2 - x1);
            dy = (y2 - y1);
            if (((dx == 0) && (dy == 0)))
            {
                //  It's a point not a line segment.
                dx = (px - x1);
                dy = (py - y1);
                return Sqrt(((dx * dx) + (dy * dy)));

            }
            t = ((px + (py - (x1 - y1))) / (dx + dy));
            if ((t < 0))
            {
                dx = (px - x1);
                dy = (py - y1);
            }
            else if ((t > 1))
            {
                dx = (px - x2);
                dy = (py - y2);
            }
            else
            {
                double x3 = (x1 + (t * dx));
                double y3 = (y1 + (t * dy));
                dx = (px - x3);
                dy = (py - y3);
            }
            return Sqrt(((dx * dx) + (dy * dy)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns>Return True if (px, py) is within close_distance if the segment from (x1, y1) to (X2, y2).</returns>
        public static bool PointNearSegment(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            return (DistToSegment2(px, py, x1, y1, x2, y2) <= close_distance);
        }

        /// <summary>
        /// Return True if (px, py) is within close_distance if the segment from (x1, y1) to (X2, y2).
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointNearSegment2(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            return (DistToSegment(px, py, x1, y1, x2, y2) <= close_distance);
        }

        /// <summary>
        /// Calculate the distance between the point and the segment.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double DistToSegment(double px, double py, double x1, double y1, double x2, double y2)
        {
            double dx = (x2 - x1);
            double dy = (y2 - y1);
            if (((dx == 0) && (dy == 0)))
            {
                //  It's a point not a line segment.
                dx = (px - x1);
                dy = (py - y1);
                return Sqrt(((dx * dx) + (dy * dy)));
            }
            double t = ((px + (py - (x1 - y1))) / (dx + dy));
            if ((t < 0))
            {
                dx = (px - x1);
                dy = (py - y1);
            }
            else if ((t > 1))
            {
                dx = (px - x2);
                dy = (py - y2);
            }
            else
            {
                double x3 = (x1 + (t * dx));
                double y3 = (y1 + (t * dy));
                dx = (px - x3);
                dy = (py - y3);
            }
            return Sqrt(((dx * dx) + (dy * dy)));
        }

        #endregion

        #region Circle Properties

        /// <summary>
        /// Find the Center of A Circle from Three Points
        /// </summary>
        /// <param name="pointA">First Point on the Ellipse</param>
        /// <param name="pointB">Second Point on the Ellipse</param>
        /// <param name="pointC">Last Point on the Ellipse</param>
        /// <returns>Returns the Center point of a Circle defined by three points</returns>
        /// <remarks>
        /// </remarks>
        public static Point2D TripointCircleCenter(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            //  Calculate the slopes of the lines.
            double slopeA = (pointA.Slope(pointB));
            double slopeB = (pointC.Slope(pointB));
            double fY = ((((pointA.X - pointB.X) * (pointA.X + pointB.X)) + ((pointA.Y - pointB.Y) * (pointA.Y + pointB.Y))) / (2 * (pointA.X - pointB.X)));
            double fX = ((((pointC.X - pointB.X) * (pointC.X + pointB.X)) + ((pointC.Y - pointB.Y) * (pointC.Y + pointB.Y))) / (2 * (pointC.X - pointB.X)));
            double newY = ((fX - fY) / (slopeB - slopeA));
            double newX = (fX - (slopeB * newY));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// Find the Bounds of A Circle from Three Points 
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>A Rectangle Representing the bounds of A Circle Defined from three 
        /// Points</returns>
        public static Rectangle2D TripointCircleBounds(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            Point2D Center = TripointCircleCenter(PointA, PointB, PointC);
            double Radius = (Center.Length(PointA));
            Rectangle2D Bounds = Rectangle2D.FromLTRB((Center.X - Radius), (Center.Y - Radius), (Center.X + Radius), (Center.Y + Radius));
            return Bounds;
        }

        #endregion

        #region Linear Offset Interpolation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value1"></param>
        /// <param name="Value2"></param>
        /// <param name="Offset"></param>
        /// <param name="Weight"></param>
        /// <returns></returns>
        public static Point2D OffsetInterpolate(Point2D Value1, Point2D Value2, double Offset, double Weight)
        {
            Vector2D UnitVectorAB = new Vector2D(Value1, Value2);
            Vector2D PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Maths.LinearInterpolate(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }

        #endregion

        #region Cubic Bezier Get T

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Lut"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218</remarks>
        public static List<double> findTforCoordinate(Point2D value, List<Point2D> Lut)
        {
            Point2D point = new Point2D();
            List<double> found = new List<double>();
            for (int i = 0, len = Lut.Count; i < len; i++)
            {
                point.X = Lut[i].X;
                point.Y = Lut[i].Y;
                if (value.X == point.X && value.Y == point.Y)
                {
                    found.Add(i / len);
                }
            }
            return found;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218</remarks>
        public static List<Point2D> buildLUT(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            List<Point2D> Lut = new List<Point2D>(100);
            for (double t = 0; t <= 1; t += 0.01)
            {
                Lut[(int)(t * 100)] = new Point2D(Interpolaters.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t));
            }
            return Lut;
        }

        #endregion

        #region Quadratic Bezier Length approximations

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <returns></returns>
        public static double QuadraticBezierArcLengthByIntegral(this QuadraticBezier bezier)
        {
            return QuadraticBezierArcLengthByIntegral(bezier.A, bezier.B, bezier.C);
        }

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        public static double QuadraticBezierArcLengthByIntegral(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double ax = pointA.X - 2 * pointB.X + pointC.X;
            double ay = pointA.Y - 2 * pointB.Y + pointC.Y;
            double bx = 2 * pointB.X - 2 * pointA.X;
            double by = 2 * pointB.Y - 2 * pointA.Y;

            double a = 4 * (ax * ax + ay * ay);
            double b = 4 * (ax * bx + ay * by);
            double c = bx * bx + by * by;

            double abc = 2 * Sqrt(a + b + c);
            double a2 = Sqrt(a);
            double a32 = 2 * a * a2;
            double c2 = 2 * Sqrt(c);
            double ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4 * c * a - b * b) * Log((2 * a2 + ba + abc) / (ba + c2))) / (4 * a32);
        }

        /// <summary>
        /// Naive computation of arc length by summing up small segment lengths.
        /// </summary>
        /// <returns></returns>
        public static double QuadraticBezierArcLengthBySegments(this QuadraticBezier bezier)
        {
            return QuadraticBezierArcLengthBySegments(bezier.A, bezier.B, bezier.C);
        }

        /// <summary>
        /// Naive computation of arc length by summing up small segment lengths.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        private static double QuadraticBezierArcLengthBySegments(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double length = 0;
            Point2D p = new Point2D(Interpolaters.QuadraticBezier(pointA.X, pointA.Y, pointB.X, pointB.Y, pointC.X, pointC.Y, 0));
            double prevX = p.X;
            double prevY = p.Y;
            for (double t = 0.005; t <= 1.0; t += 0.005)
            {
                p = new Point2D(Interpolaters.QuadraticBezier(pointA.Y, pointA.X, pointB.Y, pointB.X, pointC.X, pointC.Y, t));
                double deltaX = p.X - prevX;
                double deltaY = p.Y - prevY;
                length += Sqrt(deltaX * deltaX + deltaY * deltaY);

                prevX = p.X;
                prevY = p.Y;
            }

            // exercise:  due to roundoff, it's possible to miss a small segment at the end.  how to compensate??
            return length;
        }

        /// <summary>
        /// Approximate arc-length by Gauss-Legendre numerical integration.
        /// </summary>
        /// <returns></returns>
        public static double QuadraticBezierApproxArcLength(this QuadraticBezier bezier)
        {
            return QuadraticBezierApproxArcLength(bezier.A, bezier.B, bezier.C);
        }

        /// <summary>
        /// Approximate arc-length by Gauss-Legendre numerical integration.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// https://code.google.com/archive/p/degrafa/source/default/source
        /// </remarks>
        private static double QuadraticBezierApproxArcLength(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double sum = 0;

            // Compute the quadratic bezier polynomial coefficients.
            double coeff0X = pointA.X;
            double coeff0Y = pointA.Y;
            double coeff1X = 2.0 * (pointB.X - pointA.X);
            double coeff1Y = 2.0 * (pointB.Y - pointA.Y);
            double coeff2X = pointA.X - 2.0 * pointB.X + pointC.X;
            double coeff2Y = pointA.Y - 2.0 * pointB.Y + pointC.Y;

            // Count should be between 2 and 8 to align with MathExtensions.abscissa and MathExtensions.weight.
            int count = 5;
            int startl = (count == 2) ? 0 : count * (count - 1) / 2 - 1;

            // Minimum, maximum, and scalers. 
            double min = 0;
            double max = 1;
            double mult = 0.5 * (max - min);
            double ab2 = 0.5 * (min + max);

            double theta = 0;
            double xPrime = 0;
            double yPrime = 0;
            double integrand = 0;

            // Evaluate the integral arc length along entire curve from t=0 to t=1.
            for (int index = 0; index < count; ++index)
            {
                theta = ab2 + mult * Maths.abscissa[startl + index];

                // First-derivative of the quadratic bezier.
                xPrime = coeff1X + 2.0 * coeff2X * theta;
                yPrime = coeff1Y + 2.0 * coeff2Y * theta;

                // Integrand for Gauss-Legendre numerical integration.
                integrand = Sqrt(xPrime * xPrime + yPrime * yPrime);

                sum += integrand * Maths.weight[startl + index];
            }

            return mult == 0 ? sum : mult * sum;
        }

        #endregion

        #region Self Intersecting Bezier
        // https://github.com/Parclytaxel/Kinross/blob/master/kinback/segment.py
        #endregion

        #region Is Convex

        /// <summary>
        /// For each set of three adjacent points A, B, C,
        /// find the dot product AB · BC. If the sign of
        /// all the dot products is the same, the angles
        /// are all positive or negative (depending on the
        /// order in which we visit them) so the polygon
        /// is convex.
        /// </summary>
        /// <returns>
        /// Return true if the polygon is convex.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-polygon-is-convex-in-c/</remarks>
        public static bool IsConvex(this Polygon polygon)
        {
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

        #endregion

        #region Find Ear

        /// <summary>
        /// Find the indexes of three points that form an "ear."
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static Triangle FindEar(this Polygon polygon, ref int A, ref int B, ref int C)
        {
            int num_points = polygon.Points.Count;

            for (A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                if (FormsEar(polygon, A, B, C)) return new Triangle(polygon.Points[A], polygon.Points[B], polygon.Points[C]);
            }

            // We should never get here because there should
            // always be at least two ears.
            Debug.Assert(false);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns>Return true if the three points form an ear.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        private static bool FormsEar(this Polygon polygon, int A, int B, int C)
        {
            // See if the angle ABC is concave.
            if (Maths.AngleVector(
                polygon.Points[A].X, polygon.Points[A].Y,
                polygon.Points[B].X, polygon.Points[B].Y,
                polygon.Points[C].X, polygon.Points[C].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            Triangle triangle = new Triangle(polygon.Points[A], polygon.Points[B], polygon.Points[C]);

            // Check the other points to see 
            // if they lie in triangle A, B, C.
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                if ((i != A) && (i != B) && (i != C))
                {
                    if (triangle.PointInPolygon(polygon.Points[i]))
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

        #endregion

        #region Misc

        /// <summary>
        /// rectHasNaN - this returns true if this rectangle has X, Y , Height or Width as NaN.
        /// </summary>
        /// <param name='rect'>The rectangle to test</param>
        /// <returns>returns whether the Rectangle has NaN</returns>        
        public static bool RectHasNaN(this Rectangle2D rect)
        {
            if (double.IsNaN(rect.X)
                 || double.IsNaN(rect.Y)
                 || double.IsNaN(rect.Height)
                 || double.IsNaN(rect.Width))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove an ear from the polygon and add it to the triangles array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="triangles"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void RemoveEar(this Polygon polygon, List<Triangle> triangles)
        {
            // Find an ear.
            int A = 0;
            int B = 0;
            int C = 0;

            // Create a new triangle for the ear.
            triangles.Add(polygon.FindEar(ref A, ref B, ref C));

            // Remove the ear from the polygon.
            polygon.RemovePoint(B);
        }

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void RemovePoint(this Polygon polygon, int target)
        {
            polygon.Points.RemoveAt(target);
            //List<Point2D> points =  new List<Point2D>(polygon.Points.Count - 1);
            //List.Copy(polygon.Points, 0, points, 0, target);
            //Array.Copy(polygon.Points, target + 1, points, target, polygon.Points.Count - target - 1);
            //polygon.Points = points;
        }

        /// <summary>
        /// Triangulate the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// For a nice, detailed explanation of this method,
        /// see Ian Garton's Web page:
        /// http://www-cgrl.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/cutting_ears.html
        /// </remarks>
        public static List<Triangle> Triangulate(this Polygon polygon)
        {
            // Copy the points into a scratch array.
            List<Point2D> pts = new List<Point2D>(polygon.Points);

            // Make a scratch polygon.
            Polygon pgon = new Polygon(pts);

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
        /// If the polygon is oriented counterclockwise,
        /// reverse the order of its points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void OrientPolygonClockwise(this Polygon polygon)
        {
            if (!polygon.PolygonIsOrientedClockwise())
                polygon.Points.Reverse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T swap = a;
            a = b;
            b = swap;
        }

        /// <summary>
        /// Retrieve Cursor Resource from Executable
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        /// <remarks>BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!</remarks>
        /// <history>
        /// Shkyrockett[Alma Jenks]    9/January/2006    Created
        /// </history>
        public static System.Windows.Forms.Cursor RetriveCursorResource(string ResourceName)
        {
            //  Get the namespace 
            string strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            //  Get the resource into a stream 
            System.IO.Stream ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream((strNameSpace + ("." + ResourceName)));
            if ((ResourceStream == null))
            {
                // TODO: #If Then ... Warning!!! not translated
                System.Windows.Forms.MessageBox.Show(("Unable to find: "
                                + (ResourceName + ("\r\n" + ("Be Sure "
                                + (ResourceName + (" Property Build Action is set to Embedded Resource" + ("\r\n" + "Another reason can be that the Project Root Namespace is not the same as the Assembly Name"))))))));
                // TODO: # ... Warning!!! not translated
            }
            else
            {
                //  ToDo: Report the Error message in a nicer fashion since this in game. 
                //  Perhaps on Exit provide a message errors were encountered and 
                //  ignored would you like to send an error report?
                // TODO: #End If ... Warning!!! not translated
                return System.Windows.Forms.Cursors.Default;
            }
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
            {
                return new System.Windows.Forms.Cursor(ResourceStream);
            }
            else
            {
                return System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Size2D fitRect(Size2D size, double radians)
        {
            double angleCos = Cos(radians);
            double angleSin = Sin(radians);

            double x1 = -size.Width * 0.5f;
            double x2 = size.Width * 0.5f;
            double x3 = size.Width * 0.5f;
            double x4 = -size.Width * 0.5f;

            double y1 = size.Height * 0.5f;
            double y2 = size.Height * 0.5f;
            double y3 = -size.Height * 0.5f;
            double y4 = -size.Height * 0.5f;

            double x11 = (x1 * angleCos) + (y1 * angleSin);
            double y11 = (-x1 * angleSin) + (y1 * angleCos);

            double x21 = (x2 * angleCos) + (y2 * angleSin);
            double y21 = (-x2 * angleSin) + (y2 * angleCos);

            double x31 = (x3 * angleCos) + (y3 * angleSin);
            double y31 = (-x3 * angleSin) + (y3 * angleCos);

            double x41 = (x4 * angleCos) + (y4 * angleSin);
            double y41 = (-x4 * angleSin) + (y4 * angleCos);

            double x_min = Min(Min(x11, x21), Min(x31, x41));
            double x_max = Max(Max(x11, x21), Max(x31, x41));

            double y_min = Min(Min(y11, y21), Min(y31, y41));
            double y_max = Max(Max(y11, y21), Max(y31, y41));

            return new Size2D((x_max - x_min), (y_max - y_min));
        }

        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.  
        /// </summary>
        /// <param name="center">The point around which to rotate.</param>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix2D RotateAroundPoint(Point2D center, double angle)
        {
            // Translate the point to the origin.
            Matrix2D result = new Matrix2D();

            // We need to go counter-clockwise.
            result.RotateAt(-angle.ToDegrees(), center.X, center.Y);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static double GetAngle(this Point2D pt)
        {
            return Atan2(pt.X, -pt.Y) * 180 / PI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Point2D SetAngle(this Point2D pt, double angle)
        {
            var rads = angle * (PI / 180);
            var dist = Sqrt(pt.X * pt.X + pt.Y * pt.Y);
            pt.X = Sin(rads) * dist;
            pt.Y = -(Cos(rads) * dist);
            return pt;
        }

        /// <summary>
        /// Find the Center of A Circle from Three Points
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>Returns the Center point of a Circle defined by three points</returns>
        public static Point2D TripointArcCenter(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            //  Calculate the slopes of the lines.
            double SlopeA = (PointA.Slope(PointB));
            double SlopeB = (PointC.Slope(PointB));
            double FY = ((((PointA.X - PointB.X) * (PointA.X + PointB.X)) + ((PointA.Y - PointB.Y) * (PointA.Y + PointB.Y))) / (2 * (PointA.X - PointB.X)));
            double FX = ((((PointC.X - PointB.X) * (PointC.X + PointB.X)) + ((PointC.Y - PointB.Y) * (PointC.Y + PointB.Y))) / (2 * (PointC.X - PointB.X)));
            double NewY = ((FX - FY) / (SlopeB - SlopeA));
            double NewX = (FX - (SlopeB * NewY));
            return new Point2D(NewX, NewY);
        }

        /// <summary>
        /// Find the Bounds of A Circle from Three Points 
        /// </summary>
        /// <param name="PointA">First Point on the Ellipse</param>
        /// <param name="PointB">Second Point on the Ellipse</param>
        /// <param name="PointC">Last Point on the Ellipse</param>
        /// <returns>A Rectangle Representing the bounds of A Circle Defined from three 
        /// Points</returns>
        public static Rectangle2D TripoinArcBounds(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            Point2D Center = TripointArcCenter(PointA, PointB, PointC);
            double Radius = Center.Length(PointA);
            Rectangle2D Bounds = Rectangle2D.FromLTRB((Center.X - Radius), (Center.Y - Radius), (Center.X + Radius), (Center.Y + Radius));
            return Bounds;
        }

        #endregion
    }
}
