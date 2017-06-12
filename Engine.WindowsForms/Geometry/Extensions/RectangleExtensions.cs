// <copyright file="RectangleExtensions.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing;

namespace Engine
{
    /// <summary>
    /// Class to extend the <see cref="Rectangle"/> class with additional features.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        /// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        /// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        public static Point Center(this Rectangle rectangle) => new Point((int)(0.5f * rectangle.Width) + rectangle.X, (int)(0.5f * rectangle.Height) + rectangle.Y);

        ///// <summary>
        ///// Extension method to find the center point of a rectangle.
        ///// </summary>
        ///// <param name="rectangle">The <see cref="RectangleF"/> of which you want the center.</param>
        ///// <returns>A <see cref="PointF"/> representing the center point of the <see cref="RectangleF"/>.</returns>
        ///// <remarks>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.</remarks>
        //public static PointF Center(this RectangleF rectangle)
        //{
        //    return new PointF(0.5f * rectangle.Width + rectangle.X, 0.5f * rectangle.Height + rectangle.Y);
        //}

        /// <summary>
        /// Convert a rectangle to an array of it's corner points.
        /// </summary>
        /// <param name="rectangle">The rectangle to get the corners from.</param>
        /// <returns>An array of points representing the corners of a rectangle.</returns>
        public static Point[] ToPoints(this Rectangle rectangle) => new Point[]
            {
                rectangle.Location,
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            };

        /// <summary>
        /// Creates a <see cref="Rectangle"/> from a center point and it's size.
        /// </summary>
        /// <param name="center">The center point to create the <see cref="Rectangle"/> as a <see cref="Point"/>.</param>
        /// <param name="size">The height and width of the new <see cref="Rectangle"/> as a <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Rectangle"/> based around a center point and it's size.</returns>
        public static Rectangle RectangleFromCenter(Point center, Size size) => new Rectangle(Point.Subtract(center, size.Inflate(0.5f)), size);
    }
}
