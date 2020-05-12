// <copyright file="IShapeSegment.cs" company="Shkyrockett" >
//     Copyright © 2020 Shkyrockett. All rights reserved.
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
    /// The IShapeSegment interface.
    /// </summary>
    public interface IShapeSegment
        : IShape
    {
        /// <summary>
        /// Gets or sets the leading segment.
        /// </summary>
        /// <value>
        /// The leading.
        /// </value>
        IShapeSegment? Leading { get; set; }

        /// <summary>
        /// Gets or sets the trailing segment.
        /// </summary>
        /// <value>
        /// The trailing.
        /// </value>
        IShapeSegment? Trailing { get; set; }

        /// <summary>
        /// Gets or sets the head point.
        /// </summary>
        /// <value>
        /// The head.
        /// </value>
        Point2D? Head { get; set; }

        /// <summary>
        /// Gets or sets the tail point.
        /// </summary>
        /// <value>
        /// The tail.
        /// </value>
        Point2D? Tail { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        double Length { get; set; }

        /// <summary>
        /// Interpolates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        Point2D Interpolate(double t);
    }
}
