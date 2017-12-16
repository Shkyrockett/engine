// <copyright file="GreinerHormannClipper.cs" >
//     Copyright © 2015 - 2017 w8r. All rights reserved.
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
    /// The greiner hormann clipper class.
    /// </summary>
    public static class GreinerHormannClipper
    {
        /// <summary>
        /// The union.
        /// </summary>
        /// <param name="polygonA">The polygonA.</param>
        /// <param name="polygonB">The polygonB.</param>
        /// <returns>The <see cref="T:List{List{Point2D}}"/>.</returns>
        public static List<List<Point2D>> Union(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, false, false);

        /// <summary>
        /// The intersection.
        /// </summary>
        /// <param name="polygonA">The polygonA.</param>
        /// <param name="polygonB">The polygonB.</param>
        /// <returns>The <see cref="T:List{List{Point2D}}"/>.</returns>
        public static List<List<Point2D>> Intersection(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, true, true);

        /// <summary>
        /// The diff.
        /// </summary>
        /// <param name="polygonA">The polygonA.</param>
        /// <param name="polygonB">The polygonB.</param>
        /// <returns>The <see cref="T:List{List{Point2D}}"/>.</returns>
        public static List<List<Point2D>> Diff(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, false, true);

        /// <summary>
        /// The diff2.
        /// </summary>
        /// <param name="polygonA">The polygonA.</param>
        /// <param name="polygonB">The polygonB.</param>
        /// <returns>The <see cref="T:List{List{Point2D}}"/>.</returns>
        public static List<List<Point2D>> Diff2(List<Point2D> polygonA, List<Point2D> polygonB)
            => DoClip(polygonA, polygonB, true, false);

        /// <summary>
        /// Do the clip.
        /// </summary>
        /// <param name="polygonA">The polygonA.</param>
        /// <param name="polygonB">The polygonB.</param>
        /// <param name="eA">The eA.</param>
        /// <param name="eB">The eB.</param>
        /// <returns>The <see cref="T:List{List{Point2D}}"/>.</returns>
        public static List<List<Point2D>> DoClip(List<Point2D> polygonA, List<Point2D> polygonB, bool eA, bool eB)
        {
            var source = new ClippingPolygon(polygonA);
            var clip = new ClippingPolygon(polygonB);
            var result = source.Clip(clip, eA, eB);

            return result;
        }
    }
}
