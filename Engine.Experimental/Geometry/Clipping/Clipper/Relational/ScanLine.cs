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
    /// The scan line class.
    /// </summary>
    public class ScanLine
    {
        /// <summary>
        /// The y intercept.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The next.
        /// </summary>
        public ScanLine NextScanLine { get; set; }
    };
}
