// <copyright file="IIteratablePathElement.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
