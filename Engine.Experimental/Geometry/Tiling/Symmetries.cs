using System;

namespace Engine.Experimental
{
    /// <summary>
    /// The symmetries enum.
    /// </summary>
    [Flags]
    public enum Symmetries
    {
        /// <summary>
        /// The Translation.
        /// </summary>
        Translation,

        /// <summary>
        /// The Reflection.
        /// </summary>
        Reflection,

        /// <summary>
        /// The Rotation.
        /// </summary>
        Rotation
    }
}
