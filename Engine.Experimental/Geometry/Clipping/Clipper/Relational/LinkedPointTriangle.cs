/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  13 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Triangulate clipping solutions                                  *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental;

/// <summary>
/// The out point tri class.
/// </summary>
/// <seealso cref="Engine.Experimental.LinkedPoint" />
public class LinkedPointTriangle
    : LinkedPoint
{
    /// <summary>
    /// The outrec.
    /// </summary>
    /// <value>
    /// The outrec.
    /// </value>
    public OutRec Outrec { get; set; }

    /// <summary>
    /// Gets or sets the right outrec.
    /// </summary>
    /// <value>
    /// The right outrec.
    /// </value>
    public OutRecTri RightOutrec { get; set; }
};
