/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

#pragma warning disable RCS1060 // Declare each type in separate file.


namespace Engine
{

    /// <summary>
    /// 
    /// </summary>
    public class Arc1
    {
        /// <summary>
        /// 
        /// </summary>
        public Point3D Center { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double Radius { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double E { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double S { get; internal set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
