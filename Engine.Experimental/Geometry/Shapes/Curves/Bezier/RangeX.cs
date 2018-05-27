/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The range x class.
    /// </summary>
    public class RangeX
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeX"/> class.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public RangeX(int min, int max)
            : this(min, min + (max - min) * 0.5d, max, min - max)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeX"/> class.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="mid">The mid.</param>
        /// <param name="max">The max.</param>
        /// <param name="size">The size.</param>
        public RangeX(double min, double mid, double max, double size)
        {
            Min = min;
            Mid = mid;
            Max = max;
            Size = size;
        }

        /// <summary>
        /// Gets or sets the min.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        public double Mid { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public double Size { get; set; }
    }
}
