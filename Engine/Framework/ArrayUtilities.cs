// <copyright file="ArrayUtilities.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = t;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="array"></param>
        ///// <param name="t"></param>
        //public static T[] Add<T>(this T[] array, T t)
        //{
        //    array.Add(t);
        //    return array;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public static void RemoveAt<T>(ref T[] array, int index)
        {
            if (index < array.Length - 1)
                Array.Copy(array, index + 1, array, index, array.Length - index - 1);
            Array.Resize(ref array, array.Length - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            array.RemoveAt(index);
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Pop<T>(this List<T> list)
        {
            T r = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Shift<T>(this List<T> list)
        {
            T r = list[0];
            list.RemoveAt(0);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> UnShift<T>(this List<T> list, T item)
        {
            list.Insert(0, item);
            return list;
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
            var len = end - start;

            // Return new array.
            T[] res = new T[len];
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
        /// <param name="source"></param>
        /// <remarks>
        /// https://www.dotnetperls.com/array-slice
        /// </remarks>
        public static T[] Slice<T>(this T[] source)
        {
            // Return new array.
            T[] res = new T[source.Length];
            for (var i = 0; i < source.Length; i++)
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="Start"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        /// <remarks> http://stackoverflow.com/q/9325627 </remarks>
        public static List<T> Splice<T>(this List<T> Source, int Start, int Size)
        {
            var retVal = Source.Skip(Start).Take(Size).ToList<T>();
            Source.RemoveRange(Start, Size);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.vcskicks.com/randomize_array.php
        /// </remarks>
        public static List<T> Shuffle<T>(this List<T> inputList)
        {
            var randomList = new List<T>();

            var r = new Random();
            var randomIndex = 0;

            while (inputList.Count > 0)
            {
                //Choose a random object in the list
                randomIndex = r.Next(0, inputList.Count);

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
