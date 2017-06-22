// <copyright file="Filters.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public static class Filters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="factors"></param>
        /// <returns></returns>
        public static S ScaleDistort<S>(this S shape, Size2D factors)
            where S : Shape
        {
            var filter = new ScaleDistort(factors);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="flipHorizontal"></param>
        /// <param name="flipVertical"></param>
        /// <returns></returns>
        public static S FlipDistort<S>(this S shape, Point2D center, bool flipHorizontal, bool flipVertical)
            where S : Shape
        {
            var filter = new FlipDistort(center, flipHorizontal, flipVertical);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static S TranslateDistort<S>(this S shape, Vector2D offset)
            where S : Shape
        {
            var filter = new TranslateDistort(offset);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static S RotateDistort<S>(this S shape, Point2D center, double angle)
            where S : Shape
        {
            var filter = new RotateDistort(center, angle);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static S MatrixDistort<S>(this S shape, Matrix3x2D matrix)
            where S : Shape
        {
            var filter = new MatrixDistort(matrix);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="shape"></param>
        /// <param name="functions"></param>
        /// <returns></returns>
        public static S ParametricPreservingDistort<S>(this S shape, params Func<Point2D, Point2D>[] functions)
            where S : Shape
        {
            var filter = new ParametricPreservingDistort(functions);
            return filter.Process<S, S>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="functions"></param>
        /// <returns></returns>
        public static T ParametricDestructiveDistort<S,T>(this S shape, params Func<Point2D, Point2D>[] functions)
            where S : Shape
            where T : Shape
        {
            var filter = new ParametricDestructiveDistort(functions);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T BulgeDistort<S,T>(this S shape, Point2D center, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new BulgeDistort(center, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="rect"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T SphereDistort<S,T>(this S shape, Rectangle2D rect, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new SphereDistort(rect, strength);
            return filter.Process<S, T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T SphereDistort<S,T>(this S shape, Point2D center, double radius, double strength = 0.5d)
            where S : Shape
            where T : Shape
        {
            var filter = new SphereDistort(center, radius, strength);
            return filter.Process<S,T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T SwirlDistort<S,T>(this S shape, Point2D center, double strength = 0.008d)
            where S : Shape
            where T : Shape
        {
            var filter = new SwirlDistort(center, strength);
            return filter.Process<S,T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T TimeWarpDistort<S,T>(this S shape, Point2D center, double strength = 10d)
            where S : Shape
            where T : Shape
        {
            var filter = new TimeWarpDistort(center, strength);
            return filter.Process<S,T>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static T WaterDistort<S,T>(this S shape, Point2D center, double strength = 8d)
            where S : Shape
            where T : Shape
        {
            var filter = new WaterDistort(center, strength);
            return filter.Process<S,T>(shape);
        }
    }
}
