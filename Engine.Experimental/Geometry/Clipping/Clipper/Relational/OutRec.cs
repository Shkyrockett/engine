/*******************************************************************************
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
        /// <summary>
        /// 
        /// </summary>
        public OutRec()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="owner"></param>
        /// <param name="startEdge"></param>
        /// <param name="endEdge"></param>
        /// <param name="points"></param>
        /// <param name="polyPath"></param>
        /// <param name="flag"></param>
        public OutRec(int dx, OutRec owner, Edge startEdge, Edge endEdge, LinkedPoint points, PolyPath polyPath, OutrecFlag flag)
        {
            IDx = dx;
            Owner = owner;
            StartEdge = startEdge;
            EndEdge = endEdge;
            Points = points;
            PolyPath = polyPath;
            Flag = flag;
        }

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
        #endregion Properties

        /// <summary>
        /// Set the orientation.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        public void SetOrientation(Edge e1, Edge e2)
        {
            StartEdge = e1;
            EndEdge = e2;
            e1.outRec = this;
            e2.outRec = this;
        }

        /// <summary>
        /// The swap sides.
        /// </summary>
        public void SwapSides()
        {
            var e2 = StartEdge;
            StartEdge = EndEdge;
            EndEdge = e2;
            Points = Points.Next;
        }

        /// <summary>
        /// The end out rec.
        /// </summary>
        public void EndOutRec()
        {
            StartEdge.outRec = null;
            if (EndEdge != null)
            {
                EndEdge.outRec = null;
            }

            StartEdge = null;
            EndEdge = null;
        }

        /// <summary>
        /// Update the helper.
        /// </summary>
        /// <param name="leftOutpt">The leftOutpt.</param>
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
