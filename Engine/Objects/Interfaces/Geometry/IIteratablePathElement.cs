// <copyright file="IIteratablePathElement.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IIteratablePathElement
    {
        /// <summary>
        /// 
        /// </summary>
        double Length { get; }

        /// <summary>
        /// 
        /// </summary>
        double InterpolationMin { get; }

        /// <summary>
        /// 
        /// </summary>
        double InterpolationMax { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Point2D Iterpolate(double t);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Point2D> IterpolationChain();
    }
}
