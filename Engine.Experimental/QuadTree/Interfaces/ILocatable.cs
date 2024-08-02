namespace Engine.Experimental;

/// <summary>
/// An interface that defines and object with a location point.
/// </summary>
public interface ILocatable
{
    /// <summary>
    /// Gets the point.
    /// </summary>
    /// <value>The <see cref="Point2D"/>.</value>
    Point2D Location { get; }
}
