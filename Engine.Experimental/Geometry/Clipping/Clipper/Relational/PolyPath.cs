/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Collections.Generic;

namespace Engine.Experimental
{
    //using PolygonContour = List<Point2D>;
    //using Polygon = List<List<Point2D>>;

    /// <summary>
    /// PolyTree and PolyNode classes
    /// </summary>
    public class PolyPath
    {
        #region Fields

        /// <summary>
        /// The parent.
        /// </summary>
        private PolyPath parent;

        /// <summary>
        /// The children.
        /// </summary>
        private List<PolyPath> children = new List<PolyPath>();

        /// <summary>
        /// The path.
        /// </summary>
        private PolygonContour path = new PolygonContour();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the parent.
        /// </summary>
        public PolyPath Parent
            => parent;

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<PolyPath> Children
            => children;

        /// <summary>
        /// Gets the child count.
        /// </summary>
        public int ChildCount
            => children.Count;

        /// <summary>
        /// Gets the path.
        /// </summary>
        public PolygonContour Path
            => path;

        #endregion

        /// <summary>
        /// Add the child.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="PolyPath"/>.</returns>
        public PolyPath AddChild(PolygonContour p)
        {
            var child = new PolyPath
            {
                parent = this,
                path = p
            };
            Children.Add(child);
            return child;
        }

        /// <summary>
        /// Clear.
        /// </summary>
        public void Clear()
            => Children.Clear();

        /// <summary>
        /// The is hole node.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool IsHoleNode()
        {
            var result = true;
            PolyPath node = parent;
            while (node != null)
            {
                result = !result;
                node = node.parent;
            }
            return result;
        }

        //the following two methods are really only for debugging ...

        /// <summary>
        /// Add the PolyNode to paths.
        /// </summary>
        /// <param name="pp">The pp.</param>
        /// <param name="paths">The paths.</param>
        private static void AddPolyNodeToPaths(PolyPath pp, Polygon paths)
        {
            var cnt = pp.path.Count;
            if (cnt > 0)
            {
                var p = new PolygonContour
                {
                    Capacity = cnt
                };
                foreach (var ip in pp.path)
                {
                    p.Add(ip);
                }

                paths.Add(p);
            }
            foreach (var polyp in pp.children)
            {
                AddPolyNodeToPaths(polyp, paths);
            }
        }

        /// <summary>
        /// The PolyTree to paths.
        /// </summary>
        /// <returns>The <see cref="Polygon"/>.</returns>
        public Polygon PolyTreeToPaths()
        {
            var paths = new Polygon();
            AddPolyNodeToPaths(this, paths);
            return paths;
        }
    }
}
