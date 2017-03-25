// <copyright file="Experiments.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Math;
using static Engine.Maths;
using System.Numerics;

namespace MethodSpeedTester
{
    /// <summary>
    /// Class to contain experimental methods to test.
    /// </summary>
    public class Experiments
    {
        #region Absolute Angle

        /// <summary>
        /// Set of tests to run testing methods that calculate the absolute angle of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AbsoluteAngleTests))]
        public static List<SpeedTester> AbsoluteAngleTests() => new List<SpeedTester> {
                new SpeedTester(() => AbsoluteAngle0(0, 0, 1, 1),
                $"{nameof(Experiments.AbsoluteAngle0)}(0, 0, 1, 1)"),
                 new SpeedTester(() => AbsoluteAngle1(0, 0, 1, 1),
                $"{nameof(Experiments.AbsoluteAngle1)}(0, 0,, 1, 1)")
           };

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="aX">Horizontal Component of Point Starting Point</param>
        /// <param name="aY">Vertical Component of Point Starting Point</param>
        /// <param name="bX">Horizontal Component of Ending Point</param>
        /// <param name="bY">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle0(double aX, double aY, double bX, double bY)
        {
            // Find the angle of point a and point b.
            var test = -Angle(aX, aY, bX, bY) % PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
                test += PI;

            return test;
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="aX">Horizontal Component of Point Starting Point</param>
        /// <param name="aY">Vertical Component of Point Starting Point</param>
        /// <param name="bX">Horizontal Component of Ending Point</param>
        /// <param name="bY">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle1(double aX, double aY, double bX, double bY)
        {
            // Find the angle of point a and point b.
            var test = -Angle(aX, aY, bX, bY) % PI;
            return test < 0 ? test += PI : test;
        }

        #endregion

        #region Angle Between Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AngleBetweenTests))]
        public static List<SpeedTester> AngleBetweenTests() => new List<SpeedTester> {
                new SpeedTester(() => AngleBetween(0, 0, 1, 1),
                $"{nameof(Experiments.AngleBetween)}(0, 0, 1, 1)")
           };

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX"></param>
        /// <param name="uY"></param>
        /// <param name="vX"></param>
        /// <param name="vY"></param>
        /// <returns></returns>
        /// <remarks>http://james-ramsden.com/angle-between-two-vectors/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY,
            double vX, double vY)
            => Acos((uX * vX + uY * vY) / Sqrt((uX * uX + uY * uY) * (vX * vX + vY * vY)));

        #endregion

        #region Angle Between Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AngleBetween3DTests))]
        public static List<SpeedTester> AngleBetween3DTests() => new List<SpeedTester> {
                new SpeedTester(() => AngleBetween(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.AngleBetween)}(0, 0, 0, 1, 1, 1)")
           };

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX"></param>
        /// <param name="uY"></param>
        /// <param name="uZ"></param>
        /// <param name="vX"></param>
        /// <param name="vY"></param>
        /// <param name="vZ"></param>
        /// <returns></returns>
        /// <remarks>http://james-ramsden.com/angle-between-two-vectors/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY, double uZ,
            double vX, double vY, double vZ)
            => Acos((uX * vX + uY * vY + uZ * vZ) / Sqrt((uX * uX + uY * uY + uZ * uZ) * (vX * vX + vY * vY + vZ * vZ)));

        #endregion

