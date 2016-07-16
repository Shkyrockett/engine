using System;
using System.Collections.Generic;
using System.Drawing;

namespace MethodSpeedTester
{
    public static class ListHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static Point ToPoint(this (double X, double Y) tuple)
            => new Point((int)tuple.Item1, (int)tuple.Item2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static PointF ToPointF(this (double X, double Y) tuple)
            => new PointF((float)tuple.Item1, (float)tuple.Item2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point[] ToPointArray(this List<(double X, double Y)> list)
            => list.ConvertAll(new Converter<(double X, double Y), Point>(ToPoint)).ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PointF[] ToPointFArray(this List<(double X, double Y)> list)
            => list.ConvertAll(new Converter<(double X, double Y), PointF>(ToPointF)).ToArray();
    }
}
