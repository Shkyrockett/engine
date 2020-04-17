/*
  A port of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine.Experimental
{
    /// <summary>
    /// The derivative method double.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    public delegate double DerivitiveMethodDouble(double x);

    /// <summary>
    /// The derivative method2d.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    public delegate Point2D DerivitiveMethod2D(double x);

    /// <summary>
    /// The derivative method3d.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <returns>
    /// The <see cref="Point3D" />.
    /// </returns>
    public delegate Point3D DerivitiveMethod3D(double x);

    /// <summary>
    /// The bezier maths class.
    /// </summary>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    public static class BezierUtil
    {
        #region Gauss Tables
        /// <summary>
        /// Legendre-Gauss abscissae with n=24 (x_i values, defined at i=n as the roots of the nth order Legendre polynomial Pn(x))
        /// </summary>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        public static readonly double[] Tvalues = new double[]
        {
            // 
            -0.0640568928626056260850430826247450385909,
             0.0640568928626056260850430826247450385909,
            // 
            -0.1911188674736163091586398207570696318404,
             0.1911188674736163091586398207570696318404,
            //
            -0.3150426796961633743867932913198102407864,
             0.3150426796961633743867932913198102407864,
            //
            -0.4337935076260451384870842319133497124524,
             0.4337935076260451384870842319133497124524,
            //
            -0.5454214713888395356583756172183723700107,
             0.5454214713888395356583756172183723700107,
            //
            -0.6480936519369755692524957869107476266696,
             0.6480936519369755692524957869107476266696,
            //
            -0.7401241915785543642438281030999784255232,
             0.7401241915785543642438281030999784255232,
            //
            -0.8200019859739029219539498726697452080761,
             0.8200019859739029219539498726697452080761,
            //
            -0.8864155270044010342131543419821967550873,
             0.8864155270044010342131543419821967550873,
            //
            -0.9382745520027327585236490017087214496548,
             0.9382745520027327585236490017087214496548,
            //
            -0.9747285559713094981983919930081690617411,
             0.9747285559713094981983919930081690617411,
            //
            -0.9951872199970213601799974097007368118745,
             0.9951872199970213601799974097007368118745
        };

        /// <summary>
        /// Legendre-Gauss weights with n=24 (w_i values, defined by a function linked to in the Bezier primer article)
        /// </summary>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        public static readonly double[] Cvalues = new double[]
        {
            //
            0.1279381953467521569740561652246953718517d,
            0.1279381953467521569740561652246953718517d,
            //
            0.1258374563468282961213753825111836887264d,
            0.1258374563468282961213753825111836887264d,
            //
            0.1216704729278033912044631534762624256070d,
            0.1216704729278033912044631534762624256070d,
            //
            0.1155056680537256013533444839067835598622d,
            0.1155056680537256013533444839067835598622d,
            //
            0.1074442701159656347825773424466062227946d,
            0.1074442701159656347825773424466062227946d,
            //
            0.0976186521041138882698806644642471544279d,
            0.0976186521041138882698806644642471544279d,
            //
            0.0861901615319532759171852029837426671850d,
            0.0861901615319532759171852029837426671850d,
            //
            0.0733464814110803057340336152531165181193d,
            0.0733464814110803057340336152531165181193d,
            //
            0.0592985849154367807463677585001085845412d,
            0.0592985849154367807463677585001085845412d,
            //
            0.0442774388174198061686027482113382288593d,
            0.0442774388174198061686027482113382288593d,
            //
            0.0285313886289336631813078159518782864491d,
            0.0285313886289336631813078159518782864491d,
            //
            0.0123412297999871995468056670700372915759d,
            0.0123412297999871995468056670700372915759d
        };
        #endregion Gauss Tables

        /// <summary>
        /// The length.
        /// </summary>
        /// <param name="derivativeFn">The derivativeFn.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(DerivitiveMethod2D derivativeFn)
        {
            double z = 0.5, sum = 0, len = Tvalues.Length, t;
            for (var i = 0; i < len; i++)
            {
                t = (z * Tvalues[i]) + z;
                sum += Cvalues[i] * Arcfn(t, derivativeFn);
            }

            return z * sum;
        }

        /// <summary>
        /// The length.
        /// </summary>
        /// <param name="derivativeFn">The derivativeFn.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(DerivitiveMethod3D derivativeFn)
        {
            double z = 0.5, sum = 0, len = Tvalues.Length, t;
            for (var i = 0; i < len; i++)
            {
                t = (z * Tvalues[i]) + z;
                sum += Cvalues[i] * Arcfn(t, derivativeFn);
            }

            return z * sum;
        }

        /// <summary>
        /// The arcfn.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="derivativeFn">The derivativeFn.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Arcfn(double t, DerivitiveMethod2D derivativeFn)
        {
            var d = derivativeFn(t);
            var l = (d.X * d.X) + (d.Y * d.Y);
            return Sqrt(l);
        }

        /// <summary>
        /// The arcfn.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="derivativeFn">The derivativeFn.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Arcfn(double t, DerivitiveMethod3D derivativeFn)
        {
            var d = derivativeFn(t);
            var l = (d.X * d.X) + (d.Y * d.Y) + (d.Z * d.Z);
            return Sqrt(l);
        }

        /// <summary>
        /// Align points.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="line">The line.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#aligning
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> AlignPoints(List<Point2D> points, Line2D line)
        {
            if (points is null) return null;
            var angle = -Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X);
            var sinA = Sin(angle);
            var cosA = Cos(angle);

            var results = new List<Point2D>();

            foreach (var point in points)
            {
                results.Add(new Point2D(
                    ((point.X - line.P1.X) * cosA) - ((point.Y - line.P1.Y) * sinA),
                    ((point.X - line.P1.X) * sinA) + ((point.Y - line.P1.Y) * cosA))
                    );
            }

            return results;
        }

        /// <summary>
        /// The align.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="line">The line.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#aligning
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point3D> Align(List<Point3D> points, Line3D line)
        {
            if (points is null) return null;
            var angle = -Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X);
            var cosA = Cos(angle);
            var sinA = Sin(angle);

            var results = new List<Point3D>();

            foreach (var point in points)
            {
                results.Add(new Point3D(
                    ((point.X - line.P1.X) * cosA) - ((point.Y - line.P1.Y) * sinA),
                    ((point.X - line.P1.X) * sinA) + ((point.Y - line.P1.Y) * cosA),
                    0)
                    );
            }

            return results;
        }

        /// <summary>
        /// The angle.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(Point2D o, Point2D v1, Point2D v2)
        {
            var dx1 = v1.X - o.X;
            var dy1 = v1.Y - o.Y;
            var dx2 = v2.X - o.X;
            var dy2 = v2.Y - o.Y;

            var cross = (dx1 * dy2) - (dy1 * dx2);

            var m1 = Sqrt((dx1 * dx1) + (dy1 * dy1));
            var m2 = Sqrt((dx2 * dx2) + (dy2 * dy2));

            dx1 /= m1;
            dy1 /= m1;
            dx2 /= m2;
            dy2 /= m2;

            var dot = (dx1 * dx2) + (dy1 * dy2);

            return Atan2(cross, dot);
        }

        /// <summary>
        /// The angle.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(Point3D o, Point3D v1, Point3D v2)
        {
            var dx1 = v1.X - o.X;
            var dy1 = v1.Y - o.Y;
            var dx2 = v2.X - o.X;
            var dy2 = v2.Y - o.Y;

            var cross = (dx1 * dy2) - (dy1 * dx2);

            var m1 = Sqrt((dx1 * dx1) + (dy1 * dy1));
            var m2 = Sqrt((dx2 * dx2) + (dy2 * dy2));

            dx1 /= m1;
            dy1 /= m1;
            dx2 /= m2;
            dy2 /= m2;

            var dot = (dx1 * dx2) + (dy1 * dy2);

            return Atan2(cross, dot);
        }

        /// <summary>
        /// The projection ratio.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ProjectionRatio(double t = 0.5d, double n = 0.5d)
        {
            // see u(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }

            if (t == 0.5d)
            {
                t = 0.5d;
            }
            else if (t == 0 || t == 1)
            {
                return t;
            }

            var top = Pow(1 - t, n);
            var bottom = Pow(t, n) + top;
            return top / bottom;
        }

        /// <summary>
        /// The abcratio.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="n">The n.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbcRatio(double t = 0.5d, double n = 0.5d)
        {
            // see ratio(t) note on http://pomax.github.io/bezierinfo/#abc
            if (n != 2 && n != 3)
            {
                return double.NaN;
            }

            if (t == 0.5d)
            {
                t = 0.5;
            }
            else if (t == 0 || t == 1)
            {
                return t;
            }

            var bottom = Pow(t, n) + Pow(1 - t, n);
            var top = bottom - 1;
            return Abs(top / bottom);
        }

        /// <summary>
        /// Get the ABC.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="S">The S.</param>
        /// <param name="B">The B.</param>
        /// <param name="E">The E.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Point2D, Point2D, Point2D) GetABC(double n, Point2D S, Point2D B, Point2D E, double t = 0.5d)
        {
            var u = ProjectionRatio(t, n);
            var um = 1 - u;
            var C = new Point2D(
                x: (u * S.X) + (um * E.X),
                y: (u * S.Y) + (um * E.Y)
            );
            var s = AbcRatio(t, n);
            var A = new Point2D(
                x: B.X + ((B.X - C.X) / s),
                y: B.Y + ((B.Y - C.Y) / s)
            );
            return (A, B, C);
        }

        /// <summary>
        /// Get the ABC.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="S">The S.</param>
        /// <param name="B">The B.</param>
        /// <param name="E">The E.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Point3D, Point3D, Point3D) GetABC(double n, Point3D S, Point3D B, Point3D E, double t = 0.5d)
        {
            var u = ProjectionRatio(t, n);
            var um = 1 - u;
            var C = new Point3D(
                x: (u * S.X) + (um * E.X),
                y: (u * S.Y) + (um * E.Y),
                z: (u * S.Z) + (um * E.Z)
            );
            var s = AbcRatio(t, n);
            var A = new Point3D(
                x: B.X + ((B.X - C.X) / s),
                y: B.Y + ((B.Y - C.Y) / s),
                z: B.Z + ((B.Z - C.Z) / s)
            );
            return (A, B, C);
        }

        /// <summary>
        /// Get the min max.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <param name="d">The d.</param>
        /// <param name="list">The list.</param>
        /// <returns>
        /// The <see cref="RangeX" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RangeX GetMinMax(Bezier curve, int d, List<double> list)
        {
            if (curve is null) return null;
            if (list is null)
            {
                return new RangeX(min: 0, max: 0);
            }

            double min = 0xFFFFFFFFFFFFFFFF;
            var max = -min;
            double t;
            Point2D c;
            if (list.IndexOf(0) == -1)
            {
                list.Insert(0, 0);
            }

            if (list.IndexOf(1) == -1)
            {
                list.Add(1);
            }

            for (int i = 0, len = list.Count; i < len; i++)
            {
                t = list[i];
                c = curve.Interpolate_Ported(t);
                switch (d)
                {
                    case 0:
                        if (c.X < min)
                        {
                            min = c.X;
                        }

                        if (c.X > max)
                        {
                            max = c.X;
                        }

                        break;
                    case 1:
                        if (c.Y < min)
                        {
                            min = c.Y;
                        }

                        if (c.Y > max)
                        {
                            max = c.Y;
                        }

                        break;
                    //case 2:
                    //    if (c.Z < min)
                    //        min = c.Z;
                    //    if (c.Z > max)
                    //        max = c.Z;
                    //    break;
                    default:
                        break;
                }
            }
            return new RangeX(min: min, mid: (min + max) / 2, max: max, size: max - min);
        }

        /// <summary>
        /// The getccenter.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <returns>
        /// The <see cref="Arc2D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Arc2D Getccenter(Point2D p1, Point2D p2, Point2D p3)
        {
            var dx1 = p2.X - p1.X;
            var dy1 = p2.Y - p1.Y;
            var dx2 = p3.X - p2.X;
            var dy2 = p3.Y - p2.Y;
            var dx1p = (dx1 * Cos(Quart)) - (dy1 * Sin(Quart));
            var dy1p = (dx1 * Sin(Quart)) + (dy1 * Cos(Quart));
            var dx2p = (dx2 * Cos(Quart)) - (dy2 * Sin(Quart));
            var dy2p = (dx2 * Sin(Quart)) + (dy2 * Cos(Quart));
            // chord midpoints
            var mx1 = (p1.X + p2.X) * 0.5d;
            var my1 = (p1.Y + p2.Y) * 0.5d;
            var mx2 = (p2.X + p3.X) * 0.5d;
            var my2 = (p2.Y + p3.Y) * 0.5d;
            // midpoint offsets
            var mx1n = mx1 + dx1p;
            var my1n = my1 + dy1p;
            var mx2n = mx2 + dx2p;
            var my2n = my2 + dy2p;
            // intersection of these lines:
            var arcCenter = Lli8(mx1, my1, mx1n, my1n, mx2, my2, mx2n, my2n);
            var r = Measurements.Distance(arcCenter.Value, p1);
            // arc start/end values, over mid point:
            var s = Atan2(p1.Y - arcCenter.Value.Y, p1.X - arcCenter.Value.X);
            var m = Atan2(p2.Y - arcCenter.Value.Y, p2.X - arcCenter.Value.X);
            var e = Atan2(p3.Y - arcCenter.Value.Y, p3.X - arcCenter.Value.X);
            double _;
            // determine arc direction (cw/ccw correction)
            if (s < e)
            {
                // if s<m<e, arc(s, e)
                // if m<s<e, arc(e, s + tau)
                // if s<e<m, arc(e, s + tau)
                if (s > m || m > e)
                {
                    s += Tau;
                }

                if (s > e)
                { _ = e; e = s; s = _; }
            }
            else
            {
                // if e<m<s, arc(e, s)
                // if m<e<s, arc(s, e + tau)
                // if e<s<m, arc(s, e + tau)
                if (e < m && m < s)
                { _ = e; e = s; s = _; }
                else
                { e += Tau; }
            }
            // assign and done.
            var arc = new Arc2D
            {
                Center = arcCenter.Value,
                Start = s,
                End = e,
                Radius = r
            };
            return arc;
        }

        /// <summary>
        /// The getccenter.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <returns>
        /// The <see cref="Arc3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Arc3D Getccenter(Point3D p1, Point3D p2, Point3D p3)
        {
            var dx1 = p2.X - p1.X;
            var dy1 = p2.Y - p1.Y;
            var dx2 = p3.X - p2.X;
            var dy2 = p3.Y - p2.Y;
            var dx1p = (dx1 * Cos(Quart)) - (dy1 * Sin(Quart));
            var dy1p = (dx1 * Sin(Quart)) + (dy1 * Cos(Quart));
            var dx2p = (dx2 * Cos(Quart)) - (dy2 * Sin(Quart));
            var dy2p = (dx2 * Sin(Quart)) + (dy2 * Cos(Quart));
            // chord midpoints
            var mx1 = (p1.X + p2.X) * 0.5d;
            var my1 = (p1.Y + p2.Y) * 0.5d;
            var mx2 = (p2.X + p3.X) * 0.5d;
            var my2 = (p2.Y + p3.Y) * 0.5d;
            // midpoint offsets
            var mx1n = mx1 + dx1p;
            var my1n = my1 + dy1p;
            var mx2n = mx2 + dx2p;
            var my2n = my2 + dy2p;
            // intersection of these lines:
            var arcCenter = Lli8(mx1, my1, 0, mx1n, my1n, 0, mx2, my2, 0, mx2n, my2n, 0);
            var r = Measurements.Distance(arcCenter.Value, p1);
            // arc start/end values, over mid point:
            var s = Atan2(p1.Y - arcCenter.Value.Y, p1.X - arcCenter.Value.X);
            var m = Atan2(p2.Y - arcCenter.Value.Y, p2.X - arcCenter.Value.X);
            var e = Atan2(p3.Y - arcCenter.Value.Y, p3.X - arcCenter.Value.X);
            double _;
            // determine arc direction (cw/ccw correction)
            if (s < e)
            {
                // if s<m<e, arc(s, e)
                // if m<s<e, arc(e, s + tau)
                // if s<e<m, arc(e, s + tau)
                if (s > m || m > e)
                {
                    s += Tau;
                }

                if (s > e)
                { _ = e; e = s; s = _; }
            }
            else
            {
                // if e<m<s, arc(e, s)
                // if m<e<s, arc(s, e + tau)
                // if e<s<m, arc(s, e + tau)
                if (e < m && m < s)
                { _ = e; e = s; s = _; }
                else
                { e += Tau; }
            }
            // assign and done.
            var arc = new Arc3D
            {
                Center = arcCenter.Value,
                Start = s,
                End = e,
                Radius = r
            };
            return arc;
        }

        /// <summary>
        /// The inflections.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#inflections
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> Inflections(List<Point2D> points)
        {
            if (points is null) return null;
            var p = AlignPoints(points, new Line2D(p1: points[0], p2: points[3]));
            var a = p[2].X * p[1].Y;
            var b = p[3].X * p[1].Y;
            var c = p[1].X * p[2].Y;
            var d = p[3].X * p[2].Y;
            var v1 = 18 * ((-3 * a) + (2 * b) + (3 * c) - d);
            var v2 = 18 * ((3 * a) - b - (3 * c));
            var v3 = 18 * (c - a);

            if (Approximately(v1, 0))
            {
                return new List<double>();
            }

            var descriminant = (v2 * v2) - (4 * v1 * v3);
            var sq = Sqrt(descriminant);
            d = 2 * v1;

            return Approximately(d, 0)
                ? new List<double>()
                : new List<double>(
                from r in new List<double> { (sq - v2) / d, -(v2 + sq) / d }
                where 0 <= r && r <= 1
                select r
                );
        }

        /// <summary>
        /// The inflections.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> Inflections(List<Point3D> points)
        {
            if (points is null) return null;
            var p = Align(points, new Line3D(p1: points[0], p2: points[3]));
            var a = p[2].X * p[1].Y;
            var b = p[3].X * p[1].Y;
            var c = p[1].X * p[2].Y;
            var d = p[3].X * p[2].Y;
            var v1 = 18 * ((-3 * a) + (2 * b) + (3 * c) - d);
            var v2 = 18 * ((3 * a) - b - (3 * c));
            var v3 = 18 * (c - a);

            if (Approximately(v1, 0))
            {
                return new List<double>();
            }

            var trm = (v2 * v2) - (4 * v1 * v3);
            var sq = Sqrt(trm);
            d = 2 * v1;

            return Approximately(d, 0)
                ? new List<double>()
                : new List<double>(
                from r in new List<double> { (sq - v2) / d, -(v2 + sq) / d }
                where 0 <= r && r <= 1
                select r
                );
        }

        /// <summary>
        /// The closest.
        /// </summary>
        /// <param name="lookUpTable">The lookUpTable.</param>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Closest(List<Point2D> lookUpTable, Point2D point)
        {
            var mdist = Pow(2, 63);
            double mpos = 0;
            for (var i = 0; i < lookUpTable.Count; i++)
            {
                var d = Measurements.Distance(point, lookUpTable[i]);
                if (d < mdist)
                {
                    mdist = d;
                    mpos = i;
                }
            }

            return (mdist, mpos);
        }

        /// <summary>
        /// The closest.
        /// </summary>
        /// <param name="lookUpTable">The lookUpTable.</param>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Closest(List<Point3D> lookUpTable, Point3D point)
        {
            var mdist = Pow(2, 63);
            double mpos = 0;
            for (var i = 0; i < lookUpTable.Count; i++)
            {
                var d = Measurements.Distance(point, lookUpTable[i]);
                if (d < mdist)
                {
                    mdist = d;
                    mpos = i;
                }
            }

            return (mdist, mpos, 0);
        }

        /// <summary>
        /// The compute bezier.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/25458216
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ComputeBezier(double a, double b, double c, double d, double t)
        {
            var ti = 1 - t;
            return (a * ti * ti * ti) + (3 * b * ti * ti * t) + (3 * c * (1 - t) * t * t) + (d * t * t * t);
        }

        /// <summary>
        /// The compute cubic bezier derivative.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/25458216
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ComputeCubicBezierDerivative(double a, double b, double c, double d, double t)
        {
            var ti = 1d - t;
            a = 3d * (b - a);
            b = 3d * (c - b);
            c = 3d * (d - c);
            return (a * ti * ti) + (2d * b * ti * t) + (3d * c * t * t);
        }

        /// <summary>
        /// The linear distance function.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The e.</param>
        /// <param name="tlen">The tlen.</param>
        /// <param name="alen">The alen.</param>
        /// <param name="slen">The slen.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearDistanceFunction(double s, double e, double tlen, double alen, double slen)
        {
            const double v = 0;
            var f1 = alen / tlen;
            var f2 = (alen + slen) / tlen;
            var d = e - s;
            return Map(v, 0, 1, s + (f1 * d), s + (f2 * d));
        }

        /// <summary>
        /// The lli.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D? Lli(Point2D v1, Point2D v2)
        // return utils.lli4(v1,v1.c,v2,v2.c);
            => Lli4(v1, v1, v2, v2);

        /// <summary>
        /// The lli.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns>
        /// The <see cref="Point3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D? Lli(Point3D v1, Point3D v2)
        // return utils.lli4(v1,v1.c,v2,v2.c);
            => Lli4(v1, v1, v2, v2);

        /// <summary>
        /// The lli4.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D? Lli4(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
            => Lli8(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y);

        /// <summary>
        /// The lli4.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="p3">The p3.</param>
        /// <param name="p4">The p4.</param>
        /// <returns>
        /// The <see cref="Point3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D? Lli4(Point3D p1, Point3D p2, Point3D p3, Point3D p4)
            => Lli8(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z, p3.X, p3.Y, p3.Z, p4.X, p4.Y, p4.Z);

        /// <summary>
        /// The lli8.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D? Lli8(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            var nx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
            var ny = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));
            var d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
            return d == 0 ? null : (Point2D?)new Point2D(nx / d, ny / d);
        }

        /// <summary>
        /// The lli8.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        /// <param name="z4">The z4.</param>
        /// <returns>
        /// The <see cref="Point3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D? Lli8(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3, double x4, double y4, double z4)
        {
            var nx = (((x1 * y2) - (y1 * x2)) * (x3 - x4)) - ((x1 - x2) * ((x3 * y4) - (y3 * x4)));
            var ny = (((x1 * y2) - (y1 * x2)) * (y3 - y4)) - ((y1 - y2) * ((x3 * y4) - (y3 * x4)));
            var d = ((x1 - x2) * (y3 - y4)) - ((y1 - y2) * (x3 - x4));
            return d == 0 ? null : (Point3D?)new Point3D(nx / d, ny / d, 0);
        }

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="ds">The ds.</param>
        /// <param name="de">The de.</param>
        /// <param name="ts">The ts.</param>
        /// <param name="te">The te.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Map(double v, double ds, double de, double ts, double te)
        {
            var d1 = de - ds;
            var d2 = te - ts;
            var v2 = v - ds;
            var r = v2 / d1;
            return ts + (d2 * r);
        }

        /// <summary>
        /// Copy.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="Point3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Copy(Point2D obj)
            => new Point2D(obj);

        /// <summary>
        /// Copy.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="Point3D" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Copy(Point3D obj)
            => new Point3D(obj);

        /// <summary>
        /// The make line.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns>
        /// The <see cref="Bezier" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bezier MakeLine(Point2D p1, Point2D p2)
        {
            var x1 = p1.X;
            var y1 = p1.Y;
            var x2 = p2.X;
            var y2 = p2.Y;
            var dx = (x2 - x1) / 3d;
            var dy = (y2 - y1) / 3d;
            return new Bezier(x1, y1, x1 + dx, y1 + dy, x1 + (2d * dx), y1 + (2d * dy), x2, y2);
        }

        /// <summary>
        /// The make line.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns>
        /// The <see cref="Bezier" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bezier MakeLine(Point3D p1, Point3D p2)
        {
            var x1 = p1.X;
            var y1 = p1.Y;
            var x2 = p2.X;
            var y2 = p2.Y;
            var dx = (x2 - x1) / 3d;
            var dy = (y2 - y1) / 3d;
            return new Bezier(x1, y1, x1 + dx, y1 + dy, x1 + (2d * dx), y1 + (2d * dy), x2, y2);
        }

        /// <summary>
        /// The make shape.
        /// </summary>
        /// <param name="forward">The forward.</param>
        /// <param name="back">The back.</param>
        /// <returns>
        /// The <see cref="Shape1" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Shape1 MakeShape(Bezier forward, Bezier back)
        {
            if (forward is null || back is null) return null;
            var bpl = back.Points.Count;
            var fpl = forward.Points.Count;
            var start = MakeLine(back.Points[bpl - 1], forward.Points[0]);
            var end = MakeLine(forward.Points[fpl - 1], back.Points[0]);
            var shape = new Shape1(
                startcap: start,
                forward: forward,
                back: back,
                endcap: end,
                bbox: FindBoundingBox(new List<Bezier> { start, forward, back, end })
              );
            //shape.intersections = new Shape2D.IntersectionsDelegate(Bezier s2)
            //{
            //    return shapeintersections(shape, shape.bbox, s2, s2.bbox);
            //};
            return shape;
        }

        /// <summary>
        /// Find the bounding box.
        /// </summary>
        /// <param name="sections">The sections.</param>
        /// <returns>
        /// The <see cref="BBox" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BBox FindBoundingBox(List<Bezier> sections)
        {
            if (sections is null) return null;
            var mx = 99999999d;
            var my = mx;
            var MX = -mx;
            var MY = MX;
            foreach (var s in sections)
            {
                var bbox = s.Bbox();
                if (mx > bbox.X.Min)
                {
                    mx = bbox.X.Min;
                }

                if (my > bbox.Y.Min)
                {
                    my = bbox.Y.Min;
                }

                if (MX < bbox.X.Max)
                {
                    MX = bbox.X.Max;
                }

                if (MY < bbox.Y.Max)
                {
                    MY = bbox.Y.Max;
                }
            }

            return new BBox(
                x: new RangeX(min: mx, mid: (mx + MX) / 2, max: MX, size: MX - mx),
                y: new RangeX(min: my, mid: (my + MY) / 2, max: MY, size: MY - my)
            );
        }

        /// <summary>
        /// The bboxoverlap.
        /// </summary>
        /// <param name="b1">The b1.</param>
        /// <param name="b2">The b2.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Bboxoverlap(BBox b1, BBox b2)
        {
            if (b1 is null || b2 is null) return false;
            //var dims = new List<int> { 0/*X*/, 1/*Y*/ };
            //double len = dims.Count;
            double l;
            double t;
            double d;
            l = b1.X.Mid;
            t = b2.X.Mid;
            d = (b1.X.Size + b2.X.Size) / 2d;
            if (Abs(l - t) >= d)
            {
                return false;
            }

            l = b1.Y.Mid;
            t = b2.Y.Mid;
            d = (b1.Y.Size + b2.Y.Size) / 2d;
            return Abs(l - t) < d;
        }

        /// <summary>
        /// The expandbox.
        /// </summary>
        /// <param name="bbox">The bbox.</param>
        /// <param name="_bbox">The _bbox.</param>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Expandbox(BBox bbox, BBox _bbox)
        {
            if (bbox is null || _bbox is null) return;
            if (_bbox.X.Min < bbox.X.Min)
            {
                bbox.X.Min = _bbox.X.Min;
            }

            if (_bbox.Y.Min < bbox.Y.Min)
            {
                bbox.Y.Min = _bbox.Y.Min;
            }

            if (_bbox.Z != null && _bbox.Z.Min < bbox.Z.Min)
            {
                bbox.Z.Min = _bbox.Z.Min;
            }

            if (_bbox.X.Max > bbox.X.Max)
            {
                bbox.X.Max = _bbox.X.Max;
            }

            if (_bbox.Y.Max > bbox.Y.Max)
            {
                bbox.Y.Max = _bbox.Y.Max;
            }

            if (_bbox.Z != null && _bbox.Z.Max > bbox.Z.Max)
            {
                bbox.Z.Max = _bbox.Z.Max;
            }

            bbox.X.Mid = (bbox.X.Min + bbox.X.Max) / 2d;
            bbox.Y.Mid = (bbox.Y.Min + bbox.Y.Max) / 2d;
            if (bbox.Z != null)
            {
                bbox.Z.Mid = (bbox.Z.Min + bbox.Z.Max) / 2d;
            }

            bbox.X.Size = bbox.X.Max - bbox.X.Min;
            bbox.Y.Size = bbox.Y.Max - bbox.Y.Min;
            if (bbox.Z != null)
            {
                bbox.Z.Size = bbox.Z.Max - bbox.Z.Min;
            }
        }

        /// <summary>
        /// The pairiteration.
        /// </summary>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Pair> Pairiteration(Bezier c1, Bezier c2)
        {
            if (c1 is null || c2 is null) return null;
            var c1b = c1.Bbox();
            var c2b = c2.Bbox();
            //double r = 100000;
            const double threshold = 0.5;
            if (c1b.X.Size + c1b.Y.Size < threshold && c2b.X.Size + c2b.Y.Size < threshold)
            {
                //return new List<Pair>() { ((r * (c1._t1 + c1._t2) / 2) | 0d) / r + "/" + ((r * (c2._t1 + c2._t2) / 2) | 0) / r };
            }
            //var cc1 = Bezier.Split(0.5);
            //var cc2 = Bezier.Split(0.5);

            //var pairs = new List<Pair>(
            //    from pair in new List<Pair> {
            //    new Pair(left: cc1.Left, right: cc2.Left),
            //    new Pair(left: cc1.Left, right: cc2.Right),
            //    new Pair(left: cc1.Right, right: cc2.Right),
            //    new Pair(left: cc1.Right, right: cc2.Left)}
            //    where Bboxoverlap(pair.Left.Bbox(), pair.Right.Bbox())
            //    select pair);

            var results = new List<Pair>();
            //if (pairs.Count == 0)
            //    return results;

            //foreach (Pair pair in pairs)
            //    results.AddRange(Pairiteration(pair.Left, pair.Right));

            return (List<Pair>)new List<Pair>(
                from v in results
                select v).Distinct();
        }

        /// <summary>
        /// The shape intersections.
        /// </summary>
        /// <param name="s1">The s1.</param>
        /// <param name="bbox1">The bbox1.</param>
        /// <param name="s2">The s2.</param>
        /// <param name="bbox2">The bbox2.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Pair> ShapeIntersections(Shape1 s1, BBox bbox1, Shape1 s2, BBox bbox2)
        {
            if (!Bboxoverlap(bbox1, bbox2))
            {
                return new List<Pair>();
            }

            var intersections = new List<Pair>();
            var a1 = new List<Bezier> { s1.Startcap, s1.Forward, s1.Back, s1.Endcap };
            var a2 = new List<Bezier> { s2.Startcap, s2.Forward, s2.Back, s2.Endcap };
            foreach (var l1 in a1)
            {
                if (l1.Virtual)
                {
                    return new List<Pair>();
                }

                foreach (var l2 in a2)
                {
                    if (l2.Virtual)
                    {
                        return new List<Pair>();
                    }

                    var iss = l1.Intersects(l2);
                    foreach (var i in iss)
                    {
                        if (i.Length > 0)
                        {
                            i.Left = l1;
                            i.Right = l2;
                            i.S1 = s1;
                            i.S2 = s2;
                            intersections.Add(i);
                        }
                    }
                }
            }
            return intersections;
        }

        /// <summary>
        /// The roots.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="line">The line.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> Roots(List<Point2D> points, Line2D line)
        {
            if (points is null) return null;
            //line = line || new Line(p1: new Point2D(x: 0, y: 0), p2: new Point2D(x: 1, y: 0));
            var order = points.Count - 1;
            var pts = AlignPoints(points, line);

            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;

            double m1 = 0;
            double m2 = 0;

            if (order == 2)
            {
                a = pts[0].Y;
                b = pts[1].Y;
                c = pts[2].Y;
                d = a - (2d * b) + c;
                if (d != 0)
                {
                    m1 = -Sqrt((b * b) - (a * c));
                    m2 = -a + b;
                    var v1_ = -(m1 + m2) / d;
                    var v2_ = -(-m1 + m2) / d;

                    return new List<double>(
                        from t0 in new List<double> { v1_, v2_ }
                        where 0 <= t0 && t0 <= 1
                        select t0
                    );
                }
                else if (b != c && d == 0)
                {
                    return new List<double>(
                        from t1 in new List<double> { ((2d * b) - c) / 2d * (b - c) }
                        where 0 <= t1 && t1 <= 1
                        select t1
                    );
                }
                return new List<double>();
            }

            // see http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
            var pa = pts[0].Y;
            var pb = pts[1].Y;
            var pc = pts[2].Y;
            var pd = pts[3].Y;
            d = -pa + (3d * pb) - (3d * pc) + pd;
            a = ((3d * pa) - (6d * pb) + (3d * pc)) / d;
            b = ((-3d * pa) + (3d * pb)) / d;
            c = pa / d;
            var p_ = ((3d * b) - (a * a)) / 3d;
            var p3 = p_ / 3d;
            var q = ((2d * a * a * a) - (9d * a * b) + (27d * c)) / 27d;
            var q2 = q / 2d;
            var discriminant = (q2 * q2) + (p3 * p3 * p3);
            double u1, v1, x1, x2, x3;
            if (discriminant < 0)
            {
                var mp3 = -p_ / 3d;
                var mp33 = mp3 * mp3 * mp3;
                var r = Sqrt(mp33);
                var t = -q / (2d * r);
                var cosphi = t < -1d ? -1d : t > 1d ? 1d : t;
                var phi = Acos(cosphi);
                var crtr = Crt(r);
                var t1 = 2d * crtr;
                x1 = (t1 * Cos(phi / 3d)) - (a / 3d);
                x2 = (t1 * Cos((phi + Tau) / 3d)) - (a / 3d);
                x3 = (t1 * Cos((phi + (2d * Tau)) / 3d)) - (a / 3d);

                return new List<double>(
                    from t2 in new List<double> { x1, x2, x3 }
                    where 0d <= t2 && t2 <= 1d
                    select t2
                );
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0d ? Crt(-q2) : -Crt(q2);
                x1 = (2d * u1) - (a / 3d);
                x2 = -u1 - (a / 3d);

                return new List<double>(
                    from t3 in new List<double> { x1, x2 }
                    where 0d <= t3 && t3 <= 1d
                    select t3
                );
            }
            else
            {
                var sd = Sqrt(discriminant);
                u1 = Crt(-q2 + sd);
                v1 = Crt(q2 + sd);

                return new List<double>(
                    from t4 in new List<double> { u1 - v1 - (a / 3d) }
                    where 0d <= t4 && t4 <= 1d
                    select t4
                );
            }
        }

        /// <summary>
        /// The roots.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="line">The line.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> Roots(List<Point3D> points, Line3D line)
        {
            if (points is null) return null;
            //line = line || new Line(p1: new Point2D(x: 0, y: 0), p2: new Point2D(x: 1, y: 0));
            var order = points.Count - 1;
            var pts = Align(points, line);

            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;

            double m1 = 0;
            double m2 = 0;

            if (order == 2)
            {
                a = pts[0].Y;
                b = pts[1].Y;
                c = pts[2].Y;
                d = a - (2d * b) + c;
                if (d != 0)
                {
                    m1 = -Sqrt((b * b) - (a * c));
                    m2 = -a + b;
                    var v1_ = -(m1 + m2) / d;
                    var v2_ = -(-m1 + m2) / d;

                    return new List<double>(
                        from t0 in new List<double> { v1_, v2_ }
                        where 0d <= t0 && t0 <= 1d
                        select t0
                    );
                }
                else if (b != c && d == 0)
                {
                    return new List<double>(
                        from t1 in new List<double> { ((2d * b) - c) / 2d * (b - c) }
                        where 0d <= t1 && t1 <= 1d
                        select t1
                    );
                }
                return new List<double>();
            }

            // see http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
            var pa = pts[0].Y;
            var pb = pts[1].Y;
            var pc = pts[2].Y;
            var pd = pts[3].Y;
            d = -pa + (3d * pb) - (3d * pc) + pd;
            a = ((3d * pa) - (6d * pb) + (3d * pc)) / d;
            b = ((-3d * pa) + (3d * pb)) / d;
            c = pa / d;
            var p_ = ((3d * b) - (a * a)) / 3d;
            var p3 = p_ / 3d;
            var q = ((2d * a * a * a) - (9d * a * b) + (27d * c)) / 27d;
            var q2 = q / 2d;
            var discriminant = (q2 * q2) + (p3 * p3 * p3);
            double u1, v1, x1, x2, x3;
            if (discriminant < 0)
            {
                var mp3 = -p_ / 3d;
                var mp33 = mp3 * mp3 * mp3;
                var r = Sqrt(mp33);
                var t = -q / (2d * r);
                var cosphi = t < -1d ? -1d : t > 1d ? 1d : t;
                var phi = Acos(cosphi);
                var crtr = Crt(r);
                var t1 = 2d * crtr;
                x1 = (t1 * Cos(phi / 3d)) - (a / 3d);
                x2 = (t1 * Cos((phi + Tau) / 3d)) - (a / 3d);
                x3 = (t1 * Cos((phi + (2d * Tau)) / 3d)) - (a / 3d);

                return new List<double>(
                    from t2 in new List<double> { x1, x2, x3 }
                    where 0d <= t2 && t2 <= 1d
                    select t2
                );
            }
            else if (discriminant == 0)
            {
                u1 = q2 < 0d ? Crt(-q2) : -Crt(q2);
                x1 = (2d * u1) - (a / 3);
                x2 = -u1 - (a / 3d);

                return new List<double>(
                    from t3 in new List<double> { x1, x2 }
                    where 0d <= t3 && t3 <= 1d
                    select t3
                );
            }
            else
            {
                var sd = Sqrt(discriminant);
                u1 = Crt(-q2 + sd);
                v1 = Crt(q2 + sd);

                return new List<double>(
                    from t4 in new List<double> { u1 - v1 - (a / 3d) }
                    where 0d <= t4 && t4 <= 1d
                    select t4
                );
            }
        }

        /// <summary>
        /// The roots.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> DRoots(List<double> p)
        {
            if (p is null) return null;
            // quadratic roots are easy
            if (p.Count == 3)
            {
                var a = p[0];
                var b = p[1];
                var c = p[2];
                var d = a - (2d * b) + c;
                if (d != 0)
                {
                    var m1 = -Sqrt((b * b) - (a * c));
                    var m2 = -a + b;
                    var v1 = -(m1 + m2) / d;
                    var v2 = -(-m1 + m2) / d;
                    return new List<double> { v1, v2 };
                }
                else if (b != c && d == 0d)
                {
                    return new List<double> { ((2d * b) - c) / (2d * (b - c)) };
                }
                return new List<double>();
            }

            // linear roots are even easier
            if (p.Count == 2)
            {
                var a = p[0];
                var b = p[1];
                return a != b ? new List<double> { a / (a - b) } : new List<double>();
            }

            return new List<double>();
        }
    }
}
