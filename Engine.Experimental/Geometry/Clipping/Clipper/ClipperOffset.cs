﻿/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  8 November 2017                                                  *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Offset paths                                                    *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine.Experimental;

/// <summary>
/// Clipper Offset
/// </summary>
public class ClipperOffset
{
    #region Constants
    /// <summary>
    /// The default arc frac (const). Value: 0.02d.
    /// </summary>
    private const double defaultArcFrac = 0.02d;

    /// <summary>
    /// The tolerance (const). Value: 1.0E-15.
    /// </summary>
    private const double tolerance = 1.0E-15;
    #endregion Constants

    #region Fields
    /// <summary>
    /// The delta.
    /// </summary>
    private double delta;

    /// <summary>
    /// The sin a.
    /// </summary>
    private double sinA;

    /// <summary>
    /// The sin.
    /// </summary>
    private double sin;

    /// <summary>
    /// The cos.
    /// </summary>
    private double cos;

    /// <summary>
    /// Important: miterLim below is a temp field that differs from the MiterLimit property
    /// </summary>
    private double miterLim;

    /// <summary>
    /// The steps per rad.
    /// </summary>
    private double stepsPerRad;

    /// <summary>
    /// The solution.
    /// </summary>
    private Polygon2D solution;

    /// <summary>
    /// The path in.
    /// </summary>
    private PolygonContour2D pathIn;

    /// <summary>
    /// The path out.
    /// </summary>
    private PolygonContour2D pathOut;

