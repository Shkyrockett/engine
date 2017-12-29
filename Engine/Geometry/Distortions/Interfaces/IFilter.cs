// <copyright file="IFilter.cs" company="Shkyrockett" >
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
    /// The IFilter interface.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        Point2D Process(Point2D point);

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns>The <see cref="T"/>.</returns>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        T Process<S, T>(S shape)
            where S : Shape
            where T : Shape;
    }
}
