// <copyright file="QuadTreeNode.cs" >
//     Copyright © 2008 - 2017 Michael Coyle BlueToque. All rights reserved.
// </copyright>
// <author id="Michael Coyle">Michael Coyle</author>
// <license>
//     This software and code are free, and are committed to the public domain. Users can download, 
//     adapt, remix, port, compile and sell this software as much as they like.
//     This code is released WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
//     or FITNESS FOR A PARTICULAR PURPOSE.My name, or the name of BlueToque Software cannot be used to 
//     market or promote this software in any way without prior authorization in writing.
// </license>
// <summary>
// A Quadtree is a structure designed to partition space so
// that it's faster to find out what is inside or outside a given 
// area. See http://en.wikipedia.org/wiki/Quadtree
// This QuadTree contains items that have an area (Rectangle2D)
// it will store a reference to the item in the quad 
// that is just big enough to hold it. Each quad has a bucket that 
// contain multiple items.
// </summary>
// <references>
// H. Samet, The Design and Analysis of Spatial Data Structures, Addison-Wesley, Reading, MA, 1990. ISBN 0-201-50255-0
// H.Samet, Applications of Spatial Data Structures: Computer Graphics, Image Processing, and GIS, Addison-Wesley, Reading, MA, 1990. ISBN 0-201-50300-0
// Mark de Berg, Marc van Kreveld, Mark Overmars, Otfried Schwarzkopf, Computational Geometry: Algorithms and Applications, 2nd Edition, Springer-Verlag 2000 ISBN: 3-540-65620-0
// </references>
// <remarks></remarks>

