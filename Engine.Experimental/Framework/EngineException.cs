namespace Engine.Experimental;

/// <summary>
/// The engine exception class.
/// </summary>
public class EngineException
    : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    public EngineException()
        : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    /// <param name="description">The description.</param>
    public EngineException(string description)
        : base(description) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public EngineException(string message, Exception innerException)
        : base(message, innerException) { }
}
