namespace Engine.Experimental
{
    /// <summary>
    /// An interface that defines and object with a bounding axis aligned rectangle
    /// </summary>
    public interface IBoundable
    {
        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <value>The <see cref="Rectangle2D"/>.</value>
        Rectangle2D Bounds { get; }
    }
}
