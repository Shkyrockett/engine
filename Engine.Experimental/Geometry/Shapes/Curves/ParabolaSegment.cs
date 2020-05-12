// <copyright file="Parabola.cs" company="Shkyrockett" >
//     Copyright © 2015 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The parabola class.
    /// </summary>
    /// <remarks>
    /// <para>References to look at for development:</para>
    /// <list type="bullet">
    /// <item><description><a href="https://www.youtube.com/watch?v=4Af3NBN34ME">https://www.youtube.com/watch?v=4Af3NBN34ME</a></description></item>
    /// <item><description><a href="https://www.mathwarehouse.com/quadratic/parabola/interactive-parabola.php">https://www.mathwarehouse.com/quadratic/parabola/interactive-parabola.php</a></description></item>
    /// <item><description><a href="http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/">http://csharphelper.com/blog/2014/11/draw-a-conic-section-from-its-polynomial-equation-in-c/</a></description></item>
    /// <item><description><a href="http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/">http://csharphelper.com/blog/2014/11/select-a-conic-section-in-c/</a></description></item>
    /// <item><description><a href="http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/">http://csharphelper.com/blog/2014/11/see-where-two-conic-sections-intersect-in-c/</a></description></item>
    /// <item><description><a href="https://stackoverflow.com/questions/21384619/drawing-a-quadratic-curve">https://stackoverflow.com/questions/21384619/drawing-a-quadratic-curve</a></description></item>
    /// <item><description><a href="https://stackoverflow.com/questions/17328322/create-a-parabolic-trajectory-with-fixed-angle">https://stackoverflow.com/questions/17328322/create-a-parabolic-trajectory-with-fixed-angle</a></description></item>
    /// <item><description><a href="https://stackoverflow.com/a/717833">https://stackoverflow.com/a/717833</a></description></item>
    /// <item><description><a href="https://forum.unity.com/threads/generating-dynamic-parabola.211681/">https://forum.unity.com/threads/generating-dynamic-parabola.211681/</a></description></item>
    /// <item><description><a href="https://www.mathwarehouse.com/geometry/parabola/standard-and-vertex-form.php">https://www.mathwarehouse.com/geometry/parabola/standard-and-vertex-form.php</a></description></item>
    /// </list>
    /// </remarks>
    public class ParabolaSegment
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        private double a;

        /// <summary>
        /// 
        /// </summary>
        private double b;

        /// <summary>
        /// 
        /// </summary>
        private double c;

        /// <summary>
        /// 
        /// </summary>
        private double h;

        /// <summary>
        /// 
        /// </summary>
        private double k;
        #endregion

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
        public static (double X, double Y) SampleRotatedParabola(double t, (double X, double Y) start, (double X, double Y) end, double height)
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
                var up = Normalize((end.Y > start.Y) ? -CrossProduct(right, right, travelDirectionX, travelDirectionY) : CrossProduct(right, right, travelDirectionX, travelDirectionY));
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
        public static (double X, double Y, double Z) SampleRotatedParabola(double t, (double X, double Y, double Z) start, (double X, double Y, double Z) end, double height)
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
                up = Normalize(up.X, up.Y, up.Z);
                var result = (X: start.X + (t * travelDirectionX), Y: start.Y + (t * travelDirectionY), Z: start.Z + (t * travelDirectionZ));
                result = (X: result.X + (((-parabolicT * parabolicT) + 1) * height * up.X), Y: result.Y + (((-parabolicT * parabolicT) + 1) * height * up.Y), Z: result.Z + (((-parabolicT * parabolicT) + 1) * height * up.Z));
                return result;
            }
        }

        /// <summary>
        /// Interpolates the parabola standard.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="x2">The x2.</param>
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
        /// Interpolates the parabola vertex.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="x2">The x2.</param>
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
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="b">The <paramref name="b" /> component of the parabola.</param>
        /// <param name="c">The <paramref name="c" /> component of the parabola.</param>
        /// <param name="x1">The <paramref name="x1" /> minimum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2" /> maximum x value to interpolate.</param>
        /// <param name="t">The <paramref name="t" />ime index of the iteration.</param>
        /// <returns>
        /// Returns a <see cref="ValueTuple{T1, T2}" /> representing the interpolated point at the t index.
        /// </returns>
        /// <example>
        ///   <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var b = -2d * a * h;
        /// var c = (b * b / (4 * a)) + k;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)&gt;();
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        /// list.Add(InterpolateVertexParabola(a, b, c, -100, 100, 1d / i));
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
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="h">The horizontal component of the parabola vertex.</param>
        /// <param name="k">The vertical component of the parabola vertex.</param>
        /// <param name="x1">The <paramref name="x1" /> minimum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2" /> maximum x value to interpolate.</param>
        /// <param name="t">The <paramref name="t" />ime index of the iteration.</param>
        /// <returns>
        /// Returns a <see cref="ValueTuple{T1, T2}" /> representing the interpolated point at the t index.
        /// </returns>
        /// <example>
        ///   <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)&gt;();
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        /// list.Add(InterpolateVertexParabola(a, h, k, -100, 100, 1d / i));
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
            //var x1 = x + 1;
            // ToDo: This should be finding the tangent ray.
            //var y1 = (x1 * ((a * x1) + b)) + c;
            //var y = (((2d * a * x1) + b) * (x - x1)) + y1;
            => (x, (x * ((a * x) + b)) + c, 0, 0);

        /// <summary>
        /// Parabolas the normal.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://math.stackexchange.com/a/1258196
        /// https://www.emathzone.com/tutorials/geometry/equation-of-tangent-and-normal-to-parabola.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double x, double y, double i, double j) ParabolaNormal(double a, double b, double c, double x)
        {
            _ = a;
            _ = b;
            _ = c;
            _ = x;

            // ToDo: Figure out how to find the Normal of a point on a parabola.
            return (0, 0, 0, 0);
        }

        /// <summary>
        /// Lines the slope intercept to standard.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://sciencing.com/how-to-convert-slope-intercept-form-to-standard-form-13712257.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double a, double b, double c) LineSlopeInterceptToStandard(double m, double b)
        {
            _ = m;
            _ = b;

            // ToDo: Work out how to convert from slope intercept form to standard form for a line.
            return (0, 0, 0);
        }

        /// <summary>
        /// Lines the standard to slope intercept.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://sciencing.com/how-to-convert-slope-intercept-form-to-standard-form-13712257.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (double m, double b) LineStandardToSlopeIntercept(double a, double b, double c)
        {
            _ = a;
            _ = b;
            _ = c;
            // ToDo: Work out how to convert from standard form to slope intercept form for a line.
            return (0, 0);
        }

        /// <summary>
        /// Intersections the parabola line.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.youtube.com/watch?v=t6n-ShpFFjo
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y)[] IntersectionParabolaLine(double a, double b, double c, double x, double y, double i, double j)
        {
            _ = x;
            _ = y;

            // Parabola: y = ax^2 + bx + c
            // Line: y = ix + j

            // ax^2 + bx + c = ix + j
            // ax^2 + bx + c - ix - j = 0
            // ax^2 + bx - ix + c - j = 0
            // ax^2 + x(b-i) + (c - j) = 0
            // x = (-b+/-sqrt(b^2+4ac))/(2a)
            // x = (-(b-i)+/-sqrt((b-i)^2+4a(c - j)))/(2a)

            // ToDo: Figure out how to get this working properly.
            // ToDo: Where does x and y come in here?

            var x1 = (-(b - i) + Sqrt(((b - i) * (b - i)) + (4 * a * (c - j)))) / (2 * a);
            var x2 = (-(b - i) - Sqrt(((b - i) * (b - i)) + (4 * a * (c - j)))) / (2 * a);
            return new (double X, double Y)[] { (x1, Y: (a * (x1 * x1)) + ((b * x1) + c)), (x2, Y: (a * (x2 * x2)) + ((b * x2) + c)) };
        }

        /// <summary>
        /// http://www.aboutmech.com/2014/06/equation-of-path-of-projectile.html
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="v">The v.</param>
        /// <param name="a">a.</param>
        /// <param name="g">The g.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) TrajectoryStep(double x, double y, double v, double a, double g = 9.81d) => (x, y + (x * Tan(a)) - (g * x * x / (2d * v * v * Cos(a) * Cos(a))));

        /// <summary>
        /// Calculates a projected parabola from a trajectory. http://www.calctool.org/CALC/phys/newtonian/projectile
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="gravity">The gravity.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Height, double Distance, double Time) TrajectoryToParabola(double x, double angle, double velocity, double gravity = 9.80665)
        {
            var b = velocity * velocity * Sin(angle * 0.5d) / gravity;
            var cos = Cos(angle);
            return (
                x + b * 0.5d * Tan(angle) - (gravity * 0.5d * (b * 0.5d / velocity * cos) * (b * 0.5d / velocity * cos)),
                b,
                b / (velocity * cos)
                );
        }
    }
}
