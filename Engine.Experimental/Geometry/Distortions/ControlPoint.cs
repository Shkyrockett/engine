using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The control point class.
    /// </summary>
    public struct ControlPoint
    {
        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// Gets or sets the horizontal anchor.
        /// </summary>
        public Point2D AnchorH { get; set; }

        /// <summary>
        /// Gets or sets the vertical anchor.
        /// </summary>
        public Point2D AnchorV { get; set; }

        /// <summary>
        /// Gets or sets the global horizontal anchor.
        /// </summary>
        public Point2D AnchorHGlobal { get { return LocalToGlobal(AnchorH); } set { AnchorH = GlobalToLocal(value); } }

        /// <summary>
        /// Gets or sets the global vertical anchor.
        /// </summary>
        public Point2D AnchorVGlobal { get { return LocalToGlobal(AnchorV); } set { AnchorV = GlobalToLocal(value); } }

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
