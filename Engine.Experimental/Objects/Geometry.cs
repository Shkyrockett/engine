// <copyright file="Geometry.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
    public class Geometry_
    {
        /// <summary>
        /// 
        /// </summary>
        public Shape[] Origional { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Shape[] Current { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Matrix3x2D Transform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle2D Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IMetadata Metadata { get; set; }
    }
}
