// <copyright file="ArrayUtilities.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The array utilities class.
    /// </summary>
    public static class ArrayUtilities
    {
        /// <summary>
        /// The r
        /// </summary>
        private static readonly Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <remarks>
        /// <para>https://social.msdn.microsoft.com/Forums/vstudio/en-US/ae359c99-4294-4c7e-9afd-a161e8096de3/how-to-add-add-extension-method-to-array?forum=csharpgeneral</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(ref T[] array, T t)
        {
            if (array is not null && array is T[] a)
            {
                Array.Resize(ref a, a.Length + 1);
                a[^1] = t;
            }
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            if (array is not null && array is T[] a)
            {
                if (index < a.Length - 1)
                {
                    Array.Copy(a, index + 1, a, index, a.Length - index - 1);
                }

                Array.Resize(ref a, a.Length - 1);
            }
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>
        /// The <see cref="Array" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            array.RemoveAt(index);
            return array;
        }

        /// <summary>
        /// The pop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// The <see cref="Type" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pop<T>(this List<T> list)
        {
            if (list is not null && list is List<T> l)
            {
                var r = l[^1];
                l.RemoveAt(l.Count - 1);
                return r;
            }

            return default;
        }

        /// <summary>
        /// The shift.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// The <see cref="Type" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Shift<T>(this List<T> list)
        {
            if (list is not null && list is List<T> l)
            {
                var r = l[0];
                l.RemoveAt(0);
                return r;
            }

            return default;
        }

        /// <summary>
        /// The unshift.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> UnShift<T>(this List<T> list, T item)
        {
            if (list is not null && list is List<T> l)
            {
                l.Insert(0, item);
            }

            return list;
        }

        /// <summary>
        /// Slices the specified start.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>
        /// The <see cref="Span{T}" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Slice<T>(this T[] source, int start, int end) => Slice(new Span<T>(source), start, end);

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>
        /// The <see cref="Span{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>https://www.dotnetperls.com/array-slice</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Slice<T>(this Span<T> source, int start, int end) =>
            //if ((source is not null))
            //{
            //    // Handles negative ends.
            //    if (end < 0)
            //    {
            //        end = source.Length + end;
            //    }
            //    var len = end - start;

            //    // Return new array.
            //    var res = new T[len];
            //    for (var i = 0; i < len; i++)
            //    {
            //        res[i] = source[i + start];
            //    }
            //    return res;
            //}

            //return source;
            source.Slice(start, end);

        /// <summary>
        /// Slices the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="Span{T}" />.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Slice<T>(this T[] source) => Slice(new Span<T>(source));

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="Span{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>https://www.dotnetperls.com/array-slice</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Slice<T>(this Span<T> source) =>
            //if ((source is not null))
            //{
            //    // Return new array.
            //    var res = new T[source.Length];
            //    for (var i = 0; i < source.Length; i++)
            //    {
            //        res[i] = source[i];
            //    }
            //    return res;
            //}

            //return source;
            source.Slice();

        /// <summary>
        /// The map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <returns>
        /// The <see cref="IEnumerable{TResult}" />.
        /// </returns>
        /// <remarks>
        /// <para>http://www.justinshield.com/2011/06/mapreduce-in-c//</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> list, Func<T, TResult> func)
        {
            if (list is not null && list is IEnumerable<T> l)
            {
                foreach (var i in l)
                {
                    yield return func(i);
                }
            }
        }

        /// <summary>
        /// The reduce.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <param name="acc">The acc.</param>
        /// <returns>
        /// The <see cref="Type" />.
        /// </returns>
        /// <remarks>
        /// <para>http://www.justinshield.com/2011/06/mapreduce-in-c//</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Reduce<T, U>(this IEnumerable<U> list, Func<U, T, T> func, T acc)
        {
            if (list is not null && list is IEnumerable<U> l)
            {
                foreach (var i in l)
                {
                    acc = func(i, acc);
                }
            }

            return acc;
        }

        /// <summary>
        /// The splice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The Source.</param>
        /// <param name="start">The Start.</param>
        /// <param name="size">The Size.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/q/9325627</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> Splice<T>(this List<T> source, int start, int size)
        {
            if (source is not null && source is List<T> s)
            {
                var retVal = s.Skip(start).Take(size).ToList();
                s.RemoveRange(start, size);
                return retVal;
            }

            return source;
        }

        /// <summary>
        /// The shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList">The inputList.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>http://www.vcskicks.com/randomize_array.php</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> Shuffle<T>(this List<T> inputList)
        {
            var randomList = new List<T>();

            while (inputList?.Count > 0)
            {
                // Choose a random object in the list
                var randomIndex = random.Next(0, inputList.Count);

                // Add it to the new, random list
                randomList.Add(inputList[randomIndex]);

                // Remove to avoid duplicates
                inputList.RemoveAt(randomIndex);
            }

            // Return the new random list
            return randomList;
        }
    }
}
