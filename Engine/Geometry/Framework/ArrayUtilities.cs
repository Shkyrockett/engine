// <copyright file="ArrayUtilities.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="t"></param>
        /// <remarks>
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/ae359c99-4294-4c7e-9afd-a161e8096de3/how-to-add-add-extension-method-to-array?forum=csharpgeneral
        /// </remarks>
        public static void Add<T>(ref T[] array, T t)
        {
            int newSize = array.Length + 1;
            Array.Resize(ref array, newSize);
            array[newSize - 1] = t;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
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
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>
        /// https://www.dotnetperls.com/array-slice
        /// </remarks>
        public static T[] Slice<T>(this T[] source)
        {
            // Return new array.
            T[] res = new T[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                res[i] = source[i];
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.justinshield.com/2011/06/mapreduce-in-c//
        /// </remarks>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> list, Func<T, TResult> func)
        {
            foreach (var i in list)
                yield return func(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.justinshield.com/2011/06/mapreduce-in-c//
        /// </remarks>
        public static T Reduce<T, U>(this IEnumerable<U> list, Func<U, T, T> func, T acc)
        {
            foreach (var i in list)
                acc = func(i, acc);

            return acc;
        }
    }
}
