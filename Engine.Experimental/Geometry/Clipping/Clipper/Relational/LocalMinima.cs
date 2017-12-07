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
    /// The local minima class.
    /// </summary>
    public class LocalMinima
    {
        /// <summary>
        /// The vertex.
        /// </summary>
        internal Vertex Vertex;

        /// <summary>
        /// The path type.
        /// </summary>
        internal PolygonRelations PathType;

        /// <summary>
        /// The is open.
        /// </summary>
        internal bool IsOpen;
    };
}
