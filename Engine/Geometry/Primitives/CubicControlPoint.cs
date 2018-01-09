// <copyright file="CubicControlPoint.cs" company="Shkyrockett" >
//     Copyright © 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The control point class.
    /// </summary>
    public struct CubicControlPoint
    {
        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// Gets or sets the horizontal anchor.
        /// </summary>
        public Point2D AnchorA { get; set; }

        /// <summary>
        /// Gets or sets the vertical anchor.
        /// </summary>
        public Point2D AnchorB { get; set; }

        /// <summary>
        /// Gets or sets the global horizontal anchor.
        /// </summary>
        public Point2D AnchorAGlobal { get { return LocalToGlobal(AnchorA); } set { AnchorA = GlobalToLocal(value); } }

        /// <summary>
        /// Gets or sets the global vertical anchor.
        /// </summary>
        public Point2D AnchorBGlobal { get { return LocalToGlobal(AnchorB); } set { AnchorB = GlobalToLocal(value); } }

        /// <summary>
        /// The local to global method.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Point2D LocalToGlobal(Point2D point)
            => new Point2D(point.X + Point.X, point.Y + Point.Y);

        /// <summary>
        /// The global to local method.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Point2D GlobalToLocal(Point2D point)
            => new Point2D(Point.X - point.X, Point.Y - point.Y);
    }
}
