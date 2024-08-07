﻿// // // // // // // // // // // // //
// QuadTree and supporting code
// by Kyle Schouviller
// http://www.kyleschouviller.com
//
// December 2006: Original version
// May 06, 2007:  Updated for XNA Framework 1.0
//                and public release.
//
// You may use and modify this code however you
// wish, under the following condition:
// *) I must be credited
// A little line in the credits is all I ask -
// to show your appreciation.
// 
// If you have any questions, please use the
// contact form on my website.
//
// Now get back to making great games!
// // // // // // // // // // // // //

namespace Engine.Experimental;

/// <summary>
/// A node in a QuadTree
/// </summary>
/// <typeparam name="T">The type of the QuadTree's items' parents</typeparam>
public class QuadTreeNode<T>
    where T : IBoundable
{
    #region Fields
    /// <summary>
    /// The rect.
    /// </summary>
    protected Rectangle2D bounds;

    /// <summary>
    /// The maximum number of items in this node before partitioning
    /// </summary>
    protected int MaxItems;

    /// <summary>
    /// Whether or not this node has been partitioned
    /// </summary>
    protected bool IsPartitioned;

    /// <summary>
    /// The parent node
    /// </summary>
    protected QuadTreeNode<T> ParentNode;

    /// <summary>
    /// The top left node
    /// </summary>
    protected QuadTreeNode<T> TopLeftNode;

    /// <summary>
    /// The top right node
    /// </summary>
    protected QuadTreeNode<T> TopRightNode;

    /// <summary>
    /// The bottom left node
    /// </summary>
    protected QuadTreeNode<T> BottomLeftNode;

    /// <summary>
    /// The bottom right node
    /// </summary>
    protected QuadTreeNode<T> BottomRightNode;

    /// <summary>
    /// The items in this node
    /// </summary>
    protected List<QuadTreePositionItem<T>> Items;
    #endregion Fields

    #region Delegates
    /// <summary>
    /// World resize delegate
    /// </summary>
    /// <param name="newSize">The new world size</param>
    public delegate void ResizeDelegate(Rectangle2D newSize);

    /// <summary>
    /// Resize the world
    /// </summary>
    protected ResizeDelegate WorldResize;
    #endregion Delegates

    #region Properties
    /// <summary>
    /// The rectangle of this node
    /// </summary>
    /// <summary>
    /// Gets the rectangle of this node
    /// </summary>
    public Rectangle2D Bounds
    {
        get { return bounds; }
        protected set { bounds = value; }
    }
    #endregion Properties

    #region Constructors
    /// <summary>
    /// QuadTreeNode constructor
    /// </summary>
    /// <param name="parentNode">The parent node of this QuadTreeNode</param>
    /// <param name="rect">The rectangle of the QuadTreeNode</param>
    /// <param name="maxItems">Maximum number of items in the QuadTreeNode before partitioning</param>
    public QuadTreeNode(QuadTreeNode<T> parentNode, Rectangle2D rect, int maxItems)
    {
        ParentNode = parentNode;
        Bounds = rect;
        MaxItems = maxItems;
        IsPartitioned = false;
        Items = [];
    }

    /// <summary>
    /// QuadTreeNode constructor
    /// </summary>
    /// <param name="rect">The rectangle of the QuadTreeNode</param>
    /// <param name="maxItems">Maximum number of items in the QuadTreeNode before partitioning</param>
    /// <param name="worldResize">The function to return the size</param>
    public QuadTreeNode(Rectangle2D rect, int maxItems, ResizeDelegate worldResize)
    {
        ParentNode = null;
        Bounds = rect;
        MaxItems = maxItems;
        WorldResize = worldResize;
        IsPartitioned = false;
        Items = [];
    }
    #endregion Constructors

    #region Methods
    /// <summary>
    /// Insert an item in this node
    /// </summary>
    /// <param name="item">The item to insert</param>
    public void Insert(QuadTreePositionItem<T> item)
    {
        if (item is null) return;
        // If partitioned, try to find child node to add to
        if (!InsertInChild(item))
        {
            item.Destroy += new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
            item.Move += new QuadTreePositionItem<T>.MoveHandler(ItemMove);
            Items.Add(item);

            // Check if this node needs to be partitioned
            if (!IsPartitioned && Items.Count >= MaxItems)
            {
                Partition();
            }
        }
    }

    /// <summary>
    /// Inserts an item into one of this node's children
    /// </summary>
    /// <param name="item">The item to insert in a child</param>
    /// <returns>Whether or not the insert succeeded</returns>
    protected bool InsertInChild(QuadTreePositionItem<T> item)
    {
        if (!IsPartitioned)
        {
            return false;
        }

        if (TopLeftNode.ContainsRect(item?.Bounds))
        {
            TopLeftNode.Insert(item);
        }
        else if (TopRightNode.ContainsRect(item.Bounds))
        {
            TopRightNode.Insert(item);
        }
        else if (BottomLeftNode.ContainsRect(item.Bounds))
        {
            BottomLeftNode.Insert(item);
        }
        else if (BottomRightNode.ContainsRect(item.Bounds))
        {
            BottomRightNode.Insert(item);
        }
        else
        {
            return false; // insert in child failed
        }

        return true;
    }

    /// <summary>
    /// Pushes an item down to one of this node's children
    /// </summary>
    /// <param name="i">The index of the item to push down</param>
    /// <returns>Whether or not the push was successful</returns>
    public bool PushItemDown(int i)
    {
        if (InsertInChild(Items[i]))
        {
            RemoveItem(i);
            return true;
        }

        else
        {
            return false;
        }
    }

    /// <summary>
    /// Push an item up to this node's parent
    /// </summary>
    /// <param name="i">The index of the item to push up</param>
    public void PushItemUp(int i)
    {
        var m = Items[i];

        RemoveItem(i);
        ParentNode.Insert(m);
    }

    /// <summary>
    /// Repartitions this node
    /// </summary>
    protected void Partition()
    {
        // Create the nodes
        var MidPoint = (Point2D)((Bounds.TopLeft + Bounds.BottomRight) / 2.0f);

        TopLeftNode = new QuadTreeNode<T>(this, new Rectangle2D(Bounds.TopLeft, MidPoint), MaxItems);
        TopRightNode = new QuadTreeNode<T>(this, new Rectangle2D(new Point2D(MidPoint.X, Bounds.Top), new Point2D(Bounds.Right, MidPoint.Y)), MaxItems);
        BottomLeftNode = new QuadTreeNode<T>(this, new Rectangle2D(new Point2D(Bounds.Left, MidPoint.Y), new Point2D(MidPoint.X, Bounds.Bottom)), MaxItems);
        BottomRightNode = new QuadTreeNode<T>(this, new Rectangle2D(MidPoint, Bounds.BottomRight), MaxItems);

        IsPartitioned = true;

        // Try to push items down to child nodes
        var i = 0;
        while (i < Items.Count)
        {
            if (!PushItemDown(i))
            {
                i++;
            }
        }
    }
    #endregion Methods

    #region Query Methods
    /// <summary>
    /// Gets a list of items containing a specified point
    /// </summary>
    /// <param name="point">The point</param>
    /// <param name="itemsFound">The list to add found items to (list will not be cleared first)</param>
    /// <remarks><para>ItemsFound is assumed to be initialized, and will not be cleared</para></remarks>
    public void GetItems(Point2D point, ref List<QuadTreePositionItem<T>> itemsFound)
    {
        if (itemsFound is null) return;
        // test the point against this node
        if (Bounds.Contains(point))
        {
            // test the point in each item
            foreach (var Item in Items)
            {
                if (Item.Bounds.Contains(point))
                {
                    itemsFound.Add(Item);
                }
            }

            // query all subtrees
            if (IsPartitioned)
            {
                TopLeftNode.GetItems(point, ref itemsFound);
                TopRightNode.GetItems(point, ref itemsFound);
                BottomLeftNode.GetItems(point, ref itemsFound);
                BottomRightNode.GetItems(point, ref itemsFound);
            }
        }
    }

    /// <summary>
    /// Gets a list of items intersecting a specified rectangle
    /// </summary>
    /// <param name="rect">The rectangle</param>
    /// <param name="itemsFound">The list to add found items to (list will not be cleared first)</param>
    /// <remarks><para>ItemsFound is assumed to be initialized, and will not be cleared</para></remarks>
    public void GetItems(Rectangle2D rect, ref List<QuadTreePositionItem<T>> itemsFound)
    {
        if (rect is null || itemsFound is null) return;
        // test the point against this node
        if (rect.Intersects(rect))
        {
            // test the point in each item
            foreach (var Item in Items)
            {
                if (Item.Bounds.Intersects(rect))
                {
                    itemsFound.Add(Item);
                }
            }

            // query all subtrees
            if (IsPartitioned)
            {
                TopLeftNode.GetItems(rect, ref itemsFound);
                TopRightNode.GetItems(rect, ref itemsFound);
                BottomLeftNode.GetItems(rect, ref itemsFound);
                BottomRightNode.GetItems(rect, ref itemsFound);
            }
        }
    }

    /// <summary>
    /// Gets a list of all items within this node
    /// </summary>
    /// <param name="itemsFound">The list to add found items to (list will not be cleared first)</param>
    /// <remarks><para>ItemsFound is assumed to be initialized, and will not be cleared</para></remarks>
    public void GetAllItems(ref List<QuadTreePositionItem<T>> itemsFound)
    {
        itemsFound?.AddRange(Items);

        // query all subtrees
        if (IsPartitioned)
        {
            TopLeftNode.GetAllItems(ref itemsFound);
            TopRightNode.GetAllItems(ref itemsFound);
            BottomLeftNode.GetAllItems(ref itemsFound);
            BottomRightNode.GetAllItems(ref itemsFound);
        }
    }

    /// <summary>
    /// Finds the node containing a specified item
    /// </summary>
    /// <param name="item">The item to find</param>
    /// <returns>The node containing the item</returns>
    public QuadTreeNode<T> FindItemNode(QuadTreePositionItem<T> item)
    {
        if (item is null) return null;
        if (Items.Contains(item))
        {
            return this;
        }
        else if (IsPartitioned)
        {
            QuadTreeNode<T> n = null;

            // Check the nodes that could contain the item
            if (TopLeftNode.ContainsRect(item.Bounds))
            {
                n = TopLeftNode.FindItemNode(item);
            }
            if (n is null &&
                TopRightNode.ContainsRect(item.Bounds))
            {
                n = TopRightNode.FindItemNode(item);
            }
            if (n is null &&
                BottomLeftNode.ContainsRect(item.Bounds))
            {
                n = BottomLeftNode.FindItemNode(item);
            }
            if (n is null &&
                BottomRightNode.ContainsRect(item.Bounds))
            {
                n = BottomRightNode.FindItemNode(item);
            }

            return n;
        }

        else
        {
            return null;
        }
    }
    #endregion Query Methods

    #region Destruction
    /// <summary>
    /// Destroys this node
    /// </summary>
    public void Destroy()
    {
        // Destroy all child nodes
        if (IsPartitioned)
        {
            TopLeftNode.Destroy();
            TopRightNode.Destroy();
            BottomLeftNode.Destroy();
            BottomRightNode.Destroy();

            TopLeftNode = null;
            TopRightNode = null;
            BottomLeftNode = null;
            BottomRightNode = null;
        }

        // Remove all items
        while (Items.Count > 0)
        {
            RemoveItem(0);
        }
    }

    /// <summary>
    /// Removes an item from this node
    /// </summary>
    /// <param name="item">The item to remove</param>
    public void RemoveItem(QuadTreePositionItem<T> item)
    {
        if (item is null) return;
        // Find and remove the item
        if (Items.Contains(item))
        {
            item.Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
            item.Destroy -= new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
            Items.Remove(item);
        }
    }

    /// <summary>
    /// Removes an item from this node at a specific index
    /// </summary>
    /// <param name="i">the index of the item to remove</param>
    protected void RemoveItem(int i)
    {
        if (i < Items.Count)
        {
            Items[i].Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
            Items[i].Destroy -= new QuadTreePositionItem<T>.DestroyHandler(ItemDestroy);
            Items.RemoveAt(i);
        }
    }
    #endregion Destruction

    #region Observer Methods
    /// <summary>
    /// Handles item movement
    /// </summary>
    /// <param name="item">The item that moved</param>
    public void ItemMove(QuadTreePositionItem<T> item)
    {
        if (item is null) return;
        // Find the item
        if (Items.Contains(item))
        {
            var i = Items.IndexOf(item);

            // Try to push the item down to the child
            if (!PushItemDown(i))
            {
                // otherwise, if not root, push up
                if (ParentNode is not null)
                {
                    PushItemUp(i);
                }
                else if (!ContainsRect(item.Bounds))
                {
                    WorldResize(new Rectangle2D(
                         GeometryOperations.Min(Bounds.TopLeft, item.Bounds.TopLeft) * 2,
                         GeometryOperations.Max(Bounds.BottomRight, item.Bounds.BottomRight) * 2));
                }

            }
        }
        else
        {
            // this node doesn't contain that item, stop notifying it about it
            item.Move -= new QuadTreePositionItem<T>.MoveHandler(ItemMove);
        }
    }

    /// <summary>
    /// Handles item destruction
    /// </summary>
    /// <param name="item">The item that is being destroyed</param>
    public void ItemDestroy(QuadTreePositionItem<T> item) => RemoveItem(item);
    #endregion Observer Methods

    #region Helper Methods
    /// <summary>
    /// Tests whether this node contains a rectangle
    /// </summary>
    /// <param name="rect">The rectangle to test</param>
    /// <returns>Whether or not this node contains the specified rectangle</returns>
    public bool ContainsRect(Rectangle2D rect)
    {
        if (rect is null) return false;
        return rect.TopLeft.X >= Bounds.TopLeft.X &&
            rect.TopLeft.Y >= Bounds.TopLeft.Y &&
            rect.BottomRight.X <= Bounds.BottomRight.X &&
            rect.BottomRight.Y <= Bounds.BottomRight.Y;
    }
    #endregion Helper Methods
}
