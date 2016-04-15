// <copyright file="Intersection.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Geometry
{
    /// <summary>
    /// Geometry Intersection Return Structure
    /// </summary>
    /// <structure>Engine.Geometry.Intersection</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Intersection")]
    public class Intersection
    {
        /// <summary>
        /// Return Value of whether an intersection occurred
        /// </summary>
        /// <remarks></remarks>
        private bool itersecting;

        /// <summary>
        /// 
        /// </summary>
        private bool paralell;

        /// <summary>
        /// 
        /// </summary>
        private IntersectionType type;

        /// <summary>
        /// Returns of the point(s) of Intersection
        /// </summary>
        /// <remarks></remarks>
        private Point2D[] intersectionPoint;

        /// <summary>
        /// 
        /// </summary>
        public Intersection()
        {
            Itersecting = false;
            intersectionPoint = new Point2D[] { Point2D.Empty };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isIntersection"></param>
        /// <param name="intersectionPoint"></param>
        public Intersection(bool isIntersection, Point2D[] intersectionPoint)
        {
            Itersecting = isIntersection;
            this.intersectionPoint = intersectionPoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Type"></param>
        /// <remarks></remarks>
        public Intersection(Point2D[] Locations, bool Intersects, IntersectionType Type)
        {
            intersectionPoint = Locations;
            Itersecting = Intersects;
            Paralell = false;
            this.Type = Type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Paralell"></param>
        /// <remarks></remarks>
        public Intersection(Point2D[] Locations, bool Intersects, bool Paralell)
        {
            intersectionPoint = Locations;
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
            {
                Type = IntersectionType.Parallel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Paralell"></param>
        /// <remarks></remarks>
        public Intersection(Point2D Locations, bool Intersects, bool Paralell)
        {
            intersectionPoint = new Point2D[] { Locations };
            Itersecting = Intersects;
            this.Paralell = Paralell;
            if (Paralell)
            {
                Type = IntersectionType.Parallel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Locations"></param>
        /// <param name="Intersects"></param>
        /// <param name="Type"></param>
        /// <remarks></remarks>
        public Intersection(Point2D Locations, bool Intersects, IntersectionType Type)
        {
            intersectionPoint = new Point2D[] {
                     Locations};
            Paralell = false;
            Itersecting = Intersects;
            this.Type = Type;
        }

        /// <summary>
        /// Return Value of whether an intersection occurred
        /// </summary>
        /// <remarks></remarks>
        public bool Itersecting
        {
            get { return itersecting; }
            set { itersecting = value; }
        }

        /// <summary>
        /// Returns of the point(s) of Intersection
        /// </summary>
        /// <remarks></remarks>
        public Point2D[] IntersectionPoint
        {
            get { return intersectionPoint; }
            set { intersectionPoint = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Paralell
        {
            get { return paralell; }

            set { paralell = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IntersectionType Type
        {
            get { return type; }
            set { type = value; }
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

        /// <summary>
        /// Finds the Intersection of a Ellipse and a Line
        /// </summary>
        /// <param name="Ellipse"></param>
        /// <param name="Line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse Ellipse, LineSegment Line)
        {
            double SlopeA = Line.Slope();
            double SlopeB = (Line.A.Y - (SlopeA * Line.A.X));
            double A = (1 + (SlopeA * SlopeA));
            double B = ((2 * (SlopeA * (SlopeB - Ellipse.Center.Y))) - (2 * Ellipse.Center.X));
            double C = ((Ellipse.Center.X * Ellipse.Center.X) + (((SlopeB - Ellipse.Center.Y) * (SlopeB - Ellipse.Center.X)) - (Ellipse.MajorRadius * Ellipse.MajorRadius)));
            double XA = ((((B * -1) + Math.Sqrt(((B * B) - (A * C)))) / (2 * A)));
            double XB = ((((B - Math.Sqrt(((B * B) - (A * C)))) * -1) / (2 * A)));
            double YA = ((SlopeA * XA) + SlopeB);
            double YB = ((SlopeA * XB) + SlopeB);
            return new LineSegment(XA, YA, XB, YB);
        }

        /// <summary>
        /// Finds Intersection of two Ellipse'
        /// </summary>
        /// <param name="EllipseA"></param>
        /// <param name="EllipseB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(Ellipse EllipseA, Ellipse EllipseB)
        {
            double D = (EllipseB.Center.X * EllipseB.Center.X - EllipseA.Center.X * EllipseA.Center.X - EllipseB.MajorRadius * EllipseB.MajorRadius - Math.Pow(EllipseB.Center.Y - EllipseA.Center.Y, 2) + EllipseA.MajorRadius * EllipseA.MajorRadius);
            double A = (Math.Pow(2 * EllipseA.Center.X - 2 * EllipseB.Center.X, 2) + 4 * Math.Pow(EllipseB.Center.Y - EllipseA.Center.Y, 2));
            double B = (2 * D * (2 * EllipseA.Center.X - 2 * EllipseB.Center.X) - 8 * EllipseB.Center.X * Math.Pow(EllipseB.Center.Y - EllipseA.Center.Y, 2));
            double C = (4 * EllipseB.Center.X * EllipseB.Center.X * Math.Pow(EllipseB.Center.Y - EllipseA.Center.Y, 2) + D * D - 4 * Math.Pow(EllipseB.Center.Y - EllipseA.Center.Y, 2) * EllipseB.MajorRadius * EllipseB.MajorRadius);
            double XA = ((-B + Math.Sqrt(B * B - 4 * A * C)) / (2 * A));
            double XB = ((-B - Math.Sqrt(B * B - 4 * A * C)) / (2 * A));
            double YA = (Math.Sqrt(EllipseA.MajorRadius * EllipseA.MajorRadius - Math.Pow(XA - EllipseA.Center.X, 2)) + EllipseA.Center.Y);
            double YB = (-Math.Sqrt(EllipseA.MajorRadius * EllipseA.MajorRadius - Math.Pow(XA - EllipseA.Center.X, 2)) + EllipseA.Center.Y);
            double YC = (Math.Sqrt(EllipseA.MajorRadius * EllipseA.MajorRadius - Math.Pow(XB - EllipseA.Center.X, 2)) + EllipseA.Center.Y);
            double YD = (-Math.Sqrt(EllipseA.MajorRadius * EllipseA.MajorRadius - Math.Pow(XB - EllipseA.Center.X, 2)) + EllipseA.Center.Y);
            double E = ((XA - EllipseB.Center.X) + Math.Pow(YA - EllipseB.Center.Y, 2) - EllipseB.MajorRadius * EllipseB.MajorRadius);
            double F = ((XA - EllipseB.Center.X) + Math.Pow(YB - EllipseB.Center.Y, 2) - EllipseB.MajorRadius * EllipseB.MajorRadius);
            double G = ((XB - EllipseB.Center.X) + Math.Pow(YC - EllipseB.Center.Y, 2) - EllipseB.MajorRadius * EllipseB.MajorRadius);
            double H = ((XB - EllipseB.Center.X) + Math.Pow(YD - EllipseB.Center.Y, 2) - EllipseB.MajorRadius * EllipseB.MajorRadius);
            if (Math.Abs(F) < Math.Abs(E)) YA = YB;
            if (Math.Abs(H) < Math.Abs(G)) YC = YD;
            if (EllipseA.Center.Y == EllipseB.Center.Y) YC = 2 * EllipseA.Center.Y - YA;
            return new LineSegment(XA, YA, XB, YC);
        }

        /// <summary>
        /// Finds the Intersection of a line and an Ellipse
        /// </summary>
        /// <param name="Line"></param>
        /// <param name="Ellipse"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static LineSegment Intersect(LineSegment Line, Ellipse Ellipse)
        {
            double SlopeA = Line.Slope();
            double SlopeB = (Line.A.Y - (SlopeA * Line.A.X));
            double A = (1 + (SlopeA * SlopeA));
            double B = ((2 * (SlopeA * (SlopeB - Ellipse.Center.Y))) - (2 * Ellipse.Center.X));
            double C = ((Ellipse.Center.X * Ellipse.Center.X) + (((SlopeB - Ellipse.Center.Y) * (SlopeB - Ellipse.Center.X)) - (Ellipse.MajorRadius * Ellipse.MajorRadius)));
            double XA = ((((B * -1) + Math.Sqrt(((B * B) - (A * C)))) / (2 * A)));
            double XB = ((((B - Math.Sqrt(((B * B) - (A * C)))) * -1) / (2 * A)));
            double YA = ((SlopeA * XA) + SlopeB);
            double YB = ((SlopeA * XB) + SlopeB);
            return new LineSegment(XA, YA, XB, YB);
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
            Point2D Slopes = new Point2D((float)PointB.Slope(PointA), (float)PointD.Slope(PointC));
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
        /// should check if the intersection is done in floating point with a result outside
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
            Point2D Slopes = new Point2D((float)PointA.Slope(PointB), (float)PointC.Slope(PointD));
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
        /// should check if the intersection is done in floating point with a result outside
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
            Point2D Slopes = new Point2D((float)PointA.Slope(PointB), (float)PointC.Slope(PointD));
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
        /// should check if the intersection is done in floating point with a result outside
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
            Point2D Slopes = new Point2D((float)DeltaDC.CrossProduct(DeltaBA), (float)DeltaDC.CrossProduct(DeltaCA));
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
                        / (float)DeltaBA.CrossProduct(DeltaDC));
            double t = (((DeltaDC.X * DeltaAC.Y)
                        + (DeltaCA.X * DeltaDC.Y))
                        / (float)DeltaBA.CrossProduct(DeltaDC));
            //  If it exists, the point of intersection is:
            return new Intersection(
                new Point2D((PointA.X + (t * DeltaBA.X)), (PointA.Y + (t * DeltaBA.Y))),
                                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                                IntersectionType.Cross
                                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Point"></param>
        /// <param name="Line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool PointOnLine(Point2D Point, LineSegment Line)
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
        public static float Distance(Point2D PointA, Point2D PointB, Point2D PointC)
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
        public static float DistanceToSegment(Point2D p, Point2D A, Point2D B, out Point2D RetNear)
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
                return (float)(Math.Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
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
            return (float)(Math.Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
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
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="close_distance"></param>
        /// <returns>Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).</returns>
        public bool PointNearPoint(double x1, double y1, double x2, double y2, double close_distance)
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
        public bool PointNearPoint2(double x1, double y1, double x2, double y2, double close_distance)
        {
            return ((Math.Abs((x2 - x1)) <= close_distance) && (Math.Abs((y2 - y1)) <= close_distance));
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
        public bool PointNearSegment(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
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
        public bool PointNearEllipse(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
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
        public bool PointNearEllipse2(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            double a = ((Math.Abs((x2 - x1)) / 2) + close_distance);
            double b = ((Math.Abs((y2 - y1)) / 2) + close_distance);
            px = (px - (x2 + x1) / 2);
            py = (py - (y2 + y1) / 2);
            return ((((px * px) / (a * a)) + (((py * py) / (b * b))) <= 1));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="pgon_pts"></param>
        ///// <returns>Return True if the point is within the polygon.</returns>
        //public bool PointNearPolygon(double x, double y, Point2D[] pgon_pts)
        //{
        //    //  Make a region for the polygon.
        //    GraphicsPath pgon_path = new GraphicsPath(FillMode.Alternate);
        //    pgon_path.AddPolygon(pgon_pts);
        //    Region pgon_region = new Region(pgon_path);
        //    //  See if the point is in the region.
        //    return pgon_region.IsVisible(x, y);
        //}

        ///// <summary>
        ///// Return True if the point is within the polygon.
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="pgon_pts"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public bool PointInPolygon(double x, double y, Point2D[] pgon_pts)
        //{
        //    //  Make a region for the polygon.
        //    GraphicsPath pgon_path = new GraphicsPath(FillMode.Alternate);
        //    pgon_path.AddPolygon(pgon_pts);
        //    Region pgon_region = new Region(pgon_path);
        //    //  See if the point is in the region.
        //    return pgon_region.IsVisible(x, y);
        //}
    }
}
