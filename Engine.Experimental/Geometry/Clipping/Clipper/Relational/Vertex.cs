/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental
{
    /// <summary>
    /// The vertex class.
    /// </summary>
    public class Vertex
    {
        #region Fields

        /// <summary>
        /// The point.
        /// </summary>
        private Point2D point;

        /// <summary>
        /// The next vertex.
        /// </summary>
        private Vertex nextVertex;

        /// <summary>
        /// The previous vertex.
        /// </summary>
        private Vertex previousVertex;

        /// <summary>
        /// The flags.
        /// </summary>
        private VertexFlags flags;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// </summary>
        /// <param name="point">The vertex point.</param>
        public Vertex(Point2D point)
        {
            this.point = point;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The point.
        /// </summary>
        public Point2D Point { get { return point; } set { point = value; } }

        /// <summary>
        /// The next vertex.
        /// </summary>
        public Vertex NextVertex { get { return nextVertex; } set { nextVertex = value; } }

        /// <summary>
        /// The previous vertex.
        /// </summary>
        public Vertex PreviousVertex { get { return previousVertex; } set { previousVertex = value; } }

        /// <summary>
        /// The vertex flags.
        /// </summary>
        public VertexFlags Flags { get { return flags; } set { flags = value; } }

        #endregion
    }
}
