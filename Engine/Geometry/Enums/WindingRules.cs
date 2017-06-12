// <copyright file="ClipingOperations.cs" company="Shkyrockett" >
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
    /// An enumeration of polygon filling winding rules.
    /// </summary>
    public enum WindingRules
    {
        /// <summary>
        /// 
        /// </summary>
        NonZero = 0,

        /// <summary>
        /// 
        /// </summary>
        EvenOdd = 1,

        /// <summary>
        /// 
        /// </summary>
        Positive = 2,

        /// <summary>
        /// 
        /// </summary>
        Negitive = 3
    }
}
