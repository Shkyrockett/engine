﻿// <copyright file="Heart.cs" company="Shkyrockett" >
//     Copyright (c) 2015 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// http://csharphelper.com/blog/2016/02/draw-parametric-heart-shaped-curve-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Heart))]
    public class Heart
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => "Heart";
    }
}