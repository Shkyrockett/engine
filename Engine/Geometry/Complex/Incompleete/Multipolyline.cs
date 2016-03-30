﻿// <copyright file="Multipolyline.cs" company="Shkyrockett">
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName("Multi-polyline")]
    public class Multipolyline
        : Shape
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Multipolyline";
        }
    }
}
