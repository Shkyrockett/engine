﻿// <copyright file="ScrollEventType.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public enum ScrollEventType
    {
        /// <summary>
        /// 
        /// </summary>
        SmallDecrement = 0,

        /// <summary>
        /// 
        /// </summary>
        SmallIncrement = 1,

        /// <summary>
        /// 
        /// </summary>
        LargeDecrement = 2,

        /// <summary>
        /// 
        /// </summary>
        LargeIncrement = 3,

        /// <summary>
        /// 
        /// </summary>
        ThumbPosition = 4,

        /// <summary>
        /// 
        /// </summary>
        ThumbTrack = 5,

        /// <summary>
        /// 
        /// </summary>
        First = 6,

        /// <summary>
        /// 
        /// </summary>
        Last = 7,

        /// <summary>
        /// 
        /// </summary>
        EndScroll = 8
    }
}