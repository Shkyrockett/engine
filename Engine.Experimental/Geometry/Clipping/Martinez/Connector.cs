// <copyright file="Connector.cs" >
//     Copyright © 2011 Mahir Iqbal. All rights reserved.
// </copyright>
// <author id="akavel">Mahir Iqbal</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/akavel/martinez-src </remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The connector class.
    /// </summary>
    public class Connector
    {
        #region Fields

        /// <summary>
        /// The open polygons.
        /// </summary>
        private List<PointChain> openPolygons;

        /// <summary>
        /// The closed polygons.
        /// </summary>
        private List<PointChain> closedPolygons;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Connector"/> class.
        /// </summary>
        public Connector()
        {
            openPolygons = new List<PointChain>();
            closedPolygons = new List<PointChain>();
        }

        #endregion

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="s">The s.</param>
        public void Add(LineSegment s)
        {
            // j iterates through the openPolygon chains.
            for (var j = 0; j < openPolygons.Count; j++)
            {
                var chain = openPolygons[j];
                if (chain.LinkSegment(s))
                {
                    if (chain.Closed)
                    {
                        if (chain.PointList.Count == 2)
                        {
                            // We tried linking the same segment (but flipped end and start) to 
                            // a chain. (i.e. chain was <p0, p1>, we tried linking Segment(p1, p0)
                            // so the chain was closed illegally.
                            chain.Closed = false;
                            return;
                        }

                        openPolygons.Splice(j, 1);
                        closedPolygons.Add(chain);
                    }
                    else
                    {
                        for (var i = j + 1; i < openPolygons.Count; i++)
                        {
                            // Try to connect this open link to the rest of the chains. 
                            // We won't be able to connect this to any of the chains preceding this one
                            // because we know that linkSegment failed on those.
                            if (chain.LinkPointChain(openPolygons[i]))
                            {
                                openPolygons.Splice(i, 1);
                                break;
                            }
                        }
                    }

                    return;
                }
            }

            var newChain = new PointChain(s);
            openPolygons.Add(newChain);
        }

        /// <summary>
        /// The to polygon.
        /// </summary>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public Polygon ToPolygon()
        {
            var polygon = new Polygon();
            foreach (var pointChain in closedPolygons)
            {
                /*if (pointChain.pointList.length == 2)
                {
                    // Invalid contour...
                    throw new Error("Invalid contour");
                }*/

                var c = new PolygonContour();
                foreach (var p in pointChain.PointList)
                    c.Add(p);
                polygon.Add(c);

            }
            return polygon;
        }
    }
}
