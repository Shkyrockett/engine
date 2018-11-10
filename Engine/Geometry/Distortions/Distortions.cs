// <copyright file="Filter.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>
// <copyright file="CurvePreprocess.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>
//     Linearize, RecursiveRamerDouglasPeukerReduce, and RemoveDuplicates are from: https://github.com/burningmime/curves
// </remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// The distortions class.
    /// </summary>
    public static class Distortions
    {
        #region Point Warp Filters
        /// <summary>
        /// The scale distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="factors">The factors.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(Point2D point, Size2D factors)
            => new Point2D(point.X * factors.Width, point.Y * factors.Height);

        /// <summary>
        /// The flip distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="flipHorz">The bHorz.</param>
        /// <param name="flipVert">The bVert.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Flip(Point2D point, Point2D fulcrum, bool flipHorz, bool flipVert)
        {
            var x = flipHorz ? fulcrum.X - (point.X - fulcrum.X + 1d) : point.X;
            var y = flipVert ? fulcrum.Y - (point.Y - fulcrum.Y + 1d) : point.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// The Translate distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Translate(Point2D point, Vector2D offset)
            => point + offset;

        /// <summary>
        /// The matrix distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Matrix(Point2D point, Matrix3x2D matrix)
            => matrix.Transform(point);

        /// <summary>
        /// Rotate a point about a center point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="fulcrum">The center axis point.</param>
        /// <param name="xAxis">The Sine and Cosine of the angle on the x-axis.</param>
        /// <param name="yAxis">The Sine and Cosine of the angle on the y-axis.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Rotate(Point2D point, Point2D fulcrum, Point2D xAxis, Point2D yAxis)
            => new Point2D(
                fulcrum.X + ((point.X - fulcrum.X) * xAxis.X + (point.Y - fulcrum.Y) * xAxis.Y),
                fulcrum.Y + ((point.X - fulcrum.X) * yAxis.X + (point.Y - fulcrum.Y) * yAxis.Y));

        /// <summary>
        /// Rotate all the coordinates in-place around the center point (cx, cy) by angle theta.
        /// </summary>
        /// <param name="a">an array of arrays of coordinates in 2D space.</param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="theta"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RotateArrays(List<List<Point2D>> a, double cx, double cy, double theta)
        {
            var cosine = Cos(theta);
            var sine = Sin(theta);
            foreach (var p in a)
            {
                for (var j = 0; j < p.Count; j++)
                {
                    var x = p[j].X - cx;
                    var y = p[j].Y - cy;
                    p[j] = new Point2D(cosine * x - sine * y + cx, sine * x + cosine * y + cy);
                }
            }
        }

        /// <summary>
        /// Warp the shape using Linear Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// Based roughly on the ideas presented in: https://web.archive.org/web/20160825211055/http://www.neuroproductions.be:80/experiments/envelope-distort-with-actionscript/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D LinearEnvelope(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topRight, Point2D bottomRight, Point2D bottomLeft)
        {
            // topLeft          topRight
            //   0-----------------0
            //   |                 |
            //   |                 |
            //   |                 |
            //   |                 |
            //   |                 |
            //   |                 |
            //   0-----------------0
            // bottomLeft   bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            var leftAnchor = Interpolators.Linear(topLeft.X, topLeft.Y, bottomLeft.X, bottomLeft.Y, normal.Y);
            var rightAnchor = Interpolators.Linear(topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, normal.Y);
            return Interpolators.Linear(leftAnchor.X, leftAnchor.Y, rightAnchor.X, rightAnchor.Y, normal.X);
        }

        /// <summary>
        /// Warp the shape using Quadratic Bézier Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topHandle">The topLeftH.</param>
        /// <param name="leftHandle">The topLeftV.</param>
        /// <param name="rightHandle">The topRightV.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomHandle">The bottomRightH.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// Based roughly on the ideas presented in: https://web.archive.org/web/20160825211055/http://www.neuroproductions.be:80/experiments/envelope-distort-with-actionscript/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D QuadraticBezierEnvelope(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topHandle, Point2D topRight, Point2D rightHandle,
            Point2D bottomRight, Point2D bottomHandle, Point2D bottomLeft, Point2D leftHandle)
        {
            // topLeft                             topRight
            //   0------------------0------------------0
            //   |              topHandle              |
            //   |                                     |
            //   |                                     |
            //   |                                     |
            //   |                                     |
            //   0 leftHandle              rightHandle 0
            //   |                                     |
            //   |                                     |
            //   |                                     |
            //   |                                     |
            //   |             bottomHandle            |
            //   0------------------0------------------0
            // bottomLeft                       bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            var leftAnchor = Interpolators.QuadraticBezier(topLeft.X, topLeft.Y, leftHandle.X, leftHandle.Y, bottomLeft.X, bottomLeft.Y, normal.Y);
            var handle = Interpolators.Linear(topHandle.X, topHandle.Y, bottomHandle.X, bottomHandle.Y, normal.Y);
            var rightAnchor = Interpolators.QuadraticBezier(topRight.X, topRight.Y, rightHandle.X, rightHandle.Y, bottomRight.X, bottomRight.Y, normal.Y);
            return Interpolators.QuadraticBezier(leftAnchor.X, leftAnchor.Y, handle.X, handle.Y, rightAnchor.X, rightAnchor.Y, normal.X);
        }

        /// <summary>
        /// Warp the shape using Cubic Bézier Envelope distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="topLeft">The topLeft.</param>
        /// <param name="topLeftH">The topLeftH.</param>
        /// <param name="topLeftV">The topLeftV.</param>
        /// <param name="topRight">The topRight.</param>
        /// <param name="topRightH">The topRightH.</param>
        /// <param name="topRightV">The topRightV.</param>
        /// <param name="bottomRight">The bottomRight.</param>
        /// <param name="bottomRightH">The bottomRightH.</param>
        /// <param name="bottomRightV">The bottomRightV.</param>
        /// <param name="bottomLeft">The bottomLeft.</param>
        /// <param name="bottomLeftH">The bottomLeftH.</param>
        /// <param name="bottomLeftV">The bottomLeftV.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// Based roughly on the ideas presented in: https://web.archive.org/web/20160825211055/http://www.neuroproductions.be:80/experiments/envelope-distort-with-actionscript/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CubicBezierEnvelope(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topLeftH, Point2D topLeftV,
            Point2D topRight, Point2D topRightH, Point2D topRightV,
            Point2D bottomRight, Point2D bottomRightH, Point2D bottomRightV,
            Point2D bottomLeft, Point2D bottomLeftH, Point2D bottomLeftV)
        {
            // topLeft                             topRight
            //   0--------0                 0----------0
            //   |   topLeftH             topRightH    |
            //   |                                     |
            //   |                                     |
            //   0 topLeftV                  topRightV 0
            //   
            //   
            //   
            //   0 bottomLeftV            bottomRightV 0
            //   |                                     |
            //   |                                     |
            //   |  bottomLeftH         bottomRightH   |
            //   0--------0                 0----------0
            // bottomLeft                       bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            var normalSquared = (X: normal.X * normal.X, Y: normal.Y * normal.Y);
            var normalCubed = (X: normalSquared.X * normal.X, Y: normalSquared.Y * normal.Y);
            var reverseNormal = (X: 1d - normal.X, Y: 1d - normal.Y);
            var reverseNormalSquared = (X: reverseNormal.X * reverseNormal.X, Y: reverseNormal.Y * reverseNormal.Y);
            var reverseNormalCubed = (X: reverseNormalSquared.X * reverseNormal.X, Y: reverseNormalSquared.Y * reverseNormal.Y);

            // Cubic interpolate the left anchor node.
            var leftAnchor = (
                X: topLeft.X * reverseNormalCubed.Y + 3d * topLeftV.X * normal.Y * reverseNormalSquared.Y + 3d * bottomLeftV.X * normalSquared.Y * reverseNormal.Y + bottomLeft.X * normalCubed.Y,
                Y: topLeft.Y * reverseNormalCubed.Y + 3d * topLeftV.Y * normal.Y * reverseNormalSquared.Y + 3d * bottomLeftV.Y * normalSquared.Y * reverseNormal.Y + bottomLeft.Y * normalCubed.Y
                );
            // Linear interpolate the left handle node.
            var leftHandle = (
                X: topLeftH.X * reverseNormal.Y + bottomLeftH.X * normal.Y,
                Y: topLeftH.Y * reverseNormal.Y + bottomLeftH.Y * normal.Y
                );
            // Linear interpolate the right handle node.
            var rightHandle = (
                X: topRightH.X * reverseNormal.Y + bottomRightH.X * normal.Y,
                Y: topRightH.Y * reverseNormal.Y + bottomRightH.Y * normal.Y
                );
            // Cubic interpolate the right anchor node.
            var rightAnchor = (
                X: topRight.X * reverseNormalCubed.Y + 3d * topRightV.X * normal.Y * reverseNormalSquared.Y + 3d * bottomRightV.X * normalSquared.Y * reverseNormal.Y + bottomRight.X * normalCubed.Y,
                Y: topRight.Y * reverseNormalCubed.Y + 3d * topRightV.Y * normal.Y * reverseNormalSquared.Y + 3d * bottomRightV.Y * normalSquared.Y * reverseNormal.Y + bottomRight.Y * normalCubed.Y
                );
            // Cubic interpolate the final result.
            return (
                X: leftAnchor.X * reverseNormalCubed.X + 3d * leftHandle.X * normal.X * reverseNormalSquared.X + 3d * rightHandle.X * normalSquared.X * reverseNormal.X + rightAnchor.X * normalCubed.X,
                Y: leftAnchor.Y * reverseNormalCubed.X + 3d * leftHandle.Y * normal.X * reverseNormalSquared.X + 3d * rightHandle.Y * normalSquared.X * reverseNormal.X + rightAnchor.Y * normalCubed.X
                );
        }

        /// <summary>
        /// Warp the shape using Cubic Bézier envelope distortion.
        /// </summary>
        /// <param name="point">The point to move.</param>
        /// <param name="bounds">The bounds of the shape.</param>
        /// <param name="topLeft">The top left anchor point of the envelope.</param>
        /// <param name="topLeftH">The top left horizontal point of the envelope.</param>
        /// <param name="topLeftV">The top left vertical point of the envelope.</param>
        /// <param name="topRight">The top right anchor point of the envelope.</param>
        /// <param name="topRightH">The top right horizontal point of the envelope.</param>
        /// <param name="topRightV">The top right vertical point of the envelope.</param>
        /// <param name="bottomRight">The bottom right anchor point of the envelope.</param>
        /// <param name="bottomRightH">The bottom right horizontal point of the envelope.</param>
        /// <param name="bottomRightV">The bottom right vertical point of the envelope.</param>
        /// <param name="bottomLeft">The bottom left anchor point of the envelope.</param>
        /// <param name="bottomLeftH">The bottom left horizontal point of the envelope.</param>
        /// <param name="bottomLeftV">The bottom left vertical point of the envelope.</param>
        /// <returns>Returns a <see cref="Point2D"/> shifted by the envelope.</returns>
        /// <acknowledgment>
        /// Based roughly on the ideas presented in: https://web.archive.org/web/20160825211055/http://www.neuroproductions.be:80/experiments/envelope-distort-with-actionscript/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CubicBezierEnvelope0(
            Point2D point,
            Rectangle2D bounds,
            Point2D topLeft, Point2D topLeftH, Point2D topLeftV,
            Point2D topRight, Point2D topRightH, Point2D topRightV,
            Point2D bottomRight, Point2D bottomRightH, Point2D bottomRightV,
            Point2D bottomLeft, Point2D bottomLeftH, Point2D bottomLeftV)
        {
            // topLeft                             topRight
            //   0--------0                 0----------0
            //   |   topLeftH             topRightH    |
            //   |                                     |
            //   |                                     |
            //   0 topLeftV                  topRightV 0
            //   
            //   
            //   
            //   0 bottomLeftV            bottomRightV 0
            //   |                                     |
            //   |                                     |
            //   |  bottomLeftH         bottomRightH   |
            //   0--------0                 0----------0
            // bottomLeft                       bottomRight
            // 
            // Install "Match Margin" Extension to enable word match highlighting, to help visualize where a variable resides in the ASCI map. 

            var normal = (X: (point.X - bounds.X) / bounds.Width, Y: (point.Y - bounds.Top) / bounds.Height);
            var leftAnchor = Interpolators.CubicBezier(topLeft.X, topLeft.Y, topLeftV.X, topLeftV.Y, bottomLeftV.X, bottomLeftV.Y, bottomLeft.X, bottomLeft.Y, normal.Y);
            var leftHandle = Interpolators.Linear(topLeftH.X, topLeftH.Y, bottomLeftH.X, bottomLeftH.Y, normal.Y);
            var rightHandle = Interpolators.Linear(topRightH.X, topRightH.Y, bottomRightH.X, bottomRightH.Y, normal.Y);
            var rightAnchor = Interpolators.CubicBezier(topRight.X, topRight.Y, topRightV.X, topRightV.Y, bottomRightV.X, bottomRightV.Y, bottomRight.X, bottomRight.Y, normal.Y);
            return Interpolators.CubicBezier(leftAnchor.X, leftAnchor.Y, leftHandle.X, leftHandle.Y, rightHandle.X, rightHandle.Y, rightAnchor.X, rightAnchor.Y, normal.X);
        }

        /// <summary>
        /// The pinch distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D point, Point2D fulcrum, double strength = OneHalf)
        {
            if (fulcrum == point)
            {
                return point;
            }

            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            var distance = Sqrt(distanceSquared);
            if (strength < 0d)
            {
                var r = distance;
                var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                var rn = Pow(r, strength) * distance;
                var newX = rn * Cos(a) + fulcrum.X;
                var newY = rn * Sin(a) + fulcrum.Y;
                sx += newX - point.X;
                sy += newY - point.Y;
            }
            else
            {
                var dirX = dx / distance;
                var dirY = dy / distance;
                var alpha = distance;
                var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
            {
                return point;
            }

            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                if (strength < 0d)
                {
                    var r = distance / radius;
                    var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                    var rn = Pow(r, strength) * distance;
                    var newX = rn * Cos(a) + fulcrum.X;
                    var newY = rn * Sin(a) + fulcrum.Y;
                    sx += newX - point.X;
                    sy += newY - point.Y;
                }
                else
                {
                    var dirX = dx / distance;
                    var dirY = dy / distance;
                    var alpha = distance / radius;
                    var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                    sx -= distortionFactor * dirX;
                    sy -= distortionFactor * dirY;
                }
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch1 distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch1(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
            {
                return point;
            }

            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                var r = distance / radius;
                var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                var rn = Pow(r, strength) * distance;
                var newX = rn * Cos(a) + fulcrum.X;
                var newY = rn * Sin(a) + fulcrum.Y;
                sx += newX - point.X;
                sy += newY - point.Y;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch2 distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch2(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
            {
                return point;
            }

            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                var dirX = dx / distance;
                var dirY = dy / distance;
                var alpha = distance / radius;
                var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The swirl distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="degree">The degree.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Swirl(Point2D point, Point2D fulcrum, double degree = OneHalf)
        {
            if (fulcrum == point)
            {
                return point;
            }

            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Atan2(dY, dX);
            var radius = Sqrt(dX * dX + dY * dY);
            var newX = fulcrum.X + (radius * Cos(theta + degree * radius));
            var newY = fulcrum.Y + (radius * Sin(theta + degree * radius));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// The time warp distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D TimeWarp(Point2D point, Point2D fulcrum, double factor = 10d)
        {
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Atan2(dY, dX); // Might this be simplified by finding the unit of the vector?
            var radius = Sqrt(dX * dX + dY * dY);
            var newRadius = Sqrt(radius) * factor;
            var newX = fulcrum.X + (newRadius * Cos(theta));
            var newY = fulcrum.Y + (newRadius * Sin(theta));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// The water distortion.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="nWave">The nWave.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Water(Point2D point, Point2D fulcrum, double nWave = 1)
        {
            var xo = nWave * Sin(2d * PI * point.Y / 128d);
            var yo = nWave * Cos(2d * PI * point.X / 128d);
            var newX = point.X + xo;
            var newY = point.Y + yo;
            return new Point2D(newX, newY);
        }
        #endregion Point Warp Filters

        #region Helper Methods
        /// <summary>
        /// Normalizes a point, so that it is expressed as percentage coordinates relative to the bounding box.
        /// </summary>
        /// <param name="bounds">The bounding box of the shape.</param>
        /// <param name="point">The point to warp.</param>
        /// <returns>The returned point in normalized percentage form.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D NormalizePoint(Rectangle2D bounds, Point2D point)
            => new Point2D((point.X - bounds.X) / bounds.Width, (point.Y - bounds.Top) / bounds.Height);

        /// <summary>
        /// Creates a list of equally spaced points that lie on the path described by straight line segments between
        /// adjacent points in the source list.
        /// </summary>
        /// <param name="source">Source list of points.</param>
        /// <param name="distance">Distance between points on the new path.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>List of equally-spaced points on the path.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Linearize(List<Point2D> source, double distance, double epsilon = Epsilon)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source), "List must not be null");
            }

            if (distance <= epsilon)
            {
                throw new InvalidOperationException($"{nameof(distance)} {distance} must not be less than epsilon { epsilon }.");
            }

            var dest = new List<Point2D>();
            if (source.Count < 1)
            {
                return dest;
            }

            var pp = source[0];
            dest.Add(pp);
            double cd = 0;
            for (var ip = 1; ip < source.Count; ip++)
            {
                var p0 = source[ip - 1];
                var p1 = source[ip];
                var td = p0.Distance(p1);
                if (cd + td > distance)
                {
                    var pd = distance - cd;
                    dest.Add(p0.Lerp(p1, pd / td));
                    var rd = td - pd;
                    while (rd > distance)
                    {
                        rd -= distance;
                        var np = p0.Lerp(p1, (td - rd) / td);
                        if (!Primitives.EqualsOrClose(np, pp))
                        {
                            dest.Add(np);
                            pp = np;
                        }
                    }
                    cd = rd;
                }
                else
                {
                    cd += td;
                }
            }

            // last point
            var lp = source[source.Count - 1];
            if (!Primitives.EqualsOrClose(pp, lp))
            {
                dest.Add(lp);
            }

            return dest;
        }

        /// <summary>
        /// "Reduces" a set of line segments by removing points that are too far away. Does not modify the input list; returns
        /// a new list with the points removed.
        /// </summary>
        /// <param name="points">Points to reduce</param>
        /// <param name="error">Maximum distance of a point to a line. Low values (~2-4) work well for mouse/touchscreen data.</param>
        /// <returns>A new list containing only the points needed to approximate the curve.</returns>
        /// <remarks>
        /// </remarks>
        /// <acknowledgment>
        /// The image says it better than I could ever describe: http://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif
        /// The Wiki article: http://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
        /// Based on:  http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RamerDouglasPeukerReduce(List<Point2D> points, double error = 4)
        {
            if (points is null)
            {
                throw new ArgumentNullException(nameof(points), "Must not be null.");
            }

            points = RemoveDuplicates(points);
            if (points.Count < 3)
            {
                return new List<Point2D>(points);
            }

            var keepIndex = new List<int>(Max(points.Count / 2, 16)) { 0, points.Count - 1 };
            RecursiveRamerDouglasPeukerReduce(points, error, 0, points.Count - 1, ref keepIndex);
            keepIndex.Sort();
            var res = new List<Point2D>(keepIndex.Count);
            foreach (var idx in keepIndex)
            {
                res.Add(points[idx]);
            }

            return res;
        }

        /// <summary>
        /// The recursive ramer douglas peuker reduce.
        /// </summary>
        /// <param name="pts">The pts.</param>
        /// <param name="error">The error.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <param name="keepIndex">The keepIndex.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RecursiveRamerDouglasPeukerReduce(List<Point2D> pts, double error, int first, int last, ref List<int> keepIndex)
        {
            var nPts = last - first + 1;
            if (nPts < 3)
            {
                return;
            }

            var segment = new LineSegment(pts[first], pts[last]);

            var maxDist = error;
            var split = 0;
            for (var i = first + 1; i < last - 1; i++)
            {
                var p = pts[i];
                var pDist = Measurements.PerpendicularDistance(segment, p);
                if (!double.IsNaN(pDist) && pDist > maxDist)
                {
                    maxDist = pDist;
                    split = i;
                }
            }

            if (split != 0)
            {
                keepIndex.Add(split);
                RecursiveRamerDouglasPeukerReduce(pts, error, first, split, ref keepIndex);
                RecursiveRamerDouglasPeukerReduce(pts, error, split, last, ref keepIndex);
            }
        }

        /// <summary>
        /// Removes any repeated points (that is, one point extremely close to the previous one). The same point can
        /// appear multiple times just not right after one another. This does not modify the input list. If no repeats
        /// were found, it returns the input list; otherwise it creates a new list with the repeats removed.
        /// </summary>
        /// <param name="points">Initial list of points.</param>
        /// <returns>Either points (if no duplicates were found), or a new list containing points with duplicates removed.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RemoveDuplicates(List<Point2D> points)
        {
            if (points.Count < 2)
            {
                return points;
            }

            // Common case -- no duplicates, so just return the source list
            var prev = points[0];
            var len = points.Count;
            var nDup = 0;
            for (var i = 1; i < len; i++)
            {
                var cur = points[i];
                if (Primitives.EqualsOrClose(prev, cur))
                {
                    nDup++;
                }
                else
                {
                    prev = cur;
                }
            }

            if (nDup == 0)
            {
                return points;
            }
            else
            {
                // Create a copy without them
                var dst = new List<Point2D>(len - nDup);
                prev = points[0];
                dst.Add(prev);
                for (var i = 1; i < len; i++)
                {
                    var cur = points[i];
                    if (!Primitives.EqualsOrClose(prev, cur))
                    {
                        dst.Add(cur);
                        prev = cur;
                    }
                }
                return dst;
            }
        }

        /// <summary>
        /// Add the points to sides.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolygonContour AddPointsToSides(PolygonContour contour)
        {
            var result = new PolygonContour();
            for (var i = 1; i < contour.Count; i++)
            {
                for (double j = 0; j < 1; j = j + 1d / (contour[contour.Count - 1].Distance(contour[0]) * 8))
                {
                    result.Add(Interpolators.Linear(contour[i - 1], contour[i], j));
                }
            }
            for (double j = 0; j < 1; j = j + 1d / (contour[contour.Count - 1].Distance(contour[0]) * 8))
            {
                result.Add(Interpolators.Linear(contour[contour.Count - 1], contour[0], j));
            }

            return result;
        }

        /// <summary>
        /// Evaluate the bilinear transform.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Bilinear(Point2D[] point, double u, double v)
        {
            // Evaluate the bilinear transform
            var r = new Point2D(
                (1 - u) * point[0].X + u * point[1].X,
                (1 - u) * point[0].Y + u * point[1].Y);

            var s = new Point2D(
                (1 - u) * point[3].X + u * point[2].X,
                (1 - u) * point[3].Y + u * point[2].Y);

            return new Point2D(
                (1 - v) * r.X + v * s.X,
                (1 - v) * r.Y + v * s.Y);
        }

        /// <summary>
        /// Evaluate the homographic perspective transform.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="c">The c.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Perspective(Point2D[] points, (double a, double b, double d, double e, double g, double h) c, double u, double v)
        {
            // Evaluate the homographic transform
            var T = c.g * u + c.h * v + 1;
            return new Point2D((c.a * u + c.b * v) / T + points[0].X, (c.d * u + c.e * v) / T + points[0].Y);
        }

        /// <summary>
        /// The solve perspective.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double a, double b, double d, double e, double g, double h) SolvePerspective(Point2D[] points)
        {
            // Compute the transform coefficients
            var t = (points[2].X - points[1].X) * (points[2].Y - points[3].Y) - (points[2].X - points[3].X) * (points[2].Y - points[1].Y);

            var g = ((points[2].X - points[0].X) * (points[2].Y - points[3].Y) - (points[2].X - points[3].X) * (points[2].Y - points[0].Y)) / t;
            var h = ((points[2].X - points[1].X) * (points[2].Y - points[0].Y) - (points[2].X - points[0].X) * (points[2].Y - points[1].Y)) / t;

            var a = g * (points[1].X - points[0].X);
            var d = g * (points[1].Y - points[0].Y);
            var b = h * (points[3].X - points[0].X);
            var e = h * (points[3].Y - points[0].Y);

            g -= 1;
            h -= 1;
            return (a, b, d, e, g, h);
        }
        #endregion Helper Methods
    }
}
