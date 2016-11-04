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
    public class Shape1
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s2"></param>
        public delegate void IntersectionsDelegate(Bezier s2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startcap"></param>
        /// <param name="forward"></param>
        /// <param name="back"></param>
        /// <param name="endcap"></param>
        /// <param name="bbox"></param>
        public Shape1(Bezier startcap, Bezier forward, Bezier back, Bezier endcap, BBox bbox)
        {
            this.startcap = startcap;
            this.forward = forward;
            this.back = back;
            this.endcap = endcap;
            this.bbox = bbox;
        }

        /// <summary>
        /// 
        /// </summary>
        public Bezier startcap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier forward { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier back { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier endcap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public BBox bbox { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public IntersectionsDelegate intersections { get; internal set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
