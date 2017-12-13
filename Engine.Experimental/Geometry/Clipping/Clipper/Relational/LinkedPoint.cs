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
    /// A doubly Linked point list.
    /// </summary>
    public class LinkedPoint
    {
        #region Properties

        /// <summary>
        /// The pt.
        /// </summary>
        public Point2D Pt { get; set; }

        /// <summary>
        /// The next.
        /// </summary>
        public LinkedPoint Next { get; set; }

        /// <summary>
        /// The previous.
        /// </summary>
        public LinkedPoint Prev { get; set; }

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

            var p = this;
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
            var op2 = this;
            do
            {
                var opt = (LinkedPointTriangle)op2;
                if (opt.RightOutrec != null)
                {
                    opt.RightOutrec.UpdateHelper(null);
                }

                opt.Outrec = outrec;
                op2 = op2.Next;
            } while (op2 != this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        public static void DisposeOutPt(LinkedPoint op)
        {
            if (op.Prev != null)
            {
                op.Prev.Next = op.Next;
            }

            if (op.Next != null)
            {
                op.Next.Prev = op.Prev;
            }

            var opt = (LinkedPointTriangle)op;
            if (opt.RightOutrec != null)
            {
                opt.RightOutrec.LeftOutpt = null;
            }
        }
    }
}
