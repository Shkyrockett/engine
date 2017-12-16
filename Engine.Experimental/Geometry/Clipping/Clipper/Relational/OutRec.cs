﻿/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental
{
    /// <summary>
    /// The out rec class contains a path in the clipping solution. Edges in the AEL will
    /// carry a pointer to an OutRec when they are part of the clipping solution.
    /// </summary>
    public class OutRec
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Idx.
        /// </summary>
        public int IDx { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public OutRec Owner { get; set; }

        /// <summary>
        /// Gets or sets the start edge.
        /// </summary>
        public Edge StartEdge { get; set; }

        /// <summary>
        /// Gets or sets the end edge.
        /// </summary>
        public Edge EndEdge { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public LinkedPoint Points { get; set; }

        /// <summary>
        /// Gets or sets the poly path.
        /// </summary>
        public PolyPath PolyPath { get; set; }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        public OutrecFlag Flag { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        public void SetOrientation(Edge e1, Edge e2)
        {
            StartEdge = e1;
            EndEdge = e2;
            e1.OutRec = this;
            e2.OutRec = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SwapSides()
        {
            var e2 = StartEdge;
            StartEdge = EndEdge;
            EndEdge = e2;
            Points = Points.Next;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndOutRec()
        {
            StartEdge.OutRec = null;
            if (EndEdge != null)
            {
                EndEdge.OutRec = null;
            }

            StartEdge = null;
            EndEdge = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftOutpt"></param>
        public void UpdateHelper(LinkedPoint leftOutpt)
        {
            var leftOpt = (LinkedPointTriangle)leftOutpt;
            var rightOrt = (OutRecTri)this;
            if (leftOpt != null && leftOpt.RightOutrec != null)
            {
                leftOpt.RightOutrec.LeftOutpt = null;
            }

            if (rightOrt.LeftOutpt != null)
            {
                rightOrt.LeftOutpt.RightOutrec = null;
            }

            rightOrt.LeftOutpt = leftOpt;
            if (leftOpt != null)
            {
                leftOpt.RightOutrec = rightOrt;
            }
        }
    }
}