﻿// <copyright file="Verticality.cs" company="Shkyrockett" >
//     Copyright © 2015 - 2020 Shkyrockett. All rights reserved.
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
    /// The verticality enum.
    /// </summary>
    [Flags]
    public enum Verticalities
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
