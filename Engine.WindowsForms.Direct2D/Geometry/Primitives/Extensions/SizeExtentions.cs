// <copyright file="SizeExtentions.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class SizeExtentions
    {
        /// <summary>
        /// Inflates a <see cref="Size2D"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size2D"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Point2D"/>.</param>
        /// <returns>Returns a <see cref="Size2D"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size2D Inflate(this Size size, Point2D factor)
            => new Size2D(size.Width * factor.X, size.Height * factor.Y);

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, int factor)
            => new Size(size.Width * factor, size.Height * factor);

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, float factor)
            => new Size((int)(size.Width * factor), (int)(size.Height * factor));

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The factor to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, double factor)
            => new Size((int)(size.Width * factor), (int)(size.Height * factor));

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Point factor)
            => new Size(size.Width * factor.X, size.Height * factor.Y);

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, PointF factor)
            => new Size((int)(size.Width * factor.X), (int)(size.Height * factor.Y));

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Size factor)
            => new Size(size.Width * factor.Width, size.Height * factor.Height);

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="Size"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, SizeF factor)
            => new Size((int)(size.Width * factor.Width), (int)(size.Height * factor.Height));

        /// <summary>
        /// Inflates a <see cref="Size"/> by a given factor.
        /// </summary>
        /// <param name="size">The <see cref="Size"/> to inflate.</param>
        /// <param name="factor">The size factors to inflate the <see cref="SizeF"/>.</param>
        /// <returns>Returns a <see cref="Size"/> structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Inflate(this Size size, Vector2D factor)
            => new Size((int)(size.Width * factor.I), (int)(size.Height * factor.J));

        /// <summary>
        /// Unit of a Point
        /// </summary>
        /// <param name="value">The Point to Unitize</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Size Unit(this Size value)
            => value.Inflate((float)(1 / Sqrt((value.Width * value.Width) + (value.Height * value.Height))));
    }
}
