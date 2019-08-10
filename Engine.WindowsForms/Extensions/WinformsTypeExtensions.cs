// <copyright file="WinformsTypeExtensions.cs" company="Shkyrockett" >
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
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// Provides extension methods for converting between WinForms structs and Engine classes.
    /// </summary>
    public static class WinformsTypeExtensions
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tuple"></param>
        ///// <returns></returns>
        //public static Point ToPoint(this (double X, double Y) tuple)
        //    => new Point((int)tuple.X, (int)tuple.Y);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tuple"></param>
        ///// <returns></returns>
        //public static PointF ToPointF(this (double X, double Y) tuple)
        //    => new PointF((float)tuple.X, (float)tuple.Y);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="tuple"></param>
        ///// <returns></returns>
        //public static Point2D ToPoint2D(this (double X, double Y) tuple)
        //    => new Point2D(tuple.X, tuple.Y);

        /// <summary>
        /// The to point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point ToPoint(this Point2D point)
            => new Point((int)point.X, (int)point.Y);

        /// <summary>
        /// The to point f.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF ToPointF(this Point2D point)
            => new PointF((float)point.X, (float)point.Y);

        /// <summary>
        /// The to point2d.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D ToPoint2D(this Point point)
            => new Point2D(point.X, point.Y);

        /// <summary>
        /// The to point2d.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D ToPoint2D(this PointF point)
            => new Point2D(point.X, point.Y);

        /// <summary>
        /// The to size.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Size"/>.</returns>
        public static Size ToSize(this Size2D point)
            => new Size((int)point.Width, (int)point.Height);

        /// <summary>
        /// The to size f.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="SizeF"/>.</returns>
        public static SizeF ToSizeF(this Size2D point)
            => new SizeF((float)point.Width, (float)point.Height);

        /// <summary>
        /// The to size2d.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public static Size2D ToSize2D(this Size point)
            => new Size2D(point.Width, point.Height);

        /// <summary>
        /// The to size2d.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public static Size2D ToSize2D(this SizeF point)
            => new Size2D(point.Width, point.Height);

        /// <summary>
        /// The to rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        public static Rectangle ToRectangle(this Rectangle2D rectangle)
            => new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);

        /// <summary>
        /// The to rectangle f.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="RectangleF"/>.</returns>
        public static RectangleF ToRectangleF(this Rectangle2D rectangle)
            => new RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height);

        /// <summary>
        /// The to rectangle2d.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D ToRectangle2D(this RectangleF rectangle)
            => new Rectangle2D(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        /// <summary>
        /// The to rectangle2d.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D ToRectangle2D(this Rectangle rectangle)
            => new Rectangle2D(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static Point[] ToPointArray(this List<(double X, double Y)> list)
        //    => list?.ConvertAll(new Converter<(double X, double Y), Point>(ToPoint)).ToArray();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static PointF[] ToPointFArray(this List<(double X, double Y)> list)
        //    => list?.ConvertAll(new Converter<(double X, double Y), PointF>(ToPointF)).ToArray();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static List<Point2D> ToPoint2DArray(this List<(double X, double Y)> list)
        //    => list?.ConvertAll(new Converter<(double X, double Y), Point2D>(ToPoint2D));

        /// <summary>
        /// The to point array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static Point[] ToPointArray(this List<Point2D> list)
            => list?.ConvertAll(new Converter<Point2D, Point>(ToPoint)).ToArray();

        /// <summary>
        /// The to point2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Point2D> ToPoint2DList(this Point[] list)
            => new List<Point>(list)?.ConvertAll(new Converter<Point, Point2D>(ToPoint2D));

        /// <summary>
        /// The to point f array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static PointF[] ToPointFArray(this List<Point2D> list)
            => list?.ConvertAll(new Converter<Point2D, PointF>(ToPointF)).ToArray();

        /// <summary>
        /// The to point2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Point2D> ToPoint2DList(this PointF[] list)
            => new List<PointF>(list)?.ConvertAll(new Converter<PointF, Point2D>(ToPoint2D));

        /// <summary>
        /// The to size array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static Size[] ToSizeArray(this List<Size2D> list)
            => list?.ConvertAll(new Converter<Size2D, Size>(ToSize)).ToArray();

        /// <summary>
        /// The to size2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Size2D> ToSize2DList(this Size[] list)
            => new List<Size>(list)?.ConvertAll(new Converter<Size, Size2D>(ToSize2D));

        /// <summary>
        /// The to size f array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static SizeF[] ToSizeFArray(this List<Size2D> list)
            => list?.ConvertAll(new Converter<Size2D, SizeF>(ToSizeF)).ToArray();

        /// <summary>
        /// The to size2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Size2D> ToSize2DList(this SizeF[] list)
            => new List<SizeF>(list)?.ConvertAll(new Converter<SizeF, Size2D>(ToSize2D));

        /// <summary>
        /// The to rectangle array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static Rectangle[] ToRectangleArray(this List<Rectangle2D> list)
            => list?.ConvertAll(new Converter<Rectangle2D, Rectangle>(ToRectangle)).ToArray();

        /// <summary>
        /// The to rectangle2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Rectangle2D> ToRectangle2DList(this Rectangle[] list)
            => new List<Rectangle>(list)?.ConvertAll(new Converter<Rectangle, Rectangle2D>(ToRectangle2D));

        /// <summary>
        /// The to rectangle f array.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public static RectangleF[] ToRectangleFArray(this List<Rectangle2D> list)
            => list?.ConvertAll(new Converter<Rectangle2D, RectangleF>(ToRectangleF)).ToArray();

        /// <summary>
        /// The to rectangle f list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<RectangleF> ToRectangleFList(this List<Rectangle2D> list)
            => list?.ConvertAll(new Converter<Rectangle2D, RectangleF>(ToRectangleF));

        /// <summary>
        /// The to rectangle2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Rectangle2D> ToRectangle2DList(this RectangleF[] list)
            => new List<RectangleF>(list)?.ConvertAll(new Converter<RectangleF, Rectangle2D>(ToRectangle2D));

        /// <summary>
        /// The to rectangle2d list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Rectangle2D> ToRectangle2DList(this List<RectangleF> list)
            => list?.ConvertAll(new Converter<RectangleF, Rectangle2D>(ToRectangle2D));

        /// <summary>
        /// The to font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <returns>The <see cref="Font"/>.</returns>
        public static Font ToFont(this RenderFont font)
            => new Font(font.Name, (float)font.Size, (FontStyle)font.Style);

        /// <summary>
        /// The to render font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <returns>The <see cref="RenderFont"/>.</returns>
        public static RenderFont ToRenderFont(this Font font)
            => new RenderFont(font.Name, font.Size, (Engine.TextStyles)font.Style);

        /// <summary>
        /// The to string format.
        /// </summary>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="StringFormat"/>.</returns>
        public static StringFormat ToStringFormat(this TextFormat stringFormat)
            => new StringFormat((StringFormatFlags)stringFormat.Format, stringFormat.Language);

        /// <summary>
        /// The to text format.
        /// </summary>
        /// <param name="stringFormat">The stringFormat.</param>
        /// <returns>The <see cref="TextFormat"/>.</returns>
        public static TextFormat ToTextFormat(this StringFormat stringFormat)
            => new TextFormat((TextBoxFormatFlags)stringFormat.FormatFlags, stringFormat.DigitSubstitutionLanguage);
    }
}
