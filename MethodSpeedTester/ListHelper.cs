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
        public static Point ToPoint(this Tuple<double, double> tuple) => new Point((int)tuple.Item1, (int)tuple.Item2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static PointF ToPointF(this Tuple<double, double> tuple) => new PointF((float)tuple.Item1, (float)tuple.Item2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point[] ToPointArray(this List<Tuple<double, double>> list) => list.ConvertAll(new Converter<Tuple<double, double>, Point>(ToPoint)).ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PointF[] ToPointFArray(this List<Tuple<double, double>> list) => list.ConvertAll(new Converter<Tuple<double, double>, PointF>(ToPointF)).ToArray();
    }
}
