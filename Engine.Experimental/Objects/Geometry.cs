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
    /// The geometry class.
    /// </summary>
    public class Geometry_
    {
        /// <summary>
        /// Gets or sets the origional.
        /// </summary>
        public Shape[] Origional { get; set; }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public Shape[] Current { get; set; }

        /// <summary>
        /// Gets or sets the transform.
        /// </summary>
        public Matrix3x2D Transform { get; set; }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        public Rectangle2D Bounds { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public IStyle Style { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        public IMetadata Metadata { get; set; }
    }
}
