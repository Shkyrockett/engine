// <copyright file="IntersectionType.cs" >
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
using System.Drawing;
using System.Linq;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class IntersectionExtention
    {
        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or 
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        public static bool AreClose(this float value1, float value2)
        {
            //in case they are Infinities (then epsilon check does not work)
            if (value1 == value2) return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * MathExtensions.DoubleEpsilon;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// Compares two points for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='point1'>The first point to compare</param>
        /// <param name='point2'>The second point to compare</param>
        /// <returns>Whether or not the two points are equal</returns>
        public static bool AreClose(this PointF point1, PointF point2)
        {
            return AreClose(point1.X, point2.X) &&
            AreClose(point1.Y, point2.Y);
        }

        /// <summary>
        /// Compares two Size instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='size1'>The first size to compare</param>
        /// <param name='size2'>The second size to compare</param>
        /// <returns>Whether or not the two Size instances are equal</returns>
        public static bool AreClose(this SizeF size1, SizeF size2)
        {
            return AreClose(size1.Width, size2.Width) &&
                   AreClose(size1.Height, size2.Height);
        }

        /// <summary>
        /// Compares two Vector instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        /// <returns>Whether or not the two Vector instances are equal</returns>
        public static bool AreClose(this VectorF vector1, VectorF vector2)
        {
            return AreClose(vector1.X, vector2.X) &&
                   AreClose(vector1.Y, vector2.Y);
        }

        /// <summary>
        /// Compares two rectangles for fuzzy equality.  This function
        /// helps compensate for the fact that double values can 
        /// acquire error when operated upon
        /// </summary>
        /// <param name='rect1'>The first rectangle to compare</param>
        /// <param name='rect2'>The second rectangle to compare</param>
        /// <returns>Whether or not the two rectangles are equal</returns>
        public static bool AreClose(this RectangleF rect1, RectangleF rect2)
        {
            // If they're both empty, don't bother with the double logic.
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }

            // At this point, rect1 isn't empty, so the first thing we can test is
            // rect2.IsEmpty, followed by property-wise compares.

            return (!rect2.IsEmpty) &&
                AreClose(rect1.X, rect2.X) &&
                AreClose(rect1.Y, rect2.Y) &&
                AreClose(rect1.Height, rect2.Height) &&
                AreClose(rect1.Width, rect2.Width);
        }

        /// <summary>
        /// rectHasNaN - this returns true if this rectangle has X, Y , Height or Width as NaN.
        /// </summary>
        /// <param name='rect'>The rectangle to test</param>
        /// <returns>returns whether the Rectangle has NaN</returns>        
        public static bool RectHasNaN(this RectangleF rect)
        {
            if (float.IsNaN(rect.X)
                 || float.IsNaN(rect.Y)
                 || float.IsNaN(rect.Height)
                 || float.IsNaN(rect.Width))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        public static bool PointInPolygonSaeedAmiri(this Polygon polygon, PointF point)
        {
            var coef = polygon.Points.Skip(1).Select((p, i) =>
                                            (point.Y - polygon[i].Y) * (p.X - polygon[i].X)
                                          - (point.X - polygon[i].X) * (p.Y - polygon[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        /// https://social.msdn.microsoft.com/Forums/windows/en-US/95055cdc-60f8-4c22-8270-ab5f9870270a/determine-if-the-point-is-in-the-polygon-c?forum=winforms
        /// </remarks>
        public static bool PointInPolygonKeith(this Polygon polygon, PointF point)
        {
            PointF p1;
            PointF p2;
            bool inside = false;
            if (polygon.Points.Count < 3)
            {
                return inside;
            }
            var oldPoint = new PointF(
                polygon.Points[polygon.Points.Count - 1].X, polygon.Points[polygon.Points.Count - 1].Y);
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                var newPoint = new PointF(polygon.Points[i].X, polygon.Points[i].Y);
                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }
                if ((newPoint.X < point.X) == (point.X <= oldPoint.X)
                    && (point.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (point.X - p1.X))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }
            return inside;
        }

        /// <summary>
        /// Determines if the given point is inside the polygon
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="point">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        public static bool PointInPolygonMeowNET(this Polygon polygon, PointF point)
        {
            bool result = false;
            int j = polygon.Points.Count() - 1;
            for (int i = 0; i < polygon.Points.Count(); i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon/
        ///  Globals which should be set before calling this function:
        ///
        ///  int    polyCorners  =  how many corners the polygon has (no repeats)
        ///  float  polyX[]      =  horizontal coordinates of corners
        ///  float  polyY[]      =  vertical coordinates of corners
        ///  float  x, y         =  point to be tested
        ///
        ///  (Globals are used in this example for purposes of speed.  Change as
        ///  desired.)
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        ///
        ///  Note that division by zero is avoided because the division is protected
        ///  by the "if" clause which surrounds it.
        /// </remarks>
        public static bool PointInPolygonAlienRyderFlex(this Polygon polygon, PointF point)
        {
            int i;
            int j = polygon.Points.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Points.Count; i++)
            {
                if (polygon.Points[i].Y < point.Y && polygon.Points[j].Y >= point.Y
                || polygon.Points[j].Y < point.Y && polygon.Points[i].Y >= point.Y)
                {
                    if (polygon.Points[i].X + (point.Y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) * (polygon.Points[j].X - polygon.Points[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon/
        ///  Globals which should be set before calling this function:
        ///
        ///  int    polyCorners  =  how many corners the polygon has (no repeats)
        ///  float  polyX[]      =  horizontal coordinates of corners
        ///  float  polyY[]      =  vertical coordinates of corners
        ///  float  x, y         =  point to be tested
        ///
        ///  (Globals are used in this example for purposes of speed.  Change as
        ///  desired.)
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        ///
        ///  Note that division by zero is avoided because the division is protected
        ///  by the "if" clause which surrounds it.
        /// </remarks>
        public static bool PointInPolygonNathanMercer(this Polygon polygon, PointF point)
        {
            int i;
            int j = polygon.Points.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Points.Count; i++)
            {
                if ((polygon.Points[i].Y < point.Y && polygon.Points[j].Y >= point.Y
                || polygon.Points[j].Y < point.Y && polygon.Points[i].Y >= point.Y)
                && (polygon.Points[i].X <= point.X || polygon.Points[j].X <= point.X))
                {
                    if (polygon.Points[i].X + (point.Y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) * (polygon.Points[j].X - polygon.Points[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon/
        ///  Globals which should be set before calling this function:
        ///
        ///  int    polyCorners  =  how many corners the polygon has (no repeats)
        ///  float  polyX[]      =  horizontal coordinates of corners
        ///  float  polyY[]      =  vertical coordinates of corners
        ///  float  x, y         =  point to be tested
        ///
        ///  (Globals are used in this example for purposes of speed.  Change as
        ///  desired.)
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        ///
        ///  Note that division by zero is avoided because the division is protected
        ///  by the "if" clause which surrounds it.
        /// </remarks>
        public static bool PointInPolygonLaschaLagidse(this Polygon polygon, PointF point)
        {
            int i;
            int j = polygon.Points.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Points.Count; i++)
            {
                if ((polygon.Points[i].Y < point.Y && polygon.Points[j].Y >= point.Y
                || polygon.Points[j].Y < point.Y && polygon.Points[i].Y >= point.Y)
                && (polygon.Points[i].X <= point.X || polygon.Points[j].X <= point.X))
                {
                    oddNodes ^= (polygon.Points[i].X + (point.Y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) * (polygon.Points[j].X - polygon.Points[i].X) < point.X);
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        /// http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test
        /// http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
        /// </remarks>
        public static bool PointInPolygonGilKr(this Polygon polygon, PointF point)
        {
            int i, j;
            int nvert = polygon.Points.Count;
            bool c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon.Points[i].Y > point.Y) != (polygon.Points[j].Y > point.Y)) &&
                 (point.X < (polygon.Points[j].X - polygon.Points[i].X) * (point.Y - polygon.Points[i].Y) / (polygon.Points[j].Y - polygon.Points[i].Y) + polygon.Points[i].X))
                    c = !c;
            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test</remarks>
        public static bool PointInPolygonMKatz(this Polygon polygon, PointF point)
        {
            double minX = polygon.Points[0].X;
            double maxX = polygon.Points[0].X;
            double minY = polygon.Points[0].Y;
            double maxY = polygon.Points[0].Y;
            for (int i = 1; i < polygon.Points.Count; i++)
            {
                PointF q = polygon.Points[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minY = Math.Min(q.Y, minY);
                maxY = Math.Max(q.Y, maxY);
            }

            if (point.X < minX || point.X > maxX || point.Y < minY || point.Y > maxY)
            {
                return false;
            }

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Points.Count - 1; i < polygon.Points.Count; j = i++)
            {
                if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                     point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns>Return true if the point is in the polygon.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static bool PointInPolygonRodStephens(this Polygon polygon, PointF point)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon.Points.Count - 1;
            float total_angle = GetAngle(
                polygon.Points[max_point].X, polygon.Points[max_point].Y,
                point.X, point.Y,
                polygon.Points[0].X, polygon.Points[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon.Points[i].X, polygon.Points[i].Y,
                    point.X, point.Y,
                    polygon.Points[i + 1].X, polygon.Points[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 0.000001);
        }

        /// <summary>
        /// Find out if a Point is in an Arc. 
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool PointInArc(this Arc arc, PointF point)
        {
            //ToDo: Locate the position in relation to the arc start and end points chord.
            return (arc.Radius > (float)(arc.Center.Length(point)));
        }

        /// <summary>
        /// Find out if a Point is in a Circle. 
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool PointInCircle(this Circle circle, PointF point)
        {
            return (circle.Radius > (float)(circle.Center.Length(point)));
        }

        /// <summary>
        /// Find the polygon's centroid.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/find-the-centroid-of-a-polygon-in-c/</remarks>
        public static PointF Centroid(this Polygon polygon)
        {
            // Add the first point at the end of the array.
            int num_points = polygon.Points.Count;
            PointF[] pts = new PointF[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Find the centroid.
            float X = 0;
            float Y = 0;
            float second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y -
                    pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            float polygon_area = polygon.PolygonArea();
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }

            return new PointF(X, Y);
        }

        /// <summary>
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns>
        /// Return the polygon's area in "square units."
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/calculate-the-area-of-a-polygon-in-c/</remarks>
        public static float PolygonArea(this Polygon polygon)
        {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polygon is
            // oriented clockwise.
            return Math.Abs(polygon.SignedPolygonArea());
        }

        /// <summary>
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// The value will be negative if the polygon is
        /// oriented clockwise.
        /// </summary>
        /// <returns>
        /// Return the polygon's area in "square units."
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/calculate-the-area-of-a-polygon-in-c/</remarks>
        public static float SignedPolygonArea(this Polygon polygon)
        {
            // Add the first point to the end.
            int num_points = polygon.Points.Count;
            PointF[] pts = new PointF[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Get the areas.
            float area = 0;
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

                float cross_product =
                    CrossProductLength(
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
            if (GetAngle(
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
                    if (triangle.PointInPolygonRodStephens(polygon.Points[i]))
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
            //List<PointF> points =  new List<PointF>(polygon.Points.Count - 1);
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
            List<PointF> pts = new List<PointF>(polygon.Points);

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
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns>
        /// Return true if the polygon is oriented clockwise.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static bool PolygonIsOrientedClockwise(this Polygon polygon)
        {
            return (polygon.SignedPolygonArea() < 0);
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
        /// Return True if the segments intersect.
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <param name="A1"></param>
        /// <param name="B1"></param>
        /// <param name="A2"></param>
        /// <param name="B2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool SegmentsIntersect(float X1, float Y1, float X2, float Y2, float A1, float B1, float A2, float B2)
        {
            float dx = (X2 - X1);
            float dy = (Y2 - Y1);
            float da = (A2 - A1);
            float db = (B2 - B1);
            if ((((da * dy) - (db * dx)) == 0)) return false; //  The segments are parallel.
            float s = (((dx * (B1 - Y1)) + (dy * (X1 - A1))) / ((da * dy) - (db * dx)));
            float t = (((da * (Y1 - B1)) + (db * (A1 - X1))) / ((db * dx) - (da * dy)));

            return ((s >= 0.0d) && (s <= 1.0d) && (t >= 0.0d) && (t <= 1.0d));

            // If it exists, the point of intersection is:
            // (x1 + t * dx, y1 + t * dy)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the dot product.
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static float GetAngle(PointF a, PointF b, PointF c)
        {
            // Get the dot product.
            float dotProduct = DotProduct(a, b, c);

            // Get the cross product.
            float crossProduct = CrossProductLength(a, b, c);

            // Calculate the angle.
            return (float)Math.Atan2(crossProduct, dotProduct);
        }

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static float CrossProductLength(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static float CrossProductLength(PointF a, PointF b, PointF c)
        {
            // Get the vectors' coordinates.
            float bax = a.X - b.X;
            float bay = a.Y - b.Y;
            float bcx = c.X - b.X;
            float bcy = c.Y - b.Y;

            // Calculate the Z coordinate of the cross product.
            return (bax * bcy - bay * bcx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </remarks>
        public static float DotProduct(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </remarks>
        public static float DotProduct(PointF a, PointF b, PointF c)
        {
            // Get the vectors' coordinates.
            float BAx = a.X - b.X;
            float BAy = a.Y - b.Y;
            float BCx = c.X - b.X;
            float BCy = c.Y - b.Y;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static PointF ClosestPointOnLineSegmentMvG(PointF a, PointF b, PointF p)
        {
            // Vector A->B
            PointF diffAB = new PointF(a.X - b.X, a.Y - b.Y);

            float det = a.Y * b.X - a.X * b.Y;

            float dot = diffAB.X * p.X + diffAB.Y * p.Y;

            PointF val = new PointF(dot * diffAB.X + det * diffAB.Y, dot * diffAB.Y - det * diffAB.X);

            float magnitude = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            float inverseDist = 1 / magnitude;

            return new PointF(val.X * inverseDist, val.Y * inverseDist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static PointF ClosestPointOnLineSegmentDarienPardinas(PointF a, PointF b, PointF p)
        {
            // Vector A->P 
            PointF diffAP = new PointF(p.X - a.X, p.Y - a.Y);

            // Vector A->B
            PointF diffAB = new PointF(b.X - a.X, b.Y - a.Y);

            float dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            float dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            //  # The normalized "distance" from a to the closest point
            float dist = dotABAP / dotAB;

            if (dist < 0)
            {
                return a;
            }
            else if (dist > dotABAP)
            {
                return b;
            }
            else
            {
                return new PointF(a.X + diffAB.X * dist, a.Y + diffAB.Y * dist);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static PointF ClosestPointOnLineDarienPardinas(PointF a, PointF b, PointF p)
        {
            // Vector A->P 
            PointF diffAP = new PointF(p.X - a.X, p.Y - a.Y);

            // Vector A->B
            PointF diffAB = new PointF(b.X - a.X, b.Y - a.Y);

            float dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;

            // The dot product of diffAP and diffAB
            float dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;

            // The normalized "distance" from a to the closest point
            float dist = dotABAP / dotAB;

            return new PointF(a.X + diffAB.X * dist, a.Y + diffAB.Y * dist);
        }
    }
}
