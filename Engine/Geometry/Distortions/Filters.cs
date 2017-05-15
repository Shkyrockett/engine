// <copyright file="Filters.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
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
        /// <param name="shape"></param>
        /// <param name="factors"></param>
        /// <returns></returns>
        public static Shape ScaleDistort(this Shape shape, Size2D factors)
        {
            var filter = new ScaleDistort(factors);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="flipHorizontal"></param>
        /// <param name="flipVertical"></param>
        /// <returns></returns>
        public static Shape FlipDistort(this Shape shape, Point2D center, bool flipHorizontal, bool flipVertical)
        {
            var filter = new FlipDistort(center, flipHorizontal, flipVertical);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Shape TranslateDistort(this Shape shape, Vector2D offset)
        {
            var filter = new TranslateDistort(offset);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Shape RotateDistort(this Shape shape, Point2D center, double angle)
        {
            var filter = new RotateDistort(center, angle);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Shape MatrixDistort(this Shape shape, Matrix3x2D matrix)
        {
            var filter = new MatrixDistort(matrix);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="functions"></param>
        /// <returns></returns>
        public static Shape ParametricPreservingDistort(this Shape shape, params Func<Point2D, Point2D>[] functions)
        {
            var filter = new ParametricPreservingDistort(functions);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="functions"></param>
        /// <returns></returns>
        public static Shape ParametricDestructiveDistort(this Shape shape, params Func<Point2D, Point2D>[] functions)
        {
            var filter = new ParametricDestructiveDistort(functions);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape BulgeDistort(this Shape shape, Point2D center, double strength = 0.5)
        {
            var filter = new BulgeDistort(center, strength);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="rect"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape SphereDistort(this Shape shape, Rectangle2D rect, double strength = 0.5)
        {
            var filter = new SphereDistort(rect, strength);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape SphereDistort(this Shape shape, Point2D center, double radius, double strength = 0.5)
        {
            var filter = new SphereDistort(center, radius, strength);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape SwirlDistort(this Shape shape, Point2D center, double strength = 0.008)
        {
            var filter = new SwirlDistort(center, strength);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape TimeWarpDistort(this Shape shape, Point2D center, double strength = 10)
        {
            var filter = new TimeWarpDistort(center, strength);
            return filter.Process(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="center"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape WaterDistort(this Shape shape, Point2D center, double strength = 8)
        {
            var filter = new WaterDistort(center, strength);
            return filter.Process(shape);
        }
    }
}
