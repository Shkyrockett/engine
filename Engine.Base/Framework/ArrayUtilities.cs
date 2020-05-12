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
            if (!(array is null))
            {
                Array.Resize(ref array, (array?.Length).Value + 1);
                array[^1] = t;
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
            if (!(array is null))
            {
                if (index < array.Length - 1)
                {
                    Array.Copy(array, index + 1, array, index, array.Length - index - 1);
                }

                Array.Resize(ref array, array.Length - 1);
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
            if (!(list is null))
            {
                var r = list[^1];
                list.RemoveAt(list.Count - 1);
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
            if (!(list is null))
            {
                var r = list[0];
                list.RemoveAt(0);
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
            if (!(list is null))
            {
                list.Insert(0, item);
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
            //if (!(source is null))
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
            //if (!(source is null))
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
            if (!(list is null))
            {
                foreach (var i in list)
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
            if (!(list is null))
            {
                foreach (var i in list)
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
        /// <param name="Source">The Source.</param>
        /// <param name="Start">The Start.</param>
        /// <param name="Size">The Size.</param>
        /// <returns>
        /// The <see cref="List{T}" />.
        /// </returns>
        /// <remarks>
        /// <para>http://stackoverflow.com/q/9325627</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> Splice<T>(this List<T> Source, int Start, int Size)
        {
            if (!(Source is null))
            {
                var retVal = Source.Skip(Start).Take(Size).ToList();
                Source.RemoveRange(Start, Size);
                return retVal;
            }

            return Source;
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
