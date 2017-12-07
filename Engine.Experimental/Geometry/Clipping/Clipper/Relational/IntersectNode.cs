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
    /// The intersect node struct.
    /// </summary>
    public struct IntersectNode
    {
        #region Fields

        /// <summary>
        /// The edge1.
        /// </summary>
        private Edge edgeA;

        /// <summary>
        /// The edge2.
        /// </summary>
        private Edge edgeB;

        /// <summary>
        /// The point.
        /// </summary>
        private Point2D point;

        #endregion

        #region Properties

        /// <summary>
        /// The edge1.
        /// </summary>
        public Edge EdgeA { get { return edgeA; } set { edgeA = value; } }

        /// <summary>
        /// The edge2.
        /// </summary>
        public Edge EdgeB { get { return edgeB; } set { edgeB = value; } }

        /// <summary>
        /// The point.
        /// </summary>
        public Point2D Point { get { return point; } set { point = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EdgesAdjacentInSEL()
            => (edgeA.NextInSEL == edgeB) || (edgeA.PrevInSEL == edgeB);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public static int IntersectNodeSort(IntersectNode node1, IntersectNode node2)
          // The following typecast should be safe because the differences in Pt.Y will
          //be limited to the height of the Scan-line ...
          => (int)(node2.Point.Y - node1.Point.Y);

        #endregion
    }
}
