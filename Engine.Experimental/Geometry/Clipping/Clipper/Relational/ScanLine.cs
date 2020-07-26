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
        /// Initializes a new instance of the <see cref="ScanLine" /> class.
        /// </summary>
        public ScanLine()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanLine" /> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="nextScanLine">The next scan line.</param>
        public ScanLine(double y, ScanLine nextScanLine)
        {
            Y = y;
            NextScanLine = nextScanLine;
        }

        /// <summary>
        /// The y intercept.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y { get; set; }

        /// <summary>
        /// The next.
        /// </summary>
        /// <value>
        /// The next scan line.
        /// </value>
        public ScanLine NextScanLine { get; set; }
    }
}
