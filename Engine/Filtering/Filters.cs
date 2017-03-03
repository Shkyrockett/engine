// <copyright file="Filters.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        /// <param name="rect"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static Shape SphereDistort(Shape shape, Rectangle2D rect, double strength = 0.5)
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
        public static Shape SphereDistort(Shape shape, Point2D center, double radius, double strength = 0.5)
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
        public static Shape SwirlDistort(Shape shape, Point2D center, double strength = 0.008)
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
        public static Shape TimeWarpDistort(Shape shape, Point2D center, double strength = 10)
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
        public static Shape WaterDistort(Shape shape, Point2D center, double strength = 8)
        {
            var filter = new WaterDistort(center, strength);
            return filter.Process(shape);
        }
    }
}
