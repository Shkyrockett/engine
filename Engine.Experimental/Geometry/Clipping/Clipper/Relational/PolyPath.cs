/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

namespace Engine.Experimental;

/// <summary>
/// PolyTree and PolyNode classes
/// </summary>
public class PolyPath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PolyPath"/> class.
    /// </summary>
    public PolyPath()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PolyPath"/> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="children">The children.</param>
    /// <param name="path">The path.</param>
    public PolyPath(PolyPath parent, List<PolyPath> children, PolygonContour2D path)
    {
        Parent = parent;
        Children = children;
        Path = path;
    }

    #region Properties
    /// <summary>
    /// Gets the parent.
    /// </summary>
    /// <value>
    /// The parent.
    /// </value>
    public PolyPath Parent { get; private set; }

    /// <summary>
    /// Gets the children.
    /// </summary>
    /// <value>
    /// The children.
    /// </value>
    public List<PolyPath> Children { get; private set; }

    /// <summary>
    /// Gets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public PolygonContour2D Path { get; private set; }

    /// <summary>
    /// Gets the child count.
    /// </summary>
    /// <value>
    /// The child count.
    /// </value>
    public int ChildCount => Children.Count;
    #endregion Properties

    /// <summary>
    /// Add the child.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>
    /// The <see cref="PolyPath" />.
    /// </returns>
    public PolyPath AddChild(PolygonContour2D p)
    {
        var child = new PolyPath()
        {
            Parent = this,
            Path = p
        };

        Children.Add(child);
        return child;
    }

    /// <summary>
    /// Clear.
    /// </summary>
    public void Clear() => Children.Clear();

    /// <summary>
    /// The is hole node.
    /// </summary>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    internal bool IsHoleNode()
    {
        var result = true;
        var node = Parent;
        while (node is not null)
        {
            result = !result;
            node = node.Parent;
        }

        return result;
    }

    //the following two methods are really only for debugging ...

    /// <summary>
    /// Add the PolyNode to paths.
    /// </summary>
    /// <param name="pp">The pp.</param>
    /// <param name="paths">The paths.</param>
    private static void AddPolyNodeToPaths(PolyPath pp, Polygon2D paths)
    {
        var cnt = pp.Path.Count;
        if (cnt > 0)
        {
            var p = new PolygonContour2D
            {
                Capacity = cnt
            };
            foreach (var ip in pp.Path)
            {
                p.Add(ip);
            }

            paths.Add(p);
        }
        foreach (var polyp in pp.Children)
        {
            AddPolyNodeToPaths(polyp, paths);
        }
    }

    /// <summary>
    /// The PolyTree to paths.
    /// </summary>
    /// <returns>
    /// The <see cref="Polygon2D" />.
    /// </returns>
    public Polygon2D PolyTreeToPaths()
    {
        var paths = new Polygon2D();
        AddPolyNodeToPaths(this, paths);
        return paths;
    }
}
