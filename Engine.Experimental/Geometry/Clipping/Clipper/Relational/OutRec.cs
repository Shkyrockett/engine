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
    /// The out rec class contains a path in the clipping solution. Edges in the AEL will
    /// carry a pointer to an OutRec when they are part of the clipping solution.
    /// </summary>
    public class OutRec
    {
        #region Fields

        /// <summary>
        /// The I dx.
        /// </summary>
        internal int IDx;

        /// <summary>
        /// The owner.
        /// </summary>
        internal OutRec Owner;

        /// <summary>
        /// The start e.
        /// </summary>
        internal Edge StartE;

        /// <summary>
        /// The end e.
        /// </summary>
        internal Edge EndE;

        /// <summary>
        /// The pts.
        /// </summary>
        internal OutPoint Pts;

        /// <summary>
        /// The poly path.
        /// </summary>
        internal PolyPath PolyPath;

        /// <summary>
        /// The flag.
        /// </summary>
        internal OutrecFlag Flag;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        public void SetOrientation(Edge e1, Edge e2)
        {
            StartE = e1;
            EndE = e2;
            e1.OutRec = this;
            e2.OutRec = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SwapSides()
        {
            Edge e2 = StartE;
            StartE = EndE;
            EndE = e2;
            Pts = Pts.Next;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndOutRec()
        {
            StartE.OutRec = null;
            if (EndE != null)
            {
                EndE.OutRec = null;
            }

            StartE = null;
            EndE = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftOutpt"></param>
        public void UpdateHelper(OutPoint leftOutpt)
        {
            var leftOpt = (OutPointTri)leftOutpt;
            var rightOrt = (OutRecTri)this;
            if (leftOpt != null && leftOpt.rightOutrec != null)
            {
                leftOpt.rightOutrec.leftOutpt = null;
            }

            if (rightOrt.leftOutpt != null)
            {
                rightOrt.leftOutpt.rightOutrec = null;
            }

            rightOrt.leftOutpt = leftOpt;
            if (leftOpt != null)
            {
                leftOpt.rightOutrec = rightOrt;
            }
        }
    }
}
