﻿// <copyright file="SutherlandHodgman.cs" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;

namespace Engine;

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
    /// <para>http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
    /// Based on the pseudo code from:
    /// http://en.wikipedia.org/wiki/Sutherland%E2%80%93Hodgman</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static List<Point2D> PolygonPolygon(List<Point2D> subjectPoly, List<Point2D> clipPoly)
    {
        if (subjectPoly?.Count < 3 || clipPoly?.Count < 3)
        {
            throw new ArgumentException($"The polygons passed in must have at least 3 points: subject={subjectPoly?.Count}, clip={clipPoly?.Count}");
        }

        // Clone it
        var outputList = subjectPoly.ToList();

        // Make sure it's clockwise
        if (!PolygonExtensions.IsClockwise(subjectPoly))
        {
            outputList.Reverse();
        }

        // Walk around the clip polygon clockwise
        foreach (var clipEdge in PolygonExtensions.IterateEdgesClockwise(clipPoly))
        {
            // clone it
            var inputList = outputList.ToList();
            outputList.Clear();

            // Sometimes when the polygons don't intersect, this list goes to zero.
            // Jump out to avoid an index out of range exception
            if (inputList.Count == 0)
            {
                break;
            }

            var S = inputList[^1];

            foreach (var e in inputList)
            {
                if (PolygonExtensions.IsInside(clipEdge, e))
                {
                    if (!PolygonExtensions.IsInside(clipEdge, S))
                    {
                        var point = Intersections.Intersection(new LineSegment2D(S.X, S.Y, e.X, e.Y), new LineSegment2D(clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y));
                        if (point.Count < 1)
                        {
                            // May be collinear, or may be a bug
                            throw new Exception($"Line segments don't intersect.");
                        }
                        else if (point.Count == 1)
                        {
                            outputList.Add(point[0]);
                        }
                        else
                        {
                            // May be collinear, or may be a bug
                            throw new Exception($"Line segments are collinear.");
                        }
                    }

                    outputList.Add(e);
                }
                else if (PolygonExtensions.IsInside(clipEdge, S))
                {
                    var point = Intersections.Intersection(new LineSegment2D(S.X, S.Y, e.X, e.Y), new LineSegment2D(clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y));
                    if (point.Count < 1)
                    {
                        // may be collinear, or may be a bug
                        throw new Exception("Line segments don't intersect");
                    }
                    else if (point.Count == 1)
                    {
                        outputList.Add(point[0]);
                    }
                    else
                    {
                        // May be collinear, or may be a bug
                        throw new Exception("Line segments are collinear.");
                    }
                }

                S = e;
            }
        }

        // Exit Function
        return outputList;
    }
}

