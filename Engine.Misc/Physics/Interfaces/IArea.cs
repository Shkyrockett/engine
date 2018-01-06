// <copyright file="IArea.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IArea
    {
        /// <summary>
        /// 
        /// </summary>
        double Value { get; /*set;*/ }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
