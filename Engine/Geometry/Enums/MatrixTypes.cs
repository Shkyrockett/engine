﻿// <copyright file="MatrixTypes.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine
{
    /// <summary>
    /// MatrixTypes
    /// </summary>
    [Flags]
    public enum MatrixTypes
        : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Identity = 0,

        /// <summary>
        /// 
        /// </summary>
        Translation = 1,

        /// <summary>
        /// 
        /// </summary>
        Scaling = 2,

        /// <summary>
        /// 
        /// </summary>
        Unknown = 4
    }
}
