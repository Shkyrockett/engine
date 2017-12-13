/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  13 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Triangulate clipping solutions                                  *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental
{
    /// <summary>
    /// The out rec tri class.
    /// </summary>
    public class OutRecTri
        : OutRec
    {
        /// <summary>
        /// 
        /// </summary>
        public LinkedPointTriangle LeftOutpt { get; set; }
    };
}
