/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The arc1 class.
    /// </summary>
    public class Arc3D
    {
        /// <summary>
        /// Gets or sets the center.
        /// </summary>
        public Point3D Center { get; internal set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        public double Radius { get; internal set; }

        /// <summary>
        /// Gets or sets the e.
        /// </summary>
        public double End { get; internal set; }

        /// <summary>
        /// Gets or sets the s.
        /// </summary>
        public double Start { get; internal set; }
    }
}
