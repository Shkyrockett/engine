﻿/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System;
using System.Runtime.CompilerServices;

namespace Engine.Experimental
{
    /// <summary>
    /// The intersect node struct.
    /// </summary>
    public struct IntersectNode
        : IComparable<IntersectNode>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the point of intersection.
        /// </summary>
        public Point2D Point { get; set; }

        /// <summary>
        /// Gets or sets the first edge.
        /// </summary>
        public Edge EdgeA { get; set; }

        /// <summary>
        /// Gets or sets the second edge.
        /// </summary>
        public Edge EdgeB { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The edges adjacent in SEL.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool EdgesAdjacentInSEL()
            => (EdgeA.NextInSEL == EdgeB) || (EdgeA.PrevInSEL == EdgeB);

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(IntersectNode other)
            => Compare(this, other);

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="node1">The node1.</param>
        /// <param name="node2">The node2.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Compare(IntersectNode node1, IntersectNode node2)
            => node2.Point.Y.CompareTo(node1.Point.Y); // Soft descending sort.

        #endregion
    }
}
