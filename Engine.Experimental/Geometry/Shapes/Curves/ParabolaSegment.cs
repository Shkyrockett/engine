// <copyright file="Parabola.cs" company="Shkyrockett" >
//     Copyright © 2015 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The parabola class.
    /// </summary>
    /// <remarks>
    /// References to look at for development:
    /// <ul>
    /// <li><a href="https://www.youtube.com/watch?v=4Af3NBN34ME">https://www.youtube.com/watch?v=4Af3NBN34ME</a></li>
    /// <li><a href="https://www.mathwarehouse.com/quadratic/parabola/interactive-parabola.php">https://www.mathwarehouse.com/quadratic/parabola/interactive-parabola.php</a></li>
    /// <li><a href="http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/">http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/</a></li>
    /// <li><a href="http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/">http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/</a></li>
    /// <li><a href="http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/">http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/</a></li>
    /// <li><a href="https://stackoverflow.com/questions/21384619/drawing-a-quadratic-curve">https://stackoverflow.com/questions/21384619/drawing-a-quadratic-curve</a></li>
    /// <li><a href="https://stackoverflow.com/questions/17328322/create-a-parabolic-trajectory-with-fixed-angle">https://stackoverflow.com/questions/17328322/create-a-parabolic-trajectory-with-fixed-angle</a></li>
    /// <li><a href="https://stackoverflow.com/a/717833">https://stackoverflow.com/a/717833</a></li>
    /// <li><a href="https://forum.unity.com/threads/generating-dynamic-parabola.211681/">https://forum.unity.com/threads/generating-dynamic-parabola.211681/</a></li>
    /// <li><a href="https://www.mathwarehouse.com/geometry/parabola/standard-and-vertex-form.php">https://www.mathwarehouse.com/geometry/parabola/standard-and-vertex-form.php</a></li>
    /// </ul>
    /// </remarks>
    public class ParabolaSegment
    {
        private double a;
        private double b;
        private double c;
        private double h;
        private double k;

        /// <summary>
        /// The a component of a parabolic curve.
        /// </summary>
        public double A
        {
            get { return a; }
            set
            {
                a = value;
                h = -(b / (2 * a));
                k = -(b * b / (4 * a)) + c;
            }
        }

        /// <summary>
        /// The b component of a parabolic curve.
        /// </summary>
        public double B
        {
            get { return b; }
            set
            {
                b = value;
                h = -(b / (2 * a));
                k = -(b * b / (4 * a)) + c;
            }
        }

        /// <summary>
        /// The c component of a parabolic curve.
        /// </summary>
        public double C
        {
            get { return c; }
            set
            {
                c = value;
                h = -(b / (2 * a));
                k = -(b * b / (4 * a)) + c;
            }
        }

        /// <summary>
        /// The horizontal component of the parabola vertex.
        /// </summary>
        public double H
        {
            get { return h; }
            set
            {
                h = value;
                b = -2d * a * h;
                c = (b * b / (4 * a)) + k;
            }
        }

        /// <summary>
        /// The vertical component of the parabola vertex.
        /// </summary>
        public double K
        {
            get { return k; }
            set
            {
                k = value;
                b = -2d * a * h;
                c = (b * b / (4 * a)) + k;
            }
        }

        /// <summary>
        /// Get position from a parabola defined by start and end, height, and time
        /// https://forum.unity.com/threads/generating-dynamic-parabola.211681/
        /// </summary>
        /// <param name='start'>
        /// The start point of the parabola
        /// </param>
        /// <param name='end'>
        /// The end point of the parabola
        /// </param>
        /// <param name='height'>
        /// The height of the parabola at its maximum
        /// </param>
        /// <param name='t'>
        /// Normalized time (0->1)
        /// </param>S
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) SampleRotatedParabola((double X, double Y) start, (double X, double Y) end, double height, double t)
        {
            var parabolicT = (t * 2d) - 1d;
            if (Abs(start.Y - end.Y) < 0.1d)
            {
                // Start and end are roughly level, pretend they are - simpler solution with less steps.
                var (travelDirectionX, travelDirectionY) = (end.X - start.X, end.Y - start.Y);
                var result = (X: start.X + (t * travelDirectionX), Y: start.Y + (t * travelDirectionY));
                result.Y += ((-parabolicT * parabolicT) + 1) * height;
                return result;
            }
            else
            {
                // Start and end are not level, gets more complicated.
                var (travelDirectionX, travelDirectionY) = (end.X - start.X, end.Y - start.Y);
                var (levelDirectionX, levelDirectionY) = (end.X - start.X, end.Y - end.Y);
                var right = CrossProduct(travelDirectionX, travelDirectionY, levelDirectionX, levelDirectionY);
                var up = Normalize1D((end.Y > start.Y) ? -CrossProduct(right, right, travelDirectionX, travelDirectionY) : CrossProduct(right, right, travelDirectionX, travelDirectionY));
                return (
                    X: start.X + (t * travelDirectionX) + (((-parabolicT * parabolicT) + 1d) * height * up),
                    Y: start.Y + (t * travelDirectionY) + (((-parabolicT * parabolicT) + 1d) * height * up));
            }
        }

        /// <summary>
        /// Get position from a parabola defined by start and end, height, and time
        /// https://forum.unity.com/threads/generating-dynamic-parabola.211681/
        /// </summary>
        /// <param name='start'>
        /// The start point of the parabola
        /// </param>
        /// <param name='end'>
        /// The end point of the parabola
        /// </param>
        /// <param name='height'>
        /// The height of the parabola at its maximum
        /// </param>
        /// <param name='t'>
        /// Normalized time (0->1)
        /// </param>S
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) SampleRotatedParabola((double X, double Y, double Z) start, (double X, double Y, double Z) end, double height, double t)
        {
            var parabolicT = (t * 2d) - 1d;
            if (Abs(start.Y - end.Y) < 0.1d)
            {
                // Start and end are roughly level, pretend they are - simpler solution with less steps.
                var (travelDirectionX, travelDirectionY, travelDirectionZ) = (end.X - start.X, end.Y - start.Y, end.Z - start.Z);
                var result = (X: start.X + (t * travelDirectionX), Y: start.Y + (t * travelDirectionY), Z: start.Z + (t * travelDirectionZ));
                result.Y += ((-parabolicT * parabolicT) + 1) * -height;
                return result;
            }
            else
            {
                // Start and end are not level, gets more complicated.
                var (travelDirectionX, travelDirectionY, travelDirectionZ) = (end.X - start.X, end.Y - start.Y, end.Z - start.Z);
                var (levelDirectionX, levelDirectionY, levelDirectionZ) = (end.X - start.X, end.Y - end.Y, end.Z - start.Z);
                var (rightX, rightY, rightZ) = CrossProduct(travelDirectionX, travelDirectionY, travelDirectionZ, levelDirectionX, levelDirectionY, levelDirectionZ);
                var up = CrossProduct(rightX, rightY, rightZ, travelDirectionX, travelDirectionY, travelDirectionZ);
                if (end.Y < start.Y) up = (X: -up.X, Y: -up.Y, Z: -up.Z);
                up = Normalize3D(up.X, up.Y, up.Z);
                var result = (X: start.X + (t * travelDirectionX), Y: start.Y + (t * travelDirectionY), Z: start.Z + (t * travelDirectionZ));
                result = (X: result.X + (((-parabolicT * parabolicT) + 1) * height * up.X), Y: result.Y + (((-parabolicT * parabolicT) + 1) * height * up.Y), Z: result.Z + (((-parabolicT * parabolicT) + 1) * height * up.Z));
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.youtube.com/watch?v=4Af3NBN34ME
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y)[] InterpolateParabolaStandard(double a, double b, double c, double x1, double x2)
        {
            //var h = -(b / (2 * a));
            //var k = -(b * b / (4 * a)) + c;
            var set = new SortedSet<(double X, double Y)>();
            for (var x = x1; x <= x2; x++)
            {
                // Equation for finding the y of a parabola in standard form.
                var y = (a * (x * x)) + ((b * x) + c);
                set.Add((x, y));
            }
            return set.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y)[] InterpolateParabolaVertex(double a, double h, double k, double x1, double x2)
        {
            var set = new SortedSet<(double X, double Y)>();
            for (var x = x1; x <= x2; x++)
            {
                // Equation for finding the y of a parabola in vertex form.
                var y = (a * (x - h) * (x - h)) + k;
                set.Add((x, y));
            }
            return set.ToArray();
        }

        /// <summary>
        /// Interpolate a parabola from the standard parabolic equation.
        /// </summary>
        /// <param name="a">The <paramref name="a"/> component of the parabola.</param>
        /// <param name="b">The <paramref name="b"/> component of the parabola.</param>
        /// <param name="c">The <paramref name="c"/> component of the parabola.</param>
        /// <param name="x1">The <paramref name="x1"/>imum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2"/>imum x value to interpolate.</param>
        /// <param name="t">The <paramref name="t"/>ime index of the iteration.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing the interpolated point at the t index.</returns>
        /// <example>
        /// <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var b = -2d * a * h;
        /// var c = (b * b / (4 * a)) + k;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)>();
        /// 
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        ///     list.Add(InterpolateVertexParabola(a, b, c, -100, 100, 1d / i));
        /// }
        /// </code>
        /// </example>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InterpolateStandardParabola(double a, double b, double c, double x1, double x2, double t)
        {
            // Scale the t index to the segment range.
            var x = x1 + ((x2 - x1) * t);
            return (x, Y: (a * (x * x)) + ((b * x) + c));
        }

        /// <summary>
        /// Interpolate a parabola from the general vertex form of the parabolic equation.
        /// </summary>
        /// <param name="a">The <paramref name="a"/> component of the parabola.</param>
        /// <param name="h">The horizontal component of the parabola vertex.</param>
        /// <param name="k">The vertical component of the parabola vertex.</param>
        /// <param name="x1">The <paramref name="x1"/>imum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2"/>imum x value to interpolate.</param>
        /// <param name="t">The <paramref name="t"/>ime index of the iteration.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing the interpolated point at the t index.</returns>
        /// <example>
        /// <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)>();
        /// 
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        ///     list.Add(InterpolateVertexParabola(a, h, k, -100, 100, 1d / i));
        /// }
        /// </code>
        /// </example>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InterpolateVertexParabola(double a, double h, double k, double x1, double x2, double t)
        {
            // Scale the t index to the segment range.
            var x = x1 + ((x2 - x1) * t);
            return (x, Y: (a * (x - h) * (x - h)) + k);
        }

        /// <summary>
        /// Find a parabola in standard form from three points on the parabola.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="x3">The x component of the third point.</param>
        /// <param name="y3">The y component of the third point.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/717833
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c) FindStandardParabolaFromThreePoints(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            // ToDo: Work out what to do when denom is 0
            var a = ((x3 * (y2 - y1)) + (x2 * (y1 - y3)) + (x1 * (y3 - y2))) / denom;
            var b = ((x3 * x3 * (y1 - y2)) + (x2 * x2 * (y3 - y1)) + (x1 * x1 * (y2 - y3))) / denom;
            var c = ((x2 * x3 * (x2 - x3) * y1) + (x3 * x1 * (x3 - x1) * y2) + (x1 * x2 * (x1 - x2) * y3)) / denom;
            return (a, b, c);
        }

        /// <summary>
        /// Find a parabola in vertex form from three points on the parabola.
        /// </summary>
        /// <param name="x1">The x component of the first point.</param>
        /// <param name="y1">The y component of the first point.</param>
        /// <param name="x2">The x component of the second point.</param>
        /// <param name="y2">The y component of the second point.</param>
        /// <param name="x3">The x component of the third point.</param>
        /// <param name="y3">The y component of the third point.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/717833
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double h, double k) FindVertexParabolaFromThreePoints(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
            // ToDo: Work out what to do when denom is 0.
            var a = ((x3 * (y2 - y1)) + (x2 * (y1 - y3)) + (x1 * (y3 - y2))) / denom;
            var b = ((x3 * x3 * (y1 - y2)) + (x2 * x2 * (y3 - y1)) + (x1 * x1 * (y2 - y3))) / denom;
            var c = ((x2 * x3 * (x2 - x3) * y1) + (x3 * x1 * (x3 - x1) * y2) + (x1 * x2 * (x1 - x2) * y3)) / denom;

            return (a, -b / (2d * a), c - (b * b / (4d * a)));
        }

        /// <summary>
        /// Find the parabola that passes through two points and has a k vertex height.
        /// </summary>
        /// <param name="x1">The x component of the first point on the parabola.</param>
        /// <param name="y1">The y component of the first point on the parabola.</param>
        /// <param name="x2">The x component of the second point on the parabola.</param>
        /// <param name="y2">The y component of the second point on the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double h, double k) FindVertexParabolaFromTwoPointsAndK(double x1, double y1, double x2, double y2, double k)
        {
            var h = FindParabolaHFromTwoPointsAndK(x1, y1, x2, y2, k);
            var hv = (h.a > x1 && h.a < x2) ? h.a : h.b;
            return FindVertexParabolaFromThreePoints(x1, y1, hv, k, x2, y2);
        }

        /// <summary>
        /// Find the h of a parabola given two points on the parabola and the k vertex height.
        /// </summary>
        /// <param name="x1">The x component of the first point on the parabola.</param>
        /// <param name="y1">The y component of the first point on the parabola.</param>
        /// <param name="x2">The x component of the second point on the parabola.</param>
        /// <param name="y2">The y component of the second point on the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b) FindParabolaHFromTwoPointsAndK(double x1, double y1, double x2, double y2, double k)
        {
            var a = 1d - ((y2 - k) / (y1 - k));
            var b = (-2d * x2) + (2d * x1 * ((y2 - k) / (y1 - k)));
            var c = (x2 * x2​) - (x1 * x1 * ((y2 - k) / (y1 - k)));

            // Find the roots.
            if (a is 0d)
            {
                // If a is zero, reduce to linear, if b is also zero reduce to constant.
                return b is 0d ? (c, c) : (-c / b, -c / b);
            }
            else
            {
                var b_ = b / a;
                var c_ = c / a;
                var discriminant = (b_ * b_) - (4d * c_);

                if (discriminant == 0)
                {
                    return (OneHalf * -b_, OneHalf * -b_);
                }
                else if (discriminant > 0d)
                {
                    var e = Sqrt(discriminant);
                    return (OneHalf * (-b_ + e), OneHalf * (-b_ - e));
                }
                else
                {
                    // ToDo: Not sure exactly what to do here.
                    // Imaginary number.
                    var e = Sqrt(Abs(discriminant));
                    return (OneHalf * (-b_ + e), OneHalf * (-b_ - e));
                    //return (double.NaN, double.NaN);
                }
            }
        }

        /// <summary>
        /// Find the a of a parabola given two points on the parabola and the k vertex height.
        /// </summary>
        /// <param name="x">The x component of a point on the parabola.</param>
        /// <param name="y">The y component of a point on the parabola.</param>
        /// <param name="h">The h or horizontal component of the vertex of the parabola.</param>
        /// <param name="k">The k or vertex height of the parabola.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://answers.yahoo.com/question/index?qid=20090730215957AAFg8ZK
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double FindParabolaAFromAPointAndVertex(double x, double y, double h, double k)
            => x - h == 0d ? 0d : (y - k) / ((x - h) * (x - h));

        /// <summary>
        /// Find the tangent value of a point on a parabola.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/1258196
        /// https://www.emathzone.com/tutorials/geometry/equation-of-tangent-and-normal-to-parabola.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double x, double y, double i, double j) ParabolaTangent(double a, double b, double c, double x)
        {
            //var x1 = x + 1;
            // ToDo: This should be finding the tangent ray.
            //var y1 = (x1 * ((a * x1) + b)) + c;
            //var y = (((2d * a * x1) + b) * (x - x1)) + y1;
            return (x, (x * ((a * x) + b)) + c, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/1258196
        /// https://www.emathzone.com/tutorials/geometry/equation-of-tangent-and-normal-to-parabola.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double x, double y, double i, double j) ParabolaNormal(double a, double b, double c, double x)
        {
            return (0, 0, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://sciencing.com/how-to-convert-slope-intercept-form-to-standard-form-13712257.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double a, double b, double c) LineSlopeInterceptToStandard(double m, double b)
        {
            return (0, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://sciencing.com/how-to-convert-slope-intercept-form-to-standard-form-13712257.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double m, double b) LineStandardToSlopeIntercept(double a, double b, double c)
        {
            return (0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.youtube.com/watch?v=t6n-ShpFFjo
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y)[] IntersectionParabolaLine(double a, double b, double c, double x, double y, double i, double j)
        {
            // Parabola: y = ax^2 + bx + c
            // Line: y = ix + j

            // ax^2 + bx + c = ix + j
            // ax^2 + bx + c - ix - j = 0
            // ax^2 + bx - ix + c - j = 0
            // ax^2 + x(b-i) + (c - j) = 0
            // x = (-b+/-sqrt(b^2+4ac))/(2a)
            // x = (-(b-i)+/-sqrt((b-i)^2+4a(c - j)))/(2a)

            // ToDo: Where does x and y come in here?

            var x1 = (-(b - i) + Sqrt((b - i) * (b - i) + 4 * a * (c - j))) / (2 * a);
            var x2 = (-(b - i) - Sqrt((b - i) * (b - i) + 4 * a * (c - j))) / (2 * a);
            return new (double X, double Y)[] { (x1, Y: (a * (x1 * x1)) + ((b * x1) + c)), (x2, Y: (a * (x2 * x2)) + ((b * x2) + c)) };
        }
    }
}
