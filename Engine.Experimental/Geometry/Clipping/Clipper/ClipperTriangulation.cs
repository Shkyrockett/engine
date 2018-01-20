/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  13 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Triangulate clipping solutions                                  *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Runtime.CompilerServices;
using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// ClipperTri
    /// </summary>
    public class ClipperTriangulation
        : Clipper
    {
        #region Fields

        /// <summary>
        /// The last op.
        /// </summary>
        private LinkedPoint LastOp;

        /// <summary>
        /// The triangles.
        /// </summary>
        private readonly Polygon triangles = new Polygon();

        #endregion

        #region Overrides

        /// <summary>
        /// Add the local min poly.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="pt">The pt.</param>
        protected override void AddLocalMinPoly(Edge e1, Edge e2, Point2D pt)
        {
            base.AddLocalMinPoly(e1, e2, pt);

            var locMinOr = e1.OutRec;
            (locMinOr.Points as LinkedPointTriangle).Outrec = locMinOr;
            locMinOr.UpdateHelper(locMinOr.Points);
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

            LinkedPoint botLft = (e.OutRec as OutRecTri).LeftOutpt;
            var botRt = e.GetOutPt();

            if (botLft == null || botRt.Pt.Y < botLft.Pt.Y)
            {
                botLft = botRt;
            }

            botRt = InsertPoint(botLft.Pt, botLft.Prev);
            var botOr = (botLft as LinkedPointTriangle).Outrec;
            if (botOr.Points == null)
            {
                botOr = botOr.Owner;
            }

            var startOp = botOr.Points;
            var endOp = startOp.Next;

            locMinOr.Flag = OutrecFlag.Outer;
            locMinOr.Owner = null;
            var locMinLft = locMinOr.Points;
            var locMinRt = InsertPoint(locMinLft.Pt, locMinLft);

            //locMinOr will contain the polygon to the right of the join (ascending),
            //and botOr will contain the polygon to the left of the join (descending).

            //tail . botRt . locMinRt : locMinRt is joined by botRt tail
            locMinRt.Next = endOp;
            endOp.Prev = locMinRt;
            botRt.Next = locMinRt;
            locMinRt.Prev = botRt;
            locMinOr.Points = locMinRt;

            //locMinLft . botLft . head : locMinLft joins behind botLft (left)
            startOp.Next = locMinLft;
            locMinLft.Prev = startOp;
            botLft.Prev = locMinLft;
            locMinLft.Next = botLft;
            (locMinLft as LinkedPointTriangle).Outrec = botOr; //ie abbreviated update()

            locMinRt.Update(locMinOr); //updates the outrec for each op

            // exchange endE's ...
            e = botOr.EndEdge;
            botOr.EndEdge = locMinOr.EndEdge;
            locMinOr.EndEdge = e;
            botOr.EndEdge.OutRec = botOr;
            locMinOr.EndEdge.OutRec = locMinOr;

            // update helper info  ...
            locMinOr.UpdateHelper(locMinRt);
            botOr.UpdateHelper(botOr.Points);
            Triangulate(locMinOr);
            Triangulate(botOr);
        }

        /// <summary>
        /// Add the local max poly.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <param name="Pt">The Pt.</param>
        protected override void AddLocalMaxPoly(Edge e1, Edge e2, Point2D Pt)
        {
            var outrec = e1.OutRec;
            //very occasionally IsStartSide(e1) is wrong so ...
            var is_outer = e1.IsStartSide() || (e1.OutRec == e2.OutRec);
            if (is_outer)
            {
                var ort = (OutRecTri)(e1.OutRec);
                if (ort.LeftOutpt != null)
                {
                    outrec.UpdateHelper(null);
                }

                e2.OutRec.UpdateHelper(null);
            }

            base.AddLocalMaxPoly(e1, e2, Pt);

            if (outrec.Points == null)
            {
                outrec = outrec.Owner;
            }

            if (is_outer)
            {
                var ort = (LinkedPointTriangle)outrec.Points;
                var ort2 = (LinkedPointTriangle)outrec.Points.Next;
                if (ort.RightOutrec != null)
                {
                    ort.RightOutrec.UpdateHelper(null);
                }
                else if (ort2.RightOutrec != null)
                {
                    ort2.RightOutrec.UpdateHelper(null);
                }
            }
            else
            {
                var e = e2.GetRightAdjacentHotEdge();
                if (e != null)
                {
                    e.OutRec.UpdateHelper(LastOp);
                }

                outrec.Points.Update(outrec);
            }
            Triangulate(outrec);
        }

        /// <summary>
        /// Create the out point.
        /// </summary>
        /// <returns>The <see cref="LinkedPoint"/>.</returns>
        protected override LinkedPoint CreateOutPoint() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutPt ...
          new LinkedPointTriangle();

        /// <summary>
        /// Create the out rec.
        /// </summary>
        /// <returns>The <see cref="OutRec"/>.</returns>
        protected override OutRec CreateOutRec() =>
          //this is a virtual method as descendant classes may need
          //to produce descendant classes of OutRec ...
          new OutRecTri();

        /// <summary>
        /// Add the out point.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="pt">The pt.</param>
        /// <returns>The <see cref="LinkedPoint"/>.</returns>
        protected override LinkedPoint AddOutPoint(Edge e, Point2D pt)
        {
            var result = base.AddOutPoint(e, pt);
            var opt = (LinkedPointTriangle)result;
            opt.Outrec = e.OutRec;
            LastOp = result;
            Triangulate(e.OutRec);
            //Triangulate() above may assign Result.OutRecRt so ...
            if (e.IsStartSide() && opt.RightOutrec == null)
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
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public override Polygon Execute(ClippingOperations clipType, WindingRules ft = WindingRules.EvenOdd)
        {
            var tris = new Polygon();
            try
            {
                if (!ExecuteInternal(clipType, ft))
                {
                    return null;
                }

                tris.Capacity = triangles.Count;
                foreach (PolygonContour p in triangles)
                {
                    tris.Add(p);
                }

                return tris;
            }
            finally { CleanUp(); }
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="Open">The Open.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public override Polygon Execute(ClippingOperations clipType, Polygon Open, WindingRules ft = WindingRules.EvenOdd)
            => null; //unsupported

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="clipType">The clipType.</param>
        /// <param name="polytree">The polytree.</param>
        /// <param name="Open">The Open.</param>
        /// <param name="ft">The ft.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Execute(ClippingOperations clipType, PolyTree polytree, Polygon Open, WindingRules ft = WindingRules.EvenOdd)
            => false; //unsupported

        #endregion

        /// <summary>
        /// Insert the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="afterOutPoint">The afterOutPoint.</param>
        /// <returns>The <see cref="LinkedPointTriangle"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinkedPointTriangle InsertPoint(Point2D point, LinkedPoint afterOutPoint)
        {
            var result = (LinkedPointTriangle)CreateOutPoint();
            result.Pt = point;
            result.Prev = afterOutPoint;
            result.Next = afterOutPoint.Next;
            result.Outrec = (afterOutPoint as LinkedPointTriangle).Outrec;
            result.RightOutrec = null;
            afterOutPoint.Next.Prev = result;
            afterOutPoint.Next = result;
            return result;
        }

        /// <summary>
        /// Add the polygon.
        /// </summary>
        /// <param name="pt1">The pt1.</param>
        /// <param name="pt2">The pt2.</param>
        /// <param name="pt3">The pt3.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        /// The triangulate.
        /// </summary>
        /// <param name="outrec">The outrec.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Triangulate(OutRec outrec)
        {
            var op = outrec.Points;
            if (op.Next == op.Prev)
            {
                return;
            }

            var end_op = op.Next;
            LinkedPointTriangle opt;
            for (; ; )
            {
                var op2 = op;
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
                            opt = (LinkedPointTriangle)op;
                            if (opt.Outrec != null)
                            {
                                opt.Outrec.UpdateHelper(op2);
                            }

                            LinkedPoint.DisposeOutPt(op);
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

                opt = (LinkedPointTriangle)op.Prev;
                if (opt.Outrec != null)
                {
                    opt.Outrec.UpdateHelper(op);
                }

                LinkedPoint.DisposeOutPt(op.Prev);
                if (op != outrec.Points)
                {
                    op = op.Next;
                }
            }
        }
    }
}