    /// <summary>
    /// The lowest idx.
    /// </summary>
    private int lowestIdx;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="ClipperOffset" /> class.
    /// </summary>
    /// <param name="MiterLimit">The MiterLimit.</param>
    /// <param name="ArcTolerance">The ArcTolerance.</param>
    public ClipperOffset(double MiterLimit = 2d, double ArcTolerance = 0d)
    {
        this.MiterLimit = MiterLimit;
        this.ArcTolerance = ArcTolerance;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the norms.
    /// </summary>
    /// <value>
    /// The norms.
    /// </value>
    private List<Point2D> Norms { get; set; } = [];

    /// <summary>
    /// Gets or sets the nodes.
    /// </summary>
    /// <value>
    /// The nodes.
    /// </value>
    private List<PathNode> Nodes { get; set; } = [];

    /// <summary>
    /// Gets or sets the arc tolerance.
    /// </summary>
    /// <value>
    /// The arc tolerance.
    /// </value>
    public double ArcTolerance { get; set; }

    /// <summary>
    /// Gets or sets the miter limit.
    /// </summary>
    /// <value>
    /// The miter limit.
    /// </value>
    public double MiterLimit { get; set; }
    #endregion Properties

    #region Methods
    /// <summary>
    /// Add the path.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <param name="jt">The jt.</param>
    /// <param name="et">The et.</param>
    public void AddPath(PolygonContour2D p, LineJoin jt, LineEndType et)
    {
        ArgumentNullException.ThrowIfNull(p);

        PathNode? pn = new PathNode(p, jt, et);
        if (pn.Value.Path is not null)
        {
            Nodes.Add(pn.Value);
        }
    }

    /// <summary>
    /// Add the paths.
    /// </summary>
    /// <param name="paths">The paths.</param>
    /// <param name="jt">The jt.</param>
    /// <param name="et">The et.</param>
    public void AddPaths(Polygon2D paths, LineJoin jt, LineEndType et)
    {
        ArgumentNullException.ThrowIfNull(paths);

        foreach (var p in paths)
        {
            AddPath(p, jt, et);
        }
    }

    /// <summary>
    /// The execute.
    /// </summary>
    /// <param name="delta">The delta.</param>
    /// <returns>
    /// The <see cref="Polygon2D" />.
    /// </returns>
    public Polygon2D Execute(double delta)
    {
        var sol = new Polygon2D();
        if (Nodes.Count == 0)
        {
            return null;
        }

        GetLowestPolygonIdx();
        var negate = lowestIdx >= 0 && Measurements.PolygonArea(Nodes[lowestIdx].Path.Points) < 0;
        // if polygon orientations are reversed, then 'negate' ...
        if (negate)
        {
            this.delta = -delta;
        }
        else
        {
            this.delta = delta;
        }

        DoOffset(this.delta);

        // now clean up 'corners' ...
        var clpr = new Clipper();
        clpr.AddPaths(solution, ClippingRelation.Subject);
        return negate ? clpr.Execute(ClippingOperation.Union, sol, WindingRule.Negative) : clpr.Execute(ClippingOperation.Union, sol, WindingRule.Positive);
    }

    /// <summary>
    /// The offset paths.
    /// </summary>
    /// <param name="pp">The pp.</param>
    /// <param name="delta">The delta.</param>
    /// <param name="jt">The jt.</param>
    /// <param name="et">The et.</param>
    /// <returns>
    /// The <see cref="Polygon2D" />.
    /// </returns>
    public static Polygon2D OffsetPaths(Polygon2D pp, double delta, LineJoin jt, LineEndType et)
    {
        var co = new ClipperOffset();
        co.AddPaths(pp, jt, et);
        return co.Execute(delta);
    }
    #endregion Methods

    /// <summary>
    /// Get the lowest polygon idx.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void GetLowestPolygonIdx()
    {
        lowestIdx = -1;
        var ip1 = Point2D.Empty;
        Point2D ip2;
        for (var i = 0; i < Nodes.Count; i++)
        {
            var node = Nodes[i];
            if (node.EndType != LineEndType.ClosedPolygon)
            {
                continue;
            }

            if (lowestIdx < 0)
            {
                ip1 = new(node.Path[node.LowestIndex]);
                lowestIdx = i;
            }
            else
            {
                ip2 = node.Path[node.LowestIndex];
                if (ip2.Y >= ip1.Y && (ip2.Y > ip1.Y || ip2.X < ip1.X))
                {
                    lowestIdx = i;
                    ip1 = new(ip2);
                }
            }
        }
    }

    /// <summary>
    /// The offset point.
    /// </summary>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="jointype">The jointype.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void OffsetPoint(int j, ref int k, LineJoin jointype)
    {
        // A: angle between adjoining paths on left side (left WRT winding direction).
        // A == 0 deg (or A == 360 deg): collinear edges heading in same direction
        // A == 180 deg: collinear edges heading in opposite directions (IE a 'spike')
        // sin(A) < 0: convex on left.
        // cos(A) > 0: angles on both left and right sides > 90 degrees

        // cross product ...
        sinA = (Norms[k].X * Norms[j].Y) - (Norms[j].X * Norms[k].Y);

        if (Abs(sinA * delta) < 1d) // angle is approaching 180 or 360 deg.
        {
            // dot product ...
            var cosA = (Norms[k].X * Norms[j].X) + (Norms[j].Y * Norms[k].Y);
            if (cosA > 0) // given condition above the angle is approaching 360 deg.
            {
                // with angles approaching 360 deg collinear (whether concave or convex),
                // offsetting with two or more vertices (that would be so close together)
                // occasionally causes tiny self-intersections due to rounding.
                // So we offset with just a single vertex here ...
                pathOut.Add(new(
                    pathIn[j].X + (Norms[k].X * delta),
                    pathIn[j].Y + (Norms[k].Y * delta)));
                return;
            }
        }
        else if (sinA > 1d)
        {
            sinA = 1d;
        }
        else if (sinA < -1d)
        {
            sinA = -1d;
        }

        if (sinA * delta < 0) // ie a concave offset
        {
            pathOut.Add(new(
                pathIn[j].X + (Norms[k].X * delta),
                pathIn[j].Y + (Norms[k].Y * delta)));
            pathOut.Add(pathIn[j]);
            pathOut.Add(new(
                pathIn[j].X + (Norms[j].X * delta),
                pathIn[j].Y + (Norms[j].Y * delta)));
        }
        else
        {
            // convex offsets here ...
            switch (jointype)
            {
                case LineJoin.Miter:
                    var cosA = (Norms[j].X * Norms[k].X) + (Norms[j].Y * Norms[k].Y);
                    // see offset_triginometry3.svg
                    if (1 + cosA < miterLim)
                    {
                        DoSquare(j, k);
                    }
                    else
                    {
                        DoMiter(j, k, 1 + cosA);
                    }

                    break;
                case LineJoin.Square:
                    cosA = (Norms[j].X * Norms[k].X) + (Norms[j].Y * Norms[k].Y);
                    if (cosA >= 0)
                    {
                        DoMiter(j, k, 1 + cosA); // angles >= 90 deg. don't need squaring
                    }
                    else
                    {
                        DoSquare(j, k);
                    }

                    break;
                case LineJoin.Round:
                    DoRound(j, k);
                    break;
            }
        }

        k = j;
    }

    /// <summary>
    /// Do the square.
    /// </summary>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void DoSquare(int j, int k)
    {
        // Two vertices, one using the prior offset's (k) normal one the current (j).
        // Do a 'normal' offset (by delta) and then another by 'de-normaling' the
        // normal hence parallel to the direction of the respective edges.

        if (delta > 0)
        {
            pathOut.Add(new(
                pathIn[j].X + (delta * (Norms[k].X - Norms[k].Y)),
                pathIn[j].Y + (delta * (Norms[k].Y + Norms[k].X))));
            pathOut.Add(new(
                pathIn[j].X + (delta * (Norms[j].X + Norms[j].Y)),
                pathIn[j].Y + (delta * (Norms[j].Y - Norms[j].X))));
        }
        else
        {
            pathOut.Add(new(
                pathIn[j].X + (delta * (Norms[k].X + Norms[k].Y)),
                pathIn[j].Y + (delta * (Norms[k].Y - Norms[k].X))));
            pathOut.Add(new(
                pathIn[j].X + (delta * (Norms[j].X - Norms[j].Y)),
                pathIn[j].Y + (delta * (Norms[j].Y + Norms[j].X))));
        }
    }

    /// <summary>
    /// Do the miter.
    /// </summary>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="cosAplus1">The cosAplus1.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void DoMiter(int j, int k, double cosAplus1)
    {
        // see offset_triginometry4.svg
        var q = delta / cosAplus1; //0 < cosAplus1 <= 2
        pathOut.Add(new(
            pathIn[j].X + ((Norms[k].X + Norms[j].X) * q),
            pathIn[j].Y + ((Norms[k].Y + Norms[j].Y) * q)));
    }

    /// <summary>
    /// Do the round.
    /// </summary>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void DoRound(int j, int k)
    {
        var normsj = new Point2D(Norms[j]);
        var normsk = new Point2D(Norms[k]);

        // ToDo: Figure out what is being accomplished here as Atan2 is slow.
        var a = Atan2(sinA, (normsk.X * normsj.X) + (normsk.Y * normsj.Y));
        var steps = Max(stepsPerRad * Abs(a), 1d);

        var pathj = pathIn[j];
        var X = normsk.X;
        var Y = normsk.Y;
        double tempX;
        for (var i = 0; i < steps; ++i)
        {
            pathOut.Add(new(
                pathj.X + (X * delta),
                pathj.Y + (Y * delta)));
            tempX = X;
            X = (X * cos) - (sin * Y);
            Y = (tempX * sin) + (Y * cos);
        }
        pathOut.Add(new(
                pathj.X + (normsj.X * delta),
                pathj.Y + (normsj.Y * delta)));
    }

    /// <summary>
    /// Do the offset.
    /// </summary>
    /// <param name="d">The d.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void DoOffset(double d)
    {
        solution = null;
        delta = d;
        var absDelta = Abs(d);

        // if a Zero offset, then just copy CLOSED polygons to FSolution and return ...
        if (absDelta < tolerance)
        {
            solution = new Polygon2D
            {
                Capacity = Nodes.Count
            };
            foreach (var node in Nodes)
            {
                if (node.EndType == LineEndType.ClosedPolygon)
                {
                    solution.Add(node.Path);
                }
            }

            return;
        }

        // MiterLimit: see offset_triginometry3.svg in the documentation folder ...
        miterLim = MiterLimit > 2 ? 2 / (MiterLimit * MiterLimit) : 0.5;

        double arcTol;
        arcTol = ArcTolerance < defaultArcFrac ? absDelta * defaultArcFrac : ArcTolerance;

        // see offset_triginometry2.svg in the documentation folder ...
        var steps = PI / Acos(1 - (arcTol / absDelta));  // steps per 360 degrees
        if (steps > absDelta * PI)
        {
            steps = absDelta * PI; // ie excessive precision check
        }

        sin = Sin(Tau / steps);
        cos = Cos(Tau / steps);
        if (d < 0)
        {
            sin = -sin;
        }

        stepsPerRad = steps / Tau;

        solution = new Polygon2D
        {
            Capacity = Nodes.Count * 2
        };
        foreach (var node in Nodes)
        {
            pathIn = node.Path;
            pathOut = [];
            var pathInCnt = pathIn.Count;

            // if a single vertex then build circle or a square ...
            if (pathInCnt == 1)
            {
                if (node.JoinType == LineJoin.Round)
                {
                    var X = 1.0;
                    var Y = 0.0;
                    for (var j = 1; j <= steps; j++)
                    {
                        pathOut.Add(new(
                          pathIn[0].X + (X * delta),
                          pathIn[0].Y + (Y * delta)));
                        var X2 = X;
                        X = (X * cos) - (sin * Y);
                        Y = (X2 * sin) + (Y * cos);
                    }
                }
                else
                {
                    var X = -1.0;
                    var Y = -1.0;
                    for (var j = 0; j < 4; ++j)
                    {
                        pathOut.Add(new(
                          pathIn[0].X + (X * delta),
                          pathIn[0].Y + (Y * delta)));
                        if (X < 0)
                        {
                            X = 1;
                        }
                        else if (Y < 0)
                        {
                            Y = 1;
                        }
                        else
                        {
                            X = -1;
                        }
                    }
                }
                solution.Add(pathOut);
                continue;
            }

            // build norms ...
            Norms.Clear();
            Norms.Capacity = pathInCnt;
            for (var j = 0; j < pathInCnt - 1; j++)
            {
                Norms.Add(UnitNormal(pathIn[j].X, pathIn[j].Y, pathIn[j + 1].X, pathIn[j + 1].Y));
            }

            if (node.EndType == LineEndType.OpenJoined || node.EndType == LineEndType.ClosedPolygon)
            {
                Norms.Add(UnitNormal(pathIn[pathInCnt - 1].X, pathIn[pathInCnt - 1].Y, pathIn[0].X, pathIn[0].Y));
            }
            else
            {
                Norms.Add(new(Norms[pathInCnt - 2]));
            }

            if (node.EndType == LineEndType.ClosedPolygon)
            {
                var k = pathInCnt - 1;
                for (var j = 0; j < pathInCnt; j++)
                {
                    OffsetPoint(j, ref k, node.JoinType);
                }

                solution.Add(pathOut);
            }
            else if (node.EndType == LineEndType.OpenJoined)
            {
                var k = pathInCnt - 1;
                for (var j = 0; j < pathInCnt; j++)
                {
                    OffsetPoint(j, ref k, node.JoinType);
                }

                solution.Add(pathOut);
                pathOut = [];
                // re-build norms ...
                var n = Norms[pathInCnt - 1];
                for (var j = pathInCnt - 1; j > 0; j--)
                {
                    Norms[j] = new(-Norms[j - 1].X, -Norms[j - 1].Y);
                }

                Norms[0] = new(-n.X, -n.Y);
                k = 0;
                for (var j = pathInCnt - 1; j >= 0; j--)
                {
                    OffsetPoint(j, ref k, node.JoinType);
                }

                solution.Add(pathOut);
            }
            else
            {
                var k = 0;
                for (var j = 1; j < pathInCnt - 1; j++)
                {
                    OffsetPoint(j, ref k, node.JoinType);
                }

                Point2D pt1;
                if (node.EndType == LineEndType.OpenButt)
                {
                    var j = pathInCnt - 1;
                    pt1 = new(
                        pathIn[j].X + (Norms[j].X * delta),
                        pathIn[j].Y + (Norms[j].Y * delta));
                    pathOut.Add(pt1);
                    pt1 = new(
                        pathIn[j].X - (Norms[j].X * delta),
                        pathIn[j].Y - (Norms[j].Y * delta));
                    pathOut.Add(pt1);
                }
                else
                {
                    var j = pathInCnt - 1;
                    k = pathInCnt - 2;
                    sinA = 0;
                    Norms[j] = new(-Norms[j].X, -Norms[j].Y);
                    if (node.EndType == LineEndType.OpenSquare)
                    {
                        DoSquare(j, k);
                    }
                    else
                    {
                        DoRound(j, k);
                    }
                }

                // reverse norms ...
                for (var j = pathInCnt - 1; j > 0; j--)
                {
                    Norms[j] = new(-Norms[j - 1].X, -Norms[j - 1].Y);
                }

                Norms[0] = new(-Norms[1].X, -Norms[1].Y);

                k = pathInCnt - 1;
                for (var j = k - 1; j > 0; --j)
                {
                    OffsetPoint(j, ref k, node.JoinType);
                }

                if (node.EndType == LineEndType.OpenButt)
                {
                    pt1 = new(
                        pathIn[0].X - (Norms[0].X * delta),
                        pathIn[0].Y - (Norms[0].Y * delta));
                    pathOut.Add(pt1);
                    pt1 = new(
                        pathIn[0].X + (Norms[0].X * delta),
                        pathIn[0].Y + (Norms[0].Y * delta));
                    pathOut.Add(pt1);
                }
                else
                {
                    k = 1;
                    sinA = 0;
                    if (node.EndType == LineEndType.OpenSquare)
                    {
                        DoSquare(0, 1);
                    }
                    else
                    {
                        DoRound(0, 1);
                    }
                }
                solution.Add(pathOut);
            }
        }
    }
}
