// <copyright file="ScrollEventType.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Tools
{
    /// <summary>
    /// The scroll event type enum.
    /// </summary>
    public enum ScrollEventType
    {
        /// <summary>
        /// The SmallDecrement = 0.
        /// </summary>
        SmallDecrement = 0,

        /// <summary>
        /// The SmallIncrement = 1.
        /// </summary>
        SmallIncrement = 1,

        /// <summary>
        /// The LargeDecrement = 2.
        /// </summary>
        LargeDecrement = 2,

        /// <summary>
        /// The LargeIncrement = 3.
        /// </summary>
        LargeIncrement = 3,

        /// <summary>
        /// The ThumbPosition = 4.
        /// </summary>
        ThumbPosition = 4,

        /// <summary>
        /// The ThumbTrack = 5.
        /// </summary>
        ThumbTrack = 5,

        /// <summary>
        /// The First = 6.
        /// </summary>
        First = 6,

        /// <summary>
        /// The Last = 7.
        /// </summary>
        Last = 7,

        /// <summary>
        /// The EndScroll = 8.
        /// </summary>
        EndScroll = 8
    }
}
