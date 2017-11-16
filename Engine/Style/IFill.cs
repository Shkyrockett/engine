// <copyright file="IBrush.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    public interface IFill
    {
        /// <summary>
        /// 
        /// </summary>
        IColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        FillMode FillMode { get; set; }
    }
}
