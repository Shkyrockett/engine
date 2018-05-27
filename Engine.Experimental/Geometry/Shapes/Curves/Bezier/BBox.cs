/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The b box class.
    /// </summary>
    public class BBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BBox"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public BBox(RangeX x, RangeX y, RangeX z = null)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public RangeX X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public RangeX Y { get; set; }

        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        public RangeX Z { get; set; }
    }
}
