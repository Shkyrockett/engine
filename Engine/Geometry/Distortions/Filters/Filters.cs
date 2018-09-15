// <copyright file="Filters.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// The filters class.
    /// </summary>
    public static class Filters
    {
        /// <summary>
        /// The parametric preserving distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="functions">The functions.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S ParametricPreservingDistort<S>(this S shape, params Func<Point2D, Point2D>[] functions)
            where S : Shape
        {
            var filter = new ParametricPreservingDistort(functions);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// The flip distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="flipHorizontal">The flipHorizontal.</param>
        /// <param name="flipVertical">The flipVertical.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S FlipDistort<S>(this S shape, Point2D center, bool flipHorizontal, bool flipVertical)
            where S : Shape
        {
            var filter = new FlipDistort(center, flipHorizontal, flipVertical);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// The matrix distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S MatrixDistort<S>(this S shape, Matrix3x2D matrix)
            where S : Shape
        {
            var filter = new MatrixDistort(matrix);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// The rotate distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S RotateDistort<S>(this S shape, Point2D center, double angle)
            where S : Shape
        {
            var filter = new RotateDistort(center, angle);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// The scale distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="factors">The factors.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S ScaleDistort<S>(this S shape, Size2D factors)
            where S : Shape
        {
            var filter = new ScaleDistort(factors);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// Translate the distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        public static S TranslateDistort<S>(this S shape, Vector2D offset)
            where S : Shape
        {
            var filter = new TranslateDistort(offset);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// The parametric destructive distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="functions">The functions.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T ParametricDestructiveDistort<S, T>(this S shape, params Func<Point2D, Point2D>[] functions)
            where S : Shape
            where T : Shape
        {
            var filter = new ParametricDestructiveDistort(functions);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The bulge distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T BulgeDistort<S, T>(this S shape, Point2D center, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new BulgeDistort(center, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The envelope distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="envelope">The envelope.</param>
        /// <param name="bounds">The bounding rectangle.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T EnvelopeDistort<S, T>(this S shape, Envelope envelope, Rectangle2D bounds)
            where S : Shape
            where T : Shape
        {
            var filter = new EnvelopeDistort(envelope, bounds);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The sphere distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T SphereDistort<S, T>(this S shape, Rectangle2D rect, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new SphereDistort(rect, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The sphere distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T SphereDistort<S, T>(this S shape, Point2D center, double radius, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new SphereDistort(center, radius, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The swirl distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T SwirlDistort<S, T>(this S shape, Point2D center, double strength = 0.008d)
            where S : Shape
            where T : Shape
        {
            var filter = new SwirlDistort(center, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The time warp distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T TimeWarpDistort<S, T>(this S shape, Point2D center, double strength = 10d)
            where S : Shape
            where T : Shape
        {
            var filter = new TimeWarpDistort(center, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// The water distort.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="center">The center.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static T WaterDistort<S, T>(this S shape, Point2D center, double strength = 8d)
            where S : Shape
            where T : Shape
        {
            var filter = new WaterDistort(center, strength);
            return filter.Process<S, T>(shape);
        }
    }
}
