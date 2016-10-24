// <copyright file="ChainMember.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ChainMember
    {
        /// <summary>
        /// 
        /// </summary>
        public ChainMember Previous { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ChainMember Next { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract Point2D Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract Point2D End { get; set; }
    }
}
