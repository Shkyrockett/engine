﻿/*******************************************************************************
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
/// The out rec tri class.
/// </summary>
/// <seealso cref="Engine.Experimental.OutRec" />
public class OutRecTri
    : OutRec
{
    /// <summary>
    /// Gets or sets the left outpt.
    /// </summary>
    /// <value>
    /// The left outpt.
    /// </value>
    public LinkedPointTriangle LeftOutpt { get; set; }
};
