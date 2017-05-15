// <copyright file="MouseButtons.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
        : int
    {
        /// <summary>
        /// No mouse button was pressed.
        /// </summary>
        None = 0b0000_0000_0000_0000_0000_0000_0000,

        /// <summary>
        /// The left mouse button was pressed.
        /// </summary>
        Left = 0b0000_0001_0000_0000_0000_0000_0000,

        /// <summary>
        /// The right mouse button was pressed.
        /// </summary>
        Right = 0b0000_0010_0000_0000_0000_0000_0000,

        /// <summary>
        /// The middle mouse button was pressed.
        /// </summary>
        Middle = 0b0000_0100_0000_0000_0000_0000_0000,

        /// <summary>
        /// The back mouse button was pressed.
        /// </summary>
        Back = 0b0000_1000_0000_0000_0000_0000_0000,

        /// <summary>
        /// The forward mouse button was pressed.
        /// </summary>
        Forward = 0b0001_0000_0000_0000_0000_0000_0000,
    }
}
