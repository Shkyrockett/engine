/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  13 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Triangulate clipping solutions                                  *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using static Engine.Maths;

namespace Engine.Experimental
{
    //using PolygonContour = List<Point2D>;
    //using Polygon = List<List<Point2D>>;

    /// <summary>
    /// ClipperTri 
    /// </summary>
    public class ClipperTriangulation
        : Clipper
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        OutPoint LastOp;

        /// <summary>
        /// 
        /// </summary>
        Polygon triangles = new Polygon();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="afterOutPt"></param>
        /// <returns></returns>
        private OutPointTri InsertPt(Point2D pt, OutPoint afterOutPt)
        {
            var result = (OutPointTri)CreateOutPt();
            result.Pt = pt;
            result.Prev = afterOutPt;
            result.Next = afterOutPt.Next;
            result.outrec = (afterOutPt as OutPointTri).outrec;
            result.rightOutrec = null;
            afterOutPt.Next.Prev = result;
            afterOutPt.Next = result;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        private void AddPolygon(Point2D pt1, Point2D pt2, Point2D pt3)
        {
            var p = new PolygonContour
            {
                Capacity = 3
            };
            p.Add(pt3);
            p.Add(pt2);
            p.Add(pt1);
            triangles.Add(p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outrec"></param>
        private void Triangulate(OutRec outrec)
        {
            OutPoint op = outrec.Pts;
            if (op.Next == op.Prev)
            {
                return;
            }

            OutPoint end_op = op.Next;
            OutPointTri opt;
            for (; ; )
            {
                OutPoint op2 = op;
                var cpval = 0d;
                while (op.Prev != end_op)
                {
                    cpval = CrossProductVector(op.Pt.X, op.Pt.Y, op.Prev.Pt.X, op.Prev.Pt.Y, op.Prev.Prev.Pt.X, op.Prev.Prev.Pt.Y);
                    if (cpval >= 0)
                    {
                        break;
                    }

                    if (op2 != op)
                    {
                        //Due to rounding, the clipping algorithm can occasionally produce
                        //tiny self-intersections and these need removing ...
                        cpval = CrossProductVector(op2.Pt.X, op2.Pt.Y, op.Pt.X, op.Pt.Y, op.Prev.Prev.Pt.X, op.Prev.Prev.Pt.Y);
                        if (cpval > 0)
                        {
                            opt = (OutPointTri)op;
                            if (opt.outrec != null)
                            {
                                opt.outrec.UpdateHelper(op2);
                            }

                            OutPoint.DisposeOutPt(op);
                            op = op2;
                            continue;
                        }
                    }
                    op = op.Prev;
                }

                if (op.Prev == end_op)
                {
                    break;
                }

                if (cpval != 0)
                {
                    AddPolygon(op.Pt, op.Prev.Pt, op.Prev.Prev.Pt);
                }

                opt = (OutPointTri)op.Prev;
                if (opt.outrec != null)
                {
                    opt.outrec.UpdateHelper(op);
                }

                OutPoint.DisposeOutPt(op.Prev);
                if (op != outrec.Pts)
                {
                    op = op.Next;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="pt"></param>
        protected override void AddLocalMinPoly(Edge e1, Edge e2, Point2D pt)
        {
            base.AddLocalMinPoly(e1, e2, pt);

            OutRec locMinOr = e1.OutRec;
            (locMinOr.Pts as OutPointTri).outrec = locMinOr;
            locMinOr.UpdateHelper(locMinOr.Pts);
            if (locMinOr.Flag == OutrecFlag.Outer)
            {
                return;
            }

            //do 'key-holing' ...
            var e = e1.GetRightAdjacentHotEdge();
            if (e == e2)
            {
                e = e2.GetRightAdjacentHotEdge();
            }

            if (e == null)
            {
                e = e1.GetLeftAdjacentHotEdge();
            }

            OutPoint botLft = (e.OutRec as OutRecTri).leftOutpt;
            var botRt = e.GetOutPt();

            if (botLft == null || botRt.Pt.Y < botLft.Pt.Y)
            {
                botLft = botRt;
            }

            botRt = InsertPt(botLft.Pt, botLft.Prev);
            var botOr = (botLft as OutPointTri).outrec;
            if (botOr.Pts == null)
            {
                botOr = botOr.Owner;
            }

            var startOp = botOr.Pts;
            var endOp = startOp.Next;

            locMinOr.Flag = OutrecFlag.Outer;
            locMinOr.Owner = null;
            var locMinLft = locMinOr.Pts;
            var locMinRt = InsertPt(locMinLft.Pt, locMinLft);

            //locMinOr will contain the polygon to the right of the join (ascending),
            //and botOr will contain the polygon to the left of the join (descending).

            //tail . botRt . locMinRt : locMinRt is joined by botRt tail
            locMinRt.Next = endOp;
            endOp.Prev = locMinRt;
            botRt.Next = locMinRt;
            locMinRt.Prev = botRt;
            locMinOr.Pts = locMinRt;

            //locMinLft . botLft . head : locMinLft joins behind botLft (left)
            startOp.Next = locMinLft;
            locMinLft.Prev = startOp;
            botLft.Prev = locMinLft;
            locMinLft.Next = botLft;
            (locMinLft as OutPointTri).outrec = botOr; //ie abbreviated update()

            locMinRt.Update(locMinOr); //updates the outrec for each op

            // exchange endE's ...
            e = botOr.EndE;
            botOr.EndE = locMinOr.EndE;
            locMinOr.EndE = e;
            botOr.EndE.OutRec = botOr;
            locMinOr.EndE.OutRec = locMinOr;

            // update helper info  ...
            locMinOr.UpdateHelper(locMinRt);
            botOr.UpdateHelper(botOr.Pts);
            Triangulate(locMinOr);
            Triangulate(botOr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="Pt"></param>
        protected override void AddLocalMaxPoly(Edge e1, Edge e2, Point2D Pt)
        {
            OutRec outrec = e1.OutRec;
            //very occasionally IsStartSide(e1) is wrong so ...
            var is_outer = e1.IsStartSide() || (e1.OutRec == e2.OutRec);
            if (is_outer)
            {
                var ort = (OutRecTri)(e1.OutRec);
                if (ort.leftOutpt != null)
                {
                    outrec.UpdateHelper(null);
                }

                e2.OutRec.UpdateHelper(null);
            }

            base.AddLocalMaxPoly(e1, e2, Pt);

            if (outrec.Pts == null)
            {
                outrec = outrec.Owner;
            }

            if (is_outer)
            {
                var ort = (OutPointTri)outrec.Pts;
                var ort2 = (OutPointTri)outrec.Pts.Next;
                if (ort.rightOutrec != null)
                {
                    ort.rightOutrec.UpdateHelper(null);
                }
                else if (ort2.rightOutrec != null)
                {
                    ort2.rightOutrec.UpdateHelper(null);
                }
            }
            else
            {
                var e = e2.GetRightAdjacentHotEdge();
                if (e != null)
                {
                    e.OutRec.UpdateHelper(LastOp);
                }

                outrec.Pts.Update(outrec);
            }
            Triangulate(outrec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override OutPoint CreateOutPt() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutPt ...
          new OutPointTri();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override OutRec CreateOutRec() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutRec ...
          new OutRecTri();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected override OutPoint AddOutPt(Edge e, Point2D pt)
        {
            var result = base.AddOutPt(e, pt);
            var opt = (OutPointTri)result;
            opt.outrec = e.OutRec;
            LastOp = result;
            Triangulate(e.OutRec);
            //Triangulate() above may assign Result.OutRecRt so ...
            if (e.IsStartSide() && opt.rightOutrec == null)
            {
                var e2 = e.GetRightAdjacentHotEdge();
                if (e2 != null)
                {
                    e2.OutRec.UpdateHelper(result);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public override Polygon Execute(ClipingOperations clipType, WindingRules ft = WindingRules.EvenOdd)
        {
            var tris = new Polygon();
            try
            {
                if (!ExecuteInternal(clipType, ft))
                {
                    return null;
                }

                tris.Capacity = this.triangles.Count;
                foreach (PolygonContour p in this.triangles)
                {
                    tris.Add(p);
                }

                return tris;
            }
            finally { CleanUp(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="Open"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public override Polygon Execute(ClipingOperations clipType, Polygon Open, WindingRules ft = WindingRules.EvenOdd) => null; //unsupported

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clipType"></param>
        /// <param name="polytree"></param>
        /// <param name="Open"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public override bool Execute(ClipingOperations clipType, PolyTree polytree, Polygon Open, WindingRules ft = WindingRules.EvenOdd) => false; //unsupported
    }
}
