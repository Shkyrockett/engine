/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental;

/// <summary>
/// The vertex flags enum.
/// </summary>
[Flags]
public enum VertexFlags
{
    /// <summary>
    /// The OpenStart = 1.
    /// </summary>
    OpenStart = 1,

    /// <summary>
    /// The OpenEnd = 2.
    /// </summary>
    OpenEnd = 2,

    /// <summary>
    /// The LocMax = 4.
    /// </summary>
    LocMax = 4,

    /// <summary>
    /// The LocMin = 8.
    /// </summary>
    LocMin = 8
};
