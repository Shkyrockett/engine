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
            Startcap = startcap;
            Forward = forward;
            Back = back;
            Endcap = endcap;
            Bbox = bbox;
        }

        /// <summary>
        /// 
        /// </summary>
        public Bezier Startcap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier Forward { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier Back { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Bezier Endcap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public BBox Bbox { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public IntersectionsDelegate Intersections { get; internal set; }
    }
}

#pragma warning restore RCS1060 // Declare each type in separate file.
