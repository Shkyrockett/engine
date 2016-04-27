using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Experimental
    {
        #region Intersection of Point and Point

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns>Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).</returns>
        public static bool PointNearPoint(double x1, double y1, double x2, double y2, double close_distance)
        {
            return (Math.Abs(x2 - x1) <= close_distance) && (Math.Abs(y2 - y1) <= close_distance);
        }

        /// <summary>
        /// Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointNearPoint2(double x1, double y1, double x2, double y2, double close_distance)
        {
            return ((Math.Abs((x2 - x1)) <= close_distance) && (Math.Abs((y2 - y1)) <= close_distance));
        }

        #endregion

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
        /// 
        /// </summary>
        /// <param name="PointA"></param>
        /// <param name="PointB"></param>
        /// <param name="PointC"></param>
        /// <returns></returns>
        public static double Distance(Point2D PointA, Point2D PointB, Point2D PointC)
        {
            //  ToDo: Add Point Distance from line Method.
            // Dim P As Single = (1 - r)A + rB = A + r(B  A)
            return 0;
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
                return (Math.Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
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
            return (Math.Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
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
                return Math.Sqrt(((dx * dx) + (dy * dy)));

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
            return Math.Sqrt(((dx * dx) + (dy * dy)));
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
                return Math.Sqrt(((dx * dx) + (dy * dy)));
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
            return Math.Sqrt(((dx * dx) + (dy * dy)));
        }

        #endregion

        #region Intersection of Line segment and Line segment

        /// <summary>
        /// Find the point of intersection between the lines p1 --> p2 and p3 --> p4.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="lines_intersect"></param>
        /// <param name="segments_intersect"></param>
        /// <param name="intersection"></param>
        /// <param name="close_p1"></param>
        /// <param name="close_p2"></param>
        /// <remarks>http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/</remarks>
        private static void FindIntersection(Point2D p1, Point2D p2, Point2D p3, Point2D p4, out bool lines_intersect, out bool segments_intersect, out Point2D intersection, out Point2D close_p1, out Point2D close_p2)
        {
            // Get the segments' parameters.
            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);

            double t1;
            try
            {
                t1 = ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;
            }
            catch
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection = new Point2D(double.NaN, double.NaN);
                close_p1 = new Point2D(double.NaN, double.NaN);
                close_p2 = new Point2D(double.NaN, double.NaN);
                return;
            }
            lines_intersect = true;

            double t2 = ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12) / -denominator;

            // Find the point of intersection.
            intersection = new Point2D(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_p1 = new Point2D(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            close_p2 = new Point2D(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }

        /// <summary>
        /// Find the Intersection of given lines and returns the intersection point
        /// </summary>
        /// <param name="LineA"></param>
        /// <param name="LineB"></param>
        /// <returns></returns>
        /// <remarks>This function can handle vertical as well as horizontal and Parallel lines. </remarks>
        public static Point2D Intersect(LineSegment LineA, LineSegment LineB)
        {
            //  Calculate the slopes of the lines.
            double SlopeA = LineA.Slope();
            double SlopeB = LineB.Slope();
            //  To avoid an overflow from parallel lines return nothing and exit.
            if ((SlopeA == SlopeB)) return Point2D.Empty;
            //  Create the constants of linear equations.
            double PointSlopeA = (LineA.A.Y - (SlopeA * LineA.A.X));
            double PointSlopeB = (LineB.A.Y - (SlopeB * LineB.A.X));
            //---------------------- Fastest Method --------------------------
            // Compute the inverse of the determinate of the coefficient.
            double DeterminantInverse = (1 / (SlopeA * ((1 - (SlopeB * -1)) * -1)));
            // Use Kramer's rule to compute the returning point structure.
            return new Point2D((((PointSlopeB - (PointSlopeA * -1)) * -1) * DeterminantInverse), (((SlopeB * PointSlopeA) - (SlopeA * PointSlopeB)) * DeterminantInverse));
            // '---------------------- Slower Method --------------------------
            // ' Return New Point
            // Dim NewX As Single = (PointSlopeA - PointSlopeB) / (SlopeB - SlopeA)
            // Dim NewY As Single = SlopeA * NewX + PointSlopeA
            // Return New Point2D(NewX, NewY)
        }

        /// <summary>
        /// New Return True if the segments intersect.
        /// </summary>
        /// <param name="Point1"></param>
        /// <param name="Point2"></param>
        /// <param name="Point3"></param>
        /// <param name="Point4"></param>
        /// <param name="ReturnPoint"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool Intersect(Point2D Point1, Point2D Point2, Point2D Point3, Point2D Point4, ref Point2D ReturnPoint)
        {
            bool ReturnValue = false;
            Point2D Delta1 = new Point2D((Point2.X - Point1.X), (Point2.Y - Point1.Y));
            Point2D Delta2 = new Point2D((Point4.X - Point3.X), (Point4.Y - Point3.Y));
            //  If the segments are parallel return false.
            if ((((Delta2.X * Delta1.Y) - (Delta2.Y * Delta1.X)) == 0)) ReturnValue = false;
            double s = (((Delta1.X * (Point3.Y - Point1.Y)) + (Delta1.Y * (Point1.X - Point3.X))) / ((Delta2.X * Delta1.Y) - (Delta2.Y * Delta1.X)));
            double t = (((Delta2.X * (Point1.Y - Point3.Y)) + (Delta2.Y * (Point3.X - Point1.X))) / ((Delta2.Y * Delta1.X) - (Delta2.X * Delta1.Y)));
            ReturnValue = (s >= 0.0d) && (s <= 1.0d) && (t >= 0.0d) && (t <= 1.0d);
            //  If it exists, the point of intersection is:
            ReturnPoint = new Point2D((Point1.X + (t * Delta1.X)), (Point1.Y + (t * Delta1.Y)));
            return ReturnValue;
        }

        /// <summary>Find the Intersection of two line segments.</summary>
        /// <param name="PointA">First Point on First Line</param>
        /// <param name="PointB">Second Point on First Line</param>
        /// <param name="PointC">First Point on Second Line</param>
        /// <param name="PointD">Second Point on Second Line</param>
        /// <returns>An Intersection structure defining: 
        /// The point of intersection. 
        /// A boolean which returns True if the segments intersect</returns>
        /// <remarks></remarks>
        [DisplayName("Test Intersection 1")]
        [Description("Test Intersection 1")]
        public static Intersection TestIntersections1(Point2D PointA, Point2D PointB, Point2D PointC, Point2D PointD)
        {
            // -------------------- Experimental Method -----------------------
            bool Intersecting = false;
            IntersectionType Type = IntersectionType.Cross;
            //  Calculate the slopes of the lines.
            Point2D Slopes = new Point2D(PointB.Slope(PointA), PointD.Slope(PointC));
            //  To avoid an overflow from parallel lines return nothing and exit.
            if ((Slopes.X == Slopes.Y))
            {
                Intersecting = false;
            }
            Type = IntersectionType.Parallel;
            // : Return PointA : Exit Function
            //  Create the constants of linear equations.
            Point2D PointSlope = new Point2D((PointA.Y
                            - (Slopes.X * PointA.X)), (PointC.Y
                            - (Slopes.Y * PointC.X)));
            //  Compute the inverse of the determinate of the coefficient.
            double Determinant = (1
                        / (Slopes.X - Slopes.Y));
            return new Intersection(new Point2D(((PointSlope.Y - PointSlope.X)
                                * Determinant), (((Slopes.X * PointSlope.Y)
                                - (Slopes.Y * PointSlope.X))
                                * Determinant)), Intersecting, Type);
        }

        /// <summary>Faster 2D line intersection.</summary>
        /// <param name="PointA">First Point on First Line</param>
        /// <param name="PointB">Second Point on First Line</param>
        /// <param name="PointC">First Point on Second Line</param>
        /// <param name="PointD">Second Point on Second Line</param>
        /// <returns>An Intersection structure defining: 
        /// The point of intersection. 
        /// A boolean which returns True if the segments intersect</returns>
        /// <remarks>Computes intersection of (infinitely extended) lines A--AA and B--BB,
        /// placing the intersection point in *result and returning a true value. If the lines
        /// are parallel or degenerate and don't intersect, we return 0 and *result will be
        /// unchanged. After F S Hill, "The Pleasures of 'Perpendicular Dot' Products," in Graphics Gems
        /// IV, p. 142. Note: we do this in integer coordinates to the extent possible. Scaffold:
        /// should check if the intersection is done in double point with a result outside
        /// INT_MAX, and consider it a parallel non-intersection. </remarks>
        /// <permission>Permission to copy with the following attribution is hereby granted. Richard J Kinch k...@holonet.net, May 1998.</permission>
        [DisplayName("Test Intersection 2")]
        [Description("Test Intersection 2")]
        public static Intersection TestIntersections2(Point2D PointA, Point2D PointB, Point2D PointC, Point2D PointD)
        {
            // ----------------------- Faster Method --------------------------
            bool Intersecting = false;
            IntersectionType Type;
            //  Calculate the slopes of the lines.
            Point2D Slopes = new Point2D(PointA.Slope(PointB), PointC.Slope(PointD));
            //  To avoid an overflow from parallel lines return nothing and exit.
            if ((Slopes.X == Slopes.Y))
            {
                Intersecting = false;
            }
            Type = IntersectionType.Parallel;
            // : Return PointA : Exit Function
            //  Create the constants of linear equations.
            Point2D PointSlope = new Point2D((PointA.Y
                            - (Slopes.X * PointA.X)), (PointC.Y
                            - (Slopes.Y * PointC.X)));
            //  Compute the inverse of the determinate of the coefficient.
            double DeterminantInverse = (1 / (((1 * Slopes.X) * -1) - ((1 * Slopes.Y) * -1)));
            return new Intersection(new Point2D(((((1 * PointSlope.Y) * -1) - ((1 * PointSlope.X) * -1)) * DeterminantInverse), (((Slopes.Y * PointSlope.X) - (Slopes.X * PointSlope.Y)) * DeterminantInverse)), Intersecting, Type);
        }

        /// <summary>Slower 2D line intersection.</summary>
        /// <param name="PointA">First Point on First Line</param>
        /// <param name="PointB">Second Point on First Line</param>
        /// <param name="PointC">First Point on Second Line</param>
        /// <param name="PointD">Second Point on Second Line</param>
        /// <returns>An Intersection structure defining: 
        /// The point of intersection. 
        /// A boolean which returns True if the segments intersect</returns>
        /// <remarks>Computes intersection of (infinitely extended) lines A--AA and B--BB,
        /// placing the intersection point in *result and returning a true value. If the lines
        /// are parallel or degenerate and don't intersect, we return 0 and *result will be
        /// unchanged. After F S Hill, "The Pleasures of 'Perpendicular Dot' Products," in Graphics Gems
        /// IV, p. 142. Note: we do this in integer coordinates to the extent possible. Scaffold:
        /// should check if the intersection is done in double point with a result outside
        /// INT_MAX, and consider it a parallel non-intersection. </remarks>
        /// <permission>Permission to copy with the following attribution is hereby granted. Richard J Kinch k...@holonet.net, May 1998.</permission>
        [DisplayName("Test Intersection 3")]
        [Description("Test Intersection 3")]
        public static Intersection TestIntersections3(Point2D PointA, Point2D PointB, Point2D PointC, Point2D PointD)
        {
            // ---------------------- Slower Method --------------------------
            bool Intersecting = false;
            IntersectionType Type;
            //  Calculate the slopes of the lines.
            Point2D Slopes = new Point2D(PointA.Slope(PointB), PointC.Slope(PointD));
            //  To avoid an overflow from parallel lines return nothing and exit.
            if ((Slopes.X == Slopes.Y))
            {
                Intersecting = false;
            }
            Type = IntersectionType.Parallel;
            // : Return PointA : Exit Function
            //  Create the constants of linear equations.
            Point2D PointSlope = new Point2D((PointA.Y
                            - (Slopes.X * PointA.X)), (PointC.Y
                            - (Slopes.Y * PointC.X)));
            //  Return New Point
            double NewX = ((PointSlope.X - PointSlope.Y)
                        / (Slopes.Y - Slopes.X));
            double NewY = ((Slopes.X * NewX)
                        + PointSlope.X);
            return new Intersection(new Point2D(NewX, NewY), Intersecting, Type);
        }

        /// <summary>2D line intersection.</summary>
        /// <param name="PointA">First Point on First Line</param>
        /// <param name="PointB">Second Point on First Line</param>
        /// <param name="PointC">First Point on Second Line</param>
        /// <param name="PointD">Second Point on Second Line</param>
        /// <returns>An Intersection structure defining: 
        /// The point of intersection. 
        /// A boolean which returns True if the segments intersect</returns>
        /// <remarks>Computes intersection of (infinitely extended) lines A--AA and B--BB,
        /// placing the intersection point in *result and returning a true value. If the lines
        /// are parallel or degenerate and don't intersect, we return 0 and *result will be
        /// unchanged. After F S Hill, "The Pleasures of 'Perpendicular Dot' Products," in Graphics Gems
        /// IV, p. 142. Note: we do this in integer coordinates to the extent possible. Scaffold:
        /// should check if the intersection is done in double point with a result outside
        /// INT_MAX, and consider it a parallel non-intersection. </remarks>
        /// <permission>Permission to copy with the following attribution is hereby granted. Richard J Kinch k...@holonet.net, May 1998.</permission>
        [DisplayName("Test Intersection 4")]
        [Description("Test Intersection 4")]
        public static Intersection TestIntersections4(Point2D PointA, Point2D PointB, Point2D PointC, Point2D PointD)
        {
            // ---------------------- Vector Method --------------------------
            //  Direction vectors AA-A and BB-B 
            Point2D DeltaBA = (PointB - new Size2D(PointA));
            Point2D DeltaDC = (PointD - new Size2D(PointC));
            //  Vector B-A
            Point2D DeltaCA = (PointC - new Size2D(PointA));
            //  Convert the lines to parametric forms A+at and B+bu
            //  The cross product slope for intersection
            Point2D Slopes = new Point2D(DeltaDC.CrossProduct(DeltaBA), DeltaDC.CrossProduct(DeltaCA));
            //  Lines are parallel, or either line came in as coincident endpoints
            IntersectionType Type = IntersectionType.Parallel;
            bool Intersecting = !(Slopes.X == 0);
            if (Intersecting)
            {
                Type = IntersectionType.Parallel;
            }
            //  If m.y/m.x times a is integer, then the solution is integer.
            return new Intersection(new Point2D((PointA.X
                                + (Slopes.Y
                                * (DeltaBA.X / Slopes.X))), (PointA.Y
                                + (Slopes.Y
                                * (DeltaBA.Y / Slopes.X)))), Intersecting, Type);
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="PointA">First Point on First Line</param>
        /// <param name="PointB">Second Point on First Line</param>
        /// <param name="PointC">First Point on Second Line</param>
        /// <param name="PointD">Second Point on Second Line</param>
        /// <returns>An Intersection structure defining: 
        /// The point of intersection. 
        /// A boolean which returns True if the segments intersect</returns>
        /// <remarks></remarks>
        [DisplayName("Test Intersection 5")]
        [Description("Test Intersection 5")]
        public static Intersection TestIntersections5(Point2D PointA, Point2D PointB, Point2D PointC, Point2D PointD)
        {
            Point2D DeltaBA = (PointB - new Size2D(PointA));
            Point2D DeltaDC = (PointD - new Size2D(PointC));
            Point2D DeltaCA = (PointC - new Size2D(PointA));
            Point2D DeltaAC = (PointA - new Size2D(PointC));
            //  If the segments are parallel.
            if ((((DeltaDC.X * DeltaBA.Y)
                        - (DeltaDC.Y * DeltaBA.X))
                        == 0))
            {
                return new Intersection(Point2D.Empty, false, IntersectionType.Parallel);
            }
            // Dim s As Single = (DeltaBA.X * DeltaCA.Y + -DeltaCA.X * DeltaBA.Y) / _
            //                   CrossProduct(DeltaBA, DeltaDC)
            // Dim t As Single = (DeltaDC.X * -DeltaCA.Y + DeltaCA.X * DeltaDC.Y) / _
            //                   CrossProduct(DeltaBA, DeltaDC)
            double s = (((DeltaBA.X * DeltaCA.Y)
                        + (DeltaAC.X * DeltaBA.Y))
                        / DeltaBA.CrossProduct(DeltaDC));
            double t = (((DeltaDC.X * DeltaAC.Y)
                        + (DeltaCA.X * DeltaDC.Y))
                        / DeltaBA.CrossProduct(DeltaDC));
            //  If it exists, the point of intersection is:
            return new Intersection(
                new Point2D((PointA.X + (t * DeltaBA.X)), (PointA.Y + (t * DeltaBA.Y))),
                                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                                IntersectionType.Cross
                                );
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

        #region Intersection of Circle and line

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="intersection1"></param>
        /// <param name="intersection2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/</remarks>
        private static int FindLineCircleIntersections(Point2D center, double radius, Point2D point1, Point2D point2, out Point2D intersection1, out Point2D intersection2)
        {
            double t;

            double dx = point2.X - point1.X;
            double dy = point2.Y - point1.Y;

            double A = dx * dx + dy * dy;
            double B = 2 * (dx * (point1.X - center.X) + dy * (point1.Y - center.Y));
            double C = (point1.X - center.X) * (point1.X - center.X) + (point1.Y - center.Y) * (point1.Y - center.Y) - radius * radius;

            double det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return 0;
            }
            else if (det == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new Point2D(point1.X + t * dx, point1.Y + t * dy);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return 1;
            }
            else
            {
                // Two solutions.
                t = ((-B + Math.Sqrt(det)) / (2 * A));
                intersection1 = new Point2D(point1.X + t * dx, point1.Y + t * dy);
                t = ((-B - Math.Sqrt(det)) / (2 * A));
                intersection2 = new Point2D(point1.X + t * dx, point1.Y + t * dy);
                return 2;
            }
        }

        #endregion

        #region Intersection of Circle and Circle

        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <param name="intersection1"></param>
        /// <param name="intersection2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        private static int FindCircleCircleIntersections(double cx0, double cy0, double radius0, double cx1, double cy1, double radius1, out Point2D intersection1, out Point2D intersection2)
        {
            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return 0;
            }
            else if (dist < Math.Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return 0;
            }
            else if ((dist == 0) && (radius0 == radius1))
            {
                // No solutions, the circles coincide.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return 0;
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Math.Sqrt(radius0 * radius0 - a * a);

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
                if (dist == radius0 + radius1) return 1;
                return 2;
            }
        }

        #endregion

        #region Ellipse Perimeter Lengths

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeter1(this Ellipse ellipse)
        {
            return EllipsePerimeter1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// This approximation is within about 5% of the true value, so long as a is not more than 3 times longer than b (in other words, the ellipse is not too "squashed"):
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeter1(double a, double b)
        {
            return 2 * Math.PI * (Math.Sqrt(0.5 * ((b * b) + (a * a))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeter2(this Ellipse ellipse)
        {
            return EllipsePerimeter2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference.blogspot.com/
        /// </remarks>
        private static double EllipsePerimeter2(double a, double b)
        {
            double h = (((b - a) * (b - a)) / ((b + a) * (b + a)));
            double H2 = 4 - 3 * h;
            double d = ((11 * Math.PI / (44 - 14 * Math.PI)) + 24100) - 24100 * h;
            return Math.PI * (b + a) * (1 + (3 * h) / (10 + Math.Pow(H2, 0.5)) + (1.5 * Math.Pow(h, 6) - .5 * Math.Pow(h, 12)) / d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterKepler(this Ellipse ellipse)
        {
            return EllipsePerimeterKepler(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterKepler(double a, double b)
        {
            return 2 * Math.PI * (Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterSipos(this Ellipse ellipse)
        {
            return EllipsePerimeterSipos(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterSipos(double a, double b)
        {
            return 2 * Math.PI * (((a + b) * (a + b)) / ((Math.Sqrt(a) + Math.Sqrt(a)) * (Math.Sqrt(b) + Math.Sqrt(b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterNaive(this Ellipse ellipse)
        {
            return EllipsePerimeterNaive(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterNaive(double a, double b)
        {
            return Math.PI * (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPeano(this Ellipse ellipse)
        {
            return EllipsePerimeterPeano(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterPeano(double a, double b)
        {
            return Math.PI * ((3 * (a + b) / 2) - Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterEuler(this Ellipse ellipse)
        {
            return EllipsePerimeterEuler(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterEuler(double a, double b)
        {
            return 2 * Math.PI * Math.Sqrt(((a * a) + (b * b)) / 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterAlmkvist(this Ellipse ellipse)
        {
            return EllipsePerimeterAlmkvist(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterAlmkvist(double a, double b)
        {
            return 2 * Math.PI
                * ((2 * Math.Pow(a + b, 2) - Math.Pow(Math.Sqrt(a) - Math.Sqrt(b), 4))
                / (Math.Pow(Math.Sqrt(a) - Math.Sqrt(b), 2) + (2 * Math.Sqrt(2 * (a + b)) * Math.Pow(a * b, (1 / 4)))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterQuadratic(this Ellipse ellipse)
        {
            return EllipsePerimeterQuadratic(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterQuadratic(double a, double b)
        {
            return (Math.PI / 2) * Math.Sqrt((6) * (a * a + b * b) + (4 * a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterMuir(this Ellipse ellipse)
        {
            return EllipsePerimeterMuir(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterMuir(double a, double b)
        {
            return 2 * Math.PI * Math.Pow((Math.Pow(a, 3 / 2) + Math.Pow(b, 3 / 2)) / 2, 2 / 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterLindner(this Ellipse ellipse)
        {
            return EllipsePerimeterLindner(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterLindner(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * Math.Sqrt(1 + (h / 8));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful(this Ellipse ellipse)
        {
            return EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        private static double EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful(double a, double b)
        {
            return 4 * ((Math.PI * a * b) + ((a - b) * (a - b))) / (a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterYNOT(this Ellipse ellipse)
        {
            return EllipsePerimeterYNOT(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterYNOT(double a, double b)
        {
            double s = Math.Log(2, Math.E) / Math.Log(Math.PI / 2, Math.E);
            return 4 * Math.Pow(Math.Pow(a, s) + Math.Pow(b, s), 1 / s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCombinedPadé(this Ellipse ellipse)
        {
            return EllipsePerimeterCombinedPadé(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterCombinedPadé(double a, double b)
        {
            double d1 = (Math.PI / 4) * (19 / 15) - 1;
            double d2 = (Math.PI / 4) * (80 / 63) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((64 + 16 * h)
                / (64 - h * h))
                + (1 - p) * ((16 + 3 * h) / (16 - h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCombinedPadé2(this Ellipse ellipse)
        {
            return EllipsePerimeterCombinedPadé2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterCombinedPadé2(double a, double b)
        {
            double d1 = (Math.PI / 4) * (81 / 64) - 1;
            double d2 = (Math.PI / 4) * (19 / 15) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((16 - 3 * h)
                / (16 - h))
                + (1 - p) * Math.Pow(1 + (h) / 8, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterJacobsenWaadelandHudsonLipka(this Ellipse ellipse)
        {
            return EllipsePerimeterJacobsenWaadelandHudsonLipka(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterJacobsenWaadelandHudsonLipka(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((256 - 48 * h - 21 * h * h)
                / (256 - 112 * h + 3 * h * h))
                + (1 - p) * ((64 - 3 * h * h) / (64 - 16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeter2_3JacobsenWaadeland(this Ellipse ellipse)
        {
            return EllipsePerimeter2_3JacobsenWaadeland(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeter2_3JacobsenWaadeland(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h))
                + (1 - p) * ((256 - 48 * h - 21 * h * h) / (256 - 112 * h + 3 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeter3_3_3_2(this Ellipse ellipse)
        {
            return EllipsePerimeter3_3_3_2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeter3_3_3_2(double a, double b)
        {
            double d1 = (Math.PI / 4) * (61 / 48) - 1;
            double d2 = (Math.PI / 4) * (187 / 147) - 1;
            double p = d1 / (d1 - d2);
            double h = 1;
            return Math.PI * (a + b) * (p * ((135168 - 85760 * h - 5568 * h * h + 3867 * h * h * h)
                / (135168 - 119552 * h + 22208 * h * h - 345 * h * h * h))
                + (1 - p) * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterRamanujan(this Ellipse ellipse)
        {
            return EllipsePerimeterRamanujan(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan(double a, double b)
        {
            return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterSelmer(this Ellipse ellipse)
        {
            return EllipsePerimeterSelmer(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSelmer(double a, double b)
        {
            return (Math.PI / 4) * ((6 + .5 * (Math.Pow(a - b, 2) * Math.Pow(a - b, 2) / Math.Pow(a + b, 2) * Math.Pow(a + b, 2))) * (a + b) - Math.Sqrt(2 * (a * a + 3 * a * b + b * b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterRamanujan2(this Ellipse ellipse)
        {
            return EllipsePerimeterRamanujan2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * (1 + ((3 * h) / (10 + Math.Sqrt(4 - 3 * h))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadéSelmer(this Ellipse ellipse)
        {
            return EllipsePerimeterPadéSelmer(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéSelmer(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((16 + (3 * h)) / (16 - h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadéMichon(this Ellipse ellipse)
        {
            return EllipsePerimeterPadéMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (16 * h)) / (64 - (h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadéHudsonLipkaBronshtein(this Ellipse ellipse)
        {
            return EllipsePerimeterPadéHudsonLipkaBronshtein(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéHudsonLipkaBronshtein(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCombinedPadéHudsonLipkaMichon(this Ellipse ellipse)
        {
            return EllipsePerimeterCombinedPadéHudsonLipkaMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// Not correct.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCombinedPadéHudsonLipkaMichon(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadéJacobsenWaadeland(this Ellipse ellipse)
        {
            return EllipsePerimeterPadéJacobsenWaadeland(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadéJacobsenWaadeland(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((256 - (48 * h) - (21 * h * h)) / (256 - (112 * h) + 3 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadé3_2(this Ellipse ellipse)
        {
            return EllipsePerimeterPadé3_2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadé3_2(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * ((3072 - (1280 * h) - (252 * h * h) + (33 * h * h * h)) / (3072 - (2048 * h) + 212 * h * h));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterPadé3_3(this Ellipse ellipse)
        {
            return EllipsePerimeterPadé3_3(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterPadé3_3(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) *
                ((135168 - (85760 * h) - (5568 * h * h) + (3867 * h * h * h))
                / (135168 - (119552 * h) + (22208 * h * h) - (345 * h * h * h)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterOptimizedPeano(this Ellipse ellipse)
        {
            return EllipsePerimeterOptimizedPeano(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedPeano(double a, double b)
        {
            double p = 1.32;
            return 2 * Math.PI * (p * ((a + b) / 2) + (1 - p) * Math.Sqrt(a * b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterOptimizedQuadratic1(this Ellipse ellipse)
        {
            return EllipsePerimeterOptimizedQuadratic1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedQuadratic1(double a, double b)
        {
            double w = 0.7966106;
            return 2 * Math.PI * Math.Sqrt(w * ((a * a + b * b) / 2) + (1 - w) * a * b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterOptimizedQuadratic2(this Ellipse ellipse)
        {
            return EllipsePerimeterOptimizedQuadratic2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedQuadratic2(double a, double b)
        {
            return Math.PI * Math.Sqrt(2 * (a * a + b * b) + (a - b) * (a - b) / 2.458338);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterOptimizedRamanujan1(this Ellipse ellipse)
        {
            return EllipsePerimeterOptimizedRamanujan1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedRamanujan1(double a, double b)
        {
            double p = 3.0273;
            double w = 3;
            return 2 * Math.PI * (p * ((a + b) / 2) + (1 - p) * Math.Sqrt((a + w * b) * (w * a + b)) / (1 + w));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterBartolomeuMichon(this Ellipse ellipse)
        {
            return EllipsePerimeterBartolomeuMichon(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeuMichon(double a, double b)
        {
            return a == b ? 2 * Math.PI * a : Math.PI * ((a - b) / Math.Atan((a - b) / (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCantrell2(this Ellipse ellipse)
        {
            return EllipsePerimeterCantrell2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell2(double a, double b)
        {
            double p = 0.410117;
            double w = 74;
            return 4 * (a + b) - ((8 - 2 * Math.PI) * a * b) /
                (p * (a + b) + (1 - 2 * p) * (Math.Sqrt((a + w * b) * (w * a + b)) / (1 + w)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterTakakazuSeki(this Ellipse ellipse)
        {
            return EllipsePerimeterTakakazuSeki(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterTakakazuSeki(double a, double b)
        {
            return 2 * Math.Sqrt(Math.PI * Math.PI * a * b + 4 * (a - b) * (a - b));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterLockwood(this Ellipse ellipse)
        {
            return EllipsePerimeterLockwood(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterLockwood(double a, double b)
        {
            return 4 * (((b * b) / a) * Math.Atan(a / b) + ((a * a) / b) * Math.Atan(b / a));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterBartolomeu(this Ellipse ellipse)
        {
            return EllipsePerimeterBartolomeu(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeu(double a, double b)
        {
            double t = (Math.PI / 4) * ((a - b) / b);
            return Math.PI * Math.Sqrt(2 * (a * a + b * b)) * (Math.Sin(t) / t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterRivera1(this Ellipse ellipse)
        {
            return EllipsePerimeterRivera1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera1(double a, double b)
        {
            return 4 * a + 2 * (Math.PI - 2) * a * Math.Pow(b / a, 1.456);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterRivera2(this Ellipse ellipse)
        {
            return EllipsePerimeterRivera2(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera2(double a, double b)
        {
            return 4 * ((Math.PI * a * b + (a - b) * (a - b)) / (a + b)) - (89 / 146) * Math.Pow((b * Math.Sqrt(a) - a * Math.Sqrt(b)) / (a + b), 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCantrell(this Ellipse ellipse)
        {
            return EllipsePerimeterSykora(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell(double a, double b)
        {
            double s = Math.Log(2) / Math.Log(2 / (4 - Math.PI));
            return 4 * (a + b) - ((2 * (4 - Math.PI) * a * b) / Math.Pow(Math.Pow(a, s) + Math.Pow(b, s), 1 / s));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterSykora(this Ellipse ellipse)
        {
            return EllipsePerimeterSykora(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSykora(double a, double b)
        {
            return 4 * (((Math.PI * a * b + (a - b) * (a - b))) / (a + b)) - 0.5 * ((a * b) / (a + b)) * (((a - b) * (a - b)) / (Math.PI * a * b + (a + b) * (a + b)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCantrellRamanujan(this Ellipse ellipse)
        {
            return EllipsePerimeterCantrellRamanujan(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrellRamanujan(double a, double b)
        {
            double h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return Math.PI * (a + b) * (1 + ((3 * h) / (10 + Math.Sqrt(4 - 3 * h))) + ((4 / Math.PI) - ((14) / (11))) * Math.Pow(h, 12));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterK13(this Ellipse ellipse)
        {
            return EllipsePerimeterK13(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterK13(double a, double b)
        {
            return Math.PI * (((a + b) / 2) + Math.Sqrt((a * a + b * b) / 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterThomasBlankenhorn1(this Ellipse ellipse)
        {
            return EllipsePerimeterThomasBlankenhorn1(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// This one is not as good with a circle. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://ellipse-circumference2.blogspot.com/2011/12/accurate-online-ellipse-circumference.html</remarks>
        private static double EllipsePerimeterThomasBlankenhorn1(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Math.Max(X1, X2);
            double HMN = Math.Min(X1, X2);
            double H1 = HMN / HMX;
            return 2 * Math.PI * HMX * ((2 / Math.PI) + 0.0000122 * Math.Pow(H1, 0.6125) - 0.0021973 * Math.Pow(H1, 1.225) + 0.919315 * Math.Pow(H1, 1.8375) - 1.0359227 * Math.Pow(H1, 2.45) + 0.861913 * Math.Pow(H1, 3.0625) - 0.7274398 * Math.Pow(H1, 3.675) + 0.6352295 * Math.Pow(H1, 4.2875) - 0.436051 * Math.Pow(H1, 4.9) + 0.1818904 * Math.Pow(H1, 5.5125) - 0.0333691 * Math.Pow(H1, 6.125));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterThomasBlankenhorn8(this Ellipse ellipse)
        {
            return EllipsePerimeterThomasBlankenhorn8(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference3.blogspot.com/
        /// </remarks>
        private static double EllipsePerimeterThomasBlankenhorn8(double a, double b)
        {
            double X1 = a;
            double X2 = b;
            double HMX = Math.Max(X1, X2);
            double HMN = Math.Min(X1, X2);
            double H1 = HMN / HMX;
            return HMX * (4 + (3929 * Math.Pow(H1, 1.5) + 1639157 * Math.Pow(H1, 2) + 19407215 * Math.Pow(H1, 2.5) + 24302653 * Math.Pow(H1, 3) + 12892432 * Math.Pow(H1, 3.5)) / (86251 + 1924742 * Math.Pow(H1, 0.5) + 6612384 * Math.Pow(H1, 1) + 7291509 * Math.Pow(H1, 1.5) + 6436977 * Math.Pow(H1, 2) + 3158719 * Math.Pow(H1, 2.5)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterCantrell2006(this Ellipse ellipse)
        {
            return EllipsePerimeterCantrell2006(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double EllipsePerimeterCantrell2006(double a, double b)
        {
            double p = 3.982901;
            double q = 66.71674;
            double s = 18.31287;
            double t = 23.39728;
            double r = 4 * ((4 - Math.PI) * (4 * s + t + 16) - (4 * p + q));
            return 4 * (a + b)
                - ((a * b) / (a + b))
                * ((p * (a + b) * (a + b) + q * a * b + r * ((a * b) / (a + b)) * ((a * b) / (a + b)))
                / ((a + b) * (a + b) + s * a * b + t * ((a * b) / (a + b)) * ((a * b) / (a + b))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        public static double EllipsePerimeterAhmadi2006(this Ellipse ellipse)
        {
            return EllipsePerimeterAhmadi2006(ellipse.A, ellipse.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox06.html
        /// </remarks>
        private static double EllipsePerimeterAhmadi2006(double a, double b)
        {
            double c1 = Math.PI - 3;
            double c2 = Math.PI;
            double c3 = 0.5;
            double c4 = (Math.PI + 1) / 2;
            double c5 = 4;
            double k = 1 - ((c1 * a * b) / ((a * a + b * b) + c2 * Math.Sqrt(c3 * a * b * a * b + a * b * Math.Sqrt(a * b * (c4 * (a * a + b * b) + c5 * a * b)))));
            return 4 * ((Math.PI * a * b + k * (a - b) * (a - b)) / (a + b));
        }

        #endregion

        #region Intersection of Ellipse and Point

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
        /// <returns>Return True if the point is inside the ellipse (expanded by distance close_distance vertically and horizontally).</returns>
        public static bool PointNearEllipse(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            double a = ((Math.Abs((x2 - x1)) / 2) + close_distance);
            double b = ((Math.Abs((y2 - y1)) / 2) + close_distance);
            px = (px - (x2 + x1) / 2);
            py = (py - (y2 + y1) / 2);
            return (((px * px) / (a * a)) + (((py * py) / (b * b))) <= 1);
        }

        /// <summary>
        /// Return True if the point is inside the ellipse
        /// (expanded by distance close_distance vertically
        /// and horizontally).
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
        public static bool PointNearEllipse2(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            double a = ((Math.Abs((x2 - x1)) / 2) + close_distance);
            double b = ((Math.Abs((y2 - y1)) / 2) + close_distance);
            px = (px - (x2 + x1) / 2);
            py = (py - (y2 + y1) / 2);
            return ((((px * px) / (a * a)) + (((py * py) / (b * b))) <= 1));
        }

        #endregion

        #region Intersection of Ellipse and Line

        /// <summary>
        /// Finds the Intersection of a line and an Ellipse
        /// </summary>
        /// <param name="line"></param>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(LineSegment line, Ellipse ellipse)
        {
            return Intersect(ellipse, line);
        }

        /// <summary>
        /// Finds the Intersection of a Ellipse and a Line
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse ellipse, LineSegment line)
        {
            double SlopeA = line.Slope();
            double SlopeB = (line.A.Y - (SlopeA * line.A.X));
            double A = (1 + (SlopeA * SlopeA));
            double B = ((2 * (SlopeA * (SlopeB - ellipse.Center.Y))) - (2 * ellipse.Center.X));
            double C = ((ellipse.Center.X * ellipse.Center.X) + (((SlopeB - ellipse.Center.Y) * (SlopeB - ellipse.Center.X)) - (ellipse.MajorRadius * ellipse.MajorRadius)));
            double XA = ((((B * -1) + Math.Sqrt(((B * B) - (A * C)))) / (2 * A)));
            double XB = ((((B - Math.Sqrt(((B * B) - (A * C)))) * -1) / (2 * A)));
            double YA = ((SlopeA * XA) + SlopeB);
            double YB = ((SlopeA * XB) + SlopeB);
            return new LineSegment(XA, YA, XB, YB);
        }

        #endregion

        #region Intersection of Ellipse and Ellipse

        /// <summary>
        /// Finds Intersection of two Ellipse'
        /// </summary>
        /// <param name="ellipseA"></param>
        /// <param name="ellipseB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse ellipseA, Ellipse ellipseB)
        {
            double d = (ellipseB.Center.X * ellipseB.Center.X - ellipseA.Center.X * ellipseA.Center.X - ellipseB.MajorRadius * ellipseB.MajorRadius - Math.Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + ellipseA.MajorRadius * ellipseA.MajorRadius);
            double a = (Math.Pow(2 * ellipseA.Center.X - 2 * ellipseB.Center.X, 2) + 4 * Math.Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            double b = (2 * d * (2 * ellipseA.Center.X - 2 * ellipseB.Center.X) - 8 * ellipseB.Center.X * Math.Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            double C = (4 * ellipseB.Center.X * ellipseB.Center.X * Math.Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + d * d - 4 * Math.Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) * ellipseB.MajorRadius * ellipseB.MajorRadius);
            double XA = ((-b + Math.Sqrt(b * b - 4 * a * C)) / (2 * a));
            double XB = ((-b - Math.Sqrt(b * b - 4 * a * C)) / (2 * a));
            double YA = (Math.Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Math.Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YB = (-Math.Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Math.Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YC = (Math.Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Math.Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double YD = (-Math.Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Math.Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            double E = ((XA - ellipseB.Center.X) + Math.Pow(YA - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double F = ((XA - ellipseB.Center.X) + Math.Pow(YB - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double G = ((XB - ellipseB.Center.X) + Math.Pow(YC - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            double H = ((XB - ellipseB.Center.X) + Math.Pow(YD - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            if (Math.Abs(F) < Math.Abs(E)) YA = YB;
            if (Math.Abs(H) < Math.Abs(G)) YC = YD;
            if (ellipseA.Center.Y == ellipseB.Center.Y) YC = 2 * ellipseA.Center.Y - YA;
            return new LineSegment(XA, YA, XB, YC);
        }

        /// <summary>
        /// 
        /// </summary>
        public class EllipseIntersectStuff
        {
            internal bool GotEllipse1 = false, GotEllipse2 = false;
            private Rectangle2D Ellipse1, Ellipse2;

            // Equations that define the ellipses.
            internal double Dx2, Dy2, Rx2, Ry2;
            internal double A2, B2, C2, D2, E2, F2;
            internal double Dx1, Dy1, Rx1, Ry1;
            internal double A1, B1, C1, D1, E1, F1;

            // The points of intersection.
            internal List<Point2D> Roots = new List<Point2D>();
            internal List<double> RootSign1 = new List<double>();
            internal List<double> RootSign2 = new List<double>();
            internal List<Point2D> PointsOfIntersection = new List<Point2D>();

            // Difference function tangent lines.
            internal double TangentX = 0;
            internal List<Point2D> TangentCenters = null;
            internal List<Point2D> TangentP1 = null;
            internal List<Point2D> TangentP2 = null;
        }

        private const double small = 0.1f;

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void FindPointsOfIntersectionNewtonsMethod(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingNewtonsMethod(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (int i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (int i = 0; i < eis.Roots.Count; i++)
            {
                double y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                double y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                const double small = 0.001f;
                Debug.Assert(Math.Abs(y1 - y2) < small);
            }
        }

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static void FindPointsOfIntersectionUsingBinaryDivision(double xmin, double xmax, EllipseIntersectStuff eis)
        {
            eis.Roots = new List<Point2D>();
            eis.RootSign1 = new List<double>();
            eis.RootSign2 = new List<double>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingBinaryDivision(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (int i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (int i = 0; i < eis.Roots.Count; i++)
            {
                double y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                double y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Math.Abs(y1 - y2) < small);
            }
        }

        /// <summary>
        /// Find roots by using Newton's method.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<Point2D> FindRootsUsingNewtonsMethod(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            List<Point2D> roots = new List<Point2D>();
            const int num_tests = 1000;
            double delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            double x0 = xmin;
            double x, y;
            for (int i = 0; i < num_tests; i++)
            {
                // Try to find a root at this position.
                UseNewtonsMethod(x0, out x, out y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    bool is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        const double small = 0.001f;
                        if (Math.Abs(pt.X - x) < small)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1) return roots;
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find roots by using binary division.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static List<Point2D> FindRootsUsingBinaryDivision(double xmin, double xmax,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            List<Point2D> roots = new List<Point2D>();
            const int num_tests = 10;
            double delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            double x0 = xmin;
            double x, y;
            for (int i = 0; i < num_tests; i++)
            {
                // Try to find a root in this range.
                UseBinaryDivision(x0, delta_x, out x, out y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    bool is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Math.Abs(pt.X - x) < small)
                        {
                            is_new = false;
                            break;
                        }
                    }

                    // If this is a new point, save it.
                    if (is_new)
                    {
                        roots.Add(new Point2D(x, y));

                        // If we've found two roots, we won't find any more.
                        if (roots.Count > 1) return roots;
                    }
                }

                x0 += delta_x;
            }

            return roots;
        }

        /// <summary>
        /// Find a root by using Newton's method.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void UseNewtonsMethod(double x0, out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const double cutoff = 0.0000001f;
            const double tiny = 0.00001f;
            const int max_iterations = 100;
            double epsilon;
            int iterations = 0;

            do
            {
                // Display this guess x0.
                iterations++;

                // Make sure x0 isn't on a flat spot.
                double g_prime = GPrime(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                while (Math.Abs(g_prime) < tiny)
                {
                    x0 += tiny;
                    g_prime = GPrime(x0,
                        A1, B1, C1, D1, E1, F1, sign1,
                        A2, B2, C2, D2, E2, F2, sign2);
                }

                // Calculate the next estimate for x0.
                double g = G(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                epsilon = -g / g_prime;
                x0 += epsilon;
            } while ((Math.Abs(epsilon) > cutoff) && (iterations < max_iterations));

            x = x0;
            y = G(x0,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            //Console.WriteLine("G1(" + x + ") = " + y +
            //    ", Epsilon: " + epsilon +
            //    ", Iterations: " + iterations);
        }

        /// <summary>
        /// Find a root by using binary division.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="delta_x"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-4/</remarks>
        private static void UseBinaryDivision(double x0, double delta_x,
            out double x, out double y,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            const int num_trials = 200;
            const int sgn_nan = -2;

            // Get G(x) for the bounds.
            double xmin = x0;
            double g_xmin = G(xmin,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Math.Abs(g_xmin) < small)
            {
                x = xmin;
                y = g_xmin;
                return;
            }

            double xmax = xmin + delta_x;
            double g_xmax = G(xmax,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Math.Abs(g_xmax) < small)
            {
                x = xmax;
                y = g_xmax;
                return;
            }

            // Get the sign of the values.
            int sgn_min, sgn_max;
            if (IsNumber(g_xmin)) sgn_min = Math.Sign(g_xmin);
            else sgn_min = sgn_nan;
            if (IsNumber(g_xmax)) sgn_max = Math.Sign(g_xmax);
            else sgn_max = sgn_nan;

            // If the two values have the same sign,
            // then there is no root here.
            if (sgn_min == sgn_max)
            {
                x = 1;
                y = double.NaN;
                return;
            }

            // Use binary division to find the point of intersection.
            double xmid = 0, g_xmid = 0;
            int sgn_mid = 0;
            for (int i = 0; i < num_trials; i++)
            {
                // Get values for the midpoint.
                xmid = (xmin + xmax) / 2;
                g_xmid = G(xmid,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                if (IsNumber(g_xmid)) sgn_mid = Math.Sign(g_xmid);
                else sgn_mid = sgn_nan;

                // If sgn_mid is 0, gxmid is 0 so this is the root.
                if (sgn_mid == 0) break;

                // See which half contains the root.
                if (sgn_mid == sgn_min)
                {
                    // The min and mid values have the same sign.
                    // Search the right half.
                    xmin = xmid;
                    g_xmin = g_xmid;
                }
                else if (sgn_mid == sgn_max)
                {
                    // The max and mid values have the same sign.
                    // Search the left half.
                    xmax = xmid;
                    g_xmax = g_xmid;
                }
                else
                {
                    // The three values have different signs.
                    // Assume min or max is NaN.
                    if (sgn_min == sgn_nan)
                    {
                        // Value g_xmin is NaN. Use the right half.
                        xmin = xmid;
                        g_xmin = g_xmid;
                    }
                    else if (sgn_max == sgn_nan)
                    {
                        // Value g_xmax is NaN. Use the right half.
                        xmax = xmid;
                        g_xmax = g_xmid;
                    }
                    else
                    {
                        // This is a weird case. Just trap it.
                        throw new InvalidOperationException(
                            "Unexpected difference curve. " +
                            "Cannot find a root between X = " +
                            xmin + " and X = " + xmax);
                    }
                }
            }

            if (IsNumber(g_xmid) && (Math.Abs(g_xmid) < small))
            {
                x = xmid;
                y = g_xmid;
            }
            else if (IsNumber(g_xmin) && (Math.Abs(g_xmin) < small))
            {
                x = xmin;
                y = g_xmin;
            }
            else if (IsNumber(g_xmax) && (Math.Abs(g_xmax) < small))
            {
                x = xmax;
                y = g_xmax;
            }
            else
            {
                x = xmid;
                y = double.NaN;
            }
        }

        /// <summary>
        /// Get an ellipse's points from its equation.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<Point2D> GetPointsFromEquation(double xmin, double xmax,
            double A, double B, double C, double D, double E, double F)
        {
            List<Point2D> points = new List<Point2D>();
            for (double x = xmin; x <= xmax; x++)
            {
                double y = G1(A, B, C, D, E, F, x, +1f);
                if (IsNumber(y)) points.Add(new Point2D(x, y));
            }
            for (double x = xmax; x >= xmin; x--)
            {
                double y = G1(A, B, C, D, E, F, x, -1f);
                if (IsNumber(y)) points.Add(new Point2D(x, y));
            }
            return points;
        }

        /// <summary>
        /// Get points representing the difference between the two ellipses' equations. 
        /// </summary>
        /// <param name="xmin1"></param>
        /// <param name="xmax1"></param>
        /// <param name="xmin2"></param>
        /// <param name="xmax2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<List<Point2D>> GetDifferencePoints(
            double xmin1, double xmax1,
            double xmin2, double xmax2,
            double A1, double B1, double C1, double D1, double E1, double F1,
            double A2, double B2, double C2, double D2, double E2, double F2)
        {
            double xmin = Math.Min(xmin1, xmin2);
            double xmax = Math.Max(xmax1, xmax2);
            List<List<Point2D>> result = new List<List<Point2D>>();

            double[] signs = { -1f, +1f };
            foreach (double sign1 in signs)
            {
                foreach (double sign2 in signs)
                {
                    List<Point2D> points = new List<Point2D>();
                    result.Add(points);
                    for (double x = xmin; x <= xmax; x++)
                    {
                        double y1 = G1(A1, B1, C1, D1, E1, F1, x, sign1);
                        if (IsNumber(y1))
                        {
                            double y2 = G1(A2, B2, C2, D2, E2, F2, x, sign2);
                            if (IsNumber(y2)) points.Add(new Point2D(x, y1 - y2));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find tangents to the difference functions.
        /// </summary>
        /// <param name="eis"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void FindDifferenceTangents(EllipseIntersectStuff eis)
        {
            eis.TangentCenters = new List<Point2D>();
            eis.TangentP1 = new List<Point2D>();
            eis.TangentP2 = new List<Point2D>();

            if (!eis.GotEllipse1 || !eis.GotEllipse2) return;

            const double tangent_length = 50;

            //++
            double tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    double delta_x = Math.Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //+-
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    double delta_x = Math.Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //-+
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    double delta_x = Math.Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }

            //--
            tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
            if (IsNumber(tangent_y))
            {
                double slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f) -
                    G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    double delta_x = Math.Sqrt(
                        tangent_length * tangent_length / (1 + slope * slope)) / 2;
                    eis.TangentCenters.Add(new Point2D(eis.TangentX, tangent_y));
                    eis.TangentP1.Add(new Point2D(eis.TangentX - delta_x, tangent_y - slope * delta_x));
                    eis.TangentP2.Add(new Point2D(eis.TangentX + delta_x, tangent_y + slope * delta_x));
                }
            }
        }

        /// <summary>
        /// Get the equation for this ellipse.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <param name="Rx"></param>
        /// <param name="Ry"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void GetEllipseFormula(Rectangle2D rect,
            out double Dx, out double Dy, out double Rx, out double Ry,
            out double A, out double B, out double C, out double D,
            out double E, out double F)
        {
            Dx = rect.X + rect.Width / 2f;
            Dy = rect.Y + rect.Height / 2f;
            Rx = rect.Width / 2f;
            Ry = rect.Height / 2f;

            A = 1f / Rx / Rx;
            B = 0;
            C = 1f / Ry / Ry;
            D = -2f * Dx / Rx / Rx;
            E = -2f * Dy / Ry / Ry;
            F = Dx * Dx / Rx / Rx + Dy * Dy / Ry / Ry - 1;

            // Verify the parameters.
            Console.WriteLine();
            double xmid = rect.Left + rect.Width / 2f;
            double ymid = rect.Top + rect.Height / 2f;
            VerifyEquation(A, B, C, D, E, F, rect.Left, ymid);
            VerifyEquation(A, B, C, D, E, F, rect.Right, ymid);
            VerifyEquation(A, B, C, D, E, F, xmid, rect.Top);
            VerifyEquation(A, B, C, D, E, F, xmid, rect.Bottom);
        }

        /// <summary>
        /// Verify that the equation gives a value close to 0 for the given point (x, y).
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void VerifyEquation(double A, double B, double C, double D, double E, double F, double x, double y)
        {
            double total = A * x * x + B * x * y + C * y * y + D * x + E * y + F;
            Console.WriteLine("VerifyEquation (" + x + ", " + y + ") = " + total);
            Debug.Assert(Math.Abs(total) < 0.001f);
        }

        /// <summary>
        /// Calculate G1(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1(double x, double A, double B, double C, double D, double E, double F, double root_sign)
        {
            double result = B * x + E;
            result = result * result;
            result = result - 4 * C * (A * x * x + D * x + F);
            result = root_sign * Math.Sqrt(result);
            result = -(B * x + E) + result;
            result = result / 2 / C;

            return result;
        }

        /// <summary>
        /// Calculate G1'(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1Prime(double x, double A, double B, double C, double D, double E, double F, double root_sign)
        {
            double numerator = 2 * (B * x + E) * B -
                4 * C * (2 * A * x + D);
            double denominator = 2 * Math.Sqrt(
                (B * x + E) * (B * x + E) -
                4 * C * (A * x * x + D * x + F));
            double result = -B + root_sign * numerator / denominator;
            result = result / 2 / C;

            return result;
        }

        /// <summary>
        /// Calculate G(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            return
                G1(x, A1, B1, C1, D1, E1, F1, sign1) -
                G1(x, A2, B2, C2, D2, E2, F2, sign2);
        }

        /// <summary>
        /// Calculate G'(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="C1"></param>
        /// <param name="D1"></param>
        /// <param name="E1"></param>
        /// <param name="F1"></param>
        /// <param name="sign1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <param name="C2"></param>
        /// <param name="D2"></param>
        /// <param name="E2"></param>
        /// <param name="F2"></param>
        /// <param name="sign2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double GPrime(double x,
            double A1, double B1, double C1, double D1, double E1, double F1, double sign1,
            double A2, double B2, double C2, double D2, double E2, double F2, double sign2)
        {
            return
                G1Prime(x, A1, B1, C1, D1, E1, F1, sign1) -
                G1Prime(x, A2, B2, C2, D2, E2, F2, sign2);
        }

        /// <summary>
        /// Return true if the number is not infinity or NaN.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static bool IsNumber(double number)
        {
            return !(double.IsNaN(number) || double.IsInfinity(number));
        }

        #endregion

        #region Intersection of Parabola and Hyperbola
        //http://csharphelper.com/blog/2014/11/see-where-a-parabola-and-hyperbola-intersect-in-c/
        #endregion

        #region Intersection of Conic Section with Line segment
        // http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
        #endregion

        #region Intersection of Conic Section with Conic Section
        // http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
        #endregion

        #region Linear Interpolation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point2D LinearInterpolate(this LineSegment segment, double index)
        {
            return LinearInterpolate1(segment.A, segment.B, index);
        }

        /// <summary>
        /// Interpolates two points
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D LinearInterpolate0(Point2D a, Point2D b, double index)
        {
            return new Point2D(
                a.X + (index * (b.X - a.X)),
                a.Y + (index * (b.Y - a.Y)));
        }

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static Point2D LinearInterpolate1(Point2D a, Point2D b, double index)
        {
            return new Point2D(
                (a.X + (b.X - a.X) * index),
                (a.Y + (b.Y - a.Y) * index));
        }

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public static Point2D LinearInterpolate2(Point2D a, Point2D b, double index)
        {
            return new Point2D(
                (a.X * (1 - index)) + (b.X * index),
                (a.Y * (1 - index)) + (b.Y * index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D LinearInterpolate3(Point2D a, Point2D b, double index)
        {
            return new Point2D(
                a.X + ((1 / (a.X - b.X)) * index),
                a.Y + ((1 / (a.Y - b.Y)) * index));
        }

        /// <summary>
        /// Function For normal Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D LinearInterpolate4(Point2D a, Point2D b, double index)
        {
            return (Point2D)(a.Scale(1 - index)).Add(b.Scale(index));
        }

        #endregion

        #region List Cubic Bezier Interpolation Points

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolatePoints(this CubicBezier bezier, int count)
        {
            Point2D[] ipoints = new Point2D[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1d / count) * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(this CubicBezier bezier, int count)
        {
            return InterpolateCubicBeizerPoints(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        private static List<Point2D> InterpolateCubicBeizerPoints(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            int Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = InterpolateCubicBeizerPoint2(a, b, c, d, Index);
            }

            return new List<Point2D>(BPoints);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> ComputeBezierInterpolations(this CubicBezier bezier, int count)
        {
            return ComputeBezierInterpolations(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        ///  ComputeBezier fills an array of Point2D structs with the curve points
        ///  generated from the control points cp. Caller must allocate sufficient memory
        ///  for the result, which is [sizeof(Point2D) * numberOfPoints]
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="numberOfPoints"></param>
        private static List<Point2D> ComputeBezierInterpolations(Point2D a, Point2D b, Point2D c, Point2D d, int numberOfPoints)
        {
            List<Point2D> curve = new List<Point2D>();
            double t = 0;
            double dt = (1.0d / (numberOfPoints - 1));
            for (int i = 0; (i <= numberOfPoints); i++)
            {
                t += dt;
                curve.Add(InterpolatePointOnCubicBezier(a, b, c, d, t));
            }
            return curve;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(this CubicBezier bezier, int count)
        {
            return InterpolateCubicBeizerPoints0(bezier.A, bezier.B, bezier.C, bezier.D, count);
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        private static List<Point2D> InterpolateCubicBeizerPoints0(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            Point2D[] BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            int Node = 0;
            for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            {
                Node++;
                BPoints[Node] = InterpolateCubicBSplinePoint(a, b, c, d, Index);
            }

            return new List<Point2D>(BPoints);
        }

        #endregion

        #region Cubic Bezier Interpolation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D InterpolateCubicBezier(this CubicBezier bezier, double t)
        {
            return InterpolateCubicBezier(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        private static Point2D InterpolateCubicBezier(Point2D a, Point2D b, Point2D c, Point2D d, double index)
        {
            double mum1 = 1 - index;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = index * index * index;

            return new Point2D(
                (mum13 * a.X + 3 * index * mum1 * mum1 * b.X + 3 * index * index * mum1 * c.X + mu3 * d.X),
                (mum13 * a.Y + 3 * index * mum1 * mum1 * b.Y + 3 * index * index * mum1 * c.Y + mu3 * d.Y)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D CubicBezierInterpolate0(this CubicBezier bezier, double t)
        {
            return CubicBezierInterpolate0(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static Point2D CubicBezierInterpolate0(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            // point between a and b
            Point2D ab = LinearInterpolate1(a, b, t);
            // point between b and c
            Point2D bc = LinearInterpolate1(b, c, t);
            // point between c and d
            Point2D cd = LinearInterpolate1(c, d, t);
            // point between ab and bc
            Point2D abbc = LinearInterpolate1(ab, bc, t);
            // point between bc and cd
            Point2D bccd = LinearInterpolate1(bc, cd, t);
            // point on the bezier-curve
            return LinearInterpolate1(abbc, bccd, t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D InterpolateCubicBezierPoint1(this CubicBezier bezier, double t)
        {
            return InterpolateCubicBezierPoint1(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// Calculate parametric value of x or y given t and the four point
        /// coordinates of a cubic bezier curve. This is a separate function
        /// because we need it for both x and y values.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>http://www.lemoda.net/maths/bezier-length/index.html</remarks>
        private static Point2D InterpolateCubicBezierPoint1(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            // Formula from Wikipedia article on Bezier curves.
            return new Point2D(
            a.X * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * b.X * (1.0 - t) * (1.0 - t) * t + 3.0 * c.X * (1.0 - t) * t * t + d.X * t * t * t,
            a.Y * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * b.Y * (1.0 - t) * (1.0 - t) * t + 3.0 * c.Y * (1.0 - t) * t * t + d.Y * t * t * t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D InterpolateCubicBeizerPoint2(this CubicBezier bezier, double t)
        {
            return InterpolateCubicBeizerPoint2(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Point2D InterpolateCubicBeizerPoint2(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            double V1 = t;
            double V2 = (1 - t);
            return new Point2D(
                (a.X * V2 * V2 * V2) + (3 * b.X * V1 * V2 * V2) + (3 * c.X * V1 * V1 * V2) + (d.X * V2 * V2 * V2),
                ((a.Y * V2 * V2 * V2) + (3 * b.Y * V1 * V2 * V2) + (3 * c.Y * V1 * V1 * V2) + (d.Y * V2 * V2 * V2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D InterpolatePointOnCubicBezier(this CubicBezier bezier, double t)
        {
            return InterpolatePointOnCubicBezier(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        ///  Code to generate a cubic Bezier curve
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="t">
        ///  t is the parameter value, 0 less than or equal to t less than or equal to 1
        /// </param>
        /// <returns></returns>
        /// <remarks>
        ///  Warning - untested code
        /// </remarks>
        private static Point2D InterpolatePointOnCubicBezier(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            // calculate the curve point at parameter value t 
            double tSquared = (t * t);
            double tCubed = (tSquared * t);

            // calculate the polynomial coefficients 
            Point2D cC = new Point2D((3 * (b.X - a.X)), (3 * (b.Y - a.Y)));
            Point2D cB = new Point2D(((3 * (c.X - b.X)) - cC.X), ((3 * (c.Y - b.Y)) - cC.Y));
            Point2D cA = new Point2D((d.X - (a.X - (cC.X - cB.X))), (d.Y - (a.Y - (cC.Y - cB.Y))));
            return new Point2D(((cA.X * tCubed) + ((cB.X * tSquared) + ((cC.X * t) + a.X))), ((cA.Y * tCubed) + ((cB.Y * tSquared) + ((cC.Y * t) + a.Y))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D Interpolate1(this CubicBezier bezier, double t)
        {
            return Interpolate1(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static Point2D Interpolate1(Point2D a, Point2D b, Point2D c, Point2D d, double index)
        {
            //Point2D P = (v3 - v2) - (v0 - v1);
            //Point2D Q = (v0 - v1) - P;
            //Point2D R = v2 - v0;
            //Point2D S = v1;
            //return P * Math.Pow(x, 3) + Q * Math.Pow(x, 2) + R * x + S;
            Vector2D P = d.Subtract(c).Subtract(a.Subtract(b));
            Vector2D Q = a.Subtract(b).Subtract(P);
            Vector2D R = c.Subtract(a);
            Vector2D S = b;
            return (Point2D)P.Scale(index * index * index).Add(Q.Scale(index * index)).Add(R.Scale(index)).Add(S);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Point2D InterpolateCubicBSplinePoint(this CubicBezier bezier, double t)
        {
            return InterpolateCubicBSplinePoint(bezier.A, bezier.B, bezier.C, bezier.D, t);
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Point2D InterpolateCubicBSplinePoint(Point2D a, Point2D b, Point2D c, Point2D d, double t)
        {
            Point2D c1 = new Point2D(((d.X - c.X) - (a.X - b.X)), ((d.Y - c.Y) - (a.Y - b.Y)));
            Point2D c2 = new Point2D(((a.X - b.X) - a.X), ((a.Y - b.Y) - a.Y));
            Point2D c3 = new Point2D((c.X - a.X), (c.Y - a.Y));
            Point2D c4 = a;
            return new Point2D((c1.X * t * t * t + c2.X * t * t * t + c3.X * t + c4.X), (c1.Y * t * t * t + c2.Y * t * t * t + c3.Y * t + c4.Y));
        }

        #endregion

        #region Cubic Bezier Length approximations

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static double CubicBezierArcLength(CubicBezier bezier)
        {
            return CubicBezierArcLength(bezier.A, bezier.B, bezier.C, bezier.D);
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
        private static double CubicBezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            Point2D k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            Point2D k2 = 3 * (p1 + p3) - 6 * p2;
            Point2D k3 = (Point2D)(3 * (p2 - p1));
            Point2D k4 = p1;

            double q1 = 9.0 * (Math.Sqrt(Math.Abs(k1.X)) + Math.Sqrt((Math.Abs(k1.Y))));
            double q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            double q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Math.Sqrt(Math.Abs(k2.X)) + Math.Sqrt(Math.Abs(k2.Y)));
            double q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            double q5 = Math.Sqrt(Math.Abs(k3.X)) + Math.Sqrt(Math.Abs(k3.Y));

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

            while (n < n_limit && (Math.Abs(est1) > 0 && Math.Abs((est1 - est0) / est1) > TOLERANCE))
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
            result = Math.Sqrt(Math.Abs(result));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static double CubicBezierLength(this CubicBezier bezier, int steps)
        {
            return CubicBezierLength(bezier.A, bezier.B, bezier.C, bezier.D, steps);
        }

        /// <summary>
        /// Approximate length of the Bezier curve which starts at "start" and
        /// is defined by "c". According to Computing the Arc Length of Cubic Bezier Curves
        /// there is no closed form integral for it.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        /// <remarks>http://www.lemoda.net/maths/bezier-length/index.html</remarks>
        private static double CubicBezierLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4, int steps = 10)
        {
            double t;
            Point2D dot;
            Point2D previous_dot = new Point2D();
            double length = 0.0;
            for (int i = 0; i <= steps; i++)
            {
                t = (double)i / steps;
                dot = InterpolateCubicBezierPoint1(p1, p2, p3, p4, t);
                if (i > 0)
                {
                    double x_diff = dot.X - previous_dot.X;
                    double y_diff = dot.Y - previous_dot.Y;
                    length += Math.Sqrt(x_diff * x_diff + y_diff * y_diff);
                }
                previous_dot = dot;
            }
            return length;
        }

        //public static double JensGravesenBezierLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        //{

        //}

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
                Lut[(int)(t * 100)] = InterpolateCubicBezier(a, b, c, d, t);
            }
            return Lut;
        }

        #endregion

        #region Cubic Bezier and Line Intersections

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static List<double> CubicBezierCardanoIntersection(this CubicBezier bezier, LineSegment line)
        {
            return CubicBezierCardanoIntersection(bezier.A, bezier.B, bezier.C, bezier.D, line);
        }

        /// <summary>
        /// Cardano's algorithm
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top
        /// http://jsbin.com/mutaracihafi/1/edit?js,output
        /// http://pomax.github.io/bezierinfo/
        /// based on
        /// http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
        /// </remarks>
        private static List<double> CubicBezierCardanoIntersection(Point2D p1, Point2D p2, Point2D p3, Point2D p4, LineSegment line)
        {
            // align curve with the intersecting line, translating/rotating
            // so that the first point becomes (0,0), and the last point
            // ends up lying on the line we're trying to use as root-intersect.
            List<Point2D> aligned = Utilities.Align(new List<Point2D>() { p1, p2, p3, p4 }, line);
            // rewrite from [a(1-t)^3 + 3bt(1-t)^2 + 3c(1-t)t^2 + dt^3] form...
            double pa = aligned[0].Y;
            double pb = aligned[1].Y;
            double pc = aligned[2].Y;
            double pd = aligned[3].Y;
            // ...to [t^3 + at^2 + bt + c] form:
            double d = (-pa + 3 * pb - 3 * pc + pd);
            double a = (3 * pa - 6 * pb + 3 * pc) / d;
            double b = (-3 * pa + 3 * pb) / d;
            double c = pa / d;
            // then, determine p and q:
            double p = (3 * b - a * a) / 3;
            double dp3 = p / 3;
            double q = (2 * a * a * a - 9 * a * b + 27 * c) / 27;
            double q2 = q / 2;
            // and determine the discriminant:
            double discriminant = q2 * q2 + dp3 * dp3 * dp3;
            // and some reserved variables for later
            double u1, v1, x1, x2, x3;

            // If the discriminant is negative, use polar coordinates
            // to get around square roots of negative numbers
            if (discriminant < 0)
            {
                double mp3 = -p / 3,
                    mp33 = mp3 * mp3 * mp3,
                    r = Math.Sqrt(mp33),
                    t = -q / (2 * r),
                    // deal with IEEE rounding yielding <-1 or >1
                    cosphi = t < -1 ? -1 : t > 1 ? 1 : t,
                    phi = Math.Acos(cosphi),
                    crtr = MathExtensions.Crt(r),
                    t1 = 2 * crtr;
                x1 = t1 * Math.Cos(phi / 3) - a / 3;
                x2 = t1 * Math.Cos((phi + MathExtensions.Tau) / 3) - a / 3;
                x3 = t1 * Math.Cos((phi + 2 * MathExtensions.Tau) / 3) - a / 3;
                return new List<double>() { x1, x2, x3 };
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0 ? MathExtensions.Crt(-q2) : -MathExtensions.Crt(q2);
                x1 = 2 * u1 - a / 3;
                x2 = -u1 - a / 3;
                return new List<double>() { x1, x2 };
            }
            else
            {
                // one real root, and two imaginary roots
                double sd = Math.Sqrt(discriminant);
                double tt = -q2 + sd;
                u1 = MathExtensions.Crt(-q2 + sd);
                v1 = MathExtensions.Crt(q2 + sd);
                x1 = u1 - v1 - a / 3;
                return new List<double>() { x1 };
            }
        }

        #endregion

        #region List Quadratic Bezier Interpolation Points

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateQuadraticBezierPoints(QuadraticBezier bezier, int count)
        {
            Point2D[] ipoints = new Point2D[count + 1];
            for (int i = 0; i <= count; i += 1)
            {
                double v = (1f / count) * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        #endregion

        #region Quadratic Bezier Interpolation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Point2D InterpolateQuadraticBezier(this QuadraticBezier bezier, double t)
        {
            return InterpolateQuadraticBezier(bezier.A, bezier.B, bezier.C, t);
        }

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static Point2D InterpolateQuadraticBezier(Point2D a, Point2D b, Point2D c, double t)
        {
            // point between a and b
            Point2D ab = LinearInterpolate1(a, b, t);
            // point between b and c
            Point2D bc = LinearInterpolate1(b, c, t);
            // point on the bezier-curve
            return LinearInterpolate1(ab, bc, t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Point2D QuadraticBezierInterpolate(this QuadraticBezier bezier, double t)
        {
            return QuadraticBezierInterpolate(bezier.A, bezier.B, bezier.C, t);
        }

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        private static Point2D QuadraticBezierInterpolate(Point2D a, Point2D b, Point2D c, double mu)
        {
            double mu1 = 1 - mu;
            double mu12 = mu1 * mu1;
            double mu2 = mu * mu;

            return new Point2D(
                (a.X * mu12 + 2 * b.X * mu1 * mu + c.X * mu2),
                (a.Y * mu12 + 2 * b.Y * mu1 * mu + c.Y * mu2)
                );
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

            double abc = 2 * Math.Sqrt(a + b + c);
            double a2 = Math.Sqrt(a);
            double a32 = 2 * a * a2;
            double c2 = 2 * Math.Sqrt(c);
            double ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4 * c * a - b * b) * Math.Log((2 * a2 + ba + abc) / (ba + c2))) / (4 * a32);
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
            Point2D p = InterpolateQuadraticBezier(pointA, pointB, pointC, 0);
            double prevX = p.X;
            double prevY = p.Y;
            for (double t = 0.005; t <= 1.0; t += 0.005)
            {
                p = InterpolateQuadraticBezier(pointA, pointB, pointC, t);
                double deltaX = p.X - prevX;
                double deltaY = p.Y - prevY;
                length += Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

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
                theta = ab2 + mult * MathExtensions.abscissa[startl + index];

                // First-derivative of the quadratic bezier.
                xPrime = coeff1X + 2.0 * coeff2X * theta;
                yPrime = coeff1Y + 2.0 * coeff2Y * theta;

                // Integrand for Gauss-Legendre numerical integration.
                integrand = Math.Sqrt(xPrime * xPrime + yPrime * yPrime);

                sum += integrand * MathExtensions.weight[startl + index];
            }

            return mult == 0 ? sum : mult * sum;
        }

        #endregion

        #region Self Intersecting Bezier
        // https://github.com/Parclytaxel/Kinross/blob/master/kinback/segment.py
        #endregion



        #region Interpolations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double CosineInterpolate(double y1, double y2, double mu)
        {
            double mu2 = (1 - Math.Cos(mu * Math.PI)) / 2;
            return (y1 * (1 - mu2) + y2 * mu2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double CubicInterpolate(double y0, double y1, double y2, double y3, double mu)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = mu * mu;
            a0 = y3 - y2 - y0 + y1;
            a1 = y0 - y1 - a0;
            a2 = y2 - y0;
            a3 = y1;

            return (a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double CubicInterpolateCatmullRomSplines(double y0, double y1, double y2, double y3, double mu)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = mu * mu;
            a0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            a1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            a2 = -0.5 * y0 + 0.5 * y2;
            a3 = y1;

            return (a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double HermiteInterpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            double m0, m1, mu2, mu3;
            double a0, a1, a2, a3;

            mu2 = mu * mu;
            mu3 = mu2 * mu;
            m0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            m0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            m1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            m1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
        }

        #endregion

    }
}
