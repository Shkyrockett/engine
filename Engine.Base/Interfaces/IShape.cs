// <copyright file="IShape.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The IShape interface.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IShape
    {
        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        /// <value>The <see cref="Rectangle2D"/>.</value>
        Rectangle2D Bounds { get; set; }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool Contains(Point2D point);
    }
}
