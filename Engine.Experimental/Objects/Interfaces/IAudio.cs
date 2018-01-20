﻿// <copyright file="IAudio.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAudio
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> Lyrics { get; set; }
    }
}