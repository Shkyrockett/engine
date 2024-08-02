// <copyright file="PointQuadTree.cs" >
// Copyright © 2008 - 2017 Michael Coyle BlueToque. All rights reserved.
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

namespace Engine.Experimental;

/// <summary>
/// The point quad tree class.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PointQuadTree<T>
    where T : ILocatable
{
    #region Fields
    /// <summary>
    /// The root QuadTreeNode
    /// </summary>
    private readonly PointQuadTreeNode<T> root;

    /// <summary>
    /// The bounds of this QuadTree
    /// </summary>
    private readonly Rectangle2D rectangle;
    #endregion Fields

    #region Delegates
    /// <summary>
    /// An delegate that performs an action on a QuadTreeNode
    /// </summary>
    /// <param name="obj"></param>
    public delegate void QTAction(PointQuadTreeNode<T> obj);

    /// <summary>
    /// Find.
    /// </summary>
    /// <param name="item">The item to find.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public delegate bool Find(T item);
    #endregion Delegates

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="PointQuadTree{T}"/> class.
    /// </summary>
    /// <param name="rectangle">The rectangle.</param>
    public PointQuadTree(Rectangle2D rectangle)
    {
        this.rectangle = rectangle;
        root = new PointQuadTreeNode<T>(this.rectangle);
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Get the count of items in the QuadTree
    /// </summary>
    public int Count
        => root.Count;
    #endregion Properties

    #region Methods
    /// <summary>
    /// Insert the feature into the QuadTree
    /// </summary>
    /// <param name="item"></param>
    public void Insert(T item)
        => root.Insert(item);

    /// <summary>
    /// Query the QuadTree, returning the items that are in the given area
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    public List<T> Query(Rectangle2D area)
        => root.Query(area);

    /// <summary>
    /// Do the specified action for each item in the quadtree
    /// </summary>
    /// <param name="action"></param>
    public void ForEach(QTAction action)
        => root.ForEach(action);

    /// <summary>
    /// Return the contents of this node and all sub-nodes in the tree below this one.
    /// </summary>
    /// <param name="funFind">The funFind.</param>
    public void EchoSubTreeContents(Find funFind)
        => root.EchoSubTreeContents(funFind);

    /// <summary>
    /// Delete.
    /// </summary>
    /// <param name="queryArea">The queryArea.</param>
    /// <param name="funFind">The funFind.</param>
    public void Delete(Rectangle2D queryArea, Find funFind)
        => root.Delete(queryArea, funFind);
    #endregion Methods
}
