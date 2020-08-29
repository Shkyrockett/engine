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

using static Engine.Maths;

namespace Engine.Experimental
{
    /// <summary>
    /// A position item in a quadtree
    /// </summary>
    /// <typeparam name="T">The type of the QuadTree item's parent</typeparam>
    public class QuadTreePositionItem<T>
        where T : IBoundable
    {
        #region Fields
        /// <summary>
        /// The center position of this item
        /// </summary>
        private Point2D location;

        /// <summary>
        /// The size of this item
        /// </summary>
        private Size2D size;

        /// <summary>
        /// The rectangle containing this item
        /// </summary>
        private readonly Rectangle2D bounds;

        /// <summary>
        /// The parent of this item
        /// </summary>
        /// <remarks><para>The Parent accessor is used to gain access to the item controlling this position item</para></remarks>
        private readonly T parent;
        #endregion Fields

        #region Delegates
        /// <summary>
        /// A movement handler delegate
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void MoveHandler(QuadTreePositionItem<T> positionItem);

        /// <summary>
        /// A destruction handler delegate - fired when the item is destroyed
        /// </summary>
        /// <param name="positionItem">The item that fired the event</param>
        public delegate void DestroyHandler(QuadTreePositionItem<T> positionItem);
        #endregion Delegates

        #region Events
        /// <summary>
        /// Event handler for the move action
        /// </summary>
        public event MoveHandler Move;

        /// <summary>
        /// Event handler for the destroy action
        /// </summary>
        public event DestroyHandler Destroy;
        #endregion Events

        #region Constructors
        /// <summary>
        /// Creates a position item in a QuadTree
        /// </summary>
        /// <param name="parent">The parent of this item</param>
        /// <param name="location">The position of this item</param>
        /// <param name="size">The size of this item</param>
        public QuadTreePositionItem(T parent, Point2D location, Size2D size)
        {
            bounds = new Rectangle2D(0f, 0f, 1f, 1f);

            this.parent = parent;
            this.location = location;
            this.size = size;
            OnMove();
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the center position of this item
        /// </summary>
        public Point2D Location
        {
            get { return location; }
            set
            {
                location = value;
                OnMove();
            }
        }

        /// <summary>
        /// Gets or sets the size of this item
        /// </summary>
        public Size2D Size
        {
            get { return size; }
            set
            {
                size = value;
                bounds.TopLeft = location - (size * OneHalf);
                bounds.BottomRight = location + (size * OneHalf);
                OnMove();
            }
        }

        /// <summary>
        /// Gets a rectangle containing this item
        /// </summary>
        public Rectangle2D Bounds
            => bounds;

        /// <summary>
        /// Gets the parent of this item
        /// </summary>
        public T Parent
            => parent;
        #endregion Properties

        #region Event Handlers
        /// <summary>
        /// Handles the move event
        /// </summary>
        protected void OnMove()
        {
            // Update rectangles
            bounds.TopLeft = location - (size * OneHalf);
            bounds.BottomRight = location + (size * OneHalf);

            // Call event handler
            Move?.Invoke(this);
        }

        /// <summary>
        /// Handles the destroy event
        /// </summary>
        protected void OnDestroy()
            => Destroy?.Invoke(this);
        #endregion Event Handlers

        #region Methods
        /// <summary>
        /// Destroys this item and removes it from the QuadTree
        /// </summary>
        public void Delete()
            => OnDestroy();
        #endregion Methods
    }
}
