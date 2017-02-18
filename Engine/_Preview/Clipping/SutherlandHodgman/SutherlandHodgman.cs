// <copyright file="SutherlandHodgman.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of geometry.
    /// </summary>
    public static class SutherlandHodgman
    {
        /// <summary>
        /// Sutherland Hodgman Intersection. This clips the subject polygon against the clip polygon (gets the intersection of the two polygons)
        /// </summary>
        /// <param name="subjectPoly">Can be concave or convex</param>
        /// <param name="clipPoly">Must be convex</param>
        /// <returns>The intersection of the two polygons (or null)</returns>
        /// <remarks>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Based on the pseudocode from:
        /// http://en.wikipedia.org/wiki/Sutherland%E2%80%93Hodgman
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> PolygonPolygon(List<Point2D> subjectPoly, List<Point2D> clipPoly)
        {
            if (subjectPoly.Count < 3 || clipPoly.Count < 3)
                throw new ArgumentException($"The polygons passed in must have at least 3 points: subject={subjectPoly.Count}, clip={clipPoly.Count}");

            // Clone it
            List<Point2D> outputList = subjectPoly.ToList();

            // Make sure it's clockwise
            if (!PolygonExtensions.IsClockwise(subjectPoly))
                outputList.Reverse();

            // Walk around the clip polygon clockwise
            foreach (LineSegment clipEdge in PolygonExtensions.IterateEdgesClockwise(clipPoly))
            {
                // clone it
                List<Point2D> inputList = outputList.ToList();
                outputList.Clear();

                // Sometimes when the polygons don't intersect, this list goes to zero.
                // Jump out to avoid an index out of range exception
                if (inputList.Count == 0)
                    break;

                Point2D S = inputList[inputList.Count - 1];

                foreach (Point2D e in inputList)
                {
                    if (PolygonExtensions.IsInside(clipEdge, e))
                    {
                        if (!PolygonExtensions.IsInside(clipEdge, S))
                        {
                            var point = Intersections.LineSegmentLineSegmentIntersection(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                            if (point.Count < 1)
                            {
                                // May be collinear, or may be a bug
                                throw new ApplicationException($"Line segments don't intersect.");
                            }
                            else if (point.Count == 1)
                            {
                                outputList.Add(point[0]);
                            }
                            else
                            {
                                // May be collinear, or may be a bug
                                throw new ApplicationException($"Line segments are collinear.");
                            }
                        }

                        outputList.Add(e);
                    }
                    else if (PolygonExtensions.IsInside(clipEdge, S))
                    {
                        var point = Intersections.LineSegmentLineSegmentIntersection(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                        if (point.Count < 1)
                        {
                            // may be collinear, or may be a bug
                            throw new ApplicationException("Line segments don't intersect");
                        }
                        else if (point.Count == 1)
                        {
                            outputList.Add(point[0]);
                        }
                        else
                        {
                            // May be collinear, or may be a bug
                            throw new ApplicationException("Line segments are collinear.");
                        }
                    }

                    S = e;
                }
            }

            // Exit Function
            return outputList;
        }
    }
}

