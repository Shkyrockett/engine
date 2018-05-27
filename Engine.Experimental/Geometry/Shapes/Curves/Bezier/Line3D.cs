/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The line1 class.
    /// </summary>
    public struct Line3D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line3D"/> class.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public Line3D(Point3D p1, Point3D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Gets or sets the p1.
        /// </summary>
        public Point3D P1 { get; set; }

        /// <summary>
        /// Gets or sets the p2.
        /// </summary>
        public Point3D P2 { get; set; }
    }
}
