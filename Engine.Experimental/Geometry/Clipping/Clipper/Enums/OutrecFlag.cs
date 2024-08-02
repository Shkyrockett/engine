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
/// The out record join mode flag enum.
/// </summary>
public enum OutrecFlag
{
    /// <summary>
    /// The Inner.
    /// </summary>
    Inner,

    /// <summary>
    /// The Outer.
    /// </summary>
    Outer,

    /// <summary>
    /// The Open.
    /// </summary>
    Open
};
