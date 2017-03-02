/*
  Aport of the javascript Bezier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

#pragma warning disable RCS1060 // Declare each type in separate file.


namespace Engine
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
            this.Min = min;
            this.Mid = mid;
            this.Max = max;
            this.Size = size;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Mid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Size { get; set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
