/*
  Aport of the javascript Bézier curve Utility library by Pomax.

  Based on http://pomax.github.io/bezierinfo

  This code is MIT licensed.
*/

namespace Engine
{
    /// <summary>
    /// The shape1 class.
    /// </summary>
    public class Shape1
    {
        /// <summary>
        /// The intersections delegate.
        /// </summary>
        /// <param name="s2">The s2.</param>
        public delegate void IntersectionsDelegate(Bezier s2);

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape1"/> class.
        /// </summary>
        /// <param name="startcap">The startcap.</param>
        /// <param name="forward">The forward.</param>
        /// <param name="back">The back.</param>
        /// <param name="endcap">The endcap.</param>
        /// <param name="bbox">The bbox.</param>
        public Shape1(Bezier startcap, Bezier forward, Bezier back, Bezier endcap, BBox bbox)
        {
            Startcap = startcap;
            Forward = forward;
            Back = back;
            Endcap = endcap;
            Bbox = bbox;
        }

        /// <summary>
        /// Gets or sets the startcap.
        /// </summary>
        public Bezier Startcap { get; internal set; }

        /// <summary>
        /// Gets or sets the forward.
        /// </summary>
        public Bezier Forward { get; internal set; }

        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        public Bezier Back { get; internal set; }

        /// <summary>
        /// Gets or sets the endcap.
        /// </summary>
        public Bezier Endcap { get; internal set; }

        /// <summary>
        /// Gets or sets the bbox.
        /// </summary>
        public BBox Bbox { get; internal set; }

        /// <summary>
        /// Gets or sets the intersections.
        /// </summary>
        public IntersectionsDelegate Intersections { get; internal set; }
    }
}