        #region Angle of a Vector

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(AngleofVectorTests))]
        public static List<SpeedTester> AngleofVectorTests()
            => new List<SpeedTester> {
                new SpeedTester(() => GetAngle0(0, 1),
                $"{nameof(Experiments.GetAngle0)}(0, 1)"),
                new SpeedTester(() => GetAngleAtan2v2(0, 1),
                $"{nameof(Experiments.GetAngleAtan2v2)}(0, 1)"),
                new SpeedTester(() => GetAngle(0, 1),
                $"{nameof(Experiments.GetAngle)}(0, 1)")
           };

        /// <summary>
        /// Angle with tangent opp/hyp
        /// </summary>
        /// <param name="i">opposite component.</param>
        /// <param name="j">adjacent component.</param>
        /// <returns>Return the angle with tangent opp/hyp. The returned value is between PI and -PI.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetAngle0(double i, double j)
            => Atan2(i, -j);// * 180 / PI; // Original source method converted radians to degrees.

        /// <summary>
        /// Returns the Angle of two deltas.
        /// </summary>
        /// <param name="i">Delta Angle 1</param>
        /// <param name="j">Delta Angle 2</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        public static double GetAngleAtan2v2(double i, double j)
        {
            if ((Abs(i) < DoubleEpsilon) && (Abs(j) < DoubleEpsilon))
                return 0;
            var Value = Asin(i / Sqrt(i * i + j * j));
            if (j < 0)
                Value = (PI - Value);
            if (Value < 0)
                Value = (Value + (2 * PI));
            return Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static double GetAngle(double i, double j)
            => (Tau + (j > 0.0 ? 1.0 : -1.0) * Acos(i / Sqrt(i * i + j * j)) % Tau);

        #endregion

        #region Angle of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle2DTests))]
        public static List<SpeedTester> Angle2DTests() => new List<SpeedTester> {
                new SpeedTester(() => Angle(0, 0, 1, 1),
                $"{nameof(Experiments.Angle)}(0, 0, 1, 1)")
           };

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(
            double x1, double y1,
            double x2, double y2)
            => Atan2((y1 - y2), (x1 - x2));

        #endregion

        #region Angle of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle3DTests))]
        public static List<SpeedTester> Angle3DTests() => new List<SpeedTester> {
                new SpeedTester(() => Angle(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.Angle)}(0, 0, 0, 1, 1, 1)")
           };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        public static double Angle(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (Abs(x1 - x2) < DoubleEpsilon
            && Abs(y1 - y2) < DoubleEpsilon
            && Abs(z1 - z2) < DoubleEpsilon)
            ? 0 : Acos(Min(1.0d, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

        #endregion

        #region Angle of the vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle3Points2DTests))]
        public static List<SpeedTester> Angle3Points2DTests() => new List<SpeedTester> {
                new SpeedTester(() => AngleVector_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.AngleVector_0)}(0, 0, 1, 0, 1, 1)"),
                 new SpeedTester(() => AngleVector_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.AngleVector_1)}(0, 0, 1, 0, 1, 1)")
           };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleVector_0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => Atan2(CrossProductVector2D_0(x1, y1, x2, y2, x3, y3), DotProductVector2D_0(x1, y1, x2, y2, x3, y3));

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static double AngleVector_1(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
        {
            // Get the dot product.
            var dotProduct = DotProductVector2D_0(aX, aY, bX, bY, cX, cY);

            // Get the cross product.
            var crossProduct = CrossProductVector2D_0(aX, aY, bX, bY, cX, cY);

            // Calculate the angle.
            return Atan2(crossProduct, dotProduct);
        }

        #endregion

        #region Area of Polygon

        /// <summary>
        /// Set of tests to run testing methods that calculate the area of a polygon.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(PolygonAreaTests))]
        public static List<SpeedTester> PolygonAreaTests() => new List<SpeedTester> {
                new SpeedTester(() => PolygonArea00(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea00)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea0(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea0)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea1(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea1)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea2(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea2)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea3(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea3)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea4(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea4)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})"),
                new SpeedTester(() => PolygonArea5(new List<Point2D> { new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }),
                $"{nameof(Experiments.PolygonArea5)}(new List<Point2D> {{ new Point2D(0, 0), new Point2D(1, 0), new Point2D(1, 1) }})")
          };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>From http://www.angusj.com</remarks>
        public static double PolygonArea00(List<Point2D> polygon)
        {
            var cnt = polygon.Count;
            if (cnt < 3) return 0;
            double area = 0;
            for (int i = 0, j = cnt - 1; i < cnt; ++i)
            {
                area += (polygon[j].X + polygon[i].X) * (polygon[j].Y - polygon[i].Y);
                j = i;
            }

            return -area * 0.5;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon_area/</remarks>
        public static double PolygonArea0(IEnumerable<Point2D> polygon)
        {
            var points = polygon as List<Point2D>;
            var j = points.Count - 1;
            var area = 0d;
            for (var i = 0; i < points.Count; i++)
            {
                area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y); j = i;
            }

            return area * 0.5d; ;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/geometry/polygonmesh/source1.c</remarks>
        public static double PolygonArea1(List<Point2D> polygon)
        {
            int i, j;
            double area = 0;

            for (i = 0; i < polygon.Count; i++)
            {
                j = (i + 1) % polygon.Count;
                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }

            area /= 2;
            return (area < 0 ? -area : area);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp</remarks>
        public static double PolygonArea2(List<Point2D> polygon)
        {
            var points = polygon;

            points.Add(points[0]);
            return Abs(points.Take(points.Count - 1)
               .Select((p, i) => (points[i + 1].X - p.X) * (points[i + 1].Y + p.Y))
               .Sum() / 2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/2034540/calculating-area-of-irregular-polygon-in-c-sharp</remarks>
        public static double PolygonArea3(List<Point2D> polygon)
        {
            polygon.Add(polygon[0]);
            return Abs(polygon.Take(polygon.Count - 1).Select((p, i) => (p.X * polygon[i + 1].Y) - (p.Y * polygon[i + 1].X)).Sum() / 2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static double PolygonArea4(List<Point2D> polygon)
        {
            if (polygon.Count < 3)
            {
                return 0;
            }
            var area = Determinant(polygon[polygon.Count - 1].X, polygon[polygon.Count - 1].Y, polygon[0].X, polygon[0].Y);
            for (var i = 1; i < polygon.Count; i++)
            {
                area += Determinant(polygon[i - 1].X, polygon[i - 1].Y, polygon[i].X, polygon[i].Y);
            }
            return area / 2;
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
        public static double PolygonArea5(IEnumerable<Point2D> polygon)
            => Abs(SignedPolygonArea5(polygon as List<Point2D>));

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
        private static double SignedPolygonArea5(List<Point2D> polygon)
        {
            // Add the first point to the end.
            var num_points = polygon.Count;
            var pts = new Point2D[num_points + 1];
            polygon.CopyTo(pts, 0);
            pts[num_points] = polygon[0];

            // Get the areas.
            double area = 0;
            for (var i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X)
                    * (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        #endregion

        #region Area of the intersection of two circles

        /// <summary>
        ///
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.xarg.org/2016/07/calculate-the-intersection-area-of-two-circles/
        /// </remarks>
        public double Area(Circle A, Circle B)
        {
            var d = Sqrt(Pow(B.X - A.X, 2) + Pow(B.Y - A.Y, 2));
            if (d < A.Radius + B.Radius)
            {
                var a = A.Radius * A.Radius;
                var b = B.Radius * B.Radius;
                var x = (a - b + d * d) / (2 * d);
                var z = x * x;
                var y = Sqrt(a - z);
                if (d < Abs(B.Radius - A.Radius))
                {
                    return PI * Min(a, b);
                }

                return a * Asin(y / A.Radius) + b * Asin(y / B.Radius) - y * (x + Sqrt(z + b - a));
            }

            return 0;
        }

        #endregion

        #region Barycentric

        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static double Barycentric(float value1, double value2, double value3, double amount1, double amount2)
                => value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;

        #endregion

        #region Bezier Coefficients

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.gamedev.net/topic/643117-coefficients-for-bezier-curves/
        /// </remarks>
        private static (double A, double B, double C, double D) BezierCoefficients0(double a, double b, double c, double d)
            => (d - (3d * c) + (3d * b) - a,
                (3d * c) - (6d * b) + (3d * a),
                3d * (b - a),
                a);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://www.particleincell.com/2013/cubic-line-intersection/
        /// </remarks>
        private static (double A, double B, double C, double D) BezierCoefficients1(double a, double b, double c, double d)
            => (-a + 3d * b + -3d * c + d,
                3d * a - 6d * b + 3d * c,
                -3d * a + 3d * b,
                a);

        #endregion

        #region Boundaries of Polygons

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static Rectangle2D GetBounds(List<List<Point2D>> paths)
        {
            int i = 0, cnt = paths.Count;
            while (i < cnt && paths[i].Count == 0) i++;
            if (i == cnt) return new Rectangle2D(0, 0, 0, 0);
            var result = new Rectangle2D()
            {
                Left = paths[i][0].X
            };
            result.Right = result.Left;
            result.Top = paths[i][0].Y;
            result.Bottom = result.Top;
            for (; i < cnt; i++)
                for (var j = 0; j < paths[i].Count; j++)
                {
                    if (paths[i][j].X < result.Left) result.Left = paths[i][j].X;
                    else if (paths[i][j].X > result.Right) result.Right = paths[i][j].X;
                    if (paths[i][j].Y < result.Top) result.Top = paths[i][j].Y;
                    else if (paths[i][j].Y > result.Bottom) result.Bottom = paths[i][j].Y;
                }
            return result;
        }

        #endregion

        #region Boundaries of Polygons

        /// <summary>
        /// Set of tests to run testing methods that Find the bounds of polygons.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(BoundsOfPolygon))]
        public static List<SpeedTester> BoundsOfPolygon()
            => new List<SpeedTester> {
                new SpeedTester(() => PolygonBounds0(new List<Point2D>(){(10, 10), (25,5), (5,30)}),
                $"{nameof(Experiments.PolygonBounds0)}(new List<Point2D>(){{(10, 10), (25,5), (5,30)}})"),
                 new SpeedTester(() => PolygonBounds1(new List<Point2D>(){(10, 10), (25,5), (5,30)}),
                $"{nameof(Experiments.PolygonBounds1)}(new List<Point2D>(){{(10, 10), (25,5), (5,30)}})")
           };

        /// <summary>
        /// Calculate the external bounding rectangle of a Polygon.
        /// </summary>
        /// <param name="polygon">The points of the polygon.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds0(IEnumerable<Point2D> polygon)
        {
            var points = (polygon as List<Point2D>);
            if (points?.Count < 1) return null;

            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            foreach (Point2D point in points)
            {
                // ToDo: Measure performance impact of overwriting each time.
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D PolygonBounds1(List<Point2D> path)
        {
            var result = new Rectangle2D()
            {
                Left = path[0].X,
                Top = path[0].Y,
                Right = path[0].X,
                Bottom = path[0].Y
            };

            for (var i = 0; i < path.Count; i++)
            {
                if (path[i].X < result.Left) result.Left = path[i].X;
                else if (path[i].X > result.Right) result.Right = path[i].X;
                if (path[i].Y < result.Top) result.Top = path[i].Y;
                else if (path[i].Y > result.Bottom) result.Bottom = path[i].Y;
            }

            return result;
        }

        #endregion

        #region Boundary of Rotated Ellipse

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(BoundsOfRotatedEllipseTests))]
        public static List<SpeedTester> BoundsOfRotatedEllipseTests() => new List<SpeedTester> {
                new SpeedTester(() => EllipseBoundingBox(5, 5, 5, 4, 45d.ToRadians()),
                $"{nameof(Experiments.EllipseBoundingBox)}(5, 5, 5, 4, {45d.ToRadians()})"),
                 new SpeedTester(() => EllipseBounds(5, 5, 5, 4, 45d.ToRadians()),
                $"{nameof(Experiments.EllipseBounds)}(5, 5, 5, 4, {45d.ToRadians()})")
           };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Rectangle2D EllipseBoundingBox(double x, double y, int r1, int r2, double angle)
        {
            var a = r1 * Cos(angle);
            var b = r2 * Sin(angle);
            var c = r1 * Sin(angle);
            var d = r2 * Cos(angle);
            var width = Sqrt((a * a) + (b * b)) * 2;
            var height = Sqrt((c * c) + (d * d)) * 2;
            var x2 = x - width * 0.5;
            var y2 = y - height * 0.5;
            return new Rectangle2D(x2, y2, width, height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipseBounds(double x, double y, double r1, double r2, double angle)
        {
            var phi = angle;
            var aspect = r2 / r1;
            var ux = r1 * Cos(phi);
            var uy = r1 * Sin(phi);
            var vx = (r1 * aspect) * Cos(phi + PI / 2);
            var vy = (r1 * aspect) * Sin(phi + PI / 2);

            var bbox_halfwidth = Sqrt(ux * ux + vx * vx);
            var bbox_halfheight = Sqrt(uy * uy + vy * vy);

            return Rectangle2D.FromLTRB(
                (x - bbox_halfwidth),
                (y - bbox_halfheight),
                (x + bbox_halfwidth),
                (y + bbox_halfheight)
                );
        }

        #endregion

        #region Boundary of Rotated Elliptical Arc

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(BoundsOfRotatedEllipticalArcTests))]
        public static List<SpeedTester> BoundsOfRotatedEllipticalArcTests() => new List<SpeedTester> {
                new SpeedTester(() => Measurements.EllipticalArcBounds(200, 200, 100, 200, 30d.ToRadians(), -30d.ToRadians(), 90d.ToRadians()),
                $"{nameof(Measurements.EllipticalArcBounds)}(200, 200, 100, 200, {30d.ToRadians()}, {-30d.ToRadians()}, {90d.ToRadians()})"),
                new SpeedTester(() => EllipticalArc0(200, 200, 100, 200, 30d.ToRadians(), -30d.ToRadians(), 90d.ToRadians()),
                $"{nameof(Experiments.EllipticalArc0)}(200, 200, 100, 200, {30d.ToRadians()}, {-30d.ToRadians()}, {90d.ToRadians()})"),
                 new SpeedTester(() => EllipticalArc1(200, 200, 100, 200, 30d.ToRadians(), -30d.ToRadians(), 90d.ToRadians()),
                $"{nameof(Experiments.EllipticalArc1)}(200, 200, 100, 200, {30d.ToRadians()}, {-30d.ToRadians()}, {90d.ToRadians()})"),
                 new SpeedTester(() => EllipticalArc2(200, 200, 100, 200, 30d.ToRadians(), -30d.ToRadians(), 90d.ToRadians()),
                $"{nameof(Experiments.EllipticalArc2)}(200, 200, 100, 200, {30d.ToRadians()}, {-30d.ToRadians()}, {90d.ToRadians()})"),
                 new SpeedTester(() => EllipticalArc3(200, 200, 100, 200, 30d.ToRadians(), -30d.ToRadians(), 90d.ToRadians()),
                $"{nameof(Experiments.EllipticalArc3)}(200, 200, 100, 200, {30d.ToRadians()}, {-30d.ToRadians()}, {90d.ToRadians()})")
           };

        /// <summary>
        /// Find the close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <remarks>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArc0(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            var i = (r1 - r2) * (r1 + r2) * sinT * cosT;

            // Find the angles of the Cartesian extremes.
            var angles = new double[4] {
                Atan2(i, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, i),
                Atan2(i, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT) + PI,
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, i) + PI };

            // Sort the angles so that like sides are consistently at the same index.
            Array.Sort(angles);

            // Get the start and end angles adjusted to polar coordinates.
            var t0 = EllipsePolarAngle(startAngle, r1, r2);
            var t1 = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

            // Interpolate the ratios of height and width of the chord.
            var sinT0 = Sin(t0);
            var cosT0 = Cos(t0);
            var sinT1 = Sin(t1);
            var cosT1 = Cos(t1);

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT0 * cosT - r2 * sinT0 * sinT),
                    cY + (r1 * cosT0 * sinT + r2 * sinT0 * cosT)),
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT1 * cosT - r2 * sinT1 * sinT),
                    cY + (r1 * cosT1 * sinT + r2 * sinT1 * cosT)));

            // Find the parent ellipse's horizontal and vertical radii extremes.
            var halfWidth = Sqrt((r1 * r1 * cosT * cosT) + (r2 * r2 * sinT * sinT));
            var halfHeight = Sqrt((r1 * r1 * sinT * sinT) + (r2 * r2 * cosT * cosT));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Within(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Intersections.Within(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Within(angles[3], angle + startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        /// Find the close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <remarks>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArc1(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Find the angles of the Cartesian extremes.
            var angles = new double[4] {
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT),
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT) + PI,
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT) + PI };

            // Sort the angles so that like sides are consistently at the same index.
            Array.Sort(angles);

            // Get the start and end angles adjusted to polar coordinates.
            var t0 = EllipsePolarAngle(startAngle, r1, r2);
            var t1 = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

            // Interpolate the ratios of height and width of the chord.
            var sinT0 = Sin(t0);
            var cosT0 = Cos(t0);
            var sinT1 = Sin(t1);
            var cosT1 = Cos(t1);

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT0 * cosT - r2 * sinT0 * sinT),
                    cY + (r1 * cosT0 * sinT + r2 * sinT0 * cosT)),
                // Apply the rotation transformation and translate to new center.
                new Point2D(
                    cX + (r1 * cosT1 * cosT - r2 * sinT1 * sinT),
                    cY + (r1 * cosT1 * sinT + r2 * sinT1 * cosT)));

            // Find the parent ellipse's horizontal and vertical radii extremes.
            var halfWidth = Sqrt((r1 * r1 * cosT * cosT) + (r2 * r2 * sinT * sinT));
            var halfHeight = Sqrt((r1 * r1 * sinT * sinT) + (r2 * r2 * cosT * cosT));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Within(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Intersections.Within(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Within(angles[3], angle + startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        /// Find the close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <remarks>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArc2(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Find the angles of the Cartesian extremes.
            var angles = new List<double>(4) {
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT),
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT),
                Atan2((r1 - r2) * (r1 + r2) * sinT * cosT, r2 * r2 * sinT * sinT + r1 * r1 * cosT * cosT) + PI,
                Atan2(r1 * r1 * sinT * sinT + r2 * r2 * cosT * cosT, (r1 - r2) * (r1 + r2) * sinT * cosT) + PI };

            // Sort the angles so that like sides are consistently at the same index.
            angles.Sort();

            // Calculate the radii of the angle of rotation.
            var a = r1 * cosT;
            var b = r2 * sinT;
            var c = r1 * sinT;
            var d = r2 * cosT;

            // Find the parent ellipse's horizontal and vertical radii extremes.
            var halfWidth = Sqrt((a * a) + (b * b));
            var halfHeight = Sqrt((c * c) + (d * d));

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                Interpolators.EllipticalArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 0),
                Interpolators.EllipticalArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 1));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Within(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Intersections.Within(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Within(angles[3], angle + startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        /// Find the close fitting rectangular bounding box of a rotated ellipse elliptical arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a rotated elliptical arc.</returns>
        /// <remarks>
        /// Helpful hints on how this might be implemented came from:
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html,
        /// http://bazaar.launchpad.net/~inkscape.dev/inkscape/trunk/view/head:/src/2geom/elliptical-arc.cpp
        /// and http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArc3(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Calculate the radii of the angle of rotation.
            var a = r1 * cosT;
            var b = r2 * sinT;
            var c = r1 * sinT;
            var d = r2 * cosT;

            // Calculate the vectors of the Cartesian extremes.
            var u1 = r1 * Cos(Atan2(d, c));
            var v1 = -(r2 * Sin(Atan2(d, c)));
            var u2 = r1 * Cos(Atan2(-b, a));
            var v2 = -(r2 * Sin(Atan2(-b, a)));

            // Find the angles of the Cartesian extremes.
            var angles = new List<double>(4) {
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT),
                Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT),
                Atan2(u1 * sinT - v1 * cosT, u1 * cosT + v1 * sinT) + PI,
                Atan2(u2 * sinT - v2 * cosT, u2 * cosT + v2 * sinT) + PI };

            // Sort the angles so that like sides are consistently at the same index.
            angles.Sort();

            // Find the parent ellipse's horizontal and vertical radii extremes.
            var halfWidth = Sqrt((a * a) + (b * b));
            var halfHeight = Sqrt((c * c) + (d * d));

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                Interpolators.EllipticalArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 0),
                Interpolators.EllipticalArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 1));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Within(angles[0], angle + startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Within(angles[1], angle + startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;
            if (Intersections.Within(angles[2], angle + startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Within(angles[3], angle + startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html
        /// </remarks>
        public static Rectangle2D EllpticArc(
            double x1, double y1,
            double r1, double r2,
            double angle,
            bool largeArc, bool sweep,
            double x2, double y2)
        {
            double xmin;
            double ymin;
            double xmax;
            double ymax;

            if (r1 < 0d)
                r1 *= -1d;
            if (r2 < 0d)
                r2 *= -1d;

            if (r1 == 0d || r2 == 0d)
            {
                xmin = (x1 < x2 ? x1 : x2);
                xmax = (x1 > x2 ? x1 : x2);
                ymin = (y1 < y2 ? y1 : y2);
                ymax = (y1 > y2 ? y1 : y2);
                return Rectangle2D.FromLTRB(xmin, ymin, xmax, ymax);
            }

            var x1prime = Cos(angle) * (x1 - x2) / 2 + Sin(angle) * (y1 - y2) / 2;
            var y1prime = -Sin(angle) * (x1 - x2) / 2 + Cos(angle) * (y1 - y2) / 2;

            var radicant = (r1 * r1 * r2 * r2 - r1 * r1 * y1prime * y1prime - r2 * r2 * x1prime * x1prime);
            radicant /= (r1 * r1 * y1prime * y1prime + r2 * r2 * x1prime * x1prime);
            var cxprime = 0d;
            var cyprime = 0d;
            if (radicant < 0d)
            {
                var ratio = r1 / r2;
                radicant = y1prime * y1prime + x1prime * x1prime / (ratio * ratio);
                if (radicant < 0d)
                {
                    xmin = (x1 < x2 ? x1 : x2);
                    xmax = (x1 > x2 ? x1 : x2);
                    ymin = (y1 < y2 ? y1 : y2);
                    ymax = (y1 > y2 ? y1 : y2);
                    return Rectangle2D.FromLTRB(xmin, ymin, xmax, ymax);
                }
                r2 = Sqrt(radicant);
                r1 = ratio * r2;
            }
            else
            {
                var factor = (largeArc == sweep ? -1.0 : 1.0) * Sqrt(radicant);

                cxprime = factor * r1 * y1prime / r2;
                cyprime = -factor * r2 * x1prime / r1;
            }

            var cx = cxprime * Cos(angle) - cyprime * Sin(angle) + (x1 + x2) / 2;
            var cy = cxprime * Sin(angle) + cyprime * Cos(angle) + (y1 + y2) / 2;

            double txmin, txmax, tymin, tymax;

            if (angle == 0 || angle == PI)
            {
                xmin = cx - r1;
                txmin = GetAngle(-r1, 0);
                xmax = cx + r1;
                txmax = GetAngle(r1, 0);
                ymin = cy - r2;
                tymin = GetAngle(0, -r2);
                ymax = cy + r2;
                tymax = GetAngle(0, r2);
            }
            else if (angle == PI / 2.0 || angle == 3.0 * PI / 2.0)
            {
                xmin = cx - r2;
                txmin = GetAngle(-r2, 0);
                xmax = cx + r2;
                txmax = GetAngle(r2, 0);
                ymin = cy - r1;
                tymin = GetAngle(0, -r1);
                ymax = cy + r1;
                tymax = GetAngle(0, r1);
            }
            else
            {
                txmin = -Atan(r2 * Tan(angle) / r1);
                txmax = PI - Atan(r2 * Tan(angle) / r1);
                xmin = cx + r1 * Cos(txmin) * Cos(angle) - r2 * Sin(txmin) * Sin(angle);
                xmax = cx + r1 * Cos(txmax) * Cos(angle) - r2 * Sin(txmax) * Sin(angle);
                if (xmin > xmax)
                {
                    Swap(ref xmin, ref xmax);
                    Swap(ref txmin, ref txmax);
                }
                var tmpY = cy + r1 * Cos(txmin) * Sin(angle) + r2 * Sin(txmin) * Cos(angle);
                txmin = GetAngle(xmin - cx, tmpY - cy);
                tmpY = cy + r1 * Cos(txmax) * Sin(angle) + r2 * Sin(txmax) * Cos(angle);
                txmax = GetAngle(xmax - cx, tmpY - cy);

                tymin = Atan(r2 / (Tan(angle) * r1));
                tymax = Atan(r2 / (Tan(angle) * r1)) + PI;
                ymin = cy + r1 * Cos(tymin) * Sin(angle) + r2 * Sin(tymin) * Cos(angle);
                ymax = cy + r1 * Cos(tymax) * Sin(angle) + r2 * Sin(tymax) * Cos(angle);
                if (ymin > ymax)
                {
                    Swap(ref ymin, ref ymax);
                    Swap(ref tymin, ref tymax);
                }
                var tmpX = cx + r1 * Cos(tymin) * Cos(angle) - r2 * Sin(tymin) * Sin(angle);
                tymin = GetAngle(tmpX - cx, ymin - cy);
                tmpX = cx + r1 * Cos(tymax) * Cos(angle) - r2 * Sin(tymax) * Sin(angle);
                tymax = GetAngle(tmpX - cx, ymax - cy);
            }

            var angle1 = GetAngle(x1 - cx, y1 - cy);
            var angle2 = GetAngle(x2 - cx, y2 - cy);

            if (!sweep)
                Swap(ref angle1, ref angle2);

            var otherArc = false;
            if (angle1 > angle2)
            {
                Swap(ref angle1, ref angle2);
                otherArc = true;
            }

            if ((!otherArc && (angle1 > txmin || angle2 < txmin)) || (otherArc && !(angle1 > txmin || angle2 < txmin)))
                xmin = x1 < x2 ? x1 : x2;
            if ((!otherArc && (angle1 > txmax || angle2 < txmax)) || (otherArc && !(angle1 > txmax || angle2 < txmax)))
                xmax = x1 > x2 ? x1 : x2;
            if ((!otherArc && (angle1 > tymin || angle2 < tymin)) || (otherArc && !(angle1 > tymin || angle2 < tymin)))
                ymin = y1 < y2 ? y1 : y2;
            if ((!otherArc && (angle1 > tymax || angle2 < tymax)) || (otherArc && !(angle1 > tymax || angle2 < tymax)))
                ymax = y1 > y2 ? y1 : y2;

            return Rectangle2D.FromLTRB(xmin, ymin, xmax, ymax);
        }

        #endregion

        #region Boundary of Cubic Bezier

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Rectangle2D CubicBezierBounds(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            var sortOfCloseLength = (int)Measurements.CubicBezierArcLength(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y);
            var points = new List<Point2D>(Interpolators.Interpolate0to1((i) => Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, i), sortOfCloseLength));

            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            foreach (Point2D point in points)
            {
                // ToDo: Measure performance impact of overwriting each time.
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/24809978/calculating-the-bounding-box-of-cubic-bezier-curve
        /// http://floris.briolas.nl/floris/2009/10/bounding-box-of-cubic-bezier/
        /// http://jsfiddle.net/SalixAlba/QQnvm/4/
        /// </remarks>
        public static Rectangle2D CubicBezierBounds2(Point2D p0, Point2D p1, Point2D p2, Point2D p3)
        {
            var a = 3 * p3.X - 9 * p2.X + 9 * p1.X - 3 * p0.X;
            var b = 6 * p0.X - 12 * p1.X + 6 * p2.X;
            var c = 3 * p1.X - 3 * p0.X;

            var disc = b * b - 4 * a * c;
            var xl = p0.X;
            var xh = p0.X;
            if (p3.X < xl) xl = p3.X;
            if (p3.X > xh) xh = p3.X;
            if (disc >= 0)
            {
                var t1 = (-b + Sqrt(disc)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var x1 = Interpolators.Cubic(p0.X, p1.X, p2.X, p3.X, t1);
                    if (x1 < xl) xl = x1;
                    if (x1 > xh) xh = x1;
                }

                var t2 = (-b - Sqrt(disc)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var x2 = Interpolators.Cubic(p0.X, p1.X, p2.X, p3.X, t2);
                    if (x2 < xl) xl = x2;
                    if (x2 > xh) xh = x2;
                }
            }

            a = 3 * p3.Y - 9 * p2.Y + 9 * p1.Y - 3 * p0.Y;
            b = 6 * p0.Y - 12 * p1.Y + 6 * p2.Y;
            c = 3 * p1.Y - 3 * p0.Y;
            disc = b * b - 4 * a * c;
            var yl = p0.Y;
            var yh = p0.Y;
            if (p3.Y < yl) yl = p3.Y;
            if (p3.Y > yh) yh = p3.Y;
            if (disc >= 0)
            {
                var t1 = (-b + Sqrt(disc)) / (2 * a);

                if (t1 > 0 && t1 < 1)
                {
                    var y1 = Interpolators.Cubic(p0.Y, p1.Y, p2.Y, p3.Y, t1);
                    if (y1 < yl) yl = y1;
                    if (y1 > yh) yh = y1;
                }

                var t2 = (-b - Sqrt(disc)) / (2 * a);

                if (t2 > 0 && t2 < 1)
                {
                    var y2 = Interpolators.Cubic(p0.Y, p1.Y, p2.Y, p3.Y, t2);
                    if (y2 < yl) yl = y2;
                    if (y2 > yh) yh = y2;
                }
            }

            return new Rectangle2D(xl, xh, yl, yh);
        }
        //private static double evalBez(double p0, double p1, double p2, double p3, double t)
        //{
        //    return p0 * (1 - t) * (1 - t) * (1 - t) + 3 * p1 * t * (1 - t) * (1 - t) + 3 * p2 * t * t * (1 - t) + p3 * t * t * t;
        //}

        #endregion

        #region Boundary of Quadratic Bezier

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Rectangle2D QuadraticBezierBounds(Point2D a, Point2D b, Point2D c)
        {
            var sortOfCloseLength = Measurements.QuadraticBezierArcLengthByIntegral(a.X, a.Y, b.X, b.Y, c.X, c.Y);
            // ToDo: Need to make this more efficient. Don't need to rebuild the point array every time.
            var points = new List<Point2D>(Interpolators.Interpolate0to1((i) => Interpolators.QuadraticBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, i), (int)(sortOfCloseLength / 3)));

            var left = points[0].X;
            var top = points[0].Y;
            var right = points[0].X;
            var bottom = points[0].Y;

            foreach (Point2D point in points)
            {
                // ToDo: Measure performance impact of overwriting each time.
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        #endregion

        #region Calculate Rectangular boundaries of a circle defined by three points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CircleBoundsFromThreePointsTests))]
        public static List<SpeedTester> CircleBoundsFromThreePointsTests() => new List<SpeedTester> {
                new SpeedTester(() => TripointCircleBounds(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.TripointCircleBounds)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => CircleBoundsFromPoints(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CircleBoundsFromPoints)}(0, 0, 0, 1, 1, 1)")
           };

        /// <summary>
        /// Find the Bounds of A Circle from Three Points
        /// </summary>
        /// <param name="PointAX">First Point on the Ellipse</param>
        /// <param name="PointAY">First Point on the Ellipse</param>
        /// <param name="PointBX">Second Point on the Ellipse</param>
        /// <param name="PointBY">Second Point on the Ellipse</param>
        /// <param name="PointCX">Last Point on the Ellipse</param>
        /// <param name="PointCY">Last Point on the Ellipse</param>
        /// <returns>A Rectangle Representing the bounds of A Circle Defined from three
        /// Points</returns>
        public static Rectangle2D TripointCircleBounds(
            double PointAX, double PointAY,
            double PointBX, double PointBY,
            double PointCX, double PointCY)
        {
            (double X, double Y) Center = TripointCircleCenter(PointAX, PointAY, PointBX, PointBY, PointCX, PointCY);
            var Radius = Distance2D_0(Center.X, Center.Y, PointAX, PointAY);
            return Rectangle2D.FromLTRB((Center.X - Radius), (Center.Y - Radius), (Center.X + Radius), (Center.Y + Radius));
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
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4103405/what-is-the-algorithm-for-finding-the-center-of-a-circle-from-three-points
        /// </remarks>
        public static Rectangle2D CircleBoundsFromPoints(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y)
        {
            var offset = (p2X * p2X) + (p2Y * p2Y);
            var bc = ((p1X * p1X) + (p1Y * p1Y) - offset) * 0.5d;
            var cd = (offset - (p3X * p3X) - (p3Y * p3Y)) * 0.5d;
            var determinant = (p1X - p2X) * (p2Y - p3Y) - (p2X - p3X) * (p1Y - p2Y);

            if (Abs(determinant) < DoubleEpsilon)
                return null;

            var centerx = (bc * (p2Y - p3Y) - cd * (p1Y - p2Y)) / determinant;
            var centery = (cd * (p1X - p2X) - bc * (p2X - p3X)) / determinant;

            var radius = Sqrt(((p2X - centerx) * (p2X - centerx)) + ((p2Y - centery) * (p2Y - centery)));

            return Rectangle2D.FromLTRB((centerx - radius), (centery - radius), (centerx + radius), (centery + radius));
        }

        #endregion

        #region Catmull-Rom 1D Spline Interpolation

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CatmullRomSplineInterpolation1DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation1DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => CatmullRom(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CatmullRom)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="v1">The first position in the interpolation.</param>
        /// <param name="v2">The second position in the interpolation.</param>
        /// <param name="v3">The third position in the interpolation.</param>
        /// <param name="v4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <remarks>http://www.mvps.org/directx/articles/catmull/</remarks>
        public static double CatmullRom(
            double v1,
            double v2,
            double v3,
            double v4,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return (
                0.5d * (2d * v2
                + (v3 - v1) * t
                + (2d * v1 - 5d * v2 + 4d * v3 - v4) * tSquared
                + (3d * v2 - v1 - 3.0d * v3 + v4) * tCubed));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CatmullRomSpline(
            double v0,
            double v1,
            double v2,
            double v3,
            double t)
        {
            var mu2 = t * t;
            var a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
            var a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
            var a2 = -0.5 * v0 + 0.5 * v2;
            var a3 = v1;
            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        #endregion

        #region Catmull-Rom 2D Spline Interpolation

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CatmullRomSplineInterpolation2DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation2DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => InterpolateCatmullRom(0, 0, 0, 1, 1, 1, 1, 0, 0.5d),
                $"{nameof(Experiments.InterpolateCatmullRom)}(0, 0, 0, 1, 1, 1, 1, 0, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 0, 1, 1, 1, 1, 0, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 0, 1, 1, 1, 1, 0, 0.5d)")
           };

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <param name="t0X">First Point</param>
        /// <param name="t0Y">First Point</param>
        /// <param name="p1X">Second Point</param>
        /// <param name="p1Y">Second Point</param>
        /// <param name="p2X">Third Point</param>
        /// <param name="p2Y">Third Point</param>
        /// <param name="t3X">Fourth Point</param>
        /// <param name="t3Y">Fourth Point</param>
        /// <param name="t">
        /// Normalized distance between second and third point
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        /// <remarks>
        /// Points calculated exist on the spline between points two and three.
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </remarks>
        public static (double X, double Y) InterpolateCatmullRom(
            double t0X, double t0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double t3X, double t3Y,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return (
                0.5d * (2d * p1X
                + (-t0X + p2X) * t
                + (2d * t0X - 5d * p1X + 4d * p2X - t3X) * tSquared
                + (-t0X + 3d * p1X - 3d * p2X + t3X) * tCubed),
                0.5d * (2d * p1Y
                + (-t0Y + p2Y) * t
                + (2d * t0Y - 5d * p1Y + 4d * p2Y - t3Y) * tSquared
                + (-t0Y + 3d * p1Y - 3d * p2Y + t3Y) * tCubed));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CatmullRomSpline(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        #endregion

        #region Catmull-Rom 3D Spline Interpolation

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CatmullRomSplineInterpolation3DTests))]
        public static List<SpeedTester> CatmullRomSplineInterpolation3DTests()
            => new List<SpeedTester>
            {
                new SpeedTester(() => CatmullRom(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d),
                $"{nameof(Experiments.CatmullRom)}(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d)"),
                new SpeedTester(() => CatmullRomSpline(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d),
                $"{nameof(Experiments.CatmullRomSpline)}(0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0.5d)")
           };

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="x1">The first position in the interpolation.</param>
        /// <param name="x2">The second position in the interpolation.</param>
        /// <param name="x3">The third position in the interpolation.</param>
        /// <param name="x4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <remarks>http://www.mvps.org/directx/articles/catmull/</remarks>
        public static (double X, double Y, double Z) CatmullRom(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double x4, double y4, double z4,
            double t)
        {
            var tSquared = t * t;
            var tCubed = tSquared * t;
            return (
                0.5d * (2d * x2
                + (x3 - x1) * t
                + (2d * x1 - 5d * x2 + 4d * x3 - x4) * tSquared
                + (3d * x2 - x1 - 3d * x3 + x4) * tCubed),
                0.5d * (2d * x2
                + (y3 - y1) * t
                + (2d * y1 - 5d * y2 + 4d * y3 - y4) * tSquared
                + (3d * y2 - y1 - 3d * y3 + y4) * tCubed),
                0.5d * (2d * z2
                + (z3 - z1) * t
                + (2d * z1 - 5d * z2 + 4d * z3 - z4) * tSquared
                + (3d * z2 - z1 - 3d * z3 + z4) * tCubed));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CatmullRomSpline(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aZ0 = -0.5 * z0 + 1.5 * z1 - 1.5 * z2 + 0.5 * z3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aZ1 = z0 - 2.5 * z1 + 2 * z2 - 0.5 * z3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;
            var aZ2 = -0.5 * z0 + 0.5 * z2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Circle from Three points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CircleFromThreePointsTests))]
        public static List<SpeedTester> CircleFromThreePointsTests() => new List<SpeedTester> {
                new SpeedTester(() => TripointCircle(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.TripointCircle)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => CircleFromPoints(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CircleFromPoints)}(0, 0, 0, 1, 1, 1)")
           };

        /// <summary>
        /// Find the Bounds of A Circle from Three Points
        /// </summary>
        /// <param name="PointAX">First Point on the Ellipse</param>
        /// <param name="PointAY">First Point on the Ellipse</param>
        /// <param name="PointBX">Second Point on the Ellipse</param>
        /// <param name="PointBY">Second Point on the Ellipse</param>
        /// <param name="PointCX">Last Point on the Ellipse</param>
        /// <param name="PointCY">Last Point on the Ellipse</param>
        /// <returns>A Rectangle Representing the bounds of A Circle Defined from three
        /// Points</returns>
        public static Circle TripointCircle(
            double PointAX, double PointAY,
            double PointBX, double PointBY,
            double PointCX, double PointCY)
        {
            (double X, double Y) center = TripointCircleCenter(PointAX, PointAY, PointBX, PointBY, PointCX, PointCY);
            var radius = Distance2D_0(center.X, center.Y, PointAX, PointAY);
            return new Circle(new Point2D(center.X, center.Y), radius);
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
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4103405/what-is-the-algorithm-for-finding-the-center-of-a-circle-from-three-points
        /// </remarks>
        public static Circle CircleFromPoints(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y)
        {
            var offset = (p2X * p2X) + (p2Y * p2Y);
            var bc = ((p1X * p1X) + (p1Y * p1Y) - offset) * 0.5d;
            var cd = (offset - (p3X * p3X) - (p3Y * p3Y)) * 0.5d;
            var determinant = (p1X - p2X) * (p2Y - p3Y) - (p2X - p3X) * (p1Y - p2Y);

            if (Abs(determinant) < DoubleEpsilon)
                return null;

            var centerx = (bc * (p2Y - p3Y) - cd * (p1Y - p2Y)) / determinant;
            var centery = (cd * (p1X - p2X) - bc * (p2X - p3X)) / determinant;

            var radius = Sqrt(((p2X - centerx) * (p2X - centerx)) + ((p2Y - centery) * (p2Y - centery)));

            return new Circle(new Point2D(centerx, centery), radius);
        }

        #endregion

        #region Change the Angle of a Vector

        /// <summary>
        /// Set of tests to run testing methods that calculate the change of an angle of a vector.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ChangeAngleofVectorTests))]
        public static List<SpeedTester> ChangeAngleofVectorTests() => new List<SpeedTester> {
                new SpeedTester(() => SetAngle(0, 1, 1),
                $"{nameof(Experiments.SetAngle)}(0, 1, 1)")
           };

        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static (double X, double Y) SetAngle(double i, double j, double angle)
        {
            //double rads = angle; // * (PI / 180); // Original code used degrees rather than radians.
            var dist = Sqrt(i * i + j * j);
            return (
                Sin(angle) * dist,
                -(Cos(angle) * dist));
        }

        #endregion

        #region Complex Product of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the complex product of Two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ComplexProduct2DTests))]
        public static List<SpeedTester> ComplexProduct2DTests() => new List<SpeedTester> {
                new SpeedTester(() => ComplexProduct(0, 0, 1, 1),
                $"{nameof(Experiments.ComplexProduct)}(0, 0, 1, 1)")
           };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) ComplexProduct(
            double x0, double y0,
            double x1, double y1)
            => (x0 * x1 - y0 * y1, x0 * y1 + y0 * x1);

        #endregion

        #region Cosine Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolateTests1D))]
        public static List<SpeedTester> CosineInterpolateTests1D() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate1D(0, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate1D)}(0, 1, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static double CosineInterpolate1D(double v1, double v2, double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }

        #endregion

        #region Cosine Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolate2DTests))]
        public static List<SpeedTester> CosineInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate2D(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate2D)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y) CosineInterpolate2D(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2);
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public static Point2D Interpolate(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            var MU2 = (1.0 - Cos(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        /// <summary>
        /// Function For cosine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D CosineInterpolate(Point2D a, Point2D b, double Index)
        {
            var MU = ((1 - Cos((Index * 180))) / 2);
            return new Point2D(
                (a.X * (1 - MU)) + (b.X * MU),
                (a.Y * (1 - MU)) + (b.Y * MU)
                );
        }

        #endregion

        #region Cosine Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolate3DTests))]
        public static List<SpeedTester> CosineInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CosineInterpolate3D(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate3D)}(0, 0, 0, 1, 1, 1, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y, double Z) CosineInterpolate3D(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }

        #endregion

        #region Clamp a value between a minimum and a maximum

        /// <summary>
        /// Set of tests to run testing methods that clamp a number between a minimum, and a maximum.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ClampTests))]
        public static List<SpeedTester> ClampTests() => new List<SpeedTester> {
                new SpeedTester(() => Clamp0(2, 0, 1),
                $"{nameof(Experiments.Clamp0)}(2, 0, 1)"),
                new SpeedTester(() => Clamp1(2, 0, 1),
                $"{nameof(Experiments.Clamp1)}(2, 0, 1)")
            };

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp0(double value, double min, double max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp1(double value, double min, double max)
            => Max(min, Min(value, max));

        #endregion

        #region Closest Point on line

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(NearestPointOnLineSegmentTests))]
        public static List<SpeedTester> NearestPointOnLineSegmentTests() => new List<SpeedTester> {
                new SpeedTester(() => ClosestPointOnLineSegmentMvG(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineSegmentMvG)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => ClosestPointOnLineSegmentDarienPardinas(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineSegmentDarienPardinas)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => ClosestPointOnLineDarienPardinas(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.ClosestPointOnLineDarienPardinas)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineSegmentMvG(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->B
            var diffAB = new Point2D(aX - bX, aY - bY);
            var det = aY * bX - aX * bY;
            var dot = diffAB.X * pX + diffAB.Y * pY;
            var val = new Point2D(dot * diffAB.X + det * diffAB.Y, dot * diffAB.Y - det * diffAB.X);
            var magnitude = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;
            var inverseDist = 1 / magnitude;
            return new Point2D(val.X * inverseDist, val.Y * inverseDist);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineSegmentDarienPardinas(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->P
            var diffAP = new Point2D(pX - aX, pY - aY);
            // Vector A->B
            var diffAB = new Point2D(bX - aX, bY - aY);
            var dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;
            // The dot product of diffAP and diffAB
            var dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;
            //  # The normalized "distance" from a to the closest point
            var dist = dotABAP / dotAB;
            if (dist < 0)
                return new Point2D(aX, aY);
            else if (dist > dotABAP)
                return new Point2D(bX, bY);
            else
                return new Point2D(aX + diffAB.X * dist, aY + diffAB.Y * dist);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/3120357/get-closest-point-to-a-line</remarks>
        private static Point2D ClosestPointOnLineDarienPardinas(
            double aX, double aY,
            double bX, double bY,
            double pX, double pY)
        {
            // Vector A->P
            var diffAP = new Point2D(pX - aX, pY - aY);
            // Vector A->B
            var diffAB = new Point2D(bX - aX, bY - aY);
            var dotAB = diffAB.X * diffAB.X + diffAB.Y * diffAB.Y;
            // The dot product of diffAP and diffAB
            var dotABAP = diffAP.X * diffAB.X + diffAP.Y * diffAB.Y;
            // The normalized "distance" from a to the closest point
            var dist = dotABAP / dotAB;
            return new Point2D(aX + diffAB.X * dist, aY + diffAB.Y * dist);
        }

        #endregion

        #region Cross Product of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProduct2Points2DTests))]
        public static List<SpeedTester> CrossProduct2Points2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CrossProduct2Points2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.CrossProduct2Points2D_0)}(0, 0, 1, 0)")
            };

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct2Points2D_0(
            double x1, double y1,
            double x2, double y2)
            => (x1 * y2) - (y1 * x2);

        #endregion

        #region Cross Product of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProduct2Points3DTests))]
        public static List<SpeedTester> CrossProduct2Points3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CrossProduct2Points3D_0(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CrossProduct2Points3D_0)}(0, 0, 0, 1, 1, 1)")
            };

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CrossProduct2Points3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                    (y1 * z2) - (z1 * y2), // X
                    (z1 * x2) - (x1 * z2), // Y
                    (x1 * y2) - (y1 * x2)  // Z
                );

        #endregion

        #region Cross Product of the Vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProductVector2DTests))]
        public static List<SpeedTester> CrossProductVector2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CrossProductVector2D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => CrossProductVector2D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => CrossProductVector2D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_2)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector2D_0(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
            => ((aX - bX) * (cY - bY) - (aY - bY) * (cX - bX));

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static double CrossProductVector2D_1(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
        {
            // Get the vectors' coordinates.
            var BAx = aX - bX;
            var BAy = aY - bY;
            var BCx = cX - bX;
            var BCy = cY - bY;

            // Calculate the Z coordinate of the cross product.
            return ((BAx) * (BCy) - (BAy) * (BCx));
        }

        /// <summary>
        /// Return the cross product AB x BC.
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
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static double CrossProductVector2D_2(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            var BAx = Ax - Bx;
            var BAy = Ay - By;
            var BCx = Cx - Bx;
            var BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
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
        public static List<double> FindTforCoordinate(Point2D value, List<Point2D> Lut)
        {
            var point = new Point2D();
            var found = new List<double>();
            var len = Lut.Count;
            for (var i = 0; i < len; i++)
            {
                point.X = Lut[i].X;
                point.Y = Lut[i].Y;
                if (Abs(value.X - point.X) < DoubleEpsilon && Abs(value.Y - point.Y) < DoubleEpsilon)
                    found.Add(i / len);
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
        public static List<Point2D> BuildLUT(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            var Lut = new List<Point2D>(100);
            for (double t = 0; t <= 1; t += 0.01)
                Lut[(int)(t * 100)] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t));
            return Lut;
        }

        #endregion

        #region Cubic Bezier Length approximations

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
            var k1 = (Point2D)(-p1 + 3 * (p2 - p3) + p4);
            Point2D k2 = 3 * (p1 + p3) - 6 * p2;
            var k3 = (Point2D)(3 * (p2 - p1));
            Point2D k4 = p1;

            var q1 = 9.0 * (Sqrt(Abs(k1.X)) + Sqrt((Abs(k1.Y))));
            var q2 = 12.0 * (k1.X * k2.X + k1.Y * k2.Y);
            var q3 = 3.0 * (k1.X * k3.X + k1.Y * k3.Y) + 4.0 * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            var q4 = 4.0 * (k2.X * k3.X + k2.Y * k3.Y);
            var q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson.
            double a = 0;
            double b = 1;
            var n_limit = 1024;
            var TOLERANCE = 0.001;

            var n = 1;

            var multiplier = (b - a) / 6.0;
            var endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            var interval = (b - a) / 2.0;
            double asum = 0;
            var bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            var est1 = multiplier * (endsum + 2 * asum + 4 * bsum);
            var est0 = 2 * est1;

            while (n < n_limit && (Abs(est1) > 0 && Abs((est1 - est0) / est1) > TOLERANCE))
            {
                n *= 2;
                multiplier /= 2;
                interval /= 2;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                var interval_div_2n = interval / (2.0 * n);

                for (var i = 1; i < 2 * n; i += 2)
                {
                    var t = a + i * interval_div_2n;
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
            var result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
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
            var previous_dot = new Point2D();
            var length = 0.0;
            for (var i = 0; i <= steps; i++)
            {
                t = (double)i / steps;
                dot = new Point2D(Interpolators.CubicBezier(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y, t));
                if (i > 0)
                {
                    var x_diff = dot.X - previous_dot.X;
                    var y_diff = dot.Y - previous_dot.Y;
                    length += Sqrt(x_diff * x_diff + y_diff * y_diff);
                }
                previous_dot = dot;
            }
            return length;
        }

        #endregion

        #region Cubic Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate1DTests))]
        public static List<SpeedTester> CubicInterpolate1DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolate1D(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolate1D)}(0, 1, 2, 3, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static double CubicInterpolate1D(double v0, double v1, double v2, double v3, double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = v3 - v2 - v0 + v1;
            a1 = v0 - v1 - a0;
            a2 = v2 - v0;
            a3 = v1;

            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        #endregion

        #region Cubic Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate2DTests))]
        public static List<SpeedTester> CubicInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolate2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicInterpolate2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y) CubicInterpolate2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = x3 - x2 - x0 + x1;
            var aY0 = y3 - y2 - y0 + y1;
            var aX1 = x0 - x1 - aX0;
            var aY1 = y0 - y1 - aY0;
            var aX2 = x2 - x0;
            var aY2 = y2 - y0;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        #endregion

        #region Cubic Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate3DTests))]
        public static List<SpeedTester> CubicInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolate3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d),
                $"{nameof(Experiments.CubicInterpolate3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y, double Z) CubicInterpolate3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = x3 - x2 - x0 + x1;
            var aY0 = y3 - y2 - y0 + y1;
            var aZ0 = z3 - z2 - z0 + z1;
            var aX1 = x0 - x1 - aX0;
            var aY1 = y0 - y1 - aY0;
            var aZ1 = z0 - z1 - aZ0;
            var aX2 = x2 - x0;
            var aY2 = y2 - y0;
            var aZ2 = z2 - z0;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines1DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines1DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines1D(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines1D)}(0, 1, 2, 3, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static double CubicInterpolateCatmullRomSplines1D(double v0, double v1, double v2, double v3, double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
            a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
            a2 = -0.5 * v0 + 0.5 * v2;
            a3 = v1;

            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines2DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y) CubicInterpolateCatmullRomSplines2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines3DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines3DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y, double Z) CubicInterpolateCatmullRomSplines3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            var mu2 = t * t;

            var aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            var aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            var aZ0 = -0.5 * z0 + 1.5 * z1 - 1.5 * z2 + 0.5 * z3;
            var aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            var aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            var aZ1 = z0 - 2.5 * z1 + 2 * z2 - 0.5 * z3;
            var aX2 = -0.5 * x0 + 0.5 * x2;
            var aY2 = -0.5 * y0 + 0.5 * y2;
            var aZ2 = -0.5 * z0 + 0.5 * z2;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Cubic Bezier Derivative


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cs.mtu.edu/~shene/COURSES/cs3621/NOTES/spline/Bezier/bezier-der.html</remarks>
        private PointF CubicBezierDerivative0(PointF p0, PointF p1, PointF p2, PointF p3, double t) => new PointF((float)(3 * Pow(1 - t, 2) * (p1.X - p0.X) + 6 * (1 - t) * t * (p2.X - p1.X) + 3 * Pow(t, 2) * (p3.X - p2.X)),
                              (float)(3 * Pow(1 - t, 2) * (p1.Y - p0.Y) + 6 * (1 - t) * t * (p2.Y - p1.Y) + 3 * Pow(t, 2) * (p3.Y - p2.Y)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cs.mtu.edu/~shene/COURSES/cs3621/NOTES/spline/Bezier/bezier-der.html</remarks>
        private PointF CubicBezierDerivative1(PointF p0, PointF p1, PointF p2, PointF p3, double t)
        {
            var mu1 = 1 - t;
            var mu12 = mu1 * mu1;
            var mu2 = t * t;

            return new PointF(
                (float)(3 * mu12 * (p1.X - p0.X) + 6 * mu1 * t * (p2.X - p1.X) + 3 * mu2 * (p3.X - p2.X)),
                (float)(3 * mu12 * (p1.Y - p0.Y) + 6 * mu1 * t * (p2.Y - p1.Y) + 3 * mu2 * (p3.Y - p2.Y))
                );
        }

        #endregion

        #region Cubic Bezier Interpolation of 1D Points

        #endregion

        #region Cubic Bezier Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicBezierInterpolate2DTests))]
        public static List<SpeedTester> CubicBezierInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => CubicBezierInterpolate2D_0(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_0)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_1(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_1)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_3(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_3)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_4(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_4)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_5(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_5)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_6(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_6)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)")
            };

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        public static (double X, double Y) CubicBezierInterpolate2D_0(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            var mum1 = 1 - t;
            var mum13 = mum1 * mum1 * mum1;
            var mu3 = t * t * t;

            return (
                (mum13 * x0 + 3 * t * mum1 * mum1 * x1 + 3 * t * t * mum1 * x2 + mu3 * x3),
                (mum13 * y0 + 3 * t * mum1 * mum1 * y1 + 3 * t * t * mum1 * y2 + mu3 * y3)
                );
        }

        /// <summary>
        /// Calculate parametric value of x or y given t and the four point
        /// coordinates of a cubic bezier curve. This is a separate function
        /// because we need it for both x and y values.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.lemoda.net/maths/bezier-length/index.html</remarks>
        // Formula from Wikipedia article on Bezier curves.
        public static (double X, double Y) CubicBezierInterpolate2D_1(
        double aX, double aY,
        double bX, double bY,
        double cX, double cY,
        double dX, double dY,
        double t)
            => (
            aX * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * bX * (1.0 - t) * (1.0 - t) * t + 3.0 * cX * (1.0 - t) * t * t + dX * t * t * t,
            aY * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * bY * (1.0 - t) * (1.0 - t) * t + 3.0 * cY * (1.0 - t) * t * t + dY * t * t * t);

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        public static (double X, double Y) CubicBezierInterpolate2D_2(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            // point between a and b
            (double X, double Y) ab = LinearInterpolate2D_0(x0, y0, x1, y1, t);
            // point between b and c
            (double X, double Y) bc = LinearInterpolate2D_0(x1, y1, x2, y2, t);
            // point between c and d
            (double X, double Y) cd = LinearInterpolate2D_0(x2, y2, x3, y3, t);
            // point between ab and bc
            (double X, double Y) abbc = LinearInterpolate2D_0(ab.X, ab.Y, bc.X, bc.Y, t);
            // point between bc and cd
            (double X, double Y) bccd = LinearInterpolate2D_0(bc.X, bc.Y, cd.X, cd.Y, t);
            // point on the bezier-curve
            return LinearInterpolate2D_0(abbc.X, abbc.Y, bccd.X, bccd.Y, t);
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="aX">the starting point, or A in the above diagram</param>
        /// <param name="aY">the starting point, or A in the above diagram</param>
        /// <param name="bX">the first control point, or B</param>
        /// <param name="bY">the first control point, or B</param>
        /// <param name="cX">the second control point, or C</param>
        /// <param name="cY">the second control point, or C</param>
        /// <param name="dX">the end point, or D</param>
        /// <param name="dY">the end point, or D</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static (double X, double Y) CubicBezierInterpolate2D_3(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            var V1 = t;
            var V2 = (1 - t);
            return (
                (aX * V2 * V2 * V2) + (3 * bX * V1 * V2 * V2) + (3 * cX * V1 * V1 * V2) + (dX * V2 * V2 * V2),
                ((aY * V2 * V2 * V2) + (3 * bY * V1 * V2 * V2) + (3 * cY * V1 * V1 * V2) + (dY * V2 * V2 * V2)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static (double X, double Y) CubicBezierInterpolate2D_4(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            //(double X, double Y) P = (v3 - v2) - (v0 - v1);
            //(double X, double Y) Q = (v0 - v1) - P;
            //(double X, double Y) R = v2 - v0;
            //(double X, double Y) S = v1;
            //(double X, double Y) P * Pow(x, 3) + Q * Pow(x, 2) + R * x + S;
            var PX = (dX - cX) - (aX - bX);
            var PY = (dY - cY) - (aY - bY);
            var QX = (aX - bX) - PX;
            var QY = (aY - bY) - PY;
            var RX = cX - aX;
            var RY = cY - aY;
            var SX = bX;
            var SY = bY;
            return (
                PX * (t * t * t) + QX * (t * t) + RX * t + SX,
                PY * (t * t * t) + QY * (t * t) + RY * t + SY);
        }

        /// <summary>
        ///  Code to generate a cubic Bezier curve
        /// </summary>
        /// <param name="aX">the starting point, or A in the above diagram</param>
        /// <param name="aY">the starting point, or A in the above diagram</param>
        /// <param name="bX">the first control point, or B</param>
        /// <param name="bY">the first control point, or B</param>
        /// <param name="cX">the second control point, or C</param>
        /// <param name="cY">the second control point, or C</param>
        /// <param name="dX">the end point, or D</param>
        /// <param name="dY">the end point, or D</param>
        /// <param name="t">
        ///  t is the parameter value, 0 less than or equal to t less than or equal to 1
        /// </param>
        /// <returns></returns>
        /// <remarks>
        ///  Warning - untested code
        /// </remarks>
        public static (double X, double Y) CubicBezierInterpolate2D_5(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            // calculate the curve point at parameter value t
            var tSquared = (t * t);
            var tCubed = (tSquared * t);

            // calculate the polynomial coefficients
            var cC = ((3 * (bX - aX)), (3 * (bY - aY)));
            var cB = (((3 * (cX - bX)) - cC.Item1), ((3 * (cY - bY)) - cC.Item2));
            var cA = ((dX - (aX - (cC.Item1 - cB.Item1))), (dY - (aY - (cC.Item2 - cB.Item2))));
            return (((cA.Item1 * tCubed) + ((cB.Item1 * tSquared) + ((cC.Item1 * t) + aX))), ((cA.Item2 * tCubed) + ((cB.Item2 * tSquared) + ((cC.Item2 * t) + aY))));
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static (double X, double Y) CubicBezierInterpolate2D_6(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            var c1 = (((dX - cX) - (aX - bX)), ((dY - cY) - (aY - bY)));
            var c2 = (((aX - bX) - aX), ((aY - bY) - aY));
            var c3 = ((cX - aX), (cY - aY));
            var c4 = (aX, aY);
            return (
                (c1.Item1 * t * t * t + c2.Item1 * t * t * t + c3.Item1 * t + c4.Item1),
                (c1.Item2 * t * t * t + c2.Item2 * t * t * t + c3.Item2 * t + c4.Item2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://en.wikipedia.org/wiki/B%C3%A9zier_curve</remarks>
        private PointF CubicBezierCurve(PointF p0, PointF p1, PointF p2, PointF p3, double t)
            => new PointF((float)(Pow(1 - t, 3) * p0.X + 3 * Pow(1 - t, 2) * t * p1.X
                        + 3 * (1 - t) * Pow(t, 2) * p2.X + Pow(t, 3) * p3.X),
                (float)(Pow(1 - t, 3) * p0.Y + 3 * Pow(1 - t, 2) * t * p1.Y
                        + 3 * (1 - t) * Pow(t, 2) * p2.Y + Pow(t, 3) * p3.Y));

        #endregion

        #region Cubic Bezier Interpolation of 3D Points

        #endregion

        #region Cubic Bezier and Line Intersections

        #endregion

        #region Cubic BSpline Interpolation

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D InterpolateBSpline(List<Point2D> points, double index)
        {
            if (points.Count >= 4)
            {
                var VPoints = new List<Point2D>(4);

                var A = 0;
                var B = 1;
                var C = 2;
                var D = 3;

                VPoints.Add(new Point2D(
                    ((points[D].X - points[C].X) - (points[A].X - points[B].X)),
                    ((points[D].Y - points[C].Y) - (points[A].Y - points[B].Y))
                    ));

                VPoints.Add(new Point2D(
                    ((points[A].X - points[B].X) - VPoints[A].X),
                    ((points[A].Y - points[B].Y) - VPoints[A].Y)
                    ));

                VPoints.Add(new Point2D(
                    (points[C].X - points[A].X),
                    (points[C].Y - points[A].Y)
                    ));

                VPoints.Add(points[1]);

                return new Point2D(
                    VPoints[0].X * index * index * index + VPoints[1].X * index * index * index + VPoints[2].X * index + VPoints[3].X,
                    VPoints[0].Y * index * index * index + VPoints[1].Y * index * index * index + VPoints[2].Y * index + VPoints[3].Y
                );
            }

            return Point2D.Empty;
        }

        /// <summary>
        /// General Bezier curve Number of control points is n+1 0 less than or equal to mu less than 1
        /// IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point2D Interpolate(List<Point2D> points, double index)
        {
            var n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            var munk = Pow(1 - index, n);

            var b = new Point2D(0.0f, 0.0f);

            for (var k = 0; k <= n; k++)
            {
                nn = n;
                kn = k;
                nkn = n - k;
                blend = muk * munk;
                muk *= index;
                munk /= (1 - index);
                while (nn >= 1)
                {
                    blend *= nn;
                    nn--;
                    if (kn > 1)
                    {
                        blend /= kn;
                        kn--;
                    }
                    if (nkn > 1)
                    {
                        blend /= nkn;
                        nkn--;
                    }
                }

                b = new Point2D(
                b.X + points[k].X * blend,
                b.Y + points[k].Y * blend
                    );
            }

            return (b);
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Point2D InterpolateCubicBSplinePoint(Point2D[] Points, double Index)
        {
            var A = 0;
            var B = 1;
            var C = 2;
            var D = 3;
            var V1 = Index;
            var VPoints = new Point2D[4];

            VPoints[0] = new Point2D(
                ((Points[D].X - Points[C].X) - (Points[A].X - Points[B].X)),
                ((Points[D].Y - Points[C].Y) - (Points[A].Y - Points[B].Y))
                );

            VPoints[1] = new Point2D(
                ((Points[A].X - Points[B].X) - VPoints[A].X),
                ((Points[A].Y - Points[B].Y) - VPoints[A].Y)
                );

            VPoints[2] = new Point2D(
                (Points[C].X - Points[A].X),
                (Points[C].Y - Points[A].Y)
                );

            VPoints[3] = Points[1];

            return new Point2D(
                VPoints[0].X * V1 * V1 * V1 + VPoints[1].X * V1 * V1 * V1 + VPoints[2].X * V1 + VPoints[3].X,
                VPoints[0].Y * V1 * V1 * V1 + VPoints[1].Y * V1 * V1 * V1 + VPoints[2].Y * V1 + VPoints[3].Y
            );
        }

        #endregion

        #region Distance Between Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Distance3DTests))]
        public static List<SpeedTester> Distance3DTests() => new List<SpeedTester> {
                new SpeedTester(() => Distance3D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_2)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1)
                + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance3D_1(
        double x1, double y1, double z1,
        double x2, double y2, double z2) => Sqrt((x2 - x1) * (x2 - x1)
        + (y2 - y1) * (y2 - y1)
        + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        public static double Distance3D_2(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            var x = (x2 - x1);
            var y = (y2 - y1);
            var z = (z2 - z1);
            return Sqrt(x * x + y * y + z * z);
        }

        #endregion

        #region Distance Between Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Distance2DTests))]
        public static List<SpeedTester> Distance2DTests() => new List<SpeedTester> {
                new SpeedTester(() => Distance2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_0)}(0, 0, 1, 0)"),
                new SpeedTester(() => Distance2D_1((0, 0), (1, 0)),
                $"{nameof(Experiments.Distance2D_1)}((0, 0), (1, 0))"),
                new SpeedTester(() => Distance2D_2(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_2)}(0, 0, 1, 0)"),
                new SpeedTester(() => Distance2D_3(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_3)}(0, 0, 1, 0)")
            };

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_0(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="a">First component.</param>
        /// <param name="b">Second component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_1(
            (double X, double Y) a,
            (double X, double Y) b)
            => Sqrt((b.X - a.X) * (b.X - a.X)
                + (b.Y - a.Y) * (b.X - a.Y));

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_2(
            double x1, double y1,
            double x2, double y2)
                => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_3(
            double x1, double y1,
            double x2, double y2)
        {
            var x = (x2 - x1);
            var y = (y2 - y1);
            return Sqrt(x * x + y * y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Point2D point1, Point2D point2)
        {
            var dx = point1.X - point2.X;
            var dy = point1.Y - point2.Y;
            return Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector2D vector1, Vector2D vector2)
        {
            var dx = vector1.I - vector2.I;
            var dy = vector1.J - vector2.J;
            return Sqrt(dx * dx + dy * dy);
        }

        #endregion

        #region Distance of Point to Line Segment

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
            var Delta = new Point2D((B.X - A.X), (B.Y - A.Y));
            if ((Abs(Delta.X) < DoubleEpsilon) && (Abs(Delta.Y) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                Delta.X = (p.X - A.X);
                Delta.Y = (p.Y - A.Y);
                RetNear.X = A.X;
                RetNear.Y = A.Y;
                return (Sqrt(((Delta.X * Delta.X) + (Delta.Y * Delta.Y))));
            }
            //  Calculate the t that minimizes the distance.
            var t = ((((p.X - A.X) * Delta.X) + ((p.Y - A.Y) * Delta.Y)) / ((Delta.X * Delta.X) + (Delta.Y * Delta.Y)));
            if (t < 0)
            {
                Delta.X = (p.X - A.X);
                Delta.Y = (p.Y - A.Y);
                RetNear.X = A.X;
                RetNear.Y = A.Y;
            }
            else if (t > 1)
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
            if ((Abs(dx) < DoubleEpsilon) && (Abs(dy) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                dx = (px - x1);
                dy = (py - y1);
                return Sqrt(((dx * dx) + (dy * dy)));
            }
            t = ((px + (py - (x1 - y1))) / (dx + dy));
            if (t < 0)
            {
                dx = (px - x1);
                dy = (py - y1);
            }
            else if (t > 1)
            {
                dx = (px - x2);
                dy = (py - y2);
            }
            else
            {
                var x3 = (x1 + (t * dx));
                var y3 = (y1 + (t * dy));
                dx = (px - x3);
                dy = (py - y3);
            }
            return Sqrt(((dx * dx) + (dy * dy)));
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
            var dx = (x2 - x1);
            var dy = (y2 - y1);
            if ((Abs(dx) < DoubleEpsilon) && (Abs(dy) < DoubleEpsilon))
            {
                //  It's a point not a line segment.
                dx = (px - x1);
                dy = (py - y1);
                return Sqrt(((dx * dx) + (dy * dy)));
            }
            var t = ((px + (py - (x1 - y1))) / (dx + dy));
            if (t < 0)
            {
                dx = (px - x1);
                dy = (py - y1);
            }
            else if (t > 1)
            {
                dx = (px - x2);
                dy = (py - y2);
            }
            else
            {
                var x3 = (x1 + (t * dx));
                var y3 = (y1 + (t * dy));
                dx = (px - x3);
                dy = (py - y3);
            }
            return Sqrt(((dx * dx) + (dy * dy)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q0"></param>
        /// <param name="q1"></param>
        /// <param name="radius"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        /// <remarks>
        /// From: http://stackoverflow.com/questions/2255842/detecting-coincident-subset-of-two-coincident-line-segments/2255848
        /// </remarks>
        private static double DistFromSeg(Point2D p, Point2D q0, Point2D q1, double radius, ref double u)
        {
            // formula here:
            // http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
            // where x0,y0 = p
            //       x1,y1 = q0
            //       x2,y2 = q1
            var dx21 = q1.X - q0.X;
            var dy21 = q1.Y - q0.Y;
            var dx10 = q0.X - p.X;
            var dy10 = q0.Y - p.Y;
            var segLength = Math.Sqrt(dx21 * dx21 + dy21 * dy21);
            if (segLength < Epsilon)
                throw new Exception("Expected line segment, not point.");
            var num = Math.Abs(dx21 * dy10 - dx10 * dy21);
            var d = num / segLength;
            return d;
        }

        #endregion

        #region Dot Product of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProduct2Points2DTests))]
        public static List<SpeedTester> DotProduct2Points2DTests() => new List<SpeedTester> {
                new SpeedTester(() => DotProduct2Points2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.DotProduct2Points2D_0)}(0, 0, 1, 0)"),
                new SpeedTester(() => DotProduct2Points2D_1(0, 0, 1, 0),
                $"{nameof(Experiments.DotProduct2Points2D_1)}(0, 0, 1, 0)")
            };

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct2Points2D_0(
            double x1, double y1,
            double x2, double y2)
            => ((x1 * x2) + (y1 * y2));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct2Points2D_1(
        double x1, double y1,
        double x2, double y2) => ((x1 * x2) + (y1 * y2));

        #endregion

        #region Dot Product of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product for two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProduct3D_0Tests))]
        public static List<SpeedTester> DotProduct3D_0Tests() => new List<SpeedTester> {
                new SpeedTester(() => DotProduct(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.DotProduct)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => DotProduct((0, 0, 0), 1, 1, 1),
                $"{nameof(Experiments.DotProduct)}((0, 0, 0), 1, 1, 1)"),
                new SpeedTester(() => DotProduct((0, 0, 0), (1, 1, 1)),
                $"{nameof(Experiments.DotProduct)}((0, 0, 0), (1, 1, 1))")
            };

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => ((x1 * x2) + (y1 * y2) + (z1 * z2));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple,
            double x2, double y2, double z2)
            => DotProduct(tuple.X, tuple.Y, tuple.Z, x2, y2, z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>The Dot Product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple1,
            (double X, double Y, double Z) tuple2)
            => DotProduct(
                tuple1.X, tuple1.Y, tuple1.Z,
                tuple2.X, tuple2.Y, tuple2.Z
                );

        #endregion

        #region Dot Product of the Vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProductVector2DTests))]
        public static List<SpeedTester> DotProductVector2DTests() => new List<SpeedTester> {
                new SpeedTester(() => DotProductVector2D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProductVector2D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProductVector2D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_2)}(0, 0, 1, 0, 1, 1)")
            };

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductVector2D_0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => ((x1 - x2) * (x3 - x2)
            + (y1 - y2) * (y3 - y2));

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductVector2D_1(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3) => ((x1 - x2) * (x3 - x2)
        + (y1 - y2) * (y3 - y2));

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        public static double DotProductVector2D_2(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Get the vectors' coordinates.
            var BAx = x1 - x2;
            var BAy = y1 - y2;
            var BCx = x3 - x2;
            var BCy = y3 - y2;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        #endregion

        #region Ellipse Perimeter Length

        /// <summary>
        /// This approximation is within about 5% of the true value, so long as a is not more than 3 times longer than b (in other words, the ellipse is not too "squashed"):
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeter1(double a, double b) => 2 * PI * (Sqrt(0.5 * ((b * b) + (a * a))));

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://ellipse-circumference.blogspot.com/
        /// </remarks>
        private static double EllipsePerimeter2(double a, double b)
        {
            var h = (((b - a) * (b - a)) / ((b + a) * (b + a)));
            var H2 = 4 - 3 * h;
            var d = ((11 * PI / (44 - 14 * PI)) + 24100) - 24100 * h;
            return PI * (b + a) * (1 + (3 * h) / (10 + Pow(H2, 0.5)) + (1.5 * Pow(h, 6) - .5 * Pow(h, 12)) / d);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterKepler(double a, double b) => 2 * PI * (Sqrt(a * b));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterSipos(double a, double b) => 2 * PI * (((a + b) * (a + b)) / ((Sqrt(a) + Sqrt(a)) * (Sqrt(b) + Sqrt(b))));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterNaive(double a, double b) => PI * (a + b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterPeano(double a, double b) => PI * ((3 * (a + b) / 2) - Sqrt(a * b));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterEuler(double a, double b) => 2 * PI * Sqrt(((a * a) + (b * b)) / 2);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterAlmkvist(double a, double b) => 2 * PI
        * ((2 * Pow(a + b, 2) - Pow(Sqrt(a) - Sqrt(b), 4))
        / (Pow(Sqrt(a) - Sqrt(b), 2) + (2 * Sqrt(2 * (a + b)) * Pow(a * b, (1 / 4)))));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterQuadratic(double a, double b) => (PI / 2) * Sqrt((6) * (a * a + b * b) + (4 * a * b));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterMuir(double a, double b) => 2 * PI * Pow((Pow(a, 3 / 2) + Pow(b, 3 / 2)) / 2, 2 / 3);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.ebyte.it/library/docs/math05a/EllipsePerimeterApprox05.html</remarks>
        private static double EllipsePerimeterLindner(double a, double b)
        {
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * Sqrt(1 + (h / 8));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        private static double EllipsePerimeterSykoraRiveraCantrellsParticularlyFruitful(double a, double b) => 4 * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterYNOT(double a, double b)
        {
            var s = Log(2, E) / Log(PI / 2, E);
            return 4 * Pow(Pow(a, s) + Pow(b, s), 1 / s);
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
            var d1 = (PI / 4) * (19 / 15) - 1;
            var d2 = (PI / 4) * (80 / 63) - 1;
            var p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((64 + 16 * h)
                / (64 - h * h))
                + (1 - p) * ((16 + 3 * h) / (16 - h)));
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
            var d1 = (PI / 4) * (81 / 64) - 1;
            var d2 = (PI / 4) * (19 / 15) - 1;
            var p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((16 - 3 * h)
                / (16 - h))
                + (1 - p) * Pow(1 + (h) / 8, 2));
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
            var d1 = (PI / 4) * (61 / 48) - 1;
            var d2 = (PI / 4) * (187 / 147) - 1;
            var p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((256 - 48 * h - 21 * h * h)
                / (256 - 112 * h + 3 * h * h))
                + (1 - p) * ((64 - 3 * h * h) / (64 - 16 * h)));
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
            var d1 = (PI / 4) * (61 / 48) - 1;
            var d2 = (PI / 4) * (187 / 147) - 1;
            var p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h))
                + (1 - p) * ((256 - 48 * h - 21 * h * h) / (256 - 112 * h + 3 * h * h)));
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
            var d1 = (PI / 4) * (61 / 48) - 1;
            var d2 = (PI / 4) * (187 / 147) - 1;
            var p = d1 / (d1 - d2);
            double h = 1;
            return PI * (a + b) * (p * ((135168 - 85760 * h - 5568 * h * h + 3867 * h * h * h)
                / (135168 - 119552 * h + 22208 * h * h - 345 * h * h * h))
                + (1 - p) * ((3072 - 1280 * h - 252 * h * h + 33 * h * h * h)
                / (3072 - 2048 * h + 212 * h * h)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan(double a, double b) => PI * (3 * (a + b) - Sqrt((3 * a + b) * (a + 3 * b)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSelmer(double a, double b) => (PI / 4) * ((6 + .5 * (Pow(a - b, 2) * Pow(a - b, 2) / Pow(a + b, 2) * Pow(a + b, 2))) * (a + b) - Sqrt(2 * (a * a + 3 * a * b + b * b)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRamanujan2(double a, double b)
        {
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * (1 + ((3 * h) / (10 + Sqrt(4 - 3 * h))));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((16 + (3 * h)) / (16 - h));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (16 * h)) / (64 - (h * h)));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((64 + (3 * h * h)) / (64 - (16 * h)));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((256 - (48 * h) - (21 * h * h)) / (256 - (112 * h) + 3 * h * h));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * ((3072 - (1280 * h) - (252 * h * h) + (33 * h * h * h)) / (3072 - (2048 * h) + 212 * h * h));
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
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b)
                * ((135168 - (85760 * h) - (5568 * h * h) + (3867 * h * h * h))
                / (135168 - (119552 * h) + (22208 * h * h) - (345 * h * h * h)));
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
            var p = 1.32;
            return 2 * PI * (p * ((a + b) / 2) + (1 - p) * Sqrt(a * b));
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
            var w = 0.7966106;
            return 2 * PI * Sqrt(w * ((a * a + b * b) / 2) + (1 - w) * a * b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedQuadratic2(double a, double b) => PI * Sqrt(2 * (a * a + b * b) + (a - b) * (a - b) / 2.458338);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterOptimizedRamanujan1(double a, double b)
        {
            var p = 3.0273;
            double w = 3;
            return 2 * PI * (p * ((a + b) / 2) + (1 - p) * Sqrt((a + w * b) * (w * a + b)) / (1 + w));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeuMichon(double a, double b)
            => Abs(a - b) < DoubleEpsilon ? 2 * PI * a : PI * ((a - b) / Atan((a - b) / (a + b)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell2(double a, double b)
        {
            var p = 0.410117;
            double w = 74;
            return 4 * (a + b) - ((8 - 2 * PI) * a * b)
                / (p * (a + b) + (1 - 2 * p) * (Sqrt((a + w * b) * (w * a + b)) / (1 + w)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterTakakazuSeki(double a, double b) => 2 * Sqrt(PI * PI * a * b + 4 * (a - b) * (a - b));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterLockwood(double a, double b) => 4 * (((b * b) / a) * Atan(a / b) + ((a * a) / b) * Atan(b / a));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterBartolomeu(double a, double b)
        {
            var t = (PI / 4) * ((a - b) / b);
            return PI * Sqrt(2 * (a * a + b * b)) * (Sin(t) / t);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera1(double a, double b) => 4 * a + 2 * (PI - 2) * a * Pow(b / a, 1.456);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterRivera2(double a, double b) => 4 * ((PI * a * b + (a - b) * (a - b)) / (a + b)) - (89 / 146) * Pow((b * Sqrt(a) - a * Sqrt(b)) / (a + b), 2);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrell(double a, double b)
        {
            var s = Log(2) / Log(2 / (4 - PI));
            return 4 * (a + b) - ((2 * (4 - PI) * a * b) / Pow(Pow(a, s) + Pow(b, s), 1 / s));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterSykora(double a, double b) => 4 * ((PI * a * b + (a - b) * (a - b)) / (a + b)) - 0.5 * ((a * b) / (a + b)) * (((a - b) * (a - b)) / (PI * a * b + (a + b) * (a + b)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://www.mathsisfun.com/geometry/ellipse-perimeter.html</remarks>
        private static double EllipsePerimeterCantrellRamanujan(double a, double b)
        {
            var h = ((a - b) * (a - b)) / ((a + b) * (a + b));
            return PI * (a + b) * (1 + ((3 * h) / (10 + Sqrt(4 - 3 * h))) + ((4 / PI) - ((14) / (11))) * Pow(h, 12));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math07/EllipsePerimeterApprox07add.html
        /// </remarks>
        private static double EllipsePerimeterK13(double a, double b) => PI * (((a + b) / 2) + Sqrt((a * a + b * b) / 2));

        /// <summary>
        /// This one is not as good with a circle.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://ellipse-circumference2.blogspot.com/2011/12/accurate-online-ellipse-circumference.html</remarks>
        private static double EllipsePerimeterThomasBlankenhorn1(double a, double b)
        {
            var X1 = a;
            var X2 = b;
            var HMX = Max(X1, X2);
            var HMN = Min(X1, X2);
            var H1 = HMN / HMX;
            return 2 * PI * HMX * ((2 / PI) + 0.0000122 * Pow(H1, 0.6125) - 0.0021973 * Pow(H1, 1.225) + 0.919315 * Pow(H1, 1.8375) - 1.0359227 * Pow(H1, 2.45) + 0.861913 * Pow(H1, 3.0625) - 0.7274398 * Pow(H1, 3.675) + 0.6352295 * Pow(H1, 4.2875) - 0.436051 * Pow(H1, 4.9) + 0.1818904 * Pow(H1, 5.5125) - 0.0333691 * Pow(H1, 6.125));
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
            var X1 = a;
            var X2 = b;
            var HMX = Max(X1, X2);
            var HMN = Min(X1, X2);
            var H1 = HMN / HMX;
            return HMX * (4 + (3929 * Pow(H1, 1.5) + 1639157 * Pow(H1, 2) + 19407215 * Pow(H1, 2.5) + 24302653 * Pow(H1, 3) + 12892432 * Pow(H1, 3.5)) / (86251 + 1924742 * Pow(H1, 0.5) + 6612384 * Pow(H1, 1) + 7291509 * Pow(H1, 1.5) + 6436977 * Pow(H1, 2) + 3158719 * Pow(H1, 2.5)));
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
            var p = 3.982901;
            var q = 66.71674;
            var s = 18.31287;
            var t = 23.39728;
            var r = 4 * ((4 - PI) * (4 * s + t + 16) - (4 * p + q));
            return 4 * (a + b)
                - ((a * b) / (a + b))
                * ((p * (a + b) * (a + b) + q * a * b + r * ((a * b) / (a + b)) * ((a * b) / (a + b)))
                / ((a + b) * (a + b) + s * a * b + t * ((a * b) / (a + b)) * ((a * b) / (a + b))));
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
            var c1 = PI - 3;
            var c2 = PI;
            var c3 = 0.5;
            var c4 = (PI + 1) / 2;
            double c5 = 4;
            var k = 1 - ((c1 * a * b) / ((a * a + b * b) + c2 * Sqrt(c3 * a * b * a * b + a * b * Sqrt(a * b * (c4 * (a * a + b * b) + c5 * a * b)))));
            return 4 * ((PI * a * b + k * (a - b) * (a - b)) / (a + b));
        }

        #endregion

        #region Elliptic Arc From Points and Radii

        #endregion

        #region Elliptic Star Points

        /// <summary>
        ///
        /// </summary>
        /// <param name="num_points"></param>
        /// <param name="bounds"></param>
        /// <returns>Return PointFs to define a star.</returns>
        private PointF[] StarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            var pts = new PointF[num_points];

            double rx = bounds.Width / 2;
            double ry = bounds.Height / 2;
            var cx = bounds.X + rx;
            var cy = bounds.Y + ry;

            // Start at the top.
            var theta = -PI / 2;
            var dtheta = 4 * PI / num_points;
            for (var i = 0; i < num_points; i++)
            {
                pts[i] = new PointF(
                    (float)(cx + rx * Cos(theta)),
                    (float)(cy + ry * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }

        #endregion

        #region Find Center of a Circle From Three points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle between Two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CircleCenterFromThreePointsTests))]
        public static List<SpeedTester> CircleCenterFromThreePointsTests() => new List<SpeedTester> {
                new SpeedTester(() => TripointCircleCenter(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.TripointCircleCenter)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => CircleCenterFromPoints(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CircleCenterFromPoints)}(0, 0, 0, 1, 1, 1)")
           };

        /// <summary>
        /// Find the Center of A Circle from Three Points
        /// </summary>
        /// <param name="pointAX">First Point on the Ellipse</param>
        /// <param name="pointAY">First Point on the Ellipse</param>
        /// <param name="pointBX">Second Point on the Ellipse</param>
        /// <param name="pointBY">Second Point on the Ellipse</param>
        /// <param name="pointCX">Last Point on the Ellipse</param>
        /// <param name="pointCY">Last Point on the Ellipse</param>
        /// <returns>Returns the Center point of a Circle defined by three points</returns>
        /// <remarks>
        /// </remarks>
        public static (double X, double Y) TripointCircleCenter(
            double pointAX, double pointAY,
            double pointBX, double pointBY,
            double pointCX, double pointCY)
        {
            //  Calculate the slopes of the lines.
            var slopeA = Slope(pointAX, pointAY, pointBX, pointBY);
            var slopeB = Slope(pointCX, pointCY, pointBX, pointBY);
            var fY = ((((pointAX - pointBX) * (pointAX + pointBX)) + ((pointAY - pointBY) * (pointAY + pointBY))) / (2 * (pointAX - pointBX)));
            var fX = ((((pointCX - pointBX) * (pointCX + pointBX)) + ((pointCY - pointBY) * (pointCY + pointBY))) / (2 * (pointCX - pointBX)));
            var newY = ((fX - fY) / (slopeB - slopeA));
            var newX = (fX - (slopeB * newY));
            return (newX, newY);
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
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4103405/what-is-the-algorithm-for-finding-the-center-of-a-circle-from-three-points
        /// </remarks>
        public static (double X, double Y)? CircleCenterFromPoints(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y)
        {
            var offset = (p2X * p2X) + (p2Y * p2Y);
            var bc = ((p1X * p1X) + (p1Y * p1Y) - offset) * 0.5d;
            var cd = (offset - (p3X * p3X) - (p3Y * p3Y)) * 0.5d;
            var determinant = (p1X - p2X) * (p2Y - p3Y) - (p2X - p3X) * (p1Y - p2Y);

            if (Abs(determinant) < DoubleEpsilon)
                return null;

            return (
                (bc * (p2Y - p3Y) - cd * (p1Y - p2Y)) / determinant,
                (cd * (p1X - p2X) - bc * (p2X - p3X)) / determinant);
        }

        #endregion

        #region Find Polygon Ear

        /// <summary>
        /// Find the indexes of three points that form an "ear."
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static Triangle FindEar(Contour polygon, ref int a, ref int b, ref int c)
        {
            var num_points = polygon.Points.Count;

            for (a = 0; a < num_points; a++)
            {
                b = (a + 1) % num_points;
                c = (b + 1) % num_points;

                if (FormsEar(polygon, a, b, c))
                    return new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);
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
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Return true if the three points form an ear.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        private static bool FormsEar(Contour polygon, int a, int b, int c)
        {
            // See if the angle ABC is concave.
            if (AngleVector(
                polygon.Points[a].X, polygon.Points[a].Y,
                polygon.Points[b].X, polygon.Points[b].Y,
                polygon.Points[c].X, polygon.Points[c].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            var triangle = new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);

            // Check the other points to see
            // if they lie in triangle A, B, C.
            for (var i = 0; i < polygon.Points.Count; i++)
            {
                if ((i != a) && (i != b) && (i != c) && triangle.Contains(polygon.Points[i]))
                {
                    // This point is in the triangle
                    // do this is not an ear.
                    return false;
                }
            }

            // This is an ear.
            return true;
        }

        #endregion

        #region Fit in Rectangle

        /// <summary>
        ///
        /// </summary>
        /// <param name="size"></param>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Size2D FitRect(Size2D size, double radians)
        {
            var angleCos = Cos(radians);
            var angleSin = Sin(radians);

            var x1 = -size.Width * 0.5f;
            var x2 = size.Width * 0.5f;
            var x3 = size.Width * 0.5f;
            var x4 = -size.Width * 0.5f;

            var y1 = size.Height * 0.5f;
            var y2 = size.Height * 0.5f;
            var y3 = -size.Height * 0.5f;
            var y4 = -size.Height * 0.5f;

            var x11 = (x1 * angleCos) + (y1 * angleSin);
            var y11 = (-x1 * angleSin) + (y1 * angleCos);

            var x21 = (x2 * angleCos) + (y2 * angleSin);
            var y21 = (-x2 * angleSin) + (y2 * angleCos);

            var x31 = (x3 * angleCos) + (y3 * angleSin);
            var y31 = (-x3 * angleSin) + (y3 * angleCos);

            var x41 = (x4 * angleCos) + (y4 * angleSin);
            var y41 = (-x4 * angleSin) + (y4 * angleCos);

            var x_min = Min(Min(x11, x21), Min(x31, x41));
            var x_max = Max(Max(x11, x21), Max(x31, x41));

            var y_min = Min(Min(y11, y21), Min(y31, y41));
            var y_max = Max(Max(y11, y21), Max(y31, y41));

            return new Size2D((x_max - x_min), (y_max - y_min));
        }

        #endregion

        #region Gear Points

        // Draw the gear.
        private void PicGears_Paint(PaintEventArgs e, Rectangle bounds)
        {
            // Draw smoothly.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            const double radius = 50;
            const double tooth_length = 10;
            var x = bounds.Width / 2 - radius - tooth_length - 1;
            double y = bounds.Height / 3;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightBlue, Pens.Blue, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);

            x += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightGreen, Pens.Green, new Point2D(x, y),
                radius, tooth_length, 10, 5, false);

            y += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.Pink, Pens.Red, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);
        }

        // Draw a gear.
        private void DrawGear(Graphics gr, Brush axle_brush, Brush gear_brush, Pen gear_pen, Point2D center, double radius, double tooth_length, int num_teeth, double axle_radius, bool start_with_tooth)
        {
            var dtheta = PI / num_teeth;
            var dtheta_degrees = dtheta * 180 / PI; // dtheta in degrees.

            const double chamfer = 2;
            var tooth_width = radius * dtheta - chamfer;
            var alpha = tooth_width / (radius + tooth_length);
            var alpha_degrees = alpha * 180 / PI;
            var phi = (dtheta - alpha) / 2;

            // Set theta for the beginning of the first tooth.
            double theta;
            if (start_with_tooth)
                theta = dtheta / 2;
            else
                theta = -dtheta / 2;

            // Make rectangles to represent the gear's inner and outer arcs.
            var inner_rect = new Rectangle2D(
                center.X - radius, center.Y - radius,
                2 * radius, 2 * radius);
            var outer_rect = new Rectangle2D(
                center.X - radius - tooth_length, center.Y - radius - tooth_length,
                2 * (radius + tooth_length), 2 * (radius + tooth_length));

            // Make a path representing the gear.
            var path = new GraphicsPath();
            for (var i = 0; i < num_teeth; i++)
            {
                // Move across the gap between teeth.
                var degrees = theta * 180 / PI;
                path.AddArc(inner_rect.ToRectangleF(), (float)degrees, (float)dtheta_degrees);
                theta += dtheta;

                // Move across the tooth's outer edge.
                degrees = (theta + phi) * 180 / PI;
                path.AddArc(outer_rect.ToRectangleF(), (float)degrees, (float)alpha_degrees);
                theta += dtheta;
            }

            path.CloseFigure();

            // Draw the gear.
            gr.FillPath(gear_brush, path);
            gr.DrawPath(gear_pen, path);
            gr.FillEllipse(axle_brush,
                 (float)(center.X - axle_radius), (float)(center.Y - axle_radius),
                (float)(2 * axle_radius), (float)(2 * axle_radius));
        }

        #endregion

        #region Heart Interpolation

        // The curve's parametric equations.
        private Point2D Heart(double t)
        {
            var sin_t = Sin(t);
            return new Point2D(16 * sin_t * sin_t * sin_t,
                 13 * Cos(t)
                - 5 * Cos(2 * t)
                - 2 * Cos(3 * t)
                - Cos(4 * t));
        }

        // Draw the curve on a bitmap.
        private Bitmap DrawHeart(int width, int height)
        {
            var bm = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Generate the points.
                const int num_points = 100;
                var points = new List<Point2D>();
                var dt = 2 * PI / num_points;
                for (double t = 0; t <= 2 * PI; t += dt)
                    points.Add(new Point2D(Heart(t).X, Heart(t).Y));

                // Get the coordinate bounds.
                var wxmin = points[0].X;
                var wxmax = wxmin;
                var wymin = points[0].Y;
                var wymax = wymin;
                foreach (Point2D point in points)
                {
                    if (wxmin > point.X)
                        wxmin = point.X;
                    if (wxmax < point.X)
                        wxmax = point.X;
                    if (wymin > point.Y)
                        wymin = point.Y;
                    if (wymax < point.Y)
                        wymax = point.Y;
                }

                // Make the world coordinate rectangle.
                var world_rect = new Rectangle2D(
                    wxmin, wymin, wxmax - wxmin, wymax - wymin);

                // Make the device coordinate rectangle with a margin.
                const int margin = 5;
                var device_rect = new Rectangle2D(
                    margin, margin,
                    width - 2 * margin,
                    height - 2 * margin);

                // Map world to device coordinates without distortion.
                // Flip vertically so Y increases downward.
                SetTransformationWithoutDisortion(gr, world_rect, device_rect, false, true);

                // Draw the curve.
                gr.FillPolygon(Brushes.Pink, points.ToPointFArray());
                using (var pen = new Pen(Color.Red, 0))
                {
                    gr.DrawPolygon(pen, points.ToPointFArray());

                    //// Draw a rectangle around the coordinate bounds.
                    //pen.Color = Color.Blue;
                    //gr.DrawRectangle(pen, Rectangle.Round( world_rect));

                    //// Draw the X and Y axes.
                    //pen.Color = Color.Green;
                    //gr.DrawLine(pen, -20, 0, 20, 0);
                    //gr.DrawLine(pen, 0, -20, 0, 20);
                    //for (int x = -20; x <= 20; x++)
                    //    gr.DrawLine(pen, x, -0.3f, x, 0.3f);
                    //for (int y = -20; y <= 20; y++)
                    //    gr.DrawLine(pen, -0.3f, y, 0.3f, y);
                }
            }
            return bm;
        }

        // Map from world coordinates to device coordinates
        // without distortion.
        private void SetTransformationWithoutDisortion(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            // Get the aspect ratios.
            var world_aspect = world_rect.Width / world_rect.Height;
            var device_aspect = device_rect.Width / device_rect.Height;

            // Adjust the world rectangle to maintain the aspect ratio.
            var world_cx = world_rect.X + world_rect.Width / 2f;
            var world_cy = world_rect.Y + world_rect.Height / 2f;
            if (world_aspect > device_aspect)
            {
                // The world coordinates are too short and width.
                // Make them taller.
                var world_height = world_rect.Width / device_aspect;
                world_rect = new Rectangle2D(
                    world_rect.Left,
                    world_cy - world_height / 2f,
                    world_rect.Width,
                    world_height);
            }
            else
            {
                // The world coordinates are too tall and thin.
                // Make them wider.
                var world_width = device_aspect * world_rect.Height;
                world_rect = new Rectangle2D(
                    world_cx - world_width / 2f,
                    world_rect.Top,
                    world_width,
                    world_rect.Height);
            }

            // Map the new world coordinates into the device coordinates.
            SetTransformation(gr, world_rect, device_rect, invert_x, invert_y);
        }

        // Map from world coordinates to device coordinates.
        private void SetTransformation(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            var device_points = new List<Point2D>
            {
                new Point2D(device_rect.Left, device_rect.Top),      // Upper left.
                new Point2D(device_rect.Right, device_rect.Top),     // Upper right.
                new Point2D(device_rect.Left, device_rect.Bottom)   // Lower left.
            };

            if (invert_x)
            {
                device_points[0] = new Point2D(device_rect.Right, device_points[0].Y);
                device_points[1] = new Point2D(device_rect.Left, device_points[1].Y);
                device_points[2] = new Point2D(device_rect.Right, device_points[2].Y);
            }
            if (invert_y)
            {
                device_points[0] = new Point2D(device_points[0].X, device_rect.Bottom);
                device_points[1] = new Point2D(device_points[1].X, device_rect.Bottom);
                device_points[2] = new Point2D(device_points[2].X, device_rect.Top);
            }

            gr.Transform = new Matrix(world_rect.ToRectangleF(), device_points.ToPointFArray());
        }

        #endregion

        #region Hermite Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate1DTests))]
        public static List<SpeedTester> HermiteInterpolate1DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate1D(0, 1, 2, 3, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate1D)}(0, 1, 2, 3, 0.5d, 1, 0)"),
                new SpeedTester(() => Hermite(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.Hermite)}(0, 1, 2, 3, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="s"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        private static double HermiteInterpolate1D(
            double v0,
            double v1,
            double v2,
            double v3,
            double s, double tension, double bias)
        {
            var sSquared = s * s;
            var sCubed = sSquared * s;
            var m0 = (v1 - v0) * (1 + bias) * (1 - tension) / 2;
            m0 += (v2 - v1) * (1 - bias) * (1 - tension) / 2;
            var m1 = (v2 - v1) * (1 + bias) * (1 - tension) / 2;
            m1 += (v3 - v2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * sCubed - 3 * sSquared + 1;
            var a1 = sCubed - 2 * sSquared + s;
            var a2 = sCubed - sSquared;
            var a3 = -2 * sCubed + 3 * sSquared;

            return (a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2);
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="v1">Source position.</param>
        /// <param name="t1">Source tangent.</param>
        /// <param name="v2">Source position.</param>
        /// <param name="t2">Source tangent.</param>
        /// <param name="s">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static double Hermite(
            double v1,
            double t1,
            double v2,
            double t2,
            double s)
        {
            double result;
            var sSquared = s * s;
            var sCubed = sSquared * s;

            if (s == 0f)
            {
                result = v1;
            }
            else if (s == 1f)
            {
                result = v2;
            }
            else
            {
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed
                   + (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared
                   + t1 * s
                   + v1;
            }

            return result;
        }

        #endregion

        #region Hermite Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate2DTests))]
        public static List<SpeedTester> HermiteInterpolate2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        private static (double X, double Y) HermiteInterpolate2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double mu, double tension, double bias)
        {
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;

            var mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            var mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            var mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            var mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + mu;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2);
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate1(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            //double mu2 = mu * mu;
            //double mu3 = mu2 * mu;
            //Point2D m0 = (y1 - y0) * (1 + bias) * (1 - tension) * 0.5;
            //m0 += (y2 - y1) * (1 - bias) * (1 - tension) * 0.5;
            //Point2D m1 = (y2 - y1) * (1 + bias) * (1 - tension) * 0.5;
            //m1 += (y3 - y2) * (1 - bias) * (1 - tension) * 0.5;
            //double a0 = 2 * mu3 - 3 * mu2 + 1;
            //double a1 = mu3 - 2 * mu2 + mu;
            //double a2 = mu3 - mu2;
            //double a3 = -2 * mu3 + 3 * mu2;
            //return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
            var mu2 = index * index;
            var mu3 = mu2 * index;
            Vector2D m0 = aTan.Subtract(a).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m0 = m0.Add(b.Subtract(aTan).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            Vector2D m1 = b.Subtract(aTan).Scale(1 + bias).Scale(1 - tension).Scale(0.5);
            m1 = m1.Add(bTan.Subtract(b).Scale(1 - bias).Scale(1 - tension).Scale(0.5));
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + index;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;
            return ((Point2D)aTan.Scale(a0).Add(m0.Scale(a1)).Add(m1.Scale(a2)).Add(b.Scale(a3)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">Tension: 1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">Bias: 0 is even,</param>
        /// <remarks>positive is towards First segment, negative towards the other</remarks>
        /// <returns></returns>
        private double Hermite_Interpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            var m0 = ((y1 - y0) * ((1 + bias) * ((1 - tension) / 2)));
            m0 += ((y2 - y1) * ((1 - bias) * ((1 - tension) / 2)));
            var m1 = ((y2 - y1) * ((1 + bias) * ((1 - tension) / 2)));
            m1 += ((y3 - y2) * ((1 - bias) * ((1 - tension) / 2)));
            var mu2 = (mu * mu);
            var mu3 = (mu2 * mu);
            var a0 = (((2 * mu3) - (3 * mu2)) + 1);
            var a1 = ((mu3 - (2 * mu2)) + mu);
            var a2 = (mu3 - mu2);
            var a3 = (((2 * mu3) * -1) + (3 * mu2));
            return ((a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2))));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        private double HermiteInterpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            var m0 = ((y1 - y0) * ((1 + bias) * ((1 - tension) / 2)));
            m0 += ((y2 - y1) * ((1 - bias) * ((1 - tension) / 2)));
            var m1 = ((y2 - y1) * ((1 + bias) * ((1 - tension) / 2)));
            m1 += ((y3 - y2) * ((1 - bias) * ((1 - tension) / 2)));
            var mu2 = (mu * mu);
            var mu3 = (mu2 * mu);
            var a0 = (((2 * mu3) - (3 * mu2)) + 1);
            var a1 = ((mu3 - (2 * mu2)) + mu);
            var a2 = (mu3 - mu2);
            var a3 = (((2 * mu3) * -1) + (3 * mu2));

            return ((a0 * y1) + ((a1 * m0) + ((a2 * m1) + (a3 * y2))));
        }

        /// <summary>
        /// Tension: 1 is high, 0 normal, -1 is low
        /// Bias: 0 is even,
        /// positive is towards First segment,
        /// negative towards the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aTan"></param>
        /// <param name="b"></param>
        /// <param name="bTan"></param>
        /// <param name="index"></param>
        /// <param name="tension"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D Interpolate2(Point2D a, Point2D aTan, Point2D b, Point2D bTan, double tension, double bias, double index)
        {
            var t2 = (index * index);
            var t3 = (t2 * index);
            var tb = ((1 + bias) * ((1 - tension) / 2));
            return (Point2D)aTan.Scale(((2 * t3) - (3 * t2)) + 1).Add(aTan.Subtract(a).Add(b.Subtract(aTan)).Scale((t3 - (2 * t2)) + index).Add(b.Subtract(aTan).Add(bTan.Subtract(b)).Scale(t3 - t2)).Scale(tb).Add(b.Scale((3 * t2) - (2 * t3))));
        }

        #endregion

        #region Hermite Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate3DTests))]
        public static List<SpeedTester> HermiteInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => HermiteInterpolate3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        private static (double X, double Y, double Z) HermiteInterpolate3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double mu, double tension, double bias)
        {
            var mu2 = mu * mu;
            var mu3 = mu2 * mu;

            var mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            var mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            var mZ0 = (z1 - z0) * (1 + bias) * (1 - tension) / 2;
            mZ0 += (z2 - z1) * (1 - bias) * (1 - tension) / 2;
            var mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            var mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            var mZ1 = (z2 - z1) * (1 + bias) * (1 - tension) / 2;
            mZ1 += (z3 - z2) * (1 - bias) * (1 - tension) / 2;
            var a0 = 2 * mu3 - 3 * mu2 + 1;
            var a1 = mu3 - 2 * mu2 + mu;
            var a2 = mu3 - mu2;
            var a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2,
                a0 * z1 + a1 * mZ0 + a2 * mZ1 + a3 * z2);
        }

        #endregion

        #region Hermite to Cubic Bezier

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/29087503/how-to-create-jigsaw-puzzle-pieces-using-opengl-and-bezier-curve/29089681#29089681</remarks>
        public CubicBezier ToCubicBezier(Point2D a, Point2D aTan, Point2D b, Point2D bTan) => new CubicBezier(aTan, new Point2D(aTan.X - (b.X - a.X) / 6, aTan.Y - (b.Y - a.Y) / 6), new Point2D(b.X + (bTan.X - aTan.X) / 6, b.Y + (bTan.Y - aTan.Y) / 6), bTan);

        #endregion

        #region Horizontal Line Segments Overlap

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segAX"></param>
        /// <param name="segAY"></param>
        /// <param name="segBX"></param>
        /// <param name="segBY"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        public static bool HorzSegmentsOverlap(double segAX, double segAY, double segBX, double segBY)
        {
            if (segAX > segAY) Maths.Swap(ref segAX, ref segAY);
            if (segBX > segBY) Maths.Swap(ref segBX, ref segBY);
            return (segAX < segBY) && (segBX < segAY);
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
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        public static (int, (double X, double Y), (double X, double Y)) FindCircleCircleIntersections(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // Find the distance between the centers.
            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Sqrt(dx * dx + dy * dy);

            (double X, double Y) intersection1;
            (double X, double Y) intersection2;

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if ((Abs(dist) < DoubleEpsilon) && (Abs(radius0 - radius1) < DoubleEpsilon))
            {
                // No solutions, the circles coincide.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                var a = (radius0 * radius0
                    - radius1 * radius1 + dist * dist) / (2 * dist);
                var h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                var cx2 = cx0 + a * (cx1 - cx0) / dist;
                var cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = (
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist));
                intersection2 = (
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist));

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < DoubleEpsilon)
                    return (1, intersection1, intersection2);

                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.xarg.org/2016/07/calculate-the-intersection-points-of-two-circles/
        /// </remarks>
        public static (int, (double X, double Y), (double X, double Y))? CircleCircleIntersection(Circle A, Circle B)
        {
            var d = Sqrt(Pow(B.X - A.X, 2) + Pow(B.Y - A.Y, 2));

            if (d <= A.Radius + B.Radius)
            {
                var x = (A.Radius * A.Radius - B.Radius * B.Radius + d * d) / (2 * d);
                var y = Sqrt(A.Radius * A.Radius - x * x);

                if (A.Radius < Abs(x))
                {
                    // No intersection, one circle is in the other
                    return null;
                }
                else
                {
                    (double X, double Y) e1 = ((B.X - A.X) / d, (B.Y - A.Y) / d);
                    (double X, double Y) e2 = (-e1.X, e1.Y);
                    (double X, double Y) P1 = (A.X + x * e1.X + y * e2.Y, A.Y + x * e1.Y + y * e2.X);
                    (double X, double Y) P2 = (A.X + x * e1.X - y * e2.Y, A.Y + x * e1.Y - y * e2.X);
                    return (2, P1, P2);
                }
            }
            else
            {
                // No Intersection, far outside
                return null;
            }
        }

        #endregion

        #region Intersection of Circle and line Segment

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircleLineSegmentIntersection1(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
                return result;

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Add the points if they are between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(lAX + t * dx, lAY + t * dy));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points if they are between the end points of the line segment.
                result = new Intersection(IntersectionState.Intersection);
                if ((t1 >= 0d) && (t1 <= 1d))
                    result.AppendPoint(new Point2D(lAX + t1 * dx, lAY + t1 * dy));
                if ((t2 >= 0d) && (t2 <= 1d))
                    result.AppendPoint(new Point2D(lAX + t2 * dx, lAY + t2 * dy));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircleLineSegmentIntersection(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            Intersection result;

            var a = (lBX - lAX) * (lBX - lAX) + (lBY - lAY) * (lBY - lAY);
            var b = 2 * ((lBX - lAX) * (lAX - cX) + (lBY - lAY) * (lAY - cY));
            var c = cX * cX + cY * cY + lAX * lAX + lAY * lAY - 2 * (cX * lAX + cY * lAY) - r * r;

            var determinant = b * b - 4 * a * c;
            if (determinant < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (determinant == 0)
            {
                result = new Intersection(IntersectionState.Tangent | IntersectionState.Intersection);
                var u1 = (-b) / (2 * a);
                if (u1 < 0 || u1 > 1)
                {
                    if ((u1 < 0) || (u1 > 1))
                    {
                        result = new Intersection(IntersectionState.Outside);
                    }
                    else
                    {
                        result = new Intersection(IntersectionState.Inside);
                    }
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= u1 && u1 <= 1)
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                }
            }
            else
            {
                var e = Sqrt(determinant);
                var u1 = (-b + e) / (2 * a);
                var u2 = (-b - e) / (2 * a);
                if ((u1 < 0 || u1 > 1) && (u2 < 0 || u2 > 1))
                {
                    if ((u1 < 0 && u2 < 0) || (u1 > 1 && u2 > 1))
                    {
                        result = new Intersection(IntersectionState.Outside);
                    }
                    else
                    {
                        result = new Intersection(IntersectionState.Inside);
                    }
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= u1 && u1 <= 1)
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    if (0 <= u2 && u2 <= 1)
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u2));
                }
            }
            return result;
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
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (int, (double X, double Y), (double X, double Y)) LineCircle(
            (double X, double Y) center,
            double radius,
            (double X, double Y) point1,
            (double X, double Y) point2)
        {
            double t;

            var dx = point2.X - point1.X;
            var dy = point2.Y - point1.Y;

            var A = dx * dx + dy * dy;
            var B = 2 * (dx * (point1.X - center.X) + dy * (point1.Y - center.Y));
            var C = (point1.X - center.X) * (point1.X - center.X) + (point1.Y - center.Y) * (point1.Y - center.Y) - radius * radius;

            (double X, double Y) intersection1;
            (double X, double Y) intersection2;

            var det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = (double.NaN, double.NaN);
                intersection2 = (double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (Abs(det) < DoubleEpsilon)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = (point1.Item1 + t * dx, point1.Item2 + t * dy);
                intersection2 = (double.NaN, double.NaN);
                return (1, intersection1, intersection2);
            }
            else
            {
                // Two solutions.
                t = ((-B + Sqrt(det)) / (2 * A));
                intersection1 = (point1.Item1 + t * dx, point1.Item2 + t * dy);
                t = ((-B - Sqrt(det)) / (2 * A));
                intersection2 = (point1.Item1 + t * dx, point1.Item2 + t * dy);
                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircleLineIntersection(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
                return result;

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t * dx, lAY + t * dy));
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t1 * dx, lAY + t1 * dy));
                result.AppendPoint(new Point2D(lAX + t2 * dx, lAY + t2 * dy));
            }

            return result;
        }

        #endregion

        #region Intersection of Conic Section with Line segment
        // http://csharphelper.com/blog/2014/11/see-where-a-line-intersects-a-conic-section-in-c/
        #endregion

        #region Intersection of Conic Section with Conic Section
        // http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/
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
            var d = (ellipseB.Center.X * ellipseB.Center.X - ellipseA.Center.X * ellipseA.Center.X - ellipseB.MajorRadius * ellipseB.MajorRadius - Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + ellipseA.MajorRadius * ellipseA.MajorRadius);
            var a = (Pow(2 * ellipseA.Center.X - 2 * ellipseB.Center.X, 2) + 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            var b = (2 * d * (2 * ellipseA.Center.X - 2 * ellipseB.Center.X) - 8 * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2));
            var C = (4 * ellipseB.Center.X * ellipseB.Center.X * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) + d * d - 4 * Pow(ellipseB.Center.Y - ellipseA.Center.Y, 2) * ellipseB.MajorRadius * ellipseB.MajorRadius);
            var XA = ((-b + Sqrt(b * b - 4 * a * C)) / (2 * a));
            var XB = ((-b - Sqrt(b * b - 4 * a * C)) / (2 * a));
            var YA = (Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            var YB = (-Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XA - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            var YC = (Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            var YD = (-Sqrt(ellipseA.MajorRadius * ellipseA.MajorRadius - Pow(XB - ellipseA.Center.X, 2)) + ellipseA.Center.Y);
            var e = ((XA - ellipseB.Center.X) + Pow(YA - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            var F = ((XA - ellipseB.Center.X) + Pow(YB - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            var g = ((XB - ellipseB.Center.X) + Pow(YC - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            var H = ((XB - ellipseB.Center.X) + Pow(YD - ellipseB.Center.Y, 2) - ellipseB.MajorRadius * ellipseB.MajorRadius);
            if (Abs(F) < Abs(e))
                YA = YB;
            if (Abs(H) < Abs(g))
                YC = YD;
            if (Abs(ellipseA.Center.Y - ellipseB.Center.Y) < DoubleEpsilon)
                YC = 2 * ellipseA.Center.Y - YA;
            return new LineSegment(XA, YA, XB, YC);
        }

        /// <summary>
        ///
        /// </summary>
        public class EllipseIntersectStuff
        {
            internal bool GotEllipse1 = false;
            internal bool GotEllipse2 = false;
            private Rectangle2D Ellipse1 = new Rectangle2D();
            private Rectangle2D Ellipse2 = new Rectangle2D();

            // Equations that define the ellipses.
            internal double Dx1 = 0;
            internal double Dy1 = 0;
            internal double Dx2 = 0;
            internal double Dy2 = 0;

            internal double Rx1 = 0;
            internal double Ry1 = 0;
            internal double Rx2 = 0;
            internal double Ry2 = 0;

            internal double A1 = 0;
            internal double B1 = 0;
            internal double C1 = 0;
            internal double D1 = 0;
            internal double E1 = 0;
            internal double F1 = 0;
            internal double A2 = 0;
            internal double B2 = 0;
            internal double C2 = 0;
            internal double D2 = 0;
            internal double E2 = 0;
            internal double F2 = 0;

            // The points of intersection.
            internal List<Point2D> Roots = new List<Point2D>();
            internal List<double> RootSign1 = new List<double>();
            internal List<double> RootSign2 = new List<double>();
            internal List<Point2D> PointsOfIntersection = new List<Point2D>();

            // Difference function tangent lines.
            internal double TangentX = 0;
            internal List<Point2D> TangentCenters;
            internal List<Point2D> TangentP1;
            internal List<Point2D> TangentP2;
        }

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

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
                return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingNewtonsMethod(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (var i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (var i = 0; i < eis.Roots.Count; i++)
            {
                var y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                var y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Abs(y1 - y2) < DoubleEpsilon);
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

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
                return;

            // Find roots for each of the difference equations.
            double[] signs = { +1f, -1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    List<Point2D> points = FindRootsUsingBinaryDivision(
                        xmin, xmax,
                        eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, sign1,
                        eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, sign2);
                    if (points.Count > 0)
                    {
                        eis.Roots.AddRange(points);
                        for (var i = 0; i < points.Count; i++)
                        {
                            eis.RootSign1.Add(sign1);
                            eis.RootSign2.Add(sign2);
                        }
                    }
                }
            }

            // Find corresponding points of intersection.
            eis.PointsOfIntersection = new List<Point2D>();
            for (var i = 0; i < eis.Roots.Count; i++)
            {
                var y1 = G1(eis.Roots[i].X, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, eis.RootSign1[i]);
                var y2 = G1(eis.Roots[i].X, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, eis.RootSign2[i]);
                eis.PointsOfIntersection.Add(new Point2D(eis.Roots[i].X, y1));

                // Validation.
                Debug.Assert(Abs(y1 - y2) < DoubleEpsilon);
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
            var roots = new List<Point2D>();
            const int num_tests = 1000;
            var delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            var x0 = xmin;
            for (var i = 0; i < num_tests; i++)
            {
                // Try to find a root at this position.
                UseNewtonsMethod(x0, out double x, out double y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    var is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Abs(pt.X - x) < DoubleEpsilon)
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
                        if (roots.Count > 1)
                            return roots;
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
            var roots = new List<Point2D>();
            const int num_tests = 10;
            var delta_x = (xmax - xmin) / (num_tests - 1);

            // Loop over the possible x values looking for roots.
            var x0 = xmin;
            for (var i = 0; i < num_tests; i++)
            {
                // Try to find a root in this range.
                UseBinaryDivision(x0, delta_x, out double x, out double y,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);

                // See if we have already found this root.
                if (IsNumber(y))
                {
                    var is_new = true;
                    foreach (Point2D pt in roots)
                    {
                        if (Abs(pt.X - x) < DoubleEpsilon)
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
                        if (roots.Count > 1)
                            return roots;
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
            var iterations = 0;

            do
            {
                // Display this guess x0.
                iterations++;

                // Make sure x0 isn't on a flat spot.
                var g_prime = GPrime(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                while (Abs(g_prime) < tiny)
                {
                    x0 += tiny;
                    g_prime = GPrime(x0,
                        A1, B1, C1, D1, E1, F1, sign1,
                        A2, B2, C2, D2, E2, F2, sign2);
                }

                // Calculate the next estimate for x0.
                var g = G(x0,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                epsilon = -g / g_prime;
                x0 += epsilon;
            } while ((Abs(epsilon) > cutoff) && (iterations < max_iterations));

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
            var xmin = x0;
            var g_xmin = G(xmin,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmin) < DoubleEpsilon)
            {
                x = xmin;
                y = g_xmin;
                return;
            }

            var xmax = xmin + delta_x;
            var g_xmax = G(xmax,
                A1, B1, C1, D1, E1, F1, sign1,
                A2, B2, C2, D2, E2, F2, sign2);
            if (Abs(g_xmax) < DoubleEpsilon)
            {
                x = xmax;
                y = g_xmax;
                return;
            }

            // Get the sign of the values.
            int sgn_min, sgn_max;
            if (IsNumber(g_xmin))
                sgn_min = Sign(g_xmin);
            else
                sgn_min = sgn_nan;
            if (IsNumber(g_xmax))
                sgn_max = Sign(g_xmax);
            else
                sgn_max = sgn_nan;

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
            var sgn_mid = 0;
            for (var i = 0; i < num_trials; i++)
            {
                // Get values for the midpoint.
                xmid = (xmin + xmax) / 2;
                g_xmid = G(xmid,
                    A1, B1, C1, D1, E1, F1, sign1,
                    A2, B2, C2, D2, E2, F2, sign2);
                if (IsNumber(g_xmid))
                    sgn_mid = Sign(g_xmid);
                else
                    sgn_mid = sgn_nan;

                // If sgn_mid is 0, gxmid is 0 so this is the root.
                if (sgn_mid == 0)
                    break;

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

            if (IsNumber(g_xmid) && (Abs(g_xmid) < DoubleEpsilon))
            {
                x = xmid;
                y = g_xmid;
            }
            else if (IsNumber(g_xmin) && (Abs(g_xmin) < DoubleEpsilon))
            {
                x = xmin;
                y = g_xmin;
            }
            else if (IsNumber(g_xmax) && (Abs(g_xmax) < DoubleEpsilon))
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
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static List<Point2D> GetPointsFromEquation(double xmin, double xmax,
            double a, double b, double c, double d, double e, double f)
        {
            var points = new List<Point2D>();
            for (var x = xmin; x <= xmax; x++)
            {
                var y = G1(a, b, c, d, e, f, x, +1f);
                if (IsNumber(y))
                    points.Add(new Point2D(x, y));
            }
            for (var x = xmax; x >= xmin; x--)
            {
                var y = G1(a, b, c, d, e, f, x, -1f);
                if (IsNumber(y))
                    points.Add(new Point2D(x, y));
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
            var xmin = Min(xmin1, xmin2);
            var xmax = Max(xmax1, xmax2);
            var result = new List<List<Point2D>>();

            double[] signs = { -1f, +1f };
            foreach (var sign1 in signs)
            {
                foreach (var sign2 in signs)
                {
                    var points = new List<Point2D>();
                    result.Add(points);
                    for (var x = xmin; x <= xmax; x++)
                    {
                        var y1 = G1(A1, B1, C1, D1, E1, F1, x, sign1);
                        if (IsNumber(y1))
                        {
                            var y2 = G1(A2, B2, C2, D2, E2, F2, x, sign2);
                            if (IsNumber(y2))
                                points.Add(new Point2D(x, y1 - y2));
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

            if (!eis.GotEllipse1 || !eis.GotEllipse2)
                return;

            const double tangent_length = 50;

            //++
            var tangent_y = G(eis.TangentX,
                eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f,
                eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
            if (IsNumber(tangent_y))
            {
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
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
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, +1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
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
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, +1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
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
                var slope =
                    G1Prime(eis.TangentX, eis.A1, eis.B1, eis.C1, eis.D1, eis.E1, eis.F1, -1f)
                    - G1Prime(eis.TangentX, eis.A2, eis.B2, eis.C2, eis.D2, eis.E2, eis.F2, -1f);
                if (IsNumber(slope))
                {
                    var delta_x = Sqrt(
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
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void GetEllipseFormula(Rectangle2D rect,
            out double Dx, out double Dy, out double Rx, out double Ry,
            out double a, out double b, out double c, out double d,
            out double e, out double f)
        {
            Dx = rect.X + rect.Width / 2f;
            Dy = rect.Y + rect.Height / 2f;
            Rx = rect.Width / 2f;
            Ry = rect.Height / 2f;

            a = 1f / Rx / Rx;
            b = 0;
            c = 1f / Ry / Ry;
            d = -2f * Dx / Rx / Rx;
            e = -2f * Dy / Ry / Ry;
            f = Dx * Dx / Rx / Rx + Dy * Dy / Ry / Ry - 1;

            // Verify the parameters.
            Console.WriteLine();
            var xmid = rect.Left + rect.Width / 2f;
            var ymid = rect.Top + rect.Height / 2f;
            VerifyEquation(a, b, c, d, e, f, rect.Left, ymid);
            VerifyEquation(a, b, c, d, e, f, rect.Right, ymid);
            VerifyEquation(a, b, c, d, e, f, xmid, rect.Top);
            VerifyEquation(a, b, c, d, e, f, xmid, rect.Bottom);
        }

        /// <summary>
        /// Verify that the equation gives a value close to 0 for the given point (x, y).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static void VerifyEquation(double a, double b, double c, double d, double e, double f, double x, double y)
        {
            var total = a * x * x + b * x * y + c * y * y + d * x + e * y + f;
            Console.WriteLine($"VerifyEquation ({x}, {y}) = {total}");
            Debug.Assert(Abs(total) < 0.001f);
        }

        /// <summary>
        /// Calculate G1(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1(double x, double a, double b, double c, double d, double e, double f, double root_sign)
        {
            var result = b * x + e;
            result *= result;
            result -= 4 * c * (a * x * x + d * x + f);
            result = root_sign * Sqrt(result);
            result = -(b * x + e) + result;
            result = result / 2 / c;

            return result;
        }

        /// <summary>
        /// Calculate G1'(x). root_sign is -1 or 1.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="root_sign"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static double G1Prime(double x, double a, double b, double c, double d, double e, double f, double root_sign)
        {
            var numerator = 2 * (b * x + e) * b - 4 * c * (2 * a * x + d);
            var denominator = 2 * Sqrt((b * x + e) * (b * x + e) - 4 * c * (a * x * x + d * x + f));
            var result = -b + root_sign * numerator / denominator;
            result = result / 2 / c;
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
            => G1(x, A1, B1, C1, D1, E1, F1, sign1)
            - G1(x, A2, B2, C2, D2, E2, F2, sign2);

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
            => G1Prime(x, A1, B1, C1, D1, E1, F1, sign1)
            - G1Prime(x, A2, B2, C2, D2, E2, F2, sign2);

        /// <summary>
        /// Return true if the number is not infinity or NaN.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/11/see-where-two-ellipses-intersect-in-c-part-1/</remarks>
        private static bool IsNumber(double number)
            => !(double.IsNaN(number) || double.IsInfinity(number));

        #endregion

        #region Intersection of Ellipse and Line Segment

        /// <summary>
        /// Find the points of the intersection between an unrotated ellipse and a line segment.
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
        public static (bool, (double, double)?, bool, (double, double)?) FindEllipseSegmentIntersections(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((cx == 0d) || (cy == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return (false, null, false, null);

            // Translate lines to meet the ellipse translated centered at the origin.
            var p1X = x0 - cx;
            var p1Y = y0 - cy;
            var p2X = x1 - cx;
            var p2Y = y1 - cy;

            // Calculate the quadratic parameters.
            var a = (p2X - p1X) * (p2X - p1X) / rx / rx + (p2Y - p1Y) * (p2Y - p1Y) / ry / ry;
            var b = 2d * p1X * (p2X - p1X) / rx / rx + 2 * p1Y * (p2Y - p1Y) / ry / ry;
            var c = p1X * p1X / rx / rx + p1Y * p1Y / ry / ry - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                var t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return ((t >= 0d) && (t <= 1d),
                        (p1X + (p2X - p1X) * t + cx,
                        p1Y + (p2Y - p1Y) * t + cy),
                        false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                var t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                var t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Return the points. If the points are on the segment set the bool to true.
                return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // Return the points.
            return (false, null, false, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://forums.codeguru.com/showthread.php?157823-How-to-get-ellipse-and-line-Intersection-points
        /// </remarks>
        public static (bool, (double, double)?, bool, (double, double)?) EllipseLineSegment(
            double cX, double cY,
            double rX, double rY,
            double x1, double y1,
            double x2, double y2)
        {
            double a;
            double b;
            double c;
            var m = 0d;

            // Check whether line is vertical.
            if (x1 != x2)
            {
                m = (y2 - y1) / (x2 - x1);
                var cc = y1 - m * x1;

                // Non-vertical line case.
                a = rY * rY + rX * rX * m * m;
                b = 2d * rX * rX * cc * m - 2d * rX * rX * cY * m - 2d * cX * rY * rY;
                c = rY * rY * cX * cX + rX * rX * cc * cc - 2 * rX * rX * cY * cc + rX * rX * cY * cY - rX * rX * rY * rY;
            }
            else
            {
                // vertical line case.
                a = rX * rX;
                b = -2d * cY * rX * rX;
                c = -rX * rX * rY * rY + rY * rY * (x1 - cX) * (x1 - cX);
            }

            var discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                if (x1 != x2)
                {
                    var t = 0.5d * -b / a;
                    return ((t >= 0d) && (t <= 1d), (t, y1 + m * (t - x1)), false, null);
                }
                else
                {
                    var t = 0.5d * -b / a;
                    return ((t >= 0d) && (t <= 1d), (x1, t), false, null);
                }
            }
            else if (discriminant > 0d)
            {
                if (x1 != x2)
                {
                    var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                    var t2 = (-b - Sqrt(discriminant)) / (2d * a);
                    return ((t1 >= 0d) && (t1 <= 1d), (t1, y1 + m * (t1 - x1)),
                            (t2 >= 0d) && (t2 <= 1d), (t2, y1 + m * (t2 - x1)));
                }
                else
                {
                    var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                    var t2 = (-b - Sqrt(discriminant)) / (2d * a);
                    return ((t1 >= 0d) && (t1 <= 1d), (x1, t1),
                            (t2 >= 0d) && (t2 <= 1d), (x2, t2));
                }
            }
            else
            {
                // no intersections
                return (false, null, false, null);
            }
        }

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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection UnrotatedEllipseLineSegmentIntersection2(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Calculate the quadratic parameters.
            var a = (u2 - u1) * (u2 - u1) / (rx * rx) + (v2 - v1) * (v2 - v1) / (ry * ry);
            var b = 2d * u1 * (u2 - u1) / (rx * rx) + 2d * v1 * (v2 - v1) / (ry * ry);
            var c = (u1 * u1) / (rx * rx) + (v1 * v1) / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the points if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.State = IntersectionState.Intersection;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                var t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineSegmentIntersection(
            double centerX, double centerY,
            double rx,
            double ry,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            Intersection result;
            var origin = new Vector2D(a1X, a1Y);
            var dir = new Vector2D(a1X, a1Y, a2X, a2Y);
            var diff = origin.Subtract(centerX, centerY);
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.I / (rx * rx), diff.J / (ry * ry));
            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1.0;
            var d = b * b - a * c;
            if (d < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (d > 0)
            {
                var root = Sqrt(d);
                var t_a = (-b - root) / a;
                var t_b = (-b + root) / a;
                if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
                {
                    if ((t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1))
                        result = new Intersection(IntersectionState.Outside);
                    else result = new Intersection(IntersectionState.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= t_a && t_a <= 1) result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_a));
                    if (0 <= t_b && t_b <= 1) result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_b));
                }
            }
            else
            {
                var t = -b / a; if (0 <= t && t <= 1)
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t));
                }
                else
                {
                    result = new Intersection(IntersectionState.Outside);
                }
            }

            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipseLineSegmentIntersection(
            double cx, double cy,
            double rx, double ry,
            double angle,
            double x0, double y0,
            double x1, double y1,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Get the Sine and Cosine of the angle.
            var sinA = Sin(angle);
            var cosA = Cos(angle);

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = (0 + (u1 * cosA - v1 * sinA));
            var v1A = (0 + (u1 * sinA + v1 * cosA));
            var u2A = (0 + (u2 * cosA - v2 * sinA));
            var v2A = (0 + (u2 * sinA + v2 * cosA));

            // Calculate the quadratic parameters.
            var a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            var b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            var c = (u1A * u1A) / (rx * rx) + (v1A * v1A) / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.State = IntersectionState.Intersection;
                }

            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                var t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }

                // ToDo: Figure out why the results are weird between 30 degrees and 5 degrees.
            }

            return result;
        }

        /// <summary>
        /// Finds the Intersection of a Ellipse and a Line
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static (bool, (double, double)?, bool, (double, double)?) Intersect(Ellipse ellipse, LineSegment line)
        {
            var slopeA = line.Slope();
            var slopeB = (line.A.Y - (slopeA * line.A.X));

            var a = (1 + (slopeA * slopeA));
            var b = ((2 * (slopeA * (slopeB - ellipse.Center.Y))) - (2d * ellipse.Center.X));
            var c = ((ellipse.Center.X * ellipse.Center.X) + (((slopeB - ellipse.Center.Y) * (slopeB - ellipse.Center.X)) - (ellipse.MajorRadius * ellipse.MajorRadius)));

            var xA = (((b * -1) + Sqrt(((b * b) - (a * c)))) / (2d * a));
            var xB = (((b - Sqrt(((b * b) - (a * c)))) * -1d) / (2d * a));
            var yA = ((slopeA * xA) + slopeB);
            var yB = ((slopeA * xB) + slopeB);

            return (true, (xA, yA), true, (xB, yB));
        }

        #endregion

        #region Intersection of Parabola and Hyperbola
        //http://csharphelper.com/blog/2014/11/see-where-a-parabola-and-hyperbola-intersect-in-c/
        #endregion

        #region Intersection of two Line Segments

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LineIntersection2DTests))]
        public static List<SpeedTester> LineIntersection2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => Intersection0(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection0)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection1(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection1)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection2(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection2)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection3(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection3)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection4(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection4)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection5(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection5)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => FindIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.FindIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineSegmentIntersection(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineSegmentIntersection)}(0, 0, 2, 2, 0, 2, 2, 0)")
            };

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns>Returns the point of intersection.</returns>
        public static (bool intersects, (double X, double Y)? point) Intersection0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaBAI = (x2 - x1);
            var deltaBAJ = (y2 - y1);
            var deltaDCI = (x4 - x3);
            var deltaDCJ = (y4 - y3);
            var deltaCAI = (x3 - x1);
            var deltaCAJ = (y3 - y1);

            //  If the segments are parallel return false.
            if (Abs((deltaDCI * deltaBAJ) - (deltaDCJ * deltaBAI)) < DoubleEpsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            var s = (((deltaBAI * deltaCAJ) + (deltaBAJ * -deltaCAI)) / ((deltaDCI * deltaBAJ) - (deltaDCJ * deltaBAI)));
            var t = (((deltaDCI * -deltaCAJ) + (deltaDCJ * deltaCAI)) / ((deltaDCJ * deltaBAI) - (deltaDCI * deltaBAJ)));

            return (
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                (x1 + (t * deltaBAI), y1 + (t * deltaBAJ)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/</remarks>
        public static (bool, (double X, double Y)?) Intersection1(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = (x1 - x2);
            var deltaAJ = (y2 - y1);
            var deltaBI = (y4 - y3);
            var deltaBJ = (x3 - x4);

            // Calculate the determinant of the vectors.
            var determinant = (deltaAJ * deltaBJ) - (deltaBI * deltaAI);

            // Check if the lines are parallel.
            if (Abs(determinant) < DoubleEpsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            var s = (deltaAJ * x1 + deltaAI * y1) / -determinant;
            var t = (deltaBI * x3 + deltaBJ * y3) / determinant;

            // Interpolate the point of intersection.
            return (
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                (-((deltaAI * t) + (deltaBJ * s)), (deltaAJ * t) + (deltaBI * s)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        public static (bool, (double X, double Y)?) Intersection2(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = (x2 - x1);
            var deltaAJ = (y2 - y1);
            var deltaBI = (x4 - x3);
            var deltaBJ = (y4 - y3);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (Abs(determinant) < DoubleEpsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            var s = ((x1 - x3) * deltaAJ + (y3 - y1) * deltaAI) / -determinant;
            var t = ((x3 - x1) * deltaBJ + (y1 - y3) * deltaBI) / determinant;

            return (
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                (x1 + t * deltaAI, y1 + t * deltaAJ));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/</remarks>
        public static (bool, (double X, double Y)?) Intersection3(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            var deltaAI = (x2 - x1);
            var deltaAJ = (y2 - y1);
            var deltaBI = (x4 - x3);
            var deltaBJ = (y4 - y3);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (deltaBI * deltaAJ) - (deltaBJ * deltaAI);

            // Check if the lines are parallel.
            if (Abs(determinant) < DoubleEpsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            var s = ((x3 - x1) * deltaAJ + (y1 - y3) * deltaAI) / -determinant;
            var t = ((x1 - x3) * deltaBJ + (y3 - y1) * deltaBI) / determinant;

            // Interpolate the point of intersection.
            return (
                // The segments intersect if t1 and t2 are between 0 and 1.
                (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                (x1 + t * deltaAI, y1 + t * deltaAJ));

            //// Find the closest points on the segments.
            //if (t < 0) t = 0;
            //else if (t > 1) t = 1;
            //if (s < 0) s = 0;
            //else if (s > 1) s = 1;

            //Point2D close_p1 = new Point2D(aX + deltaAI * t, aY + deltaAJ * t);
            //Point2D close_p2 = new Point2D(cX + deltaBI * s, cY + deltaBJ * s);
        }

        /// <summary>
        /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data
        /// type). Used in the Slope of a LineSeg
        /// </summary>
        /// <remarks></remarks>
        public const double SlopeMax = 9223372036854775807d;

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
        /// <remarks>http://www.gamedev.net/page/resources/_/technical/math-and-physics/fast-2d-line-intersection-algorithm-r423</remarks>
        public static (bool, (double X, double Y)?) Intersection4(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Compute the slopes of each line. Note the kludge for infinity, however, this will be close enough.
            var m1 = (Abs(x1 - x0) < DoubleEpsilon) ? SlopeMax : (y1 - y0) / (x1 - x0);
            var m2 = (Abs(x3 - x2) < DoubleEpsilon) ? SlopeMax : (y3 - y2) / (x3 - x2);

            // Check if the lines are parallel.
            if (Abs(m1 - m2) < DoubleEpsilon)
                return (false, null);

            // Compute the determinate of the coefficient matrix.
            var determinate = m2 - m1;

            var s = (y0 - (m1 * x0)) / determinate;
            var t = (y2 - (m2 * x2)) / -determinate;

            // Use Cramer's rule to compute the return values.
            return (
                (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                (s + t, m2 * s + m1 * t));
        }

        /// <summary>
        /// Returns the intersection of the two lines (line segments are passed in, but they are treated like infinite lines)
        /// </summary>
        /// <remarks>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Got this here:
        /// http://stackoverflow.com/questions/14480124/how-do-i-detect-triangle-and-rectangle-intersection
        /// </remarks>
        public static (bool, (double X, double Y)?) Intersection5(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            var direction1I = x1 - x0;
            var direction1J = y1 - y0;
            var direction2I = x3 - x2;
            var direction2J = y3 - y2;

            var dotPerp = (direction1I * direction2J) - (direction1J * direction2I);

            // Check if the lines are parallel.
            if (Abs(dotPerp) < DoubleEpsilon)
                return (false, null);

            // If it's 0, it means the lines are parallel so have infinite intersection points
            if (NearZero0(dotPerp))
                return (false, null);

            var cI = x2 - x0;
            var cJ = y2 - y0;
            var t = (cI * direction2J - cJ * direction2I) / dotPerp;
            //if ((t < 0) || (t > 1)) return null; // lies outside the line segment

            var u = (cI * direction1J - cJ * direction1I) / dotPerp;
            //if ((u < 0) || (u > 1)) return null; // lies outside the line segment

            //	Return the intersection point
            return (
                (t > 0) && (t < 1) && (u > 0) && (u < 1),
                (
                x0 + (t * direction1I),
                y0 + (t * direction1J)));
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
        private static (bool, (double X, double Y)?) FindIntersection(
            double X1, double Y1,
            double X2, double Y2,
            double A1, double B1,
            double A2, double B2)
        {
            var dx = X2 - X1;
            var dy = Y2 - Y1;
            var da = A2 - A1;
            var db = B2 - B1;

            // If the segments are parallel, return False.
            if (Abs(da * dy - db * dx) < Epsilon)
                return (false, null);

            // Find the point of intersection.
            var s = (dx * (B1 - Y1) + dy * (X1 - A1)) / (da * dy - db * dx);
            var t = (da * (Y1 - B1) + db * (A1 - X1)) / (db * dx - da * dy);

            return (true, (X1 + t * dx, Y1 + t * dy));
        }

        /// <summary>
        ///  Determines the intersection point of the line defined by points A and B with the
        ///  line defined by points C and D.
        ///
        ///  Returns YES if the intersection point was found, and stores that point in X,Y.
        ///  Returns NO if there is no determinable intersection point, in which case X,Y will
        ///  be unmodified.
        ///  /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/intersect/
        /// </remarks>
        public static (bool, (double X, double Y)?) LineIntersection(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            double distAB, theCos, theSin, newX, ABpos;

            //  Fail if either line is undefined.
            if (Ax == Bx && Ay == By || Cx == Dx && Cy == Dy) return (false, null);

            //  (1) Translate the system so that point A is on the origin.
            Bx -= Ax; By -= Ay;
            Cx -= Ax; Cy -= Ay;
            Dx -= Ax; Dy -= Ay;

            //  Discover the length of segment A-B.
            distAB = Sqrt(Bx * Bx + By * By);

            //  (2) Rotate the system so that point B is on the positive X axis.
            theCos = Bx / distAB;
            theSin = By / distAB;
            newX = Cx * theCos + Cy * theSin;
            Cy = Cy * theCos - Cx * theSin; Cx = newX;
            newX = Dx * theCos + Dy * theSin;
            Dy = Dy * theCos - Dx * theSin; Dx = newX;

            //  Fail if the lines are parallel.
            if (Cy == Dy) return (false, null);

            //  (3) Discover the position of the intersection point along line A-B.
            ABpos = Dx + (Cx - Dx) * Dy / (Dy - Cy);

            //  Success.
            //  (4) Apply the discovered position to line A-B in the original coordinate system.
            return (true, (Ax + ABpos * theCos, Ay + ABpos * theSin));
        }

        /// <summary>
        ///  Determines the intersection point of the line segment defined by points A and B
        ///  with the line segment defined by points C and D.
        ///
        ///  Returns YES if the intersection point was found, and stores that point in X,Y.
        ///  Returns NO if there is no determinable intersection point, in which case X,Y will
        ///  be unmodified.
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <param name="Dx"></param>
        /// <param name="Dy"></param>
        /// <returns></returns>
        /// <remarks>
        ///  public domain function by Darel Rex Finley, 2006
        ///  http://alienryderflex.com/intersect/
        /// </remarks>
        public static (bool, (double X, double Y)?) LineSegmentIntersection(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            double distAB, theCos, theSin, newX, ABpos;

            //  Fail if either line segment is zero-length.
            if (Ax == Bx && Ay == By || Cx == Dx && Cy == Dy) return (false, null);

            //  Fail if the segments share an end-point.
            if (Ax == Cx && Ay == Cy || Bx == Cx && By == Cy
           || Ax == Dx && Ay == Dy || Bx == Dx && By == Dy)
            {
                return (false, null);
            }

            //  (1) Translate the system so that point A is on the origin.
            Bx -= Ax; By -= Ay;
            Cx -= Ax; Cy -= Ay;
            Dx -= Ax; Dy -= Ay;

            //  Discover the length of segment A-B.
            distAB = Sqrt(Bx * Bx + By * By);

            //  (2) Rotate the system so that point B is on the positive X axis.
            theCos = Bx / distAB;
            theSin = By / distAB;
            newX = Cx * theCos + Cy * theSin;
            Cy = Cy * theCos - Cx * theSin; Cx = newX;
            newX = Dx * theCos + Dy * theSin;
            Dy = Dy * theCos - Dx * theSin; Dx = newX;

            //  Fail if segment C-D doesn't cross line A-B.
            if (Cy < 0d && Dy < 0d || Cy >= 0d && Dy >= 0d) return (false, null);

            //  (3) Discover the position of the intersection point along line A-B.
            ABpos = Dx + (Cx - Dx) * Dy / (Dy - Cy);

            //  Fail if segment C-D crosses line A-B outside of segment A-B.
            if (ABpos < 0d || ABpos > distAB) return (false, (Ax + ABpos * theCos, Ay + ABpos * theSin));

            //  Success.
            //  (4) Apply the discovered position to line A-B in the original coordinate system.
            return (true, (Ax + ABpos * theCos, Ay + ABpos * theSin));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-intersections
        /// </remarks>
        public static List<Point2D> IntersectLineLine(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy,
            double Dx, double Dy)
        {
            List<Point2D> result;

            var ua_t = (Dx - Cx) * (Ay - Cy) - (Dy - Cy) * (Ax - Cx);
            var ub_t = (Bx - Ax) * (Ay - Cy) - (By - Ay) * (Ax - Cx);
            var u_b = (Dy - Cy) * (Bx - Ax) - (Dx - Cx) * (By - Ay);

            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;

                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result = new List<Point2D>
                    {
                        new Point2D(
                            Ax + ua * (Bx - Ax),
                            Ay + ua * (By - Ay)
                        )
                    };
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

        #endregion

        #region Intersection of a Line and a Line Segment

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LineLineSegmentIntersection2DTests))]
        public static List<SpeedTester> LineLineSegmentIntersection2DTests()
            => new List<SpeedTester> {
                new SpeedTester(() => LineLineSegmentIntersection0(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection0)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineLineSegmentIntersection1(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection1)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => LineLineSegmentIntersection2(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.LineLineSegmentIntersection2)}(0, 0, 2, 2, 0, 2, 2, 0)"),
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://www.gamedev.net/topic/488904-lineline-segment-intersection-in-2d/
        /// </remarks>
        private static Intersection LineLineSegmentIntersection0(
            double aX, double aY,
            double bX, double bY, //Line
            double cX, double cY,
            double dX, double dY,  // Segment
            double epsilon = Epsilon)
        {
            //test for parallel case
            var denom = (dY - cY) * (bX - aX) - (dX - cX) * (bY - aY);
            if (Abs(denom) < epsilon)
                return new Intersection(IntersectionState.NoIntersection);

            //calculate segment parameter and ensure its within bounds
            var t = ((bX - aX) * (aY - cY) - (bY - aY) * (aX - cX)) / denom;
            if (t < 0d || t > 1d)
                return new Intersection(IntersectionState.NoIntersection);

            //store actual intersection
            var result = new Intersection(IntersectionState.Intersection);
            result.AppendPoint(new Point2D(cX + (dX - cX) * t, cY + (dY - cY) * t));
            return result;

        }

        /// <summary>
        /// Find the intersection point between two line segments.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineLineSegmentIntersection1(
                double a1X, double a1Y,
                double a2X, double a2Y,
                double b1X, double b1Y,
                double b2X, double b2Y)
        {
            Intersection result;
            var ua_t = (b2X - b1X) * (a1Y - b1Y) - (b2Y - b1Y) * (a1X - b1X);
            var ub_t = (a2X - a1X) * (a1Y - b1Y) - (a2Y - a1Y) * (a1X - b1X);
            var u_b = (b2Y - b1Y) * (a2X - a1X) - (b2X - b1X) * (a2Y - a1Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (//true
                    //0 <= ua && ua <= 1
                    //&&
                    0 <= ub && ub <= 1
                    )
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(a1X + ua * (a2X - a1X), a1Y + ua * (a2Y - a1Y)));
                    //result.AppendPoint(new Point2D(b1X + ub * (b2X - b1X), b1Y + ub * (b2Y - b1Y)));
                }
                else
                {
                    result = new Intersection(IntersectionState.NoIntersection);
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionState.Coincident | IntersectionState.Parallel | IntersectionState.Intersection);
                    result.AppendPoints(new List<Point2D> { new Point2D(b1X, b1Y), new Point2D(b2X, b2Y) });
                }
                else
                {
                    result = new Intersection(IntersectionState.Parallel | IntersectionState.NoIntersection);
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection point between a line and a line segment.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineLineSegmentIntersection2(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate lines to origin.
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel or coincident.
            if (Abs(determinant) < Epsilon)
                return result;

            // Find the index where the intersection point lies on the line.
            //var s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            var t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            if (t >= 0d && t <= 1d)
            {
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));
            }

            return result;
        }

        #endregion

        #region Intersection of a Line Segment and a Cubic Bezier

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
        /// This method has an error where it does not return an intersection with a horizontal line and the end points of the curve share the same y value, as well as the handles sharing another y value.
        /// Found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Based on code now found at: http://www.abecedarical.com/javascript/script_cubic.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierLineSegmentIntersection1(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new Intersection(IntersectionState.NoIntersection);

            var A = l1y - l0y;      //A=y2-y1
            var B = l0x - l1x;      //B=x1-x2
            var C = l0x * (l0y - l1y) + l0y * (l1x - l0x);  //C=x1*(y1-y2)+y1*(x2-x1)

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            var r = CubicRoots(
                A * bx.A + B * by.A,    /*t^3*/
                A * bx.B + B * by.B,    /*t^2*/
                A * bx.C + B * by.C,    /*t*/
                A * bx.D + B * by.D + C /*1*/
                );

            /*verify the roots are in bounds of the linear segment*/
            for (var i = 0; i < 3; i++)
            {
                var t = r[i];

                var x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                var y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

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
                {
                    /*intersection point*/
                    I.AppendPoint(new Point2D(x, y));
                    I.State = IntersectionState.Intersection;
                }
            }
            return I;
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
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            double cl;
            Vector2D n;
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p1X, p1Y).Scale(-1);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = new Vector2D(p3X, p3Y).Scale(-3);
            d = a.Add(b.Add(c.Add(new Vector2D(p4X, p4Y))));
            c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1X, p1Y);
            n = new Vector2D(a1Y - a2Y, a2X - a1X);
            cl = a1X * a2Y - a2X * a1Y;
            var roots = new Polynomial(
                n.DotProduct(c0) + cl,
                n.DotProduct(c1),
                n.DotProduct(c2),
                n.DotProduct(c3)
                ).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    var p5 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    var p6 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    var p7 = Lerp(p3X, p3Y, p4X, p4Y, t);
                    var p8 = Lerp(p5.X, p5.Y, p6.X, p6.Y, t);
                    var p9 = Lerp(p6.X, p6.Y, p7.X, p7.Y, t);
                    var p10 = Lerp(p8.X, p8.Y, p9.X, p9.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (p10.GreaterThanOrEqual(min) && p10.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Intersection of a Line Segment and a Quadratic Bezier

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierLineSegmentIntersection1(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var intersections = new Intersection(IntersectionState.NoIntersection);

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
            var roots = new List<double>();
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
                        Interpolators.Linear(Interpolators.Linear(p1X, p2X, t), Interpolators.Linear(p2X, p3X, t), t),
                        Interpolators.Linear(Interpolators.Linear(p1Y, p2Y, t), Interpolators.Linear(p2Y, p3Y, t), t));
                    var x = point.X;
                    var y = point.Y;
                    var result = new Intersection(IntersectionState.Intersection);
                    // bounds checks
                    if (a1X == a2X && y >= minY && y <= maxY)
                    {
                        // vertical line
                        intersections.AppendPoint(point);
                    }
                    else if (a1Y == a2Y && x >= minX && x <= maxX)
                    {
                        // horizontal line
                        intersections.AppendPoint(point);
                    }
                    else if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                    {
                        // line passed bounds check
                        intersections.AppendPoint(point);
                    }
                }
            }
            return intersections;
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
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(p2X, p2Y).Scale(-2);
            var c2 = new Vector2D(p1X, p1Y).Add(a.Add(new Vector2D(p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            var b = new Vector2D(p2X, p2Y).Scale(2);
            var c1 = a.Add(b);
            var c0 = new Point2D(p1X, p1Y);
            var n = new Point2D(a1Y - a2Y, a2X - a1X);
            var cl = a1X * a2Y - a2X * a1Y;
            var roots = new Polynomial(
                n.DotProduct(c0) + cl,
                n.DotProduct(c1),
                n.DotProduct(c2)).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    Point2D p4 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    Point2D p5 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    Point2D p6 = Lerp(p4.X, p4.Y, p5.X, p5.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (p6.GreaterThanOrEqual(min) && p6.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p6);
                    }
                }
            }
            return result;
        }

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
        public static bool IsConvex(Contour polygon)
        {
            var got_negative = false;
            var got_positive = false;
            var num_points = polygon.Points.Count;
            int B, C;
            for (var A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                var cross_product = CrossProductVector(
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

        #endregion

        #region Is Valid

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsValidTests))]
        public static List<SpeedTester> IsValidTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsValid(double.NaN),
                $"{nameof(Experiments.IsValid)}({double.NaN})"),
                new SpeedTester(() => IsValid1(double.NaN),
                $"{nameof(Experiments.IsValid1)}({double.NaN})"),
            };

        /// <summary>
        /// Make sure that a double number is not a NaN or infinity.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, returns false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(double value)
            => !double.IsNaN(value) && !double.IsInfinity(value);

        /// <summary>
        /// This function is used to ensure that a floating point number is
        /// not a NaN or infinity.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>
        /// 	<c>true</c> if the specified x is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid1(double x)
        {
            if (double.IsNaN(x))
            {
                // NaN.
                return false;
            }

            return !double.IsInfinity(x);
        }

        #endregion

        #region Linear Interpolation of Two 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate1DTests))]
        public static List<SpeedTester> LinearInterpolate1DTests() => new List<SpeedTester> {
                new SpeedTester(() => LinearInterpolate1D_0(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_0)}(0, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate1D_1(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_1)}(0, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate1D_2(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_2)}(0, 1, 0.5d)")
            };

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_0(
            double v1, double v2, double t)
            => (1 - t) * v1 + t * v2;

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_1(
            double v1, double v2, double t)
            => v1 + t * (v2 - v1);

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_2(
            double v1, double v2, double t)
            => (Abs(v1 - v2) < DoubleEpsilon) ? 0 : v1 - ((1 / (v1 - v2)) * t);

        #endregion

        #region Linear Interpolation of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate2DTests))]
        public static List<SpeedTester> LinearInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => LinearInterpolate2D_0(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_0)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_1(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_1)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_2(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_2)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_3(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_3)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static (double X, double Y) LinearInterpolate2D_0(
            double x1, double y1,
            double x2, double y2,
            double t)
            => (
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2);

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y) LinearInterpolate2D_1(
            double x1, double y1,
            double x2, double y2,
            double t)
            => (
                x1 + t * (x2 - x1),
                y1 + t * (y2 - y1));

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y) LinearInterpolate2D_2(
            double x1, double y1,
            double x2, double y2,
            double t)
            => (
                LinearInterpolate1D_0(x1, x2, t),
                LinearInterpolate1D_0(y1, y2, t));

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) LinearInterpolate2D_3(
            double x1, double y1,
            double x2, double y2,
            double t)
            => (
                (Abs(x1 - x2) < DoubleEpsilon) ? 0 : x1 - ((1 / (x1 - x2)) * t),
                (Abs(y1 - y2) < DoubleEpsilon) ? 0 : y1 - ((1 / (y1 - y2)) * t));

        #endregion

        #region Linear Interpolation of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate3DTests))]
        public static List<SpeedTester> LinearInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => LinearInterpolate3D_0(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_0)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_1(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_1)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_2(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_2)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_3(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_3)}(0, 0, 0, 1, 1, 1, 0.5d)")
            };

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static (double X, double Y, double Z) LinearInterpolate3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => ((1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2,
                (1 - t) * z1 + t * z2);

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y, double Z) LinearInterpolate3D_1(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => (
                x1 + t * (x2 - x1),
                y1 + t * (y2 - y1),
                z1 + t * (z2 - z1));

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double X, double Y, double Z) LinearInterpolate3D_2(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => (
                LinearInterpolate1D_0(x1, x2, t),
                LinearInterpolate1D_0(y1, y2, t),
                LinearInterpolate1D_0(z1, z2, t));

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) LinearInterpolate3D_3(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => (
                (Abs(x1 - x2) < DoubleEpsilon) ? 0 : x1 - ((1 / (x1 - x2)) * t),
                (Abs(y1 - y2) < DoubleEpsilon) ? 0 : y1 - ((1 / (y1 - y2)) * t),
                (Abs(z1 - z2) < DoubleEpsilon) ? 0 : z1 - ((1 / (z1 - z2)) * t));

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
            var UnitVectorAB = new Vector2D(Value1, Value2);
            Vector2D PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5).Scale(Offset);
            return Interpolators.Linear(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }

        #endregion

        #region Line in Polyline

        /// <summary>
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool LineInPolygon(Point2D start, Point2D end, Contour polygon)
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
            var dist = Sqrt(end.X * end.X + end.Y * end.Y);
            var theCos = end.X / dist;
            var theSin = end.Y / dist;
            for (i = 0; i < polygon.Points.Count; i++)
            {
                j = i + 1;
                if (j == polygon.Points.Count)
                    j = 0;

                sX = polygon.Points[i].X - start.X;
                sY = polygon.Points[i].Y - start.Y;
                eX = polygon.Points[j].X - start.X;
                eY = polygon.Points[j].Y - start.Y;
                if (Abs(sX) < DoubleEpsilon
                    && Abs(sY) < DoubleEpsilon
                    && Abs(eX - end.X) < DoubleEpsilon
                    && Abs(eY - end.Y) < DoubleEpsilon
                    || Abs(eX) < DoubleEpsilon
                    && Abs(eY) < DoubleEpsilon
                    && Abs(sX - end.X) < DoubleEpsilon
                    && Abs(sY - end.Y) < DoubleEpsilon)
                {
                    return true;
                }

                rotSX = sX * theCos + sY * theSin;
                rotSY = sY * theCos - sX * theSin;
                rotEX = eX * theCos + eY * theSin;
                rotEY = eY * theCos - eX * theSin;
                if (rotSY < 0d && rotEY > 0d
                || rotEY < 0d && rotSY > 0d)
                {
                    crossX = rotSX + (rotEX - rotSX) * (0d - rotSY) / (rotEY - rotSY);
                    if (crossX >= 0.0 && crossX <= dist)
                        return false;
                }

                if (Abs(rotSY) < DoubleEpsilon
                    && Abs(rotEY) < DoubleEpsilon
                    && (rotSX >= 0d || rotEX >= 0d)
                    && (rotSX <= dist || rotEX <= dist)
                    && (rotSX < 0d || rotEX < 0d
                    || rotSX > dist || rotEX > dist))
                {
                    return false;
                }
            }

            return polygon.Contains(new Point2D(start.X + end.X * 0.5d, start.Y + end.Y * 0.5d));
        }

        #endregion

        #region Line in Polyline set

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
        public static bool LineInPolygonSet(Polygon allPolys, Point2D start, Point2D end)
        {
            double theCos, theSin, dist, sX, sY, eX, eY, rotSX, rotSY, rotEX, rotEY, crossX;
            int i, j, polyI;

            end.X -= start.X;
            end.Y -= start.Y;
            dist = Sqrt(end.X * end.X + end.Y * end.Y);
            theCos = end.X / dist;
            theSin = end.Y / dist;

            for (polyI = 0; polyI < allPolys.Count; polyI++)
            {
                for (i = 0; i < allPolys.Contours[polyI].Points.Count; i++)
                {
                    j = i + 1;
                    if (j == allPolys.Contours[polyI].Points.Count)
                        j = 0;

                    sX = allPolys.Contours[polyI].Points[i].X - start.X;
                    sY = allPolys.Contours[polyI].Points[i].Y - start.Y;
                    eX = allPolys.Contours[polyI].Points[j].X - start.X;
                    eY = allPolys.Contours[polyI].Points[j].Y - start.Y;
                    if (Abs(sX) < DoubleEpsilon
                        && Abs(sY) < DoubleEpsilon
                        && Abs(eX - end.X) < DoubleEpsilon
                        && Abs(eY - end.Y) < DoubleEpsilon
                        || Abs(eX) < DoubleEpsilon
                        && Abs(eY) < DoubleEpsilon
                        && Abs(sX - end.X) < DoubleEpsilon
                        && Abs(sY - end.Y) < DoubleEpsilon)
                    {
                        return true;
                    }

                    rotSX = sX * theCos + sY * theSin;
                    rotSY = sY * theCos - sX * theSin;
                    rotEX = eX * theCos + eY * theSin;
                    rotEY = eY * theCos - eX * theSin;
                    if (rotSY < 0d && rotEY > 0d

                    || rotEY < 0d && rotSY > 0d)
                    {
                        crossX = rotSX + (rotEX - rotSX) * (0d - rotSY) / (rotEY - rotSY);
                        if (crossX >= 0d && crossX <= dist)
                            return false;
                    }

                    if (Abs(rotSY) < DoubleEpsilon
                        && Abs(rotEY) < DoubleEpsilon
                        && (rotSX >= 0d || rotEX >= 0d)
                        && (rotSX <= dist || rotEX <= dist)
                        && (rotSX < 0d || rotEX < 0d
                        || rotSX > dist || rotEX > dist))
                    {
                        return false;
                    }
                }
            }

            return allPolys.Contains(new Point2D(start.X + end.X * 0.5d, start.Y + end.Y * 0.5d));
        }

        #endregion

        #region Line Overlap

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        public static bool GetOverlap(double a1, double a2, double b1, double b2, out double Left, out double Right)
        {
            if (a1 < a2)
            {
                if (b1 < b2) { Left = Max(a1, b1); Right = Min(a2, b2); }
                else { Left = Max(a1, b2); Right = Min(a2, b1); }
            }
            else
            {
                if (b1 < b2) { Left = Max(a2, b1); Right = Min(a1, b2); }
                else { Left = Max(a2, b2); Right = Min(a1, b1); }
            }
            return Left < Right;
        }

        #endregion

        #region List Interpolation Points of Cubic Bezier

        /// <summary>
        ///
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolatePoints(CubicBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                var v = (1d / count) * i;
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
        public static List<Point2D> InterpolateCubicBeizerPoints(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints(bezier.A, bezier.B, bezier.C, bezier.D, count);

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
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> ComputeBezierInterpolations(CubicBezier bezier, int count) => ComputeBezierInterpolations(bezier.A, bezier.B, bezier.C, bezier.D, count);

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
            var curve = new List<Point2D>();
            double t = 0;
            var dt = (1.0d / (numberOfPoints - 1));
            for (var i = 0; (i <= numberOfPoints); i++)
            {
                t += dt;
                curve.Add(new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t)));
            }
            return curve;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints0(bezier.A, bezier.B, bezier.C, bezier.D, count);

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
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; (Index <= 1); Index = (Index + Precision))
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        #endregion

        #region List Interpolation points of Quadratic Bezier

        /// <summary>
        ///
        /// </summary>
        /// <param name="bezier"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateQuadraticBezierPoints(QuadraticBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                double v = (1f / count) * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        #endregion

        #region Log2

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Log2Tests))]
        public static List<SpeedTester> Log2Tests()
            => new List<SpeedTester> {
                new SpeedTester(() => Log(12, 2),
                $"{nameof(Math.Log)}(12, 2)"),
                new SpeedTester(() => Log2(12),
                $"{nameof(Experiments.Log2)}(12)"),
                new SpeedTester(() => Log2_1(12),
                $"{nameof(Experiments.Log2_1)}(12)")
            };

        /// <summary>
        /// Determine the position of the highest one-bit in a number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Log2(int a)
        {
            byte bits = 0;
            while (a != 0)
            {
                ++bits;
                a >>= 1;
            };

            return bits;
        }

        // Source: http://graphics.stanford.edu/~seander/bithacks.html
        private static readonly byte[] multiplyDeBruijnBitPosition = new byte[32]
        {
            0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31
        };

        /// <summary>
        /// Returns log2(x) for positive values of x.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2_1(int x)
        {
            if (x <= 0) throw new ArgumentOutOfRangeException(nameof(x), "must be positive");
            if (x == 1) return 0;

            // Locate the highest set bit.
            var v = unchecked((uint)x);
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;

            var i = unchecked(v * 0x7c4acdd) >> 27;
            int r = multiplyDeBruijnBitPosition[i];

            return r;
        }

        /// <summary>
        /// Returns log2(x) for positive values of x.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2_1(uint x)
        {
            if (x <= 0) throw new ArgumentOutOfRangeException(nameof(x), "must be positive");
            if (x == 1) return 0;

            // Locate the highest set bit.
            var v = unchecked(x);
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;

            var i = unchecked(v * 0x7c4acdd) >> 27;
            int r = multiplyDeBruijnBitPosition[i];

            return r;
        }

        #endregion

        #region Mixed product of Three 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the mixed product for three 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(MixedProduct3D_0Tests))]
        public static List<SpeedTester> MixedProduct3D_0Tests() => new List<SpeedTester> {
                new SpeedTester(() => MixedProduct3D_0(0, 0,0, 1, 1, 1,2,2,2),
                $"{nameof(Experiments.MixedProduct3D_0)}(0, 0, 1, 1, 0.5d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <returns></returns>
        public static double MixedProduct3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3)
            => DotProduct(CrossProduct2Points3D_0(x1, y1, z1, x2, y2, z2), x3, y3, z3);

        #endregion

        #region Near Zero Inquiry

        /// <summary>
        ///
        /// </summary>
        public const double NearZeroEpsilon = 1E-20;

        /// <summary>
        /// Set of tests to run testing methods that query whether a number is near zero.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(NearZeroTests))]
        public static List<SpeedTester> NearZeroTests() => new List<SpeedTester> {
                new SpeedTester(() => NearZero0(0.000000001d),
                $"{nameof(Experiments.NearZero0)}(0.000000001d)"),
                new SpeedTester(() => NearZero1(0.000000001d),
                $"{nameof(Experiments.NearZero1)}(0.000000001d)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero0(double value, double epsilon = NearZeroEpsilon)
            => (value > -epsilon) && (value < -epsilon);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool NearZero1(double value, double epsilon = NearZeroEpsilon)
            => Abs(value) <= epsilon;

        #endregion

        #region Normalize a 2D Vector

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize(
            double i, double j)
            => (
                i / Sqrt((i * i) + (j * j)),
                j / Sqrt((i * i) + (j * j))
                );

        #endregion

        #region Normalize a 3D Vector

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize(
            double i, double j, double k)
            => (
                i / Sqrt((i * i) + (j * j) + (k * k)),
                j / Sqrt((i * i) + (j * j) + (k * k)),
                k / Sqrt((i * i) + (j * j) + (k * k))
                );

        #endregion

        #region Normalize the Vector Between Two 2D Points

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize(
            double x1, double y1,
            double x2, double y2)
            => (
                x1 / Sqrt(((x1 * x2) + (y1 * y2))),
                y1 / Sqrt(((x1 * x2) + (y1 * y2)))
                );

        #endregion

        #region Normalize the Vector Between Two 3D Points

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="z1">The z component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <param name="z2">The z component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks>http://www.fundza.com/vectors/normalize/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                y1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                z1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2))
                );

        #endregion

        #region N Polygon Star

        // Draw the stars.
        private void PicCanvas_Paint(PaintEventArgs e, int NumPoints, Rectangle bounds, bool chkHalfOnly, bool chkRelPrimeOnly)
        {
            if (NumPoints < 3)
                return;

            // Get the radii.
            int r1, r2, r3;
            r3 = Min(bounds.Width, bounds.Height) / 2;
            r1 = r3 / 2;
            r2 = r3 / 4;
            r3 = r1 + r2;

            // Position variables.
            var cx = bounds.Width / 2;
            var cy = bounds.Height / 2;

            // Position the original points.
            var pts1 = new PointF[NumPoints];
            var pts2 = new PointF[NumPoints];
            var theta = -PI / 2;
            var dtheta = 2 * PI / NumPoints;
            for (var i = 0; i < NumPoints; i++)
            {
                pts1[i].X = (float)(r1 * Cos(theta));
                pts1[i].Y = (float)(r1 * Sin(theta));
                pts2[i].X = (float)(r2 * Cos(theta));
                pts2[i].Y = (float)(r2 * Sin(theta));
                theta += dtheta;
            }

            // Draw stars.
            var max = NumPoints - 1;
            if (chkHalfOnly)
                max = NumPoints / 2;
            for (var skip = 1; skip <= max; skip++)
            {
                // See if they are relatively prime.
                var draw_all = !chkRelPrimeOnly;
                if (draw_all || GCD(skip, NumPoints) == 1)
                {
                    // Draw the big version of the star.
                    DrawStar(e.Graphics, cx, cy, pts1, skip, NumPoints);

                    // Draw the smaller version.
                    theta = -PI / 2 + skip * 2 * PI / NumPoints;
                    var x = (int)(cx + r3 * Cos(theta));
                    var y = (int)(cy + r3 * Sin(theta));

                    DrawStar(e.Graphics, x, y, pts2, skip, NumPoints);
                }
            }
        }

        // Return the greatest common divisor (GCD) of a and b.
        private long GCD(long a, long b)
        {
            long remainder;

            // Pull out remainders.
            for (;;)
            {
                remainder = a % b;
                if (remainder == 0)
                    break;
                a = b;
                b = remainder;
            }

            return b;
        }

        // Draw a star centered at (x, y) using this skip value.
        private void DrawStar(Graphics gr, int x, int y, PointF[] orig_pts, int skip, int NumPoints)
        {
            // Make a PointF array with the points in the proper order.
            var pts = new PointF[NumPoints];
            for (var i = 0; i < NumPoints; i++)
                pts[i] = orig_pts[(i * skip) % NumPoints];

            // Draw the star.
            gr.TranslateTransform(x, y);
            gr.DrawPolygon(Pens.Blue, pts);
            gr.ResetTransform();
        }

        #endregion

        #region N Polygon Intersecting Star

        // Return PointFs to define a non-intersecting star.
        private PointF[] NonIntersectingStarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            var pts = new PointF[2 * num_points];

            double rx1 = bounds.Width / 2;
            double ry1 = bounds.Height / 2;
            var rx2 = rx1 * 0.5;
            var ry2 = ry1 * 0.5;
            var cx = bounds.X + rx1;
            var cy = bounds.Y + ry1;

            // Start at the top.
            var theta = -PI / 2;
            var dtheta = PI / num_points;
            for (var i = 0; i < 2 * num_points; i += 2)
            {
                pts[i] = new PointF(
                    (float)(cx + rx1 * Cos(theta)),
                    (float)(cy + ry1 * Sin(theta)));
                theta += dtheta;

                pts[i + 1] = new PointF(
                    (float)(cx + rx2 * Cos(theta)),
                    (float)(cy + ry2 * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }

        #endregion

        #region Offset Line

        /// <summary>
        /// Calculate the geometry of points offset at a specified distance. aka Double Line.
        /// </summary>
        /// <param name="pointA">First reference point.</param>
        /// <param name="pointB">First inclusive point.</param>
        /// <param name="pointC">Second inclusive point.</param>
        /// <param name="pointD">Second reference point.</param>
        /// <param name="offsetDistance">Offset Distance</param>
        /// <returns></returns>
        /// <remarks>
        /// Suppose you have 4 points; A, B C, and D. With Lines AB, BC, and CD.<BR/>
        ///<BR/>
        ///                 B1         BC1       C1<BR/>
        ///                   |\¯B¯¯¯¯¯BC¯¯¯C¯¯¯/|<BR/>
        ///                   | \--------------/ |<BR/>
        ///                   | |\____________/| |<BR/>
        ///                   | | |B2  BC2 C2| | |<BR/>
        ///                 AB| | |          | | |CD<BR/>
        ///                AB1| | |AB2    CD2| | |CD1<BR/>
        ///                   | | |          | | |<BR/>
        ///                   | | |          | | |<BR/>
        ///               A1  A  A2      D2  D  D1<BR/>
        ///</remarks>
        public static Point2D[] CenteredOffsetLinePoints(Point2D pointA, Point2D pointB, Point2D pointC, Point2D pointD, double offsetDistance)
        {
            // To get the vectors of the angles at each corner B and C, Normalize the Unit Delta Vectors along AB, BC, and CD.
            Vector2D UnitVectorAB = pointB.Subtract(pointA).Unit();
            Vector2D UnitVectorBC = pointC.Subtract(pointB).Unit();
            Vector2D UnitVectorCD = pointD.Subtract(pointC).Unit();

            //  Find the Perpendicular of the outside vectors
            Vector2D PerpendicularAB = UnitVectorAB.Perpendicular();
            Vector2D PerpendicularCD = UnitVectorCD.Perpendicular();

            //  Normalized Vectors pointing out from B and C.
            Vector2D OutUnitVectorB = (UnitVectorAB - UnitVectorBC).Unit();
            Vector2D OutUnitVectorC = (UnitVectorCD - UnitVectorBC).Unit();

            //  The distance out from B is the radius / Cos(theta) where theta is the angle
            //  from the perpendicular of BC of the UnitVector. The cosine can also be
            //  calculated by doing the dot product of  Unit(Perpendicular(AB)) and
            //  UnitVector.
            var BPointScale = PerpendicularAB.DotProduct(OutUnitVectorB) * offsetDistance;
            var CPointScale = PerpendicularCD.DotProduct(OutUnitVectorC) * offsetDistance;

            OutUnitVectorB = OutUnitVectorB.Scale(CPointScale);
            OutUnitVectorC = OutUnitVectorC.Scale(BPointScale);

            // Corners of the parallelogram to draw
            var Out = new Point2D[] {
                (pointC + OutUnitVectorC),
                (pointB + OutUnitVectorB),
                (pointB - OutUnitVectorB),
                (pointC - OutUnitVectorC),
                (pointC + OutUnitVectorC)
            };
            return Out;
        }

        #endregion

        #region Operation Addition Safe

        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsAdditionSafeTests))]
        public static List<SpeedTester> IsAdditionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsAdditionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe2(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe2)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe3(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe3)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe4(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe4)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe5(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe5)}(int.MaxValue / 2, int.MaxValue / 2)"),
                new SpeedTester(() => IsAdditionSafe6(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsAdditionSafe6)}(int.MaxValue / 2, int.MaxValue / 2)")
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsAdditionSafe(int a, int b)
            => (Log2(a) < sizeof(int) && Log2(b) < sizeof(int));

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name="a"></param>
        /// <param name=""></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsAdditionSafe2(int a, int b)
        {
            var L_Mask = int.MaxValue;
            L_Mask >>= 1;
            L_Mask = ~L_Mask;

            a &= L_Mask;
            b &= L_Mask;

            return (a == 0 || b == 0 || a == -0 || b == -0);
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1&lq=1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe3(int a, int b)
        {
            if (a > 0) return b > (int.MaxValue - a);
            if (a < 0) return b > (int.MinValue + a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1&lq=1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe4(int a, int b)
            => a < 0 != b < 0 || (a < 0
            ? b > int.MinValue - a
            : b < int.MaxValue - a);

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long?noredirect=1&lq=1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe5(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (int.MinValue - a);
            if (a > 0) return b <= (int.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe6(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (b > 0) return (a > int.MaxValue - b);
            if (b < 0) return (a < int.MinValue - b);
            return true;
        }

        #endregion

        #region Operation Division Safe

        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsDivisionSafeTests))]
        public static List<SpeedTester> IsDivisionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsDivisionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsDivisionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1</remarks>
        public static bool IsDivisionSafe(int a, int b)
        {
            if (b == 0) return false;
            //for division(except for the INT_MIN and - 1 special case) there is no possibility of going over INT_MIN or INT_MAX.
            if (a == int.MinValue && b == -1) return false;
            return true;
        }

        #endregion

        #region Operation Exponentiation Safe

        /// <summary>
        /// Set of tests to run testing methods that calculate the safty of operations.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsExponentiationSafeTests))]
        public static List<SpeedTester> IsExponentiationSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsExponentiationSafe(2, 39),
                $"{nameof(Experiments.IsExponentiationSafe)}(2, 39)")
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsExponentiationSafe(int a, int b)
            => (Log2(a) * b <= sizeof(int));

        #endregion

        #region Operation Multiplication Safe

        /// <summary>
        /// Set of tests to run testing methods that calculate the safety of operations.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsMultiplicationSafeTests))]
        public static List<SpeedTester> IsMultiplicationSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsMultiplicationSafe(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe0(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe0)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe1(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe1)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe2(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe2)}(2, int.MaxValue / 2)"),
                new SpeedTester(() => IsMultiplicationSafe3(2, int.MaxValue / 2),
                $"{nameof(Experiments.IsMultiplicationSafe3)}(2, int.MaxValue / 2)")
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsMultiplicationSafe(int a, int b)
            => (Log2(a) + Log2(b) <= sizeof(int));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsMultiplicationSafe0(uint a, uint b)
            => (Log2_1(a) + Log2_1(b) <= sizeof(uint));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsMultiplicationSafe1(uint a, uint b)
            => (Math.Round(Log(a, 2) + Log(b, 2), MidpointRounding.AwayFromZero) <= sizeof(uint));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c</remarks>
        public static bool IsMultiplicationSafe2(int a, int b)
        {
            if (a == 0) return true;
            // a * b would overflow
            return (b > int.MaxValue / a);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1</remarks>
        public static bool IsMultiplicationSafe3(int a, int b)
        {
            if (a == 0) return true;
            if (a > int.MaxValue / b) return false /* `a * x` would overflow */;
            if ((a < int.MinValue / b)) return false /* `a * x` would underflow */;
            // there may be need to check for -1 for two's complement machines
            if ((a == -1) && (b == int.MinValue)) return false /* `a * x` can overflow */;
            if ((b == -1) && (a == int.MinValue)) return false /* `a * x` (or `a / x`) can overflow */;
            return true;
        }

        #endregion

        #region Operation Subtraction Safe

        /// <summary>
        /// Set of tests to run testing methods that calculate the safety of operations.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(IsSubtractionSafeTests))]
        public static List<SpeedTester> IsSubtractionSafeTests()
            => new List<SpeedTester> {
                new SpeedTester(() => IsSubtractionSafe(int.MaxValue / 2, int.MaxValue / 2),
                $"{nameof(Experiments.IsSubtractionSafe)}(int.MaxValue / 2, int.MaxValue / 2)"),
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c?rq=1</remarks>
        public static bool IsSubtractionSafe(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (b < 0) return (a > int.MaxValue + b);
            if (b > 0) return (a < int.MinValue + b);
            return true;
        }

        #endregion

        #region Orient Polygon Clockwise

        /// <summary>
        /// If the polygon is oriented counterclockwise,
        /// reverse the order of its points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void OrientPolygonClockwise(Contour polygon)
        {
            if (polygon.Orientation == RotationDirections.CounterClockwise)
                polygon.Points.Reverse();
        }

        #endregion

        #region Perimeter of a Polygon

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Perimeter2DTests))]
        public static List<SpeedTester> Perimeter2DTests() => new List<SpeedTester> {
                new SpeedTester(() => Perimeter0(new List<(double X, double Y)> {(0,0), (1,0), (0,1)}),
                $"{nameof(Experiments.Perimeter0)}((x, y){{(0,0),(1,0),(0,1)}})"),
                new SpeedTester(() => Perimeter1(new List<(double X, double Y)> {(0,0), (1,0), (0,1)}),
                $"{nameof(Experiments.Perimeter1)}((x, y){{(0,0),(1,0),(0,1)}})")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static double Perimeter0(List<(double X, double Y)> points)
        {
            (double X, double Y) last = points[0];
            double dist = 0;
            foreach (var cur in points.Skip(1))
            {
                dist += Distance2D_0(last.X, last.Y, cur.X, cur.Y);
                last = cur;
            }
            return dist;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/2227828/find-the-distance-required-to-navigate-a-list-of-points-using-linq</remarks>
        public static double Perimeter1(List<(double X, double Y)> points)
            => points.Zip(points.Skip(1), Distance2D_1).Sum();

        #endregion

        #region Perimeter Polygon of a Polygon

        //  Determine the radian angle of the specified point (as it relates to the
        //  origin).
        //
        //  Warning:  Do not pass zero in both parameters, as this will cause a division-
        //            by-zero.
        public static double AngleOf(double x, double y)
        {
            var dist = Sqrt(x * x + y * y);
            if (y >= 0d) return Acos(x / dist);
            else return Acos(-x / dist) + PI;
        }

        /// <summary>
        ///  Returns the perimeter polygon.
        ///  If for any reason the procedure fails, it will return null.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon_perimeter/
        /// </remarks>
        public static List<(double X, double Y)> PolygonPerimeter(List<(double X, double Y)> points)
        {
            var corners = points.Count;

            const int MAX_SEGS = 1000;

            var segS = new List<(double X, double Y)>();
            var segE = new List<(double X, double Y)>();
            var segAngle = new List<double>();
            var segRet = new List<(double X, double Y)>();
            (double X, double Y)? intersect;
            var start = points[0];
            var lastAngle = PI;
            var j = (corners) - 1;
            var segs = 0;

            if ((corners) > MAX_SEGS) return null;

            //  1,3.  Reformulate the polygon as a set of line segments, and choose a
            //        starting point that must be on the perimeter.
            for (var i = 0; i < (corners); i++)
            {
                if (points[i].X != points[j].X || points[i].Y != points[j].Y)
                {
                    segS[segs] = (points[i].X, points[i].Y);
                    segE[segs] = (points[j].X, points[j].Y);
                }
                j = i;
                if (points[i].Y > start.Y || points[i].Y == start.Y && points[i].X < start.X)
                {
                    start.X = points[i].X;
                    start.Y = points[i].Y;
                }
            }
            if (segs == 0) return null;

            //  2.  Break the segments up at their intersection points.
            for (var i = 0; i < segs - 1; i++)
            {
                for (j = i + 1; j < segs; j++)
                {
                    var intersection = Intersection0(
                    segS[i].X, segS[i].Y, segE[i].X, segE[i].Y,
                    segS[j].X, segS[j].Y, segE[j].X, segE[j].Y);
                    intersect = intersection.point;
                    if (intersection.intersects)
                    {
                        if ((intersect?.X != segS[i].X || intersect?.Y != segS[i].Y)
                        && (intersect?.X != segE[i].X || intersect?.Y != segE[i].Y))
                        {
                            if (segs == MAX_SEGS) return null;
                            segS[segs] = (segS[i].X, segS[i].Y);
                            segE[segs] = ((double)intersect?.X, (double)intersect?.Y);
                            segS[i] = ((double)intersect?.X, (double)intersect?.Y);
                        }
                        if ((intersect?.X != segS[j].X || intersect?.Y != segS[j].Y)
                        && (intersect?.X != segE[j].X || intersect?.Y != segE[j].Y))
                        {
                            if (segs == MAX_SEGS) return null;
                            segS[segs] = (segS[j].X, segS[j].Y);
                            segE[segs] = ((double)intersect?.X, (double)intersect?.Y);
                            segS[j] = ((double)intersect?.X, (double)intersect?.Y);
                        }
                    }
                }
            }

            //  Calculate the angle of each segment.
            for (var i = 0; i < segs; i++)
                segAngle[i] = AngleOf(segE[i].X - segS[i].X, segE[i].Y - segS[i].Y);

            //  4.  Build the perimeter polygon.
            var c = start.X;
            var d = start.Y;
            var a = c - 1d;
            var b = d;
            double e = 0;
            double f = 0;

            double angleDif = 0;
            var bestAngleDif = Tau;

            segRet.Add((c, d));
            corners = 1;
            while (true)
            {
                bestAngleDif = Tau;
                for (var i = 0; i < segs; i++)
                {
                    if (segS[i].X == c && segS[i].Y == d && (segE[i].X != a || segE[i].Y != b))
                    {
                        angleDif = lastAngle - segAngle[i];
                        while (angleDif >= Tau) angleDif -= Tau;
                        while (angleDif < 0) angleDif += Tau;
                        if (angleDif < bestAngleDif)
                        {
                            bestAngleDif = angleDif; e = segE[i].X; f = segE[i].Y;
                        }
                    }
                    if (segE[i].X == c && segE[i].Y == d && (segS[i].X != a || segS[i].Y != b))
                    {
                        angleDif = lastAngle - segAngle[i] + .5 * Tau;
                        while (angleDif >= Tau) angleDif -= Tau;
                        while (angleDif < 0) angleDif += Tau;
                        if (angleDif < bestAngleDif)
                        {
                            bestAngleDif = angleDif;
                            e = segS[i].X;
                            f = segS[i].Y;
                        }
                    }
                }
                if ((corners) > 1
                    && c == segRet[0].X
                    && d == segRet[0].Y
                    && e == segRet[1].X
                    && f == segRet[1].Y)
                {
                    (corners)--;
                    return segRet;
                }
                if (bestAngleDif == Tau || (corners) == MAX_SEGS) return null;
                lastAngle -= bestAngleDif + .5 * Tau;
                segRet[(corners)++] = (e, f);
                a = c;
                b = d;
                c = e;
                d = f;
            }
        }

        #endregion

        #region Perpendicular Vector in the Clockwise Direction

        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularClockwise(double i, double j)
            => (-j, i);

        #endregion

        #region Perpendicular Vector in the Counter Clockwise Direction

        /// <summary>
        /// Find the Counter Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularCounterClockwise(double i, double j)
            => (j, -i);

        #endregion

        #region Point in Circle

        /// <summary>
        /// Set of tests to run testing methods that calculate whether a point is within a circle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(PointInCircle2DTests))]
        public static List<SpeedTester> PointInCircle2DTests() => new List<SpeedTester> {
                new SpeedTester(() => PointInCircle(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircle)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircle(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircle)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleInline(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleInline)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleInline(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleInline)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCirclePhilcolbourn(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCirclePhilcolbourn)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCirclePhilcolbourn(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCirclePhilcolbourn)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleNPhilcolbourn(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleNPhilcolbourn)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleNPhilcolbourn(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleNPhilcolbourn)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleWilliamMorrison(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleWilliamMorrison)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleWilliamMorrison(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleWilliamMorrison)}(0, 0, 2, 3, 3)"),
                new SpeedTester(() => PointInCircleX(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircleX)}(0, 0, 2, 1, 1)"),
                new SpeedTester(() => PointInCircleX(0, 0, 2, 3, 3),
                $"{nameof(Experiments.PointInCircleX)}(0, 0, 2, 3, 3)")
            };

        /// <summary>
        /// Find out if a Point is in a Circle.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Inclusion PointInCircle(
            double centerX, double centerY,
            double radius,
            double x, double y)
        {
            var distance = Distance2D_0(centerX, centerY, x, y);
            return (radius >= distance) ? ((Abs(radius - distance) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Find out if a Point is in a Circle.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Inclusion PointInCircleInline(
            double centerX, double centerY,
            double radius,
            double x, double y)
        {
            var distance = Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
            return (radius >= distance) ? ((Abs(radius - distance) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static Inclusion PointInCirclePhilcolbourn(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            var dx = Abs(x - centerX);
            if (dx > radius)
                return Inclusion.Outside;
            var dy = Abs(y - centerY);
            if (dy > radius)
                return Inclusion.Outside;
            //if (dx + dy <= radius) return InsideOutside.Inside;
            var distanceSquared = dx * dx + dy * dy;
            var radiusSquared = radius * radius;
            return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static Inclusion PointInCircleNPhilcolbourn(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            var dx = Abs(x - centerX);
            var dy = Abs(y - centerY);
            var distanceSquared = dx * dx + dy * dy;
            var radiusSquared = radius * radius;
            return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// test if coordinate (x, y) is within a radius from coordinate (centerX, centerY)
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle</remarks>
        public static Inclusion PointInCircleWilliamMorrison(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            if (x >= centerX - radius && x <= centerX + radius
                && y >= centerY - radius && y <= centerY + radius)
            {
                var dx = centerX - x;
                var dy = centerY - y;
                dx *= dx;
                dy *= dy;
                var distanceSquared = dx + dy;
                var radiusSquared = radius * radius;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
            }
            return Inclusion.Outside;
        }

        /// <summary>
        /// test if coordinate (x, y) is within a radius from coordinate (centerX, centerY)
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="radius"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Inclusion PointInCircleX(
            double centerX,
            double centerY,
            double radius,
            double x,
            double y)
        {
            if (x >= centerX - radius && x <= centerX + radius
                && y >= centerY - radius && y <= centerY + radius)
            {
                var dx = ((centerX > x) ? (x - centerX) : (centerX - x));
                var dy = ((centerY > y) ? (y - centerY) : (centerY - y));
                if (dx > radius || dy > radius)
                    return Inclusion.Outside;
                dx *= dx;
                dy *= dy;
                var distanceSquared = dx + dy;
                var radiusSquared = radius * radius;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
            }
            return Inclusion.Outside;
        }

        #endregion

        #region Point in Ellipse

        /// <summary>
        /// Checks whether a point is found within the boundaries of an ellipse.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [DebuggerStepThrough]
        public static Inclusion PointInEllipse(Ellipse ellipse, Point2D point)
        {
            if (ellipse.RX <= 0d || ellipse.RY <= 0d)
                return Inclusion.Outside;

            var cosT = Cos(-ellipse.Angle);
            var sinT = Sin(-ellipse.Angle);

            var u = point.X - ellipse.Center.X;
            var v = point.Y - ellipse.Center.Y;

            var a = (cosT * u + sinT * v) * (cosT * u + sinT * v);
            var b = (sinT * u - cosT * v) * (sinT * u - cosT * v);

            var d1Squared = 4 * ellipse.RX * ellipse.RX;
            var d2Squared = 4 * ellipse.RY * ellipse.RY;

            var normalizedRadius = (a / d1Squared)
                                    + (b / d2Squared);

            return (normalizedRadius <= 1d) ? ((Abs(normalizedRadius - 1d) < DoubleEpsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        #endregion

        #region Point in Ellipse Unrotated

        /// <summary>
        ///
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool UnrotatedEllipseContainsPoint(Ellipse ellipse, Point2D point)
        {
            if (ellipse.RX <= 0d || ellipse.RY <= 0d)
                return false;

            var u = point.X - ellipse.Center.X;
            var v = point.Y - ellipse.Center.Y;

            var a = u * u;
            var b = u * u;

            var d1Squared = ellipse.RX * ellipse.RX;
            var d2Squared = ellipse.RY * ellipse.RY;

            return (a / d1Squared)
                 + (b / d2Squared) <= 1d;
        }

        #endregion

        #region Point in Polygon Contour

        /// <summary>
        /// Set of tests to run testing methods that test whether a point is contained in a polygon contour.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(PointInPolygonContourTests))]
        public static List<SpeedTester> PointInPolygonContourTests()
        {
            var polygon = new List<PointF> {
                new PointF(0, 0),
                new PointF(2, 0),
                new PointF(0, 2) };
            var PatrickMullenValues = PrecalcPointInPolygonContourPatrickMullenValues(polygon);
            var point = new PointF(1, 1);
            return new List<SpeedTester> {
                //new SpeedTester(() => PointInPolygonContourDarelRexFinley(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourDarelRexFinley)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourNathanMercer(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourNathanMercer)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourLaschaLagidse(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourLaschaLagidse)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourPatrickMullen(polygon, point, PatrickMullenValues.Value.Item1, PatrickMullenValues.Value.Item2),
                //$"{nameof(Experiments.PointInPolygonContourPatrickMullen)}(polygon, {point}, constant, multiple)"),
                //new SpeedTester(() => PointInPolygonContourMeowNET(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourMeowNET)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourAlienRyderFlex(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourAlienRyderFlex)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourLaschaLagidse2(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourLaschaLagidse2)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourGilKr(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourGilKr)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourMKatzWRandolphFranklin(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourMKatzWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourRodStephens(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourRodStephens)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourSaeedAmiri(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourSaeedAmiri)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourKeith(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourKeith)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourJerryKnauss(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourJerryKnauss)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourJerryKnauss2(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourJerryKnauss2)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourPaulBourke(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourPaulBourke)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourWRandolphFranklin(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourPhilippeReverdy(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourPhilippeReverdy)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonContourBobStein(polygon, point),
                //$"{nameof(Experiments.PointInPolygonContourBobStein)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonContourHormannAgathosSimplified(polygon, point),
                $"{nameof(Experiments.PointInPolygonContourHormannAgathosSimplified)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonContourHormannAgathosExpanded3(polygon, point),
                $"{nameof(Experiments.PointInPolygonContourHormannAgathosExpanded3)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonContourHormannAgathosExpanded(polygon, point),
                $"{nameof(Experiments.PointInPolygonContourHormannAgathosExpanded)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonContourHormannAgathosExpanded2(polygon, point),
                $"{nameof(Experiments.PointInPolygonContourHormannAgathosExpanded2)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonContourHormannAgathosNewFor(polygon, point),
                $"{nameof(Experiments.PointInPolygonContourHormannAgathosNewFor)}(polygon, {point})"),
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://paulbourke.net/geometry/polygonmesh/contains.txt
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourJerryKnauss(
            List<PointF> polygon, PointF point)
        {
            var result = false;

            for (var i = 0; i < polygon.Count - 1; i++)
            {
                if ((((polygon[i + 1].Y < point.Y) && (point.Y < polygon[i].Y))
                    || ((polygon[i].Y < point.Y) && (point.Y < polygon[i + 1].Y)))
                    && (point.X < (polygon[i].X - polygon[i + 1].X)
                    * (point.Y - polygon[i + 1].Y)
                    / (polygon[i].Y - polygon[i + 1].Y) + polygon[i + 1].X))
                {
                    result = !result;
                }
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://paulbourke.net/geometry/polygonmesh/contains.txt
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourJerryKnauss2(
            List<PointF> polygon, PointF point)
        {
            var j = polygon.Count - 1;
            var result = false;

            for (var i = 0; i < polygon.Count; i++)
            {
                if (((
                    (polygon[j].Y < point.Y)
                    && (point.Y < polygon[i].Y))
                    || ((polygon[i].Y < point.Y)
                    && (point.Y < polygon[j].Y)))
                    && (point.X < (polygon[i].X - polygon[j].X)
                    * (point.Y - polygon[j].Y)
                    / (polygon[i].Y - polygon[j].Y) + polygon[j].X))
                {
                    result = !result;
                }

                j = i;
            }
            return result;
        }

        /// <summary>
        /// The function will return true if the point x,y is inside the polygon, or
        /// false if it is not.  If the point is exactly on the edge of the polygon,
        /// then the function may return true or false.
        /// </summary>
        /// <param name="point">point to be tested</param>
        /// <param name="polygon">coordinates of corners</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourDarelRexFinley(
            List<PointF> polygon, PointF point)
        {
            var j = polygon.Count - 1;
            var oddNodes = false;

            for (var i = 0; i < polygon.Count; i++)
            {
                if (
                    polygon[i].Y < point.Y
                    && polygon[j].Y >= point.Y
                    || polygon[j].Y < point.Y
                    && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// The function will return true if the point x,y is inside the polygon, or
        /// false if it is not.  If the point is exactly on the edge of the polygon,
        /// then the function may return true or false.
        /// </summary>
        /// <param name="point">point to be tested</param>
        /// <param name="polygon">coordinates of corners</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourNathanMercer(
            List<PointF> polygon, PointF point)
        {
            var j = polygon.Count - 1;
            var oddNodes = false;

            for (var i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }

                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourLaschaLagidse(
            List<PointF> polygon, PointF point)
        {
            int i;
            var j = polygon.Count - 1;
            var oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if ((polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    oddNodes ^= (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X);
                }

                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        ///  USAGE:
        ///  Call precalc_values() to initialize the constant[] and multiple[] arrays,
        ///  then call pointInPolygon(x, y) to determine if the point is in the polygon.
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon">coordinates of corners</param>
        /// <param name="constant">storage for pre-calculated constants (same size as polyX)</param>
        /// <param name="multiple">storage for pre-calculated multipliers (same size as polyX)</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourPatrickMullen(
            List<PointF> polygon, PointF point,
            List<double> constant, List<double> multiple)
        {
            int i, j = polygon.Count - 1;
            var oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    oddNodes ^= (point.Y * multiple[i] + constant[i] < point.X);
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon">coordinates of corners</param>
        ///// <param name="constant">storage for pre-calculated constants (same size as polyX)</param>
        ///// <param name="multiple">storage for pre-calculated multipliers (same size as polyX)</param>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (List<double>, List<double>)? PrecalcPointInPolygonContourPatrickMullenValues(
            List<PointF> polygon)
        {
            if (polygon == null)
                return null;

            var constant = new double[polygon.Count];
            var multiple = new double[polygon.Count];

            int i, j = polygon.Count - 1;

            for (i = 0; i < polygon.Count; i++)
            {
                if (Abs(polygon[j].Y - polygon[i].Y) < DoubleEpsilon)
                {
                    constant[i] = polygon[i].X;
                    multiple[i] = 0;
                }
                else
                {
                    constant[i] = polygon[i].X - (polygon[i].Y * polygon[j].X)
                        / (polygon[j].Y - polygon[i].Y) + (polygon[i].Y * polygon[i].X)
                        / (polygon[j].Y - polygon[i].Y);
                    multiple[i] = (polygon[j].X - polygon[i].X) / (polygon[j].Y - polygon[i].Y);
                }
                j = i;
            }

            return (new List<double>(constant), new List<double>(multiple));
        }

        /// <summary>
        /// Determines if the given point is inside the polygon contour.
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="point">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourMeowNET(
            List<PointF> polygon, PointF point)
        {
            var result = false;
            var j = polygon.Count - 1;
            for (var i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X)
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
        ///  int    polygon.Count  =  how many corners the polygon has (no repeats)
        ///  double  polyX[]      =  horizontal coordinates of corners
        ///  double  polyY[]      =  vertical coordinates of corners
        ///  double  x, y         =  point to be tested
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourAlienRyderFlex(
            List<PointF> polygon, PointF point)
        {
            int i;
            var j = polygon.Count - 1;
            var oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X)
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
        ///  int    polygon.Count  =  how many corners the polygon has (no repeats)
        ///  double  polyX[]      =  horizontal coordinates of corners
        ///  double  polyY[]      =  vertical coordinates of corners
        ///  double  x, y         =  point to be tested
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourLaschaLagidse2(
            List<PointF> polygon, PointF point)
        {
            int i;
            var j = polygon.Count - 1;
            var oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                if ((polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    oddNodes ^= (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourGilKr(
            List<PointF> polygon, PointF point)
        {
            var nvert = polygon.Count;
            var c = false;
            for (int i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y))
                 && (point.X < (polygon[j].X - polygon[i].X)
                 * (point.Y - polygon[i].Y)
                 / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    c = !c;
                }
            }
            return c;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
        /// http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourMKatzWRandolphFranklin(
            List<PointF> polygon, PointF point)
        {
            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (var i = 1; i < polygon.Count; i++)
            {
                PointF q = polygon[i];
                minX = Min(q.X, minX);
                maxX = Max(q.X, maxX);
                minY = Min(q.Y, minY);
                maxY = Max(q.Y, maxY);
            }

            if (point.X < minX || point.X > maxX || point.Y < minY || point.Y > maxY)
                return false;

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            var inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)
                     && point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y)
                     / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
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
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://astronomy.swin.edu.au/pbourke/geometry/
        /// http://www.eecs.umich.edu/courses/eecs380/HANDOUTS/PROJ2/InsidePoly.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourPaulBourke(
            List<PointF> polygon, PointF point)
        {
            PointF p1, p2;
            var counter = 0;
            int i;
            var N = polygon.Count;
            double xinters;
            p1 = polygon[0];
            for (i = 1; i <= N; i++)
            {
                p2 = polygon[i % N];
                if (point.Y > Min(p1.Y, p2.Y))
                {
                    if (point.Y <= Max(p1.Y, p2.Y))
                    {
                        if (point.X <= Max(p1.X, p2.X))
                        {
                            if (Abs(p1.Y - p2.Y) > DoubleEpsilon)
                            {
                                xinters = (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                                if (Abs(p1.X - p2.X) < DoubleEpsilon || point.X <= xinters)
                                    counter++;
                            }
                        }
                    }
                }
                p1 = p2;
            }

            return (counter % 2 != 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>https://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourWRandolphFranklin(
            List<PointF> polygon, PointF point)
        {
            var inside = false;
            var nvert = polygon.Count;
            for (int i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y))
                 && (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y)
                 / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
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
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourSaeedAmiri(
            List<PointF> polygon, PointF point)
        {
            var coef = polygon.Skip(1).Select((p, i) =>
                  (p.X - polygon[i].X) * (point.Y - polygon[i].Y)
                - (p.Y - polygon[i].Y) * (point.X - polygon[i].X)
                ).ToList();

            if (coef.Any(p => Abs(p) < DoubleEpsilon))
                return true;

            for (var i = 1; i < coef.Count; i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/geometry/polygonmesh/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourPhilippeReverdy(
            List<PointF> polygon, PointF point)
        {
            int i;
            double angle = 0;
            var p1 = new PointF();
            var p2 = new PointF();
            var n = polygon.Count;
            for (i = 0; i < n; i++)
            {
                p1.X = polygon[i].X - point.X;
                p1.Y = polygon[i].Y - point.Y;
                p2.X = polygon[(i + 1) % n].X - point.X;
                p2.Y = polygon[(i + 1) % n].Y - point.Y;
                angle += Angle2D(p1.X, p1.Y, p2.X, p2.Y);
            }

            return !(Abs(angle) < PI);
        }

        /// <summary>
        /// Return the angle between two vectors on a plane
        /// The angle is from vector 1 to vector 2, positive anticlockwise
        /// The result is between -pi -> pi
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle2D(double x1, double y1, double x2, double y2)
        {
            double dtheta, theta1, theta2;

            theta1 = Atan2(y1, x1);
            theta2 = Atan2(y2, x2);
            dtheta = theta2 - theta1;
            while (dtheta > PI)
                dtheta -= (PI * 2);
            while (dtheta < -PI)
                dtheta += (PI * 2);

            return (dtheta);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns>Return true if the point is in the polygon.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourRodStephens(
            List<PointF> polygon, PointF point)
        {
            // Get the angle between the point and the
            // first and last vertices.
            var max_point = polygon.Count - 1;
            var total_angle = AngleVector_0(
                polygon[max_point].X, polygon[max_point].Y,
                point.X, point.Y,
                polygon[0].X, polygon[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (var i = 0; i < max_point; i++)
            {
                total_angle += AngleVector_0(
                    polygon[i].X, polygon[i].Y,
                    point.X, point.Y,
                    polygon[i + 1].X, polygon[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Abs(total_angle) > 0.000001);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourKeith(
            List<PointF> polygon, PointF point)
        {
            PointF p1, p2;

            var inside = false;

            if (polygon.Count < 3)
                return inside;

            PointF oldPoint = polygon[polygon.Count - 1];

            for (var i = 0; i < polygon.Count; i++)
            {
                PointF newPoint = polygon[i];

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
        /// is target point inside a 2D polygon?
        /// </summary>
        /// <param name="polygon">polygon points</param>
        /// <param name="point">target point</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInPolygonContourBobStein(
            List<PointF> polygon, PointF point)
        {
            double xnew, ynew;
            double xold, yold;
            double x1, y1;
            double x2, y2;
            int i;
            var inside = false;
            var npoints = polygon.Count;
            if (npoints < 3)
                return (false);
            xold = polygon[npoints - 1].X;
            yold = polygon[npoints - 1].Y;
            for (i = 0; i < npoints; i++)
            {
                xnew = polygon[i].X;
                ynew = polygon[i].Y;
                if (xnew > xold)
                {
                    x1 = xold;
                    x2 = xnew;
                    y1 = yold;
                    y2 = ynew;
                }
                else
                {
                    x1 = xnew;
                    x2 = xold;
                    y1 = ynew;
                    y2 = yold;
                }
                if ((xnew < point.X) == (point.X <= xold)          /* edge "open" at one end */
                 && ((long)point.Y - (long)y1) * (long)(x2 - x1)
                  < ((long)point.Y - (long)y1) * (long)(point.X - x1))
                {
                    inside = !inside;
                }
                xold = xnew;
                yold = ynew;
            }
            return (inside);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://angusj.com/delphi/clipper.php</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PointInPolygonContourHormannAgathosExpanded(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            Inclusion result = Inclusion.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior.
            if (polygon.Count < 3)
                return Inclusion.Outside;
            PointF curPoint = polygon[0];
            for (var i = 1; i <= polygon.Count; ++i)
            {
                PointF nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                if (Abs(nextPoint.Y - point.Y) < DoubleEpsilon)
                {
                    if ((Abs(nextPoint.X - point.X) < DoubleEpsilon) || (Abs(curPoint.Y - point.Y) < DoubleEpsilon && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return Inclusion.Boundary;
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
                            if (Abs(determinant) < DoubleEpsilon)
                                return Inclusion.Boundary;
                            if ((determinant > 0d) == (nextPoint.Y > curPoint.Y))
                                result = 1 - result;
                        }
                    }
                    else if (nextPoint.X > point.X)
                    {
                        double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                        if (Abs(determinant) < DoubleEpsilon)
                            return Inclusion.Boundary;
                        if ((determinant > 0d) == (nextPoint.Y > curPoint.Y))
                            result = 1 - result;
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://angusj.com/delphi/clipper.php</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PointInPolygonContourHormannAgathosExpanded2(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            Inclusion result = Inclusion.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior.
            if (polygon.Count < 3)
                return Inclusion.Outside;
            PointF curPoint = polygon[0];
            for (var i = 1; i <= polygon.Count; ++i)
            {
                PointF nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                // Horizontal line special case.
                // Is end point horizontal to test point?
                if ((Abs(nextPoint.Y - point.Y) < DoubleEpsilon)
                    // And is end point vertical to test point?
                    && ((Abs(nextPoint.X - point.X) < DoubleEpsilon)
                    // Or is start point horizontal to test point?
                    // And is test point between end and start 
                    || (Abs(curPoint.Y - point.Y) < DoubleEpsilon && ((nextPoint.X > point.X) == (curPoint.X < point.X)))))
                {
                    return Inclusion.Boundary;
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
                            if (Abs(determinant) < DoubleEpsilon)
                                return Inclusion.Boundary;
                            if ((determinant > 0d) == (nextPoint.Y > curPoint.Y))
                                result = 1 - result;
                        }
                    }
                    else if (nextPoint.X > point.X)
                    {
                        double determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                        if (Abs(determinant) < DoubleEpsilon)
                            return Inclusion.Boundary;
                        if ((determinant > 0d) == (nextPoint.Y > curPoint.Y))
                            result = 1 - result;
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://angusj.com/delphi/clipper.php</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PointInPolygonContourHormannAgathosSimplified(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            Inclusion result = Inclusion.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior.
            if (polygon.Count < 3)
                return Inclusion.Outside;
            PointF curPoint = polygon[0];
            PointF nextPoint = polygon[1];
            for (var i = 1; i <= polygon.Count; ++i)
            {
                nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                if (Abs(nextPoint.Y - point.Y) < DoubleEpsilon)
                {
                    if ((Abs(nextPoint.X - point.X) < DoubleEpsilon)
                        || (Abs(curPoint.Y - point.Y) < DoubleEpsilon
                        && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return Inclusion.Boundary;
                    }
                }

                if ((curPoint.Y < point.Y) != (nextPoint.Y < point.Y))
                {
                    if (curPoint.X >= point.X && nextPoint.X > point.X)
                    {
                        result = 1 - result;
                    }
                    else if (curPoint.X >= point.X && nextPoint.X <= point.X || curPoint.X < point.X && nextPoint.X > point.X)
                    {
                        var determinant = (curPoint.X - point.X) * (nextPoint.Y - point.Y) - (nextPoint.X - point.X) * (curPoint.Y - point.Y);
                        if (Abs(determinant) < Epsilon)
                            return Inclusion.Boundary;
                        else if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                            result = 1 - result;
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://angusj.com/delphi/clipper.php</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PointInPolygonContourHormannAgathosExpanded3(List<PointF> polygon, PointF point)
        {
            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            Inclusion result = Inclusion.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior.
            if (polygon.Count < 3)
                return Inclusion.Outside;
            PointF curPoint = polygon[0];
            for (var i = 1; i <= polygon.Count; ++i)
            {
                PointF nextPoint = (i == polygon.Count ? polygon[0] : polygon[i]);
                if (Abs(nextPoint.Y - point.Y) < DoubleEpsilon)
                {
                    if ((Abs(nextPoint.X - point.X) < DoubleEpsilon)
                        || (Abs(curPoint.Y - point.Y) < DoubleEpsilon
                        && ((nextPoint.X > point.X) == (curPoint.X < point.X))))
                    {
                        return Inclusion.Boundary;
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
                            result = process(nextPoint.X, nextPoint.Y);
                            if (result == Inclusion.Boundary) return result;
                        }
                    }
                    else if (nextPoint.X > point.X)
                    {
                        result = process(nextPoint.X, nextPoint.Y);
                        if (result == Inclusion.Boundary) return result;
                    }
                }

                curPoint = nextPoint;
            }

            Inclusion process(double nextPointX, double nextPointY)
            {
                var determinant = (curPoint.X - point.X) * (nextPointY - point.Y) - (nextPointX - point.X) * (curPoint.Y - point.Y);
                if (Abs(determinant) < DoubleEpsilon)
                    return Inclusion.Boundary;
                if ((determinant > 0d) == (nextPointY > curPoint.Y))
                    return 1 - result;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Contour"/>.
        /// </summary>
        /// <param name="points">The points that form the corners of the polygon.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns>
        /// Returns Outside (0) if false, Inside (+1) if true, Boundary (-1) if the point is on a polygon boundary.
        /// </returns>
        /// <remarks>
        /// Adapted from Clipper library: http://www.angusj.com/delphi/clipper.php
        /// See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann and Agathos
        /// http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PointInPolygonContourHormannAgathosNewFor(
            List<PointF> points,
            PointF p,
            double epsilon = Epsilon)
        {
            // Default value is no inclusion.
            Inclusion result = Inclusion.Outside;

            // Special cases for points and line segments.
            if (points.Count < 3)
                if (points.Count == 1)
                    // If the polygon has 1 point, it is a point and has no interior, but a point can intersect a point.
                    return (p.X == points[0].X && p.Y == points[0].Y) ? Inclusion.Boundary : Inclusion.Outside;
                else if (points.Count == 2)
                    // If the polygon has 2 points, it is a line and has no interior, but a point can intersect a line.
                    return ((p.X == points[0].X) && (p.Y == points[0].Y))
                        || ((p.X == points[1].X) && (p.Y == points[1].Y))
                        || (((p.X > points[0].X) == (p.X < points[1].X))
                        && ((p.Y > points[0].Y) == (p.Y < points[1].Y))
                        && ((p.X - points[0].X) * (points[1].Y - points[0].Y) == (p.Y - points[0].Y) * (points[1].X - points[0].X))) ? Inclusion.Boundary : Inclusion.Outside;
                else
                    // Empty geometry.
                    return Inclusion.Outside;

            // Loop through each line segment.
            PointF curPoint;
            PointF nextPoint;
            for (int i = points.Count - 1, j = 0; j < points.Count; i = j++)
            {
                curPoint = points[i];
                nextPoint = points[j];
                // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                if (((nextPoint.Y == p.Y)) && (((nextPoint.X == p.X)) || ((curPoint.Y == p.Y) && ((nextPoint.X > p.X) == (curPoint.X < p.X)))))
                //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                {
                    return Inclusion.Boundary;
                }

                // If Point between start and end points horizontally.
                //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                if ((curPoint.Y < p.Y) != (nextPoint.Y < p.Y))
                {
                    // If point between start and end points vertically.
                    if (curPoint.X >= p.X)
                    {
                        if (nextPoint.X > p.X)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            var determinant = (curPoint.X - p.X) * (nextPoint.Y - p.Y) - (nextPoint.X - p.X) * (curPoint.Y - p.Y);
                            if (Abs(determinant) < epsilon)
                                return Inclusion.Boundary;
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                                result = 1 - result;
                        }
                    }
                    else if (nextPoint.X > p.X)
                    {
                        var determinant = (curPoint.X - p.X) * (nextPoint.Y - p.Y) - (nextPoint.X - p.X) * (curPoint.Y - p.Y);
                        if (Abs(determinant) < epsilon)
                            return Inclusion.Boundary;
                        if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                            result = 1 - result;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Point in Polygon set

        /// <summary>
        /// This function automatically knows that enclosed polygons are "no-go" areas.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        public static bool PointInPolygonSet(Polygon polygons, Point2D point)
        {
            var oddNodes = false;
            int j;

            for (var polyI = 0; polyI < polygons.Count; polyI++)
            {
                for (var i = 0; i < polygons.Contours[polyI].Points.Count; i++)
                {
                    j = i + 1;
                    if (j == polygons.Contours[polyI].Points.Count)
                        j = 0;
                    if (polygons.Contours[polyI].Points[i].Y < point.Y
                    && polygons.Contours[polyI].Points[j].Y >= point.Y
                    || polygons.Contours[polyI].Points[j].Y < point.Y
                    && polygons.Contours[polyI].Points[i].Y >= point.Y)
                    {
                        if (polygons.Contours[polyI].Points[i].X + (point.Y - polygons.Contours[polyI].Points[i].Y)
                        / (polygons.Contours[polyI].Points[j].Y - polygons.Contours[polyI].Points[i].Y)
                        * (polygons.Contours[polyI].Points[j].X - polygons.Contours[polyI].Points[i].X) < point.X)
                        {
                            oddNodes = !oddNodes;
                        }
                    }
                }
            }

            return oddNodes;
        }

        /// <summary>
        /// This function automatically knows that enclosed polygons are "no-go" areas.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Inclusion PointInPolygonSetShkyrockett(List<List<PointF>> polygons, PointF point)
        {
            Inclusion returnValue = Inclusion.Outside;

            foreach (List<PointF> poly in polygons)
            {
                // Use alternating rule with XOR to determine if the point is in a polygon or a hole.
                // If the point is in an odd number of polygons, it is inside. If even, it is a hole.
                returnValue ^= PointInPolygonContourHormannAgathosExpanded(poly, point);

                // Any point on any boundary is on a boundary.
                if (returnValue == Inclusion.Boundary)
                    return Inclusion.Boundary;
            }

            return returnValue;
        }

        #endregion

        #region Point in Rectangle

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion Contains(Rectangle2D rectangle, Point2D point) => (rectangle.X <= point.X
            && point.X < rectangle.X + rectangle.Width
            && rectangle.Y <= point.Y
            && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion Contains2(Rectangle2D rectangle, Point2D point)
        {
            if (((Abs(rectangle.X - point.X) < DoubleEpsilon
                || Abs(rectangle.Bottom - point.X) < DoubleEpsilon)
                && ((rectangle.Y <= point.Y) == (rectangle.Bottom >= point.Y)))
             || ((Abs(rectangle.Right - point.Y) < DoubleEpsilon
             || Abs(rectangle.Left - point.Y) < DoubleEpsilon)
             && ((rectangle.X <= point.X) == (rectangle.Right >= point.X))))
            {
                return Inclusion.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        public static Inclusion PointOnRectangleX(Rectangle2D rectangle, Point2D point)
        {
            var top = Sqrt((rectangle.TopRight.X - rectangle.TopLeft.X) * (rectangle.TopRight.X - rectangle.TopLeft.X) + (rectangle.TopRight.Y - rectangle.TopLeft.Y) * (rectangle.TopRight.Y - rectangle.TopLeft.Y));
            var right = Sqrt((rectangle.BottomRight.X - rectangle.TopRight.X) * (rectangle.BottomRight.X - rectangle.TopRight.X) + (rectangle.BottomRight.Y - rectangle.TopRight.Y) * (rectangle.BottomRight.Y - rectangle.TopRight.Y));
            var tlp = (point.X - rectangle.TopLeft.X) * (point.X - rectangle.TopLeft.X) + (point.Y - rectangle.TopLeft.Y) * (point.Y - rectangle.TopLeft.Y);
            var trp = (point.X - rectangle.TopRight.X) * (point.X - rectangle.TopRight.X) + (point.Y - rectangle.TopRight.Y) * (point.Y - rectangle.TopRight.Y);
            var brp = (point.X - rectangle.BottomRight.X) * (point.X - rectangle.BottomRight.X) + (point.Y - rectangle.BottomRight.Y) * (point.Y - rectangle.BottomRight.Y);
            var blp = (point.X - rectangle.BottomLeft.X) * (point.X - rectangle.BottomLeft.X) + (point.Y - rectangle.BottomLeft.Y) * (point.Y - rectangle.BottomLeft.Y);

            if (Abs(top - Sqrt(tlp - trp)) < DoubleEpsilon
                || Abs(right - Sqrt(trp - brp)) < DoubleEpsilon
                || Abs(top - Sqrt(brp - blp)) < DoubleEpsilon
                || Abs(right - Sqrt(blp - tlp)) < DoubleEpsilon)
            {
                return Inclusion.Boundary;
            }

            return (rectangle.X <= point.X
                && point.X < rectangle.X + rectangle.Width
                && rectangle.Y <= point.Y
                && point.Y < rectangle.Y + rectangle.Height) ? Inclusion.Inside : Inclusion.Outside;
        }

        #endregion

        #region Point Near Ellipse

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
        public static bool PointNearEllipse(double px, double py, double x1, double y1, double x2, double y2, double close_distance)
        {
            var a = ((Abs((x2 - x1)) / 2) + close_distance);
            var b = ((Abs((y2 - y1)) / 2) + close_distance);
            px = (px - (x2 + x1) / 2);
            py = (py - (y2 + y1) / 2);
            return (((px * px) / (a * a)) + ((py * py) / (b * b)) <= 1);
        }

        #endregion

        #region Point Near a Line Segment

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
            => (DistToSegment2(px, py, x1, y1, x2, y2) <= close_distance);

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
            => (DistToSegment(px, py, x1, y1, x2, y2) <= close_distance);

        #endregion

        #region Point on Line Segment

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> PointOnLineSegmentTests()
            => new List<SpeedTester> {
                new SpeedTester(() => PointOnLineSegment(1, 1, 2, 2, 1.5, 1.5),
                $"{nameof(Experiments.PointOnLineSegment)}(1, 1, 2, 2, 1.5, 1.5)"),
                new SpeedTester(() => PointLineSegment(1, 1, 2, 2, 1.5, 1.5),
                $"{nameof(Experiments.PointLineSegment)}(1, 1, 2, 2, 1.5, 1.5)"),
                new SpeedTester(() => PointOnLine( new LineSegment(1, 1, 2, 2), new Point2D( 1.5, 1.5)),
                $"{nameof(Experiments.PointOnLine)}(1, 1, 2, 2, 1.5, 1.5)")
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        public static bool PointOnLineSegment(
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY,
            double pointX,
            double pointY)
            => ((Abs(pointX - segmentAX) < DoubleEpsilon)
            && (Abs(pointY - segmentAY) < DoubleEpsilon))
            || ((Abs(pointX - segmentBX) < DoubleEpsilon)
            && (Abs(pointY - segmentBY) < DoubleEpsilon))
            || (((pointX > segmentAX) == (pointX < segmentBX))
            && ((pointY > segmentAY) == (pointY < segmentBY))
            && (Abs((pointX - segmentAX) * (segmentBY - segmentAY) - (segmentBX - segmentAX) * (pointY - segmentAY)) < DoubleEpsilon));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segmentA"></param>
        /// <param name="segmentB"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        public static bool PointLineSegment(
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
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        /// <remarks>
        /// From: http://stackoverflow.com/questions/2255842/detecting-coincident-subset-of-two-coincident-line-segments/2255848
        /// </remarks>
        private static bool PointLineSegment(Point2D p, Point2D a1, Point2D a2)
        {
            var dummyU = 0.0d;
            var d = DistFromSeg(p, a1, a2, Epsilon, ref dummyU);
            return d < Epsilon;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool PointOnLine(LineSegment segment, Point2D point)
        {
            var Length1 = point.Distance(segment.B);
            // Sqrt((Point.X - Line.B.X) ^ 2 + (Point.Y - Line.B.Y))
            var Length2 = point.Distance(segment.A);
            // Sqrt((Point.X - Line.A.X) ^ 2 + (Point.Y - Line.A.Y))
            return Abs(segment.Length() - Length1 + Length2) < DoubleEpsilon;
        }

        #endregion

        #region Points are Close

        /// <summary>
        /// Return True if (x1, y1) is within close_distance vertically and horizontally of (x2, y2).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool AreClose(double x1, double y1, double x2, double y2, double epsilon = DoubleEpsilon) => (Abs(x2 - x1) <= epsilon) && (Abs(y2 - y1) <= epsilon);

        /// <summary>
        /// Compares two points for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='point1'>The first point to compare</param>
        /// <param name='point2'>The second point to compare</param>
        /// <returns>Whether or not the two points are equal</returns>
        public static bool AreClose(Point2D point1, Point2D point2, double epsilon = DoubleEpsilon)
            => Maths.AreClose(point1.X, point2.X, epsilon)
            && Maths.AreClose(point1.Y, point2.Y, epsilon);

        /// <summary>
        /// Compares two Size instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='size1'>The first size to compare</param>
        /// <param name='size2'>The second size to compare</param>
        /// <returns>Whether or not the two Size instances are equal</returns>
        public static bool AreClose(Size2D size1, Size2D size2, double epsilon = DoubleEpsilon) => Maths.AreClose(size1.Width, size2.Width, epsilon)
            && Maths.AreClose(size1.Height, size2.Height, epsilon);

        /// <summary>
        /// Compares two Vector instances for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='vector1'>The first Vector to compare</param>
        /// <param name='vector2'>The second Vector to compare</param>
        /// <returns>Whether or not the two Vector instances are equal</returns>
        public static bool AreClose(Vector2D vector1, Vector2D vector2, double epsilon = DoubleEpsilon) => Maths.AreClose(vector1.I, vector2.I, epsilon)
            && Maths.AreClose(vector1.J, vector2.J, epsilon);

        #endregion

        #region Polygon Centroid

        /// <summary>
        /// Find the polygon's centroid.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/find-the-centroid-of-a-polygon-in-c/</remarks>
        public static Point2D Centroid(Contour polygon)
        {
            // Add the first point at the end of the array.
            var num_points = polygon.Points.Count;
            var pts = new Point2D[num_points + 1];
            polygon.Points.CopyTo(pts, 0);
            pts[num_points] = polygon.Points[0];

            // Find the centroid.
            double X = 0;
            double Y = 0;
            double second_factor;
            for (var i = 0; i < num_points; i++)
            {
                second_factor =
                    pts[i].X * pts[i + 1].Y
                    - pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            var polygon_area = PolygonArea5(polygon.Points);
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

        #endregion

        #region Polygon Oriented Clockwise

        /// <summary>
        ///
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns>
        /// Return true if the polygon is oriented clockwise.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static bool PolygonIsOrientedClockwise(Contour polygon) => (SignedPolygonArea5(polygon.Points) < 0);

        #endregion

        #region Power of Two

        /// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
        public static bool IsPowerOfTwo(int value)
            => (value > 0) && ((value & (value - 1)) == 0);

        #endregion

        #region Quadratic Bezier Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate1DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate1DTests() => new List<SpeedTester> {
                new SpeedTester(() => QuadraticBezierInterpolate1D_0(0, 1, 2, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate1D_0)}(0, 1, 2, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate1D_1(0, 1, 2, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate1D_1)}(0, 1, 2, 0.5d)")
            };

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static double QuadraticBezierInterpolate1D_0(
            double x0,
            double x1,
            double x2,
            double t)
        {
            var mu1 = 1 - t;
            var mu12 = mu1 * mu1;
            var mu2 = t * t;

            return x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2;
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static double QuadraticBezierInterpolate1D_1(
            double x0,
            double x1,
            double x2,
            double t)
        {
            // point between a and b
            var ab = LinearInterpolate1D_0(x0, x1, t);
            // point between b and c
            var bc = LinearInterpolate1D_0(x1, x2, t);
            // point on the bezier-curve
            return LinearInterpolate1D_0(ab, bc, t);
        }

        #endregion

        #region Quadratic Bezier Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate2DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate2DTests() => new List<SpeedTester> {
                new SpeedTester(() => QuadraticBezierInterpolate2D_0(0, 1, 2, 3, 4, 5, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate2D_0)}(0, 1, 2, 3, 4, 5, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate2D_1(0, 1, 2, 3, 4, 5, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate2D_1)}(0, 1, 2, 3, 4, 5, 0.5d)")
            };

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static (double X, double Y) QuadraticBezierInterpolate2D_0(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu1 = 1 - t;
            var mu12 = mu1 * mu1;
            var mu2 = t * t;

            return (
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2)
                );
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static (double X, double Y) QuadraticBezierInterpolate2D_1(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            // point between a and b
            (double X, double Y) ab = LinearInterpolate2D_0(x0, y0, x1, y1, t);
            // point between b and c
            (double X, double Y) bc = LinearInterpolate2D_0(x1, y1, x2, y2, t);
            // point on the bezier-curve
            return LinearInterpolate2D_0(ab.X, ab.Y, bc.X, bc.Y, t);
        }

        #endregion

        #region Quadratic Bezier Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate3DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate3DTests() => new List<SpeedTester> {
                new SpeedTester(() => QuadraticBezierInterpolate3D_0(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate3D_0)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate3D_1(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate3D_1)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d)")
            };

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static (double X, double Y, double Z) QuadraticBezierInterpolate3D_0(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu1 = 1 - t;
            var mu12 = mu1 * mu1;
            var mu2 = t * t;

            return (
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2),
                (z0 * mu12 + 2 * z1 * mu1 * t + z2 * mu2));
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static (double X, double Y, double Z) QuadraticBezierInterpolate3D_1(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            // point between a and b
            (double X, double Y, double Z) ab = LinearInterpolate3D_0(x0, y0, z0, x1, y1, z1, t);
            // point between b and c
            (double X, double Y, double Z) bc = LinearInterpolate3D_0(x1, y1, z1, x2, y2, z2, t);
            // point on the bezier-curve
            return LinearInterpolate3D_0(ab.X, ab.Y, ab.Z, bc.X, bc.Y, bc.Z, t);
        }

        #endregion

        #region Quadratic Bezier Length approximations

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
            var ax = pointA.X - 2 * pointB.X + pointC.X;
            var ay = pointA.Y - 2 * pointB.Y + pointC.Y;
            var bx = 2 * pointB.X - 2 * pointA.X;
            var by = 2 * pointB.Y - 2 * pointA.Y;

            var a = 4 * (ax * ax + ay * ay);
            var b = 4 * (ax * bx + ay * by);
            var c = bx * bx + by * by;

            var abc = 2 * Sqrt(a + b + c);
            var a2 = Sqrt(a);
            var a32 = 2 * a * a2;
            var c2 = 2 * Sqrt(c);
            var ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4 * c * a - b * b) * Log((2 * a2 + ba + abc) / (ba + c2))) / (4 * a32);
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
            var p = new Point2D(Interpolators.QuadraticBezier(pointA.X, pointA.Y, pointB.X, pointB.Y, pointC.X, pointC.Y, 0));
            var prevX = p.X;
            var prevY = p.Y;
            for (var t = 0.005; t <= 1.0; t += 0.005)
            {
                p = new Point2D(Interpolators.QuadraticBezier(pointA.Y, pointA.X, pointB.Y, pointB.X, pointC.X, pointC.Y, t));
                var deltaX = p.X - prevX;
                var deltaY = p.Y - prevY;
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
            var coeff0X = pointA.X;
            var coeff0Y = pointA.Y;
            var coeff1X = 2.0 * (pointB.X - pointA.X);
            var coeff1Y = 2.0 * (pointB.Y - pointA.Y);
            var coeff2X = pointA.X - 2.0 * pointB.X + pointC.X;
            var coeff2Y = pointA.Y - 2.0 * pointB.Y + pointC.Y;

            // Count should be between 2 and 8 to align with MathExtensions.abscissa and MathExtensions.weight.
            var count = 5;
            var startl = (count == 2) ? 0 : count * (count - 1) / 2 - 1;

            // Minimum, maximum, and scalers.
            double min = 0;
            double max = 1;
            var mult = 0.5 * (max - min);
            var ab2 = 0.5 * (min + max);

            double theta = 0;
            double xPrime = 0;
            double yPrime = 0;
            double integrand = 0;

            // Evaluate the integral arc length along entire curve from t=0 to t=1.
            for (var index = 0; index < count; ++index)
            {
                theta = ab2 + mult * abscissa[startl + index];

                // First-derivative of the quadratic bezier.
                xPrime = coeff1X + 2.0 * coeff2X * theta;
                yPrime = coeff1Y + 2.0 * coeff2Y * theta;

                // Integrand for Gauss-Legendre numerical integration.
                integrand = Sqrt(xPrime * xPrime + yPrime * yPrime);

                sum += integrand * weight[startl + index];
            }

            return Abs(mult) < DoubleEpsilon ? sum : mult * sum;
        }

        #endregion

        #region Rectangle Center

        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        public static PointF Center0(RectangleF rectangle) => new PointF(
            rectangle.Left + (rectangle.Right - rectangle.Left) * 0.5f,
            rectangle.Top + (rectangle.Bottom - rectangle.Top) * 0.5f
        );

        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        public static PointF Center1(RectangleF rectangle) => new PointF(
            (rectangle.Left + rectangle.Right) * 0.5f,
            (rectangle.Top + rectangle.Bottom) * 0.5f
        );

        #endregion

        #region Rectangle to Square

        private static Rectangle2D ToSquare(Rectangle2D rect)
        {
            var smallest = rect.Height <= rect.Width ? rect.Height : rect.Width;
            return new Rectangle2D(
                rect.X + ((rect.Width - smallest) * 0.5d),
                rect.Y + ((rect.Height - smallest) * 0.5d),
                smallest,
                smallest);
        }

        #endregion

        #region Rectangle has NaN

        /// <summary>
        /// rectHasNaN - this returns true if this rectangle has X, Y , Height or Width as NaN.
        /// </summary>
        /// <param name='rect'>The rectangle to test</param>
        /// <returns>returns whether the Rectangle has NaN</returns>
        public static bool RectHasNaN(Rectangle2D rect) => double.IsNaN(rect.X)
             || double.IsNaN(rect.Y)
             || double.IsNaN(rect.Height)
             || double.IsNaN(rect.Width);

        #endregion

        #region Rectangles are Close

        /// <summary>
        /// Compares two rectangles for fuzzy equality.  This function
        /// helps compensate for the fact that double values can
        /// acquire error when operated upon
        /// </summary>
        /// <param name='rect1'>The first rectangle to compare</param>
        /// <param name='rect2'>The second rectangle to compare</param>
        /// <returns>Whether or not the two rectangles are equal</returns>
        public static bool AreClose(Rectangle2D rect1, Rectangle2D rect2, double epsilon = DoubleEpsilon)
        {
            // If they're both empty, don't bother with the double logic.
            if (rect1.IsEmpty)
                return rect2.IsEmpty;

            // At this point, rect1 isn't empty, so the first thing we can test is
            // rect2.IsEmpty, followed by property-wise compares.
            return (!rect2.IsEmpty)
                && Maths.AreClose(rect1.X, rect2.X, epsilon)
                && Maths.AreClose(rect1.Y, rect2.Y, epsilon)
                && Maths.AreClose(rect1.Height, rect2.Height, epsilon)
                && Maths.AreClose(rect1.Width, rect2.Width, epsilon);
        }

        #endregion

        #region Remove Point

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void RemovePoint(Contour polygon, int target)
            => polygon.Points.RemoveAt(target);

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void RemovePoint1(Contour polygon, int target)
        {
            var points = new Point2D[polygon.Points.Count - 1];
            //List.Copy(polygon.Points, 0, points, 0, target);
            Array.Copy(polygon.Points.ToArray(), target + 1, points, target, polygon.Points.Count - target - 1);
            polygon.Points = points.ToList();
        }

        #endregion

        #region Remove Polygon Ear

        /// <summary>
        /// Remove an ear from the polygon and add it to the triangles array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="triangles"></param>
        /// <remarks>http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/</remarks>
        public static void RemoveEar(Contour polygon, List<Triangle> triangles)
        {
            // Find an ear.
            var A = 0;
            var B = 0;
            var C = 0;

            // Create a new triangle for the ear.
            triangles.Add(FindEar(polygon, ref A, ref B, ref C));

            // Remove the ear from the polygon.
            RemovePoint(polygon, B);
        }

        #endregion

        #region Retrieve Cursor Resource

        /// <summary>
        /// Retrieve Cursor Resource from Executable
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        /// <remarks>BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!</remarks>
        public static Cursor RetriveCursorResource(string ResourceName)
        {
            //  Get the namespace
            var strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //  Get the resource into a stream
            System.IO.Stream ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream((strNameSpace + ("." + ResourceName)));
            if (ResourceStream == null)
            {
                // TODO: #If Then ... Warning!!! not translated
                MessageBox.Show(("Unable to find: "
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
                return Cursors.Default;
            }
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
                return new Cursor(ResourceStream);
            else
                return Cursors.Default;
        }

        #endregion

        #region Roots

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] CubicRoots(double a, double b, double c, double d)
        {
            // The horizontal line issue seems to be somewhere in here.
            var A = b / a;
            var B = c / a;
            var C = d / a;

            double S, T, Im;

            var Q = (3 * B - Pow(A, 2)) / 9;
            var R = (9 * A * B - 27 * C - 2 * Pow(A, 3)) / 54;
            var D = Pow(Q, 3) + Pow(R, 2);    // polynomial discriminant

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
            t = SortSpecial(t);

            //Console.log(t[0] + " " + t[1] + " " + t[2]);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuadraticRoots(double a, double b, double c)
        {
            var d = a - 2 * b + c;
            if (d != 0)
            {
                var m1 = -Sqrt(b * b - a * c);
                var m2 = -a + b;
                var v1 = -(m1 + m2) / d;
                var v2 = -(-m1 + m2) / d;
                return new List<double> { v1, v2 };
            }
            else if (b != c && d == 0)
            {
                return new List<double> { (2 * b - c) / (2 * (b - c)) };
            }
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> LinearRoots(double a, double b)
        {
            if (a != b)
                return new List<double> { a / (a - b) };
            return new List<double>();
        }

        #endregion

        #region Rotated Rectangle Bounds

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
            var cosAngle = Abs(Cos(angle));
            var sinAngle = Abs(Sin(angle));

            var size = new Size2D(
                (cosAngle * width) + (sinAngle * height),
                (cosAngle * height) + (sinAngle * width)
                );

            var loc = new Point2D(
                fulcrum.X + ((-width / 2) * cosAngle + (-height / 2) * sinAngle),
                fulcrum.Y + ((-width / 2) * sinAngle + (-height / 2) * cosAngle)
                );

            return new Rectangle2D(loc, size);
        }

        #endregion

        #region Rotated Rectangle Points

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
        public static List<Point2D> RotatedRectangleCorners(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            var points = new List<Point2D>();

            var xaxis = new Point2D(Cos(angle), Sin(angle));
            var yaxis = new Point2D(-Sin(angle), Cos(angle));

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

        #endregion

        #region Rotation Matrix

        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.
        /// </summary>
        /// <param name="center">The point around which to rotate.</param>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix3x2D RotateAroundPoint(Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix3x2D();

            // We need to go counter-clockwise.
            result.RotateAt(-angle.ToDegrees(), center.X, center.Y);

            return result;
        }

        #endregion

        #region Round

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> RoundTests()
        {
            var value = 0.5d;
            return new List<SpeedTester> {
                new SpeedTester(() => RoundAFZ(value),
                $"{nameof(Experiments.RoundAFZ)}({value})"),
                new SpeedTester(() => RoundToEven(value),
                $"{nameof(Experiments.RoundToEven)}({value})"),
                new SpeedTester(() => RoundToInt32(value),
                $"{nameof(Experiments.RoundToInt32)}({value})"),
                new SpeedTester(() => Round(value),
                $"{nameof(Experiments.Round)}({value})"),
                new SpeedTester(() => Truncate(value),
                $"{nameof(Experiments.Truncate)}({value})")
            };
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value, int decimals)
            => Math.Round(value, decimals, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value)
            => Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToEven(double value)
            => Math.Round(value, 0, MidpointRounding.ToEven);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToInt32(double value)
            => Convert.ToInt32(value);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Round(double value)
            => value < 0 ? (int)(value - 0.5) : (int)(value + 0.5);

        /// <summary>
        /// Truncate rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Truncate(double value)
            => (int)value;

        #endregion

        #region Self Intersecting Bezier
        // https://github.com/Parclytaxel/Kinross/blob/master/kinback/segment.py
        #endregion

        #region Sign

        // sign of number
        private static double Sign0(double x)
            => (x < 0d) ? -1 : 1;

        #endregion

        #region Signed Triangle Area

        /// <summary>
        /// Set of Finds the signed area of a triangle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(SignedTriangleArea))]
        public static List<SpeedTester> SignedTriangleArea()
            => new List<SpeedTester> {
                new SpeedTester(() => SignedTriangleArea(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.SignedTriangleArea)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => SignedTriangleAreaW8R(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.SignedTriangleAreaW8R)}(0, 0, 0, 1, 1, 1)"),
            };

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.
        /// </returns>
        /// <remarks>From Farseer Physics Engine.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleArea(double aX, double aY, double bX, double bY, double cX, double cY)
            => aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY);

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.
        /// </returns>
        /// <remarks>w8r</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedTriangleAreaW8R(double aX, double aY, double bX, double bY, double cX, double cY)
            => (aX - cX) * (bY - cY) - (bX - cX) * (aY - cY);

        #endregion

        #region Sine

        #endregion

        #region Sine Cosine Lookup

        /// <summary>
        /// Set of tests to lookup the Sin and Cos of a radian angle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(SinCosLookupTableTests))]
        public static List<SpeedTester> SinCosLookupTableTests()
        {
            var rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var value = default(double);
            return new List<SpeedTester> {
                new SpeedTester(() => SinCos0(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos0)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos1(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos1)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos2(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos2)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos3(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos3)}({Maths.WrapAngle(value += Radian)})"),
                new SpeedTester(() => SinCos4(Maths.WrapAngle(value += Radian + ClearSinCosTable())),
                $"{nameof(Experiments.SinCos4)}({Maths.WrapAngle(value += Radian)})"),
            };
        }

        private static Dictionary<double, (double, double)?> sinCosTable = new Dictionary<double, (double, double)?>();

        public static double ClearSinCosTable()
        {
            sinCosTable.Clear();
            return 0;
        }

        public static (double, double) SinCos0(double radian)
            // lookup, if not exists add to table and return the result.
            => sinCosTable.GetValueOrDefault(radian) ?? (sinCosTable[radian] = (Sin(radian), Cos(radian))).Value;

        public static (double, double) SinCos1(double radian)
            // lookup and replace with same value, or add if not exists.
            => (sinCosTable[radian] = sinCosTable.GetValueOrDefault(radian) ?? (Sin(radian), Cos(radian))).Value;

        private static (double, double) SinCos2(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
                sinCosTable.Add(radian, (Sin(radian), Cos(radian)));
            return sinCosTable[radian].Value;
        }

        private static (double, double) SinCos3(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
            {
                var value = (Sin(radian), Cos(radian));
                sinCosTable.Add(radian, value);
                return value;
            }

            return sinCosTable[radian].Value;
        }

        private static (double, double) SinCos4(double radian)
        {
            if (!sinCosTable.ContainsKey(radian))
            {
                return (sinCosTable[radian] = (Sin(radian), Cos(radian))).Value;
            }

            return sinCosTable[radian].Value;
        }

        #endregion

        #region Sine Interpolation of 1D

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </remarks>
        public static double Sine(
            double v1,
            double v2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }

        #endregion

        #region Sine Interpolation of 2D Points

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y) Sine(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return (x1 * (1 - mu2) + x2 * mu2, y1 * (1 - mu2) + y2 * mu2);
        }

        /// <summary>
        /// Function For sine interpolated Line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        /// <remarks></remarks>
        public static Point2D InterpolateSine(Point2D a, Point2D b, double index)
        {
            //Single MU2 = (double)((1.0 - Cos(index * 180)) * 0.5);
            //return Y1 * (1.0 - MU2) + Y2 * MU2;
            var MU2 = (1.0 - Sin(index * 180)) * 0.5;
            return (Point2D)a.Scale(1.0 - MU2).Add(b.Scale(MU2));
        }

        #endregion

        #region Sine Interpolation of 3D Points

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static (double X, double Y, double Z) Sine(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu2 = (1 - Sin(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }

        #endregion

        #region Slope of a 2D Vector

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Returns the slope angle of a vector.</returns>
        /// <remarks>
        /// The slope is calculated with Slope = Y / X or rise over run
        /// If the line is vertical, return something close to infinity
        /// (Close to the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(double i, double j)
            => Abs(i) < DoubleEpsilon ? SlopeMax : (j / i);

        #endregion

        #region Slope of a 2D Line

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>
        /// If the Line is Vertical return something close to infinity (Close to
        /// the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(
            double x1, double y1,
            double x2, double y2)
            => (Abs(x1 - x2) < DoubleEpsilon) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        #endregion

        #region Slopes Near Coliniar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="distSqrd"></param>
        /// <returns></returns>
        public static bool SlopesNearCollinear(Point2D a, Point2D b, Point2D c, double distSqrd)
        {
            // this function is more accurate when the point that's GEOMETRICALLY 
            // between the other 2 points is the one that's tested for distance.  
            // nb: with 'spikes', either pt1 or pt3 is geometrically between the other pts                    
            if (Abs(a.X - b.X) > Abs(a.Y - b.Y))
            {
                if ((a.X > b.X) == (a.X < c.X))
                    return SquareDistanceToLine(a.X, a.Y, b.X, b.Y, c.X, c.Y) < distSqrd;
                else if ((b.X > a.X) == (b.X < c.X))
                    return SquareDistanceToLine(b.X, b.Y, a.X, a.Y, c.X, c.Y) < distSqrd;
                else
                    return SquareDistanceToLine(c.X, c.Y, a.X, a.Y, b.X, b.Y) < distSqrd;
            }
            else
            {
                if ((a.Y > b.Y) == (a.Y < c.Y))
                    return SquareDistanceToLine(a.X, a.Y, b.X, b.Y, c.X, c.Y) < distSqrd;
                else if ((b.Y > a.Y) == (b.Y < c.Y))
                    return SquareDistanceToLine(b.X, b.Y, a.X, a.Y, c.X, c.Y) < distSqrd;
                else
                    return SquareDistanceToLine(c.X, c.Y, a.X, a.Y, b.X, b.Y) < distSqrd;
            }
        }

        #endregion

        #region Slopes of lines Equal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="UseFullRange"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        public static bool SlopesEqual(Point2D a, Point2D b, Point2D c, bool UseFullRange = false)
            => UseFullRange ? BigInteger.Multiply((BigInteger)(a.Y - b.Y), (BigInteger)(b.X - c.X)) == BigInteger.Multiply((BigInteger)(a.X - b.X), (BigInteger)(b.Y - c.Y))
            : (a.Y - b.Y) * (b.X - c.X) - (a.X - b.X) * (b.Y - c.Y) == 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="UseFullRange"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        public static bool SlopesEqual(Point2D a, Point2D b, Point2D c, Point2D d, bool UseFullRange = false)
            => UseFullRange ? BigInteger.Multiply((BigInteger)(a.Y - b.Y), (BigInteger)(b.X - c.X)) == BigInteger.Multiply((BigInteger)(a.X - b.X), (BigInteger)(b.Y - c.Y))
            : (int)(a.Y - b.Y) * (c.X - d.X) - (int)(a.X - b.X) * (c.Y - d.Y) == 0;

        #endregion

        #region Smooth Step

        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static double SmoothStep(double value1, double value2, double amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            var result = Clamp0(amount, 0f, 1f);
            result = Hermite(value1, 0f, value2, 0f, result);

            return result;
        }

        #endregion

        #region Sorting Special

        /// <summary>
        /// Special sorting routine designed to place negitive values at the back.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] SortSpecial(double[] a)
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

        #endregion

        #region Split RGB

        /// <summary>
        /// http://xbeat.net/vbspeed/c_SplitRGB.htm
        /// by www.Abstractvb.com, Date: 3/9/2001 9:26:43 PM, 20010922
        /// </summary>
        /// <param name="lColor"></param>
        /// <param name="lRed"></param>
        /// <param name="lGreen"></param>
        /// <param name="lBlue"></param>
        public (int lRed, int lGreen, int lBlue) SplitRGB01(int lColor)
        {
            lColor = lColor & 0xFFFFFF;
            var lRed = lColor % 0x100;
            lColor = lColor / 0x100;
            var lGreen = lColor % 0x100;
            lColor = lColor / 0x100;
            var lBlue = lColor % 0x100;
            return (lRed, lGreen, lBlue);
        }

        /// <summary>
        /// http://xbeat.net/vbspeed/c_SplitRGB.htm
        /// by Donald, donald@xbeat.net, 20010922
        /// </summary>
        /// <param name="lColor"></param>
        public (int lRed, int lGreen, int lBlue) SplitRGB02(int lColor)
            => (lColor & 0xFF,
            (lColor & 0xFF00) / 0x100,
            (lColor & 0xFF0000) / 0x10000);

        #endregion

        #region Squared Distance Between Two 2D Points

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1,
            double x2, double y2)
            => ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        #endregion

        #region Squared Distance to a Line

        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <param name="x2_">The x component of the first point on the line.</param>
        /// <param name="y2_">The y component of the first point on the line.</param>
        /// <param name="x3_">The x component of the second point on the line.</param>
        /// <param name="y3_">The y component of the second point on the line.</param>
        /// <returns></returns>
        public static double SquareDistanceToLine(
            double x1, double y1,
            double x2_, double y2_,
            double x3_, double y3_)
        {
            var A = y2_ - y3_;
            var B = x3_ - x2_;
            var C = (A * x1 + B * y1) - (A * x2_ + B * y2_);
            return (C * C) / (A * A + B * B);
        }

        #endregion

        #region Shortest Path

        /// <summary>
        /// Set of tests to run testing methods that calculate the wrapped angle of an angle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ShortestPathTests))]
        public static List<SpeedTester> ShortestPathTests()
        {
            var set = new Polygon(
                new List<Contour>(
                    new List<Contour> {
                        new Contour( // Boundary
                            new List<Point2D> {
                                new Point2D(10, 10),
                                new Point2D(300, 10),
                                new Point2D(300, 300),
                                new Point2D(10, 300),
                                // Cut out
                                new Point2D(10, 200),
                                new Point2D(200, 80),
                                new Point2D(10, 150)
                            }
                        ),
                        new Contour( // First inner triangle
                            new List<Point2D> {
                                new Point2D(20, 100),
                                new Point2D(175, 60),
                                new Point2D(40, 30)
                            }
                        ),
                        new Contour( // Second inner triangle
                            new List<Point2D> {
                                new Point2D(250, 150),
                                new Point2D(150, 150),
                                new Point2D(250, 200)
                            }
                        )
                    }
                )
            );
            return new List<SpeedTester> {
                new SpeedTester(() => ShortestPath0(set,new Point2D(20, 20), new Point2D(200, 200)),
                $"{nameof(Experiments.ShortestPath0)}(set,new Point2D(20, 20), new Point2D(200, 200))"),
                new SpeedTester(() => ShortestPath1(set,new Point2D(20, 20), new Point2D(200, 200)),
                $"{nameof(Experiments.ShortestPath1)}(set,new Point2D(20, 20), new Point2D(200, 200))"),
                new SpeedTester(() => ShortestPath2(set,new Point2D(20, 20), new Point2D(200, 200)),
                $"{nameof(Experiments.ShortestPath2)}(set,new Point2D(20, 20), new Point2D(200, 200))"),
           };
        }

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
        public static Polyline ShortestPath0(Polygon polygons, Point2D start, Point2D end)
        {
            // (larger than total solution dist could ever be)
            var maxLength = double.MaxValue;// 9999999.0;

            var pointList = new List<AccumulatorPoint2D>();
            var solution = new List<Point2D>();

            int pointCount, solutionNodes;

            int treeCount, polyI, i, j, bestI = 0, bestJ;
            double bestDist, newDist;

            //  Fail if either the startpoint or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            //  If there is a straight-line solution, return with it immediately.
            if (LineInPolygonSet(polygons, start, end))
                return new Polyline(new List<Point2D> { start, end });

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the startpoint and endpoint.
            pointList.Add(start);
            pointCount = 1;
            for (polyI = 0; polyI < polygons.Count; polyI++)
            {
                for (i = 0; i < polygons.Contours[polyI].Points.Count; i++)
                {
                    pointList.Add(polygons.Contours[polyI].Points[i]);
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
                for (i = 0; i < treeCount; i++)
                {
                    for (j = treeCount; j < pointCount; j++)
                    {
                        if (LineInPolygonSet(polygons, (Point2D)pointList[i], (Point2D)pointList[j]))
                        {
                            newDist = pointList[i].TotalDistance + Distance((Point2D)pointList[i], (Point2D)pointList[j]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist;
                                bestI = i;
                                bestJ = j;
                            }
                        }
                    }
                }

                if (Abs(bestDist - maxLength) < DoubleEpsilon)
                    return null;   //  (no solution)
                pointList[bestJ].Previous = bestI;
                pointList[bestJ].TotalDistance = bestDist;

                // Swap
                AccumulatorPoint2D temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            //  Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }
            j = solutionNodes - 1;
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
        public static Polyline ShortestPath1(Polygon polygons, Point2D start, Point2D end)
        {
            // (larger than total solution dist could ever be)
            var maxLength = double.MaxValue;

            var pointList = new List<AccumulatorPoint2D>();
            var solution = new List<Point2D>();

            int pointCount;
            int solutionNodes;

            int treeCount;
            var bestI = 0;
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
            if (polygons.PolygonSetContainsPoints(start, end) == Inclusion.Inside)
                return new Polyline(new List<Point2D> { start, end });

            //  Build a point list that refers to the corners of the
            //  polygons, as well as to the startpoint and endpoint.
            pointList.Add(start);
            pointCount = 1;
            foreach (Contour poly in polygons.Contours)
            {
                foreach (Point2D point in poly.Points)
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
                for (var ti = 0; ti < treeCount; ti++)
                {
                    for (var tj = treeCount; tj < pointCount; tj++)
                    {
                        if (polygons.PolygonSetContainsPoints((Point2D)pointList[ti], (Point2D)pointList[tj]) == Inclusion.Inside)
                        {
                            newDist = pointList[ti].TotalDistance + Distance((Point2D)pointList[ti], (Point2D)pointList[tj]);
                            if (newDist < bestDist)
                            {
                                bestDist = newDist;
                                bestI = ti;
                                bestJ = tj;
                            }
                        }
                    }
                }

                if (Abs(bestDist - maxLength) < DoubleEpsilon)
                    return null;   //  (no solution)
                pointList[bestJ].Previous = bestI;
                pointList[bestJ].TotalDistance = bestDist;

                // Swap
                AccumulatorPoint2D temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            //  Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            var i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }
            var j = solutionNodes - 1;
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
        public static Polyline ShortestPath2(Polygon polygons, Point2D start, Point2D end)
        {
            // Fail if either the start point or endpoint is outside the polygon set.
            if (!polygons.Contains(start)
            || !polygons.Contains(end))
            {
                return null;
            }

            // If there is a straight-line solution, return with it immediately.
            if (polygons.PolygonSetContainsPoints(start, end) == Inclusion.Inside)
                return new Polyline(new List<Point2D> { start, end });

            // (larger than total solution dist could ever be)
            var maxLength = double.MaxValue;

            var pointList = new List<(double X, double Y, double TotalDistance, int Previous)>();
            var solution = new List<Point2D>();

            int solutionNodes;

            int treeCount;
            var bestI = 0;
            int bestJ;
            double bestDist;
            double newDist;

            // Build a point list that refers to the corners of the
            // polygons, as well as to the start point and endpoint.
            pointList.Add((start.X, start.Y, 0, 0));
            foreach (Contour poly in polygons.Contours)
            {
                foreach (Point2D point in poly.Points)
                    pointList.Add((point.X, point.Y, 0, 0));
            }

            pointList.Add((end.X, end.Y, 0, 0));

            // Initialize the shortest-path tree to include just the start point.
            treeCount = 1;
            //pointList[0].TotalDistance = 0d;

            // Iteratively grow the shortest-path tree until it reaches the endpoint
            // or until it becomes unable to grow, in which case exit with failure.
            bestJ = 0;
            while (bestJ < pointList.Count - 1)
            {
                bestDist = maxLength;
                for (var ti = 0; ti < treeCount; ti++)
                {
                    for (var tj = treeCount; tj < pointList.Count; tj++)
                    {
                        if (polygons.PolygonSetContainsPoints(new Point2D(pointList[ti].X, pointList[ti].Y), new Point2D(pointList[tj].X, pointList[tj].Y)) == Inclusion.Inside)
                        {
                            newDist = pointList[ti].TotalDistance + (new Point2D(pointList[ti].X, pointList[ti].Y)).Distance(new Point2D(pointList[tj].X, pointList[tj].Y));
                            if (newDist < bestDist)
                            {
                                bestDist = newDist;
                                bestI = ti;
                                bestJ = tj;
                            }
                        }
                    }
                }

                if (bestDist == maxLength)
                    return null; // (no solution)
                pointList[bestJ] = (pointList[bestJ].X, pointList[bestJ].Y, bestDist, bestI);

                // Swap
                var temp = pointList[bestJ];
                pointList[bestJ] = pointList[treeCount];
                pointList[treeCount] = temp;

                treeCount++;
            }

            // Load the solution arrays.
            solution.Add(start);
            solutionNodes = -1;
            var i = treeCount - 1;
            while (i > 0)
            {
                i = pointList[i].Previous;
                solutionNodes++;
            }

            var j = solutionNodes - 1;
            i = treeCount - 1;
            while (j >= 0)
            {
                i = pointList[i].Previous;
                solution.Insert(1, new Point2D(pointList[i].X, pointList[i].Y));
                j--;
            }

            solution.Add(end);

            // Success.
            return new Polyline(solution);
        }

        #endregion

        #region Swap two values

        /// <summary>
        /// Set of tests to run testing methods that swap values.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(SwapTests))]
        public static List<SpeedTester> SwapTests()
        {
            double a = 0;
            double b = 100;
            return new List<SpeedTester> {
                new SpeedTester(() => Swap(ref a,ref b),
                $"{nameof(Experiments.Swap)}(ref a,ref b)"),
           };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool Swap<T>(ref T a, ref T b)
        {
            T swap = a;
            a = b;
            b = swap;
            return true;
        }

        #endregion

        #region Triangulate a polygon

        /// <summary>
        /// Set of tests to run testing methods that get the triangles of a polygon.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(TriangulateTests))]
        public static List<SpeedTester> TriangulateTests()
            => new List<SpeedTester> {
                new SpeedTester(() => Triangulate(new Contour(new Point2D[] { new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)})),
                $"{nameof(Experiments.Triangulate)}(new Polygon(new Point2D[] {{ new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)}}))"),
           };

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
        public static List<Triangle> Triangulate(Contour polygon)
        {
            // Copy the points into a scratch array.
            var pts = new List<Point2D>(polygon.Points);

            // Make a scratch polygon.
            var pgon = new Contour(pts);

            // Orient the polygon clockwise.
            OrientPolygonClockwise(pgon);

            // Make room for the triangles.
            var triangles = new List<Triangle>();

            // While the copy of the polygon has more than
            // three points, remove an ear.
            while (pgon.Points.Count > 3)
            {
                // Remove an ear from the polygon.
                RemoveEar(pgon, triangles);
            }

            // Copy the last three points into their own triangle.
            triangles.Add(new Triangle(pgon.Points[0], pgon.Points[1], pgon.Points[2]));

            return triangles;
        }

        #endregion

        #region Values are Close

        /// <summary>
        /// Set of tests to run testing methods that determine whether values are close.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(ValuesAreCloseTests))]
        public static List<SpeedTester> ValuesAreCloseTests()
            => new List<SpeedTester> {
                new SpeedTester(() => AreClose(0d, double.Epsilon),
                $"{nameof(Experiments.AreClose)}({0d}, {double.Epsilon})"),
           };

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
        /// <param name="epsilon"></param>
        /// <remarks></remarks>
        public static bool AreClose(double value1, double value2, double epsilon = DoubleEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Abs(value1 - value2) < DoubleEpsilon)
                return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Abs(value1) + Abs(value2) + 10d) * epsilon;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        #endregion

        #region Vector between Vectors

        /// <summary>
        /// Set of tests to run testing methods that calculate whether a vector is between two others.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(VectorBetweenTests))]
        public static List<SpeedTester> VectorBetweenTests()
        {
            var a = 45d.ToRadians();
            var b = 90d.ToRadians();
            var c = 0d.ToRadians();
            var i = Sin(a);
            var j = Cos(a);
            var i2 = Sin(b);
            var j2 = Cos(b);
            var i3 = Sin(c);
            var j3 = Cos(c);

            return new List<SpeedTester> {
                new SpeedTester(() => VectorBetween0(i, j, i2, j2, i3, j3),
                $"{nameof(Experiments.VectorBetween0)}({i}, {j}, {i2}, {j2}, {i3}, {j3})"),
                new SpeedTester(() => VectorBetween1(i, j, i2, j2, i3, j3),
                $"{nameof(Experiments.VectorBetween1)}({i}, {j}, {i2}, {j2}, {i3}, {j3})"),
           };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="i3"></param>
        /// <param name="j3"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </remarks>
        public static bool VectorBetween0(double i, double j, double i2, double j2, double i3, double j3)
            => CrossProduct(i2, j2, i, j) * CrossProduct(i2, j2, i3, j3) >= 0
            && CrossProduct(i3, j3, i, j) * CrossProduct(i3, j3, i2, j2) >= 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="i3"></param>
        /// <param name="j3"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </remarks>
        public static bool VectorBetween1(double i, double j, double i2, double j2, double i3, double j3)
            => ((i2 * j) - (j2 * i)) * ((i2 * j3) - (j2 * i3)) >= 0
            && ((i3 * j) - (j3 * i)) * ((i3 * j2) - (j3 * i2)) >= 0;

        #endregion

        #region Wrap Angle

        /// <summary>
        /// Set of tests to run testing methods that calculate the wrapped angle of an angle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(WrapAngleTests))]
        public static List<SpeedTester> WrapAngleTests()
            => new List<SpeedTester> {
                new SpeedTester(() => WrapAngle0(45d.ToRadians()),
                $"{nameof(Experiments.WrapAngle0)}(45d.ToRadians())"),
                new SpeedTester(() => WrapAngle1(45d.ToRadians()),
                $"{nameof(Experiments.WrapAngle1)}(45d.ToRadians())"),
                new SpeedTester(() => WrapAngle2(45d.ToRadians()),
                $"{nameof(Experiments.WrapAngle2)}(45d.ToRadians())"),
                new SpeedTester(() => WrapAngle3(45d.ToRadians()),
                $"{nameof(Experiments.WrapAngle3)}(45d.ToRadians())")
           };

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static double WrapAngle0(double angle)
        {
            // The IEEERemainder method works better than the % modulus operator in this case, even if it is slower.
            var value = IEEERemainder(angle, Tau);
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngle1(double angle)
        {
            // The active ingredient of the IEEERemainder method.
            var value = angle - (Tau * Math.Round(angle * InverseTau));
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static double WrapAngle2(double angle)
        {
            var test = IEEERemainder(angle, Tau);
            if (test <= -PI)
                test += Tau;
            else if (test > PI)
                test -= Tau;
            return test;
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        public static double WrapAngle3(double angle)
        {
            var test = angle % Tau;
            return (test <= -PI) ? test + Tau : test - Tau;
        }

        #endregion

        #region Wrap Point on Rectangle Bounds

        /// <summary>
        /// Set of tests to run testing methods that calculate the wrapped position of a point in a rectangle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(WrapPointToRectangleTests))]
        public static List<SpeedTester> WrapPointToRectangleTests()
        {
            var reff = new Point2D();
            return new List<SpeedTester> {
                new SpeedTester(() => WrapPointToRectangle(new Rectangle2D(0, 0, 20, 20),new Point2D(31, 21), ref reff),
                $"{nameof(Experiments.WrapPointToRectangle)}(new Rectangle2D(0, 0, 20, 20),new Point2D(31, 21), ref reff)"),
           };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="point"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Point2D WrapPointToRectangle(Rectangle2D bounds, Point2D point, ref Point2D reference)
        {
            if (point.X <= bounds.X)
            {
                reference = (reference - new Size2D(bounds.X, 0));
                return new Point2D((bounds.Width - 2), point.Y);
            }
            if (point.Y <= bounds.Y)
            {
                reference = (reference - new Size2D(0, bounds.Y));
                return new Point2D(point.X, (bounds.Height - 2));
            }
            if (point.X >= (bounds.Width - 1))
            {
                reference = (reference + new Size2D(bounds.Width, 0));
                return new Point2D((bounds.X + 2), point.Y);
            }
            if (point.Y >= (bounds.Height - 1))
            {
                reference = (reference + new Size2D(0, bounds.Height));
                return new Point2D(point.X, (bounds.Y + 2));
            }
            return point;
            // 'ToDo: Adjust My_StartPoint when Screen is wrapped
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="theta"></param>
        /// <param name="ellipse"></param>
        /// <param name="phi"></param>
        /// <param name="rect"></param>
        private static void Draw_rect_at_ellipse(Graphics g, double theta, Rectangle2D ellipse, double phi, Rectangle2D rect)
        {
            var xaxis = new Point2D(Cos(theta), Sin(theta));
            var yaxis = new Point2D(-Sin(theta), Cos(theta));
            Point2D ellipse_point;

            // Ellipse equation for an ellipse at origin.
            ellipse_point = new Point2D(ellipse.Width * Cos(phi), ellipse.Height * Sin(phi));

            // Apply the rotation transformation and translate to new center.
            rect.Location = new Point2D(ellipse.Left + (ellipse_point.X * xaxis.X + ellipse_point.Y * xaxis.Y),
                                       ellipse.Top + (ellipse_point.X * yaxis.X + ellipse_point.Y * yaxis.Y));

            g.DrawRectangle(Pens.AntiqueWhite, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        /// <summary>
        /// Bow Curve (2D)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        /// <remarks>
        ///  Also known as the "cocked hat", it was first documented by Sylvester around
        ///  1864 and Cayley in 1867.
        /// </remarks>
        private static void DrawBowCurve2D(Graphics g, Pen DPen, double Precision, Size2D Offset, Size2D Multiplyer)
        {
            var NewPoint = new Point2D(
                ((1 - (Tan((PI * -1)) * 2)) * Cos((PI * -1))) * Multiplyer.Width,
                ((1 - (Tan((PI * -1)) * 2)) * (2 * Sin((PI * -1)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (var Index = (PI * -1); (Index <= PI); Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new Point2D(
                    ((1 - (Tan(Index) * 2)) * Cos(Index)) * Multiplyer.Width,
                    ((1 - (Tan(Index) * 2)) * (2 * Sin(Index))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }

        /// <summary>
        /// Butterfly Curve
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        private static void DrawButterflyCurve2D(Graphics g, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            const double N = 10000;
            var U = (0 * (24 * (PI / N)));

            var NewPoint = new Point2D(
                Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (double Index = 1; (Index <= N); Index = (Index + Precision))
            {
                LastPoint = NewPoint;
                U = (Index * (24 * (PI / N)));

                NewPoint = new Point2D(
                    Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                    (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }
    }
}
