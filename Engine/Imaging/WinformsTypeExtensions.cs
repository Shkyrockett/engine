using Engine.Geometry;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point ToPoint(this Point2D point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static PointF ToPointF(this Point2D point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point2D ToPoint2D(this Point point)
        {
            return new Point2D(point.X, point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point2D ToPoint2D(this PointF point)
        {
            return new Point2D(point.X, point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Size ToSize(this Size2D point)
        {
            return new Size((int)point.Width, (int)point.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static SizeF ToSizeF(this Size2D point)
        {
            return new SizeF((float)point.Width, (float)point.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Size2D ToSize2D(this Size point)
        {
            return new Size2D(point.Width, point.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Size2D ToSize2D(this SizeF point)
        {
            return new Size2D(point.Width, point.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Rectangle ToRectangle(this Rectangle2D rectangle)
        {
            return new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static RectangleF ToRectangleF(this Rectangle2D rectangle)
        {
            return new RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Rectangle2D ToRectangle2D(this RectangleF rectangle)
        {
            return new Rectangle2D(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Rectangle2D ToRectangle2D(this Rectangle rectangle)
        {
            return new Rectangle2D(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point[] ToPointArray(this List<Point2D> list)
        {
            return list.ConvertAll(new Converter<Point2D, Point>(ToPoint)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Point2D> ToPoint2DList(this Point[] list)
        {
            return new List<Point>(list).ConvertAll(new Converter<Point, Point2D>(ToPoint2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static PointF[] ToPointFArray(this List<Point2D> list)
        {
            return list.ConvertAll(new Converter<Point2D, PointF>(ToPointF)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Point2D> ToPoint2DList(this PointF[] list)
        {
            return new List<PointF>(list).ConvertAll(new Converter<PointF, Point2D>(ToPoint2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Size[] ToSizeArray(this List<Size2D> list)
        {
            return list.ConvertAll(new Converter<Size2D, Size>(ToSize)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Size2D> ToSize2DList(this Size[] list)
        {
            return new List<Size>(list).ConvertAll(new Converter<Size, Size2D>(ToSize2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static SizeF[] ToSizeFArray(this List<Size2D> list)
        {
            return list.ConvertAll(new Converter<Size2D, SizeF>(ToSizeF)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Size2D> ToSize2DList(this SizeF[] list)
        {
            return new List<SizeF>(list).ConvertAll(new Converter<SizeF, Size2D>(ToSize2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Rectangle[] ToRectangleArray(this List<Rectangle2D> list)
        {
            return list.ConvertAll(new Converter<Rectangle2D, Rectangle>(ToRectangle)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Rectangle2D> ToRectangle2DList(this Rectangle[] list)
        {
            return new List<Rectangle>(list).ConvertAll(new Converter<Rectangle, Rectangle2D>(ToRectangle2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static RectangleF[] ToRectangleFArray(this List<Rectangle2D> list)
        {
            return list.ConvertAll(new Converter<Rectangle2D, RectangleF>(ToRectangleF)).ToArray();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<RectangleF> ToRectangleFList(this List<Rectangle2D> list)
        {
            return list.ConvertAll(new Converter<Rectangle2D, RectangleF>(ToRectangleF));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Rectangle2D> ToRectangle2DList(this RectangleF[] list)
        {
            return new List<RectangleF>(list).ConvertAll(new Converter<RectangleF, Rectangle2D>(ToRectangle2D));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Rectangle2D> ToRectangle2DList(this List<RectangleF> list)
        {
            return list.ConvertAll(new Converter<RectangleF, Rectangle2D>(ToRectangle2D));
        }
    }
}
