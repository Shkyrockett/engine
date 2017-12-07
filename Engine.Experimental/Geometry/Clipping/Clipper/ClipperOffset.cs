/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  8 November 2017                                                  *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Offset paths                                                    *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// Clipper Offset
    /// </summary>
    public class ClipperOffset
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double delta;

        /// <summary>
        /// 
        /// </summary>
        private double sinA;

        /// <summary>
        /// 
        /// </summary>
        private double sin;

        /// <summary>
        /// 
        /// </summary>
        private double cos;

        /// <summary>
        /// nb: miterLim below is a temp field that differs from the MiterLimit property
        /// </summary>
        private double miterLim;

        /// <summary>
        /// 
        /// </summary>
        private double stepsPerRad;

        /// <summary>
        /// 
        /// </summary>
        private Polygon solution;

        /// <summary>
        /// 
        /// </summary>
        private PolygonContour pathIn;

        /// <summary>
        /// 
        /// </summary>
        private PolygonContour pathOut;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> norms = new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        private List<PathNode> nodes = new List<PathNode>();

        /// <summary>
        /// 
        /// </summary>
        private int lowestIdx;

        /// <summary>
        /// 
        /// </summary>
        private Point2D PointZero = new Point2D(0, 0);

        /// <summary>
        /// 
        /// </summary>
        private const double TwoPi = PI * 2;

        /// <summary>
        /// 
        /// </summary>
        private const double DefaultArcFrac = 0.02d;

        /// <summary>
        /// 
        /// </summary>
        private const double Tolerance = 1.0E-15;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double ArcTolerance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double MiterLimit { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MiterLimit"></param>
        /// <param name="ArcTolerance"></param>
        public ClipperOffset(double MiterLimit = 2.0, double ArcTolerance = 0)
        {
            this.MiterLimit = MiterLimit;
            this.ArcTolerance = ArcTolerance;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            nodes.Clear();
            norms.Clear();
            solution.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="jt"></param>
        /// <param name="et"></param>
        public void AddPath(PolygonContour p, LineJoins jt, LineEndType et)
        {
            var pn = new PathNode(p, jt, et);
            if (pn.path == null)
            {
                pn = null;
            }
            else
            {
                nodes.Add(pn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="jt"></param>
        /// <param name="et"></param>
        public void AddPaths(Polygon paths, LineJoins jt, LineEndType et)
        {
            foreach (PolygonContour p in paths)
            {
                AddPath(p, jt, et);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetLowestPolygonIdx()
        {
            lowestIdx = -1;
            Point2D ip1 = PointZero, ip2;
            for (var i = 0; i < nodes.Count; i++)
            {
                PathNode node = nodes[i];
                if (node.endType != LineEndType.ClosedPolygon)
                {
                    continue;
                }

                if (lowestIdx < 0)
                {
                    ip1 = node.path[node.lowestIdx];
                    lowestIdx = i;
                }
                else
                {
                    ip2 = node.path[node.lowestIdx];
                    if (ip2.Y >= ip1.Y && (ip2.Y > ip1.Y || ip2.X < ip1.X))
                    {
                        lowestIdx = i;
                        ip1 = ip2;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="jointype"></param>
        void OffsetPoint(int j, ref int k, LineJoins jointype)
        {
            //A: angle between adjoining paths on left side (left WRT winding direction).
            //A == 0 deg (or A == 360 deg): collinear edges heading in same direction
            //A == 180 deg: collinear edges heading in opposite directions (ie a 'spike')
            //sin(A) < 0: convex on left.
            //cos(A) > 0: angles on both left and right sides > 90 degrees

            //cross product ...
            sinA = (norms[k].X * norms[j].Y - norms[j].X * norms[k].Y);

            if (Abs(sinA * delta) < 1.0) //angle is approaching 180 or 360 deg.
            {
                //dot product ...
                var cosA = (norms[k].X * norms[j].X + norms[j].Y * norms[k].Y);
                if (cosA > 0) //given condition above the angle is approaching 360 deg.
                {
                    //with angles approaching 360 deg collinear (whether concave or convex),
                    //offsetting with two or more vertices (that would be so close together)
                    //occasionally causes tiny self-intersections due to rounding.
                    //So we offset with just a single vertex here ...
                    pathOut.Add(new Point2D(Maths.Round(pathIn[j].X + norms[k].X * delta),
                      Maths.Round(pathIn[j].Y + norms[k].Y * delta)));
                    return;
                }
            }
            else if (sinA > 1.0)
            {
                sinA = 1.0;
            }
            else if (sinA < -1.0)
            {
                sinA = -1.0;
            }

            if (sinA * delta < 0) //ie a concave offset
            {
                pathOut.Add(new Point2D(Maths.Round(pathIn[j].X + norms[k].X * delta),
                  Maths.Round(pathIn[j].Y + norms[k].Y * delta)));
                pathOut.Add(pathIn[j]);
                pathOut.Add(new Point2D(Maths.Round(pathIn[j].X + norms[j].X * delta),
                  Maths.Round(pathIn[j].Y + norms[j].Y * delta)));
            }
            else
            {
                //convex offsets here ...
                switch (jointype)
                {
                    case LineJoins.Miter:
                        var cosA = (norms[j].X * norms[k].X + norms[j].Y * norms[k].Y);
                        //see offset_triginometry3.svg
                        if (1 + cosA < miterLim)
                        {
                            DoSquare(j, k);
                        }
                        else
                        {
                            DoMiter(j, k, 1 + cosA);
                        }

                        break;
                    case LineJoins.Square:
                        cosA = (norms[j].X * norms[k].X + norms[j].Y * norms[k].Y);
                        if (cosA >= 0)
                        {
                            DoMiter(j, k, 1 + cosA); //angles >= 90 deg. don't need squaring
                        }
                        else
                        {
                            DoSquare(j, k);
                        }

                        break;
                    case LineJoins.Round:
                        DoRound(j, k);
                        break;
                }
            }
            k = j;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        internal void DoSquare(int j, int k)
        {
            //Two vertices, one using the prior offset's (k) normal one the current (j).
            //Do a 'normal' offset (by delta) and then another by 'de-normaling' the
            //normal hence parallel to the direction of the respective edges.
            if (delta > 0)
            {
                pathOut.Add(new Point2D(
                  Maths.Round(pathIn[j].X + delta * (norms[k].X - norms[k].Y)),
                  Maths.Round(pathIn[j].Y + delta * (norms[k].Y + norms[k].X))));
                pathOut.Add(new Point2D(
                  Maths.Round(pathIn[j].X + delta * (norms[j].X + norms[j].Y)),
                  Maths.Round(pathIn[j].Y + delta * (norms[j].Y - norms[j].X))));
            }
            else
            {
                pathOut.Add(new Point2D(
                  Maths.Round(pathIn[j].X + delta * (norms[k].X + norms[k].Y)),
                  Maths.Round(pathIn[j].Y + delta * (norms[k].Y - norms[k].X))));
                pathOut.Add(new Point2D(
                  Maths.Round(pathIn[j].X + delta * (norms[j].X - norms[j].Y)),
                  Maths.Round(pathIn[j].Y + delta * (norms[j].Y + norms[j].X))));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="cosAplus1"></param>
        internal void DoMiter(int j, int k, double cosAplus1)
        {
            //see offset_triginometry4.svg
            var q = delta / cosAplus1; //0 < cosAplus1 <= 2
            pathOut.Add(new Point2D(Maths.Round(pathIn[j].X + (norms[k].X + norms[j].X) * q),
              Maths.Round(pathIn[j].Y + (norms[k].Y + norms[j].Y) * q)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        internal void DoRound(int j, int k)
        {
            var a = Atan2(sinA,
            norms[k].X * norms[j].X + norms[k].Y * norms[j].Y);
            var steps = Max((int)Maths.Round(stepsPerRad * Abs(a)), 1);

            double X = norms[k].X, Y = norms[k].Y, X2;
            for (var i = 0; i < steps; ++i)
            {
                pathOut.Add(new Point2D(
                  Maths.Round(pathIn[j].X + X * delta),
                  Maths.Round(pathIn[j].Y + Y * delta)));
                X2 = X;
                X = X * cos - sin * Y;
                Y = X2 * sin + Y * cos;
            }
            pathOut.Add(new Point2D(
            Maths.Round(pathIn[j].X + norms[j].X * delta),
            Maths.Round(pathIn[j].Y + norms[j].Y * delta)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        private void DoOffset(double d)
        {
            solution = null;
            delta = d;
            var absDelta = Abs(d);

            //if a Zero offset, then just copy CLOSED polygons to FSolution and return ...
            if (absDelta < Tolerance)
            {
                solution = new Polygon
                {
                    Capacity = nodes.Count
                };
                foreach (PathNode node in nodes)
                {
                    if (node.endType == LineEndType.ClosedPolygon)
                    {
                        solution.Add(node.path);
                    }
                }

                return;
            }

            //MiterLimit: see offset_triginometry3.svg in the documentation folder ...
            if (MiterLimit > 2)
            {
                miterLim = 2 / (MiterLimit * MiterLimit);
            }
            else
            {
                miterLim = 0.5;
            }

            double arcTol;
            if (ArcTolerance < DefaultArcFrac)
            {
                arcTol = absDelta * DefaultArcFrac;
            }
            else
            {
                arcTol = ArcTolerance;
            }

            //see offset_triginometry2.svg in the documentation folder ...
            var steps = PI / Acos(1 - arcTol / absDelta);  //steps per 360 degrees
            if (steps > absDelta * PI)
            {
                steps = absDelta * PI; //ie excessive precision check
            }

            sin = Sin(TwoPi / steps);
            cos = Cos(TwoPi / steps);
            if (d < 0)
            {
                sin = -sin;
            }

            stepsPerRad = steps / TwoPi;

            solution = new Polygon
            {
                Capacity = nodes.Count * 2
            };
            foreach (PathNode node in nodes)
            {
                pathIn = node.path;
                pathOut = new PolygonContour();
                var pathInCnt = pathIn.Count;

                //if a single vertex then build circle or a square ...
                if (pathInCnt == 1)
                {
                    if (node.joinType == LineJoins.Round)
                    {
                        double X = 1.0, Y = 0.0;
                        for (var j = 1; j <= steps; j++)
                        {
                            pathOut.Add(new Point2D(
                              Maths.Round(pathIn[0].X + X * delta),
                              Maths.Round(pathIn[0].Y + Y * delta)));
                            var X2 = X;
                            X = X * cos - sin * Y;
                            Y = X2 * sin + Y * cos;
                        }
                    }
                    else
                    {
                        double X = -1.0, Y = -1.0;
                        for (var j = 0; j < 4; ++j)
                        {
                            pathOut.Add(new Point2D(
                              Maths.Round(pathIn[0].X + X * delta),
                              Maths.Round(pathIn[0].Y + Y * delta)));
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
                } //end of single vertex offsetting

                //build norms ...
                norms.Clear();
                norms.Capacity = pathInCnt;
                for (var j = 0; j < pathInCnt - 1; j++)
                {
                    norms.Add(GetUnitNormal(pathIn[j].X, pathIn[j].Y, pathIn[j + 1].X, pathIn[j + 1].Y));
                }

                if (node.endType == LineEndType.OpenJoined || node.endType == LineEndType.ClosedPolygon)
                {
                    norms.Add(GetUnitNormal(pathIn[pathInCnt - 1].X, pathIn[pathInCnt - 1].Y, pathIn[0].X, pathIn[0].Y));
                }
                else
                {
                    norms.Add(new Point2D(norms[pathInCnt - 2]));
                }

                if (node.endType == LineEndType.ClosedPolygon)
                {
                    var k = pathInCnt - 1;
                    for (var j = 0; j < pathInCnt; j++)
                    {
                        OffsetPoint(j, ref k, node.joinType);
                    }

                    solution.Add(pathOut);
                }
                else if (node.endType == LineEndType.OpenJoined)
                {
                    var k = pathInCnt - 1;
                    for (var j = 0; j < pathInCnt; j++)
                    {
                        OffsetPoint(j, ref k, node.joinType);
                    }

                    solution.Add(pathOut);
                    pathOut = new PolygonContour();
                    //re-build norms ...
                    Point2D n = norms[pathInCnt - 1];
                    for (var j = pathInCnt - 1; j > 0; j--)
                    {
                        norms[j] = new Point2D(-norms[j - 1].X, -norms[j - 1].Y);
                    }

                    norms[0] = new Point2D(-n.X, -n.Y);
                    k = 0;
                    for (var j = pathInCnt - 1; j >= 0; j--)
                    {
                        OffsetPoint(j, ref k, node.joinType);
                    }

                    solution.Add(pathOut);
                }
                else
                {
                    var k = 0;
                    for (var j = 1; j < pathInCnt - 1; j++)
                    {
                        OffsetPoint(j, ref k, node.joinType);
                    }

                    Point2D pt1;
                    if (node.endType == LineEndType.OpenButt)
                    {
                        var j = pathInCnt - 1;
                        pt1 = new Point2D(Maths.Round(pathIn[j].X + norms[j].X *
                          delta), Maths.Round(pathIn[j].Y + norms[j].Y * delta));
                        pathOut.Add(pt1);
                        pt1 = new Point2D(Maths.Round(pathIn[j].X - norms[j].X *
                          delta), Maths.Round(pathIn[j].Y - norms[j].Y * delta));
                        pathOut.Add(pt1);
                    }
                    else
                    {
                        var j = pathInCnt - 1;
                        k = pathInCnt - 2;
                        sinA = 0;
                        norms[j] = new Point2D(-norms[j].X, -norms[j].Y);
                        if (node.endType == LineEndType.OpenSquare)
                        {
                            DoSquare(j, k);
                        }
                        else
                        {
                            DoRound(j, k);
                        }
                    }

                    //reverse norms ...
                    for (var j = pathInCnt - 1; j > 0; j--)
                    {
                        norms[j] = new Point2D(-norms[j - 1].X, -norms[j - 1].Y);
                    }

                    norms[0] = new Point2D(-norms[1].X, -norms[1].Y);

                    k = pathInCnt - 1;
                    for (var j = k - 1; j > 0; --j)
                    {
                        OffsetPoint(j, ref k, node.joinType);
                    }

                    if (node.endType == LineEndType.OpenButt)
                    {
                        pt1 = new Point2D(Maths.Round(pathIn[0].X - norms[0].X * delta),
                          Maths.Round(pathIn[0].Y - norms[0].Y * delta));
                        pathOut.Add(pt1);
                        pt1 = new Point2D(Maths.Round(pathIn[0].X + norms[0].X * delta),
                          Maths.Round(pathIn[0].Y + norms[0].Y * delta));
                        pathOut.Add(pt1);
                    }
                    else
                    {
                        k = 1;
                        sinA = 0;
                        if (node.endType == LineEndType.OpenSquare)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sol"></param>
        /// <param name="delta"></param>
        public Polygon Execute(double delta)
        {
            Polygon sol = new Polygon();
            if (nodes.Count == 0)
            {
                return null;
            }

            GetLowestPolygonIdx();
            var negate = (lowestIdx >= 0 && Measurements.PolygonArea(nodes[lowestIdx].path.Points) < 0);
            //if polygon orientations are reversed, then 'negate' ...
            if (negate)
            {
                this.delta = -delta;
            }
            else
            {
                this.delta = delta;
            }

            DoOffset(this.delta);

            //now clean up 'corners' ...
            var clpr = new Clipper();
            clpr.AddPaths(solution, PolygonRelations.Subject);
            if (negate)
            {
                return clpr.Execute(ClipingOperations.Union, sol, WindingRules.Negative);
            }
            else
            {
                return clpr.Execute(ClipingOperations.Union, sol, WindingRules.Positive);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="delta"></param>
        /// <param name="jt"></param>
        /// <param name="et"></param>
        /// <returns></returns>
        public static Polygon OffsetPaths(Polygon pp, double delta, LineJoins jt, LineEndType et)
        {
            var co = new ClipperOffset();
            co.AddPaths(pp, jt, et);
            return co.Execute(delta);
        }
    }
}
