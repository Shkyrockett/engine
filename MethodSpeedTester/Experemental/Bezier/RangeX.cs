/*
  Aport of the javascript Bezier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

#pragma warning disable RCS1060 // Declare each type in separate file.


namespace Engine.Geometry
{

    /// <summary>
    /// 
    /// </summary>
    public class RangeX
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RangeX(int min, int max)
            : this(min, min + (max - min) / 2d, max, min - max)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="mid"></param>
        /// <param name="max"></param>
        /// <param name="size"></param>
        public RangeX(double min, double mid, double max, double size)
        {
            this.min = min;
            this.mid = mid;
            this.max = max;
            this.size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        public double min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double mid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double max { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double size { get; set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
