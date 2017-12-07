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
    /// The out pt class.
    /// </summary>
    public class OutPoint
    {
        #region Fields

        /// <summary>
        /// The pt.
        /// </summary>
        internal Point2D Pt;

        /// <summary>
        /// The next.
        /// </summary>
        internal OutPoint Next;

        /// <summary>
        /// The previous.
        /// </summary>
        internal OutPoint Prev;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PointCount()
        {
            if (this == null)
            {
                return 0;
            }

            OutPoint p = this;
            var cnt = 0;
            do
            {
                cnt++;
                p = p.Next;
            } while (p != this);
            return cnt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outrec"></param>
        public void Update(OutRec outrec)
        {
            OutPoint op2 = this;
            do
            {
                var opt = (OutPointTri)op2;
                if (opt.rightOutrec != null)
                {
                    opt.rightOutrec.UpdateHelper(null);
                }

                opt.outrec = outrec;
                op2 = op2.Next;
            } while (op2 != this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        public static void DisposeOutPt(OutPoint op)
        {
            if (op.Prev != null)
            {
                op.Prev.Next = op.Next;
            }

            if (op.Next != null)
            {
                op.Next.Prev = op.Prev;
            }

            var opt = (OutPointTri)op;
            if (opt.rightOutrec != null)
            {
                opt.rightOutrec.leftOutpt = null;
            }
        }
    }
}
