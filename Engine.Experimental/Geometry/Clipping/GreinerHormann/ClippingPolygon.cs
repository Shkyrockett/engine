// <copyright file="ClippingPolygon.cs" >
//     Copyright © 2015 - 2017 w8r. All rights reserved.
// </copyright>
// <author id="w8r">Alexander Milevski</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>Ported from https://github.com/w8r/GreinerHormann</summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ClippingPolygon
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ClippingPolygon(List<Point2D> p)
        {
            for (int i = 0, len = p.Count; i < len; i++)
            {
                AddVertex(new ClippingVertex(p[i]));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ClippingVertex First { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public int Vertices { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        private ClippingVertex LastUnprocessed { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private ClippingVertex FirstIntersect { get; set; } = null;

        #endregion

        /// <summary>
        /// Add a vertex object to the polygon (vertex is added at the 'end' of the list')
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(ClippingVertex vertex)
        {
            if (First == null)
            {
                First = vertex;
                First.Next = vertex;
                First.Previous = vertex;
            }
            else
            {
                var next = First;
                var previous = next.Previous;

                next.Previous = vertex;
                vertex.Next = next;
                vertex.Previous = previous;
                previous.Next = vertex;
            }
            Vertices++;
        }

        /// <summary>
        /// Inserts a vertex in between start and end
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void InsertVertex(ClippingVertex vertex, ClippingVertex start, ClippingVertex end)
        {
            ClippingVertex prev;
            var curr = start;

            while (!curr.Equals(end) && curr.distance < vertex.distance)
            {
                curr = curr.Next;
            }

            vertex.Next = curr;
            prev = curr.Previous;

            vertex.Previous = prev;
            prev.Next = vertex;
            curr.Previous = vertex;

            Vertices++;
        }

        /// <summary>
        /// Get next non-intersection point
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public ClippingVertex GetNext(ClippingVertex v)
        {
            var c = v;
            while (c.isIntersection)
            {
                c = c.Next;
            }

            return c;
        }

        /// <summary>
        /// Unvisited intersection
        /// </summary>
        /// <returns></returns>
        public ClippingVertex GetFirstIntersect()
        {
            var v = FirstIntersect ?? First;

            do
            {
                if (v.isIntersection && !v.visited)
                {
                    break;
                }

                v = v.Next;
            } while (!v.Equals(First));

            FirstIntersect = v;
            return v;
        }

        /// <summary>
        /// Does the polygon have unvisited vertices
        /// </summary>
        /// <returns></returns>
        public bool HasUnprocessed()
        {
            var v = LastUnprocessed ?? First;
            do
            {
                if (v.isIntersection && !v.visited)
                {
                    LastUnprocessed = v;
                    return true;
                }

                v = v.Next;
            } while (!v.Equals(First));

            LastUnprocessed = null;
            return false;
        }

        /// <summary>
        /// The output depends on what you put in, arrays or objects
        /// </summary>
        /// <returns></returns>
        public List<Point2D> GetPoints()
        {
            var points = new List<Point2D>();
            var v = First;

            do
            {
                points.Add((v.X, v.Y));
                v = v.Next;
            } while (v != First);

            return points;
        }

        /// <summary>
        /// Clip polygon against another one.
        /// Result depends on algorithm direction:
        /// Intersection: forwards forwards
        /// Union:        backwards backwards
        /// Diff:         backwards forwards
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="sourceForwards"></param>
        /// <param name="clipForwards"></param>
        /// <returns></returns>
        public List<List<Point2D>> Clip(ClippingPolygon clip, bool sourceForwards, bool clipForwards)
        {
            var sourceVertex = First;
            var clipVertex = clip.First;
            bool sourceInClip;
            bool clipInSource;

            // Calculate and mark intersections
            do
            {
                if (!sourceVertex.isIntersection)
                {
                    do
                    {
                        if (!clipVertex.isIntersection)
                        {
                            var i = new ClippingIntersection(sourceVertex, GetNext(sourceVertex.Next), clipVertex, clip.GetNext(clipVertex.Next));

                            if (i.Valid())
                            {
                                var sourceIntersection = clipVertex.CreateIntersection(i.X, i.Y, i.ToSource);
                                var clipIntersection = clipVertex.CreateIntersection(i.X, i.Y, i.ToClip);

                                sourceIntersection.corresponding = clipIntersection;
                                clipIntersection.corresponding = sourceIntersection;

                                InsertVertex(sourceIntersection, sourceVertex, GetNext(sourceVertex.Next));
                                clip.InsertVertex(clipIntersection, clipVertex, clip.GetNext(clipVertex.Next));
                            }
                        }
                        clipVertex = clipVertex.Next;
                    } while (!clipVertex.Equals(clip.First));
                }

                sourceVertex = sourceVertex.Next;
            } while (!sourceVertex.Equals(First));

            // phase two - identify entry/exit points
            sourceVertex = First;
            clipVertex = clip.First;

            sourceInClip = sourceVertex.IsInside(clip);
            clipInSource = clipVertex.IsInside(this);

            sourceForwards ^= sourceInClip;
            clipForwards ^= clipInSource;

            do
            {
                if (sourceVertex.isIntersection)
                {
                    sourceVertex.isEntry = sourceForwards;
                    sourceForwards = !sourceForwards;
                }
                sourceVertex = sourceVertex.Next;
            } while (!sourceVertex.Equals(First));

            do
            {
                if (clipVertex.isIntersection)
                {
                    clipVertex.isEntry = clipForwards;
                    clipForwards = !clipForwards;
                }
                clipVertex = clipVertex.Next;
            } while (!clipVertex.Equals(clip.First));

            // phase three - construct a list of clipped polygons
            var list = new List<List<Point2D>>();

            while (HasUnprocessed())
            {
                var current = GetFirstIntersect();
                // keep format
                var clipped = new ClippingPolygon(new List<Point2D>());

                clipped.AddVertex(new ClippingVertex(current.X, current.Y));
                do
                {
                    current.Visit();
                    if (current.isEntry)
                    {
                        do
                        {
                            current = current.Next;
                            clipped.AddVertex(new ClippingVertex(current.X, current.Y));
                        } while (!current.isIntersection);

                    }
                    else
                    {
                        do
                        {
                            current = current.Previous;
                            clipped.AddVertex(new ClippingVertex(current.X, current.Y));
                        } while (!current.isIntersection);
                    }
                    current = current.corresponding;
                } while (!current.visited);

                list.Add(clipped.GetPoints());
            }

            if (list.Count == 0)
            {
                if (sourceInClip)
                {
                    list.Add(GetPoints());
                }
                if (clipInSource)
                {
                    list.Add(clip.GetPoints());
                }
                if (list.Count == 0)
                {
                    list = null;
                }
            }

            return list;
        }
    }
}
