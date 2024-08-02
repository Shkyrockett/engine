/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  8 November 2017                                                  *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Offset paths                                                    *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental;

/// <summary>
/// The end type enum.
/// </summary>
public enum LineEndType
{
    /// <summary>
    /// The shape is a closed polygon.
    /// </summary>
    ClosedPolygon,

    /// <summary>
    /// The shape is an open polyline. Use Joined ends.
    /// </summary>
    OpenJoined,

    /// <summary>
    /// The shape is an open polyline. Use Butt ends.
    /// </summary>
    OpenButt,

    /// <summary>
    /// The shape is an open polyline. Use Square ends.
    /// </summary>
    OpenSquare,

    /// <summary>
    /// The shape is an open polyline. Use Round ends.
    /// </summary>
    OpenRound
};
