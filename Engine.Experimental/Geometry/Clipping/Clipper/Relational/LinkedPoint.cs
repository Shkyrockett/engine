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
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedPoint"/> class.
        /// </summary>
        public LinkedPoint()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedPoint"/> class.
        /// </summary>
        /// <param name="pt">The pt.</param>
        /// <param name="next">The next.</param>
        /// <param name="prev">The previous.</param>
        public LinkedPoint(Point2D pt, LinkedPoint next, LinkedPoint prev)
        {
            Pt = new(pt);
            Next = next;
            Prev = prev;
        }

        #region Properties
        /// <summary>
        /// The pt.
        /// </summary>
        /// <value>
        /// The pt.
        /// </value>
        public Point2D Pt { get; set; }

        /// <summary>
        /// The next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        public LinkedPoint Next { get; set; }

        /// <summary>
        /// The previous.
        /// </summary>
        /// <value>
        /// The previous.
        /// </value>
        public LinkedPoint Prev { get; set; }
        #endregion Properties

        /// <summary>
        /// The point count.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public int PointCount()
        {
            if (this is null)
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
        /// Update.
        /// </summary>
        /// <param name="outrec">The outrec.</param>
        public void Update(OutRec outrec)
        {
            var op2 = this;
            do
            {
                var opt = (LinkedPointTriangle)op2;
                if (opt.RightOutrec is not null)
                {
                    opt.RightOutrec.UpdateHelper(null);
                }

                opt.Outrec = outrec;
                op2 = op2.Next;
            } while (op2 != this);
        }

        /// <summary>
        /// Dispose the out pt.
        /// </summary>
        /// <param name="op">The op.</param>
        public static void DisposeOutPt(LinkedPoint op)
        {
            if (op?.Prev is not null)
            {
                op.Prev.Next = op.Next;
            }

            if (op.Next is not null)
            {
                op.Next.Prev = op.Prev;
            }

            var opt = (LinkedPointTriangle)op;
            if (opt.RightOutrec is not null)
            {
                opt.RightOutrec.LeftOutpt = null;
            }
        }
    }
}
