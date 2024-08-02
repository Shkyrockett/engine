/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental;

/// <summary>
/// The out rec class contains a path in the clipping solution. Edges in the AEL will
/// carry a pointer to an OutRec when they are part of the clipping solution.
/// </summary>
public class OutRec
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OutRec"/> class.
    /// </summary>
    public OutRec()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutRec"/> class.
    /// </summary>
    /// <param name="dx">The dx.</param>
    /// <param name="owner">The owner.</param>
    /// <param name="startEdge">The start edge.</param>
    /// <param name="endEdge">The end edge.</param>
    /// <param name="points">The points.</param>
    /// <param name="polyPath">The poly path.</param>
    /// <param name="flag">The flag.</param>
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
    /// <value>
    /// The i dx.
    /// </value>
    public int IDx { get; set; }

    /// <summary>
    /// Gets or sets the owner.
    /// </summary>
    /// <value>
    /// The owner.
    /// </value>
    public OutRec Owner { get; set; }

    /// <summary>
    /// Gets or sets the start edge.
    /// </summary>
    /// <value>
    /// The start edge.
    /// </value>
    public Edge StartEdge { get; set; }

    /// <summary>
    /// Gets or sets the end edge.
    /// </summary>
    /// <value>
    /// The end edge.
    /// </value>
    public Edge EndEdge { get; set; }

    /// <summary>
    /// Gets or sets the points.
    /// </summary>
    /// <value>
    /// The points.
    /// </value>
    public LinkedPoint Points { get; set; }

    /// <summary>
    /// Gets or sets the poly path.
    /// </summary>
    /// <value>
    /// The poly path.
    /// </value>
    public PolyPath PolyPath { get; set; }

    /// <summary>
    /// Gets or sets the flag.
    /// </summary>
    /// <value>
    /// The flag.
    /// </value>
    public OutrecFlag Flag { get; set; }
    #endregion Properties

    /// <summary>
    /// Set the orientation.
    /// </summary>
    /// <param name="e1">The e1.</param>
    /// <param name="e2">The e2.</param>
    /// <exception cref="System.ArgumentNullException">
    /// e1
    /// or
    /// e2
    /// </exception>
    public void SetOrientation(Edge e1, Edge e2)
    {
        StartEdge = e1 ?? throw new System.ArgumentNullException(nameof(e1));
        EndEdge = e2 ?? throw new System.ArgumentNullException(nameof(e2));
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
        if (EndEdge is not null)
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
        if (leftOpt is not null && leftOpt.RightOutrec is not null)
        {
            leftOpt.RightOutrec.LeftOutpt = null;
        }

        if (rightOrt.LeftOutpt is not null)
        {
            rightOrt.LeftOutpt.RightOutrec = null;
        }

        rightOrt.LeftOutpt = leftOpt;
        if (leftOpt is not null)
        {
            leftOpt.RightOutrec = rightOrt;
        }
    }
}