using System.Collections.Generic;
using System.Diagnostics;
using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// The QuadTreeNode
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoundsQuadTreeNode<T>
        where T : IBoundable
    {
        #region Fields
        /// <summary>
        /// The area of this node
        /// </summary>
        private readonly Rectangle2D bounds;

        /// <summary>
        /// The contents of this node.
        /// Note that the contents have no limit: this is not the standard way to implement a QuadTree
        /// </summary>
        private readonly List<T> contents = new List<T>();

        /// <summary>
        /// The child nodes of the QuadTree
        /// </summary>
        private readonly List<BoundsQuadTreeNode<T>> nodes = new List<BoundsQuadTreeNode<T>>(4);
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundsQuadTreeNode{T}"/> class with the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public BoundsQuadTreeNode(Rectangle2D bounds)
        {
            this.bounds = bounds;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Is the node empty
        /// </summary>
        public bool IsEmpty
            => bounds.IsEmpty || nodes.Count == 0;

        /// <summary>
        /// Area of the quadtree node
        /// </summary>
        public Rectangle2D Bounds
            => bounds;

        /// <summary>
        /// Total number of nodes in the this node and all SubNodes
        /// </summary>
        public int Count
        {
            get
            {
                var count = 0;

                foreach (var node in nodes)
                {
                    count += node.Count;
                }

                count += Contents.Count;

                return count;
            }
        }

        /// <summary>
        /// Return the contents of this node and all sub-nodes in the true below this one.
        /// </summary>
        public List<T> SubTreeContents
        {
            get
            {
                var results = new List<T>();

                foreach (var node in nodes)
                {
                    results.AddRange(node.SubTreeContents);
                }

                results.AddRange(Contents);
                return results;
            }
        }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        public List<T> Contents
            => contents;
        #endregion Properties

        #region Methods
        /// <summary>
        /// Query the QuadTree for items that are in the given area
        /// </summary>
        /// <param name="queryArea"></param>
        /// <returns></returns>
        public List<T> Query(Rectangle2D queryArea)
        {
            if (queryArea is null) return null;

            // Create a list of the items that are found
            var results = new List<T>();

            // This quad contains items that are not entirely contained by
            // it's four sub-quads. Iterate through the items in this quad 
            // to see if they intersect.
            foreach (var item in Contents)
            {
                if (queryArea.IntersectsWith(item.Bounds))
                {
                    results.Add(item);
                }
            }

            foreach (var node in nodes)
            {
                if (node.IsEmpty)
                {
                    continue;
                }

                // Case 1: search area completely contained by sub-quad
                // if a node completely contains the query area, go down that branch
                // and skip the remaining nodes (break this loop)
                if (node.Bounds.Contains(queryArea))
                {
                    results.AddRange(node.Query(queryArea));
                    break;
                }

                // Case 2: Sub-quad completely contained by search area 
                // if the query area completely contains a sub-quad,
                // just add all the contents of that quad and it's children 
                // to the result set. You need to continue the loop to test 
                // the other quads
                if (queryArea.Contains(node.Bounds))
                {
                    results.AddRange(node.SubTreeContents);
                    continue;
                }

                // Case 3: search area intersects with sub-quad
                // traverse into this quad, continue the loop to search other
                // quads
                if (node.Bounds.IntersectsWith(queryArea))
                {
                    results.AddRange(node.Query(queryArea));
                }
            }

            return results;
        }

        /// <summary>
        /// Insert an item to this node
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            // if the item is not contained in this quad, there's a problem
            if (!bounds.Contains(item.Bounds))
            {
                Trace.TraceWarning("feature is out of the bounds of this quadtree node");
                return;
            }

            // if the sub-nodes are null create them. may not be successful: see below
            // we may be at the smallest allowed size in which case the sub-nodes will not be created
            if (nodes.Count == 0)
            {
                CreateSubNodes();
            }

            // for each sub-node:
            // if the node contains the item, add the item to that node and return
            // this recurses into the node that is just large enough to fit this item
            foreach (var node in nodes)
            {
                if (node.Bounds.Contains(item.Bounds))
                {
                    node.Insert(item);
                    return;
                }
            }

            // if we make it to here, either
            // 1) none of the sub-nodes completely contained the item. or
            // 2) we're at the smallest sub-node size allowed 
            // add the item to this node's contents.
            Contents.Add(item);
        }

        /// <summary>
        /// The for each.
        /// </summary>
        /// <param name="action">The action.</param>
        public void ForEach(BoundsQuadTree<T>.QTAction action)
        {
            if (action is null) return;
            action(this);

            // draw the child quads
            foreach (var node in nodes)
            {
                node.ForEach(action);
            }
        }

        /// <summary>
        /// Internal method to create the sub-nodes (partitions space)
        /// </summary>
        private void CreateSubNodes()
        {
            // the smallest sub-node has an area 
            if ((bounds.Height * bounds.Width) <= 10)
            {
                return;
            }

            var halfWidth = bounds.Width * OneHalf;
            var halfHeight = bounds.Height * OneHalf;

            nodes.Add(new BoundsQuadTreeNode<T>(new Rectangle2D(bounds.Location, new Size2D(halfWidth, halfHeight))));
            nodes.Add(new BoundsQuadTreeNode<T>(new Rectangle2D(new Point2D(bounds.Left, bounds.Top + halfHeight), new Size2D(halfWidth, halfHeight))));
            nodes.Add(new BoundsQuadTreeNode<T>(new Rectangle2D(new Point2D(bounds.Left + halfWidth, bounds.Top), new Size2D(halfWidth, halfHeight))));
            nodes.Add(new BoundsQuadTreeNode<T>(new Rectangle2D(new Point2D(bounds.Left + halfWidth, bounds.Top + halfHeight), new Size2D(halfWidth, halfHeight))));
        }

        /// <summary>
        /// The echo sub tree contents.
        /// Return the contents of this node and all sub-nodes in the tree below this one.
        /// </summary>
        /// <param name="funFind">The funFind.</param>
        public void EchoSubTreeContents(BoundsQuadTree<T>.Find funFind)
        {
            foreach (var node in nodes)
            {
                node.EchoSubTreeContents(funFind);
            }

            for (var i = 0; i < Contents.Count; i++)
            {
                if (funFind(Contents[i]))
                {
                    Contents.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="queryArea">The queryArea.</param>
        /// <param name="funFind">The funFind.</param>
        public void Delete(Rectangle2D queryArea, BoundsQuadTree<T>.Find funFind)
        {
            if (Contents is not null)
            {
                foreach (var item in Contents)
                {
                    if (queryArea.IntersectsWith(item.Bounds) && funFind(item))
                    {
                        Contents.Remove(item);
                    }
                }
            }
            foreach (var node in nodes)
            {
                if (node.IsEmpty)
                {
                    continue;
                }

                if (node.Bounds.Contains(queryArea))
                {
                    node.Query(queryArea);
                    break;
                }
                if (queryArea.Contains(node.Bounds))
                {
                    node.EchoSubTreeContents(funFind);

                    continue;
                }
                if (node.Bounds.IntersectsWith(queryArea))
                {
                    node.Query(queryArea);
                }
            }
        }
        #endregion Methods
    }
}
