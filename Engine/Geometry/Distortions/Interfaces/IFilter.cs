﻿// <copyright file="IFilter.cs" company="Shkyrockett" >
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
    /// 
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        Point2D Process(Point2D point);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S">Shape type.</typeparam>
        /// <typeparam name="T">Return type.</typeparam>
        /// <param name="shape"></param>
        /// <returns></returns>
        T Process<S, T>(S shape)
            where S : Shape
            where T : Shape;
    }
}