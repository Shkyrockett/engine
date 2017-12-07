/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  8 November 2017                                                  *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Offset paths                                                    *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/


namespace Engine.Experimental
{
    //using PolygonContour = List<Point2D>;

    /// <summary>
    /// The path node class.
    /// </summary>
    internal class PathNode
    {
        /// <summary>
        /// The path.
        /// </summary>
        internal PolygonContour path;

        /// <summary>
        /// The join type.
        /// </summary>
        internal LineJoins joinType;

        /// <summary>
        /// The end type.
        /// </summary>
        internal LineEndType endType;

        /// <summary>
        /// The lowest idx.
        /// </summary>
        internal int lowestIdx;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathNode"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="jt">The jt.</param>
        /// <param name="et">The et.</param>
        public PathNode(PolygonContour p, LineJoins jt, LineEndType et)
        {
            joinType = jt;
            endType = et;

            var lenP = p.Count;
            if (et == LineEndType.ClosedPolygon || et == LineEndType.OpenJoined)
            {
                while (lenP > 1 && p[lenP - 1] == p[0])
                {
                    lenP--;
                }
            }
            else if (lenP == 2 && p[1] == p[0])
            {
                lenP = 1;
            }

            if (lenP == 0)
            {
                return;
            }

            if (lenP < 3 && (et == LineEndType.ClosedPolygon || et == LineEndType.OpenJoined))
            {
                if (jt == LineJoins.Round)
                {
                    endType = LineEndType.OpenRound;
                }
                else
                {
                    endType = LineEndType.OpenSquare;
                }
            }

            path = new PolygonContour()
            {
                p[0]
            };
            path.Capacity = lenP;

            Point2D lastIp = p[0];
            lowestIdx = 0;
            for (var i = 1; i < lenP; i++)
            {
                if (lastIp == p[i])
                {
                    continue;
                }

                path.Add(p[i]);
                lastIp = p[i];
                if (et != LineEndType.ClosedPolygon)
                {
                    continue;
                }

                if (p[i].Y >= path[lowestIdx].Y &&
                  (p[i].Y > path[lowestIdx].Y || p[i].X < path[lowestIdx].X))
                {
                    lowestIdx = i;
                }
            }
            if (endType == LineEndType.ClosedPolygon && path.Count < 3)
            {
                path = null;
            }
        }
    }
}
