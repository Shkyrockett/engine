// // // // // // // // // // // // //
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

using System.Collections.Generic;

namespace Engine.Experimental
{
    /// <summary>
    /// A QuadTree for partitioning a space into rectangles
    /// </summary>
    /// <typeparam name="T">The type of the QuadTree's items' parents</typeparam>
    /// <remarks>This QuadTree automatically resizes as needed</remarks>
    public class QuadTree<T>
        where T : IBoundable
    {
        #region Fields
        /// <summary>
        /// The root head node of the QuadTree
        /// </summary>
        protected QuadTreeNode<T> root;

        /// <summary>
        /// The maximum number of items in any node before partitioning
        /// </summary>
        protected int maxItems;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the world rectangle
        /// </summary>
        public Rectangle2D Bounds
            => root.Rect;
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTree{T}"/> class.
        /// </summary>
        /// <param name="bounds">The world rectangle for this QuadTree (a rectangle containing all items at all times)</param>
        /// <param name="maxItems">Maximum number of items in any cell of the QuadTree before partitioning</param>
        public QuadTree(Rectangle2D bounds, int maxItems)
        {
            root = new QuadTreeNode<T>(bounds, maxItems, Resize);
            this.maxItems = maxItems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTree{T}"/> class.
        /// </summary>
        /// <param name="size">The size of the QuadTree (i.e. the bottom-right with a top-left of (0,0))</param>
        /// <param name="maxItems">Maximum number of items in any cell of the QuadTree before partitioning</param>
        /// <remarks>This constructor is for ease of use</remarks>
        public QuadTree(Size2D size, int maxItems)
            : this(new Rectangle2D(Point2D.Empty, size), maxItems)
        { }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Inserts an item into the QuadTree
        /// </summary>
        /// <param name="item">The item to insert</param>
        /// <remarks>Checks to see if the world needs resizing and does so if needed</remarks>
        public void Insert(QuadTreePositionItem<T> item)
        {
            // check if the world needs resizing
            if (!root.ContainsRect(item.Bounds))
            {
                Resize(new Rectangle2D(
                    Primitives.Min(root.Rect.TopLeft, item.Bounds.TopLeft) * 2,
                    Primitives.Max(root.Rect.BottomRight, item.Bounds.BottomRight) * 2));
            }

            root.Insert(item);
        }

        /// <summary>
        /// Inserts an item into the QuadTree
        /// </summary>
        /// <param name="parent">The parent of the new position item</param>
        /// <param name="position">The position of the new position item</param>
        /// <param name="size">The size of the new position item</param>
        /// <returns>A new position item</returns>
        /// <remarks>Checks to see if the world needs resizing and does so if needed</remarks>
        public QuadTreePositionItem<T> Insert(T parent, Point2D position, Size2D size)
        {
            var item = new QuadTreePositionItem<T>(parent, position, size);

            // Check if the world needs resizing
            if (!root.ContainsRect(item.Bounds))
            {
                Resize(new Rectangle2D(
                    Primitives.Min(root.Rect.TopLeft, item.Bounds.TopLeft) * 2,
                    Primitives.Max(root.Rect.BottomRight, item.Bounds.BottomRight) * 2));
            }

            root.Insert(item);

            return item;
        }

        /// <summary>
        /// Resizes the Quadtree field
        /// </summary>
        /// <param name="newWorld">The new field</param>
        /// <remarks>This is an expensive operation, so try to initialize the world to a big enough size</remarks>
        public void Resize(Rectangle2D newWorld)
        {
            // Get all of the items in the tree
            var Components = new List<QuadTreePositionItem<T>>();
            GetAllItems(ref Components);

            // Destroy the head node
            root.Destroy();
            root = null;

            // Create a new head
            root = new QuadTreeNode<T>(newWorld, maxItems, Resize);

            // Reinsert the items
            foreach (var m in Components)
            {
                root.Insert(m);
            }
        }

        /// <summary>
        /// Gets a list of items containing a specified point
        /// </summary>
        /// <param name="Point">The point</param>
        /// <param name="ItemsList">The list to add found items to (list will not be cleared first)</param>
        public void GetItems(Point2D Point, ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                root.GetItems(Point, ref ItemsList);
            }
        }

        /// <summary>
        /// Gets a list of items intersecting a specified rectangle
        /// </summary>
        /// <param name="Rect">The rectangle</param>
        /// <param name="ItemsList">The list to add found items to (list will not be cleared first)</param>
        public void GetItems(Rectangle2D Rect, ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                root.GetItems(Rect, ref ItemsList);
            }
        }

        /// <summary>
        /// Get a list of all items in the quadtree
        /// </summary>
        /// <param name="ItemsList">The list to add found items to (list will not be cleared first)</param>
        public void GetAllItems(ref List<QuadTreePositionItem<T>> ItemsList)
        {
            if (ItemsList != null)
            {
                root.GetAllItems(ref ItemsList);
            }
        }
        #endregion Methods
    }
}
