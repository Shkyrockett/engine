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
    public class Line1
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public Line1(Point3D p1, Point3D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point3D P1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point3D P2 { get; set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
