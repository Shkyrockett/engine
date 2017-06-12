// <copyright file="Overflows.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

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
