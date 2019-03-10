// <copyright file="RectangleExtensions.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The rectangle f extensions class.
    /// </summary>
    public static class RectangleFExtensions
    {
        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        public static PointF Center(this RectangleF rectangle) => new PointF(
            rectangle.Left + ((rectangle.Right - rectangle.Left) * 0.5f),
            rectangle.Top + ((rectangle.Bottom - rectangle.Top) * 0.5f)
        );

        /// <summary>
        /// The top left.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF TopLeft(this RectangleF rectangle) => rectangle.Location;

        /// <summary>
        /// The top right.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF TopRight(this RectangleF rectangle) => new PointF(rectangle.Right, rectangle.Top);

        /// <summary>
        /// The bottom right.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF BottomRight(this RectangleF rectangle) => new PointF(rectangle.Right, rectangle.Bottom);

        /// <summary>
        /// The bottom left.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF"/>.</returns>
        public static PointF BottomLeft(this RectangleF rectangle) => new PointF(rectangle.Left, rectangle.Bottom);

        /// <summary>
        /// Find the outer bounds of a series of points.
        /// </summary>
        /// <param name="points">The points to find the boundaries of.</param>
        /// <returns>A rectangle that bounds all of the points in the array.</returns>
        public static Rectangle GetBounds(this PointF[] points) => Rectangle.Round(GetBoundsF(points));

        /// <summary>
        /// Find the outer bounds of a series of points.
        /// </summary>
        /// <param name="points">The points to find the boundaries of.</param>
        /// <returns>A rectangle that bounds all of the points in the array.</returns>
        public static RectangleF GetBoundsF(this PointF[] points)
        {
            var left = points[0].X;
            var right = points[0].X;
            var top = points[0].Y;
            var bottom = points[0].Y;

            for (var i = 1; i < points.Length; i++)
            {
                if (points[i].X < left)
                {
                    left = points[i].X;
                }
                else if (points[i].X > right)
                {
                    right = points[i].X;
                }

                if (points[i].Y < top)
                {
                    top = points[i].Y;
                }
                else if (points[i].Y > bottom)
                {
                    bottom = points[i].Y;
                }
            }

            return new RectangleF(left, top,
                                 Abs(right - left),
                                 Abs(bottom - top));
        }

        /// <summary>
        /// Convert a rectangle to an array of it's corner points.
        /// </summary>
        /// <param name="rectangle">The rectangle to get the corners from.</param>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public static PointF[] ToPoints(this RectangleF rectangle) => new PointF[]
            {
                rectangle.Location,
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Right, rectangle.Bottom),
                new PointF(rectangle.Left, rectangle.Bottom)
            };

        /// <summary>
        /// The round.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="Rectangle"/>.</returns>
        public static Rectangle Round(this RectangleF rect) => Rectangle.Round(rect);

        /// <summary>
        /// Creates a <see cref="RectangleF"/> from a center point and it's size.
        /// </summary>
        /// <param name="center">The center point to create the <see cref="RectangleF"/> as a <see cref="PointF"/>.</param>
        /// <param name="size">The height and width of the new <see cref="RectangleF"/> as a <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="RectangleF"/> based around a center point and it's size.</returns>
        public static RectangleF RectangleFFromCenter(PointF center, SizeF size) => new RectangleF(PointF.Subtract(center, size.Inflate(0.5f)), size);

        /// <summary>
        /// Find the bounding rectangle of a <see cref="RectangleF"/> rotated about it's center by an angle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> to get the bounds of rotating.</param>
        /// <param name="angle">The angle to rotate the <see cref="RectangleF"/></param>
        /// <returns></returns>
        public static RectangleF RotatedOffsetBounds(this RectangleF rectangle, double angle)
        {
            var cosAngle = Abs(Cos(angle));
            var sinAngle = Abs(Sin(angle));

            return new RectangleF(rectangle.Location, new SizeF(
                (int)((cosAngle * rectangle.Width) + (sinAngle * rectangle.Height)),
                (int)((cosAngle * rectangle.Height) + (sinAngle * rectangle.Width))
                ));
        }

        /// <summary>
        /// The wrap rectangle.
        /// </summary>
        /// <param name="Bounds">The Bounds.</param>
        /// <param name="Location">The Location.</param>
        /// <param name="Reference">The Reference.</param>
        /// <returns>The <see cref="Point"/>.</returns>
        public static Point WrapRectangle(Rectangle Bounds, Point Location, ref PointF Reference)
        {
            if (Location.X <= Bounds.X)
            {
                Reference -= new Size(Bounds.X, 0);
                return new Point(Bounds.Width - 2, Location.Y);
            }
            if (Location.Y <= Bounds.Y)
            {
                Reference -= new Size(0, Bounds.Y);
                return new Point(Location.X, Bounds.Height - 2);
            }
            if (Location.X >= (Bounds.Width - 1))
            {
                Reference += new Size(Bounds.Width, 0);
                return new Point(Bounds.X + 2, Location.Y);
            }
            if (Location.Y >= (Bounds.Height - 1))
            {
                Reference += new Size(0, Bounds.Height);
                return new Point(Location.X, Bounds.Y + 2);
            }
            return Location;
        }
    }
}
