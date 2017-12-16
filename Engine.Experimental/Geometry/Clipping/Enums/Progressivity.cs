// <copyright file="Progressivity.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// The progressivity enum.
    /// </summary>
    [Flags]
    public enum Progressivity
        : sbyte
    {
        /// <summary>
        /// The Forward = 1.
        /// </summary>
        Forward = 1,

        /// <summary>
        /// The Backward = 2.
        /// </summary>
        Backward = 2,
    }
}
