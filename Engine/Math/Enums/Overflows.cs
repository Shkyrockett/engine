// <copyright file="Overflows.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public enum Overflows
    {
        /// <summary>
        /// No Overflow protection.
        /// </summary>
        None,

        /// <summary>
        /// Clamp values between min and max.
        /// </summary>
        Clamp,

        /// <summary>
        /// Wrap values back to min, when past max.
        /// </summary>
        Wrap
    }
}
