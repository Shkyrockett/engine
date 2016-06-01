using System;
using System.Runtime.InteropServices;

namespace Engine.Tools
{
    /// <summary>
    /// Specifies constants that define which mouse button was pressed.
    /// </summary>
    [Flags]
    [ComVisible(true)]
    public enum MouseButtons
    {
        /// <summary>
        /// No mouse button was pressed.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// The left mouse button was pressed.
        /// </summary>
        Left = 0x00100000,

        /// <summary>
        /// The right mouse button was pressed.
        /// </summary>
        Right = 0x00200000,

        /// <summary>
        /// The middle mouse button was pressed.
        /// </summary>
        Middle = 0x00400000,

        /// <summary>
        /// The back mouse button was pressed.
        /// </summary>
        Back = 0x00800000,

        /// <summary>
        /// The forward mouse button was pressed.
        /// </summary>
        Forward = 0x01000000,
    }
}
