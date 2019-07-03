// <copyright file="Directionality.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
    /// Text reading direction.
    /// </summary>
    public enum VerticalDirectionality
        : byte
    {
        /// <summary>
        /// Right to left reading direction.
        /// </summary>
        TopToBottom,

        /// <summary>
        /// Left to right reading direction.
        /// </summary>
        BottomToTop
    }
}
