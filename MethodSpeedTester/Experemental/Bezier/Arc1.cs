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
    public class Arc1
    {
        /// <summary>
        /// 
        /// </summary>
        public Point3D center { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double radius { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double e { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double s { get; internal set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
