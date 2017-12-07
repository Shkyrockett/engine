/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Collections.Generic;

namespace Engine.Experimental
{
    /// <summary>
    /// The my intersect node sort class.
    /// </summary>
    public class MyIntersectNodeSort
        : IComparer<IntersectNode>
    {
        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="node2">The node2.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Compare(IntersectNode node1, IntersectNode node2) => node2.Point.Y.CompareTo(node1.Point.Y); //descending soft
    }
}
