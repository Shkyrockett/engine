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
    /// <summary>
    /// The path node struct.
    /// </summary>
    internal struct PathNode
    {
        #region Properties

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public PolygonContour Path { get; set; }

        /// <summary>
        /// Gets or sets the join type.
        /// </summary>
        public LineJoins JoinType { get; set; }

        /// <summary>
        /// Gets or sets the end type.
        /// </summary>
        public LineEndType EndType { get; set; }

        /// <summary>
        /// Gets or sets the lowest index.
        /// </summary>
        public int LowestIndex { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PathNode"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="jt">The jt.</param>
        /// <param name="et">The et.</param>
        public PathNode(PolygonContour p, LineJoins jt, LineEndType et)
        {
            JoinType = jt;
            EndType = et;

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
                Path = null;
                LowestIndex = 0;
                return;
            }

            if (lenP < 3 && (et == LineEndType.ClosedPolygon || et == LineEndType.OpenJoined))
            {
                EndType = jt == LineJoins.Round ? LineEndType.OpenRound : LineEndType.OpenSquare;
            }

            Path = new PolygonContour()
            {
                p[0]
            };
            Path.Capacity = lenP;

            var lastIp = p[0];
            LowestIndex = 0;
            for (var i = 1; i < lenP; i++)
            {
                if (lastIp == p[i])
                {
                    continue;
                }

                Path.Add(p[i]);
                lastIp = p[i];
                if (et != LineEndType.ClosedPolygon)
                {
                    continue;
                }

                if (p[i].Y >= Path[LowestIndex].Y &&
                  (p[i].Y > Path[LowestIndex].Y || p[i].X < Path[LowestIndex].X))
                {
                    LowestIndex = i;
                }
            }
            if (EndType == LineEndType.ClosedPolygon && Path.Count < 3)
            {
                Path = null;
            }
        }

        #endregion
    }
}
