/*
  A port of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The Line 2D class.
    /// </summary>
    public struct Line2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line2D"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line2D(Point2D p1, Point2D p2)
            : this()
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line2D"/> class.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public Line2D(double x0, double y0, double x1, double y1)
            : this()
        {
            P1 = new Point2D(x0,y0);
            P2 = new Point2D(x1,y1);
        }

        /// <summary>
        /// Gets or sets the p1.
        /// </summary>
        public Point2D P1 { get; set; }

        /// <summary>
        /// Gets or sets the p2.
        /// </summary>
        public Point2D P2 { get; set; }
    }
}
