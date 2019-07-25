// <copyright file="ArrayUtilities.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// The array utilities class.
    /// </summary>
    public static class ArrayUtilities
    {
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="t">The t.</param>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/ae359c99-4294-4c7e-9afd-a161e8096de3/how-to-add-add-extension-method-to-array?forum=csharpgeneral
        /// </remarks>
        public static void Add<T>(ref T[] array, T t)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = t;
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            if (index < array.Length - 1)
            {
                Array.Copy(array, index + 1, array, index, array.Length - index - 1);
            }

            Array.Resize(ref array, array.Length - 1);
        }

        /// <summary>
        /// Remove the at.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="T[]"/>.</returns>
        /// <typeparam name="T"></typeparam>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            array.RemoveAt(index);
            return array;
        }

        /// <summary>
        /// The pop.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="T"></typeparam>
        public static T Pop<T>(this List<T> list)
        {
            var r = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return r;
        }

        /// <summary>
        /// The shift.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="T"></typeparam>
        public static T Shift<T>(this List<T> list)
        {
            var r = list[0];
            list.RemoveAt(0);
            return r;
        }

        /// <summary>
        /// The unshift.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        /// <typeparam name="T"></typeparam>
        public static List<T> UnShift<T>(this List<T> list, T item)
        {
            list.Insert(0, item);
            return list;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>The <see cref="T[]"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// https://www.dotnetperls.com/array-slice
        /// </remarks>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            var len = end - start;

            // Return new array.
            var res = new T[len];
            for (var i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="T[]"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// https://www.dotnetperls.com/array-slice
        /// </remarks>
        public static T[] Slice<T>(this T[] source)
        {
            // Return new array.
            var res = new T[source.Length];
            for (var i = 0; i < source.Length; i++)
            {
                res[i] = source[i];
            }
            return res;
        }

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <returns>The <see cref="IEnumerable{TResult}"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <remarks>
        /// http://www.justinshield.com/2011/06/mapreduce-in-c//
        /// </remarks>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> list, Func<T, TResult> func)
        {
            foreach (var i in list)
            {
                yield return func(i);
            }
        }

        /// <summary>
        /// The reduce.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <param name="acc">The acc.</param>
        /// <returns>The <see cref="Type"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <remarks>
        /// http://www.justinshield.com/2011/06/mapreduce-in-c//
        /// </remarks>
        public static T Reduce<T, U>(this IEnumerable<U> list, Func<U, T, T> func, T acc)
        {
            foreach (var i in list)
            {
                acc = func(i, acc);
            }

            return acc;
        }

        /// <summary>
        /// The splice.
        /// </summary>
        /// <param name="Source">The Source.</param>
        /// <param name="Start">The Start.</param>
        /// <param name="Size">The Size.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// http://stackoverflow.com/q/9325627
        /// </remarks>
        public static List<T> Splice<T>(this List<T> Source, int Start, int Size)
        {
            var retVal = Source.Skip(Start).Take(Size).ToList();
            Source.RemoveRange(Start, Size);
            return retVal;
        }

        /// <summary>
        /// The shuffle.
        /// </summary>
        /// <param name="inputList">The inputList.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// http://www.vcskicks.com/randomize_array.php
        /// </remarks>
        public static List<T> Shuffle<T>(this List<T> inputList)
        {
            var randomList = new List<T>();

            var r = new Random();
            while (inputList.Count > 0)
            {
                //Choose a random object in the list
                var randomIndex = r.Next(0, inputList.Count);

                //add it to the new, random list
                randomList.Add(inputList[randomIndex]);

                //remove to avoid duplicates
                inputList.RemoveAt(randomIndex);
            }

            //return the new random list
            return randomList;
        }
    }
}
