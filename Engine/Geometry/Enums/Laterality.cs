// <copyright file="Laterality.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum Laterality
        : byte
    {
        /// <summary>
        /// Left.
        /// </summary>
        Left = 1,

        /// <summary>
        /// Right.
        /// </summary>
        Right = 2,
    }
}
