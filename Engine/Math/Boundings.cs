// <copyright file="Boundings.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Boundings
    {
        /// <summary>
        /// 
        /// </summary>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Arc(double x, double y, double r, double startAngle, double sweepAngle)
        {
            double angleEnd = startAngle + sweepAngle;
            var startPoint = new Point2D(x + r * Cos(-startAngle), y + r * Sin(-startAngle));
            var endPoint = new Point2D(x + r * Cos(-angleEnd), y + r * Sin(-angleEnd));
            var bounds = new Rectangle2D(startPoint, endPoint);
            // check that angle2 > angle1
            if (angleEnd < startAngle)
                angleEnd += 2 * PI;
            if ((angleEnd >= 0) && (startAngle <= 0))
                bounds.Right = x + r;
            if ((angleEnd >= HalfPi) && (startAngle <= HalfPi))
                bounds.Top = y - r;
            if ((angleEnd >= PI) && (startAngle <= PI))
                bounds.Left = x - r;
            if ((angleEnd >= ThreeQuarterTau) && (startAngle <= ThreeQuarterTau))
                bounds.Bottom = y + r;
            if ((angleEnd >= Tau) && (startAngle <= Tau))
                bounds.Right = x + r;
            return bounds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Circle(double x, double y, double r)
            => Rectangle2D.FromLTRB((x - r), (y - r), (x + r), (y + r));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Ellipse(double x, double y, double r1, double r2)
            => new Rectangle2D(x - r1, y - r2, r1 * 2, r2 * 2);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Ellipse(double x, double y, double r1, double r2, double angle)
        {
            double a = r1 * Cos(angle);
            double b = r2 * Sin(angle);
            double c = r1 * Sin(angle);
            double d = r2 * Cos(angle);
            double width = Sqrt((a * a) + (b * b)) * 2;
            double height = Sqrt((c * c) + (d * d)) * 2;
            double x2 = x - width * 0.5;
            double y2 = y - height * 0.5;
            return new Rectangle2D(x2, y2, width, height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="phi"></param>
        /// <param name="angle1"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://fridrich.blogspot.com/2011/06/bounding-box-of-svg-elliptical-arc.html
        /// </remarks>
        public static Rectangle2D EllpticArc(
            double cx, double cy,
            double rx, double ry,
            double phi,
            double angle1, double sweepAngle)
        {
            double angle2 = angle1 + sweepAngle;
            Point2D p1 = Interpolaters.EllipticArc(cx, cy, rx, ry, phi, angle1, sweepAngle, 0);
            Point2D p2 = Interpolaters.EllipticArc(cx, cy, rx, ry, phi, angle1, sweepAngle, 1);

            double xmin;
            double ymin;
            double xmax;
            double ymax;

            double txmin;
            double txmax;
            double tymin;
            double tymax;

            if (phi == 0 || phi == PI)
            {
                xmin = cx - rx;
                txmin = Angle(-rx, 0);
                xmax = cx + rx;
                txmax = Angle(rx, 0);
                ymin = cy - ry;
                tymin = Angle(0, -ry);
                ymax = cy + ry;
                tymax = Angle(0, ry);
            }
            else if (phi == PI / 2.0 || phi == 3.0 * PI / 2.0)
            {
                xmin = cx - ry;
                txmin = Angle(-ry, 0);
                xmax = cx + ry;
                txmax = Angle(ry, 0);
                ymin = cy - rx;
                tymin = Angle(0, -rx);
                ymax = cy + rx;
                tymax = Angle(0, rx);
            }
            else
            {
                txmin = -Atan(ry * Tan(phi) / rx);
                txmax = PI - Atan(ry * Tan(phi) / rx);

                xmin = cx + rx * Cos(txmin) * Cos(phi) - ry * Sin(txmin) * Sin(phi);
                xmax = cx + rx * Cos(txmax) * Cos(phi) - ry * Sin(txmax) * Sin(phi);

                double tmpY = cy + rx * Cos(txmin) * Sin(phi) + ry * Sin(txmin) * Cos(phi);
                txmin = Angle(xmin - cx, tmpY - cy);
                tmpY = cy + rx * Cos(txmax) * Sin(phi) + ry * Sin(txmax) * Cos(phi);
                txmax = Angle(xmax - cx, tmpY - cy);

                tymin = Atan(ry / (Tan(phi) * rx));
                tymax = Atan(ry / (Tan(phi) * rx)) + PI;
                ymin = cy + rx * Cos(tymin) * Sin(phi) + ry * Sin(tymin) * Cos(phi);
                ymax = cy + rx * Cos(tymax) * Sin(phi) + ry * Sin(tymax) * Cos(phi);

                double tmpX = cx + rx * Cos(tymin) * Cos(phi) - ry * Sin(tymin) * Sin(phi);
                tymin = Angle(tmpX - cx, ymin - cy);
                tmpX = cx + rx * Cos(tymax) * Cos(phi) - ry * Sin(tymax) * Sin(phi);
                tymax = Angle(tmpX - cx, ymax - cy);
            }

            if (xmin > xmax)
            {
                Swap(ref xmin, ref xmax);
                Swap(ref txmin, ref txmax);
            }

            if (ymin > ymax)
            {
                Swap(ref ymin, ref ymax);
                Swap(ref tymin, ref tymax);
            }

            if (angle1 > angle2)
                Swap(ref angle1, ref angle2);

            if (angle1 > txmin || angle2 < txmin)
                xmin = p1.X < p2.X ? p1.X : p2.X;
            if (angle1 > txmax || angle2 < txmax)
                xmax = p1.X > p2.X ? p1.X : p2.X;
            if (angle1 > tymin || angle2 < tymin)
                ymin = p1.Y < p2.Y ? p1.Y : p2.Y;
            if (angle1 > tymax || angle2 < tymax)
                ymax = p1.Y > p2.Y ? p1.Y : p2.Y;

            return Rectangle2D.FromLTRB(xmin, ymin, xmax, ymax);
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
        /// Parametric bounds.
        /// </summary>
        /// <param name="func">The list iterator method.</param>
        /// <returns></returns>
        public static Rectangle2D Bounds(Func<double, List<Point2D>> func)
        {
                List<Point2D> points = (func(100));
                if (points?.Count < 1)
                    return null;

                double left = points[0].X;
                double top = points[0].Y;
                double right = points[0].X;
                double bottom = points[0].Y;

                foreach (Point2D point in points)
                {
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrum"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D RotatedRectangleBounds(double x, double y, double width, double height, Point2D fulcrum, double angle)
        {
            double cosAngle = Abs(Cos(angle));
            double sinAngle = Abs(Sin(angle));

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
    }
}
