﻿/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  13 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Triangulate clipping solutions                                  *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Runtime.CompilerServices;
using static Engine.Operations;

namespace Engine.Experimental;

/// <summary>
/// ClipperTri
/// </summary>
/// <seealso cref="Engine.Experimental.Clipper" />
public class ClipperTriangulation
    : Clipper
{
    #region Fields
    /// <summary>
    /// The last op.
    /// </summary>
    private LinkedPoint lastOp;

    /// <summary>
    /// The triangles.
    /// </summary>
    private readonly Polygon2D triangles = [];
    #endregion Fields

    #region Overrides
    /// <summary>
    /// Add the local min poly.
    /// </summary>
    /// <param name="e1">The e1.</param>
    /// <param name="e2">The e2.</param>
    /// <param name="pt">The pt.</param>
    protected override void AddLocalMinPoly(Edge e1, Edge e2, Point2D pt)
    {
        base.AddLocalMinPoly(e1, e2, new(pt));

        var locMinOr = e1?.outRec;
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
            e = e2?.GetRightAdjacentHotEdge();
        }

        e ??= e1.GetLeftAdjacentHotEdge();

        LinkedPoint botLft = (e.outRec as OutRecTri).LeftOutpt;
        var botRt = e.GetOutPt();

        if (botLft is null || botRt.Pt.Y < botLft.Pt.Y)
        {
            botLft = botRt;
        }

        botRt = InsertPoint(botLft.Pt, botLft.Prev);
        var botOr = (botLft as LinkedPointTriangle).Outrec;
        if (botOr.Points is null)
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
        botOr.EndEdge.outRec = botOr;
        locMinOr.EndEdge.outRec = locMinOr;

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
        var outrec = e1?.outRec;
        //very occasionally IsStartSide(e1) is wrong so ...
        var is_outer = e1.IsStartSide() || (e1?.outRec == e2?.outRec);
        if (is_outer)
        {
            var ort = (OutRecTri)e1.outRec;
            if (ort.LeftOutpt is not null)
            {
                outrec.UpdateHelper(null);
            }

            e2?.outRec.UpdateHelper(null);
        }

        base.AddLocalMaxPoly(e1, e2, new(Pt));

        if (outrec.Points is null)
        {
            outrec = outrec.Owner;
        }

        if (is_outer)
        {
            var ort = (LinkedPointTriangle)outrec.Points;
            var ort2 = (LinkedPointTriangle)outrec.Points.Next;
            if (ort.RightOutrec is not null)
            {
                ort.RightOutrec.UpdateHelper(null);
            }
            else if (ort2.RightOutrec is not null)
            {
                ort2.RightOutrec.UpdateHelper(null);
            }
        }
        else
        {
            var e = e2?.GetRightAdjacentHotEdge();
            if (e is not null)
            {
                e.outRec.UpdateHelper(lastOp);
            }

            outrec.Points.Update(outrec);
        }

        Triangulate(outrec);
    }

    /// <summary>
    /// Create the out point.
    /// </summary>
    /// <returns>
    /// The <see cref="LinkedPoint" />.
    /// </returns>
    /// <remarks>
    /// This is a virtual method as descendant classes may need to produce descendant classes of OutPt ...
    /// </remarks>
    protected override LinkedPoint CreateOutPoint() => new LinkedPointTriangle();

    /// <summary>
    /// Create the out rec.
    /// </summary>
    /// <returns>
    /// The <see cref="OutRec" />.
    /// </returns>
    /// <remarks>
    /// This is a virtual method as descendant classes may need to produce descendant classes of OutRec ...
    /// </remarks>
    protected override OutRec CreateOutRec() => new OutRecTri();

    /// <summary>
    /// Add the out point.
    /// </summary>
    /// <param name="e">The e.</param>
    /// <param name="pt">The pt.</param>
    /// <returns>
    /// The <see cref="LinkedPoint" />.
    /// </returns>
    protected override LinkedPoint AddOutPoint(Edge e, Point2D pt)
    {
        var result = base.AddOutPoint(e, pt);
        var opt = (LinkedPointTriangle)result;
        opt.Outrec = e?.outRec;
        lastOp = result;
        Triangulate(e.outRec);
        // Triangulate() above may assign Result.OutRecRt so ...
        if (e.IsStartSide() && opt.RightOutrec is null)
        {
            var e2 = e.GetRightAdjacentHotEdge();
            if (e2 is not null)
            {
                e2.outRec.UpdateHelper(result);
            }
        }

        return result;
    }

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="clipType">The clipType.</param>
    /// <param name="ft">The ft.</param>
    /// <returns>
    /// The <see cref="Polygon2D" />.
    /// </returns>
    public override Polygon2D Execute(ClippingOperation clipType, WindingRule ft = WindingRule.EvenOdd)
    {
        var tris = new Polygon2D();
        try
        {
            if (!ExecuteInternal(clipType, ft))
            {
                return null;
            }

            tris.Capacity = triangles.Count;
            foreach (var p in triangles)
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
    /// <returns>
    /// The <see cref="Polygon2D" />.
    /// </returns>
    public override Polygon2D Execute(ClippingOperation clipType, Polygon2D Open, WindingRule ft = WindingRule.EvenOdd) => null; //unsupported

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="clipType">The clipType.</param>
    /// <param name="polytree">The polytree.</param>
    /// <param name="Open">The Open.</param>
    /// <param name="ft">The ft.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public override bool Execute(ClippingOperation clipType, PolyTree polytree, Polygon2D Open, WindingRule ft = WindingRule.EvenOdd) => false; //unsupported
    #endregion Overrides

    /// <summary>
    /// Insert the point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="afterOutPoint">The afterOutPoint.</param>
    /// <returns>
    /// The <see cref="LinkedPointTriangle" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private LinkedPointTriangle InsertPoint(Point2D point, LinkedPoint afterOutPoint)
    {
        var result = (LinkedPointTriangle)CreateOutPoint();
        result.Pt = new(point);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void AddPolygon(Point2D pt1, Point2D pt2, Point2D pt3)
    {
        var p = new PolygonContour2D
        {
            Capacity = 3
        };
        p.Add(new(pt3));
        p.Add(new(pt2));
        p.Add(new(pt1));
        triangles.Add(p);
    }

    /// <summary>
    /// The triangulate.
    /// </summary>
    /// <param name="outrec">The outrec.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
                cpval = CrossProductTriple(op.Pt.X, op.Pt.Y, op.Prev.Pt.X, op.Prev.Pt.Y, op.Prev.Prev.Pt.X, op.Prev.Prev.Pt.Y);
                if (cpval >= 0)
                {
                    break;
                }

                if (op2 != op)
                {
                    //Due to rounding, the clipping algorithm can occasionally produce
                    //tiny self-intersections and these need removing ...
                    cpval = CrossProductTriple(op2.Pt.X, op2.Pt.Y, op.Pt.X, op.Pt.Y, op.Prev.Prev.Pt.X, op.Prev.Prev.Pt.Y);
                    if (cpval > 0)
                    {
                        opt = (LinkedPointTriangle)op;
                        if (opt.Outrec is not null)
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
            if (opt.Outrec is not null)
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
