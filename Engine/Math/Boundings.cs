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
        /// Calculate the external close fitting rectangular bounds of a circular arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>The close bounding box of a circular arc.</returns>
        /// <returns>A Rectangle large enough to closely fit the circular arc inside.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Arc(
            double cX, double cY,
            double r,
            double startAngle, double sweepAngle)
        {
            double angleEnd = startAngle + sweepAngle;
            var startPoint = new Point2D(cX + r * Cos(-startAngle), cY + r * Sin(-startAngle));
            var endPoint = new Point2D(cX + r * Cos(-angleEnd), cY + r * Sin(-angleEnd));
            var bounds = new Rectangle2D(startPoint, endPoint);

            // check that angle2 > angle1
            if (angleEnd < startAngle)
                angleEnd += 2 * PI;
            if ((angleEnd >= 0) && (startAngle <= 0))
                bounds.Right = cX + r;
            if ((angleEnd >= HalfPi) && (startAngle <= HalfPi))
                bounds.Top = cY - r;
            if ((angleEnd >= PI) && (startAngle <= PI))
                bounds.Left = cX - r;
            if ((angleEnd >= Pau) && (startAngle <= Pau))
                bounds.Bottom = cY + r;
            if ((angleEnd >= Tau) && (startAngle <= Tau))
                bounds.Right = cX + r;
            return bounds;
        }

        /// <summary>
        /// Calculate the external square boundaries of a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of the Circle.</param>
        /// <returns>A Rectangle that is the size and location to envelop the circle.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Circle(
            double cX, double cY,
            double r)
            => Rectangle2D.FromLTRB((cX - r), (cY - r), (cX + r), (cY + r));

        /// <summary>
        /// Calculate the rectangular external boundaries of a non-rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <returns>A Rectangle that is the size and location to envelop an ellipse.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Ellipse(
            double cX, double cY,
            double r1, double r2)
            => new Rectangle2D(cX - r1, cY - r2, r1 * 2, r2 * 2);

        /// <summary>
        /// Calculate the external boundaries of a rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>A Rectangle that is the size and location to envelop a rotated ellipse.</returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Ellipse(
            double cX, double cY,
            double r1, double r2,
            double angle)
        {
            double a = r1 * Cos(angle);
            double b = r2 * Sin(angle);
            double c = r1 * Sin(angle);
            double d = r2 * Cos(angle);

            // Get the height and width.
            double width = Sqrt((a * a) + (b * b)) * 2;
            double height = Sqrt((c * c) + (d * d)) * 2;

            // Get the location point.
            double x2 = cX - width * 0.5;
            double y2 = cY - height * 0.5;

            // Return the bounding rectangle.
            return new Rectangle2D(x2, y2, width, height);
        }

        /// <summary>
        /// Locate the points of the Cartesian extremes of a rotated ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>A list of points that represent the points where a rotated ellipse intersects it's bounding box.</returns>
        /// <remarks>
        /// Based roughly on the principles found at:
        /// http://stackoverflow.com/questions/87734/how-do-you-calculate-the-axis-aligned-bounding-box-of-an-ellipse
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> EllipseExtremes(
            double cX, double cY,
            double r1, double r2,
            double angle)
        {
            double a = r1 * Cos(angle);
            double c = r1 * Sin(angle);
            double d = r2 * Cos(angle);
            double b = r2 * Sin(angle);

            // Find the angles of the Cartesian extremes. 
            double a1 = Atan2(-b, a);
            double a2 = Atan2(-b, a) + PI;
            double a3 = Atan2(d, c);
            double a4 = Atan2(d, c) + PI;

            // Return the points of Cartesian extreme of the rotated ellipse.
            return new List<Point2D> {
                Interpolaters.Ellipse(cX, cY, r1, r2, angle, a1),
                Interpolaters.Ellipse(cX, cY, r1, r2, angle, a2),
                Interpolaters.Ellipse(cX, cY, r1, r2, angle, a3),
                Interpolaters.Ellipse(cX, cY, r1, r2, angle, a4)
            };
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
        [Pure]
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D EllipticalArc(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle)
        {
            // Calculate the radii of the angle of rotation.
            double a = r1 * Cos(angle);
            double b = r2 * Sin(angle);
            double c = r1 * Sin(angle);
            double d = r2 * Cos(angle);

            // Find the angles of the Cartesian extremes. 
            double angleLeft = Atan2(-b, a) + PI;
            double angleTop = Atan2(d, c);
            double angleRight = Atan2(-b, a);
            double angleBottom = Atan2(d, c) + PI;

            // Keep smaller angles on the left and top to simplify angle checks.
            if (angleRight < angleLeft)
                Swap(ref angleRight, ref angleLeft);
            if (angleBottom < angleTop)
                Swap(ref angleBottom, ref angleTop);

            // Find the parent ellipse's horizontal and vertical radii extremes. 
            double halfWidth = Sqrt((a * a) + (b * b));
            double halfHeight = Sqrt((c * c) + (d * d));

            // Get the end points of the chord.
            var bounds = new Rectangle2D(
                Interpolaters.EllipticArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 0),
                Interpolaters.EllipticArc(cX, cY, r1, r2, angle, startAngle, sweepAngle, 1));

            // Expand the elliptical boundaries if any of the extreme angles fall within the sweep angle.
            if (Intersections.Contains(angleLeft, startAngle, sweepAngle))
                bounds.Left = cX - halfWidth;
            if (Intersections.Contains(angleRight, startAngle, sweepAngle))
                bounds.Right = cX + halfWidth;
            if (Intersections.Contains(angleTop, startAngle, sweepAngle))
                bounds.Top = cY - halfHeight;
            if (Intersections.Contains(angleBottom, startAngle, sweepAngle))
                bounds.Bottom = cY + halfHeight;

            // Return the points of the Cartesian extremes of the rotated elliptical arc.
            return bounds;
        }

        /// <summary>
        /// Rectangular boundaries of the Cartesian extremes of the chain of points generated by a parametric method.
        /// This loops through every point on every call, so it should be cached when possible.
        /// </summary>
        /// <param name="func">The list iterator method.</param>
        /// <param name="count">The number of points to use.</param>
        /// <returns>The external bounding rectangle of the chain of points generated by a parametric method.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D Bounds(
            Func<double, List<Point2D>> func,
            double count = 100)
        {
            // Get the list of points from the parametric method.
            List<Point2D> points = func(count);
            if (points?.Count < 1)
                return null;

            // Fill with initial point.
            double left = points[0].X;
            double top = points[0].Y;
            double right = points[0].X;
            double bottom = points[0].Y;

            // Locate the extremes of the parametric shape.
            foreach (Point2D point in points)
            {
                left = point.X <= left ? point.X : left;
                top = point.Y <= top ? point.Y : top;
                right = point.X >= right ? point.X : right;
                bottom = point.Y >= bottom ? point.Y : bottom;
            }

            // Return the rectangle that encompasses the points at the found extremes.
            return Rectangle2D.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// Calculate the external bounding rectangle of a rotated rectangle.
        /// </summary>
        /// <param name="height">The height of the rectangle to rotate.</param>
        /// <param name="width">The width of the rectangle to rotate.</param>
        /// <param name="fulcrum">The point at which to rotate the rectangle.</param>
        /// <param name="angle">The angle in radians to rotate the rectangle/</param>
        /// <returns>A Rectangle with the location and height, width bounding the rotated rectangle.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle2D RotatedRectangleBounds(double width, double height, Point2D fulcrum, double angle)
        {
            Contract.Ensures(Contract.Result<Rectangle2D>() != null);
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
    }
}
