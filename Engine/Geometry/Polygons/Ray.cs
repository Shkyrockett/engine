﻿// <copyright file="Ray.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName("Ray")]
    public class Ray
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Ray(Point2D location, Vector2D direction)
        {
            Location = location;
            Direction = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        public Point2D Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector2D Direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null) return "Ray";
            return "Ray";
        }
    }
}
