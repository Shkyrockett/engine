﻿using Engine.Colorspace;
using System.Drawing;

namespace Engine.WindowsForms
{
    public static class DrawingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(this IColor color)
        {
            (byte A, byte R, byte G, byte B) = color.ToARGBTuple();
            return Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ARGB ToARGB(this Color color)
            => new ARGB(color.A, color.R, color.G, color.B);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brush"></param>
        /// <returns></returns>
        public static Brush ToBrush(this IBrush brush)
            => new SolidBrush(brush.Color.ToColor());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pen"></param>
        /// <returns></returns>
        public static Pen ToPen(this IPen pen)
            => new Pen(pen.Brush.ToBrush());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image ToBitmap(this IBitmap image)
            => Image.FromStream(image.Stream);
    }
}