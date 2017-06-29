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
    /// 
    /// </summary>
    [Flags]
    public enum Progressivity
        : sbyte
    {
        /// <summary>
        /// 
        /// </summary>
        Forward = 1,

        /// <summary>
        /// 
        /// </summary>
        Backward = 2,
    }
}
