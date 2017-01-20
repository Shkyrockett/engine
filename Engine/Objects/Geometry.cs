// <copyright file="Geometry.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
        public Matrix2x3D Transform { get; set; }

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
