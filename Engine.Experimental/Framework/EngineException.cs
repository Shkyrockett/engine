using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The engine exception class.
    /// </summary>
    public class EngineException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineException"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public EngineException(string description)
            : base(description) { }
    }
}
