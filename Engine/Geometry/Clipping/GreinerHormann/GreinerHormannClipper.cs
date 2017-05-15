// <copyright file="GreinerHormannClipper.cs" >
//     Copyright (c) 2015 - 2017 w8r. All rights reserved.
// </copyright>
// <author id="w8r">Alexander Milevski</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>Ported from https://github.com/w8r/GreinerHormann </summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class GreinerHormannClipper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygonA"></param>
        /// <param name="polygonB"></param>
        /// <returns></returns>
        public static List<List<Point2D>> Union(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, false, false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygonA"></param>
        /// <param name="polygonB"></param>
        /// <returns></returns>
        public static List<List<Point2D>> Intersection(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, true, true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygonA"></param>
        /// <param name="polygonB"></param>
        /// <returns></returns>
        public static List<List<Point2D>> Diff(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, false, true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygonA"></param>
        /// <param name="polygonB"></param>
        /// <returns></returns>
        public static List<List<Point2D>> Diff2(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, true, false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygonA"></param>
        /// <param name="polygonB"></param>
        /// <param name="eA"></param>
        /// <param name="eB"></param>
        /// <returns></returns>
        public static List<List<Point2D>> DoClip(List<Point2D> polygonA, List<Point2D> polygonB, bool eA, bool eB)
        {
            var source = new ClippingPolygon(polygonA);
            var clip = new ClippingPolygon(polygonB);
            var result = source.Clip(clip, eA, eB);

            return result;
        }
    }
}
