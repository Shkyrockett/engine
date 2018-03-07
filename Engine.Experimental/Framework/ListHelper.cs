// <copyright file="ListHelper.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Drawing;

namespace MethodSpeedTester
{
    /// <summary>
    /// The list helper class.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// The to point.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point ToPoint(this (double X, double Y) tuple)
            => new Point((int)tuple.X, (int)tuple.Y);

        /// <summary>
        /// The to point f.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF ToPointF(this (double X, double Y) tuple)
            => new PointF((float)tuple.X, (float)tuple.Y);

        /// <summary>
        /// The to point array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="T:Point[]"/>.</returns>
        public static Point[] ToPointArray(this List<(double X, double Y)> list)
            => list.ConvertAll(new Converter<(double X, double Y), Point>(ToPoint)).ToArray();

        /// <summary>
        /// The to point f array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="T:PointF[]"/>.</returns>
        public static PointF[] ToPointFArray(this List<(double X, double Y)> list)
            => list.ConvertAll(new Converter<(double X, double Y), PointF>(ToPointF)).ToArray();

        /// <summary>
        /// Get a the value for a key. If the key does not exist, return null;
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dic">The dictionary to call this method on.</param>
        /// <param name="key">The key to look up.</param>
        /// <returns>The key value. null if this key is not in the dictionary.</returns>
        /// <remarks>
        /// http://blogs.windward.net/davidt/2012/01/12/c-dictionary-getvalueordefault/
        /// </remarks>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
            => dic.TryGetValue(key, out var result) ? result : default;
    }
}
