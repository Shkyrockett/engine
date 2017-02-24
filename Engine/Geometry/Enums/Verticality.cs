// <copyright file="Verticality.cs" company="Shkyrockett" >
//     Copyright (c) 2015 - 2017 Shkyrockett. All rights reserved.
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
    public enum Verticality
        : byte
    {
        /// <summary>
        /// Up.
        /// </summary>
        Up = 1,

        /// <summary>
        /// Down.
        /// </summary>
        Down = 2,
    }
}
