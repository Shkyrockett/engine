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
    public class BBox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public BBox(RangeX x, RangeX y, RangeX z = null)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        public RangeX x { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RangeX y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RangeX z { get; set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
