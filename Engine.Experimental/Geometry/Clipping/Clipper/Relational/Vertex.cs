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
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// </summary>
        /// <param name="point">The vertex point.</param>
        public Vertex(Point2D point)
        {
            Point = point;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// Gets or sets the next vertex.
        /// </summary>
        public Vertex NextVertex { get; set; }

        /// <summary>
        /// Gets or sets the previous vertex.
        /// </summary>
        public Vertex PreviousVertex { get; set; }

        /// <summary>
        /// Gets or sets the vertex flags.
        /// </summary>
        public VertexFlag Flags { get; set; }
        #endregion Properties
    }
}
