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
    /// The my local min sort class.
    /// </summary>
    internal class MyLocalMinSort
        : IComparer<LocalMinima>
    {
        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="lm1">The lm1.</param>
        /// <param name="lm2">The lm2.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Compare(LocalMinima lm1, LocalMinima lm2) => lm2.Vertex.Point.Y.CompareTo(lm1.Vertex.Point.Y); //descending soft
    }
}
