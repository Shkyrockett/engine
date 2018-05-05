﻿// <copyright file="IInterpolatablePathElement.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// The Interpolatable Path Element interface.
    /// </summary>
    internal interface IInterpolatablePathElement
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        double Length { get; }

        /// <summary>
        /// Gets the interpolation min.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        double InterpolationMin { get; }

        /// <summary>
        /// Gets the interpolation max.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        double InterpolationMax { get; }

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        Point2D Iterpolate(double t);

        /// <summary>
        /// The interpolation chain.
        /// </summary>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        List<Point2D> InterpolationChain();
    }
}